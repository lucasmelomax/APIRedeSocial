
namespace RedeSocial.Domain.Models
{
    public class Comments
    {

        public int CommentsId { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int PostsId { get; set; }
        public int UsersId { get; set; }


        public Users? Users { get; set; }
        public Posts? Posts { get; set; }
    }
}
