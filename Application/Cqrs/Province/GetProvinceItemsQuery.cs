
using Application.Common.Interfaces;
using Application.Common.ViewModel;
using Domain.Entities.System;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Cqrs.Province
{
    public class GetProvinceItemsQuery:IRequest<List<ItemViewModel<string,string>>>
    {
    }
    public class GetProvinceItemsQueryHandler 
        (IEfRepository<ProvinceEntity> repository)
        : IRequestHandler<GetProvinceItemsQuery, List<ItemViewModel<string, string>>>
    {
        private readonly IEfRepository<ProvinceEntity> _repository=repository;
        public async Task<List<ItemViewModel<string, string>>> Handle(GetProvinceItemsQuery request, CancellationToken cancellationToken)
        {
            var query = await _repository.GetByQueryAsync();
            return await query.Select(s => new ItemViewModel<string, string>
            {
                Id = s.Id.ToString(),
                Title = s.Name
            }).ToListAsync();

        }
    }
}
