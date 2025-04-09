using System.ComponentModel.DataAnnotations;

namespace Template.Domain.Primitives;

public abstract class Entity
{
    [Key]
    public int Id { get; set; }
}
