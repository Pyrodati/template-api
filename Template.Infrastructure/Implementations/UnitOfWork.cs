using Microsoft.EntityFrameworkCore.Storage;
using System.Security;
using Template.Domain.Repositories;

namespace Template.Infrastructure.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _appDbContext;

    public UnitOfWork(AppDbContext appDbContext,
        IPermissionRepository permissionRepository,
        IRoleRepository roleRepository,
        IUserRepository userRepository)
    {
        _appDbContext = appDbContext;
        PermissionRepository = permissionRepository;
        RoleRepository = roleRepository;
        UserRepository = userRepository;
    }

    public IPermissionRepository PermissionRepository { get; private set; }
    public IRoleRepository RoleRepository { get; private set; }
    public IUserRepository UserRepository { get; private set; }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _appDbContext.SaveChangesAsync(cancellationToken) > 0;
    }
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _appDbContext.Database.BeginTransactionAsync();
    }
    public void Dispose()
    {
        _appDbContext.Dispose();
    }
}
