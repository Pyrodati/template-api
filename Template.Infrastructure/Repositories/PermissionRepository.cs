using Template.Domain.Entities;
using Template.Domain.Repositories;
using Template.Infrastructure.Implementations;

namespace Template.Infrastructure.Repositories;

public class PermissionRepository(AppDbContext dbContext) : Repository<Permission>(dbContext), IPermissionRepository
{
}
