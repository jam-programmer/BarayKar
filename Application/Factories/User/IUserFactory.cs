using Application.Common;
using Application.Common.Model;
using Application.Common.Record.UserPanel;
using Application.Common.ViewModel;
using Application.Common.ViewModel.User;

namespace Application.Factories.User
{
    public interface IUserFactory
    {
        Task<UserInformationViewModel> GetUserInformationAsync
            (Guid Id, CancellationToken cancellation = default);
        Task<UserInformationRecord> GetUserInformationProfileAsync
            (Guid Id , CancellationToken cancellation=default);
        Task<Result> UpdateUserInformationProfileAsync(UserInformationRecord record,
            CancellationToken cancellation = default);
        Task<Result> AddBusinessAsync(AddBusinessRecord record,
            CancellationToken cancellation = default);
        Task<List<UserBusinessViewModel>> GetUserBusinessesAsync
            (Guid UserId, CancellationToken cancellation = default);

        Task<UpdateBusinessRecord> GetUserBusinessByIdAsync
             (Guid Id, CancellationToken cancellation = default);

        Task<Result> UpdateUserBusinessAsync
            (UpdateBusinessRecord record,CancellationToken cancellation=default);


        Task<PaginatedList<UserEmploymentAdvertisementViewModel>> GetUserEmploymentAdvertisementsAsync
            (UserPaginationRecord userPagination
            , CancellationToken cancellation = default);

        Task<List<ItemViewModel<string, string>>> GetUserBusinessesItemAsync(Guid UserId);
        Task<Result>
            AddEmploymentAsync(AddEmploymentRecord record, CancellationToken cancellation = default);

        Task<Result> GetUserEmploymentByIdAsync
            (Guid Id, CancellationToken cancellation = default);

        Task<Result> UpdateUserEmploymentAsync
            (UpdateEmploymentRecord record,CancellationToken cancellation=default);



        Task<UserStatisticalInformationViewModel> GetUserStatisticalInformationAsync
            (Guid Id, CancellationToken cancellation=default);

        Task<MyResumeRecord> GetUserResumeAsync(Guid UserId, 
            CancellationToken cancellation = default);

        Task<Result> UpdateMyResumeAsync(MyResumeRecord record, CancellationToken cancellation);


        Task<Result> GetRequestEmploymentByEmploymentIdAsync(Pagination pagination,Guid EmploymentId
    , CancellationToken cancellation = default);

        Task ChangeEmploymentRequestAsync(UpdateEmploymentRequestRecord record,
            CancellationToken cancellation = default);
    }
}
