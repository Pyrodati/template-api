using Microsoft.EntityFrameworkCore.Storage;

namespace Template.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    IPermissionRepository PermissionRepository { get; }
    IRoleRepository RoleRepository { get; }
    IUserRepository UserRepository { get; }
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync();
}
