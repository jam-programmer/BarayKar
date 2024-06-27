using Application.Common;
using Application.Common.Extension;
using Application.Common.Interfaces;
using Domain.Entities.Business;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Application.Cqrs.BusinessPicture
{
    public class InsertBusinessPictureCommand : IRequest<Result>
    {
        [Required(ErrorMessage = ValidationMessage.RequirdUpload)]
        public IFormFile? ImageFile { set; get; }
        public string? Title { set; get; }
        public string? Alt { set; get; }
        public Guid BusinessId { set; get; }
    }
    public class InsertBusinessPictureCommandHandler
        (IEfRepository<BusinessPictureEntity> repository,ILogger<InsertBusinessPictureCommandHandler> logger)
        : IRequestHandler<InsertBusinessPictureCommand, Result>
    {
        private readonly ILogger<InsertBusinessPictureCommandHandler> _logger = logger;
        private readonly IEfRepository<BusinessPictureEntity> _repository = repository;
        public async Task<Result> Handle
            (InsertBusinessPictureCommand request, CancellationToken cancellationToken)
        {
            BusinessPictureEntity entity = request.Adapt<BusinessPictureEntity>();

            try
            {
                entity.Image = request.ImageFile!.UploadImage("Business");
                await _repository.InsertAsync(entity, cancellationToken);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError($"خطای ایجاد تصویر برای کسب و کار رخ داده است - {ex.Message}");
            }
            return Result.Fail(FailMessage.Internal);
        }
    }
}
