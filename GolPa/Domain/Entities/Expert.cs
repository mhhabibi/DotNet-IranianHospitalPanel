using Domain.Common;
using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Entities
{
    public class Expert : SBaseEntity
    {
        [Key]
        public int ExpertId { get; set; }
    }
}
