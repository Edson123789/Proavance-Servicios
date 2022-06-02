using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroObtenerSesionesDTO
    {
        public List<int> ListaPEspecificos { get; set; }
        public int PEspecificoId { get; set; }
        public bool? CursoIndividual { get; set; }
        public int? nroGrupo { get; set; }
    }
}
