using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Entities
{
    public class EducationalCalendar
    {
        public string Professor{ get; set; }
        public string PerformDate{ get; set; }
        public string Course{ get; set; }
        public int ReqNo{ get; set; }
        public int Capacity{ get; set; }
        public string Place{ get; set; }
        public string CourseStatus{ get; set; }
        public string Applicant{ get; set; }
    }
}
