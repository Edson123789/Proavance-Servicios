using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: PlantillaAsociacionModuloSistemaBO
    /// Autor: _ _ _ _ _ .
    /// Fecha: 30/04/2021
    /// <summary>
    /// BO para la logica de PlantillaAsociacionModuloSistema
    /// </summary>
    public class PlantillaAsociacionModuloSistemaBO : BaseBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// IdPlantilla                         Id de Plantilla
        /// IdModuloSistema                     Id de módulo de sistema
        /// IdMigracion                         Id de migración
        public int IdPlantilla { get; set; }
        public int IdModuloSistema { get; set; }
        public int? IdMigracion { get; set; }

        //public virtual ModuloSistemaBO IdModuloSistemaNavigation { get; set; }
        //public virtual PlantillaBO IdPlantillaNavigation { get; set; }
    }
}
