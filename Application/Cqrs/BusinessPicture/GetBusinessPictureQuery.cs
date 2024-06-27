using Application.Common;
using Application.Common.Interfaces;
using Application.Cqrs.City;
using Domain.Entities.Business;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Cqrs.BusinessPicture
{
    public class GetBusinessPictureQuery:IRequest<Result>
    {
        public Guid Id { get; set; }    
    }
    public class GetBusinessPictureQueryHandler
        (IEfRepository<BusinessPictureEntity> repository,ILogger<GetBusinessPictureQueryHandler> logger)
        : IRequestHandler<GetBusinessPictureQuery, Result>
    {
        private readonly ILogger<GetBusinessPictureQueryHandler> _logger = logger;
        private readonly IEfRepository<BusinessPictureEntity> _repository = repository;
        public async Task<Result> Handle(GetBusinessPictureQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                _logger.LogWarning("تصویر پیدا نشد.");
                return Result.Fail();
            }
            UpdateBusinessPictureCommand command = entity.Adapt<UpdateBusinessPictureCommand>();
            command.ImagePath = entity.Image;
            return Result.Success(command);
        }
    }
}
