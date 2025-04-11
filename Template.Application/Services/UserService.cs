using Template.Application.Abstractions;
using Template.Application.Features.User.CreateUser;
using Template.Application.Features.User.GetUserById;
using Template.Application.Features.User.Login;
using Template.Application.Features.User.UpdateUser;
using Template.Application.Features.User.UpdateUserPassword;
using Template.Application.Mappers;
using Template.Domain.Authentification;
using Template.Domain.Entities;
using Template.Domain.Exceptions;
using Template.Domain.Managers;
using Template.Domain.Repositories;
using Template.Domain.Shared;

namespace Template.Application.Services;

public class UserService(IUnitOfWork unitOfWork, IPasswordManager passwordManager, IJwtProvider jwtProvider) : IUserService, IService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPasswordManager _passwordManager = passwordManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    public async Task<Result<LoginResponse>> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken)
    {
        bool isValidUser = false;

        var user = await _unitOfWork.UserRepository.GetAsync(x => x.Email == loginRequest.email, cancellationToken);

        if (user is null)
            throw new NotFoundException("Invalid login or password");

        await Task.Run(() =>
        {
            isValidUser = _passwordManager.VerifyHashedPassword(user.Password, loginRequest.password);
        }, cancellationToken);

        if (!isValidUser)
            throw new NotFoundException("Invalid login or password");

        if (!user.IsActive)
            throw new ForbiddenException("User is not active");

        var permissions = await _unitOfWork.UserRepository.GetPermissionsAsync(user.Id);

        string token = _jwtProvider.Generate(user, permissions);

        LoginResponse loginResponse = new(token);

        return Result<LoginResponse>.Success(loginResponse);
    }

    public async Task<Result<GetUserByIdResponse>> GetUserByIdAsync(int id)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

        if (user is null)
            throw new NotFoundException($"The user with Id {id} was not found");

        var response = user.ToGetUserByIdResponse();

        return Result<GetUserByIdResponse>.Success(response);
    }

    public async Task CreateUserAsync(CreateUserRequest request)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync();

        if (await _unitOfWork.UserRepository.Exists(user => user.Email == request.Email))
            throw new ConflictException($"The email {request.Email} already exist");

        User entity = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = _passwordManager.HashPassword(request.Password),
            IsActive = request.IsActive
        };

        await _unitOfWork.UserRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        foreach (var roleId in request.RoleIds)
        {
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(roleId)
                ?? throw new NotFoundException($"The role with Id {roleId} was not found");

            role.Users.Add(entity);
        }
        await _unitOfWork.SaveChangesAsync();
        transaction.Commit();
    }

    public async Task UpdateUserAsync(int id, UpdateUserRequest request)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync();

        // Base informations
        var user = await _unitOfWork.UserRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"The user with id {id} was not found");

        if (user.Email != request.Email && await _unitOfWork.UserRepository.Exists(user => user.Email == request.Email))
            throw new ConflictException($"Email {request.Email} already exist");

        user.Email = request.Email;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.IsActive = request.IsActive;

        await _unitOfWork.UserRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();

        // Roles to add
        var userRoles = await _unitOfWork.RoleRepository.GetRolesByUserIdAsync(user.Id);
        if (userRoles is not null)
        {
            var idsRolesToAdd = request.RoleIds.Where(roleid => !userRoles.Any(role => role.Id == roleid));

            if (idsRolesToAdd is not null && idsRolesToAdd.Any())
            {
                foreach (var idRoleToAdd in idsRolesToAdd)
                {
                    var role = await _unitOfWork.RoleRepository.GetAsync(x => x.Id == idRoleToAdd)
                        ?? throw new NotFoundException($"The role with Id {idRoleToAdd} was not found");

                    role.Users.Add(user);
                }
            }
        }

        //roles to remove
        var rolesToRemove = userRoles?.Where(userRole => request.RoleIds == null || !request.RoleIds.Any() || !request.RoleIds.Any(x => x == userRole.Id));

        if (rolesToRemove is not null)
        {
            foreach (var roleToRemove in rolesToRemove)
            {
                var role = await _unitOfWork.RoleRepository.GetAsync(x => x.Id == roleToRemove.Id)
                    ?? throw new NotFoundException($"The role with Id {roleToRemove.Id} was not found");

                role.Users.Remove(user);
            }
        }

        await _unitOfWork.SaveChangesAsync();

        transaction.Commit();
    }

    public async Task UpdateUserPasswordAsync(int id, UpdateUserPasswordRequest request)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"The user with id {id} was not found");

        user.Password = _passwordManager.HashPassword(request.NewPassword);

        await _unitOfWork.UserRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"The user with Id {id} was not found");

        await _unitOfWork.UserRepository.DeleteAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }
}
