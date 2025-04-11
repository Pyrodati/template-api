using Template.Application.Features.Role.GetPermissionsByRoleId;
using Template.Application.Features.Role.GetRoleById;
using Template.Domain.Entities;

namespace Template.Application.Mappers;

public static class RoleMapper
{
    public static GetRoleByIdResponse ToGetRoleByIdResponse(this Role input)
    {
        return new GetRoleByIdResponse(input.Id, input.Name);
    }

    public static GetPermissionsByRoleIdResponse ToGetPermissionsByRoleIdResponse(this Role role)
    {
        var response = new GetPermissionsByRoleIdResponse();
        response.AddRange(role.Permissions.Select(permission => new GetPermissionsByRoleIdResponse.GetPermissionsByRoleIdRecordResponse(
            permission.Id,
            permission.Name
            )));

        return response;
    }
}
