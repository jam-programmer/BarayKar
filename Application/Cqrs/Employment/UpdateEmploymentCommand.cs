using Application.Common;
using Application.Common.Enum;
using Application.Common.Interfaces;
using Domain.Entities.Business;
using Domain.Entities.Employment;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cqrs.Employment
{
    public class UpdateEmploymentCommand:IRequest<Result>
    {
        public string? BusinessDays { set; get; }
        public Status status { set; get; }
        public bool IsActive { set; get; } = false;
        public string? Title { set; get; }
        public string? Description { set; get; }
        public string? Age { set; get; }
        public Gender? Gender { set; get; }
        public string? StartTime { set; get; }
        public string? EndTime { set; get; }
        public string? TypeOfCooperation { set; get; }
        public string? WorkExperience { set; get; }
        public string? Salary { set; get; }

        public Guid? UserId { set; get; }
        public Guid? ProvinceId { set; get; }
        public Guid? CityId { set; get; }
        public Guid? BusinessId { set; get; }
        public Guid Id { set; get; }
    }
    public class UpdateEmploymentCommandHandler 
        (IEfRepository<EmploymentEntity> repository,
        ILogger<UpdateEmploymentCommandHandler> logger)
        : IRequestHandler<UpdateEmploymentCommand, Result>
    {
        private readonly ILogger<UpdateEmploymentCommandHandler> _logger = logger;
        private readonly IEfRepository<EmploymentEntity> _repository = repository;
        public async Task<Result> Handle(UpdateEmploymentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                return Result.Fail(FailMessage.Internal);
            }


            long code = (long)entity.Code!;
            entity = request.Adapt<EmploymentEntity>();
            entity.Code = code;
            try
            {
                await _repository.UpdateAsync(entity);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError($"خطای بروزرسانی آگهی رخ داده است.");
            }
            return Result.Fail(FailMessage.Internal);
        }
    }
}
