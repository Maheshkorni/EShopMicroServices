namespace Catalog.API.Exceptiions
{
    public class ProductNotFoundException:Exception
    {
        public ProductNotFoundException(string message):base(message)
        { }
    }
}
