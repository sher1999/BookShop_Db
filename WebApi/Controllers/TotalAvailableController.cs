using Domain.Constants;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Authorize(Roles = $"{Roles.Admin} , {Roles.Customer}")]
public class TotalAvailableController:ApiBaseController
{
   
    private readonly BookService _bookService;

    public TotalAvailableController(BookService bookService)
    {
        _bookService = bookService;
    }
    [HttpGet("GetByCategory")]
    public async Task<Response<List<GetBookByCategory>>> GetByCategory()
    {
        return await _bookService.GetBookByCategory();
    }
    [HttpGet("GetByName")]
    public async Task<Response<List<GetBookDto>>> GetByName(string name)
    {
        return await _bookService.GetBookByName(name);
    }

    [HttpGet("GetBookByCategoryTitle")]
    public async Task<Response<List<GetBookByCategory>>> GetBookByCategoryTitle(string title)
    {
        return await _bookService.GetBookByCategoryTitle(title);
    }


   
}