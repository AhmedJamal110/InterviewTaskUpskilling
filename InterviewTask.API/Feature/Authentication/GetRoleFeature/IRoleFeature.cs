using InterviewTask.API.Domain.Enums;

namespace InterviewTask.API.Feature.Authentication.GetRoleFeature
{
    public interface IRoleFeature
    {
        bool HasAccess(int roleId, Features features);
    }
}
