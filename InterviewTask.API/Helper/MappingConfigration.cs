using InterviewTask.API.Domain.Entities;
using InterviewTask.API.Feature.Products.CreateProduct;
using Mapster;

namespace InterviewTask.API.Helper
{
    public class MappingConfigration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<CreateProductCommand, Product>.NewConfig()
                                 .Ignore(dest => dest.Transactions);

        }
    }
}
