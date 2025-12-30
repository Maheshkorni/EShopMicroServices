

using Catalog.API.Products.GetProductById;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryResponse(List<Product> Products);
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) => {

                var result = await sender.Send(new GetProductByCategoryQuery(category));
               
                return Results.Ok(result.Adapt<GetProductByCategoryResponse>());
            })
            .WithName("GetProductByCategory")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDescription("Get Product By Category")
            .WithSummary("Get Product By Categoy");




        }

    }
}
