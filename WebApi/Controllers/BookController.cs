using System.Net;
using Domain.Constants;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers;
[Authorize(Roles = Roles.Admin)]
public class BookController:ApiBaseController
{
    private readonly BookService _bookService;

    public BookController(BookService bookService)
    {
        _bookService = bookService;
    }


    [HttpGet("Get")]
    public async Task<Response<List<GetBookDto>>> Gett()
    {
        return await _bookService.GetBook();
    }

    [HttpPost("Add")]
    public async Task<Response<AddBookDto>> Addd(AddBookDto c)
    {
        if (ModelState.IsValid)
        {
            return await _bookService.AddBook(c);
        }
        else
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<AddBookDto>(HttpStatusCode.BadRequest, errors);
        }
    }

    [HttpPut("Update")]
    public async Task<Response<AddBookDto>> Updatee( AddBookDto c) 
    {
        if (ModelState.IsValid)
        {
            return await _bookService.UpdateBook(c);
        }
        else
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<AddBookDto>(HttpStatusCode.BadRequest, errors);
        }
    }
    [HttpDelete("{id}")]
    public async Task<Response<GetBookDto>>  Delete(int id)
    {
        return await _bookService.DeleteBook(id);
    
    }
}