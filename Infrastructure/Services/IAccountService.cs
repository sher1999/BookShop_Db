using Domain.Dtos;
using Domain.Wrapper;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public interface IAccountService
{
    Task<Response<IdentityResult>> Register(RegisterDto registerDto);
    Task<Response<TokenDto>> Login(LoginDto model);
    Task<Response<List<UserDto>>> GetUsers();
    Task<Response<List<UserDto>>> ViewProfile(string email,string password);
    Task<Response<List<RoleDto>>> GetRoles();
    Task<Response<string>> DeleteRole(DeleteRoleDto model);
    Task<Response<AssignRoleDto>> AssignUserRole(AssignRoleDto model);
}