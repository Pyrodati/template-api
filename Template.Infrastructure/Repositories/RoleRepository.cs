using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;
using Template.Domain.Repositories;
using Template.Infrastructure.Implementations;

namespace Template.Infrastructure.Repositories;

public class RoleRepository(AppDbContext dbcontext) : Repository<Role>(dbcontext), IRoleRepository
{
    public async Task<List<Role>?> GetRolesByUserIdAsync(int userId)
    {
        return await appDbContext
            .Set<Role>()
            .Include(role => role.Users)
            .Where(role => role.Users.Any(user => user.Id == userId))
            .ToListAsync();
    }

    public async Task<Role?> GetWithPermissionsAsync(int id)
    {
        return await appDbContext
            .Set<Role>()
            .Include(role => role.Permissions)
            .Where(role => role.Id == id)
            .SingleOrDefaultAsync();
    }
}
