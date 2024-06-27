using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class ValidationMessage
    {
        public const string RequirdFirstName = "نام الزامی است.";
        public const string RequirdLastName = "نام خانوادگی الزامی است.";
        public const string RequirdNationalCode = "کد ملی الزامی است.";
        public const string RequirdEmail = "ایمیل الزامی است.";
        public const string RequirdPassword = "گذرواژه الزامی است.";
        public const string RequirdPhoneNumber = "شماره موبایل الزامی است.";
        public const string RequirdRoles = "انتخاب نقش کاربر الزامی است.";

        public const string RequirdPersianName = "نام فارسی الزامی است.";
        public const string RequirdEnglishName = "نام انگلیسی الزامی است.";

        public const string RequirdCategoryName = "نام دسته بندی الزامی است.";
        public const string RequirdProperty = "وارد کردن مقدار الزامی است.";
        public const string RequirdSelection = "یک مقدار انتخاب کنید.";
        public const string RequirdUpload = "لطفا فایل را آپلود نمائید.";
    }
}
