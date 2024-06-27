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
    public class DeleteRoleCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
    public class DeleteRoleCommandHander(IRole role) : IRequestHandler<DeleteRoleCommand, Result>
    {
        private readonly IRole _role = role;
        public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            return await _role.DeleteRoleAsync(request, cancellationToken);
        }
    }
}
