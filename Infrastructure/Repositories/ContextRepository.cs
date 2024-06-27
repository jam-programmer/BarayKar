using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;


namespace Infrastructure.Repositories
{
    public class ContextRepository(SqlServerContext context) : IContext
    {
        private SqlServerContext _context = context;
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public void ClearTracker()
        {
            _context.ChangeTracker.Clear();
        }
    }
}
