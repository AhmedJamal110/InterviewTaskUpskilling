using InterviewTask.API.Abstraction;
using InterviewTask.API.Domain.Entities;
using InterviewTask.API.Errors;
using InterviewTask.API.Repository;
using Mapster;
using MediatR;

namespace InterviewTask.API.Feature.Products.UpdateProduct
{
    public record UpdateProudctCommand(int id , string Name, string Description, int Quntatity, decimal Price, int intLowStockThreshold)
        : IRequest<Result<UpdateProductResponse>>
    {
    };

    public class UpdateProductHandler : IRequestHandler<UpdateProudctCommand, Result>
    {
        private readonly IGenericRepository<Product> _repository;

        public UpdateProductHandler(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }
        public async Task<Result> Handle(UpdateProudctCommand request, CancellationToken cancellationToken)
        {
            var productInDb = _repository.GetByIdAsync(request.id);
            if (productInDb is null)
                return Result.Failure(ProductErrors.ProductNotFound);

            var isProductExist = await _repository.IsEntityExsit(x => x.ID != request.id && x.Name == request.Name);
                if (isProductExist)
                    return Result.Failure<UpdateProductResponse>(ProductErrors.ProductDeplucated);

            var entity = request.Adapt<Product>();
             _repository.UpdateInclude(entity, nameof(entity.Name), nameof(entity.Description), nameof(entity.Quntatity));

            return Result.Success();
        }
    }

}
