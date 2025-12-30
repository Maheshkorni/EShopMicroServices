
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Product Product):ICommand<Unit>;
    internal class UpdateProductHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand>
    {
        public async Task<Unit> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Product.Id, cancellationToken);
            if (product is null)
                throw new ProductNotFoundException($"No Product Found with Id : {command.Product.Id}");
            product.Name = command.Product.Name;
            product.Description = command.Product.Description;
            product.Category = command.Product.Category;
            product.Price = command.Product.Price;
            product.ImageFile = command.Product.ImageFile;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

           return new Unit();
        }
    }
}
