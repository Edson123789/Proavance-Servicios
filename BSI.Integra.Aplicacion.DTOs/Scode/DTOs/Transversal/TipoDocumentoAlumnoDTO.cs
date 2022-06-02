using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    public class TipoDocumentoAlumnoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdPlantillaFrontal { get; set; }
        public int IdPlantillaPosterior { get; set; }
        public int IdOperadorComparacion { get; set; }
        public bool TieneDeuda { get; set; }
        public List<TipoDocumentoAlumnoModalidadCursoDTO> IdModalidadCurso { get; set; }
        public List<TipoDocumentoAlumnoEstadoMatriculaDTO> IdEstadoMatricula { get; set; }
        public List<TipoDocumentoAlumnoSubEstadoMatriculaDTO> IdSubEstadoMatricula { get; set; }
        public List<TipoDocumentoAlumnoPGeneralDTO> IdPGeneral { get; set; }
        public string Usuario { get; set; }
    }
}
