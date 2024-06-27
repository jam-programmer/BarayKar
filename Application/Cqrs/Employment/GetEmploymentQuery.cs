using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities.Employment;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Cqrs.Employment
{
    public class GetEmploymentQuery : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
    public class GetEmploymentQueryHandler
        (IEfRepository<EmploymentEntity> repository,
        ILogger<GetEmploymentQueryHandler> logger)
        : IRequestHandler<GetEmploymentQuery, Result>
    {
        private readonly ILogger<GetEmploymentQueryHandler> _logger = logger;
        private readonly IEfRepository<EmploymentEntity> _repository = repository;
        public async Task<Result> Handle(GetEmploymentQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                _logger.LogWarning("آگهی پیدا نشد.");
                return Result.Fail();
            }
            UpdateEmploymentCommand command = entity.Adapt<UpdateEmploymentCommand>();

            return Result.Success(command);
        }
    }

}
