using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Common.Interfaces
{
    public interface IContext
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
        void ClearTracker();
    }
}
