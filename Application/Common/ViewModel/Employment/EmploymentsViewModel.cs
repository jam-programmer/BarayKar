using Application.Common.Record.Employment;
using Application.Common.ViewModel.Home;

namespace Application.Common.ViewModel.Employment
{
    public class EmploymentsViewModel
    {
        public List<EmploymentViewModel>? Employments { get; set; }
        public EmploymentFilter? Filter { set; get; }
        public int Total { set; get; }
        public int Size { set; get; } = 16;
    }
}
