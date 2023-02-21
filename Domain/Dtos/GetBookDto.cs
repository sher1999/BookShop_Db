using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Domain.Dtos;

public class GetBookDto
{
    public  int Id { get; set; }
    [Required,MaxLength(100)]
    public string Name { get; set; }
    [Required,MaxLength(100)]
    public string  Author { get; set; }
    public decimal Price { get; set; }
    public string Direction { get; set; }
    public int CategoryId { get; set; }
}