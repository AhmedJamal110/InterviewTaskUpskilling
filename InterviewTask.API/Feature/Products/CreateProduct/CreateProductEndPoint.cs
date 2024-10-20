using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InterviewTask.API.Feature.Products.CreateProduct
{
    [Route("api/products")]
    [ApiController]
    public class CreateProductEndPoint : ControllerBase
    {
        private readonly ISender _sender;

        public CreateProductEndPoint(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("createProduct")]
        public async Task<ActionResult> CreateNewProduct([FromBody] CreateProductRequest request)
        {
            var result = await _sender.Send(new CreateProductCommand(
                           request.Name,
                           request.Description,
                           request.Quntatity,
                           request.Price,
                           request.intLowStockThreshold));

            return result.IsSuccess
                      ? Ok(result.Value)
                      : BadRequest(result.Error);
        }

    }
}
