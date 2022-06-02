using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class MaterialPespecificoSesionBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPespecificoSesion { get; set; }
        public int IdMaterialTipo { get; set; }
        public int? IdFur { get; set; }
        public int? IdMigracion { get; set; }
        public List<MaterialVersionBO> ListaMaterialVersion { get; set; }
        public MaterialPespecificoSesionBO() {
            ListaMaterialVersion = new List<MaterialVersionBO>();
        }
    }
}
