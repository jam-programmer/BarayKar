using Application.Common;
using Application.Common.Extension;
using Application.Common.Interfaces;
using Domain.Entities.System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Cqrs.Province
{
    public class DeleteProvinceCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
    public class DeleteProvinceCommandHandler 
        (IEfRepository<ProvinceEntity> repository,
        ILogger<DeleteProvinceCommandHandler> logger):
        IRequestHandler<DeleteProvinceCommand, Result>
    {
        private readonly ILogger<DeleteProvinceCommandHandler> _logger = logger;
        private readonly IEfRepository<ProvinceEntity> _repository=repository;
        public async Task<Result> Handle(DeleteProvinceCommand request,
            CancellationToken cancellationToken)
        {
            var query = await _repository.GetByQueryAsync();
            var entity = await query.Include(i=>i.cities).
                SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                return Result.Fail(FailMessage.Internal);
            }
            if (entity.cities!.Any())
            {
                return Result.Fail(FailMessage.HaveChild);
            }

            string TempImagePath = entity.Image!;
            try
            {
                await _repository.DeleteAsync(entity);
                var resultImageRemove = TempImagePath.RemoveImage("Province");
                if (!resultImageRemove.IsSuccess)
                {
                    _logger.LogWarning($"خطای حذف تصویر استان رخ داده است");
                }
                return Result.Success();

            }
            catch (Exception ex)
            {
                _logger.LogError($"خطای خذف استان رخ داده است - {ex.Message}");
            }
            return Result.Fail();
        }
    }
}
