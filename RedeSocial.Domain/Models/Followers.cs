using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Domain.Models {
    public class Followers {
        public int FollowersId { get; set; }
        public int UsersId { get; set; }


        public Users? Users { get; set; }

    }
}
