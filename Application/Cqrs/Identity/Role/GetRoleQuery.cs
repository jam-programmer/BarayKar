using Application.Common;
using Application.Services.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cqrs.Identity.Role
{
    public class GetRoleQuery : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
    public class GetRoleQueryHandler(IRole role) : IRequestHandler<GetRoleQuery, Result>
    {
        private readonly IRole _role = role;
        public async Task<Result> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            return await _role.GetRoleByIdAsync(request, cancellationToken);
        }
    }
}
