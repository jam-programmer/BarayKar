
using Application.Common.Extension;
using Application.Common.Interfaces;
using Application.Common.Model;
using Application.Common.Model.CustomModel;
using Application.Core;
using Domain.Entities.System;
using MediatR;

namespace Application.Cqrs.City
{
    public class GetCitiesQuery:IRequest<PaginatedList<CityViewModel>>
    {
        public ChildPagination? pagination { set; get; }
    }
    public class GetCitiesQueryHandler
        (IEfRepository<CityEntity> repository)
        : IRequestHandler<GetCitiesQuery, PaginatedList<CityViewModel>>
    {
        private readonly IEfRepository<CityEntity> _repository = repository;
        public async Task<PaginatedList<CityViewModel>> 
            Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            var query = await _repository.GetByQueryAsync();
            int count;
            PaginatedList<CityViewModel> model = new();
            if (string.IsNullOrEmpty(request.pagination!.keyword))
            {

                count = query.Where(w=>w.ProvinceId==request.pagination
                .ParentId).Count().PageCount(request.pagination!.pageSize);
                model = await query.Where(w => w.ProvinceId == request.pagination
                .ParentId)
                    .MappingedAsync<CityEntity, CityViewModel>
                    (request.pagination!.curentPage,
                    request.pagination!.pageSize, count);
            }
            else
            {
                count = query.Count(c => c.Name!.Contains(request.pagination!.keyword)
                && c.ProvinceId==request.pagination.ParentId)
                    .PageCount(request.pagination!.pageSize);


                model = await query.Where(w => w.Name!.Contains(request.pagination!.keyword)
                && w.ProvinceId==request.pagination.ParentId)
                    .MappingedAsync<CityEntity, CityViewModel>(request.pagination!
                    .curentPage, request.pagination!.pageSize, count);
            }
            return model;
        }
    }
    public class CityViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}
