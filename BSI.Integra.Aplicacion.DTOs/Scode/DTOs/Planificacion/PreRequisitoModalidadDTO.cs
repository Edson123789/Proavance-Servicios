using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PreRequisitoModalidadDTO
    {
        public int IdPreRequisito { get; set; }
        public int IdPGeneral { get; set; }
        public string NombrePreRequisito { get; set; }
        public int Orden { get; set; }
        public int Tipo { get; set; }
        public int IdModalidadCurso { get; set; }

        public string NombreModalidad { get; set; }
        public int IdModalidadPreRequisito { get; set; }
    }
}
