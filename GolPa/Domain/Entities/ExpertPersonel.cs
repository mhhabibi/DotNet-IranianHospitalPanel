using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities.Entities
{
    public class ExpertPersonel
    {
        [Key]
        public int PersonExpertId { get; set; }
        public int ExpertId { get; set; }
        public int PersonId { get; set; }
        public string Title { get; set; }
    }
}
