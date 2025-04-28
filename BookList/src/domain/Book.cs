namespace BookList.Domain;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; }
    public int PageCount {get; set;}
    public string Blurp { get; set; } = string.Empty;

    public string CoverUrl {get; set;} = string.Empty;

    public string ISBN {get; set;} = string.Empty;

    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;

    public Category Category {get; set;}

    public int PublisherId { get; set; }
    public Publisher Publisher { get; set; } = null!;
}
