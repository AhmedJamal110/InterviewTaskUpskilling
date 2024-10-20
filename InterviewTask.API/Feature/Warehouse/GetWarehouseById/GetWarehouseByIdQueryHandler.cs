using InterviewTask.API.Abstraction;
using InterviewTask.API.Domain.Entities;
using InterviewTask.API.Repository;
using MediatR;

namespace InterviewTask.API.Feature.Warehouse.GetWarehouseById
{
    public record GetWarehouseByIdQuery(int id) : IRequest<Result>;

    public class GetWarehouseByIdQueryHandler : IRequestHandler<GetWarehouseByIdQuery, Result>
    {
        private readonly IGenericRepository<Domain.Entities.Warehouse> _repository;

        public GetWarehouseByIdQueryHandler(IGenericRepository<Domain.Entities.Warehouse> repository)
        {
            _repository = repository;
        }
        public async Task<Result> Handle(GetWarehouseByIdQuery request, CancellationToken cancellationToken)
        {
            var warehouse = await _repository.GetByIdAsync(request.id);

            return warehouse is null
                ? Result.Failure(new Error("Warehouse.Error", "No Warehouse was Found"))
                : Result.Success();
        }
    }
}
