using Application.Common.ViewModel;
using Domain.Entities.System.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Cqrs.Identity.User
{
    public class GetUserItemsQuery : IRequest<List<ItemViewModel<string, string>>>
    {
    }
    public class GetUserItemsQueryHandler (UserManager<UserEntity> manager):
        IRequestHandler<GetUserItemsQuery, List<ItemViewModel<string, string>>>
    {
        private readonly UserManager<UserEntity> _manager = manager;
        public async Task<List<ItemViewModel<string, string>>> 
            Handle(GetUserItemsQuery request, CancellationToken cancellationToken)
        {
            var query =  _manager.Users.AsQueryable();
            return await query.Select(s => new ItemViewModel<string, string>
            {
                Id = s.Id.ToString(),
                Title = s.FirstName +" "+s.LastName
            }).ToListAsync();
        }
    }
}
