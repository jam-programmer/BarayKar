using Application.Common.ViewModel;
using Application.Services.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cqrs.Identity.Role
{
    public class GetRoleItemsQuery : IRequest<List<ItemViewModel<string, string>>>
    {
    }
    public class GetRoleItemQueryHandler(IRole role) : IRequestHandler<GetRoleItemsQuery, List<ItemViewModel<string, string>>>
    {
        private readonly IRole _role = role;
        public async Task<List<ItemViewModel<string, string>>> Handle(GetRoleItemsQuery request, CancellationToken cancellationToken)
        {
            return await _role.GetRoleItemsAsync(cancellationToken);
        }
    }
}
