using System.ComponentModel.DataAnnotations;

namespace Template.Application.Features.Role.CreateRole;

public sealed record CreateRoleRequest
{
    [Required]
    public string Name { get; init; } = string.Empty;
}
