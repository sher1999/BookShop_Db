using Domain.Entities;

namespace Domain.Dtos;

public class GetBookByCategory
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Direction { get; set; }
    public List<GetBookDto> Books { get; set; }
}