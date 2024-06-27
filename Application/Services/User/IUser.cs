
using Application.Common.Model;
using Application.Common;
using Application.Cqrs.Identity.User;
using Domain.Entities.System.Identity;
using Application.Common.Interfaces;

namespace Application.Services.User
{
    public interface IUser
    {
        ///// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TList"></typeparam>
        /// <param name="pageOption"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<PaginatedList<TModel>> GetUsersAsync<TModel>(IPagination
          pageOption, CancellationToken cancellation);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result> InsertUserAsync(InsertUserCommand command, CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result> GetUserByIdAsync(GetUserQuery query, CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result> UpdateUserAsync(UpdateUserCommand command, CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roles"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result> ChangeUserRoleAsync(UserEntity user, List<string> roles, CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<Result> ChangeUserPasswordAsync(UserEntity user, string password, CancellationToken cancellation);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result> DeleteUserAsync(DeleteUserCommand command, CancellationToken cancellationToken);
    }
}