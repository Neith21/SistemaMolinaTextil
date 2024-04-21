using MolinaTextileSystem.Data;
using MolinaTextileSystem.Repositories.CustomerOrders;
using MolinaTextileSystem.Repositories.CustomersOrdersDetails;
using MolinaTextileSystem.Repositories.Employees;
using MolinaTextileSystem.Repositories.Estado;
using MolinaTextileSystem.Repositories.LoginCredentials;
using MolinaTextileSystem.Repositories.Products;
using MolinaTextileSystem.Repositories.Suppliers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddScoped<ILoginCredentialRepository, LoginCredentialRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ICustomerOrderRepository, CustomerOrderRepository>();
builder.Services.AddScoped<IStateRepository, StateRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomersOrdersDetailsRepository, CustomersOrdersDetailsRepository>();


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
