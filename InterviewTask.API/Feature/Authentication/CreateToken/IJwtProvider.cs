using InterviewTask.API.Domain.Entities;

namespace InterviewTask.API.Feature.Authentication.CreateToken
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);

    }
}
