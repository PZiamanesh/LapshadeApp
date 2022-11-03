namespace _Framework.Application;

public class OperationResult
{
    public bool IsSucceded { get; set; }
    public string? Message { get; set; }

    public OperationResult Success(string message = "عملیات با موفقیت انجام شد.")
    {
        IsSucceded = true;
        Message = message;
        return this;
    }

    public OperationResult Failed(
        string message = "متاسفانه مشکلی در پروسه انجام عملیات رخ داد. لطفا مجدداً تلاش فرمایید.")
    {
        IsSucceded = false;
        Message = message;
        return this;
    }
}