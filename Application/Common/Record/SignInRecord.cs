using System.ComponentModel.DataAnnotations;

namespace Application.Common.Record
{
    public record SignInRecord
    {
        [Required(ErrorMessage =ValidationMessage.RequirdNationalCode)]
        public string? NationalCode { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdPassword)]
        public string? Password { set; get; }
        public bool SavePassword { set; get; }
    }
}
