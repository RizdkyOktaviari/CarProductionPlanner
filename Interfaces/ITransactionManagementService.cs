using Microsoft.EntityFrameworkCore.Storage;

namespace AspnetCoreMvcFull.Interfaces
{
  public interface ITransactionManagementService
  {
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitTransactionAsync(IDbContextTransaction transaction);
    Task RollbackTransactionAsync(IDbContextTransaction transaction);
  }
}
