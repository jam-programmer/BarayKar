using Application.Common;
using Application.Common.Enum;
using Application.Common.Interfaces;
using Domain.Entities.Business;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Application.Cqrs.BusinessTime
{
    public class UpdateBusinessTimeCommand : IRequest<Result>
    {
        [Required(ErrorMessage = ValidationMessage.RequirdProperty)]
        public string? Time { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdProperty)]
        public Day Day { set; get; }
        public Guid BusinessId { set; get; }
        public Guid Id { set; get; }
    }
    public class UpdateBusinessTimeCommandHandler
        (IEfRepository<BusinessTimeEntity> repository,
        ILogger<UpdateBusinessTimeCommandHandler> logger)
        : IRequestHandler<UpdateBusinessTimeCommand, Result>
    {
        private readonly ILogger<UpdateBusinessTimeCommandHandler> _logger = logger;
        private readonly IEfRepository<BusinessTimeEntity> _repository = repository;
        public async Task<Result> Handle(UpdateBusinessTimeCommand request, CancellationToken cancellationToken)
        {
          
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                return Result.Fail(FailMessage.Internal);
            }

            entity = request.Adapt<BusinessTimeEntity>();

            try
            {
                await _repository.UpdateAsync(entity);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError($"خطای بروزرسانی زمانبندی کسب و کار رخ داده است - {ex.Message}");
            }
            return Result.Fail(FailMessage.Internal);
        }
    }
}
