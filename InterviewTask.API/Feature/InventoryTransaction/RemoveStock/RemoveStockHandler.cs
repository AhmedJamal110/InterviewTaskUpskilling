using InterviewTask.API.Abstraction;
using InterviewTask.API.Domain.Entities;
using InterviewTask.API.Repository;
using MediatR;

namespace InterviewTask.API.Feature.InventoryTransaction.RemoveStock
{

    public record RemoveStockCommand(int id) : IRequest<Result>;
    public class RemoveStockHandler : IRequestHandler<RemoveStockCommand, Result>
    {
        private readonly IGenericRepository<Domain.Entities.InventoryTransaction> _repository;

        public RemoveStockHandler(IGenericRepository<Domain.Entities.InventoryTransaction> repository)
        {
            _repository = repository;
        }
        public async Task<Result> Handle(RemoveStockCommand request, CancellationToken cancellationToken)
        {
             await _repository.SoftDelte(request.id);

            var transaction = new Domain.Entities.InventoryTransaction()
            {
                Type = Domain.Enums.TransactionType.RemoveStock
            };
            _repository.UpdateInclude(transaction, nameof(Type));
           
            return Result.Success();

        }
    }
}
