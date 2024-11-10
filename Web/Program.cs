using Microsoft.EntityFrameworkCore;
using Objects.Models;
using Repository.Interface;
using Repository.Repository;
using DataAccess.DAO;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//register dao
builder.Services.AddScoped<CategoryDAO>();
builder.Services.AddScoped<CustomerDAO>();
builder.Services.AddScoped<ImportProductDAO>();
builder.Services.AddScoped<InvoiceDAO>();
builder.Services.AddScoped<InvoiceDetailDAO>();
builder.Services.AddScoped<ProductDAO>();
// Register repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IImportProductRepository, ImportProductRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceDetailRepository, InvoiceDetailRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
//register httpclient
builder.Services.AddHttpClient();
//
builder.Services.AddDbContext<MyDBShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDB")));
//register odata
var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<Category>("Categories");

builder.Services.AddControllers().AddOData(
    options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null).AddRouteComponents(
        routePrefix: "odata",
        model: modelBuilder.GetEdmModel()));
// Add CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyFrontend", builder =>
    {
        builder
            .WithOrigins("https://localhost:7107")  // Allow the frontend URL
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowMyFrontend");

app.MapControllers();

app.Run();
