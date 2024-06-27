using Application.Common;
using Application.Common.Enum;
using Application.Common.Interfaces;
using Domain.Entities.Business;
using Domain.Entities.System;
using Domain.Enum;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Application.Cqrs.BusinessProperty
{
    public class UpdateBusinessPropertyCommand:IRequest<Result>
    {
        [Required(ErrorMessage = ValidationMessage.RequirdProperty)]
        public string? Key { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdProperty)]
        public string? Value { set; get; }
        public Guid BusinessId { set; get; }
        public Guid Id { set; get; }
    }
    public class UpdateBusinessPropertyCommandHandler 
        (IEfRepository<BusinessPropertyEntity> repository,
        ILogger<UpdateBusinessPropertyCommandHandler> logger)
        : IRequestHandler<UpdateBusinessPropertyCommand, Result>
    {
        private readonly ILogger<UpdateBusinessPropertyCommandHandler> _logger= logger;
        private readonly IEfRepository<BusinessPropertyEntity> _repository = repository;
        public async Task<Result> Handle(UpdateBusinessPropertyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                return Result.Fail(FailMessage.Internal);
            }

            entity = request.Adapt<BusinessPropertyEntity>();

            try
            {
                await _repository.UpdateAsync(entity);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError($"خطای بروزرسانی ویژگی کسب و کار رخ داده است - {ex.Message}");
            }
            return Result.Fail(FailMessage.Internal);
        }
    }
}
