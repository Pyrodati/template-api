namespace Template.Application.Features.Role.GetPermissionsByRoleId;

public class GetPermissionsByRoleIdResponse : List<GetPermissionsByRoleIdResponse.GetPermissionsByRoleIdRecordResponse>
{
    public sealed record GetPermissionsByRoleIdRecordResponse(int Id, string Name);
}
