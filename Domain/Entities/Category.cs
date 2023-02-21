using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Category
{ 
    public int Id { get; set; }
    [Required,MaxLength(100)]
    public string Title { get; set; }
    public string Direction { get; set; }
    public List<Book> Books { get; set; }
}