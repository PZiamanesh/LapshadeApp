namespace InventoryMgmt.Application.Contract.Inventory;

public record CreateInventory
{
    public long ProductId { get; set; }
    public double UnitPrice { get; set; }
    public IEnumerable<ProductViewModel> Products { get; set; }
}
