namespace InventoryMgmt.Application.Contract.Inventory;

public record CreateInventory
{
    public long ProductId { get; set; }
    public double UnitPrice { get; set; }
}
