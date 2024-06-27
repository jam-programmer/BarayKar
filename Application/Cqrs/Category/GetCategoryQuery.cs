using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities.System;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Cqrs.Category
{
    public class GetCategoryQuery : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
    public class GetCategoryQueryHandler
        (IEfRepository<CategoryEntity> repository,
        ILogger<GetCategoryQueryHandler>logger):
        IRequestHandler<GetCategoryQuery, Result>
    {
        private readonly ILogger<GetCategoryQueryHandler> _logger = logger;
        private readonly IEfRepository<CategoryEntity> _repository = repository;
        public async Task<Result> Handle(GetCategoryQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                _logger.LogWarning("دسته بندی پیدا نشد.");
                return Result.Fail();
            }
            UpdateCategoryCommand command = entity.Adapt<UpdateCategoryCommand>();
            command.ImagePath = entity.Image;
            return Result.Success(command);
        }
    }

}
