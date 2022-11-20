using _Framework.Application;
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

        service.AddDbContext<InventoryContext>(opt => opt.UseSqlServer(connectionString));
    }
}
