using Template.Application.Features.User.CreateUser;
using Template.Application.Features.User.GetUserById;
using Template.Application.Features.User.Login;
using Template.Application.Features.User.UpdateUser;
using Template.Application.Features.User.UpdateUserPassword;
using Template.Domain.Shared;

namespace Template.Application.Abstractions;

public interface IUserService
{
    Task<Result<LoginResponse>> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken);
    Task<Result<GetUserByIdResponse>> GetUserByIdAsync(int id);
    Task CreateUserAsync(CreateUserRequest request);
    Task UpdateUserAsync(int id, UpdateUserRequest request);
    Task UpdateUserPasswordAsync(int id, UpdateUserPasswordRequest newPassword);
    Task DeleteUserAsync(int id);
}
