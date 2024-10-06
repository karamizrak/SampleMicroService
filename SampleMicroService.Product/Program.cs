using SampleMicroService.Product.Data.Interfaces;
using SampleMicroService.Product.Entities;
using SampleMicroService.Product.Repositories;
using SampleMicroService.Product.Repositories.Interfaces;
using SampleMicroService.Product.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// 1. ProductDatabaseSettings bölümünü konfigürasyona ekleyin
builder.Services.Configure<ProductDatabaseSettings>(
    builder.Configuration.GetSection(nameof(ProductDatabaseSettings)));

// 2. IProductDatabaseSettings arayüzünü implement eden sýnýfý singleton olarak ekleyin
builder.Services.AddSingleton<IProductDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<ProductDatabaseSettings>>().Value);

// 3. MongoDB istemcisini (client) kaydetme
builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetSection("ProductDatabaseSettings:ConnectionString").Value));

// 4. Veritabanýný kaydetme
builder.Services.AddSingleton<IMongoDatabase>(s =>
    s.GetRequiredService<IMongoClient>().GetDatabase(
        s.GetRequiredService<IProductDatabaseSettings>().DatabaseName));

// 5. IMongoCollection<Product> kaydetme
builder.Services.AddSingleton<IMongoCollection<Product>>(s =>
    s.GetRequiredService<IMongoDatabase>().GetCollection<Product>(
        s.GetRequiredService<IProductDatabaseSettings>().CollectionName));

// 6. Diðer hizmetlerin kaydedilmesi
builder.Services.AddSingleton<IProductContext, ProductContext>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
