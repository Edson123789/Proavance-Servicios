using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public partial class MatriculaDetalleBO : BaseBO
    {
        public int? IdMatriculaCabecera { get; set; }
        public int? IdCursoPespecifico { get; set; }
        public int? IdMigracion { get; set; }
    }
}
