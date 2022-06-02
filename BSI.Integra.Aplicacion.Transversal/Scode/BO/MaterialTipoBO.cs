using BSI.Integra.Aplicacion.Base.BO;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Transversal/MaterialTipo
    /// Autor: Gian Miranda
    /// Fecha: 11/07/2021
    /// <summary>
    /// BO para la logica de la metrica de los tipos de materiales
    /// </summary>
    public class MaterialTipoBO : BaseBO
    {
        /// Propiedades	                Significado
        /// -----------	                ------------
        /// Nombre                      Nombre del tipo de material
        /// Descripcion                 Descripcion del tipo de material
        /// IdMigracion                 Id migracion de V3 (Campo nullable)

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<MaterialAsociacionAccionBO> ListaMaterialAsociacionAccion { get; set; }
        public virtual ICollection<MaterialAsociacionCriterioVerificacionBO> ListaMaterialAsociacionCriterioVerificacion { get; set; }
        public virtual ICollection<MaterialAsociacionVersionBO> ListaMaterialAsociacionVersion { get; set; }
        public MaterialTipoBO() {
            ListaMaterialAsociacionAccion = new List<MaterialAsociacionAccionBO>();
            ListaMaterialAsociacionCriterioVerificacion = new List<MaterialAsociacionCriterioVerificacionBO>();
            ListaMaterialAsociacionVersion = new List<MaterialAsociacionVersionBO>();
        }
    }
}
