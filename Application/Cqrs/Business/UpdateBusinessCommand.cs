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
    public class UpdateBusinessCommand : IRequest<Result>
    {
        public bool IsActive { set; get; }
        public bool IsTime { set; get; }
        public Status status { set; get; }
        public Guid Id { get; set; }
        public string? Link { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdProperty)]
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
    }
    public class UpdateBusinessCommandHandler 
        (IEfRepository<BusinessEntity> repository,ILogger<UpdateBusinessCommandHandler> logger): IRequestHandler<UpdateBusinessCommand, Result>
    {

        private readonly ILogger<UpdateBusinessCommandHandler> _logger = logger;
        private readonly IEfRepository<BusinessEntity> _repository = repository;
        public async Task<Result> Handle(UpdateBusinessCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                return Result.Fail(FailMessage.Internal);
            }

         
            long code = (long)entity.Code!;
            entity = request.Adapt<BusinessEntity>();
            entity.Code = code;
            try
            {
                await _repository.UpdateAsync(entity);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError($"خطای ویرایش کسب و کار رخ داده است - {ex.Message}");
            }
            return Result.Fail(FailMessage.Internal);
        }
    }
}
