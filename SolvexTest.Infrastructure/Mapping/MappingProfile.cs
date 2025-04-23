using AutoMapper;
using SolvexTest.Application.DTOs.Auth;
using SolvexTest.Application.DTOs;
using SolvexTest.Application.Features.Products.Command.CreateProduct;
using SolvexTest.Domain.Entities.Auth;
using SolvexTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolvexTest.Application.Features.Products.Command.UpdateProduct;
using SolvexTest.Application.Features.ProductVariations.Command.CreateProductVariation;
using SolvexTest.Application.Features.Auth.Users.Commands.RegisterUser;
using SolvexTest.Application.Features.Users.Command.UpdateUser;
using SolvexTest.Application.Features.Users.Command.DeleteUser;
using SolvexTest.Application.Features.Products.Command.DeleteProduct;

namespace SolvexTest.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(
                    dest => dest.Variations,
                    opt => opt.MapFrom(src => src.ProductVariations)
                );
            CreateMap<ProductDto, Product>();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();
            CreateMap<DeleteProductCommand, Product>();
            CreateMap<ProductVariation, ProductVariationDto>();
            CreateMap<ProductVariationDto, ProductVariation>();
            CreateMap<CreateProductVariationCommand, ProductVariation>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<RegisterUserCommand, User>();
            CreateMap<UpdateUserCommand, User>();
            CreateMap<DeleteUserCommand, User>();
        }
    }
}
