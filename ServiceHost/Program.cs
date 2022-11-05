using ShopMgmt.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

ShopMgmtBootstrapper.ConfigureService(builder.Services, builder.Configuration.GetConnectionString("LampshadeDb"));

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
