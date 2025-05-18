
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductRequest(Guid Id);
    public record DeleteProductResponse(bool IsSuccess);

    public class DeleteProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}",
                async (DeleteProductRequest request, ISender sender) =>
                {
                    var command = request.Adapt<DeleteProductRequest>();
                    var result = await sender.Send(command);
                    var response = result.Adapt<DeleteProductResponse>();
                    return Results.Ok(response);
                })
            .WithName("DeleteProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product");
        }
    }
}
