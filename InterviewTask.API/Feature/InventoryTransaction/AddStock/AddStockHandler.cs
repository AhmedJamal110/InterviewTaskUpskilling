using InterviewTask.API.Abstraction;
using InterviewTask.API.Domain.Entities;
using InterviewTask.API.Domain.Enums;
using InterviewTask.API.Errors;
using InterviewTask.API.Feature.Products.GetProudctById;
using InterviewTask.API.Repository;
using Mapster;
using MediatR;

namespace InterviewTask.API.Feature.InventoryTransaction.AddStock
{
    public record AddStockCommand( int productId ,int quntity)
        : IRequest<Result<AddStockResponse>>;
    public class AddStockHandler : IRequestHandler<AddStockCommand,  Result<AddStockResponse>>
    {
        private readonly IGenericRepository<Domain.Entities.InventoryTransaction> _repository;

        public AddStockHandler(IGenericRepository<Domain.Entities.InventoryTransaction> repository)
        {
            _repository = repository;
        }
        public async Task<Result<AddStockResponse>> Handle(AddStockCommand request , CancellationToken cancellationToken)
        {
            var transaction = new Domain.Entities.InventoryTransaction()
            {
                ProductId = request.productId,
                Quantity = request.quntity,
                Type = TransactionType.AddStock,
                Date = DateTime.UtcNow,
            };

            var result = await _repository.AddAsync(transaction);

            return result is null
                    ? Result.Failure<AddStockResponse>(InventoryTransactionErrors.InventoryTransactionNotFound)
                    : Result.Success(result.Adapt<AddStockResponse>());


        }
    }
}
