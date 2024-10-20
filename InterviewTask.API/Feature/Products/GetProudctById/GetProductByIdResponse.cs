namespace InterviewTask.API.Feature.Products.GetProudctById
{
    public record GetProductByIdResponse
    (
              int ID,
           string Name,
           string Description,
           int Quntatity,
           decimal Price,
           int intLowStockThreshold
    );
}
