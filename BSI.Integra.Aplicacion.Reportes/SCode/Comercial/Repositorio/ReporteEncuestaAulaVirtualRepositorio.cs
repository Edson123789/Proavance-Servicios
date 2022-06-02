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
    /// Repositorio: Reportes/ReporteEncuestaAulaVirtual
    /// Autor: Abelson Quiñones Gutierrez
    /// Fecha: 16/06/2021
    /// <summary>
    /// El repositorio contiene la funcionalidad de consulta a la db segun lo solicitado por el controllador
    /// </summary>
    public class ReporteEncuestaAulaVirtualRepositorio: BaseRepository<TSubAreaCapacitacion, SubAreaCapacitacionBO>
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
                query = "select distinct Id as Valor,Nombre AS Nombre  from fin.V_Obtener_ProveedorParaHonorario order by Nombre";
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
        /// Fecha: 09/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener la lista de docentes 
        /// </summary>
        /// <returns>Lista del docentes para el filtro en un List<ItemComboAutocompleDTO></returns>

        public List<ItemComboAutocompleDTO> ObtenerListaAsistenteAC()
        {
            try
            {
                List<ItemComboAutocompleDTO> items = new List<ItemComboAutocompleDTO>();
                var query = string.Empty;
                query = "select TIAU.UserName AS Valor, CONCAT(UPPER(TPG.Nombres),' ',UPPER(tpg.Apellidos)) AS Nombre from GP.T_Personal AS TPG INNER JOIN conf.T_Integra_AspNetUsers AS TIAU ON TIAU.PerId = TPG.Id where AreaAbrev = 'OP' and Rol like'%cliente%' and Activo = 1 order by Nombre";
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
        public List<ReporteEncuestaAulaVirtualDTO> GenerarReporteEncuestaAulaVirtual(ReporteEncuestaAulaVirtualFiltroDTO filtroReporte)
        {
            try
            {
                string codigoMatricula = null;
                string programa = null, curso = null, docente = null,asistenteAC=null;
                if (filtroReporte.Programa != null && filtroReporte.Programa.Count() > 0) programa = String.Join(",", filtroReporte.Programa);
                if (filtroReporte.Curso != null && filtroReporte.Curso.Count() > 0) curso = String.Join(",", filtroReporte.Curso);
                if (filtroReporte.Docente != null && filtroReporte.Docente.Count() > 0) docente = String.Join(",", filtroReporte.Docente);
                if (filtroReporte.Matricula != null && filtroReporte.Matricula != "") codigoMatricula = "%" + filtroReporte.Matricula + "%";
                if (filtroReporte.AsistenteAC != null && filtroReporte.AsistenteAC.Count() >0) asistenteAC = String.Join(",", filtroReporte.AsistenteAC);
                DateTime fechainicio = new DateTime(filtroReporte.FechaInicial.Year, filtroReporte.FechaInicial.Month, filtroReporte.FechaInicial.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroReporte.FechaFin.Year, filtroReporte.FechaFin.Month, filtroReporte.FechaFin.Day, 23, 59, 59);

                List<ReporteEncuestaAulaVirtualDTO> reporteEncuestaAulaVirtual = new List<ReporteEncuestaAulaVirtualDTO>();

                if (filtroReporte.OrigenDato == 1)//1 :para Nueva Aula 
                {

                    var query = _dapper.QuerySPDapper("[pla].[SP_ReporteEncuestaAulaVirtual]", new { fechainicio, fechafin, programa, curso, docente, codigoMatricula, asistenteAC });
                    if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                    {
                        reporteEncuestaAulaVirtual = JsonConvert.DeserializeObject<List<ReporteEncuestaAulaVirtualDTO>>(query);
                        if (filtroReporte.TipoEncuesta == 10) // 10 : Encuesta inicial
                        {
                            reporteEncuestaAulaVirtual = reporteEncuestaAulaVirtual.FindAll(x => x.TipoEncuesta == "INICIAL");
                        } else if (filtroReporte.TipoEncuesta == 11) //11 : Encuesta intermedia
                        {
                            reporteEncuestaAulaVirtual = reporteEncuestaAulaVirtual.FindAll(x => x.TipoEncuesta == "INTERMEDIA");
                        }
                        else if(filtroReporte.TipoEncuesta == 12) //12 : Encuesta final
                        {
                            reporteEncuestaAulaVirtual = reporteEncuestaAulaVirtual.FindAll(x => x.TipoEncuesta == "FINAL");
                        }
                    }
                }
                else if(filtroReporte.OrigenDato == 2) //2: para moodle
                {

                }
                

                
                return reporteEncuestaAulaVirtual;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
