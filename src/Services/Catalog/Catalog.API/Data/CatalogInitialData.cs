using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync())
                return;

            session.Store<Product>(GetPreconfiguredProduct());
            await session.SaveChangesAsync();
         }

        private static IEnumerable<Product> GetPreconfiguredProduct() => new List<Product>() 
        {
            new Product
            {
                Id = new Guid("26dad760-f610-44da-9560-05a607f4f9ca"),
                Name = "New Product D",
                Category = ["c1","c3"],
                Description = "Description Product D",
                ImageFile = "ImageFile Product D",
                Price = 299
            }
        };
    }

    
    
}
