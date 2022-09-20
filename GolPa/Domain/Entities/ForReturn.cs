using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Entities
{
    public class ForReturn
    {
        public int ResCode { get; set; }
        public string Message { get; set; }
        public dynamic Info { get; set; }
    }
}
