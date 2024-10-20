using InterviewTask.API.Abstraction;
using InterviewTask.API.Domain.Entities;
using InterviewTask.API.Errors;
using InterviewTask.API.Repository;
using Mapster;
using MediatR;

namespace InterviewTask.API.Feature.Products.GetProudctById
{
    public record GetProudctByIdQuery(int id) : IRequest<Result<GetProductByIdResponse>>;
    public class GetProudctByIdQueryHandler : IRequestHandler<GetProudctByIdQuery, Result<GetProductByIdResponse>>
    {
        private readonly IGenericRepository<Product> _repository;

        public GetProudctByIdQueryHandler(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }
        public async Task<Result<GetProductByIdResponse>> Handle(GetProudctByIdQuery request, CancellationToken cancellationToken)
        {
            var prouct = await _repository.GetByIdAsync(request.id);
            return prouct is null
                ?Result.Failure<GetProductByIdResponse>(ProductErrors.ProductNotFound)
                :Result.Success(prouct.Adapt<GetProductByIdResponse>());
        }
    }
}
