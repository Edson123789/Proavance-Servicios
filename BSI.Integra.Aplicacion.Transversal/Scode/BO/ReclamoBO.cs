using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;
namespace BSI.Integra.Aplicacion.Transversal.Scode.BO
{
    public class ReclamoBO : BaseBO
    {
        public int IdMatriculaCabecera { get; set; }
        public string Descripcion { get; set; }
        public int IdReclamoEstado { get; set; }
        public int IdOrigen { get; set; }
        public int IdTipoReclamoAlumno { get; set; }
    }
}
