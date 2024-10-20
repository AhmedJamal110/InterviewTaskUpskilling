using InterviewTask.API.Abstraction;
using InterviewTask.API.Domain.Entities;
using InterviewTask.API.Repository;
using MediatR;

namespace InterviewTask.API.Feature.Products.IncreaseProductQuntity
{
    public record IncreaseProductQuntityCommand() : IRequest<Result>;

    public class IncreaseProductQuntityHandler : IRequestHandler<IncreaseProductQuntityCommand, Result>
    {
        private readonly IGenericRepository<Product> _repository;

        public IncreaseProductQuntityHandler(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }
        public Task<Result> Handle(IncreaseProductQuntityCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
