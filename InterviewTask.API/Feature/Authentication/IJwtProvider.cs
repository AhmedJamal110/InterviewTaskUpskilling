using InterviewTask.API.Domain.Entities;

namespace InterviewTask.API.Feature.Authentication
{
    public interface IJwtProvider
    {
        string  GenerateToken(User user );

    }
}
