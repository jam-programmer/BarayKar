
using Application.Common;
using Application.Common.Record;
using Application.Common.ViewModel.Home;
using Application.Common.ViewModel.User;

namespace Application.Factories.Home
{
    public interface IHomeFactory
    {
        Task<HeaderViewModel> 
            GetHeaderAsync(CancellationToken cancellation = default);
        Task<IEnumerable<ParentCategoryViewModel>> 
            GetParentCategoryAsync(CancellationToken cancellation = default);
        Task<IEnumerable<BusinessViewModel>> 
            GetLastBusinessesAsync(CancellationToken cancellation = default);

        Task<IEnumerable<EmploymentViewModel>> 
            GetLastEmploymentAsync(CancellationToken cancellation = default);

        Task<Result>SignInAsync(SignInRecord record,CancellationToken cancellation = default);
        Task SignOutAsync();
        Task<Result> SignUpAsync(SignUpRecord record, CancellationToken cancellation = default);
      
        Task<LogoViewModel>GetLogoAsync(CancellationToken cancellation = default);

        Task<List<ProvinceInfoViewModel>>GetProvinceInfoAsync(CancellationToken cancellation = default);


        Task<List<CategoryInfoViewModel>> GetCategoriesAsync(CancellationToken cancellation = default);
    }
}
