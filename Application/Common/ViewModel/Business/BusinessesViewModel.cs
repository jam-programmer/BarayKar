using Application.Common.Record.Business;
using Application.Common.ViewModel.Home;

namespace Application.Common.ViewModel.Business
{
    public class BusinessesViewModel
    {
        public List<BusinessViewModel>? Businesses { get; set; } 
        public BusinessFilter? Filter { get; set; }  
        public int Total { set; get; }
        public int Size { set; get; } = 16;
    }
}
