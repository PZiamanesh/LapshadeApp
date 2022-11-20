namespace InventoryMgmt.Application.Contract.Inventory;

public record DecreaseInventory
{
    public long ProductId { get; set; }
    public int Count { get; set; }
    public string Description { get; set; }
    public long OrderId { get; set; }
}