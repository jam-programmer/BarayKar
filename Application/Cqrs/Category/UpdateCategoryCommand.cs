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
    public class UpdateCategoryCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public IFormFile? ImageFile { set; get; }
        public string? ImagePath { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdCategoryName)]
        public required string Name { set; get; }
        public Guid? ParentCategoryId { get; set; }
    }
    public class UpdateCategoryCommandHandler
        (IEfRepository<CategoryEntity> repository,
        ILogger<UpdateCategoryCommandHandler> logger)
        : IRequestHandler<UpdateCategoryCommand, Result>
    {
        private readonly ILogger<UpdateCategoryCommandHandler> _logger = logger;
        private readonly IEfRepository<CategoryEntity> _repository = repository;
        public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                return Result.Fail(FailMessage.Internal);
            }
            if (entity.Id == request.ParentCategoryId)
            {
                return Result.Fail(FailMessage.EqualParentId);
            }
            string TempImagePath = string.Empty;
            if (request.ImageFile == null)
            {
                TempImagePath = entity.Image!;
            }
            else
            {
                var resultRemoveOldImage = entity.Image!.RemoveImage("Category");
                if (!resultRemoveOldImage.IsSuccess)
                {
                    _logger.LogWarning($"خطای حذف تصویر دسته بندی رخ داده است");
                    return Result.Fail(FailMessage.Internal);
                }
                TempImagePath = request.ImageFile.UploadImage("Category");
            }
            entity = request.Adapt<CategoryEntity>();
            entity.Image = TempImagePath;
            try
            {
                await _repository.UpdateAsync(entity);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError($"خطای بروزرسانی دسته بندی رخ داده است - {ex.Message}");
            }
            return Result.Fail(FailMessage.Internal);
        }
    }
}
