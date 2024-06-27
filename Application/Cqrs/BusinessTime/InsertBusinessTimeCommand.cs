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

namespace Application.Cqrs.BusinessTime
{
    public class InsertBusinessTimeCommand : IRequest<Result>
    {
        [Required(ErrorMessage =ValidationMessage.RequirdProperty)]
        public string? Time { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdProperty)]
        public Day Day { set; get; }
        public Guid BusinessId { set; get; }
    }
    public class InsertBusinessTimeCommandHandler 
        (IEfRepository<BusinessTimeEntity> repository,
        ILogger<InsertBusinessTimeCommandHandler> logger)
        : IRequestHandler<InsertBusinessTimeCommand, Result>
    {
        private readonly ILogger<InsertBusinessTimeCommandHandler> _logger = logger;
        private readonly IEfRepository<BusinessTimeEntity> _repository = repository;
        public async Task<Result> Handle
            (InsertBusinessTimeCommand request, CancellationToken cancellationToken)
        {
            BusinessTimeEntity entity = request.Adapt<BusinessTimeEntity>();

            try
            {
                await _repository.InsertAsync(entity, cancellationToken);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError($"خطای ایجاد زمانبندی کسب و کار رخ داده است - {ex.Message}");
            }
            return Result.Fail(FailMessage.Internal);
        }
    }
}
