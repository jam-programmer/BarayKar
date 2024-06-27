
using Application.Common.Extension;
using Application.Common.Interfaces;
using Application.Common.Model;
using Application.Core;
using Domain.Entities.System;
using MediatR;

namespace Application.Cqrs.Category
{
    public class GetCategoriesQuery : IRequest<PaginatedList<CategoryViewModel>>
    {
       public IPagination? pagination { set; get; }
    }
    public class GetCategoriesQueryHandler 
        (IEfRepository<CategoryEntity> repository)
        : IRequestHandler<GetCategoriesQuery, PaginatedList<CategoryViewModel>>
    {
        private readonly IEfRepository<CategoryEntity> _repository = repository;
        public async Task<PaginatedList<CategoryViewModel>> Handle
            (GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var query =await _repository.GetByQueryAsync();
            int count;
            PaginatedList<CategoryViewModel> model = new();
            if (string.IsNullOrEmpty(request.pagination!.keyword))
            {
                
                count = query.Count().PageCount(request.pagination!.pageSize);
                model = await query
                    .MappingedAsync<CategoryEntity, CategoryViewModel>
                    (request.pagination!.curentPage,
                    request.pagination!.pageSize, count);
            }
            else
            {
                count = query.Count(c => c.Name.Contains(request.pagination!.keyword))
                    .PageCount(request.pagination!.pageSize);
                model = await query.Where(w => w.Name.Contains(request.pagination!.keyword))
                    .MappingedAsync<CategoryEntity, CategoryViewModel>(request.pagination!
                    .curentPage, request.pagination!.pageSize, count);
            }
            return model;
        }
    }
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string? Image { set; get; }
        public string? Name { set; get; }
        public string? ParentCategoryName { set; get; }
    }
}
