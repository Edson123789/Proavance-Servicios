using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteContactabilidadCombosDTO
    {
        public List<AsesorNombreFiltroDTO> Asesores { get; set; }

        public List<PersonalAsignadoDTO> AsistentesActivos { get; set; }
        public List<PersonalAsignadoDTO> AsistentesInactivos { get; set; }
        public List<PersonalAsignadoDTO> AsistentesTotales { get; set; }
    }
}
