using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities.Entities
{
    public class Illness : SBaseEntity
    {
        [Key]
        public int IllnessId { get; set; }
        public string Description { get; set; }
    }
}
