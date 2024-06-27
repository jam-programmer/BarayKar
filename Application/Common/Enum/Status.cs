using System.ComponentModel.DataAnnotations;

namespace Application.Common.Enum
{
    public enum Status
    {
        [Display(Name = "پذیرفتن")]
        Accepted,
        [Display(Name = "عدم پذیرش")]
        Rejected,
        [Display(Name = "در انتظار بازبینی")]
        Waiting
    }
}
