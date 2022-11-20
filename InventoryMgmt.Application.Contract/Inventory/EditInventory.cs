namespace InventoryMgmt.Application.Contract.Inventory;

public record EditInventory : CreateInventory
{
    public long Id { get; set; }
}
