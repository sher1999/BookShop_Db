using Domain.Constants;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Authorize(Roles = $"{Roles.Admin} , {Roles.Customer}")]
public class ProfileController:ApiBaseController
{
    private readonly IAccountService _accountService;

    public ProfileController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet("ViewProfile")]
    public async Task<Response<List<UserDto>>> Profile(string email,string password)
    {
        return await _accountService.ViewProfile(email,password);
    }
}