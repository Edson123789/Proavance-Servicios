using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CajaResponsableDTO
    {
        public int Id { get; set; }
        public string CodigoCaja { get; set; }
        public string PersonalResponsable { get; set; }
        public int IdPersonalResponsable { get; set; }
        public int IdMoneda { get; set; }
    }
}
