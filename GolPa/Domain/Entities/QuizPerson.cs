using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities.Entities
{
    public class QuizPerson
    {
        [Key]
        public int Id { get; set; }
        public int QuizId { get; set; }
        public int PersonId { get; set; }
        public float Grade { get; set; }
        public string FullName { get; set; }
    }
}
