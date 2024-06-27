using Application.Common.Enum;
using Application.Common.Extension;
using Application.Common.Interfaces;
using Application.Common.Model;
using Application.Common.Model.CustomModel;
using Application.Core;
using Domain.Entities.Business;
using MediatR;

namespace Application.Cqrs.BusinessProperty
{
    public class GetBusinessPropertiesQuery : IRequest<PaginatedList<BusinessPropertyViewModel>>
    {
        public ChildPagination pagination { set; get; }
    }
    public class GetBusinessPropertiesQueryHandler
        (IEfRepository<BusinessPropertyEntity> repository)
        : IRequestHandler<GetBusinessPropertiesQuery, PaginatedList<BusinessPropertyViewModel>>
    {
        private readonly IEfRepository<BusinessPropertyEntity> _repository = repository;
        public async Task<PaginatedList<BusinessPropertyViewModel>> Handle(GetBusinessPropertiesQuery request, CancellationToken cancellationToken)
        {
            var query = await _repository.GetByQueryAsync();
            int count;
            PaginatedList<BusinessPropertyViewModel> model = new();
            if (string.IsNullOrEmpty(request.pagination!.keyword))
            {

                count = query.Where(w => w.BusinessId == request.pagination
              .ParentId).Count().PageCount(request.pagination!.pageSize);
                model = await query.Where(w => w.BusinessId == request.pagination.ParentId)
                    .MappingedAsync<BusinessPropertyEntity, BusinessPropertyViewModel>
                    (request.pagination!.curentPage,
                    request.pagination!.pageSize, count);
            }
            else
            {
                count = query.Count(c => c.Key!.Contains(request.pagination!.keyword)
                && c.BusinessId == request.pagination.ParentId)
                    .PageCount(request.pagination!.pageSize);


                model = await query.Where(w => w.Key!.Contains(request.pagination!.keyword)
                && w.BusinessId == request.pagination.ParentId)
              .MappingedAsync<BusinessPropertyEntity, BusinessPropertyViewModel>(request.pagination!
                    .curentPage, request.pagination!.pageSize, count);
            }
            return model;
        }
    }
    public class BusinessPropertyViewModel
    {
        public string? Key { set; get; }
        public string? Value { set; get; }
        public Guid Id { set; get; }
        public Guid BusinessId { set; get; }
    }
}
