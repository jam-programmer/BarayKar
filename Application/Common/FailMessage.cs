namespace Application.Common
{
    public class FailMessage
    {
        public const string SignUpError = "امکان ثبت نام وجود ندارد به پشتیبان سایت اطلاع دهید.";
        public const string NotFound = "نتیجه‌ای یافت نشد";
        public const string Internal = "خطای داخلی رخ داده است؛ به پشتیبان اطلاع دهید.";
        public const string ImagePathNotFound = "تصویر در مسیر پیدا نشد.";
        public const string InvalidId = "شناسه درخواستی نامعتبر است.";
        public const string EqualParentId = "یک موجودیت نمیتواند به خودش وابسته باشد.";
        public const string HaveChild = "موجودیت دارای وابستگی است.";

        public const string UserNotFound = "کدملی یا رمز عبور اشتباه است.";

        public const string AccountNotActive = "حساب کاربری فعال نیست.";
        public const string AccountLock = "حساب کاربری مسدود شده.(چند دقیقه دیگر امتحان کنید یا با پشتیبان ارتباط برقرار کنید)";

        public const string PublicInternalError = "خطای داخلی رخ داده است، کارشناسان ما در حال بررسی هستند.";
    }
}
