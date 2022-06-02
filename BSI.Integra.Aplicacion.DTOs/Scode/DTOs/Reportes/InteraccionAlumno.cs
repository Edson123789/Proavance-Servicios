using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class InteraccionAlumno
    {
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Categoria { get; set; }
        public string Nombre { get; set; }
        public string TipoInteraccion { get; set; }
        public int Duracion { get; set; }
        public string Asesor { get; set; }
    }
}
