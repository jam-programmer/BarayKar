using Domain.Enum;

namespace Application.Common.ViewModel.User
{
    public class RequestResumeViewModel
    {
        public string? UserFirstName { set; get; }
        public string? UserLastName { set; get; }
        public string? UserEmail { set; get; }
        public string? UserPhoneNumber { set; get; }
        public string? Linkdin { set; get; }
        public string? WorkRegistration { set; get; }
        public string? Title { set; get; }
        public string? Birthday { set; get; }
        public string? MilitaryService { set; get; }
        public string? MaritalStatus { set; get; }
        public string? RightsRequested { set; get; }
        public string? Skills { set; get; }

        public List<RequestEducationalRecordViewModel>? Educationals { set; get; }
        public List<RequestWorkExperienceViewModel>? Experiences { set; get; }
    }
    public class RequestEducationalRecordViewModel
    {
        public string? Title { set; get; }
        public string? Description { set; get; }
        public string? FromDate { set; get; }
        public string? ToDate { set; get; }
        public bool IsStudying { set; get; }
    }

    public class RequestWorkExperienceViewModel
    {
        public string? Title { set; get; }
        public string? Description { set; get; }
        public string? FromDate { set; get; }
        public string? ToDate { set; get; }
        public bool IsWorking { set; get; }
    }
}
