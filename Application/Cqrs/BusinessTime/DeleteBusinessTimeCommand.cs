using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities.Business;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Cqrs.BusinessTime
{
    public class DeleteBusinessTimeCommand : IRequest<Result>
    {
        public Guid Id { set; get; }
    }
    public class DeleteBusinessTimeCommandHandler
        (IEfRepository<BusinessTimeEntity> repository,
        ILogger<DeleteBusinessTimeCommandHandler> logger)
        : IRequestHandler<DeleteBusinessTimeCommand, Result>
    {
        private readonly ILogger<DeleteBusinessTimeCommandHandler> _logger = logger;
        private readonly IEfRepository<BusinessTimeEntity> _repository = repository;
        public async Task<Result> Handle(DeleteBusinessTimeCommand request, CancellationToken cancellationToken)
        {
            var query = await _repository.GetByQueryAsync();
            var entity = await query.
                SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                return Result.Fail(FailMessage.Internal);
            }


            try
            {
                await _repository.DeleteAsync(entity);

                return Result.Success();

            }
            catch (Exception ex)
            {
                _logger.LogError($"خطای حذف زمانبندی کسب و کار رخ داده است - {ex.Message}");
            }
            return Result.Fail();
        }
    }
}
