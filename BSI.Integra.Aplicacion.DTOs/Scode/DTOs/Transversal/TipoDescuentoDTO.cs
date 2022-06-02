using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
   public class TipoDescuentoDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Formula { get; set; }
        public int? PorcentajeGeneral { get; set; }
        public int? PorcentajeMatricula { get; set; }
        public int? FraccionesMatricula { get; set; }
        public int? PorcentajeCuotas { get; set; }
        public int? CuotasAdicionales { get; set; }
    }
}
