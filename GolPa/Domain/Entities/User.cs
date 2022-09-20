using System.Collections.Generic;

namespace Domain.Entities.Entities
{
    public class User
    {
        public int PersonId { get; set; }

        public string Password { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
        public List<int> Permissions { get; set; }
    }
}
