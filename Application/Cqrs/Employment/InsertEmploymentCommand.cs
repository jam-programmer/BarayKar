using Application.Common;
using Application.Common.Enum;
using Application.Common.Interfaces;
using Domain.Entities.Employment;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Application.Cqrs.Employment
{
    public class InsertEmploymentCommand : IRequest<Result>
    {
        public string? BusinessDays { set; get; }
        public Status status { set; get; }
        public bool IsActive { set; get; } = false;
        [Required(ErrorMessage =ValidationMessage.RequirdProperty)]
        public string? Title { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdProperty)]
        public string? Description { set; get; }
        public string? Age { set; get; }
        public Gender? Gender { set; get; }
        public string? StartTime { set; get; }
        public string? EndTime { set; get; }
        public string? TypeOfCooperation { set; get; }
        public string? WorkExperience { set; get; }
        public string? Salary { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdSelection)]
        public Guid? UserId { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdSelection)]
        public Guid? ProvinceId { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdSelection)]
        public Guid? CityId { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdSelection)]
        public Guid? BusinessId { set; get; }
    }

    public class InsertEmploymentCommandHandler(
        ILogger<InsertEmploymentCommandHandler> logger,
        IEfRepository<EmploymentEntity> repository) :
        IRequestHandler<InsertEmploymentCommand, Result>
    {
        private readonly ILogger<InsertEmploymentCommandHandler> _logger = logger;
        private readonly IEfRepository<EmploymentEntity> _repository = repository;
        public async Task<Result> Handle(InsertEmploymentCommand request, CancellationToken cancellationToken)
        {
            EmploymentEntity entity = request.Adapt<EmploymentEntity>();
            try
            {
                await _repository.InsertAsync(entity, cancellationToken);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError($"خطای ایجاد آگهی رخ داده است - {ex.Message}");
            }
            return Result.Fail(FailMessage.Internal);
        }
    }
}
