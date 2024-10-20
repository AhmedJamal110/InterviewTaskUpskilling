using InterviewTask.API.Abstraction;
using InterviewTask.API.Domain.Entities;
using InterviewTask.API.Errors;
using InterviewTask.API.Feature.InventoryTransaction.RemoveStock;
using InterviewTask.API.Feature.Products.GetProudctById;
using Mapster;
using MediatR;

namespace InterviewTask.API.Feature.InventoryTransaction.Shared
{
    public record RemoveStockOrchestratorCommand(int productId , int quntity ,int id) : IRequest<Result>;

    public class RemoveStockOrchestratorHanddler : IRequestHandler<RemoveStockOrchestratorCommand, Result>
    {
        private readonly IMediator _mediator;

        public RemoveStockOrchestratorHanddler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Result> Handle(RemoveStockOrchestratorCommand request, CancellationToken cancellationToken)
        {
            var productResult = await _mediator.Send(new GetProudctByIdQuery(request.productId));
                  if (productResult is null)
                     return Result.Failure(ProductErrors.ProductNotFound);

            var productInDb = productResult.Adapt<Product>();
             productInDb.Quntatity -= request.quntity;

        var result = await _mediator.Send(new RemoveStockCommand( request.id));



            return result.IsSuccess
                ? Result.Success()
                : Result.Failure(new Error("InventoryTransaction.Errors", "Error while Deleted Transaction"));
        }
    }
}
