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
    public class MoodleCursoRepositorio : BaseRepository<TMoodleCurso, MoodleCursoBO>
    {
        /// Repositorio: MoodleCurso
        /// Autor: Jose Villena
        /// Fecha: 01/05/2021
        /// <summary>
        /// Repositorio para consultas de T_MoodleCurso
        /// </summary>
        #region Metodos Base
        public MoodleCursoRepositorio() : base()
        {
        }
        public MoodleCursoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MoodleCursoBO> GetBy(Expression<Func<TMoodleCurso, bool>> filter)
        {
            IEnumerable<TMoodleCurso> listado = base.GetBy(filter);
            List<MoodleCursoBO> listadoBO = new List<MoodleCursoBO>();
            foreach (var itemEntidad in listado)
            {
                MoodleCursoBO objetoBO = Mapper.Map<TMoodleCurso, MoodleCursoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MoodleCursoBO FirstById(int id)
        {
            try
            {
                TMoodleCurso entidad = base.FirstById(id);
                MoodleCursoBO objetoBO = new MoodleCursoBO();
                Mapper.Map<TMoodleCurso, MoodleCursoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MoodleCursoBO FirstBy(Expression<Func<TMoodleCurso, bool>> filter)
        {
            try
            {
                TMoodleCurso entidad = base.FirstBy(filter);
                MoodleCursoBO objetoBO = Mapper.Map<TMoodleCurso, MoodleCursoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MoodleCursoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMoodleCurso entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MoodleCursoBO> listadoBO)
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

        public bool Update(MoodleCursoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMoodleCurso entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MoodleCursoBO> listadoBO)
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
        private void AsignacionId(TMoodleCurso entidad, MoodleCursoBO objetoBO)
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

        private TMoodleCurso MapeoEntidad(MoodleCursoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMoodleCurso entidad = new TMoodleCurso();
                entidad = Mapper.Map<MoodleCursoBO, TMoodleCurso>(objetoBO,
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


        /// 
        ///Repositorio: MoodleCurso
        ///Autor: Jose Villena
        ///Fecha: 01/05/2021
        /// <summary>
        /// Obtiene informacion de curso moodle mediante Id
        /// </summary>
        /// <param name="idMoodleCurso"> Id Moodle Curso </param>
        /// <returns>Información curso moodle: MoodleCursoDTO</returns>
        public MoodleCursoDTO ObtenerMoodleCurso(int idMoodleCurso)
		{
			try
			{
				var query = "SELECT Id, IdCategoria, IdCursoMoodle, NombreCursoMoodle FROM [ope].[V_TMoodleCurso_ObtenerCursos] WHERE IdCursoMoodle = @IdMoodleCurso";
				var res = _dapper.FirstOrDefault(query, new { IdMoodleCurso = idMoodleCurso });
				return JsonConvert.DeserializeObject<MoodleCursoDTO>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}


        ///Repositorio: Moodle Curso
        ///Autor: Jose Villena
        ///Fecha: 01/05/2021
        /// <summary>
        /// Obtiene informacion de curso moodle mediante IdCategoria
        /// </summary>
        /// <param name="idCategoria"> Id Categoria </param>
        /// <returns> Lista de Información de la categoría curso moodle: List<MoodleCursoDTO></returns>
        public List<MoodleCursoDTO> ObtenerMoodleCursosPorCategoria(int idCategoria)
		{
			try
			{
				var query = "SELECT Id, IdCategoria, IdCursoMoodle, NombreCursoMoodle FROM [ope].[V_TMoodleCurso_ObtenerCursos] WHERE IdCategoria = @IdCategoria ORDER BY NombreCursoMoodle ASC";
				var res = _dapper.QueryDapper(query, new { IdCategoria = idCategoria });
				return JsonConvert.DeserializeObject<List<MoodleCursoDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}


        ///Repositorio: MoodleCurso
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtiene informacion de la categoria de curso moodle mediante Id
        /// </summary>
        /// <param name="idCategoria"> Id Categoria </param>
        /// <returns> Categoría curso moodle: CategoriaMoodleCursoDTO </returns>
        public CategoriaMoodleCursoDTO ObtenerCategoriaMoodleCurso(int idCategoria)
		{
			try
			{
				var query = "SELECT Id, IdCategoria, NombreCategoria, TipoCategoria, AplicaProyecto FROM [ope].[V_TCategoriaMoodleTipos_ObtenerCategoriasCurso] WHERE IdCategoria = @IdCategoria";
				var res = _dapper.FirstOrDefault(query, new { IdCategoria = idCategoria });
				return JsonConvert.DeserializeObject<CategoriaMoodleCursoDTO>(res);

			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}


        ///Repositorio: MoodleCurso
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtiene matriculas segun idusuariomoodle
        /// </summary>
        /// <param name="idUsuarioMoodle"> Id Usuario Moodle </param>        
        /// <returns> Lista matrículas moodle: List<TmpMatriculasMoodleDTO></returns>
        public List<TmpMatriculasMoodleDTO> ObtenerDatosMatriculaMoodlePorIdUsuarioMoodle(long idUsuarioMoodle)
		{
			try
			{
				var query = "SELECT Id, IdUsuarioMoodle, FechaInicioMatricula, FechaFinMatricula, EstadoMatricula, IdCursoMoodle, IdEnRol, IdMatriculaMoodle FROM [ope].[V_TmpMatriculasMoodle] WHERE IdUsuarioMoodle = @IdUsuarioMoodle";
				var res = _dapper.QueryDapper(query, new { IdUsuarioMoodle = idUsuarioMoodle });
				return JsonConvert.DeserializeObject<List<TmpMatriculasMoodleDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// <summary>
		/// Obtiene matriculas segun matricula moodle
		/// </summary>
		/// <param name="idUsuarioMoodle"></param>
		/// <returns></returns>
		public List<TmpMatriculasMoodleDTO> ObtenerDatosMatriculaMoodlePorMatriculaMoodle(long idMatriculaMoodle)
		{
			try
			{
				var query = "SELECT Id, IdUsuarioMoodle, FechaInicioMatricula, FechaFinMatricula, EstadoMatricula, IdCursoMoodle, IdEnRol, IdMatriculaMoodle FROM [ope].[V_TmpMatriculasMoodle] WHERE IdMatriculaMoodle = @IdMatriculaMoodle";
				var res = _dapper.QueryDapper(query, new { IdMatriculaMoodle = idMatriculaMoodle });
				return JsonConvert.DeserializeObject<List<TmpMatriculasMoodleDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        ///Repositorio: MoodleCurso
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtiene curso moodle de la matrícula del alumno
        /// </summary>
        /// <param name="idMatriculaMoodle"> Id Matricula Moodle </param>
        /// <param name="codigoMatricula"> Codigo Matricula </param>
        /// <returns> Lista de cursos moodle: List<CursoMoodleDTO></returns>
        public List<CursoMoodleDTO> ObtenerCursoMoodlePorMatricula(int idMatriculaMoodle, string codigoMatricula)
		{
			try
			{
				List<CursoMoodleDTO> cursoMoodle = new List<CursoMoodleDTO>();
				var query = "Select CodigoMatricula,IdUsuario,IdCurso,NombreCurso, IdMatriculaMoodle From ope.V_ObtenerCursosPorMatricula Where IdMatriculaMoodle = @IdMatriculaMoodle AND CodigoMatricula = @CodigoMatricula";
				var cursoMoodleBD = _dapper.QueryDapper(query, new { IdMatriculaMoodle = idMatriculaMoodle, CodigoMatricula = codigoMatricula });
				if (!string.IsNullOrEmpty(cursoMoodleBD) && !cursoMoodleBD.Contains("[]"))
				{
					cursoMoodle = JsonConvert.DeserializeObject<List<CursoMoodleDTO>>(cursoMoodleBD);
				}
				return cursoMoodle;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}


        ///Repositorio: MoodleCurso
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtiene matriculas segun idalumno moodle y curso moodle
        /// </summary>
        /// <param name="idUsuarioMoodle"> Id Usuario Moodle </param>
        /// <param name="idCursoMoodle"> Id Curso Moodle </param>
        /// <returns> Lista de matrículas : List<TmpMatriculasMoodleDTO></returns>
        public List<TmpMatriculasMoodleDTO> ObtenerCursoMatriculadoPorCursoAlumnoMoodle(int idUsuarioMoodle, int idCursoMoodle)
		{
			try
			{
				var query = "SELECT Id, IdUsuarioMoodle, FechaInicioMatricula, FechaFinMatricula, EstadoMatricula, IdCursoMoodle, IdEnRol, IdMatriculaMoodle FROM [ope].[V_TmpMatriculasMoodle] WHERE IdUsuarioMoodle = @IdUsuarioMoodle AND IdCursoMoodle = @IdCursoMoodle ORDER BY FechaInicioMatricula DESC";
				var res = _dapper.QueryDapper(query, new { IdUsuarioMoodle = idUsuarioMoodle, IdCursoMoodle = idCursoMoodle });
				return JsonConvert.DeserializeObject<List<TmpMatriculasMoodleDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        /// <summary>
        /// Este método obtiene una lista de cursos Moodle registrados en la base de datos
        /// </summary>
        /// <returns></returns>
        public List<MoodleCursoDTO> ObtenerCursosMoodleRegistrado()
        {
            try
            {
                List<MoodleCursoDTO> listaMoodleCurso = new List<MoodleCursoDTO>();
                var query = "SELECT Id, IdCursoMoodle, NombreCategoria, NombreCursoMoodle FROM [ope].[V_TMoodleCurso_ObtenerCursos] WHERE Estado = 1";
                var res = _dapper.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    listaMoodleCurso = JsonConvert.DeserializeObject<List<MoodleCursoDTO>>(res);
                }
                return listaMoodleCurso;

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }        
    }
}
