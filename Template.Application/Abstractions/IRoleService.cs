using Template.Application.Features.Role.CreateRole;
using Template.Application.Features.Role.GetPermissionsByRoleId;
using Template.Application.Features.Role.GetRoleById;
using Template.Application.Features.Role.UpdateRole;
using Template.Application.Features.Role.UpdateRolePermissions;
using Template.Domain.Shared;

namespace Template.Application.Abstractions;

public interface IRoleService
{
    Task<Result<GetRoleByIdResponse>> GetRoleByIdAsync(int id);
    Task<Result<GetPermissionsByRoleIdResponse>> GetPermissionsByRoleIdAsync(int id);
    Task CreateRoleAsync(CreateRoleRequest request);
    Task UpdateRolePermissionsAsync(int id, UpdateRolePermissionsRequest request);
    Task UpdateRoleAsync(int id, UpdateRoleRequest request);
    Task DeleteRoleAsync(int id);
}
