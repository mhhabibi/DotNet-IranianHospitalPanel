using Domain.Common;
using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities.Entities
{
    public class Job:SBaseEntity
    {
        [Key]
        public int JobId { get; set; }
        public int JobGroupId { get; set; }
    }
}
