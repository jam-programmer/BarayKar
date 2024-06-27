using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Common.Record.UserPanel
{
    public record AddBusinessRecord
    {
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
        public Guid ProvinceId { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdSelection)]
        public Guid CityId { set; get; }
        [Required(ErrorMessage = ValidationMessage.RequirdSelection)]
        public Guid CategoryId { set; get; }
        public Guid UserId { set; get; }
        public ICollection<IFormFile>? Files { set; get; }
        public string? JsonTime { set; get; }
        public string? JsonKeyValue { set; get; }
    }
}
