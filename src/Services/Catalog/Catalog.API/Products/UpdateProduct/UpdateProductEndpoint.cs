
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductRequest(Product Product);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/updateproducts",async (UpdateProductRequest productRequest,ISender sender) => 
            {
                await sender.Send(new UpdateProductCommand(productRequest.Product));

                return Results.Ok("Product updated Successfully");
            })
            .WithName("UpdateProduct")
            .Produces<string>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Product")
            .WithDescription("Update Product");
        }
    }
}
