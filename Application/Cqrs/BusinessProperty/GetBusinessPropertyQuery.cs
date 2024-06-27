using Application.Common;
using Application.Common.Interfaces;
using Application.Cqrs.City;
using Domain.Entities.Business;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Cqrs.BusinessProperty
{
    public class GetBusinessPropertyQuery:IRequest<Result>
    {
        public Guid Id { get; set; }    
    }
    public class GetBusinessPropertyQueryHandler
        (IEfRepository<BusinessPropertyEntity> repository,
        ILogger<GetBusinessPropertyQueryHandler> logger)
        : IRequestHandler<GetBusinessPropertyQuery, Result>
    {
        private readonly ILogger<GetBusinessPropertyQueryHandler> _logger = logger;
        private readonly IEfRepository<BusinessPropertyEntity> _repository = repository;
        public async Task<Result> Handle(GetBusinessPropertyQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                _logger.LogWarning("ویژگی پیدا نشد.");
                return Result.Fail();
            }
            UpdateBusinessPropertyCommand command = entity.Adapt<UpdateBusinessPropertyCommand>();

            return Result.Success(command);
        }
    }
}
