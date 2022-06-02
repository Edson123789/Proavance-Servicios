using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    /// BO: Planificacion/ScrapingPaginaBO
    /// Autor: Ansoli Deyvis
    /// Fecha: 21-01-2021
    /// <summary>
    /// BO de la tabla T_ScrapingPagina
    /// </summary>
    public class ScrapingPaginaBO : BaseBO
    {
        /// Propiedades	Significado
        /// -----------	------------
        /// Nombre		Nombre del portal
        /// Url		Url del Portal
        /// EsPortalEmpleo		Indica si el registro es de tipo Portal Empleo
        public string Nombre { get; set; }
        public string Url { get; set; }
        public string EsPortalEmpleo { get; set; }
    }
}
