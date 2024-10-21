using InterviewTask.API.Domain.Entities;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InterviewTask.API.Feature.Authentication
{
    public record GenerateTokenCommand(User user) : IRequest<string>;

    public class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommand, string>
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IConfiguration _configuration;

        public GenerateTokenCommandHandler(IJwtProvider jwtProvider , IConfiguration configuration )
        {
            _jwtProvider = jwtProvider;
            _configuration = configuration;
        }
        public async Task<string> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            Claim[] claim =
                [
                    //new(JwtRegisteredClaimNames.Sub, request.user.ID),
                    new(JwtRegisteredClaimNames.Email, request.user.Email!),
                    new(JwtRegisteredClaimNames.GivenName, request.user.FirstName),
                    new(JwtRegisteredClaimNames.FamilyName, request.user.LastName),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                ];

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));

            var singingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Token:ValidIssuer"],
                audience: _configuration["Token:ValidAudiance"],
                claims: claim,
                signingCredentials: singingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
