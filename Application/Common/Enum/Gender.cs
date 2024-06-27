using System.ComponentModel.DataAnnotations;

namespace Application.Common.Enum
{
    public enum Gender
    {
        [Display(Name = "آقا")]
        Male,
        [Display(Name = "خانم")]
        Female,
        [Display(Name = "آقا یا خانم")]
        Both
    }
}
