using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ViewModels.ForIn;

namespace ViewModels
{
    public class JobViewModel : SBaseEntity
    {
        [Key]
        public int JobId { get; set; }
        public ForInJobGroup jobGroup { get; set; }
    }
}
