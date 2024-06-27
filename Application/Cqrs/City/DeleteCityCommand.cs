using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities.System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Cqrs.City
{
    public class DeleteCityCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
    public class DeleteCityCommandHandler
        (IEfRepository<CityEntity> repository,
        ILogger<DeleteCityCommandHandler> logger)
        : IRequestHandler<DeleteCityCommand, Result>
    {
        private readonly ILogger<DeleteCityCommandHandler> _logger = logger;
        private readonly IEfRepository<CityEntity> _repository = repository;
        public async Task<Result> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
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
                _logger.LogError($"خطای حذف شهر رخ داده است - {ex.Message}");
            }
            return Result.Fail();
        }
    }
}
