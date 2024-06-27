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
    public class GetUserQuery : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
    public class GetUserQueryHandler(IUser user) : IRequestHandler<GetUserQuery, Result>
    {
        private readonly IUser _user = user;
        public async Task<Result> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _user.GetUserByIdAsync(request, cancellationToken);
        }
    }
}
