using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PreguntaFrecuentePgeneralDTO
    {
        public int? Id { get; set; }
        public int? IdPrograma { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public int? IdTipo { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        public int? IdSeccion { get; set; }
        public string Nombre { get; set; }
    }
}
