using _Framework.Application;
using _Framework.Infrastructure;
using DiscountMgmt.Application.Contract.ColleagueDiscount;
using DiscountMgmt.Domain.ColleagueDiscountAgg;
using ShopMgmt.Infrastructure.EFCore;

namespace DiscountMgmt.Infrastructure.EFCore.Repository;

public class ColleagueDiscountRepository : RepositoryBase<long, ColleagueDiscount>, IColleagueDiscountRepository
{
    private readonly DiscountContext _context;
    private readonly ShopContext _shopContexy;

    public ColleagueDiscountRepository(DiscountContext context, ShopContext shopContexy) : base(context)
    {
        _context = context;
        _shopContexy = shopContexy;
    }

    public EditColleagueDiscount GetDetails(long id)
    {
        return _context.ColleagueDiscounts
            .Select(x => new EditColleagueDiscount()
            {
                Id = x.Id,
                ProductId = x.ProductId,
                DiscountRate = x.DiscountRate
            })
            .FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchViewModel search)
    {
        var products = _shopContexy.Products.Select(x => new { x.Id, x.Name }).ToList();

        var query = _context.ColleagueDiscounts
            .Select(x => new ColleagueDiscountViewModel()
            {
                Id = x.Id,
                ProductId = x.ProductId,
                DiscountRate = x.DiscountRate,
                CreationDate = x.CreationDate.ToFarsi()
            });

        if (search.ProductId > 0)
        {
            query = query.Where(x => x.ProductId == search.ProductId);
        }

        var discounts = query.OrderByDescending(x => x.Id).ToList();
        discounts.ForEach(discount =>
            discount.Product =
                products.FirstOrDefault(p => p.Id == discount.ProductId).Name);

        return discounts;
    }
}
