namespace Template.Application.Features.Role.UpdateRolePermissions;

public sealed record UpdateRolePermissionsRequest
{
    public List<int> PermissionsIds { get; init; } = [];
}

