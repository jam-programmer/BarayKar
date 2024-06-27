using Application.Common;
using Application.Common.Interfaces;
using Application.Cqrs.City;
using Domain.Entities.Business;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Cqrs.BusinessTime
{
    public class GetBusinessTimeQuery : IRequest<Result>
    {
        public Guid Id { get; set; }    
    }
    public class GetBusinessTimeQueryHandler
        (IEfRepository<BusinessTimeEntity> repository,
        ILogger<GetBusinessTimeQueryHandler> logger)
        : IRequestHandler<GetBusinessTimeQuery, Result>
    {
        private readonly ILogger<GetBusinessTimeQueryHandler> _logger=logger;
        private readonly IEfRepository<BusinessTimeEntity> _repository = repository;
        public async Task<Result> Handle(GetBusinessTimeQuery request, CancellationToken cancellationToken)
        {

            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                _logger.LogWarning("زمانبندی پیدا نشد. ");
                return Result.Fail();
            }
            UpdateBusinessTimeCommand command = entity.Adapt<UpdateBusinessTimeCommand>();

            return Result.Success(command);
        }
    }
}
