using Template.Application.Abstractions;
using Template.Application.Features.Role.CreateRole;
using Template.Application.Features.Role.GetPermissionsByRoleId;
using Template.Application.Features.Role.GetRoleById;
using Template.Application.Features.Role.UpdateRole;
using Template.Application.Features.Role.UpdateRolePermissions;
using Template.Application.Mappers;
using Template.Domain.Entities;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;
using Template.Domain.Shared;

namespace Template.Application.Services;

public class RoleService(IUnitOfWork unitOfWork) : IRoleService, IService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<GetRoleByIdResponse>> GetRoleByIdAsync(int id)
    {
        var role = await _unitOfWork.RoleRepository.GetByIdAsync(id);

        if (role is null)
            throw new NotFoundException($"The role with Id {id} was not found");

        var response = role.ToGetRoleByIdResponse();

        return Result<GetRoleByIdResponse>.Success(response);
    }

    public async Task<Result<GetPermissionsByRoleIdResponse>> GetPermissionsByRoleIdAsync(int id)
    {
        var role = await _unitOfWork.RoleRepository.GetWithPermissionsAsync(id)
            ?? throw new NotFoundException($"The role with Id {id} was not found");

        var response = role.ToGetPermissionsByRoleIdResponse();

        return Result<GetPermissionsByRoleIdResponse>.Success(response);
    }

    public async Task CreateRoleAsync(CreateRoleRequest request)
    {
        if (await _unitOfWork.RoleRepository.Exists(user => user.Name == request.Name))
            throw new ConflictException($"The role {request.Name} already exist");

        Role role = new()
        {
            Name = request.Name
        };

        await _unitOfWork.RoleRepository.AddAsync(role);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateRolePermissionsAsync(int id, UpdateRolePermissionsRequest request)
    {
        var role = await _unitOfWork.RoleRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"The role with id {id} was not found");

        // Add permissions
        var rolePermissions = (await _unitOfWork.RoleRepository.GetWithPermissionsAsync(id))?.Permissions;
        if (rolePermissions is not null)
        {
            var idsPermissionsToAdd = request.PermissionsIds.Where(permissionid => !rolePermissions.Any(permission => permission.Id == permissionid));
            if (idsPermissionsToAdd is not null && idsPermissionsToAdd.Any())
            {
                foreach (var idPermissionToAdd in idsPermissionsToAdd)
                {
                    var permission = await _unitOfWork.PermissionRepository.GetAsync(x => x.Id == idPermissionToAdd)
                        ?? throw new NotFoundException($"The permission with Id {idPermissionToAdd} was not found");
                    role.Permissions.Add(permission);
                }
            }
        }

        //Delete permissions
        var permissionsToRemove = rolePermissions?
            .Where(rolePermission => request.PermissionsIds == null || !request.PermissionsIds.Any() || !request.PermissionsIds.Any(x => x == rolePermission.Id))
            .ToList();

        if (permissionsToRemove is not null && permissionsToRemove.Any())
        {
            foreach (var permissionToRemove in permissionsToRemove)
            {
                var permission = await _unitOfWork.PermissionRepository.GetByIdAsync(permissionToRemove.Id)
                    ?? throw new NotFoundException($"The permission with Id {permissionToRemove.Id} was not found");

                role.Permissions.Remove(permission);
            }
        }

        await _unitOfWork.RoleRepository.UpdateAsync(role);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateRoleAsync(int id, UpdateRoleRequest request)
    {
        var role = await _unitOfWork.RoleRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"The role with id {id} was not found");

        if (role.Name != request.Name && await _unitOfWork.RoleRepository.Exists(user => user.Name == request.Name))
            throw new ConflictException($"Role {request.Name} already exist");

        role.Name = request.Name;

        await _unitOfWork.RoleRepository.UpdateAsync(role);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteRoleAsync(int id)
    {
        var role = await _unitOfWork.RoleRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"The role with Id {id} was not found");

        await _unitOfWork.RoleRepository.DeleteAsync(role);
        await _unitOfWork.SaveChangesAsync();
    }
}
