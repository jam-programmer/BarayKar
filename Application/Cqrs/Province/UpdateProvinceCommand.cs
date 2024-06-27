using Application.Common;
using Application.Common.Extension;
using Application.Common.Interfaces;
using Domain.Entities.System;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cqrs.Province
{
    public class UpdateProvinceCommand:IRequest<Result>
    {
        public Guid Id { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdProperty)]
        public string? Name { set; get; }


    
        public IFormFile? ImageFile { set; get; }
        public string? ImagePath { set; get; }
    }
    public class UpdateProvinceCommandHandler 
        (IEfRepository<ProvinceEntity> repository,
        ILogger<UpdateProvinceCommandHandler> logger)
        : IRequestHandler<UpdateProvinceCommand, Result>
    {
        private readonly ILogger<UpdateProvinceCommandHandler> _logger = logger;
        private readonly IEfRepository<ProvinceEntity> _repository= repository;
        public async Task<Result> Handle(UpdateProvinceCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                return Result.Fail(FailMessage.Internal);
            }
            string TempImagePath = string.Empty;
            if (request.ImageFile == null)
            {
                TempImagePath = entity.Image!;
            }
            else
            {
                var resultRemoveOldImage = request.ImagePath!.RemoveImage("Province");
                if (!resultRemoveOldImage.IsSuccess)
                {
                    _logger.LogWarning($"خطای حذف تصویر استان رخ داده است");
                    return Result.Fail(FailMessage.Internal);
                }

                TempImagePath = request.ImageFile.UploadImage("Province");
            }
            entity = request.Adapt<ProvinceEntity>();
            entity.Image = TempImagePath;
            try
            {
                await _repository.UpdateAsync(entity);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError($"خطای بروزرسانی استان رخ داده است - {ex.Message}");
            }
            return Result.Fail(FailMessage.Internal);
        }
    }
}
