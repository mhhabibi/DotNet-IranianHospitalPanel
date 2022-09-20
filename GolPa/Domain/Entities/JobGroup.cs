using Domain.Common;
using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities.Entities
{
    public class JobGroup:SBaseEntity
    {
        [Key]
        public int JobGroupId { get; set; }
    }
}
