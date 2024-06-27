using Application.Common.Interfaces;
using Application.Common.ViewModel;
using Domain.Entities.System;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Cqrs.City
{
    public class GetCityItemsByIdQuery : IRequest<List<ItemViewModel<string, string>>>
    {
        public Guid ParentId { set; get; }
    }
    public class GetCityItemsByIdQueryHandler
        (IEfRepository<CityEntity> repository)
        : IRequestHandler<GetCityItemsByIdQuery, List<ItemViewModel<string, string>>>
    {
        private readonly IEfRepository<CityEntity> _repository = repository;
        public async Task<List<ItemViewModel<string, string>>> Handle(GetCityItemsByIdQuery request, CancellationToken cancellationToken)
        {
            var query = await _repository.GetByQueryAsync();
            return await query.
                Where(w=>w.ProvinceId==request.ParentId)
                .Select(s => new ItemViewModel<string, string>
            {
                Id = s.Id.ToString(),
                Title = s.Name
            }).ToListAsync();
        }
    }
}
