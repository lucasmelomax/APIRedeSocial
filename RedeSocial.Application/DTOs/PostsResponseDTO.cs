
using System.ComponentModel.DataAnnotations;
using RedeSocial.Domain.Models;

namespace RedeSocial.Application.DTOs {
    public class PostsResponseDTO {
        public int PostsId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? TextContent { get; set; }
        public int UsersId { get; set; }
    }
}
