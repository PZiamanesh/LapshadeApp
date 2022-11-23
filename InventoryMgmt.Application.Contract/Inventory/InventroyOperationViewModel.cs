namespace InventoryMgmt.Application.Contract.Inventory;

public record InventroyOperationViewModel
{
    public long Id { get; set; }

    public long InventoryId { get; set; }

    public bool Operation { get; set; }

    public int Count { get; set; }

    public long OperatorId { get; set; }

    public string Operator { get; set; }

    public long OrderId { get; set; }

    public string OperationDate { get; set; }

    public int CurrentCount { get; set; }

    public string Description { get; set; }
}
