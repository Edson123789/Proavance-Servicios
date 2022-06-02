using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    /// BO: Planificacion/ScrapingEmpleoPatronClasificacion
    /// Autor: Ansoli Deyvis
    /// Fecha: 10-12-2021
    /// <summary>
    /// BO de la tabla T_ScrapingEmpleoPatronClasificacion
    /// </summary>
    public class ScrapingEmpleoPatronClasificacionBO : BaseBO
    {
        /// Propiedades	Significado
        /// -----------	------------
        /// IdAreaTrabajo		Fk con la tabla T_AreaTrabajo
        /// IdAreaFormacion     Fk con la tabla T_AreaFormacion
        /// IdCargo         Fk con la tabla T_Cargo
        /// IdIndustria     Fk con la tabla T_Industria
        /// Patron     Patron de Busqueda
        public int? IdAreaTrabajo { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int? IdCargo { get; set; }
        public int? IdIndustria { get; set; }
        public string Patron { get; set; }
    }
}
