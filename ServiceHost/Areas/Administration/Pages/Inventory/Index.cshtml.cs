using InventoryMgmt.Application.Contract.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMgmt.Application.Contract.Product;

namespace ServiceHost.Areas.Administration.Pages.Inventory;

public class IndexModel : PageModel
{
    private readonly IProductApplication _productApplication;
    private readonly IInventoryApplication _inventoryApplication;

    public IEnumerable<InventoryViewModel>? Inventory { get; set; }
    public InventorySearchViewModel? SearchModel { get; set; }
    public SelectList? Products { get; set; }

    public IndexModel(IProductApplication productApplication,
        IInventoryApplication inventoryApplication)
    {
        _productApplication = productApplication;
        _inventoryApplication = inventoryApplication;
    }

    public void OnGet(InventorySearchViewModel searchModel)
    {
        Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        Inventory = _inventoryApplication.Search(searchModel);
    }

    public IActionResult OnGetCreate()
    {
        var inventory = new CreateInventory() { Products = _productApplication.GetProducts() };
        return Partial("./Create", inventory);
    }

    public JsonResult OnPostCreate(CreateInventory command)
    {
        var result = _inventoryApplication.Create(command);
        if (result.IsSucceeded)
        {
            TempData["InventorySubmission"] = result.Message;
        }
        return new JsonResult(result);
    }

    public IActionResult OnGetEdit(long id)
    {
        var inventory = _inventoryApplication.GetDetails(id);
        inventory.Products = _productApplication.GetProducts();
        return Partial("./Edit", inventory);
    }

    public IActionResult OnPostEdit(EditInventory command)
    {
        var result = _inventoryApplication.Edit(command);
        if (result.IsSucceeded)
        {
            TempData["InventoryEdition"] = result.Message;
        }
        return new JsonResult(result);
    }

    public IActionResult OnGetIncrease(long id)
    {
        var increase = new IncreaseInventory() { InventoryId = id};
        return Partial("./Increase", increase);
    }

    public IActionResult OnPostIncrease(IncreaseInventory command)
    {
        var result = _inventoryApplication.Increase(command);
        if (result.IsSucceeded)
        {
            TempData["InventoryEdition"] = result.Message;
        }
        return new JsonResult(result);
    }

    public IActionResult OnGetDecrease(long id)
    {
        var decrease = new DecreaseInventory() { InventoryId = id };
        return Partial("./Decrease", decrease);
    }

    public IActionResult OnPostDecrease(DecreaseInventory command)
    {
        var result = _inventoryApplication.Decrease(command);
        if (result.IsSucceeded)
        {
            TempData["InventoryEdition"] = result.Message;
        }
        return new JsonResult(result);
    }

    public IActionResult OnGetLog(long id)
    {
        var logs = _inventoryApplication.GetOperationLog(id);
        return Partial("InventoryOperation", logs);
    }
}