using Application.Common.Interfaces;
using Application.Common.Model;
using Application.Services.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cqrs.Identity.Role
{
    public class GetRolesQuery : IRequest<PaginatedList<RoleViewModel>>
    {
        public IPagination? pagination { set; get; }
    }

    public class GetRolesQueryHandler(IRole role) : IRequestHandler<GetRolesQuery, PaginatedList<RoleViewModel>>
    {
        private readonly IRole _role = role;
        public async Task<PaginatedList<RoleViewModel>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            return await _role.GetRolesAsync<RoleViewModel>(request.pagination!, cancellationToken);
        }
    }

    public class RoleViewModel
    {
        public string? Id { set; get; }
        public string? Name { set; get; }
        public string? PresianName { set; get; }
    }
}
