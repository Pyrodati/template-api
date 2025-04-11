using System.ComponentModel.DataAnnotations;

namespace Template.Application.Features.User.UpdateUserPassword;

public sealed record UpdateUserPasswordRequest
{
    [Required]
    public string NewPassword { get; init; } = string.Empty;
}
