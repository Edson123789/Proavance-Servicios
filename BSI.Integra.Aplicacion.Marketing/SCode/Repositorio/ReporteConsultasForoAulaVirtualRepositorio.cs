using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    /// Controlador: ReporteConsultasForoAulaVirtual
    /// Autor: Cesar Santillana
    /// Fecha: 21/06/2021
    /// <summary>
    /// Contiene las funciones necesarias para generar el reporte de consultas del foro de aula virtual
    /// </summary>
    public class ReporteConsultasForoAulaVirtualRepositorio : BaseRepository<TSubAreaCapacitacion, SubAreaCapacitacionBO>
    {
        #region Metodos Base
        public ReporteConsultasForoAulaVirtualRepositorio() : base()
        {
        }
        public ReporteConsultasForoAulaVirtualRepositorio(integraDBContext contexto) : base(contexto)
        {
        }

        #endregion

        /// Autor: Cesar Santillana
        /// Fecha: 21/06/2021
        /// Version: 1.0
        /// <summary>
        /// Función que trae la data de consultas del Foro de AulaVirtual, segun los filtros suministrados.
        /// </summary>
        /// <returns>Retorma una lista List<ReporteConsultasForoAulaVirtualDTO> </returns>
        public List<ReporteConsultasForoAulaVirtualDTO> GenerarReporteConsultasForoAulaVirtual(ReporteConsultasForoFiltroDTO filtroReporte)
        {
            try
            {
                string programa = null, docente = null, curso = null;
                if (filtroReporte.Programa != null && filtroReporte.Programa.Count() > 0) programa = String.Join(",", filtroReporte.Programa);
                if (filtroReporte.Docente != null && filtroReporte.Docente.Count() > 0) docente = String.Join(",", filtroReporte.Docente);
                if (filtroReporte.Curso != null && filtroReporte.Curso.Count() > 0) curso = String.Join(",", filtroReporte.Curso);

                DateTime fechainicio = new DateTime(filtroReporte.FechaInicial.Year, filtroReporte.FechaInicial.Month, filtroReporte.FechaInicial.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroReporte.FechaFin.Year, filtroReporte.FechaFin.Month, filtroReporte.FechaFin.Day, 23, 59, 59);
                List<ReporteConsultasForoAulaVirtualDTO> reporteConsultasForo = new List<ReporteConsultasForoAulaVirtualDTO>();
                var query = _dapper.QuerySPDapper("[mkt].[SP_ReporteConsultasForoAulaVirtual]", new { fechainicio, fechafin, programa, docente, curso });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    reporteConsultasForo = JsonConvert.DeserializeObject<List<ReporteConsultasForoAulaVirtualDTO>>(query);
                }
                return reporteConsultasForo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }

}
