
using Application.Common;
using Application.Common.Record;
using Application.Common.ViewModel.Business;
using Application.Common.ViewModel.Employment;
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
        Task<LastBusinessViewModel> 
            GetLastBusinessesAsync(CancellationToken cancellation = default);

        Task<LastEmploymentViewModel> 
            GetLastEmploymentAsync(CancellationToken cancellation = default);

        Task<Result>SignInAsync(SignInRecord record,CancellationToken cancellation = default);
        Task SignOutAsync();
        Task<Result> SignUpAsync(SignUpRecord record, CancellationToken cancellation = default);
      
        Task<LogoViewModel>GetLogoAsync(CancellationToken cancellation = default);

        Task<ProvinceViewModel> GetProvinceInfoAsync(CancellationToken cancellation = default);


        Task<CategoryViewModel> GetCategoriesAsync(CancellationToken cancellation = default);


        Task<SocialFooterViewModel>GetSocialsAsync(CancellationToken cancellation = default);

        Task<InfoContactFooterViewModel> GetInfoContactAsync(CancellationToken cancellation = default);
    }
}
