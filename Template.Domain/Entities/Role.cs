using Template.Domain.Primitives;

namespace Template.Domain.Entities;

public partial class Role : Entity
{
    public string Name { get; set; } = string.Empty;

    // Navigation properties
    public virtual ICollection<Permission> Permissions { get; set; } = [];
    public virtual ICollection<User> Users { get; set; } = [];
}
