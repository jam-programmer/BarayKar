using Application.Common.Enum;

namespace Application.Common.ViewModel.Employment
{
    public class EmploymentDetailViewModel
    {
        public Guid Id { get; set; }
        public string? BusinessDays { set; get; }
        public long Code { set; get; }
        public string? Title { set; get; }
        public string? Description { set; get; }
        public string? Age { set; get; }
        public Gender? Gender { set; get; }
        public string? StartTime { set; get; }
        public string? EndTime { set; get; }
        public string? TypeOfCooperation { set; get; }
        public string? WorkExperience { set; get; }
        public string? Salary { set; get; }
        public string? CreateTime { set; get; }


        public string? ProvinceName { set; get; }


        public string? CityName { set; get; }

        public Guid? BusinessId { set; get; }
        public string? BusinessAccountName { set; get; }
    }
}
