
namespace RedeSocial.Domain.Models {
    public class Followers {
        public int FollowersId { get; set; }
        public int UsersId { get; set; }


        public Users? Users { get; set; }

    }
}
