namespace InterviewTask.API.Feature.Products.CreateProduct
{
    public record CreateProductResponse
        (
            int ID,
           string Name,
           string Description,
           int Quntatity,
           decimal Price,
           int intLowStockThreshold
           //List<Domain.Entities.InventoryTransaction> transaction

        );
}
