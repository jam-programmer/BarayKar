
using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities.Business;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Cqrs.Business
{
    public class DeleteBusinessCommand : IRequest<Result>
    {
        public Guid Id { set; get; }
    }
    public class DeleteBusinessCommandHandler
        (IEfRepository<BusinessEntity> repository, ILogger<DeleteBusinessCommandHandler> logger) : IRequestHandler<DeleteBusinessCommand, Result>
    {
        private readonly IEfRepository<BusinessEntity> _repository = repository;
        private readonly ILogger<DeleteBusinessCommandHandler> _logger = logger;

        public async Task<Result> Handle(DeleteBusinessCommand request, CancellationToken cancellationToken)
        {
            var query = await _repository.GetByQueryAsync();
            var entity = await query.Include(i => i.Pictures).
                Include(i => i.Properties).
                Include(i => i.Times).
                SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                return Result.Fail(FailMessage.Internal);
            }
            if (entity.Times!.Any() || entity.Properties!.Any() || entity.Pictures!.Any())
            {
                return Result.Fail(FailMessage.HaveChild);
            }

            try
            {
                await _repository.DeleteAsync(entity);

                return Result.Success();

            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}-خطای حذف کسب و کار رخ داده است");
            }
            return Result.Fail();
        }
    }
}
