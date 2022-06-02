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
    /// Controlador: ReporteEncuestasIntermedias
    /// Autor: Cesar Santillana
    /// Fecha: 17/06/2021
    /// <summary>
    /// Contiene las funciones necesarias para generar el reporte de encuestas intermedias
    /// </summary>
    public class ReporteEncuestasIntermediasRepositorio : BaseRepository<TSubAreaCapacitacion, SubAreaCapacitacionBO>
    {
        #region Metodos Base
        public ReporteEncuestasIntermediasRepositorio() : base()
        {
        }
        public ReporteEncuestasIntermediasRepositorio(integraDBContext contexto) : base(contexto)
        {
        }

        #endregion

        /// Autor: Cesar Santillana
        /// Fecha: 17/06/2021
        /// Version: 1.0
        /// <summary>
        /// Función que trae la data de las encuestas intermedias, segun los filtros suministrados.
        /// </summary>
        /// <returns>Retorma una lista List<ReporteEncuestasIntermediasDTO> </returns>
        public List<ReporteEncuestasIntermediasDTO> GenerarReporteEncuestasIntermedias(ReporteEncuestasIntermediasFiltroDTO filtroReporte)
        {
            try
            {
                string programa = null, docente = null, curso = null, codigoMatricula = null;
                if (filtroReporte.Programa != null && filtroReporte.Programa.Count() > 0) programa = String.Join(",", filtroReporte.Programa);
                if (filtroReporte.Docente != null && filtroReporte.Docente.Count() > 0) docente = String.Join(",", filtroReporte.Docente);
                if (filtroReporte.Curso != null && filtroReporte.Curso.Count() > 0) curso = String.Join(",", filtroReporte.Curso);
                if (filtroReporte.CodigoMatricula != null && filtroReporte.CodigoMatricula.Count() > 0) codigoMatricula = String.Join(",", filtroReporte.CodigoMatricula);
                
                DateTime fechainicio = new DateTime(filtroReporte.FechaInicial.Year, filtroReporte.FechaInicial.Month, filtroReporte.FechaInicial.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroReporte.FechaFin.Year, filtroReporte.FechaFin.Month, filtroReporte.FechaFin.Day, 23, 59, 59);
                List<ReporteEncuestasIntermediasDTO> reporteEncuestaIntermedia = new List<ReporteEncuestasIntermediasDTO>();
                var query = _dapper.QuerySPDapper("[pla].[SP_ReporteEncuestasIntermedias]", new { fechainicio, fechafin, programa, docente, curso, codigoMatricula});
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    reporteEncuestaIntermedia = JsonConvert.DeserializeObject<List<ReporteEncuestasIntermediasDTO>>(query);
                }
                return reporteEncuestaIntermedia;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
