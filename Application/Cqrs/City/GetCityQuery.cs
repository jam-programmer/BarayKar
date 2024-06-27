using Application.Common;
using Application.Common.Interfaces;
using Application.Cqrs.Province;
using Domain.Entities.System;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Cqrs.City
{
    public class GetCityQuery : IRequest<Result>
    {
        public Guid Id { set; get; }
    }
    public class GetCityQueryHandler 
        (IEfRepository<CityEntity> repository,ILogger<GetCityQueryHandler> logger)
        : IRequestHandler<GetCityQuery, Result>
    {
        private readonly ILogger<GetCityQueryHandler> _logger = logger;
        private readonly IEfRepository<CityEntity> _repository = repository;
        public async Task<Result> Handle(GetCityQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                _logger.LogWarning("شهر پیدا نشد.");
                return Result.Fail();
            }
            UpdateCityCommand command = entity.Adapt<UpdateCityCommand>();
           
            return Result.Success(command);
        }
    }
}
