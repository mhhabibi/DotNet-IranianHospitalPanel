using Domain.Common;
using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities.Entities
{
    public class Group : SBaseEntity
    {
        [Key]
        public int GroupId { get; set; }
        public List<int> GroupCourses { get; set; }
    }
}
