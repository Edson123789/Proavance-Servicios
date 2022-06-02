using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{ 

    public class OportunidadTiempoCapacitacionDTO
    {
        public int Id { get; set; }
        public int? IdTiempoCapacitacion { get; set; }
        public int? IdTiempoCapacitacionValidacion { get; set; }
        public string Usuario { get; set; }

    }
}
