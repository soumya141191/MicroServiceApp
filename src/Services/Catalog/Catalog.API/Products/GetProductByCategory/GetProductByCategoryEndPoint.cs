﻿
using Catalog.API.Products.GetProductById;

namespace Catalog.API.Products.GetProductByCategory
{

    public class GetProductByCategoryEndPoint : ICarterModule
    {       
        public record GetProductByCategoryResponse(IEnumerable<Product> Products);
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}",
              async (string category, ISender sender) =>
              {
                  var result = await sender.Send(new GetProductByCategory(category));
                  var response = result.Adapt<GetProductByCategoryResponse>();
                  return Results.Ok(response);
              })
          .WithName("GetProductByCategory")
          .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithSummary("Get Product By CategoryId")
          .WithDescription("Get Product By CategoryId");
        }
    }
}
