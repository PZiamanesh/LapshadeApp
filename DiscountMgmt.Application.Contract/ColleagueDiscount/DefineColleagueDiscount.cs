using _Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace DiscountMgmt.Application.Contract.ColleagueDiscount;

public record DefineColleagueDiscount
{
    [Range(1,100_000,ErrorMessage = ValidationMessage.IsRequired)]
    public long ProductId { get; set; }

    [Range(1, 99, ErrorMessage = "درصد وارد شده صحیح نیست.")]
    public int DiscountRate { get; set; }

    public IEnumerable<ProductViewModel> Products { get; set; }
}
