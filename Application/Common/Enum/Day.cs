
using System.ComponentModel.DataAnnotations;


namespace Application.Common.Enum
{
    public enum Day
    {
        [Display(Name ="دوشنبه")]
        Monday,
        [Display(Name = "سه شنبه")]
        Tuesday,
        [Display(Name = "چهارشنبه")]
        Wednesday,
        [Display(Name = "پنجشنبه")]
        Thursday,
        [Display(Name = "جمعه")]
        Friday,
        [Display(Name = "شنبه")]
        Saturday,
        [Display(Name = "یکشنبه")]
        Sunday
    }
}
