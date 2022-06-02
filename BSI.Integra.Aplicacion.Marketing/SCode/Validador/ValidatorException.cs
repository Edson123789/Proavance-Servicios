using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.Validador
{
    public class ValidatorException : Exception
    {
        public int IdContactoDuplicado { get; set; }
        public ValidatorException()
        {
        }

        public ValidatorException(string Message): base(Message)
        {
        }

        public ValidatorException(string Message, Exception Ex): base(Message, Ex)
        {
        }
    }
}
