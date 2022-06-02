using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Scode.BO
{
    public class TipoDocumentoAlumnoBO :BaseBO
    {
        public string Nombre { get; set; }
        public int IdPlantillaFrontal { get; set; }
        public int IdPlantillaPosterior { get; set; }
        public int IdOperadorComparacion { get; set; }
        public bool TieneDeuda { get; set; }
        //public List<TipoDocumentoAlumnoModalidadCursoBO> IdModalidadCurso{ get; set; }
        //public List<TipoDocumentoAlumnoEstadoMatriculaBO> IdEstadoMatricula { get; set; }
        //public List<TipoDocumentoAlumnoSubEstadoMatriculaBO> IdSubEstadoMatricula { get; set; }
        //public List<TipoDocumentoAlumnoPGeneralBO> IdPGeneral { get; set; }
    }
}
