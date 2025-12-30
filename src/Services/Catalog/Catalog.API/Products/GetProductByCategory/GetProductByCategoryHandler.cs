
namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string category):IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductByCategoryHandler(ILogger<GetProductByCategoryHandler> logger, IDocumentSession session) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            var result = session.Query<Product>().Where(x => x.Category.Contains(query.category)).ToList();
            if (result.Count == 0)
            {
                throw new ProductNotFoundException($"No product found with Category {query.category}");
            }
            return new GetProductByCategoryResult(result);
        }
    }
}
