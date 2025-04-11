using System.ComponentModel.DataAnnotations;

namespace Template.Application.Features.Role.UpdateRole;

public sealed record UpdateRoleRequest
{
    [Required]
    public string Name { get; init; } = string.Empty;
}
