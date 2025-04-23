using AutoMapper;
using MediatR;
using SolvexTest.Application.Common.Results;
using SolvexTest.Application.Contracts;
using SolvexTest.Application.Contracts.Persistence;
using SolvexTest.Application.DTOs.Auth;
using SolvexTest.Domain.Entities.Auth;

namespace SolvexTest.Application.Features.Auth.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<UserDto>>
    {
        private readonly IGenericRepository<User> userRepository;
        private readonly IPasswordHasher passwordHasher;
        private readonly IMapper mapper;

        public RegisterUserCommandHandler(IGenericRepository<User> userRepository,
            IPasswordHasher passwordHasher,
            IMapper mapper)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
            this.mapper = mapper;
        }
        public async Task<Result<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                {
                    throw new ApplicationException("El nombre de usuario y la contraseña son requeridos.");
                }

                var existingUser = await userRepository.GetWhereAsync(u => u.Username == request.Username);
                var existingUserResult = existingUser.FirstOrDefault();
                if (existingUserResult != null)
                {
                    throw new ApplicationException($"El nombre de usuario '{request.Username}' ya existe.");
                }

                var passwordHash = passwordHasher.HashPassword(request.Password);

                var newUser = new User
                {
                    Username = request.Username,
                    PasswordHash = passwordHash,
                    Role = request.Role ?? "User" 
                };

                await userRepository.AddAsync(newUser);

                return Result<UserDto>.Success(mapper.Map<UserDto>(newUser), "Usuario registrado correctamente");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
