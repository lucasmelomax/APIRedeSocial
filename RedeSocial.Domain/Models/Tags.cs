
namespace RedeSocial.Domain.Models {
    public class Tags {

        public int TagsId { get; set; }
        public int UsersId { get; set; }
        public int PostsId { get; set; }
        public Users? Users { get; set; }
        public Posts? Posts { get; set; }
    }
}
