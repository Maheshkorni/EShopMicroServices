
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.DeleteProduct
{

    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}",async (Guid id,ISender sender) =>
            {
                await sender.Send(new DeleteProductCommand(id));
                return Results.Ok($"Product with Id : {id} deleted successfully");
            })
            .WithName("DeleteProduct")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product");
        }
    }
}
