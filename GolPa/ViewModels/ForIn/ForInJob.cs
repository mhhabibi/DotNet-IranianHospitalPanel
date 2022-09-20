using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels.ForIn
{
    public class ForInJob
    {
        public int? JobId { get; set; }
        public string Title { get; set; }
        public int? JobGroupId { get; set; }
        public string JobGroupTitle { get; set; }
    }
}
