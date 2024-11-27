using AspnetCoreMvcFull.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace AspnetCoreMvcFull.Services
{
  public class TransactionManagementService : ITransactionManagementService
  {
    private readonly ApplicationDbContext _context;
    public TransactionManagementService(ApplicationDbContext context)
    {
      _context = context;
    }
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
      return await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
      await transaction.CommitAsync();
    }

    public async Task RollbackTransactionAsync(IDbContextTransaction transaction)
    {
      await transaction.RollbackAsync();
    }
  }
}
