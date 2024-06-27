using Application.Common.Extension;
using Application.Common.Interfaces;
using Application.Common.Model;
using Application.Core;
using Application.Cqrs.Category;
using Domain.Entities.System;
using MediatR;

namespace Application.Cqrs.Province
{
    public class GetProvincesQuery:IRequest<PaginatedList<ProvinceViewModel>>
    {
        public IPagination? pagination { set; get; }
    }
    public class GetProvincesQueryHandler 
        (IEfRepository<ProvinceEntity> repository )
        : IRequestHandler<GetProvincesQuery, PaginatedList<ProvinceViewModel>>
    {
        private readonly IEfRepository<ProvinceEntity> _repository = repository;
        public async Task<PaginatedList<ProvinceViewModel>> Handle(GetProvincesQuery request, CancellationToken cancellationToken)
        {
            var query = await _repository.GetByQueryAsync();
            int count;
            PaginatedList<ProvinceViewModel> model = new();
            if (string.IsNullOrEmpty(request.pagination!.keyword))
            {

                count = query.Count().PageCount(request.pagination!.pageSize);
                model = await query
                    .MappingedAsync<ProvinceEntity, ProvinceViewModel>
                    (request.pagination!.curentPage,
                    request.pagination!.pageSize, count);
            }
            else
            {
                count = query.Count(c => c.Name.Contains(request.pagination!.keyword))
                    .PageCount(request.pagination!.pageSize);
                model = await query.Where(w => w.Name.Contains(request.pagination!.keyword))
                    .MappingedAsync<ProvinceEntity, ProvinceViewModel>(request.pagination!
                    .curentPage, request.pagination!.pageSize, count);
            }
            return model;
        }
    }
    public class ProvinceViewModel
    {
        public string? Image { set; get; }
        public Guid Id { get; set; }    
        public string? Name { get; set; }    
    }
}
