using InterviewTask.API.Feature.Products.CreateProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InterviewTask.API.Feature.Products.UpdateProduct
{
    [Route("api/products")]
    [ApiController]
    public class UpdateProductEndPoint : ControllerBase
    {
        private readonly ISender _sender;

        public UpdateProductEndPoint(ISender sender)
        {
            _sender = sender;
        }

        [HttpPut("update-product/{id}")]
        public async Task<ActionResult> UpdateProduct( [FromRoute] int id  ,  [FromBody] UpdateProductRequest request)
        {
            var result = await _sender.Send(new UpdateProudctCommand(
                               id ,
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
