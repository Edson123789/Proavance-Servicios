using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MatriculaDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public string NombreProgramaGeneral { get; set; }
        public int? VersionPrograma { get; set; }
        public int? IdCentroCosto { get; set; }
        public string Documentos { get; set; }
    }
}
