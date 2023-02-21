using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Net;
using Domain.Wrapper;

namespace Infrastructure.Services;


public class CategoryService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CategoryService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Response<List<GetCategoryDto>>> GetCategory()
    {
        try
        {
            var result = await _context.Categories.ToListAsync();
            var mapped = _mapper.Map<List<GetCategoryDto>>(result);
            return new Response<List<GetCategoryDto>>(mapped);
        }
        catch (Exception e)
        {
            return new Response<List<GetCategoryDto>>(HttpStatusCode.InternalServerError,
                new List<string>() { e.Message });
        }
        
      
       
    }
    
   
    
    public async Task<Response<AddCategoryDto>> AddCategory(AddCategoryDto model)
    {
        try
        {
            var existing =await _context.Categories.FirstOrDefaultAsync(x=>x.Title == model.Title);
            if (existing == null)
            {
                var mapped = _mapper.Map<Category>(model);
                await _context.Categories.AddAsync(mapped);
                await _context.SaveChangesAsync();
                model.Id=mapped.Id;
                return new Response<AddCategoryDto>(model);
           
            }
            return new Response<AddCategoryDto>(HttpStatusCode.BadRequest,
                new List<string>() { "A Title with such data already exists" });
        }
        catch (Exception e)
        {
            return  new Response<AddCategoryDto>(HttpStatusCode.InternalServerError,new List<string>(){e.Message});
        }
        
    }
    
    public async Task<Response<AddCategoryDto>> UpdateCategory(AddCategoryDto model)
    {

        try
        {
          
            var update =await _context.Categories.Where(x=>x.Title != model.Title ).AsNoTracking().FirstOrDefaultAsync();
            if (update !=null)
            {
                var mapped = _mapper.Map<Category>(model);
                _context.Categories.Update(mapped);
                await _context.SaveChangesAsync();
                model.Id=mapped.Id;
                return new Response<AddCategoryDto>(model);
               
               
            }
            else
            {
                return new Response<AddCategoryDto>(HttpStatusCode.BadRequest,new List<string>() { $"A Title with such data already not  exists" });  

            }

        }
        catch (Exception e)
        {
            return  new Response<AddCategoryDto>(HttpStatusCode.InternalServerError,new List<string>(){e.Message});

        }
 
    }
    
    public async Task<Response<GetCategoryDto>> DeleteCategory(int id)
    {
        try
        {  
            
            var entity=await _context.Categories.Where(x=>x.Id == id).FirstOrDefaultAsync();
            if (entity==null)
            {
                return new Response<GetCategoryDto>(HttpStatusCode.BadRequest,
                    new List<string>() { $"Id {id} vijud nadora" });
            }
            else
            {
                _context.Remove(entity);
                await  _context.SaveChangesAsync();
                return new Response<GetCategoryDto>(HttpStatusCode.OK,
                    new List<string>() { $"Id {id} Delete shud" });
            }
        }
        catch (Exception e)
        {
            return  new Response<GetCategoryDto>(HttpStatusCode.InternalServerError,new List<string>(){e.Message});

        }
     
    }

}


    



