using _Framework.Domain;
using InventoryMgmt.Application.Contract.Inventory;

namespace InventoryMgmt.Domain.InventoryAgg;

public interface IInventoryRepository : IRepository<long, Inventory>
{
    EditInventory GetDetails(long id);

    IEnumerable<InventoryViewModel> Search(InventorySearchModel search);

    Inventory GetByProduct(long productId);

    IEnumerable<InventroyOperationViewModel> GetOperationLog(long id);
}
