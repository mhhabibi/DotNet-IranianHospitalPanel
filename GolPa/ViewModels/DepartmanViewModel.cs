using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using ViewModels.ForIn;

namespace ViewModels
{
    public class DepartmanViewModel : SBaseEntity
    {
        [Key]
        public int DepartmanId { get; set; }
        public ForInPerson Manager { get; set; }

    }
}
