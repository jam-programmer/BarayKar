using Application.Common;
using Application.Common.Extension;
using Application.Common.Interfaces;
using Domain.Entities.System;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Application.Cqrs.Province
{
    public class InsertProvinceCommand:IRequest<Result>
    {
        [Required(ErrorMessage =ValidationMessage.RequirdProperty)]
        public string? Name { set; get; }
        public IFormFile? ImageFile { set; get; }    
    }
    public class InsertProvinceCommandHandler 
        (IEfRepository<ProvinceEntity> repository
        ,ILogger<InsertProvinceCommandHandler> logger)
        : IRequestHandler<InsertProvinceCommand, Result>
    {
        private readonly ILogger<InsertProvinceCommandHandler> _logger=logger;
        private readonly IEfRepository<ProvinceEntity> _repository = repository;
        public async Task<Result> Handle(InsertProvinceCommand request,
            CancellationToken cancellationToken)
        {
            ProvinceEntity entity = request.Adapt<ProvinceEntity>();
            entity.Image = request.ImageFile!.UploadImage("Province");
            try
            {
                await _repository.InsertAsync(entity, cancellationToken);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError($"خطای ایجاد استان رخ داده است - {ex.Message}");
            }
            return Result.Fail(FailMessage.Internal);
        }
    }
    
}
