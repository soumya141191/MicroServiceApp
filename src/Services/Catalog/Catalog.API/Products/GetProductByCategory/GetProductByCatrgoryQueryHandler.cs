using Catalog.API.Products.GetProductById;
using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategory(string Category):IQuery<GetProductByCategoryResponse>;
    public record GetProductByCategoryResponse(IEnumerable<Product> Products);
    public class GetProductByCatrgoryQueryHandler(IDocumentSession session,ILogger<GetProductByCatrgoryQueryHandler> logger)
        : IQueryHandler<GetProductByCategory, GetProductByCategoryResponse>
    {
        public async Task<GetProductByCategoryResponse> Handle(GetProductByCategory query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCatrgoryQueryHandler.Handle");
            var products = await session.Query<Product>()
                .Where(x => x.Category.Contains(query.Category))
                .ToListAsync();

            return new GetProductByCategoryResponse(products);

        }
    }
}
