using Application.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace Application.Common.Record.UserPanel
{
    public class AddEmploymentRecord
    {
        [Required(ErrorMessage = ValidationMessage.RequirdProperty)]
        public string? Title { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdProperty)]
        public string? Description { set; get; }
        public string? BusinessDays { set; get; }
        public string? Age { set; get; }
        public Gender? Gender { set; get; }
        public string? StartTime { set; get; }
        public string? EndTime { set; get; }
        public string? TypeOfCooperation { set; get; }
        public string? WorkExperience { set; get; }
        public string? Salary { set; get; }
     
        [Required(ErrorMessage = ValidationMessage.RequirdSelection)]
        public Guid? ProvinceId { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdSelection)]
        public Guid? CityId { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdSelection)]
        public Guid? BusinessId { set; get; }
        public Guid? UserId { set; get; }
    }
}
