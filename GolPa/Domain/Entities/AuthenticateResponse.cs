using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Entities
{
    public class AuthenticateResponse
    {
        public int PersonId { get; set; }
        public string Password{ get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }

        public Dictionary<int,string> MyProperty { get; set; }
    }

}
