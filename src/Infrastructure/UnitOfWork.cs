using Application.Common;
using Infrastructure.Persistence;

namespace Infrastructure;

// RXC.Infrastructure.Persistence
public class UnitOfWork : IUnitOfWork
{
    private readonly RxcDbContext _context;

    public UnitOfWork(RxcDbContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}