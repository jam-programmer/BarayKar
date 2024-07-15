using Application.Common.Record.Business;
using Application.Common;
using Application.Common.ViewModel.Employment;
using Application.Common.Record.Employment;

namespace Application.Factories.Employment
{
    public interface IEmploymentFactory
    {
        Task<EmploymentDetailViewModel> GetEmploymentDetailAsync
            (Guid Id, CancellationToken cancellationToken=default);

        Task<Result> GetEmploymentsAsync(EmploymentFilter filter, CancellationToken cancellation = default);
        Task<List<BusinessFilterViewModel>> GetBusinessFilterAsync(CancellationToken cancellation = default);
        Task<Result> SendRequestForEmploymentAsync
            (Guid EmploymentId, Guid UserId, CancellationToken cancellation = default);





    }
}
