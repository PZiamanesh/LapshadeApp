using _Framework.Application;
using AccountMgmt.Infrastructure.Configuration;
using BlogMgmt.Infrastructure.Configuration;
using CommentMgmt.Infrastructure.Configuration;
using DiscountMgmt.Infrastructure.Configuration;
using InventoryMgmt.Infrastructure.Configuration;
using ServiceHost;
using ShopMgmt.Infrastructure.Configuration;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("LampshadeDb");

ShopMgmtBootstrapper.ConfigureService(builder.Services, connectionString);
DiscountMgmtBootstrapper.ConfigureService(builder.Services, connectionString);
InventoryMgmtBootstrapper.ConfigureService(builder.Services, connectionString);
BlogMgmtBootstrapper.ConfigureService(builder.Services, connectionString);
CommentMgmtBootstrapper.ConfigureService(builder.Services, connectionString);
AccountMgmtBootstrapper.ConfigureService(builder.Services, connectionString);

builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));
builder.Services.AddSingleton<IFileUploader, FileUploader>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
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
