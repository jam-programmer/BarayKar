
using Application.Common;
using Application.Common.Extension;
using Application.Common.Interfaces;
using Domain.Entities.System;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Application.Cqrs.Category
{
    public class InsertCategoryCommand:IRequest<Result>
    {
        public IFormFile? ImageFile { set; get; }
        [Required(ErrorMessage =ValidationMessage.RequirdCategoryName)]
        public required string Name { set; get; }
        public Guid? ParentCategoryId { get; set; }
    }
    public class InsertCategoryCommandHandler 
        (IEfRepository<CategoryEntity> repository,
        ILogger<InsertCategoryCommandHandler> logger)
        : IRequestHandler<InsertCategoryCommand, Result>
    {
        private readonly ILogger<InsertCategoryCommandHandler> _logger = logger;
        private readonly IEfRepository<CategoryEntity> _repository = repository;
        public async Task<Result> Handle(InsertCategoryCommand request, CancellationToken cancellationToken)
        {
            CategoryEntity entity = request.Adapt<CategoryEntity>();
            entity.Image = request.ImageFile!.UploadImage("Category");
            try
            {
               await _repository.InsertAsync(entity,cancellationToken);
               return Result.Success();
            }catch(Exception ex)
            {
                _logger.LogError($"خطای ایجاد دسته بندی رخ داده است - {ex.Message}");
            }
            return Result.Fail(FailMessage.Internal);
        }
    }
}
