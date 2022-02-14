using Newtonsoft.Json;
using System.Collections.Generic;

namespace TheBookStore.DTO
{
    public class AuthorDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("books")]
        public IEnumerable<AuthorBooksDto> Books { get; set; }
    }
}