
namespace Catalog.API.Exceptiions
{
    public class ProductNotFoundException:NotFoundException
    {
        public ProductNotFoundException(Guid id):base("Product",id)
        { }
    }
}
