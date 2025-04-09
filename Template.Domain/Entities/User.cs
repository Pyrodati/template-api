using Template.Domain.Primitives;

namespace Template.Domain.Entities;

public partial class User : Entity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;

    // Navigation properties
    public virtual ICollection<Role> Roles { get; set; } = [];
}
