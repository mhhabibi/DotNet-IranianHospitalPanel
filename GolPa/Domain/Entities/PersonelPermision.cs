using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Entities
{
    public class PersonelPermision
    {
        public int Id { get; set; }
        public int Permission { get; set; }
        public int PersonId { get; set; }
        public string Title{ get; set; }
    }
}
