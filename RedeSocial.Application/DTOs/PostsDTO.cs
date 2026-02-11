
using System.ComponentModel.DataAnnotations;
using RedeSocial.Domain.Models;

namespace RedeSocial.Application.DTOs
{
    public class PostsDTO
    {
        [Required]
        public int PostsId { get; set; }

        [Required]
        [StringLength(150)]
        public string? TextContent { get; set; }
        public int UsersId { get; set; }
    }
}
