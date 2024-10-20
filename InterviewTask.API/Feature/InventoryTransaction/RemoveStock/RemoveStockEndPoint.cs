using InterviewTask.API.Errors;
using InterviewTask.API.Feature.InventoryTransaction.AddStock;
using InterviewTask.API.Feature.InventoryTransaction.Shared;
using InterviewTask.API.Feature.Products.GetProudctById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InterviewTask.API.Feature.InventoryTransaction.RemoveStock
{

    [Route("api/inventory-transaction")]
    [ApiController]
    public class RemoveStockEndPoint : ControllerBase
    {
        private readonly ISender _sender;

        public RemoveStockEndPoint(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("remove-stock/{productId}")]
        public async Task<ActionResult> RemoveStock([FromQuery] int productId, [FromBody] RemoveStockRequest request)
        {
            var result = await _sender.Send( new RemoveStockOrchestratorCommand(productId , request.Quntity ,request.Id));

            return result.IsSuccess
                      ? Ok(result)
                      : BadRequest(result.Error);
        }
    }
}
