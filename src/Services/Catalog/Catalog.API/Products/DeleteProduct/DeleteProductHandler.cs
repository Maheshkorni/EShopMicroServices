
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid id):ICommand<Unit>;
    internal class DeleteProductHandler(IDocumentSession session) : ICommandHandler<DeleteProductCommand>
    {
        public async Task<Unit> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.id, cancellationToken);
            if (product != null)
            {
                session.Delete<Product>(command.id);
                await session.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
            throw new ProductNotFoundException($"No Product found with Id : {command.id}");
            
        }
    }
}
