using Application.Common;
using Application.Services.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cqrs.Identity.User
{
    public class DeleteUserCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
    public class DeleteUserCommandHandler(IUser user) : IRequestHandler<DeleteUserCommand, Result>
    {
        private readonly IUser _user = user;
        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await _user.DeleteUserAsync(request, cancellationToken);
        }
    }
}
