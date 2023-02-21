using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class GetCategoryDto
{
    public int Id { get; set; }
    [Required,MaxLength(100)]
    public string Title { get; set; }
    public string Direction { get; set; }
}