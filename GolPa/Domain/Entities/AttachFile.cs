using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Entities
{
    public class AttachFile
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int RegUser { get; set; }
        public string RegDate { get; set; }
    }
}
