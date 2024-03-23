using MolinaTextilSystem.Data;
using MolinaTextilSystem.Repositories.CustomerOrder;
using MolinaTextilSystem.Repositories.InventoryRawMaterials;
using MolinaTextilSystem.Repositories.Login;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddScoped<IInventoryRawMaterialsRepository, InventoryRawMaterialsRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
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
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
