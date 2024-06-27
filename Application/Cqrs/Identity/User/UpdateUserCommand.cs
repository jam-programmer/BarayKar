using Application.Common;
using Application.Services.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cqrs.Identity.User
{
    public class UpdateUserCommand : IRequest<Result>
    {
        public string Avatar { set; get; }
        public IFormFile? AvatarFile { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdFirstName)]
        public required string FirstName { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdLastName)]
        public required string LastName { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdNationalCode)]
        public virtual string? UserName { get; set; }
        [Required(ErrorMessage = ValidationMessage.RequirdEmail)]
        public virtual string? Email { get; set; }
        //[Required(ErrorMessage = ValidationMessage.RequirdPassword)]
        public virtual string? Password { get; set; }
        [Required(ErrorMessage = ValidationMessage.RequirdPhoneNumber)]
        public virtual string? PhoneNumber { get; set; }
        [Required(ErrorMessage = ValidationMessage.RequirdRoles)]
        public List<string>? Roles { get; set; }
        public Guid Id { set; get; }
        public virtual bool EmailConfirmed { get; set; }
        public virtual bool PhoneNumberConfirmed { get; set; }
        public bool Active
        {
            get => EmailConfirmed && PhoneNumberConfirmed;
            set
            {
                EmailConfirmed = value;
                PhoneNumberConfirmed = value;
            }
        }
    }
    public class UpdateUserCommandHandler(IUser user) : IRequestHandler<UpdateUserCommand, Result>
    {
        private readonly IUser _user = user;
        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await _user.UpdateUserAsync(request, cancellationToken);
        }
    }
}
