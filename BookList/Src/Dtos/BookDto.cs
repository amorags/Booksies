using BookList.Domain;

namespace BookList.Application.Dtos;

public class CreateBookDto
{
    public string Title {get; set;}
    public int Year {get; set;}
   
}