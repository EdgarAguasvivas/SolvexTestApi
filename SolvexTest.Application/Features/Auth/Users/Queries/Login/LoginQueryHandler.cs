using MediatR;
using SolvexTest.Application.Contracts;
using SolvexTest.Application.Contracts.Persistence;
using SolvexTest.Application.DTOs;
using SolvexTest.Application.DTOs.Auth;
using SolvexTest.Domain.Entities.Auth;

namespace SolvexTest.Application.Features.Auth.Users.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IGenericRepository<User> userRepository;
        private readonly IPasswordHasher passwordHasher;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IGenericRepository<User> userRepository, IPasswordHasher passwordHasher)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
        }
        public async Task<AuthenticationResult> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetWhereAsync(u => u.Username == request.Username);
            var userResult = user.FirstOrDefault();

            if (userResult == null)
                return new AuthenticationResult { IsAuthenticated = false, Error = "Usuario no existe" };

            if (user == null || !passwordHasher.VerifyPassword(request.Password, userResult.PasswordHash))
                return new AuthenticationResult { IsAuthenticated = false, Error = "Credenciales inválidas" };

            var token = _jwtTokenGenerator.GenerateToken("1", request.Username, userResult.Role);
            var userDto = new UserDto
            {
                Id = userResult.Id,
                Username = userResult.Username,
                Role = userResult.Role,
                Token = token
            };
            return new AuthenticationResult { IsAuthenticated = true, Token = token, User = userDto };
        }
    }
}
