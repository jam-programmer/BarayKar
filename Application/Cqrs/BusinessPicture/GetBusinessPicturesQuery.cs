using Application.Common.Enum;
using Application.Common.Extension;
using Application.Common.Interfaces;
using Application.Common.Model;
using Application.Common.Model.CustomModel;
using Application.Core;
using Domain.Entities.Business;
using MediatR;

namespace Application.Cqrs.BusinessPicture
{
    public class GetBusinessPicturesQuery : IRequest<PaginatedList<BusinessPictureViewModel>>
    {
        public ChildPagination pagination { set; get; }
    }
    public class GetBusinessPicturesQueryHandler
        (IEfRepository<BusinessPictureEntity> repository)
        : IRequestHandler<GetBusinessPicturesQuery, PaginatedList<BusinessPictureViewModel>>
    {
        private readonly IEfRepository<BusinessPictureEntity> _repository = repository;
        public async Task<PaginatedList<BusinessPictureViewModel>> Handle(GetBusinessPicturesQuery request, CancellationToken cancellationToken)
        {
            var query = await _repository.GetByQueryAsync();
            int count;
            PaginatedList<BusinessPictureViewModel> model = new();
            if (string.IsNullOrEmpty(request.pagination!.keyword))
            {

                count = query.Where(w => w.BusinessId == request.pagination
              .ParentId).Count().PageCount(request.pagination!.pageSize);
                model = await query.Where(w => w.BusinessId == request.pagination.ParentId)
                    .MappingedAsync<BusinessPictureEntity, BusinessPictureViewModel>
                    (request.pagination!.curentPage,
                    request.pagination!.pageSize, count);
            }
            else
            {
                count = query.Count(c => c.Title!.Contains(request.pagination!.keyword)
                && c.BusinessId == request.pagination.ParentId)
                    .PageCount(request.pagination!.pageSize);


                model = await query.Where(w => w.Title!.Contains(request.pagination!.keyword)
                && w.BusinessId == request.pagination.ParentId)
              .MappingedAsync<BusinessPictureEntity, BusinessPictureViewModel>(request.pagination!
                    .curentPage, request.pagination!.pageSize, count);
            }
            return model;
        }
    }
    public class BusinessPictureViewModel
    {
        public Guid BusinessId { set; get; }
        public string? Image { set; get; }
        public string? Title { set; get; }
        public string? Alt { set; get; }
        public Guid Id { set; get; }
    }
}
