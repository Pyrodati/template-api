using Microsoft.AspNetCore.Authorization;
using Template.Domain.Enums;

namespace Template.Infrastructure.Authentification;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(RightPermission permission)
    : base(policy: permission.ToString())
    {

    }
}
