using MolinaTextilSystem.Data;
using MolinaTextilSystem.Repositories.CustomerOrder;
using MolinaTextilSystem.Repositories.InventoryRawMaterials;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddScoped<IInventoryRawMaterialsRepository, InventoryRawMaterialsRepository>();

builder.Services.AddScoped<ICustomerOrderRepository, CustomerOrderRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
