using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DNP_example2.Models
{
    public class Author
    {
        [JsonPropertyName("id")]
        [Key, Range(0, int.MaxValue)]
        public int Id { get; set; }
        [JsonPropertyName("firstname")]
        [Required, MaxLength(15)]
        public string FirstName { get; set; }
        [JsonPropertyName("lastname")]
        [Required, MaxLength (15)]
        public string LastName { get; set; }
        [JsonPropertyName("books")]
        public List<Book> Books { get; set; }


        public Author()
        {
            Books = new List<Book>();
        }
    }
}
