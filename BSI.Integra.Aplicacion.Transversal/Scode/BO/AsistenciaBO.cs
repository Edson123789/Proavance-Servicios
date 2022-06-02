using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class AsistenciaBO : BaseBO
    {
        public int IdPespecificoSesion { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public bool Asistio { get; set; }
        public bool Justifico { get; set; }
        public int? IdMigracion { get; set; }
        public List<MaterialEntregaBO> ListaMaterialEntrega { get; set; }
        public AsistenciaBO() {
            ListaMaterialEntrega = new List<MaterialEntregaBO>();
        }
    }
}
