using Application.Common.Enum;
using Application.Common.Extension;
using Application.Common.Interfaces;
using Application.Common.Model;
using Application.Common.Model.CustomModel;
using Application.Core;
using Domain.Entities.Business;
using MediatR;

namespace Application.Cqrs.BusinessTime
{
    public class GetBusinessTimesQuery : IRequest<PaginatedList<BusinessTimeViewModel>>
    {
        public ChildPagination pagination { set; get; }
    }
    public class GetBusinessTimesQueryHandler (IEfRepository<BusinessTimeEntity> repository)
        : IRequestHandler<GetBusinessTimesQuery, PaginatedList<BusinessTimeViewModel>>
    {
        private readonly IEfRepository<BusinessTimeEntity> _repository = repository;
        public async Task<PaginatedList<BusinessTimeViewModel>> Handle(GetBusinessTimesQuery request, CancellationToken cancellationToken)
        {
            var query = await _repository.GetByQueryAsync();
            int count;
            PaginatedList<BusinessTimeViewModel> model = new();
            if (string.IsNullOrEmpty(request.pagination!.keyword))
            {

                count = query.Where(w => w.BusinessId == request.pagination
              .ParentId).Count().PageCount(request.pagination!.pageSize);
                model = await query.Where(w => w.BusinessId == request.pagination.ParentId)
                    .MappingedAsync<BusinessTimeEntity, BusinessTimeViewModel>
                    (request.pagination!.curentPage,
                    request.pagination!.pageSize, count);
            }
            else
            {
                count = query.Count(c => c.Time!.Contains(request.pagination!.keyword)
                && c.BusinessId == request.pagination.ParentId)
                    .PageCount(request.pagination!.pageSize);


                model = await query.Where(w => w.Time!.Contains(request.pagination!.keyword)
                && w.BusinessId == request.pagination.ParentId)
              .MappingedAsync<BusinessTimeEntity, BusinessTimeViewModel>(request.pagination!
                    .curentPage, request.pagination!.pageSize, count);
            }
            return model;
        }
    }
    public class BusinessTimeViewModel
    {
        public Guid BusinessId { set; get; }
        public string? Time { set; get; }
        public Day Day { set; get; }
        public Guid Id { set; get; }
    }
}
