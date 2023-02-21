using Domain.Constants;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Authorize(Roles = $"{Roles.Admin}")]
public class DeleteRoleController:ApiBaseController
{
   
    private readonly IAccountService _accountService;

    public DeleteRoleController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("DeleteRole")]
    public async  Task<Response<string>> Delete(DeleteRoleDto model)
    {
      return await _accountService.DeleteRole(model);
    }


}