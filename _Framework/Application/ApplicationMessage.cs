using System.Reflection.Metadata.Ecma335;

namespace _Framework.Application;

public static class ApplicationMessage
{
    public const string DuplicatedRecord = "نام وارد شده از قبل موجود می باشد، لطفا نام دیگری انتخاب کنید.";
    public const string RecordNotFound = "رکوردی با این مشخصات پیدا نشد، لطفا مجدداً تلاش کنید.";
    public const string RecordEdited = "عملیات ویرایش با موفقیت انجام شد.";
    public const string NoSlug = "slug-is-not-defined";
    public const string PasswordNotMatched = "پسورد و تکرار آن مطابقت ندارند";
    public const string WrongLoginInfo = "نام کاربری یا رمز عبور اشتباه است";
}
