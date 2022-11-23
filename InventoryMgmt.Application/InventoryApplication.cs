using _Framework.Application;
using InventoryMgmt.Application.Contract.Inventory;
using InventoryMgmt.Domain.InventoryAgg;

namespace InventoryMgmt.Application;

public class InventoryApplication : IInventoryApplication
{
    private readonly IInventoryRepository _inventoryRepository;

    public InventoryApplication(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public OperationResult Create(CreateInventory command)
    {
        var result = new OperationResult();
        if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId))
        {
            return result.Failed(ApplicationMessage.DuplicatedRecord);
        }

        var inventory = new Inventory(command.ProductId, command.UnitPrice);
        _inventoryRepository.Create(inventory);
        _inventoryRepository.Save();
        return result.Succeeded();
    }

    public OperationResult Edit(EditInventory command)
    {
        var result = new OperationResult();
        var inventory = _inventoryRepository.Get(command.Id);
        if (inventory is null)
        {
            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        inventory.Edit(command.ProductId, command.UnitPrice);
        _inventoryRepository.Save();
        return result.Succeeded(ApplicationMessage.RecordEdited);
    }

    public EditInventory GetDetails(long id)
    {
        return _inventoryRepository.GetDetails(id);
    }

    public OperationResult Increase(IncreaseInventory command)
    {
        var result = new OperationResult();
        var inventory = _inventoryRepository.Get(command.InventoryId);
        if (inventory is null)
        {
            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        inventory.Increase(command.Count, 1, command.Description);
        _inventoryRepository.Save();
        return result.Succeeded();
    }

    public OperationResult Decrease(DecreaseInventory command)
    {
        var result = new OperationResult();
        var inventory = _inventoryRepository.Get(command.InventoryId);
        if (inventory is null)
        {
            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        inventory.Decrease(command.Count, 1, command.Description, 0);
        _inventoryRepository.Save();
        return result.Succeeded();
    }

    public OperationResult Decrease(List<DecreaseInventory> command)
    {
        var result = new OperationResult();
        foreach (var item in command)
        {
            var inventory = _inventoryRepository.GetByProduct(item.ProductId); // why not use inventoryId ??
            inventory.Decrease(item.Count, 1, item.Description, item.OrderId);
        }
        _inventoryRepository.Save();
        return result.Succeeded();
    }

    public IEnumerable<InventoryViewModel> Search(InventorySearchViewModel search)
    {
        return _inventoryRepository.Search(search);
    }

    public IEnumerable<InventroyOperationViewModel> GetOperationLog(long id)
    {
        return _inventoryRepository.GetOperationLog(id);
    }
}