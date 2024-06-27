using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities.Business;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Application.Cqrs.BusinessProperty
{
    public class InsertBusinessPropertyCommand : IRequest<Result>
    {
        [Required(ErrorMessage = ValidationMessage.RequirdProperty)]
        public string? Key { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdProperty)]
        public string? Value { set; get; }
        public Guid BusinessId { set; get; }
    }
    public class InsertBusinessPropertyCommandHandler
        (IEfRepository<BusinessPropertyEntity> repository,
        ILogger<InsertBusinessPropertyCommandHandler> logger)
        : IRequestHandler<InsertBusinessPropertyCommand, Result>
    {
        private readonly ILogger<InsertBusinessPropertyCommandHandler> _logger = logger;
        private readonly IEfRepository<BusinessPropertyEntity> _repository = repository;
        public async Task<Result> Handle
            (InsertBusinessPropertyCommand request, CancellationToken cancellationToken)
        {
            BusinessPropertyEntity entity = request.Adapt<BusinessPropertyEntity>();

            try
            {
                await _repository.InsertAsync(entity, cancellationToken);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError($"خطای ایجاد ویژگی کسب و کار رخ داده است - {ex.Message}");
            }
            return Result.Fail(FailMessage.Internal);
        }
    }
}
