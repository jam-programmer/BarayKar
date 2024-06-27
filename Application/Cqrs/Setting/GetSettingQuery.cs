using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities.System;
using Mapster;
using MediatR;

namespace Application.Cqrs.Setting
{
    public class GetSettingQuery:IRequest<Result>
    {
    }
    public class GetSettingQueryHandler 
        (IEfRepository<SettingEntity> repository): IRequestHandler<GetSettingQuery, Result>
    {
        private readonly IEfRepository<SettingEntity> _repository = repository;
        public async Task<Result> Handle(GetSettingQuery request, CancellationToken cancellationToken)
        {
            var model = await _repository.FirstOrDefaultAsync(cancellationToken);
            UpdateSettingCommand command = new();
            command=model.Adapt<UpdateSettingCommand>();
          
            return Result.Success(command);
        }
    }
}
