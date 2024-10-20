using InterviewTask.API.Abstraction;
using InterviewTask.API.Domain.Entities;
using InterviewTask.API.Errors;
using InterviewTask.API.Feature.InventoryTransaction.AddStock;
using InterviewTask.API.Feature.Products.GetProudctById;
using Mapster;
using MediatR;

namespace InterviewTask.API.Feature.InventoryTransaction.Shared
{
    public record AddStockOrchestretorCommand(int productId, int quntity) : IRequest<Result<AddStockResponse>>;

    public class AddStockOrchestretorHandler : IRequestHandler<AddStockOrchestretorCommand, Result<AddStockResponse>>
    {
        private readonly IMediator _mediator;

        public AddStockOrchestretorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Result<AddStockResponse>> Handle(AddStockOrchestretorCommand request, CancellationToken cancellationToken)
        {
            var productResponse = await _mediator.Send(new GetProudctByIdQuery(request.productId));

            if (productResponse is null)
                return Result.Failure<AddStockResponse>(ProductErrors.ProductNotFound);

            var product = productResponse.Adapt<Product>();

            product.Quntatity += request.quntity;


            var result = await _mediator.Send(new AddStockCommand(request.productId, request.quntity));

            return result.IsSuccess
                ? Result.Success(result.Value)
                : Result.Failure<AddStockResponse>(InventoryTransactionErrors.InventoryTransactionNotFound);



        }
    }
}
