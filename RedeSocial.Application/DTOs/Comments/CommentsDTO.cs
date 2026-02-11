using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using RedeSocial.Domain.Models;

namespace RedeSocial.Application.DTOs.Comments
{
    public class CommentsDTO
    {
        public int CommentsId { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PostsId { get; set; }

        public int UsersId { get; set; }

    }
}
