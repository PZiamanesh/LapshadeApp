namespace InventoryMgmt.Application.Contract.Inventory;

public record IncreaseInventory
{
    public long InventoryId { get; set; }
    public int Count { get; set; }
    public string Description { get; set; }
}
