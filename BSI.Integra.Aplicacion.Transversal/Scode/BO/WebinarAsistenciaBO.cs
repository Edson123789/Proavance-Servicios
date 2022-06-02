using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class WebinarAsistenciaBO : BaseBO
    {
        public int IdWebinarDetalle { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public bool?  ConfirmoAsistencia { get; set; }
        public bool Asistio { get; set; }
        public int? IdMigracion { get; set; }
    }
}
