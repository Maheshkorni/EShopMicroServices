namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid id):IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdHandler(ILogger<GetProductByIdHandler> logger, IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByIdHandler.handle called with query: (@query)",query);
            var result = await session.LoadAsync<Product>(query.id,cancellationToken);
            if (result is  null)
            {
                throw new ProductNotFoundException($"No prroduct found with Id : {query.id}"); 
            }

            return new GetProductByIdResult(result);
        }
    }
}
