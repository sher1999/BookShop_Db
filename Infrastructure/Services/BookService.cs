using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Net;
using Domain.Wrapper;

namespace Infrastructure.Services;


public class BookService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public BookService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Response<List<GetBookDto>>> GetBook()
    {
        try
        {
            var result = await _context.Books.ToListAsync();
            var mapped = _mapper.Map<List<GetBookDto>>(result);
            return new Response<List<GetBookDto>>(mapped);
        }
        catch (Exception e)
        {
            return new Response<List<GetBookDto>>(HttpStatusCode.InternalServerError,
                new List<string>() { e.Message });
        }
        
      
       
    }
    public async Task<Response<List<GetBookDto>>> GetBookByName(string name)
    {
        try
        {
            var existing = await _context.Books.Where(x => x.Name == name).FirstOrDefaultAsync();

            if (existing!=null)
            {
                var book = await (from b in _context.Books
                    where b.Name == name
                    select new GetBookDto()
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Author = b.Author,
                        Price = b.Price,
                        Direction = b.Direction,
                        CategoryId = b.CategoryId
                    }).ToListAsync();
                return new Response<List<GetBookDto>>(book);
            }
            return new Response<List<GetBookDto>>(HttpStatusCode.BadRequest,
                new List<string>() { "Book with such data does not exist" });
        }
        catch (Exception e)
        {
            return new Response<List<GetBookDto>>(HttpStatusCode.InternalServerError,
                new List<string>() { e.Message });
        }
        
      
       
    }
    
    public async Task<Response<List<GetBookByCategory>>> GetBookByCategoryTitle(string title)
    {
        try
        {
            var existing = await _context.Categories.Where(x => x.Title == title).FirstOrDefaultAsync();

            if (existing!=null)
            {
                var bookByCategories = await (from c in _context.Categories
                    where c.Title == title
                    select new GetBookByCategory()
                    {
                        Id = c.Id,
                        Title = c.Title,
                        Direction = c.Direction,
                        Books = (from b in _context.Books 
                            where c.Id==b.CategoryId
                            select new GetBookDto()
                            {
                                Id = b.Id,
                                Name = b.Name,
                                Author = b.Author,
                                Price = b.Price,
                                Direction = b.Direction,
                                CategoryId = b.CategoryId
                                        
                            } ).ToList()
                    }).ToListAsync();
                return new Response<List<GetBookByCategory>>(bookByCategories);
            }
            return new Response<List<GetBookByCategory>>(HttpStatusCode.BadRequest,
                new List<string>() { "Book with such data does not exist" });
        }
        catch (Exception e)
        {
            return new Response<List<GetBookByCategory>>(HttpStatusCode.InternalServerError,
                new List<string>() { e.Message });
        }
        
      
       
    }


    public async Task<Response<List<GetBookByCategory>>> GetBookByCategory()
    {
        try
        {
            var books = await (from c in _context.Categories
                group c by new {c.Id,c.Title,c.Direction}
                into g
                select new GetBookByCategory()
                {   
                    Id = g.Key.Id,
                    Title = g.Key.Title,
                    Direction = g.Key.Direction,
                    Books = (from b in _context.Books
                                where b.CategoryId==g.Key.Id
                        select new GetBookDto
                        {
                            Id = b.Id,
                           Name = b.Name,
                           Author = b.Author,
                           Price=b.Price,
                           Direction=b.Direction,
                           CategoryId = b.CategoryId
                        }).ToList()


                }).ToListAsync();
            return new Response<List<GetBookByCategory>>(books);

        }
        catch (Exception e)
        {
            return new Response<List<GetBookByCategory>>(HttpStatusCode.InternalServerError,
                new List<string>() { e.Message });
        }

    

}
    
    public async Task<Response<AddBookDto>> AddBook(AddBookDto model)
    {
        try
        {
            var existing =await _context.Books.FirstOrDefaultAsync(x=>x.Name == model.Name);
            if (existing == null)
            {
                var mapped = _mapper.Map<Book>(model);
                await _context.Books.AddAsync(mapped);
                await _context.SaveChangesAsync();
                model.Id=mapped.Id;
                return new Response<AddBookDto>(model);
           
            }
            return new Response<AddBookDto>(HttpStatusCode.BadRequest,
                new List<string>() { "A Book with such data already exists" });
        }
        catch (Exception e)
        {
            return  new Response<AddBookDto>(HttpStatusCode.InternalServerError,new List<string>(){e.Message});
        }
        
    }
    
    public async Task<Response<AddBookDto>> UpdateBook(AddBookDto model)
    {

        try
        {
          
            var update =await _context.Books.Where(x=>x.Name != model.Name ).AsNoTracking().FirstOrDefaultAsync();
            if (update !=null)
            {
                var mapped = _mapper.Map<Book>(model);
                _context.Books.Update(mapped);
                await _context.SaveChangesAsync();
                model.Id=mapped.Id;
                return new Response<AddBookDto>(model);
               
               
            }
            else
            {
                return new Response<AddBookDto>(HttpStatusCode.BadRequest,new List<string>() { $"Book Name vijud nadora" });  

            }

        }
        catch (Exception e)
        {
            return  new Response<AddBookDto>(HttpStatusCode.InternalServerError,new List<string>(){e.Message});

        }
 
    }
    
    public async Task<Response<GetBookDto>> DeleteBook(int id)
    {
        try
        {  
            
            var entity=await _context.Books.Where(x=>x.Id == id).FirstOrDefaultAsync();
            if (entity==null)
            {
                return new Response<GetBookDto>(HttpStatusCode.BadRequest,
                    new List<string>() { $"Id {id} vijud nadora" });
            }
            else
            {
                _context.Remove(entity);
                await  _context.SaveChangesAsync();
                return new Response<GetBookDto>(HttpStatusCode.OK,
                    new List<string>() { $"Id {id} Delete shud" });
            }
        }
        catch (Exception e)
        {
            return  new Response<GetBookDto>(HttpStatusCode.InternalServerError,new List<string>(){e.Message});

        }
     
    }

  
}


    

