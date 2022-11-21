using _Framework.Infrastructure;
using InventoryMgmt.Application.Contract.Inventory;
using InventoryMgmt.Domain.InventoryAgg;
using ShopMgmt.Infrastructure.EFCore;

namespace InventoryMgmt.Infrastructure.EFCore.Repository;

public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
{
    private readonly InventoryContext _context;
    private readonly ShopContext _shopContext;

    public InventoryRepository(InventoryContext context, ShopContext shopContext) : base(context)
    {
        _context = context;
        _shopContext = shopContext;
    }

    public Inventory GetByProduct(long productId)
    {
        return _context.Inventory.FirstOrDefault(x => x.ProductId == productId);
    }

    public EditInventory GetDetails(long id)
    {
        return _context.Inventory.Select(x => new EditInventory()
        {
            Id = id,
            ProductId = x.ProductId,
            UnitPrice = x.UnitPrice
        }).FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<InventoryViewModel> Search(InventorySearchViewModel search)
    {
        var products = _shopContext.Products.Select(x => new { x.Id, x.Name });
        var query = _context.Inventory.Select(x => new InventoryViewModel()
        {
            Id = x.Id,
            ProductId = x.ProductId,
            UnitPrice = x.UnitPrice,
            InStock = x.InStock,
            CurrentCount = x.CalculateCurrentCount()
        });

        if (search.ProductId > 0)
        {
            query = query.Where(x => x.ProductId == search.ProductId);
        }

        if (search.InStock)
        {
            query = query.Where(x => !x.InStock);
        }

        var inventory = query.ToList();
        inventory.ForEach(x => x.Product = products.FirstOrDefault(p=>p.Id == x.ProductId).Name);

        return inventory;
    }
}
