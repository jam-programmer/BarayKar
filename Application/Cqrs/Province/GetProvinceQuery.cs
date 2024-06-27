
using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities.System;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Cqrs.Province
{
    public class GetProvinceQuery:IRequest<Result>
    {
        public Guid Id { get; set; }    
    }
    public class GetProvinceQueryHandler 
        (IEfRepository<ProvinceEntity> repository,
        ILogger<GetProvinceQueryHandler> logger)
        : IRequestHandler<GetProvinceQuery, Result>
    {
        private readonly ILogger<GetProvinceQueryHandler> _logger = logger;
        private readonly IEfRepository<ProvinceEntity> _repository = repository;
        public async Task<Result> Handle(GetProvinceQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                _logger.LogWarning("استان پیدا نشد.");
                return Result.Fail();
            }
            UpdateProvinceCommand command = entity.Adapt<UpdateProvinceCommand>();
            command.ImagePath = entity.Image;
            return Result.Success(command);
        }
    }
}
