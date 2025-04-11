using System.ComponentModel.DataAnnotations;

namespace Template.Application.Features.User.UpdateUser;

public sealed record UpdateUserRequest
{
    [Required]
    public string FirstName { get; init; } = string.Empty;

    [Required]
    public string LastName { get; init; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Required]
    public bool IsActive { get; init; } = false;

    public List<int> RoleIds { get; init; } = [];
}
