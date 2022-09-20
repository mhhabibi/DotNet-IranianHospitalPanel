using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Entities
{
    public class QuestionAnswer
    {
        [Key]
        public int Id { get; set; }
        public int QId { get; set; }
        public int AnswerId { get; set; }
        public int PersonId { get; set; }
    }
}
