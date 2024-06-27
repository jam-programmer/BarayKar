using Application.Common.Interfaces;
using Application.Common.ViewModel;
using Domain.Entities.Business;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Cqrs.Business
{
    public class GetBusinessItemsQuery : IRequest<List<ItemViewModel<string, string>>>
    {
    }
    public class GetBusinessItemsQueryHandler(IEfRepository<BusinessEntity> repository) :
        IRequestHandler<GetBusinessItemsQuery, List<ItemViewModel<string, string>>>
    {
        private readonly IEfRepository<BusinessEntity> _repository = repository;
        public async Task<List<ItemViewModel<string, string>>> Handle(GetBusinessItemsQuery request, CancellationToken cancellationToken)
        {
            var query = await _repository.GetByQueryAsync();
            return await query.Select(s => new ItemViewModel<string, string>
            {
                Id = s.Id.ToString(),
                Title = s.AccountName
            }).ToListAsync();
        }
    }
}
