using _Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace DiscountMgmt.Application.Contract.CustomerDiscount;

public record DefineCustomerDiscount
{
    [Range(1, 100_000, ErrorMessage = ValidationMessage.IsRequired)]
    public long ProductId { get; set; }

    [Range(1, 99, ErrorMessage = "درصد وارد شده صحیح نیست.")]
    public int DiscountRate { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string StartDate { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string EndDate { get; set; }

    public string Reason { get; set; }
    public IEnumerable<ProductViewModel> Products { get; set; }
}
