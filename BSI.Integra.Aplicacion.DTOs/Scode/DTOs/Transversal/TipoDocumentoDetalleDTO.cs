using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    public class TipoDocumentoDetalleDTO
    {
        public int Id { get; set; }
        public List<TipoDocumentoAlumnoModalidadCursoDTO> ModalidadCurso { get; set; }
        public List<TipoDocumentoAlumnoEstadoMatriculaDTO> EstadoMatricula { get; set; }
        public List<TipoDocumentoAlumnoSubEstadoMatriculaDTO> SubEstadoMatricula { get; set; }

        //public TipoDocumentoAlumnoModalidadCursoDTO[] ModalidadCurso { get; set; }
        //public TipoDocumentoAlumnoEstadoMatriculaDTO[] EstadoMatricula { get; set; }
        //public TipoDocumentoAlumnoSubEstadoMatriculaDTO[] SubEstadoMatricula { get; set; }
        public int OperadorComparacion { get; set; }
        public bool TieneDeuda { get; set; }
    }
}
