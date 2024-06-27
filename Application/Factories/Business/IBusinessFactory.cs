using Application.Common;
using Application.Common.Record.Business;
using Application.Common.ViewModel.Business;

namespace Application.Factories.Business
{
    public interface IBusinessFactory
    {
        Task<BusinessDetailViewModel> GetBusinessDetailAsync(Guid Id
            , CancellationToken cancellation = default);

        Task<List<PictureBusinessViewModel>> GetBusinessPicturesAsync
            (Guid Id, CancellationToken cancellation = default);

        Task<List<PropertyBusinessViewModel>> GetBusinessPropertiesAsync
            (Guid Id, CancellationToken cancellation = default);

        Task<List<TimeBusinessViewModel>>
            GetBusinessTimesAsync(Guid Id, CancellationToken cancellation = default);

        Task<Result> GetBusinessesAsync(BusinessFilter filter, CancellationToken cancellation = default);

        Task<List<CategoryFilterViewModel>> GetCategoryFilterAsync(CancellationToken cancellation=default);
        Task<List<ProvinceFilterViewModel>> GetProvinceFilterAsync(CancellationToken cancellation = default);
    }
}
