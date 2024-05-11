using MolinaTextileSystem.Data;
using MolinaTextileSystem.Repositories.CustomerOrders;
using MolinaTextileSystem.Repositories.CustomersOrdersDetails;
using MolinaTextileSystem.Repositories.Employees;
using MolinaTextileSystem.Repositories.Estado;
using MolinaTextileSystem.Repositories.LoginCredentials;
using MolinaTextileSystem.Repositories.PatternDetails;
using MolinaTextileSystem.Repositories.Products;
using MolinaTextileSystem.Repositories.Suppliers;
using MolinaTextileSystem.Repositories.Pattern;
using MolinaTextileSystem.Repositories.Category;
using MolinaTextileSystem.Repositories.RawMeterials;
using MolinaTextileSystem.Repositories.RawMaterials;

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
builder.Services.AddScoped<IPatternDetailRepository, PatternDetailRepository>();
builder.Services.AddScoped<IPatternRepository, PatternRepository>();
builder.Services.AddScoped<ICategoryRepository, Categoryrepository>();
builder.Services.AddScoped<IRawMaterialsRepository, RawMaterialsRepository>();

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/LoginCredential/Login"; // Asegúrate de que esta ruta apunte al método de tu controlador que maneja el login.
        options.AccessDeniedPath = "/LoginCredential/AccessDenied"; // Puedes configurar esta ruta según sea necesario.
        options.Cookie.Name = "MolinaTextileSystemAuthCookie";
    });


var app = builder.Build();

app.Use(async (context, next) =>
{
    context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
    context.Response.Headers["Pragma"] = "no-cache";
    context.Response.Headers["Expires"] = "0";
    await next();
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=LoginCredential}/{action=Login}/{id?}");

app.Run();
