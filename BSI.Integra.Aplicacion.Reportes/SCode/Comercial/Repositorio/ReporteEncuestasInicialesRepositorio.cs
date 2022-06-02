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
    /// Repositorio: Reportes/ReporteEncuestasIniciales
    /// Autor: Abelson Quiñones Gutierrez
    /// Fecha: 16/06/2021
    /// <summary>
    /// El repositorio contiene la funcionalidad de consulta a la db segun lo solicitado por el controllador
    /// </summary>
    public class ReporteEncuestasInicialesRepositorio: BaseRepository<TSubAreaCapacitacion, SubAreaCapacitacionBO>
    {
        /// Autor: Abelson Quiñones Gutierrez
        /// Fecha: 16/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener la lista de docentes 
        /// </summary>
        /// <returns>Lista del docentes para el filtro en un List<ItemComboAutocompleDTO></returns>
        public List<ItemComboAutocompleDTO> ObtenerListaDocentes()
        {
            try
            {
                List<ItemComboAutocompleDTO> items = new List<ItemComboAutocompleDTO>();
                var query = string.Empty;
                query = "SELECT Id As Valor,CONCAT(ApePaterno,' ',ApeMaterno,', ',Nombre1,' ',Nombre2) AS Nombre FROM fin.T_Proveedor where Estado=1";
                var lista = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(lista) && !lista.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ItemComboAutocompleDTO>>(lista);
                }
                return items;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        /// Autor: Abelson Quiñones Gutierrez
        /// Fecha: 16/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener el reporte de encuestas iniciales segun el filtro ingresado
        /// </summary>
        /// <param name="filtroReporte">filtro para la seleccion del reporte por fechainicio, fechafin, programa, curso,docente y matricula</param>
        /// <returns>Lista del reporte encuenstas iniciales en un List<ReporteEncuestasInicialesDTO></returns>
        public List<ReporteEncuestasInicialesDTO> GenerarReporteEncuestasIniciales(ReporteEncuestasInicialesFiltroDTO filtroReporte)
        {
            try
            {
                string matricula = "%%";
                string programa = null, curso = null, docente = null;
                if (filtroReporte.Programa != null && filtroReporte.Programa.Count() > 0) programa = String.Join(",", filtroReporte.Programa);
                if (filtroReporte.Curso != null && filtroReporte.Curso.Count() > 0) curso = String.Join(",", filtroReporte.Curso);
                if (filtroReporte.Docente != null && filtroReporte.Docente.Count() > 0) docente = String.Join(",", filtroReporte.Docente);
                if (filtroReporte.Matricula != null && filtroReporte.Matricula != "") matricula = "%" + filtroReporte.Matricula + "%";
                DateTime fechainicio = new DateTime(filtroReporte.FechaInicial.Year, filtroReporte.FechaInicial.Month, filtroReporte.FechaInicial.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroReporte.FechaFin.Year, filtroReporte.FechaFin.Month, filtroReporte.FechaFin.Day, 23, 59, 59);
                
                List<ReporteEncuestasInicialesDTO> reporteEncuestasIniciales = new List<ReporteEncuestasInicialesDTO>();
                var query = _dapper.QuerySPDapper("[pla].[SP_ReporteEncuestasIniciales]", new { fechainicio, fechafin, programa, curso,docente,matricula });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    reporteEncuestasIniciales = JsonConvert.DeserializeObject<List<ReporteEncuestasInicialesDTO>>(query);
                }
                return reporteEncuestasIniciales;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
