using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InterviewTask.API.Shared
{
    public class CanceletionTokenCaptureFilter : IActionFilter
    {
        private readonly CanceletionState _canceletionState;
        private readonly ILogger<CanceletionState> _logger;

        public CanceletionTokenCaptureFilter(CanceletionState canceletionState ,
            ILogger<CanceletionState> logger )
        {
            _canceletionState = canceletionState;
            _logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.HttpContext.Response.HasStarted) // Check if response has started
            {
                if (context.Result is ObjectResult objectResult)
                {
                    _logger.LogInformation($"Response status: {objectResult.StatusCode}");
                }
            }
            else
            {
                _logger.LogWarning("Response has already started; skipping logging.");
            }

        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var cancellationToken = context.HttpContext.RequestAborted;

            _canceletionState.SetCancellationToken(cancellationToken);
            
        }
    }
}
