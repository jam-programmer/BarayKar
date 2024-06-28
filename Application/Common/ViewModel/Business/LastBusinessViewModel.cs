using Application.Common.ViewModel.Home;

namespace Application.Common.ViewModel.Business
{
    public class LastBusinessViewModel
    {
        public IEnumerable<BusinessViewModel>? businesses { set; get; }
        public string? BusinessText { set; get; }
    }
}
