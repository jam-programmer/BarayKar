using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities.Business;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Cqrs.BusinessPicture
{
    public class DeleteBusinessPictureCommand : IRequest<Result>
    {
        public Guid Id { set; get; }
    }
    public class DeleteBusinessPictureCommandHandler
        (IEfRepository<BusinessPictureEntity> repository,ILogger<DeleteBusinessPictureCommandHandler> logger)
        : IRequestHandler<DeleteBusinessPictureCommand, Result>
    {
        private readonly ILogger<DeleteBusinessPictureCommandHandler> _logger=logger;
        private readonly IEfRepository<BusinessPictureEntity> _repository = repository;
        public async Task<Result> Handle(DeleteBusinessPictureCommand request, CancellationToken cancellationToken)
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
                _logger.LogError($"خطای حذف تصویر کسب و کار رخ داده است - {ex.Message}");
            }
            return Result.Fail();
        }
    }
}
