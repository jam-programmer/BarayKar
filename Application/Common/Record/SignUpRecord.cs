using System.ComponentModel.DataAnnotations;

namespace Application.Common.Record
{
    public class SignUpRecord
    {
        [Required(ErrorMessage = ValidationMessage.RequirdFirstName)]
        public required string FirstName { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdLastName)]
        public required string LastName { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdNationalCode)]
        public virtual string? UserName { get; set; }
        [Required(ErrorMessage = ValidationMessage.RequirdEmail)]
        public virtual string? Email { get; set; }
        [Required(ErrorMessage = ValidationMessage.RequirdPassword)]
        public virtual string? Password { get; set; }
        [Required(ErrorMessage = ValidationMessage.RequirdPhoneNumber)]
        public virtual string? PhoneNumber { get; set; }
    }
}
