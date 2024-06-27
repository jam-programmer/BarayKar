using Application.Common;
using Application.Common.Extension;
using Application.Common.Interfaces;
using Domain.Entities.System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Cqrs.Category
{
    public class DeleteCategoryCommand:IRequest<Result>
    {
        public Guid Id { get; set; }
    }
    public class DeleteCategoryCommandHandler 
        (IEfRepository<CategoryEntity> repository,
        ILogger<DeleteCategoryCommandHandler> logger)
        : IRequestHandler<DeleteCategoryCommand, Result>
    {
        private readonly ILogger<DeleteCategoryCommandHandler> _logger= logger;
        private readonly IEfRepository<CategoryEntity> _repository=repository;

        public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var query = await _repository.GetByQueryAsync();
            var entity =await query.Include(i => i.ChildCategories).
                SingleOrDefaultAsync(s=>s.Id==request.Id, cancellationToken);
            if (entity == null)
            {
                return Result.Fail(FailMessage.Internal);
            }
            if (entity.ChildCategories!.Any())
            {
                return Result.Fail(FailMessage.HaveChild);
            }
            string TempImagePath = entity.Image!;
            try
            {
                await _repository.DeleteAsync(entity);
                var resultImageRemove=TempImagePath.RemoveImage("Category");
                if (!resultImageRemove.IsSuccess)
                {
                    _logger.LogWarning($"خطای حذف تصویر دسته بندی رخ داده است");
                }
                return Result.Success();

            }catch(Exception ex)
            {
                _logger.LogError($"خطای حذف دسته بندی رخ داده است - {ex.Message}");
            }
            return Result.Fail();
        }
    }
}
