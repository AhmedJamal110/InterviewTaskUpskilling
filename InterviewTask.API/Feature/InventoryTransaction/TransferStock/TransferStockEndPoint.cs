using InterviewTask.API.Feature.InventoryTransaction.AddStock;
using InterviewTask.API.Feature.InventoryTransaction.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InterviewTask.API.Feature.InventoryTransaction.TransferStock
{
    [Route("api/inventory-transaction")]
    [ApiController]
    public class TransferStockEndPoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransferStockEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("transfer-stock/{productId}")]
        public async Task<ActionResult> AddNewStock([FromQuery] int productId, [FromBody] TransferStockRequest request)
        {

            var result = await _mediator.Send(new TransferStockOrchestretorCommand(
                    productId,
                    request.SourceWarehouseId,
                    request.DestinationWarehouseId,
                    request.Quantity

                    ));

            return result.IsSuccess
                      ? Ok(result)
                      : BadRequest(result.Error);
        }

    }
}
