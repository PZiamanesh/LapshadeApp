using _Framework.Application;
using InventoryMgmt.Application;
using InventoryMgmt.Application.Contract.Inventory;
using InventoryMgmt.Domain.InventoryAgg;
using InventoryMgmt.Infrastructure.EFCore;
using InventoryMgmt.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryMgmt.Infrastructure.Configuration;

public class InventoryMgmtBootstrapper
{
    public static void ConfigureService(IServiceCollection service, string connectionString)
    {
        service.AddScoped<IInventoryRepository, InventoryRepository>();
        service.AddScoped<IInventoryApplication, InventoryApplication>();

        service.AddDbContext<InventoryContext>(opt => opt.UseSqlServer(connectionString));
    }
}
