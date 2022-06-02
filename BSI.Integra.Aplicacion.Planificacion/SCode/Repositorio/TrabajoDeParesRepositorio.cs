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
    public class TrabajoDeParesRepositorio : BaseRepository<TSubAreaCapacitacion, SubAreaCapacitacionBO>
    {
        #region Metodos Base
        public TrabajoDeParesRepositorio() : base()
        {
        }
        public TrabajoDeParesRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        #endregion

        /// Autor: Cesar Santillana
        /// Fecha: 28/06/2021
        /// Version: 1.0
        /// <summary>
        /// Función que trae la data de proyectos presentados por el alumno, segun los filtros suministrados.
        /// </summary>
        /// <returns>Retorma una lista List<ProyectoPresentadoPorAlumnoDTO> </returns>
        public List<TrabajoDeParesDTO> GenerarReporteTrabajoDePares(TrabajoDeParesFiltroDTO filtroReporte)
        {
            try
            { 
                string programaEspecifico = null, alumno = null, codigoMatricula = null;
                if (filtroReporte.ProgramaEspecifico != null && filtroReporte.ProgramaEspecifico.Count() > 0) programaEspecifico = String.Join(",", filtroReporte.ProgramaEspecifico);
                if (filtroReporte.Alumno != null && filtroReporte.Alumno.Count() > 0) alumno = String.Join(",", filtroReporte.Alumno);
                if (filtroReporte.CodigoMatricula != null && filtroReporte.CodigoMatricula != 0) codigoMatricula = String.Join("%,", filtroReporte.CodigoMatricula);

                DateTime fechainicio = new DateTime(filtroReporte.FechaInicial.Year, filtroReporte.FechaInicial.Month, filtroReporte.FechaInicial.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroReporte.FechaFin.Year, filtroReporte.FechaFin.Month, filtroReporte.FechaFin.Day, 23, 59, 59);
                List<TrabajoDeParesDTO> reporteTrabajoDePares = new List<TrabajoDeParesDTO>();
                var query = _dapper.QuerySPDapper("[pla].[SP_ReporteTrabajoDePares]", new { fechainicio, fechafin, programaEspecifico, codigoMatricula, alumno });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    reporteTrabajoDePares = JsonConvert.DeserializeObject<List<TrabajoDeParesDTO>>(query);
                }
                
                return reporteTrabajoDePares;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Lourdes Priscila Pacsi Gamboa
        /// Fecha: 06/07/2021
        /// Version: 1.0
        /// <summary>
        /// Actualiza el encargado ed la revision de una tarea
        /// </summary>
        /// <returns>un entero </returns>
        public int ActualizarEncargadoTrabajoPares(int Id, int IdProveedor)
        {
            try
            {

                var _query = "com.SP_ActualizarEncargadoTrabajoPares";
                var subQuery = _dapper.QuerySPDapper(_query, new { Id, IdProveedor });
                if (!string.IsNullOrEmpty(subQuery) && !subQuery.Contains("[]"))
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Lourdes Priscila Pacsi Gamboa
        /// Fecha: 06/07/2021
        /// Version: 1.0
        /// <summary>
        /// Retorna la lista de programas de los alumnos que fueron asignados al docente para la revision del trabajo de pares
        /// </summary>
        /// <returns>Lista del tipo de Objeto ProgramaCentroCostoDocenteDTO </returns>
        public List<ProgramaCentroCostoDocenteDTO> ObtenerProgramaTrabajoPares(int idProveedor)
        {
            try
            {
                List<ProgramaCentroCostoDocenteDTO> listaProgramas = new List<ProgramaCentroCostoDocenteDTO>();
                var _query = "Select IdPGeneralPadre,ProgramaGeneral,IdPEspecificoPadre, ProgramaEspecifico,SUM(CASE WHEN Calificado = 0  THEN 1 ELSE 0 END) as TareasPendientes from [ope].[V_DocenteAsignadoTrabajoPares] where Calica= @idProveedor group by IdPGeneralPadre,ProgramaGeneral,IdPEspecificoPadre,ProgramaEspecifico ";
                var programaDB = _dapper.QueryDapper(_query, new { idProveedor });
                if (!string.IsNullOrEmpty(programaDB) && !programaDB.Contains("[]") && !programaDB.Contains("null"))
                {
                    listaProgramas = JsonConvert.DeserializeObject<List<ProgramaCentroCostoDocenteDTO>>(programaDB);
                }
                return listaProgramas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Lourdes Priscila Pacsi Gamboa
        /// Fecha: 08/10/2021
        /// Version: 1.0
        /// <summary>
        /// Retorna lista de tareas de alumnos sin calificar
        /// </summary>
        /// <returns>Lista del tipo de Objeto ProgramaCentroCostoDocenteDTO </returns>
        public List<ProgramaCentroCostoDocenteDTO> tareasSinCalificarPorIdProveerdor(int idProveedor)
        {
            try
            {
                List<ProgramaCentroCostoDocenteDTO> listaProgramas = new List<ProgramaCentroCostoDocenteDTO>();
                var _query = "Select IdPGeneralPadre from [40.76.216.5].integraDB_PortalWeb.dbo.T_TareaEvaluacionTarea where Calica= @idProveedor group by IdPGeneralPadre,ProgramaGeneral,IdPEspecificoPadre,ProgramaEspecifico ";
                var programaDB = _dapper.QueryDapper(_query, new { idProveedor });
                if (!string.IsNullOrEmpty(programaDB) && !programaDB.Contains("[]") && !programaDB.Contains("null"))
                {
                    listaProgramas = JsonConvert.DeserializeObject<List<ProgramaCentroCostoDocenteDTO>>(programaDB);
                }
                return listaProgramas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Lourdes Priscila Pacsi Gamboa
        /// Fecha: 06/07/2021
        /// Version: 1.0
        /// <summary>
        /// Retorna la lista de los alumnos que fueron asignados al docente para la revision del trabajo de pares
        /// </summary>
        /// <returns>Lista del tipo de Objeto ProgramaCentroCostoDocenteDTO </returns>
        public List<AlumnoDocenteTrabajoParesDTO> ObtenerAlumnoTrabajoPares(int idProveedor, int IdPEspecifico)
        {
            try
            {
                List<AlumnoDocenteTrabajoParesDTO> listaAlumno = new List<AlumnoDocenteTrabajoParesDTO>();
                var _query = "Select Alumno,IdAlumno,IdPEspecificoHijo,IdPGeneralHijo,IdPGeneralPadre,ProgramaGeneral,IdEvaluacion,Calificado from [ope].[V_DocenteAsignadoTrabajoPares] where Calica= @idProveedor and IdPEspecificoPadre =@IdPEspecifico";
                var programaDB = _dapper.QueryDapper(_query, new { idProveedor, IdPEspecifico });
                if (!string.IsNullOrEmpty(programaDB) && !programaDB.Contains("[]") && !programaDB.Contains("null"))
                {
                    listaAlumno = JsonConvert.DeserializeObject<List<AlumnoDocenteTrabajoParesDTO>>(programaDB);
                }
                return listaAlumno;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }   
}