using Marten.Internal;
using Marten.Linq.QueryHandlers;
using System.Data.Common;
using Weasel.Postgresql;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery():IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);
    internal class GetProductsHandler(ILogger<GetProductsHandler> logger, IDocumentSession session) : IQueryHandler<GetProductsQuery,GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsQueryHandler.Handle called with (@Query)", query);
            var result = await session.Query<Product>().ToListAsync(cancellationToken);
            return new GetProductsResult(result);
        }
    }
}
