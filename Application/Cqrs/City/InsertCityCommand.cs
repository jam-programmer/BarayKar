using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities.System;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Application.Cqrs.City
{
    public class InsertCityCommand:IRequest<Result>
    {
        public Guid ProvinceId { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdProperty)]
        public string? Name { set; get; }
    }

    public class InsertCityCommandHandler 
        (IEfRepository<CityEntity> repository,
        ILogger<InsertCityCommandHandler> logger)
        : IRequestHandler<InsertCityCommand, Result>
    {
        private readonly ILogger<InsertCityCommandHandler> _logger = logger;
        private readonly IEfRepository<CityEntity> _repository = repository;
        public async Task<Result> Handle(InsertCityCommand request, CancellationToken cancellationToken)
        {

            CityEntity entity = request.Adapt<CityEntity>();
          
            try
            {
                await _repository.InsertAsync(entity, cancellationToken);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError($"خطای ایجاد شهر رخ داده است - {ex.Message}");
            }
            return Result.Fail(FailMessage.Internal);
        }
    }
}
