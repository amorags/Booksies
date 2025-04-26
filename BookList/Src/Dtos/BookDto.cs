using System.Text.Json.Serialization;
using BookList.Domain;


namespace BookList.Application.Dtos;


    public class BookCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
        public int PageCount { get; set; }
        public string Blurp { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
        public string CoverUrl {get; set;} = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]  // This ensures the enum is handled as a string
        public Category Category { get; set; }
       
    }
