using Application.Common;
using Application.Common.Enum;
using Application.Common.Extension;
using Application.Common.Interfaces;
using Domain.Entities.Business;
using Domain.Entities.System;
using Domain.Enum;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Application.Cqrs.BusinessPicture
{
    public class UpdateBusinessPictureCommand : IRequest<Result>
    {

        public IFormFile? ImageFile { set; get; }
        public string? Title { set; get; }
        public string? Alt { set; get; }
        public Guid BusinessId { set; get; }
        public Guid Id { set; get; }
        public string? ImagePath { set; get; }
    }
    public class UpdateBusinessPictureCommandHandler
        (IEfRepository<BusinessPictureEntity> repository
        ,ILogger<UpdateBusinessPictureCommandHandler> logger)
        : IRequestHandler<UpdateBusinessPictureCommand, Result>
    {
        private readonly ILogger<UpdateBusinessPictureCommandHandler> _logger = logger;
        private readonly IEfRepository<BusinessPictureEntity> _repository = repository;
        public async Task<Result> Handle(UpdateBusinessPictureCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                return Result.Fail(FailMessage.Internal);
            }
            entity = request.Adapt<BusinessPictureEntity>();
            if (request.ImageFile == null)
            {
                entity.Image = request.ImagePath;
            }
            else
            {
                var resultRemoveOldImage = request.ImagePath!.RemoveImage("Business");
                if (!resultRemoveOldImage.IsSuccess)
                {
                    return Result.Fail(FailMessage.Internal);
                }
                entity.Image = request.ImageFile.UploadImage("Business");
            }
            try
            {
                await _repository.UpdateAsync(entity);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError($"خطای بروزرسانی تصویر کسب و کار رخ داده است - {ex.Message}");
            }
            return Result.Fail(FailMessage.Internal);
        }
    }
}
