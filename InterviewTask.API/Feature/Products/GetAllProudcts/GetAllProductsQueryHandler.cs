using InterviewTask.API.Abstraction;
using InterviewTask.API.Domain.Entities;
using InterviewTask.API.Errors;
using InterviewTask.API.Repository;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InterviewTask.API.Feature.Products.GetAllProudcts
{
    public record GetAllProductsQuery : IRequest<Result<IEnumerable<GetAllProductsResponse>>>;

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<IEnumerable<GetAllProductsResponse>>>
    {
        private readonly IGenericRepository<Product> _repository;

        public GetAllProductsQueryHandler(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }
        public async Task<Result<IEnumerable<GetAllProductsResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var proudcts = await _repository.GetAllAsync().AsNoTracking().ToListAsync();

            return proudcts.Count == 0
            ? Result.Failure<IEnumerable<GetAllProductsResponse>>(ProductErrors.ProductNotFound)
            : Result.Success(proudcts.Adapt<IEnumerable<GetAllProductsResponse>>());
        }
    }
}
