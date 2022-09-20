using System;
using System.Collections.Generic;
using System.Text;
using ViewModels.Extra;
using ViewModels.ForIn;

namespace ViewModels
{
    public class RequestCourseViewModel
    {
        public int ReqId { get; set; }
        public string PerformDate{ get; set; }
        public string FinishDate{ get; set; }
        public string EventPlace{ get; set; }
        public string Proffessor { get; set; }
        public string ReqDate{ get; set; }
        public int NeedTime { get; set; }
        public string CourseTitle{ get; set; }
        public string CourseId{ get; set; }
        public string ConfirmStatus { get; set; }
        public string Description { get; set; }
        public int WomanCapacity{ get; set; }
        public int ManCapacity{ get; set; }
        public List<CoursePersonels> Personels;
        public ForInPer_ ReqUser { get; set; }
    }
}
