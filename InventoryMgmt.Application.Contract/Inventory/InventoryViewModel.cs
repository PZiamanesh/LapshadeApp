namespace InventoryMgmt.Application.Contract.Inventory;

public record InventoryViewModel
{
    public long Id { get; set; }
    public string Product { get; set; }
    public long ProductId { get; set; }
    public string Category { get; set; }
    public double UnitPrice { get; set; }
    public bool InStock { get; set; }
    public int CurrentCount { get; set; }
    public string CreationDate { get; set; }
}
