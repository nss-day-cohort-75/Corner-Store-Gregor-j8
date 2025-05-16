using CornerStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CornerStore.Models.DTO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core and provides dummy value for testing
builder.Services.AddNpgsql<CornerStoreDbContext>(builder.Configuration["CornerStoreDbConnectionString"] ?? "testing");

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/cashier", (CornerStoreDbContext db, Cashier cashier) =>
{
    Cashier c = new Cashier
    {
        Id = cashier.Id,
        FirstName = cashier.FirstName,
        LastName = cashier.LastName
    };
        db.Cashier.Add(c);
        db.SaveChanges();
        return Results.Created($"/api/material/{c.Id}", c);
 });

app.MapGet("/cashier/{id}", (CornerStoreDbContext db, int id, IMapper mapper) =>
{
    return db.Cashier.Where(c => c.Id == id).ProjectTo<CashierDTO>(mapper.ConfigurationProvider).FirstOrDefault();
});

app.MapGet("/products", (CornerStoreDbContext db, IMapper mapper, string? category) => {
    IQueryable<Product> p = db.Product;
    if (category != null)
    {
        p = p.Where(p => p.Category.CategoryName.ToLower() == category.ToLower());
    }

    return p.ProjectTo<ProductDTO>(mapper.ConfigurationProvider).ToList();
});

app.MapPost("/product", (CornerStoreDbContext db, Product Product) =>
{
    Product p = new Product
    {
        Id = Product.Id,
        ProductName = Product.ProductName,
        Price = Product.Price,
        Brand = Product.Brand,
        CategoryId = Product.CategoryId
    };
        db.Product.Add(p);
        db.SaveChanges();
        return Results.Created($"/api/material/{p.Id}", p);
 });

app.MapPatch("/products/{id}", (CornerStoreDbContext db, int id, string? ProductName, decimal? Price, string? Brand, int? CategoryId) =>
{
    Product p = db.Product.FirstOrDefault(p => p.Id == id);

    if (p != null)
    {
        p.Id = p.Id;
        p.ProductName = ProductName ?? p.ProductName;
        p.Price = Price ?? p.Price;
        p.Brand = Brand ?? p.Brand;
        p.CategoryId = CategoryId ?? p.CategoryId;
        db.SaveChanges();
        return Results.NoContent();
    };
        return Results.NoContent();
});

app.MapGet("/orders", (CornerStoreDbContext db, IMapper mapper) =>
{
    List<Order> orders = db.Order.Include(c => c.Cashier)
    .Include(p => p.OrderProducts)
    .ThenInclude(op => op.Product)
    .ThenInclude(p => p.Category).ToList();
    return orders.AsQueryable().ProjectTo<OrderDTO>(mapper.ConfigurationProvider).ToList();
});

app.MapGet("/orders/paidDate", (CornerStoreDbContext db, DateTime? PaidOnDate) =>
{
    IQueryable<Order> o = db.Order
        .Include(c => c.Cashier)
        .Include(p => p.OrderProducts)
        .ThenInclude(op => op.Product)
        .ThenInclude(p => p.Category);

    if (PaidOnDate != default)
    {
        o = o.Where(p => p.PaidOnDate == PaidOnDate);
    }
    return o.ToList();
});

app.MapDelete("/orders/{id}", (CornerStoreDbContext db, int id) =>
{
    var order = db.Order.FirstOrDefault(o => o.Id == id);
    if (order is null)
    {
        return Results.NotFound();
    }
    db.Order.Remove(order);
    db.SaveChanges();
    return Results.NoContent();
});

app.MapPost("/orders", (CornerStoreDbContext db, Order order) =>
{
    db.Order.Add(order);
    db.SaveChanges();
    return Results.Created($"/orders/{order.Id}", order);
});

app.Run();

//don't move or change this!
public partial class Program { }