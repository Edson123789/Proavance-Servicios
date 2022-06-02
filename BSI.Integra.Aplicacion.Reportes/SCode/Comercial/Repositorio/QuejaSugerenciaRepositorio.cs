using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.Reportes.Repositorio
{
    /// Repositorio: Reportes/ReporteLibroReclamacion
    /// Autor: Abelson Quiñones Gutierrez
    /// Fecha: 01/06/2021
    /// <summary>
    /// El repositorio contiene la funcionalidad de consulta a la db segun lo solicitado por el controllador
    /// </summary>
    public class QuejaSugerenciaRepositorio: BaseRepository<TSubAreaCapacitacion, SubAreaCapacitacionBO>
    {


        /// Tipo Función: POST
        /// Autor: Abelson Quiñones Gutierrez
        /// Fecha: 01/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener el reporte de quejas y sugerencias segun el filtro ingresado
        /// </summary>
        /// <param name="filtroReporte">filtro para la seleccion del reporte de quejas y sugerencias</param>
        /// <returns>Lista del reporte quejas y sugerencias en un List<QuejaSugerenciaDTO></returns>
        public List<QuejaSugerenciaDTO> GenerarReporteQuejaSugerencia(QuejaSugerenciaFiltroDTO filtroReporte)
        {
            try
            {
                string areas = null, subareas = null, programageneral = null, tipo = null;
                if (filtroReporte.Area != null && filtroReporte.Area.Count() >0) areas = String.Join(",", filtroReporte.Area);
                if (filtroReporte.SubArea != null && filtroReporte.SubArea.Count() > 0) subareas = String.Join(",", filtroReporte.SubArea);
                if (filtroReporte.ProgramaGeneral != null && filtroReporte.ProgramaGeneral.Count() > 0) programageneral = String.Join(",", filtroReporte.ProgramaGeneral);
                if (filtroReporte.Tipo != null && filtroReporte.Tipo.Count() > 0) tipo = String.Join(",", filtroReporte.Tipo);
                DateTime fechainicio = new DateTime(filtroReporte.FechaInicial.Year, filtroReporte.FechaInicial.Month, filtroReporte.FechaInicial.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroReporte.FechaFin.Year, filtroReporte.FechaFin.Month, filtroReporte.FechaFin.Day, 23, 59, 59);
                List<QuejaSugerenciaDTO> reporteQuejaSugerencia = new List<QuejaSugerenciaDTO>();
                var query = _dapper.QuerySPDapper("[mkt].[SP_ReporteQuejaSugerencia]", new { fechainicio, fechafin, areas, subareas,programageneral,tipo});
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    reporteQuejaSugerencia = JsonConvert.DeserializeObject<List<QuejaSugerenciaDTO>>(query);
                }
                return reporteQuejaSugerencia;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
