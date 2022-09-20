using Domain.Entities.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ViewModels.Extra;
using ViewModels.ForIn;

namespace ViewModels
{
    public class GroupViewModel : SBaseEntity
    {
        [Key]
        public int GroupId { get; set; }
        public List<GroupCourses> groupCourses { get; set; }
    }
}
