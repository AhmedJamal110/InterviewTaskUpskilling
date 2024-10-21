using InterviewTask.API.Abstraction;
using InterviewTask.API.Domain.Entities;
using InterviewTask.API.Errors;
using InterviewTask.API.Repository;
using Mapster;
using MediatR;

namespace InterviewTask.API.Feature.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, int Quntatity, decimal Price, int LowStockThreshold
     ) : IRequest<Result<CreateProductResponse>>;


    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Result<CreateProductResponse>>
    {
        private readonly IGenericRepository<Product> _repository;

        public CreateProductHandler(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }
        public async Task<Result<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var isProductExsit = await _repository.IsEntityExsit(x => x.Name == request.Name);
            if (isProductExsit)
                return Result.Failure<CreateProductResponse>(ProductErrors.ProductDeplucated);

            var entity = request.Adapt<Product>();
            var result = await _repository.AddAsync(entity);

            return result is null
                ? Result.Failure<CreateProductResponse>(ProductErrors.ProductNotFound)
                : Result.Success(result.Adapt<CreateProductResponse>());

        }
    }
}
