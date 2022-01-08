using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DNP_example2.Models
{
    public class Book 
    {
        [JsonPropertyName("isbn")]
        [Key]
        public int ISBN { get; set; }
        [JsonPropertyName("title")]
        [Required, MaxLength(40)]
        public string Title { get; set; }
        [JsonPropertyName("publicationyear")]
        public int PublicationYear { get; set; }
        [JsonPropertyName("pages")]
        public int NumOPage { get; set; }
        [JsonPropertyName("genre")]
        public string Genre { get; set; }
    }
}
