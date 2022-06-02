using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{

    public class ProyectoPresentadoPorAlumnoRepositorio : BaseRepository<TSubAreaCapacitacion, SubAreaCapacitacionBO>
    {
        #region Metodos Base
        public ProyectoPresentadoPorAlumnoRepositorio() : base()
        {
        }
        public ProyectoPresentadoPorAlumnoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }

        #endregion

        /// Autor: Cesar Santillana
        /// Fecha: 21/06/2021
        /// Version: 1.0
        /// <summary>
        /// Función que trae la data de proyectos presentados por el alumno, segun los filtros suministrados.
        /// </summary>
        /// <returns>Retorma una lista List<ProyectoPresentadoPorAlumnoDTO> </returns>
        public List<ProyectoPresentadoPorAlumnoDTO> GenerarReporteProyectoPresentadoPorAlumno(ProyectoPresentadoPorAlumnoFiltroDTO filtroReporte)
        {
            try
            {
                string programaEspecifico = null, docente = null, centroCosto = null, coordinadora = null, codigoMatricula = null;
                if (filtroReporte.ProgramaEspecifico != null && filtroReporte.ProgramaEspecifico.Count() > 0) programaEspecifico = String.Join(",", filtroReporte.ProgramaEspecifico);
                if (filtroReporte.Docente != null && filtroReporte.Docente.Count() > 0) docente = String.Join(",", filtroReporte.Docente);
                if (filtroReporte.CentroCosto != null && filtroReporte.CentroCosto.Count() > 0) centroCosto = String.Join(",", filtroReporte.CentroCosto);
                if (filtroReporte.Coordinadora != null && filtroReporte.Coordinadora.Count() > 0) coordinadora = String.Join(",", filtroReporte.Coordinadora);
                if (filtroReporte.CodigoMatricula != null && filtroReporte.CodigoMatricula!= 0) codigoMatricula = String.Join(",", filtroReporte.CodigoMatricula);

                DateTime fechainicio = new DateTime(filtroReporte.FechaInicial.Year, filtroReporte.FechaInicial.Month, filtroReporte.FechaInicial.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroReporte.FechaFin.Year, filtroReporte.FechaFin.Month, filtroReporte.FechaFin.Day, 23, 59, 59);
                List<ProyectoPresentadoPorAlumnoDTO> reporteProyectoPorAlumno = new List<ProyectoPresentadoPorAlumnoDTO>();
                var query = _dapper.QuerySPDapper("[pla].[SP_ReporteProyectoPresentadoPorAlumno]", new { fechainicio, fechafin, programaEspecifico,centroCosto, docente,coordinadora, codigoMatricula });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    reporteProyectoPorAlumno = JsonConvert.DeserializeObject<List<ProyectoPresentadoPorAlumnoDTO>>(query);
                }
                return reporteProyectoPorAlumno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // <summary>
        /// Función que trae la data de centro de Costos
        /// </summary>
        /// <returns>Retorma una lista List<centroCostoFiltroDTO> </returns>
        public List<centroCostoFiltroDTO> ObtenerCentroCosto()
        {
            try
            {
                List<centroCostoFiltroDTO> coordinadores = new List<centroCostoFiltroDTO>();
                var _query = "SELECT Nombre, Id FROM [pla].[T_CentroCosto] where estado = @Estado ";
                var personalDB = _dapper.QueryDapper(_query, new { Estado = "1" });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    coordinadores = JsonConvert.DeserializeObject<List<centroCostoFiltroDTO>>(personalDB);
                }
                return coordinadores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
