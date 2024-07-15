using System.ComponentModel.DataAnnotations;

namespace Application.Common.Enum
{
    public enum Status
    {
        [Display(Name = "پذیرفتن")]
        Accepted = 0,
        [Display(Name = "عدم پذیرش")]
        Rejected = 1,
        [Display(Name = "در انتظار بازبینی")]
        Waiting = 2
    }
}
