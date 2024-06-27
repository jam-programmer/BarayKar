using Application.Common.Const;
using static Dapper.SqlMapper;

namespace Application.Common.Interfaces
{
    public interface IDapper
    {
        Task<Result> ExecuteStoredProcedureAsync<TEntity>(string storedProcedure,
            List<StoredProcedureParmeter> StoredProcedureParmeters);
    }
}
