using _Framework.Application;
using _Framework.Infrastructure;
using DiscountMgmt.Application.Contract.ColleagueDiscount;
using DiscountMgmt.Application.Contract.CustomerDiscount;
using DiscountMgmt.Domain.CustomerDiscountAgg;
using ShopMgmt.Infrastructure.EFCore;

namespace DiscountMgmt.Infrastructure.EFCore.Repository;

public class CustomerDiscountRepository : RepositoryBase<long, CustomerDiscount>, ICustomerDiscountRepository
{
    private readonly DiscountContext _context;
    private readonly ShopContext _shopContexy;

    public CustomerDiscountRepository(DiscountContext context, ShopContext shopContexy) : base(context)
    {
        _context = context;
        _shopContexy = shopContexy;
    }

    public EditCustomerDiscount GetDetails(long id)
    {
        return _context.CustomerDiscounts
            .Select(x => new EditCustomerDiscount()
            {
                Id = x.Id,
                ProductId = x.ProductId,
                DiscountRate = x.DiscountRate,
                StartDate = x.StartDate.ToFarsi(), 
                EndDate = x.EndDate.ToFarsi(), 
                Reason = x.Reason
            })
            .FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<CustomerDiscountViewModel> Search(CustomerDiscountSearchViewModel search)
    {
        var products = _shopContexy.Products.Select(x => new { x.Id, x.Name }).ToList();

        var query = _context.CustomerDiscounts
            .Select(x => new CustomerDiscountViewModel()
            {
                Id = x.Id,
                ProductId = x.ProductId,
                DiscountRate = x.DiscountRate,
                Reason = x.Reason,
                CreationDate = x.CreationDate.ToFarsi(),

                StartDate = x.StartDate.ToFarsi(), // to persian calender
                EndDate = x.EndDate.ToFarsi(),

                StartDateEn = x.StartDate, // goergian calender
                EndDateEn = x.EndDate
            });

        if (search.ProductId > 0)
        {
            query = query.Where(x => x.ProductId == search.ProductId);
        }

        if (!string.IsNullOrWhiteSpace(search.StartDate))
        {
            query = query.Where(x => x.StartDateEn >= search.StartDate.ToGeorgianDateTime());
        }

        if (!string.IsNullOrWhiteSpace(search.EndDate))
        {
            query = query.Where(x => x.EndDateEn <= search.EndDate.ToGeorgianDateTime());
        }

        var discounts = query.OrderByDescending(x => x.Id).ToList();
        discounts.ForEach(discount =>
            discount.Product =
                products.FirstOrDefault(p => p.Id == discount.ProductId).Name);

        return discounts;
    }
}
