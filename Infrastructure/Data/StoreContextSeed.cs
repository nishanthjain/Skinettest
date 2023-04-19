

using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if(!context.ProductBrands.Any())
            {
                var brandsdata=File.ReadAllText("../Infrastructure/Data/SeedData/Brands.json");
                var brands=JsonSerializer.Deserialize<List<ProductBrand>>(brandsdata);
                context.ProductBrands.AddRange(brands);
            }
            if(!context.ProductTypes.Any())
            {
                var typesdata=File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                var types=JsonSerializer.Deserialize<List<ProductType>>(typesdata);
                context.ProductTypes.AddRange(types);
            }
            if(!context.Products.Any())
            {
                var productdata=File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                var product=JsonSerializer.Deserialize<List<Product>>(productdata);
                context.Products.AddRange(product);
            }
            if(context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
    }
}