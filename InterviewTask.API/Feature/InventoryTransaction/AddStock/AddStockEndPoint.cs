using InterviewTask.API.Abstraction;
using InterviewTask.API.Errors;
using InterviewTask.API.Feature.InventoryTransaction.Shared;
using InterviewTask.API.Feature.Products.CreateProduct;
using InterviewTask.API.Feature.Products.GetProudctById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InterviewTask.API.Feature.InventoryTransaction.AddStock
{
    [Route("api/inventory-transaction")]
    [ApiController]
    [Authorize]
    public class AddStockEndPoint : ControllerBase
    { 
        private readonly ISender _sender;

        public AddStockEndPoint(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("add-stock/{productId}")]
        public async Task<ActionResult> AddNewStock([FromQuery] int productId ,  [FromBody] AddStockRequest request)
        {

            var result = await _sender.Send(new AddStockOrchestretorCommand(
                      productId,
                    request.Quntity  
                    )); 

            return result.IsSuccess
                      ? Ok(result.Value)
                      : BadRequest(result.Error);
        }
    }
}
