using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InterviewTask.API.Feature.Products.GetProudctById
{

    [Route("api/products")]
    [ApiController]
    public class GetProudctByIdEndPoint : ControllerBase
    {

        private readonly ISender _sender;

        public GetProudctByIdEndPoint(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("get-product/{id}")]
        public async Task<ActionResult> GetProductByID([FromRoute] int id)
        {
            var result = await _sender.Send(new GetProudctByIdQuery(id));


            return result.IsSuccess
                      ? Ok(result.Value)
                      : BadRequest(result.Error);
        }

    }
}
