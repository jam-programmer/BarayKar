using Application.Common;
using Application.Common.Interfaces;
using Application.Cqrs.Province;
using Domain.Entities.Business;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Cqrs.Business
{
    public class GetBusinessQuery : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
    public class GetBusinessQueryHandler(IEfRepository<BusinessEntity> repository,
        ILogger<GetBusinessQueryHandler> logger) :
        IRequestHandler<GetBusinessQuery, Result>
    {
        private readonly ILogger<GetBusinessQueryHandler> _logger = logger;
        private readonly IEfRepository<BusinessEntity> _repository = repository;
        public async Task<Result> Handle(GetBusinessQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                _logger.LogWarning("کسب و کار پیدا نشد.");
                return Result.Fail();
            }
            UpdateBusinessCommand command = entity.Adapt<UpdateBusinessCommand>();
        
            return Result.Success(command);
        }
    }
}
