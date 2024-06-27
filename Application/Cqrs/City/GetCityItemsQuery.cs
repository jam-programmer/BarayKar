using Application.Common.Interfaces;
using Application.Common.ViewModel;
using Domain.Entities.System;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Cqrs.City
{
    public class GetCityItemsQuery : IRequest<List<ItemViewModel<string, string>>>
    {
       
    }
    public class GetCityItemsQueryHandler
        (IEfRepository<CityEntity> repository)
        : IRequestHandler<GetCityItemsQuery, List<ItemViewModel<string, string>>>
    {
        private readonly IEfRepository<CityEntity> _repository = repository;
        public async Task<List<ItemViewModel<string, string>>> Handle(GetCityItemsQuery request, CancellationToken cancellationToken)
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
