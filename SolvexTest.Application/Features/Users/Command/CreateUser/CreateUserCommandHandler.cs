using AutoMapper;
using MediatR;
using SolvexTest.Application.Common.Results;
using SolvexTest.Application.Contracts;
using SolvexTest.Application.Contracts.Persistence;
using SolvexTest.Application.DTOs;
using SolvexTest.Application.DTOs.Auth;
using SolvexTest.Application.Features.Products.Command.CreateProduct;
using SolvexTest.Domain.Entities;
using SolvexTest.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolvexTest.Application.Features.Users.Command.CreateUser
{
    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserDto>>
    {
        private readonly IGenericRepository<User> userRepository;
        private readonly IMapper mapper;
        private readonly IPasswordHasher passwordHasher;

        public CreateUserCommandHandler(IGenericRepository<User> userRepository, IMapper mapper,IPasswordHasher passwordHasher)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.passwordHasher = passwordHasher;
        }

        public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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
