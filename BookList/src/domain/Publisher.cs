namespace BookList.Domain;

public class Publisher
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public string Location {get; set;} = string.Empty;

    public ICollection<Book> Books { get; set; } = new List<Book>();
}
