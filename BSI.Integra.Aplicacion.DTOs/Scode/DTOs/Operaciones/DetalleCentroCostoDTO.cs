using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public partial class DetalleCentroCostoDTO
    {
        public int Id { get; set; }
        public List<RaAlumnoListadoMinimoDTO> ListadoAlumnos { get; set; }
        public List<RaListadoMinimoCursoDTO> ListadoCursoMinimo {get;set;}
    }
}
