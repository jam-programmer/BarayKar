using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities.Business;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Cqrs.BusinessProperty
{
    public class DeleteBusinessPropertyCommand : IRequest<Result>
    {
        public Guid Id { set; get; }
    }
    public class DeleteBusinessPropertyCommandHandler
        (IEfRepository<BusinessPropertyEntity> repository
        ,ILogger<DeleteBusinessPropertyCommandHandler> logger)
        : IRequestHandler<DeleteBusinessPropertyCommand, Result>
    {
        private readonly ILogger<DeleteBusinessPropertyCommandHandler> _logger = logger;
        private readonly IEfRepository<BusinessPropertyEntity> _repository = repository;
        public async Task<Result> Handle(DeleteBusinessPropertyCommand request, CancellationToken cancellationToken)
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
                _logger.LogError($"خطای حذف ویژگی کسب و کار رخ داده است - {ex.Message}");
            }
            return Result.Fail();
        }
    }
}
