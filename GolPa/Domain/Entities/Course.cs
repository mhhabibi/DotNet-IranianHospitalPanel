using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Entities
{
    public class Course : SBaseEntity
    {
        [Key]
        public int CourseId { get; set; }
    }
}
