using MongoDB.Driver;

namespace SampleMicroService.Product.Data.Interfaces;

public static class ProductContextSeed
{
    public static void Data(IMongoCollection<Entities.Product> productCollection)
    {
        var existProduct = productCollection.Find(p => true).Any();
        if (!existProduct) productCollection.InsertManyAsync(GetConfigureProduct());
    }

    private static IEnumerable<Entities.Product> GetConfigureProduct()
    {
        return new List<Entities.Product>
        {
            new()
            {
                Name = "Ürün 1", Summary = "Ürün 1 Summary", ImageFile = "Ürün 1 ImageFile", Price = 1000,
                Category = "Category 1"
            },
            new()
            {
                Name = "Ürün 2", Summary = "Ürün 2 Summary", ImageFile = "Ürün 2 ImageFile", Price = 2000,
                Category = "Category 1"
            },
            new()
            {
                Name = "Ürün 3", Summary = "Ürün 3 Summary", ImageFile = "Ürün 3 ImageFile", Price = 3000,
                Category = "Category 2"
            },
            new()
            {
                Name = "Ürün 4", Summary = "Ürün 4 Summary", ImageFile = "Ürün 4 ImageFile", Price = 4000,
                Category = "Category 2"
            },
            new()
            {
                Name = "Ürün 5", Summary = "Ürün 5 Summary", ImageFile = "Ürün 5 ImageFile", Price = 5000,
                Category = "Category 1"
            },
            new()
            {
                Name = "Ürün 6", Summary = "Ürün 6 Summary", ImageFile = "Ürün 6 ImageFile", Price = 6000,
                Category = "Category 2"
            }
        };
    }
}