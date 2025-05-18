
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProducts
{
    public class GetProductEndpoint : ICarterModule
    {
        public record GetProductResponse(IEnumerable<Product> Products);
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products",
                async (ISender sender) =>
                {                   
                    var result = await sender.Send(new GetProductsQuery());
                    var response = result.Adapt<GetProductResponse>();
                    return Results.Ok(response);
                })
            .WithName("GetProducts")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get Products");
        }
    }
}
