using _Framework.Domain;

namespace InventoryMgmt.Domain.InventoryAgg;

public class Inventory : EntityBase<long>
{
    public long ProductId { get; private set; }
    public double UnitPrice { get; private set; }
    public bool InStock { get; private set; }

    // composition relation
    public List<InventoryOperation> Operations { get; set; }

    public Inventory(long productId, double unitPrice)
    {
        ProductId = productId;
        UnitPrice = unitPrice;
        InStock = false;
    }

    public void Edit(long productId, double unitPrice)
    {
        ProductId = productId;
        UnitPrice = unitPrice;
    }

    public int CalculateCurrentCount()
    {
        var plus = Operations.Where(x => x.Operation).Sum(x => x.Count);
        var minus = Operations.Where(x => !x.Operation).Sum(x => x.Count);
        return plus - minus;
    }

    public void Increase(int count, long operatorId, string description)
    {
        var currentCount = CalculateCurrentCount() + count;
        var operation = new InventoryOperation(true,
            count,
            operatorId,
            currentCount,
            description,
            0,
            Id);

        Operations.Add(operation);
        InStock = currentCount > 0;
    }

    public void Decrease(int count, long operatorId, string description, long orderId)
    {
        var currentCount = CalculateCurrentCount() - count;
        var operation = new InventoryOperation(false,
            count,
            operatorId,
            currentCount,
            description,
            orderId,
            Id);

        Operations.Add(operation);
        InStock = currentCount > 0;
    }
}



public class InventoryOperation
{
    public long Id { get; private set; }
    public bool Operation { get; private set; } // increase/decrease status
    public int Count { get; private set; }
    public long OperatorId { get; private set; }
    public long OrderId { get; private set; }
    public DateTime OperationDate { get; private set; }
    public int CurrentCount { get; private set; }
    public string Description { get; private set; }

    // composition relation
    public long InventoryId { get; private set; }
    public Inventory Inventory { get; private set; }

    public InventoryOperation(bool operation,
        int count,
        long operatorId,
        int currentCount,
        string description,
        long orderId,
        long inventoryId)
    {
        Operation = operation;
        Count = count;
        OperatorId = operatorId;
        CurrentCount = currentCount;
        Description = description;
        OrderId = orderId;
        InventoryId = inventoryId;
        OperationDate = DateTime.Now;
    }
}