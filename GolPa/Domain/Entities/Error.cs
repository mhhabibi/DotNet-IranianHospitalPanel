using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Entities
{
    public class Error
    {
        public string ErroreCode { get; set; }
        public string ErroreMessage { get; set; }

        public Error(string ErroreCode, string ErroreMessage)
        {
            this.ErroreCode = ErroreCode;
            this.ErroreMessage = ErroreMessage;
        }
    }
}
