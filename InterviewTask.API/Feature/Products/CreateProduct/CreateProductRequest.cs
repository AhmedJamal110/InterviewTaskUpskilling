namespace InterviewTask.API.Feature.Products.CreateProduct
{
    public record CreateProductRequest
        (
        string Name,
        string Description,
        int Quntatity,
        decimal Price,
        int intLowStockThreshold
        );
}
