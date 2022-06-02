using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    /// BO: Planificacion/ScrapingEmpleoResultadoClasificacion
    /// Autor: Ansoli Deyvis
    /// Fecha: 09-12-2021
    /// <summary>
    /// BO de la tabla T_ScrapingEmpleoResultadoClasificacion
    /// </summary>
    public class ScrapingEmpleoResultadoClasificacionBO : BaseBO
    {
        /// Propiedades	Significado
        /// -----------	------------
        /// IdScrapingPortalEmpleoResultado		Fk con la tabla T_ScrapingPortalEmpleoResultado
        /// IdScrapingEmpleoPatronClasificacion		Fk con la tabla T_ScrapingPortalEmpleoResultado
        /// IdAreaTrabajo		Fk con la tabla T_AreaTrabajo
        /// IdAreaFormacion     Fk con la tabla T_AreaFormacion
        /// IdCargo         Fk con la tabla T_Cargo
        /// IdIndustria     Fk con la tabla T_Industria
        public int IdScrapingPortalEmpleoResultado { get; set; }
        public int? IdScrapingEmpleoPatronClasificacion { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int? IdCargo { get; set; }
        public int? IdIndustria { get; set; }

        /// <summary>
        /// Elimina la clasificacion del anuncio
        /// </summary>
        /// <param name="agrupacion">Parametros de ubicacion</param>
        /// <param name="usuario">Usuario responsable</param>
        /// <returns>Estado de la eliminación</returns>
        public bool EliminarClasificacionEmpleo(ScrapinPortalEmpleoClasificacionAgrupadaDTO agrupacion)
        {
            ScrapingEmpleoResultadoClasificacionRepositorio
                repo = new ScrapingEmpleoResultadoClasificacionRepositorio();
            var listado =
                repo.GetBy(w =>
                        w.IdScrapingPortalEmpleoResultado == agrupacion.IdScrapingPortalEmpleoResultado &&
                        w.IdAreaFormacion == agrupacion.IdAreaFormacion &&
                        w.IdAreaTrabajo == agrupacion.IdAreaTrabajo &&
                        w.IdCargo == agrupacion.IdCargo &&
                        w.IdIndustria == agrupacion.IdIndustria, s => new { s.Id }
                );
            if (listado != null && listado.Count > 0)
                repo.Delete(listado.Select(s => s.Id), agrupacion.NombreUsuario);

            return true;
        }
    }
}
