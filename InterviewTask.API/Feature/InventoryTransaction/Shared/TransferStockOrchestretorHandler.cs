using InterviewTask.API.Abstraction;
using InterviewTask.API.Domain.Enums;
using InterviewTask.API.Errors;
using InterviewTask.API.Feature.InventoryTransaction.AddStock;
using InterviewTask.API.Feature.Products.GetProudctById;
using InterviewTask.API.Feature.Warehouse.GetWarehouseById;
using InterviewTask.API.Repository;
using MediatR;

namespace InterviewTask.API.Feature.InventoryTransaction.Shared
{
    public record TransferStockOrchestretorCommand(int ProductId, int SourceWarehouseId, int DestinationWarehouseId, int Quantity)
        : IRequest<Result>;
    public class TransferStockOrchestretorHandler : IRequestHandler<TransferStockOrchestretorCommand, Result>
    {
        private readonly IMediator _mediator;
        private readonly IGenericRepository<Domain.Entities.InventoryTransaction> _repository;

        public TransferStockOrchestretorHandler(IMediator mediator,
            IGenericRepository<Domain.Entities.InventoryTransaction> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }
        public async Task<Result> Handle(TransferStockOrchestretorCommand request, CancellationToken cancellationToken)
        {
            var productResponse = await _mediator.Send(new GetProudctByIdQuery(request.ProductId));

            if (productResponse is null)
                return Result.Failure(ProductErrors.ProductNotFound);

            var sourceId = await _mediator.Send(new GetWarehouseByIdQuery(request.SourceWarehouseId));
            var destenationId = await _mediator.Send(new GetWarehouseByIdQuery(request.DestinationWarehouseId));

            if (sourceId == null || destenationId == null)
                return Result.Failure(new Error("", ""));

            var sourceTransaction = new Domain.Entities.InventoryTransaction
            {
                ProductId = request.ProductId,
                SourceWarehouseId = request.SourceWarehouseId,
                Quantity = -request.Quantity,
                Type = TransactionType.TransferStock,
                Date = DateTime.UtcNow,
            };

            var destinationTransaction = new Domain.Entities.InventoryTransaction
            {
                ProductId = request.ProductId,
                DestinationWarehouseId = request.DestinationWarehouseId,
                Quantity = request.Quantity,
                Type = TransactionType.TransferStock,
                Date = DateTime.UtcNow,
            };

            await _repository.AddAsync(sourceTransaction);
            await _repository.AddAsync(destinationTransaction);

            return Result.Success();
        }
    }
}
