namespace InterviewTask.API.Feature.Products.UpdateProduct
{
    public record UpdateProductRequest
    (
        string Name, 
        string Description, 
        int Quntatity, 
        decimal Price, 
        int intLowStockThreshold


    );
}
