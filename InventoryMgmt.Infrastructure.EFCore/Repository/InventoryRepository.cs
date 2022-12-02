using _Framework.Application;
using _Framework.Infrastructure;
using InventoryMgmt.Application.Contract.Inventory;
using InventoryMgmt.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
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

    public IEnumerable<InventroyOperationViewModel> GetOperationLog(long id)
    {
        var inventroy = _context.Inventory.Include(x => x.Operations).FirstOrDefault(x => x.Id == id);
        return inventroy.Operations.Select(x => new InventroyOperationViewModel()
        {
            Id = x.Id,
            InventoryId = x.InventoryId,
            Operation = x.Operation,
            Count = x.Count,
            OperatorId = x.OperatorId,
            Operator = "مدیر سیستم",
            OrderId = x.OrderId,
            OperationDate = x.OperationDate.ToFarsi(),
            CurrentCount = x.CurrentCount,
            Description = x.Description
        }).OrderByDescending(x => x.Id).ToList();
    }

    public IEnumerable<InventoryViewModel> Search(InventorySearchModel search)
    {
        var products = _shopContext.Products
            .Include(x=>x.Category)
            .Select(x => new { x.Id, x.Name, x.Category});

        var query = _context.Inventory.Select(x => new InventoryViewModel()
        {
            Id = x.Id,
            ProductId = x.ProductId,
            UnitPrice = x.UnitPrice,
            InStock = x.InStock,
            CurrentCount = x.CalculateCurrentCount(),
            CreationDate = x.CreationDate.ToFarsi()
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

        inventory.ForEach(x =>
        {
            var product = products.FirstOrDefault(p => p.Id == x.ProductId);
            x.Product = product.Name;
            x.Category = product.Category.Name;
        });

        return inventory;
    }
}
