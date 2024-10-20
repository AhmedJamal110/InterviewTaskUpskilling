using InterviewTask.API.Persistence;

namespace InterviewTask.API.Middelwares
{
    public class TransactionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TransactionMiddleware> _logger;

        public TransactionMiddleware(RequestDelegate next , ILogger<TransactionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext, ApplicationDbContext context)
        {

            var method = httpContext.Request.Method.ToUpper();
            if (method == "POST" || method == "PUT" || method == "DELETE")
            {
                var transaction = context.Database.BeginTransaction();

                try
                {
                    await _next(httpContext);
                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    _logger.LogInformation("Transaction committed successfully for {Method} request.", method);

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Transaction rolled back due to an error for {Method} request.", method);
                        throw;
                }
            }
            else
            {
                await _next(httpContext);
            }
        }


    }
}
