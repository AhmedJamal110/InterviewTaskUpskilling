using InterviewTask.API.Feature.Products.CreateProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InterviewTask.API.Feature.Products.GetAllProudcts
{
    [Route("api/products")]
    [ApiController]
    public class GetAllProductsEndPoint : ControllerBase
    {
        private readonly ISender _sender;

        public GetAllProductsEndPoint(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("all-products")]
        public async Task<ActionResult> GetAllProducts()
        {
            var result = await _sender.Send(new GetAllProductsQuery());
                       

            return result.IsSuccess
                      ? Ok(result.Value)
                      : BadRequest(result.Error);
        }

    }
}
