using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SubEstadoMatriculaFiltroDTO
    {
        public int Id {get; set;}
        public string Nombre {get; set;}
        public int IdEstadoMatricula {get; set;}
    }

    public class SubEstadoMatriculaFiltroConfiguracionCoordinadoraDTO
    {
        public int IdSubEstadoMatricula { get; set; }
        public string SubEstadoMatricula { get; set; }
        public int IdEstadoMatricula { get; set; }
    }
}
