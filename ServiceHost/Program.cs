using _Framework.Application;
using DiscountMgmt.Infrastructure.Configuration;
using InventoryMgmt.Infrastructure.Configuration;
using ShopMgmt.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("LampshadeDb");
ShopMgmtBootstrapper.ConfigureService(builder.Services, connectionString);
DiscountMgmtBootstrapper.ConfigureService(builder.Services, connectionString);
InventoryMgmtBootstrapper.ConfigureService(builder.Services, connectionString);

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
