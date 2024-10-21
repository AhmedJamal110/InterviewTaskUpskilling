using InterviewTask.API.Domain.Entities;
using InterviewTask.API.Domain.Enums;
using InterviewTask.API.Repository;
using MediatR;

namespace InterviewTask.API.Feature.Authentication.GetRoleFeature
{
    public record GetRoleFeatureQuery(int roleId , Features Features) : IRequest<bool>;
    public class GetRoleFeatureHandler : IRequestHandler<GetRoleFeatureQuery, bool>
    {
        private readonly IGenericRepository<RoleFeautre> _repository;

        public GetRoleFeatureHandler(IGenericRepository<RoleFeautre> repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(GetRoleFeatureQuery request, CancellationToken cancellationToken)
        {
            return  await  _repository.IsEntityExsit(x => !x.IsDeleted 
                                                                    && x.RoleId == request.roleId 
                                                                  && x.Features == request.Features);
        }
    }
}
