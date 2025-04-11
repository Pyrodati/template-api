using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;
using Template.Domain.Repositories;
using Template.Infrastructure.Implementations;

namespace Template.Infrastructure.Repositories;

public class UserRepository(AppDbContext dbContext) : Repository<User>(dbContext), IUserRepository
{
    public async Task<HashSet<string>> GetPermissionsAsync(int userId)
    {
        ICollection<Role>[] roles = await appDbContext.Set<User>()
            .Where(user => user.Id == userId)
            .Include(user => user.Roles)
            .ThenInclude(role => role.Permissions)
            .Select(user => user.Roles)
            .ToArrayAsync();

        return roles
            .SelectMany(role => role)
            .SelectMany(role => role.Permissions)
            .Select(permission => permission.Name)
            .ToHashSet();
    }

    public async Task<User?> GetByIdWithRolePermissions(int userId)
    {
        return await appDbContext
            .Set<User>()
            .Include(user => user.Roles)
            .ThenInclude(role => role.Permissions)
            .SingleOrDefaultAsync(user => user.Id == userId);
    }
}
