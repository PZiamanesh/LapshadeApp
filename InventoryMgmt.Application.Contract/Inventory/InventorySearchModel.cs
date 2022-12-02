namespace InventoryMgmt.Application.Contract.Inventory;

public record InventorySearchModel
{
    public long ProductId { get; set; }
    public bool InStock { get; set; }
}
