using InterviewTask.API.Abstraction;

namespace InterviewTask.API.Errors
{
    public static class ProductErrors
    {
            public static readonly Error ProductNotFound
                = new("Product.NotFound", "Product not found");

            public static readonly Error ProductDeplucated
              = new("Product.ProductIsAlreadyExsit", "Product title is alredy Exist");


        
    }
}
