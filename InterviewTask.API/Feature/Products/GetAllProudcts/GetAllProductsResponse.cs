namespace InterviewTask.API.Feature.Products.GetAllProudcts
{
    public record GetAllProductsResponse
    (
            int ID,
           string Name,
           string Description,
           int Quntatity,
           decimal Price,
           int intLowStockThreshold
     );
    
    
}
