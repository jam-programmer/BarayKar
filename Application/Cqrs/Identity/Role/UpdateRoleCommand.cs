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
    public class UpdateRoleCommand : IRequest<Result>
    {
        [Required(ErrorMessage = ValidationMessage.RequirdEnglishName)]
        public required string Name { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdPersianName)]
        public required string PresianName { set; get; }
        public Guid Id { set; get; }
    }
    public class UpdateRoleCommandHandler(IRole role) : IRequestHandler<UpdateRoleCommand, Result>
    {
        private readonly IRole _role = role;
        public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            return await _role.UpdateRoleAsync(request, cancellationToken);
        }
    }
}
