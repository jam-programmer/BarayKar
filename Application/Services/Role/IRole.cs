using Application.Common.Model;
using Application.Common.ViewModel;
using Application.Common;
using Application.Cqrs.Identity.Role;
using Application.Common.Interfaces;

namespace Application.Services.Role
{
    public interface IRole
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<Result> InsertRoleAsync(InsertRoleCommand command, CancellationToken cancellation);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TList"></typeparam>
        /// <param name="pageOption"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<PaginatedList<TModel>> GetRolesAsync<TModel>(IPagination
            pageOption, CancellationToken cancellation);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<Result> GetRoleByIdAsync(GetRoleQuery query, CancellationToken cancellation);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<Result> UpdateRoleAsync(UpdateRoleCommand command, CancellationToken cancellation);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<Result> DeleteRoleAsync(DeleteRoleCommand command, CancellationToken cancellation);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<List<ItemViewModel<string, string>>> GetRoleItemsAsync
            (CancellationToken cancellation);
    }
}
