
using System.Text.Json.Serialization;

namespace RedeSocial.Application.DTOs.Comments
{
    public class CreateCommentsDTO
    {
        public string? Comment { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int PostsId { get; set; }
    }
}
