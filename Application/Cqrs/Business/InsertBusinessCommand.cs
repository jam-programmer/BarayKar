using Application.Common;
using Application.Common.Enum;
using Application.Common.Interfaces;
using Domain.Entities.Business;
using Domain.Entities.System;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Application.Cqrs.Business
{
    public class InsertBusinessCommand:IRequest<Result>
    {
        public Status status { set; get; }
        public string? Link { set; get; }
        [Required(ErrorMessage =ValidationMessage.RequirdProperty)]
        public string? AccountName { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdProperty)]
        public string? About { set; get; }
        public string? Address { set; get; }
        public string? Instagram { set; get; }
        public string? WhatsApp { set; get; }
        public string? Linkdin { set; get; }
        public string? FaceBook { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdProperty)]
        public string? PhoneNumber { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdProperty)]
        public string? Email { set; get; }
        public string? Latitude { set; get; }
        public string? Longitude { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdSelection)]
        public Guid UserId { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdSelection)]
        public Guid ProvinceId { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdSelection)]
        public Guid CityId { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdSelection)]
        public Guid CategoryId { set; get; }

        public bool IsActive { set; get; }
        public bool IsTime { set; get; }
    }
    public class InsertBusinessCommandHandler 
        (IEfRepository<BusinessEntity> repository,ILogger<InsertBusinessCommandHandler> logger):
        IRequestHandler<InsertBusinessCommand, Result>
    {
        private readonly ILogger<InsertBusinessCommandHandler> _logger=logger;
        private readonly IEfRepository<BusinessEntity> _repository = repository;
        public async Task<Result> Handle(InsertBusinessCommand request,
            CancellationToken cancellationToken)
        {
            BusinessEntity entity = request.Adapt<BusinessEntity>();
            try
            {
                await _repository.InsertAsync(entity, cancellationToken);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError($"خطای ایجاد کسب و کار رخ داده است-{ex.Message}");
            }
            return Result.Fail(FailMessage.Internal);
        }
    }
}
