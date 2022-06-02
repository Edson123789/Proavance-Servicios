using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class RaAlumnoRepositorio : BaseRepository<TRaAlumno, RaAlumnoBO>
    {
        #region Metodos Base
        public RaAlumnoRepositorio() : base()
        {
        }
        public RaAlumnoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RaAlumnoBO> GetBy(Expression<Func<TRaAlumno, bool>> filter)
        {
            IEnumerable<TRaAlumno> listado = base.GetBy(filter);
            List<RaAlumnoBO> listadoBO = new List<RaAlumnoBO>();
            foreach (var itemEntidad in listado)
            {
                RaAlumnoBO objetoBO = Mapper.Map<TRaAlumno, RaAlumnoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RaAlumnoBO FirstById(int id)
        {
            try
            {
                TRaAlumno entidad = base.FirstById(id);
                RaAlumnoBO objetoBO = new RaAlumnoBO();
                Mapper.Map<TRaAlumno, RaAlumnoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RaAlumnoBO FirstBy(Expression<Func<TRaAlumno, bool>> filter)
        {
            try
            {
                TRaAlumno entidad = base.FirstBy(filter);
                RaAlumnoBO objetoBO = Mapper.Map<TRaAlumno, RaAlumnoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RaAlumnoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRaAlumno entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RaAlumnoBO> listadoBO)
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

        public bool Update(RaAlumnoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRaAlumno entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RaAlumnoBO> listadoBO)
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
        private void AsignacionId(TRaAlumno entidad, RaAlumnoBO objetoBO)
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

        private TRaAlumno MapeoEntidad(RaAlumnoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRaAlumno entidad = new TRaAlumno();
                entidad = Mapper.Map<RaAlumnoBO, TRaAlumno>(objetoBO,
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

        public List<AlumnoPresencialDTO> ListadoAlumnoRegistradoPorCentroCosto(string nombreCentroCosto)
        {
            try
            {
                List<AlumnoPresencialDTO> listadoAlumnoPresencial = new List<AlumnoPresencialDTO>();
                var query = "SELECT IdRaAlumno, CodigoAlumno, NombreAlumno, NombreCentrocosto, Nombre1, Nombre2, Email1, Email2, UsuarioCoordinadorAcademico, IdEstadomatricula, Estadomatricula, Genero FROM ope.V_ObtenerAlumnoRegistradoPorCentroCosto WHERE NombreCentrocosto = @nombreCentroCosto";
                var listadoAlumnoPresencialDB = _dapper.QueryDapper(query, new { nombreCentroCosto });
                if (!string.IsNullOrEmpty(listadoAlumnoPresencialDB) && !listadoAlumnoPresencialDB.Contains("[]"))
                {
                    listadoAlumnoPresencial = JsonConvert.DeserializeObject<List<AlumnoPresencialDTO>>(listadoAlumnoPresencialDB);
                }
                return listadoAlumnoPresencial;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<ReporteAsistenciaOnlineDTO> ListadoSesionesAlumnoCurso(int idAlumno)
        {
            try
            {
                List<ReporteAsistenciaOnlineTempDTO> listadoAsistenciaOnlineTemp = new List<ReporteAsistenciaOnlineTempDTO>();
                List<ReporteAsistenciaOnlineDTO> listadoAsistenciaOnline = new List<ReporteAsistenciaOnlineDTO>();
                var query = "SELECT NombreCentroCosto, NombreCurso, HoraInicio, NombreProgramaEspecifico, Asistio FROM ope.V_ObtenerListadoSesionesAlumnoCurso WHERE IdAlumno = @idAlumno AND activo = 1;";
                var listadoAsistenciaOnlineTempDB = _dapper.QueryDapper(query, new { idAlumno });
                if (!string.IsNullOrEmpty(listadoAsistenciaOnlineTempDB) && !listadoAsistenciaOnlineTempDB.Contains("[]"))
                {
                    listadoAsistenciaOnlineTemp = JsonConvert.DeserializeObject<List<ReporteAsistenciaOnlineTempDTO>>(listadoAsistenciaOnlineTempDB);
                }
                foreach (var item in listadoAsistenciaOnlineTemp)
                {
                    var reporteAsistencia = new ReporteAsistenciaOnlineDTO() {
                        NombreCentroCosto = item.NombreCentroCosto,
                        NombrePrograma = item.NombreProgramaEspecifico,
                        NombreCurso = item.NombreCurso,
                        FechaSesion = item.HoraInicio,
                        Asistio = item.Asistio == true ? "X" : ""
                    };
                    listadoAsistenciaOnline.Add(reporteAsistencia);
                }
                return listadoAsistenciaOnline;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene un listado de alumnos matriculados filtrado por codigoMatricula o Centro de costo
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public List<AlumnoDocumentacionDetalleDTO> BusquedaContieneCodigoMatriculaOCentroCosto(string texto)
        {
            try
            {
                List<AlumnoDocumentacionDetalleDTO> documentacionAlumno = new List<AlumnoDocumentacionDetalleDTO>();
                var query = "SELECT IdAlumno, CodigoAlumno, NombreCentrocosto, NombreAlumno FROM ope.V_AlumnosMatriculados WHERE CodigoAlumno LIKE CONCAT('%',@texto,'%') OR NombreCentrocosto LIKE CONCAT('%',@texto,'%')";
                var documentacionAlumnoDB = _dapper.QueryDapper(query, new { texto });
                if (!string.IsNullOrEmpty(documentacionAlumnoDB) && !documentacionAlumnoDB.Contains("[]"))
                {
                    documentacionAlumno = JsonConvert.DeserializeObject<List<AlumnoDocumentacionDetalleDTO>>(documentacionAlumnoDB);
                }
                return documentacionAlumno;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene un alumno matriculado por codigo matricula
        /// </summary>
        /// <param name="codigoAlumno"></param>
        /// <returns></returns>
        public AlumnoMatriculadoDatoDTO ObtenerAlumnoMatriculadoPorCodigoMatricula(string codigoAlumno) {
            try
            {
                AlumnoMatriculadoDatoDTO alumnoMatriculado = new AlumnoMatriculadoDatoDTO();
                var query = "SELECT CodigoAlumno, NombreAlumno FROM ope.V_AlumnosMatriculados WHERE CodigoAlumno = @codigoAlumno";
                var alumnoMatriculadoDB = _dapper.FirstOrDefault(query, new { codigoAlumno });
                if (!string.IsNullOrEmpty(alumnoMatriculadoDB))
                {
                    alumnoMatriculado = JsonConvert.DeserializeObject<AlumnoMatriculadoDatoDTO>(alumnoMatriculadoDB);
                }
                return alumnoMatriculado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene un alumno por codigo matricula
        /// </summary>
        /// <param name="codigoAlumno"></param>
        /// <returns></returns>
        public AlumnoDatoDTO ObtenerAlumnoPorCodigoMatricula(string codigoAlumno)
        {
            try
            {
                AlumnoDatoDTO alumnoMatriculado = new AlumnoDatoDTO();
                var query = "SELECT CodigoAlumno,  NombreCentrocosto, NombreAlumno FROM ope.V_AlumnosMatriculados WHERE CodigoAlumno = @codigoAlumno;";
                var alumnoMatriculadoDB = _dapper.FirstOrDefault(query, new { codigoAlumno });
                if (!string.IsNullOrEmpty(alumnoMatriculadoDB))
                {
                    alumnoMatriculado = JsonConvert.DeserializeObject<AlumnoDatoDTO>(alumnoMatriculadoDB);
                }
                return alumnoMatriculado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public AlumnoDatoDTO ObtenerUsuarioMoodlePorCodigoMatricula(string codigoAlumno)
        {
            try
            {
                AlumnoDatoDTO alumnoMatriculado = new AlumnoDatoDTO();
                var query = "SELECT CodigoAlumno,  NombreCentrocosto, NombreAlumno FROM ope.V_AlumnosMatriculados WHERE CodigoAlumno = @codigoAlumno;";
                var alumnoMatriculadoDB = _dapper.FirstOrDefault(query, new { codigoAlumno });
                if (!string.IsNullOrEmpty(alumnoMatriculadoDB))
                {
                    alumnoMatriculado = JsonConvert.DeserializeObject<AlumnoDatoDTO>(alumnoMatriculadoDB);
                }
                return alumnoMatriculado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el idUsuario moodle por codigo matricula
        /// </summary>
        /// <param name="codigoMatricula"></param>
        /// <returns></returns>
        public IdUsuarioMoodleDTO ObtenerIdUsuarioMoodlePorCodigoAlumno(string codigoMatricula) {
            try
            {
                IdUsuarioMoodleDTO IdUsuarioMoodle = new IdUsuarioMoodleDTO();
                var query = "SELECT IdUsuarioMoodle FROM ope.V_ObtenerMatriculaMoodle WHERE CodigoMatricula = @codigoMatricula AND IdUsuarioMoodle IS NOT NULL AND IdUsuarioMoodle <> 0;";
                var alumnoMatriculadoDB = _dapper.FirstOrDefault(query, new { codigoMatricula });
                if (!string.IsNullOrEmpty(alumnoMatriculadoDB))
                {
                    IdUsuarioMoodle = JsonConvert.DeserializeObject<IdUsuarioMoodleDTO>(alumnoMatriculadoDB);
                }
                return IdUsuarioMoodle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene las matriculas por codigo matricula y usuario moodle
        /// </summary>
        /// <param name="codigoMatricula"></param>
        /// <param name="idUsuarioMoodle"></param>
        /// <returns></returns>
        public List<MatriculaMoodleDTO> ObtenerMatriculasPorCodigoMatricula(string codigoMatricula, int idUsuarioMoodle)
        {
            try
            {
                List<MatriculaMoodleDTO> matriculasMoodle = new List<MatriculaMoodleDTO>();
                var query = "SELECT Id, CodigoMatricula, IdAlumnoIntegra, IdMatriculaMoodle, IdUsuarioMoodle, IdCursoMoodle FROM ope.V_ObtenerMatriculaMoodle WHERE CodigoMatricula = @codigoMatricula AND IdUsuarioMoodle = @idUsuarioMoodle AND (IdCursoMoodle IS NOT NULL  AND IdCursoMoodle <> 0) AND (IdMatriculaMoodle IS NOT NULL AND IdMatriculaMoodle <> 0)";
                var matriculasMoodleDB = _dapper.QueryDapper(query, new { codigoMatricula, idUsuarioMoodle });
                if (!string.IsNullOrEmpty(matriculasMoodleDB) && !matriculasMoodleDB.Contains("[]"))
                {
                    matriculasMoodle = JsonConvert.DeserializeObject<List<MatriculaMoodleDTO>>(matriculasMoodleDB);
                }
                return matriculasMoodle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
