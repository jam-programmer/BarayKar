using Application.Common.ViewModel.Home;

namespace Application.Common.ViewModel.Employment
{
    public class LastEmploymentViewModel
    {
        public IEnumerable<EmploymentViewModel>? employments { set; get; }
        public string? EmploymentText { set; get; }
    }
}
