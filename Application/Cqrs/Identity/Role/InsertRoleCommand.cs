using Application.Common;
using Application.Services.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cqrs.Identity.Role
{
    public class InsertRoleCommand : IRequest<Result>
    {
        [Required(ErrorMessage = ValidationMessage.RequirdEnglishName)]
        public required string Name { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdPersianName)]
        public required string PresianName { set; get; }
    }
    public class InsertRoleCommandHandler(IRole role) : IRequestHandler<InsertRoleCommand, Result>
    {
        private readonly IRole _role = role;
        public async Task<Result> Handle(InsertRoleCommand request, CancellationToken cancellationToken)
        {
            return await _role.InsertRoleAsync(request, cancellationToken);
        }
    }
}
