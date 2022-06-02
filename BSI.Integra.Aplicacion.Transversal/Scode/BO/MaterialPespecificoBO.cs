using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class MaterialPespecificoBO : BaseBO
    {
        public int IdPespecifico { get; set; }
        public int Grupo { get; set; }
        public int IdMaterialTipo { get; set; }
        public int GrupoEdicion { get; set; }
        public int GrupoEdicionOrden { get; set; }
        public int? IdFur { get; set; }
        public int? IdMigracion { get; set; }
        public List<MaterialPespecificoDetalleBO> ListaMaterialPespecificoDetalle { get; set; }

        public MaterialPespecificoBO() {
            ListaMaterialPespecificoDetalle = new List<MaterialPespecificoDetalleBO>();
        }
    }
}
