using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public class PEspecificoMatriculaAlumnoBO : BaseBO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPespecifico { get; set; }
        public int IdPespecificoTipoMatricula { get; set; }
        public int? IdUsuarioMoodle { get; set; }
        public int? IdCursoMoodle { get; set; }
        public bool? AplicaNuevaAulaVirtual { get; set; }
        public int Grupo { get; set; }
        public int? IdMigracion { get; set; }
    }
}
