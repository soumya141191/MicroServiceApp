
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProdcutCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        :ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);
    internal class UpdateProductCommandHandler(IDocumentSession session,ILogger<UpdateProductCommandHandler> logger) 
        : ICommandHandler<UpdateProdcutCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProdcutCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductCommandHandler.Handle");
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            
            if(product == null)
            {
                throw new NotImplementedException();
            }

            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;
            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);

        }
    }
} 
