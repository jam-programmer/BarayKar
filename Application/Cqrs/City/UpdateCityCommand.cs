using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities.System;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cqrs.City
{
    public class UpdateCityCommand:IRequest<Result>
    {
        public Guid Id { get; set; }
        public Guid ProvinceId { get; set; }
        public string? Name { get; set; }    
    }
    public class UpdateCityCommandHandler 
        (IEfRepository<CityEntity> repository,ILogger<UpdateCityCommandHandler> logger)
        : IRequestHandler<UpdateCityCommand, Result>
    {
        private readonly ILogger<UpdateCityCommandHandler> _logger = logger;
        private readonly IEfRepository<CityEntity> _repository = repository;
        public async Task<Result> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                return Result.Fail(FailMessage.Internal);
            }
          
            entity = request.Adapt<CityEntity>();
          
            try
            {
                await _repository.UpdateAsync(entity);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError($"خطای بروزرسانی شهر رخ داده است - {ex.Message}");
            }
            return Result.Fail(FailMessage.Internal);
        }
    }
}
