using Template.Domain.Entities;

namespace Template.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<HashSet<string>> GetPermissionsAsync(int userId);
    Task<User?> GetByIdWithRolePermissions(int userId);
}
