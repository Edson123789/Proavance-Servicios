using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class OportunidadWhatsappDTO
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public string Celular { get; set; }
        public int IdPersonal { get; set; }
    }
}
