using _Framework.Application;

namespace InventoryMgmt.Application.Contract.Inventory;

public interface IInventoryApplication
{
    OperationResult Create(CreateInventory command);
    OperationResult Edit(EditInventory command);
    EditInventory GetDetails(long id);
    IEnumerable<InventoryViewModel> Search(InventorySearchViewModel search);
    OperationResult Increase(IncreaseInventory command);
    OperationResult Decrease(DecreaseInventory command); // inventoryWorker
    OperationResult Decrease(List<DecreaseInventory> command); // client
}