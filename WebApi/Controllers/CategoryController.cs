using System.Net;
using Domain.Constants;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;
[Authorize(Roles = Roles.Admin)]
public class CategoryController:ApiBaseController
{
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }


    [HttpGet("Get")]
    public async Task<Response<List<GetCategoryDto>>> Get()
    {
        return await _categoryService.GetCategory();
    }

    [HttpPost("Add")]
    public async Task<Response<AddCategoryDto>> Add(AddCategoryDto c)
    {
        if (ModelState.IsValid)
        {
            return await _categoryService.AddCategory(c);
        }
        else
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<AddCategoryDto>(HttpStatusCode.BadRequest, errors);
        }
    }

    [HttpPut("Update")]
    public async Task<Response<AddCategoryDto>> Updatee( AddCategoryDto c) 
    {
        if (ModelState.IsValid)
        {
            return await _categoryService.UpdateCategory(c);
        }
        else
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<AddCategoryDto>(HttpStatusCode.BadRequest, errors);
        }
    }
    [HttpDelete("{id}")]
    public async Task<Response<GetCategoryDto>>  Delete(int id)
    {
        return await _categoryService.DeleteCategory(id);
    
    }
}