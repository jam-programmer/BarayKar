using Application.Common;
using Application.Common.Interfaces;
using Application.Common.ViewModel;
using Domain.Entities.System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cqrs.Category
{
    public class GetCategoryItemsQuery:IRequest<List<ItemViewModel<string,string>>>
    {

    }
    public class GetCategoryItemsQueryHandler 
        (IEfRepository<CategoryEntity> repository):
        IRequestHandler<GetCategoryItemsQuery, List<ItemViewModel<string, string>>>
    {
        private readonly IEfRepository<CategoryEntity> _repository = repository;
        public async Task<List<ItemViewModel<string, string>>> Handle(GetCategoryItemsQuery request, CancellationToken cancellationToken)
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
