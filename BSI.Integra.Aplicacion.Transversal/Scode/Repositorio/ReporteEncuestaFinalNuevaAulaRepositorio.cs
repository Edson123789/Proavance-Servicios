using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using System.Linq;
using BSI.Integra.Persistencia.SCode.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Transversal/ReporteEncuestaFinalNuevaAula
    /// Autor : Lourdes Priscila Pacsi Gamboa
    /// Fecha: 17/06/2021
    /// <summary>
    /// Repositorio para consultas de la tabla pla.T_ExamenRealizadoRespuestaAulaVirtual
    /// </summary>
    public class ReporteEncuestaFinalNuevaAulaRepositorio
    {
        private DapperRepository _dapper;
        public ReporteEncuestaFinalNuevaAulaRepositorio()
        {
            _dapper = new DapperRepository();
        }
        /// Repositorio : ReporteEncuestaFinalNuevaAulaRepositorio
        /// Autor: Lourdes Priscila Pacsi Gamboa
        /// Fecha: 17/06/2021
        /// <summary>
        /// Obtiene los datos de la encuensta final realizada en el nuevo aula virtual para el reporte
        /// </summary>
        /// <param name="filtro">Datos traidos desde la interfaz para el filtro en el sp</param>
        /// <returns>Lista de objetos del tipo EncuestaFinalNuevaAulaDTO</returns>
        public List<EncuestaFinalNuevaAulaDTO> ObtenerDatosEncuestaFinal(FiltroReporteEncuestaFinalNuevaAulaDTO filtro)
        {
            try
            {
                var filtros = new
                {
                    ListaPGeneral = filtro.ProgramaGeneral == null ? "" : string.Join(",", filtro.ProgramaGeneral.Select(x => x)),
                    ListaPEspecifico = filtro.ProgramaEspecifico == null ? "" : string.Join(",", filtro.ProgramaEspecifico.Select(x => x)),
                    CodigoMatricula = filtro.CodigoMatricula,
                    ListaDocente = filtro.Docente == null ? "" : string.Join(",", filtro.Docente.Select(x => x)),
                    FechaInicio = new DateTime(filtro.FechaInicio.Year, filtro.FechaInicio.Month, filtro.FechaInicio.Day, 0, 0, 0),
                    FechaFin = new DateTime(filtro.FechaFin.Year, filtro.FechaFin.Month, filtro.FechaFin.Day, 23, 59, 59),
                };

                List<EncuestaFinalNuevaAulaDTO> informacionEncuesta = new List<EncuestaFinalNuevaAulaDTO>();
                string query = string.Empty;
                query = "[gp].[SP_ObtenerEncuestaFinal]";
                var subQuery = _dapper.QuerySPDapper(query, filtros);
                if (!string.IsNullOrEmpty(subQuery) && !subQuery.Contains("[]"))
                {
                    informacionEncuesta = JsonConvert.DeserializeObject<List<EncuestaFinalNuevaAulaDTO>>(subQuery);
                }
                return informacionEncuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
