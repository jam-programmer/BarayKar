using Application.Common.Enum;
using Domain.Enum;

namespace Application.Common.ViewModel.User
{
    public class EmploymentRequestAlertViewModel
    {
        public Guid Id { get; set; }
        public string? Comment { set; get; }
        public Status Status { get; set; }
        public string? EmploymentTitle { set; get; }
    }
}
