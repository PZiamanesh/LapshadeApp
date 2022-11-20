namespace InventoryMgmt.Application.Contract.Inventory;

public record InventorySearchViewModel
{
    public long ProductId { get; set; }
    public bool InStock { get; set; }
}
