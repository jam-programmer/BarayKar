using Application.Common;
using Application.Common.Const;
using Application.Common.Interfaces;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class DapperRepository : IDapper
    {
        private readonly ILogger<DapperRepository> _logger;
        private readonly SqlConnection _connection;
        public DapperRepository(ILogger<DapperRepository> logger)
        {
            _logger = logger;
            _connection = new SqlConnection("Server=162.55.86.86,2022;Database=tetronjo_DataBase;User Id=tetronjo_Sm;Password=b40m&Q5z6;TrustServerCertificate=true;Trusted_Connection=True;Integrated Security=False;Encrypt=False;MultipleActiveResultSets=True");
        }



        public async Task<Result> ExecuteStoredProcedureAsync<TEntity>
            (string storedProcedure, List<StoredProcedureParmeter>
            StoredProcedureParmeters)
        {
            if (string.IsNullOrEmpty(storedProcedure))
            {
                _logger.LogError($"نام storedProcedure وارد نشده است.");
                return Result.Fail();
            }
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                if (StoredProcedureParmeters != null)
                {
                    foreach (var item in StoredProcedureParmeters)
                    {

                        parameters.Add(item.ParmeterName, item.ParmeterValue, item.ParmeterType, ParameterDirection.Input);
                    }
                }

                await _connection.OpenAsync();
                var model = await _connection.QueryAsync<TEntity>
                    (storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                return Result.Success(model.ToList());
            }
            catch (Exception e)
            {
                _logger.LogError($"خطای فراخوانی از دیتابیس رخ داده است. - {storedProcedure} - {e.Message}");

            }
            return Result.Fail();
        }
    }
}
