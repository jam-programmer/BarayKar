using Application.Common.Enum;

namespace Application.Common.ViewModel.User
{
    public class RequestEmploymentViewModel
    {
        public Guid Id { set; get; }
        public Status Status { get; set; }
        public Guid ResumeId { set; get; }
        public Guid EmploymentId { set; get; }
        public string? ResumeTitle { set; get; }
        public required string ResumeUserFirstName { set; get; }
        public required string ResumeUserLastName { set; get; }

    }
}
