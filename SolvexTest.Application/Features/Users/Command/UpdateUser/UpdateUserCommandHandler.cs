using AutoMapper;
using MediatR;
using SolvexTest.Application.Common.Results;
using SolvexTest.Application.Contracts;
using SolvexTest.Application.Contracts.Persistence;
using SolvexTest.Application.DTOs;
using SolvexTest.Application.DTOs.Auth;
using SolvexTest.Application.Features.Users.Command.UpdateUser;
using SolvexTest.Domain.Entities;
using SolvexTest.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolvexTest.Application.Features.Users.Command.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<UserDto>>
    {
        private readonly IGenericRepository<User> userRepository;
        private readonly IMapper mapper;
        private readonly IPasswordHasher passwordHasher;

        public UpdateUserCommandHandler(IGenericRepository<User> userRepository, IMapper mapper,IPasswordHasher passwordHasher)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.passwordHasher = passwordHasher;
        }

        public async Task<Result<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userRepository.GetAsync(x => x.Id == request.Id);
                var userEntity = user.FirstOrDefault();

                if (user == null)
                    throw new ApplicationException($"Usuario con el ID {request.Id} no existe");

                userEntity.Username = request.Username;
                userEntity.PasswordHash = passwordHasher.HashPassword(request.Password);
               userEntity.UpdatedAt = DateTime.Now;

                await userRepository.UpdateAsync(userEntity);
                return Result<UserDto>.Success(mapper.Map<UserDto>(userEntity), "Usuario actualizado correctamente");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}