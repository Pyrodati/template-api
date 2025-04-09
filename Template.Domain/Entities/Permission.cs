using Template.Domain.Primitives;

namespace Template.Domain.Entities;

public partial class Permission : Entity
{
    public string Name { get; set; } = string.Empty;

    // Navigation properties
    public virtual ICollection<Role> Roles { get; set; } = [];
}
