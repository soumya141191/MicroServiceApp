
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
            if (product == null)
            {
                logger.LogWarning("Product with id {Id} not found", request.Id);
                return new DeleteProductResult(false);
            }
            session.Delete<Product>(request.Id);
            await session.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Product with id {Id} deleted", request.Id);
            return new DeleteProductResult(true);
        }
    }
}
