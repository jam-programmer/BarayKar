using Application.Common.Interfaces;
using Application.Common.Model;
using Application.Services.User;
using MediatR;


namespace Application.Cqrs.Identity.User
{
    public class GetUsersQuery : IRequest<PaginatedList<UserViewModel>>
    {
        public IPagination? pagination { set; get; }
    }

    public class GetUsersQueryHandler(IUser user) : IRequestHandler<GetUsersQuery, PaginatedList<UserViewModel>>
    {
        private readonly IUser _user = user;
        public async Task<PaginatedList<UserViewModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _user.GetUsersAsync<UserViewModel>(request.pagination!, cancellationToken);
          
        }
    }

    public class UserViewModel
    {
        public Guid Id { set; get; }
        public virtual string? FirstName { set; get; }
        public virtual string? LastName { set; get; }
        public virtual string? Avatar { set; get; }
        public virtual string? PhoneNumber { get; set; }
        public virtual string? UserName { get; set; }
        public virtual string? Email { get; set; }
        public virtual bool EmailConfirmed { get; set; }
    }
}
