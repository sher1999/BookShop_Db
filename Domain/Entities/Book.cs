using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Book
{
    public  int Id { get; set; }
    [Required,MaxLength(100)]
    public string Name { get; set; }
    [Required,MaxLength(100)]
    public string  Author { get; set; }
    public decimal Price { get; set; }
    public string Direction { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    
    
    
}