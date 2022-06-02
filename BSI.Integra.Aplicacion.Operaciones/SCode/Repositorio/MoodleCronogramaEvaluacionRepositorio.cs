using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Operaciones;
using BSI.Integra.Aplicacion.Operaciones.SCode.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    /// Repositorio: MoodleCronogramaEvaluacion
    /// Autor: Jose Villena
    /// Fecha: 03/05/2021
    /// <summary>
    /// Repositorio para consultas de T_MoodleCronogramaEvaluacion
    /// </summary>
    public class MoodleCronogramaEvaluacionRepositorio : BaseRepository<TMoodleCronogramaEvaluacion, MoodleCronogramaEvaluacionBO>
    {
        #region Metodos Base
        public MoodleCronogramaEvaluacionRepositorio() : base()
        {
        }
        public MoodleCronogramaEvaluacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MoodleCronogramaEvaluacionBO> GetBy(Expression<Func<TMoodleCronogramaEvaluacion, bool>> filter)
        {
            IEnumerable<TMoodleCronogramaEvaluacion> listado = base.GetBy(filter);
            List<MoodleCronogramaEvaluacionBO> listadoBO = new List<MoodleCronogramaEvaluacionBO>();
            foreach (var itemEntidad in listado)
            {
                MoodleCronogramaEvaluacionBO objetoBO = Mapper.Map<TMoodleCronogramaEvaluacion, MoodleCronogramaEvaluacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MoodleCronogramaEvaluacionBO FirstById(int id)
        {
            try
            {
                TMoodleCronogramaEvaluacion entidad = base.FirstById(id);
                MoodleCronogramaEvaluacionBO objetoBO = new MoodleCronogramaEvaluacionBO();
                Mapper.Map<TMoodleCronogramaEvaluacion, MoodleCronogramaEvaluacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MoodleCronogramaEvaluacionBO FirstBy(Expression<Func<TMoodleCronogramaEvaluacion, bool>> filter)
        {
            try
            {
                TMoodleCronogramaEvaluacion entidad = base.FirstBy(filter);
                MoodleCronogramaEvaluacionBO objetoBO = Mapper.Map<TMoodleCronogramaEvaluacion, MoodleCronogramaEvaluacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MoodleCronogramaEvaluacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMoodleCronogramaEvaluacion entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Insert(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IEnumerable<MoodleCronogramaEvaluacionBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Insert(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(MoodleCronogramaEvaluacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMoodleCronogramaEvaluacion entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Update(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(IEnumerable<MoodleCronogramaEvaluacionBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Update(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void AsignacionId(TMoodleCronogramaEvaluacion entidad, MoodleCronogramaEvaluacionBO objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                    objetoBO.Id = entidad.Id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private TMoodleCronogramaEvaluacion MapeoEntidad(MoodleCronogramaEvaluacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMoodleCronogramaEvaluacion entidad = new TMoodleCronogramaEvaluacion();
                entidad = Mapper.Map<MoodleCronogramaEvaluacionBO, TMoodleCronogramaEvaluacion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion


        public RespuestaWebDTO CongelarCronograma(int idMatriculaCabecera)
        {
            try
            {
                var query = _dapper.QuerySPFirstOrDefault("ope.SP_CongelarCronogramaEvaluacion", new {idMatriculaCabecera = idMatriculaCabecera});
                
                var res = JsonConvert.DeserializeObject<RespuestaWebDTO>(query);

                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
                
        public RespuestaWebDTO ReprogramarCronograma(int idMatriculaCabecera, int idEvaluacionMoodle, int diasRecorrer, bool recorreCronograma)
        {
            try
            {
                var query = _dapper.QuerySPFirstOrDefault("ope.SP_ReprogramarCronogramaEvaluacion", new { idMatriculaCabecera = idMatriculaCabecera, idEvaluacionMoodle = idEvaluacionMoodle, diasRecorrer = diasRecorrer, recorreCronograma = recorreCronograma });

                var res = JsonConvert.DeserializeObject<RespuestaWebDTO>(query);

                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public RespuestaWebDTO CongelarCrongrogramaRecuperacionEnAonline(int idMatriculaCabecera, int idCursoMoodle, int idUsuarioMoodle, string usuario)
        {
            try
            {
                var query = _dapper.QuerySPFirstOrDefault("ope.SP_CongelarCronogramaEvaluacion_RecuperacionEnAonline",
                    new
                    {
                        idMatriculaCabecera = idMatriculaCabecera, idCursoMoodle = idCursoMoodle,
                        idUsuarioMoodle = idUsuarioMoodle, usuario = usuario
                    });

                var res = JsonConvert.DeserializeObject<RespuestaWebDTO>(query);

                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<CronogramaAutoEvaluacionV2DTO> ObtenerCronogramaAutoEvaluacion_Total(int idMatriculaCabecera)
        {
            try
            {
                string sql_query = "select * from ope.V_ObtenerCronogramaEvaluaciones_Total where IdMatriculaCabecera = @IdMatriculaCabecera Order by IdMatriculaCabecera, Version desc, Orden";
                var query = _dapper.QueryDapper(sql_query, new {IdMatriculaCabecera = idMatriculaCabecera});

                var res = JsonConvert.DeserializeObject<List<CronogramaAutoEvaluacionV2DTO>>(query);

                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: ,Jose Villena
        /// Fecha: 22/03/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene la ultima version del cronograma de Autoevaluaciones
        /// </summary>
        /// <param name="idMatriculaCabecera">Id Matricula Cabecera</param>     
        /// <returns>Lista de Autoevaluaciones</returns>
        public List<CronogramaAutoEvaluacionV2DTO> ObtenerCronogramaAutoEvaluacion_UltimaVersion(int idMatriculaCabecera)
        {
            try
            {
                string sql_query = "select * from ope.V_ObtenerCronogramaEvaluaciones_UltimaVersion where IdMatriculaCabecera = @IdMatriculaCabecera Order by IdMatriculaCabecera, Version desc, Orden";
                var query = _dapper.QueryDapper(sql_query, new { IdMatriculaCabecera = idMatriculaCabecera });

                var res = JsonConvert.DeserializeObject<List<CronogramaAutoEvaluacionV2DTO>>(query);

                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<CronogramalistaCursosOonlineV2PromedioDTO> ObtenerCronogramaAutoEvaluacion_UltimaVersionPromedio(int idMatriculaCabecera)
        {
            try
            {
                string sql_query = "select * from ope.V_ObtenerCronogramaEvaluaciones_UltimaVersionPromedio where IdMatriculaCabecera = @IdMatriculaCabecera";
                var query = _dapper.QueryDapper(sql_query, new { IdMatriculaCabecera = idMatriculaCabecera });

                var res = JsonConvert.DeserializeObject<List<CronogramalistaCursosOonlineV2PromedioDTO>>(query);

                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<CronogramaAutoEvaluacionV2DTO> ObtenerCronogramaAutoEvaluacion_UltimaVersionPortal(int IdAlumno)
        {
            try
            {
                string sql_query = "select * from ope.V_ObtenerCronogramaEvaluaciones_UltimaVersion_Portal where IdAlumno = @IdAlumno Order by IdMatriculaCabecera, Version desc, Orden";
                var query = _dapper.QueryDapper(sql_query, new { IdAlumno = IdAlumno });

                var res = JsonConvert.DeserializeObject<List<CronogramaAutoEvaluacionV2DTO>>(query);

                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<CronogramaAutoEvaluacionV2DTO> ObtenerCronogramaAutoEvaluacion_UltimaVersionPorCurso(int idMatriculaCabecera, int idCursoMoodle)
        {
            try
            {
                string sql_query = "select * from ope.V_ObtenerCronogramaEvaluaciones_UltimaVersion where IdMatriculaCabecera = @IdMatriculaCabecera and IdCursoMoodle = @IdCursoMoodle Order by IdMatriculaCabecera, Version desc, Orden";
                var query = _dapper.QueryDapper(sql_query, new { IdMatriculaCabecera = idMatriculaCabecera, IdCursoMoodle  = idCursoMoodle });

                var res = JsonConvert.DeserializeObject<List<CronogramaAutoEvaluacionV2DTO>>(query);

                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<CronogramaAutoEvaluacionV2DTO> ObtenerCronogramaAutoEvaluacion_VersionOriginal(int idMatriculaCabecera)
        {
            try
            {
                string sql_query = "select * from ope.V_ObtenerCronogramaEvaluaciones_Total where IdMatriculaCabecera = @IdMatriculaCabecera and Version = 0 Order by IdMatriculaCabecera, Version desc, Orden";
                var query = _dapper.QueryDapper(sql_query, new { IdMatriculaCabecera = idMatriculaCabecera });

                var res = JsonConvert.DeserializeObject<List<CronogramaAutoEvaluacionV2DTO>>(query);

                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 22/03/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de versiones del cronograma de autoevaluaciones
        /// </summary>
        /// <param name="idMatriculaCabecera">Id Matricula Cebecera</param     
        /// <returns>Lista versiones de Autoevaluaciones</returns>
        public List<VersionCronogramaAutoEvaluacionDTO> ObtenerVersionesCronograma(int idMatriculaCabecera)
        {
            try
            {
                string sql_query = "select IdMatriculaCabecera, Version from ope.V_ObtenerCronogramaEvaluaciones_Total where IdMatriculaCabecera = @IdMatriculaCabecera group by IdMatriculaCabecera, Version Order by 1, 2";
                var query = _dapper.QueryDapper(sql_query, new { IdMatriculaCabecera = idMatriculaCabecera });

                var res = JsonConvert.DeserializeObject<List<VersionCronogramaAutoEvaluacionDTO>>(query);

                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<CronogramaAutoEvaluacionV2DTO> ObtenerCronogramaAutoEvaluacion_PorVersion(int idMatriculaCabecera, int version)
        {
            try
            {
                string sql_query = "select * from ope.V_ObtenerCronogramaEvaluaciones_Total where IdMatriculaCabecera = @IdMatriculaCabecera and Version = @Version Order by 1";
                var query = _dapper.QueryDapper(sql_query, new { IdMatriculaCabecera = idMatriculaCabecera, Version = version });

                var res = JsonConvert.DeserializeObject<List<CronogramaAutoEvaluacionV2DTO>>(query);

                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<CronogramaAutoEvaluacionV2DTO> ObtenerMatriculasSinCronograma()
        {
            try
            {
                string sql_query = "select IdMatriculaCabecera from ope.V_ObtenerMatriculas_SinCronogramaCongelado";
                var query = _dapper.QueryDapper(sql_query, null);

                var res = JsonConvert.DeserializeObject<List<CronogramaAutoEvaluacionV2DTO>>(query);
                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<IdentificadorCursoMoodlePorMatriculaComboDTO> ObtenerComboCursosMoodlePorMatricula(int idMatriculaCabecera)
        {
            try
            {
                string sql_query = "SELECT IdCursoMoodle, IdUsuarioMoodle, NombreCurso, IdMatriculaCabecera, IdOportunidad FROM ope.V_ObtenerIdentificadoresCursoMoodlePorMatricula WHERE IdMatriculaCabecera = @IdMatriculaCabecera";
                var query = _dapper.QueryDapper(sql_query, new { IdMatriculaCabecera = idMatriculaCabecera});

                var res = JsonConvert.DeserializeObject<List<IdentificadorCursoMoodlePorMatriculaComboDTO>>(query);
                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int? ObtenerIdCursoMoodlePrimeraActividadPendiente(int idMatriculaCabecera)
        {
            try
            {
                string sql_query = "SELECT TOP 1 IdCursoMoodle FROM ope.V_ObtenerCronogramaEvaluaciones_UltimaVersion WHERE Nota IS NULL AND IdMatriculaCabecera = @IdMatriculaCabecera ORDER BY Orden";
                var query = _dapper.FirstOrDefault(sql_query, new { IdMatriculaCabecera = idMatriculaCabecera });
                
                var res = JsonConvert.DeserializeObject<IdentificadorCursoMoodlePorMatriculaComboDTO>(query);
                return res?.IdCursoMoodle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 22/03/2021
        /// Version: 1.0
        /// <summary>
        /// Congela el cronograma segun el idMatriculaCabecera y FechaInicio
        /// </summary>
        /// <param name="idMatriculaCabecera">Id Matricula Cabecera</param>
        /// <param name="fechaInicio"> Fecha Inicio </param>       
        /// <returns>Respuesta del Procedimiento</returns>
        public RespuestaWebDTO CongelarCrongrogramaFechaEspecifica(int idMatriculaCabecera, DateTime fechaInicio)
        {
            try
            {
                var query = _dapper.QuerySPFirstOrDefault("ope.SP_CongelarCronogramaEvaluacion_EnFechaEspecifica",
                    new
                    {
                        idMatriculaCabecera = idMatriculaCabecera,
                        fechaInicio = fechaInicio
                    });

                var res = JsonConvert.DeserializeObject<RespuestaWebDTO>(query);

                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
