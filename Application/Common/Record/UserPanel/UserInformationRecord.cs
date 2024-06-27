using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Common.Record.UserPanel
{
    public record UserInformationRecord
    {
        [Required(ErrorMessage = ValidationMessage.RequirdPhoneNumber)]
        public virtual string? PhoneNumber { get; set; }
        [Required(ErrorMessage = ValidationMessage.RequirdEmail)]
        public virtual string? Email { get; set; }
        [Required(ErrorMessage = ValidationMessage.RequirdFirstName)]
        public virtual string? FirstName { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdLastName)]
        public virtual string? LastName { set; get; }
        public virtual string? Avatar { set; get; }
        public virtual string? Password { get; set; }
        public IFormFile? AvatarFile { set; get; }
        public Guid Id { set; get; }
    }
}
