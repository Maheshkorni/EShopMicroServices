
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid id):ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductCommandHandler : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandHandler() 
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Id is required");
        }
    }
    internal class DeleteProductHandler(IDocumentSession session) : ICommandHandler<DeleteProductCommand,DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.id, cancellationToken);
            if (product != null)
            {
                session.Delete<Product>(command.id);
                await session.SaveChangesAsync(cancellationToken);
                return new DeleteProductResult(true);
            }
            throw new ProductNotFoundException(command.id);
            
        }
    }
}
