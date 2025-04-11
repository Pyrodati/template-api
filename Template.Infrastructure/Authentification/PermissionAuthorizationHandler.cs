using Microsoft.AspNetCore.Authorization;

namespace Template.Infrastructure.Authentification;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (!context.User.Identity?.IsAuthenticated ?? false)
        {
            return Task.CompletedTask;
        }

        HashSet<string> permissions = context
            .User
            .Claims
            .Where(x => x.Type == "permissions")
            .Select(x => x.Value)
            .ToHashSet();

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }
}
