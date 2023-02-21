using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;


namespace Infrastructure.MapperProfiles;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<IdentityUser, UserDto>();
        CreateMap<IdentityRole, RoleDto>();
        
        CreateMap<GetBookDto, Book>().ReverseMap();
        CreateMap<AddBookDto, Book>().ReverseMap();
        
        CreateMap<GetCategoryDto, Category>().ReverseMap();
        CreateMap<AddBookDto, Category>().ReverseMap();
    }
}