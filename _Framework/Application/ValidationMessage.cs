using System.Drawing;

namespace _Framework.Application;

public static class ValidationMessage
{
    public const string IsRequired = "این بخش الزامی است.";
    public const string MinLength = "حداقل سه کاراکتر الزامی است.";
    public const string PictureSize = $"حداکثر حجم فایل 300 کیلوبایت است.";
    public const string PictureType = $"نوع عکس نباید غیر از jpg, jpeg, png باشد.";
}
