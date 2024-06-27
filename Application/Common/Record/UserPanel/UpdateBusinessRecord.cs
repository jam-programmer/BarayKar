using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Application.Common.Record.UserPanel
{
    public class UpdateBusinessRecord
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
        public string? JsonTime { set; get; } = "[]";
        public string? JsonKeyValue { set; get; } = "[]";
        public string? JsonRemoveTime { set; get; } = "[]";
        public string? JsonRemoveKeyValue { set; get; } = "[]";
        public string? JsonRemoveImage { set; get; } = "[]";
        public Guid Id { set; get; }
        [BindNever]
        public ICollection<PictureBusinessRecord>? Pictures { set; get; }
        [BindNever]
        public ICollection<PropertyBusinessRecord>? Properties { set; get; }
        [BindNever]
        public ICollection<TimeBusinessRecord>? Times { set; get; }
    }
}
