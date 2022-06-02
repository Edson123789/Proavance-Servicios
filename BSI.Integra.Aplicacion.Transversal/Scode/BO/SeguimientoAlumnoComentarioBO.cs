using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class SeguimientoAlumnoComentarioBO : BaseBO
    {
        public int? IdMatriculaCabecera { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public int IdSeguimientoAlumnoCategoria { get; set; }
        public int IdPersonal { get; set; }
        public int IdOportunidad { get; set; }
        public string Comentario { get; set; }
        public DateTime? FechaCompromiso { get; set; }
        public int? IdMigracion { get; set; }
    }
}
