using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities.Employment;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Cqrs.Employment
{
    public class DeleteEmploymentCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
    public class DeleteEmploymentCommandHandler 
        (IEfRepository<EmploymentEntity> repository,
        ILogger<DeleteEmploymentCommandHandler> logger):
        IRequestHandler<DeleteEmploymentCommand, Result>
    {
        private readonly ILogger<DeleteEmploymentCommandHandler> _logger = logger;
        private readonly IEfRepository<EmploymentEntity> _repository = repository;
        public async Task<Result> Handle(DeleteEmploymentCommand request,
            CancellationToken cancellationToken)
        {
            var query = await _repository.GetByQueryAsync();
            var entity = await query.SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
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
                _logger.LogError($"خطای حذف آگهی رخ داده است - {ex.Message}");
            }
            return Result.Fail();
        }
    }
}
