using Template.Domain.Entities;

namespace Template.Domain.Repositories;

public interface IRoleRepository : IRepository<Role>
{
    Task<List<Role>?> GetRolesByUserIdAsync(int userId);
    Task<Role?> GetWithPermissionsAsync(int id);
}
