namespace BookList.Domain;

public class Author
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}