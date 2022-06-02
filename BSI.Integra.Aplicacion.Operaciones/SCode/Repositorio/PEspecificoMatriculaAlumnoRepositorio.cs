using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    /// Repositorio: PEspecificoMatriculaAlumno
    /// Autor: Jose Villena
    /// Fecha: 03/05/2021
    /// <summary>
    /// Repositorio para consultas de ope.T_PEspecificoMatriculaAlumno
    /// </summary>
    public class PEspecificoMatriculaAlumnoRepositorio : BaseRepository<TPespecificoMatriculaAlumno, PEspecificoMatriculaAlumnoBO>
    {
        #region Metodos Base
        public PEspecificoMatriculaAlumnoRepositorio() : base()
        {
        }
        public PEspecificoMatriculaAlumnoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PEspecificoMatriculaAlumnoBO> GetBy(Expression<Func<TPespecificoMatriculaAlumno, bool>> filter)
        {
            IEnumerable<TPespecificoMatriculaAlumno> listado = base.GetBy(filter);
            List<PEspecificoMatriculaAlumnoBO> listadoBO = new List<PEspecificoMatriculaAlumnoBO>();
            foreach (var itemEntidad in listado)
            {
                PEspecificoMatriculaAlumnoBO objetoBO = Mapper.Map<TPespecificoMatriculaAlumno, PEspecificoMatriculaAlumnoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PEspecificoMatriculaAlumnoBO FirstById(int id)
        {
            try
            {
                TPespecificoMatriculaAlumno entidad = base.FirstById(id);
                PEspecificoMatriculaAlumnoBO objetoBO = new PEspecificoMatriculaAlumnoBO();
                Mapper.Map<TPespecificoMatriculaAlumno, PEspecificoMatriculaAlumnoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PEspecificoMatriculaAlumnoBO FirstBy(Expression<Func<TPespecificoMatriculaAlumno, bool>> filter)
        {
            try
            {
                TPespecificoMatriculaAlumno entidad = base.FirstBy(filter);
                PEspecificoMatriculaAlumnoBO objetoBO = Mapper.Map<TPespecificoMatriculaAlumno, PEspecificoMatriculaAlumnoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PEspecificoMatriculaAlumnoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPespecificoMatriculaAlumno entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PEspecificoMatriculaAlumnoBO> listadoBO)
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

        public bool Update(PEspecificoMatriculaAlumnoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPespecificoMatriculaAlumno entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PEspecificoMatriculaAlumnoBO> listadoBO)
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
        private void AsignacionId(TPespecificoMatriculaAlumno entidad, PEspecificoMatriculaAlumnoBO objetoBO)
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

        private TPespecificoMatriculaAlumno MapeoEntidad(PEspecificoMatriculaAlumnoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPespecificoMatriculaAlumno entidad = new TPespecificoMatriculaAlumno();
                entidad = Mapper.Map<PEspecificoMatriculaAlumnoBO, TPespecificoMatriculaAlumno>(objetoBO,
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


        /// <summary>
        /// Obtiene los PEspecificos asociados a una matricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public List<PEspecificoMatriculaAlumnoAgendaDTO> ObtenerTodoFiltroAutoComplete(int idMatriculaCabecera)
        {
            try
            {
                List<PEspecificoMatriculaAlumnoAgendaDTO> pEspecificoMatriculaAlumnoAgendaDTOs = new List<PEspecificoMatriculaAlumnoAgendaDTO>();
                string _queryAlumnoFiltro = string.Empty;
                _queryAlumnoFiltro = "SELECT Id, IdPEspecifico, Nombre, Tipo, TipoMatricula FROM ope.V_ObtenerPEspecifico_MatriculaAlumno WHERE Estado = 1 AND IdMatriculaCabecera = @idMatriculaCabecera ";
                var lista = _dapper.QueryDapper(_queryAlumnoFiltro, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(lista) && !lista.Contains("[]"))
                {
                    pEspecificoMatriculaAlumnoAgendaDTOs = JsonConvert.DeserializeObject<List<PEspecificoMatriculaAlumnoAgendaDTO>>(lista);
                }
                return pEspecificoMatriculaAlumnoAgendaDTOs;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Actualiza a "Recuperacion en otra Modalidad"
        /// </summary>
        /// <param name="idMatriculaCabecera" "IdPEspecifico"></param>
        /// <returns></returns>
        public void ActualizacionTipoMatriculaPEspecifico(int IdPEspecifico, int IdMatriculaCabecera)
        {
            try
            {
                //List<PEspecificoMatriculaAlumnoAgendaDTO> pEspecificoMatriculaAlumnoAgendaDTOs = new List<PEspecificoMatriculaAlumnoAgendaDTO>();
                string _queryAlumnoFiltro = string.Empty;
                _queryAlumnoFiltro = "update ope.T_PEspecificoMatriculaAlumno set IdPEspecificoTipoMatricula = 4 where IdMatriculaCabecera=@IdMatriculaCabecera and IdPEspecifico = @IdPEspecifico ";
                var lista = _dapper.QueryDapper(_queryAlumnoFiltro, new { IdPEspecifico, IdMatriculaCabecera });              
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Inserta en la tabla TMatAlumnosMoodle de V3
        /// </summary>
        /// <param name="CodigoMatricula"></param>
        /// <param name="IdAlumno"></param>
        /// <param name="IdMatriculaM"></param>
        /// <param name="IdUsuario"></param>
        /// <param name="IdCursoMoodle"></param>
        /// <param name="UsuarioCreacion"></param>
        /// <returns></returns>
        /// 

        /// Autor: Jose Villena
        /// Fecha: 22/03/2021
        /// Version: 1.0
        /// <summary>
        /// Inserta en la tabla TMatAlumnosMoodle de V3
        /// </summary>
        /// <param name="codigoMatricula">Codigo Matricula</param>
        /// <param name="idAlumno">Id del alumno</param>
        /// <param name="idMatriculaM">Id Matricula Moodle</param>
        /// <param name="idUsuario">Id Usuario Moodle</param>
        /// <param name="idCursoMoodle">Id Curso Moodle</param>
        /// <param name="usuarioCreacion">Usuario de Creacion</param>       
        /// <returns>Bool:true / false</returns>
        public bool InsertarTMatAlumnosMoodle(string codigoMatricula, int idAlumno, int idMatriculaM, int idUsuario, int idCursoMoodle, string usuarioCreacion)
        {
            try
            {
                var resultado = new Dictionary<string, bool>();

                string query = _dapper.QuerySPFirstOrDefault("ope.SP_InsertarMatAlumnosMoodle", new { codigoMatricula, idAlumno, idMatriculaM, idUsuario, idCursoMoodle, usuarioCreacion });
                if (!string.IsNullOrEmpty(query))
                {
                    resultado = JsonConvert.DeserializeObject<Dictionary<string, bool>>(query);
                }
                return resultado.Select(x => x.Value).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Inserta en la tabla TmpMatriculasMoodle de V3
        /// </summary>
        /// <param name="idUsuarioMoodle">Id Usuario Moodle</param>
        /// <param name="fechaInicioMatricula">Fecha Inicio Matricula</param>
        /// <param name="fechaFinMatricula">Fecha Fin Matricula</param>
        /// <param name="estadoMatricula">Estado matricula</param>
        /// <param name="idCursoMoodle">Id Curso Moodle</param>
        /// <param name="idEnRol">Id en Rol</param>
        /// <param name="idMatriculaMoodle">Id Matricula Moodle</param>
        /// <returns>Resultado: Dictionary<string, bool></returns>
        public bool InsertarTmpMatriculasMoodle(int idUsuarioMoodle, DateTime fechaInicioMatricula, DateTime fechaFinMatricula, bool estadoMatricula, int idCursoMoodle, int idEnRol, int idMatriculaMoodle)
		{
			try
			{
				var resultado = new Dictionary<string, bool>();

				string query = _dapper.QuerySPFirstOrDefault("ope.SP_InsertarTmpMatriculasMoodle", new { idUsuarioMoodle, fechaInicioMatricula, fechaFinMatricula, estadoMatricula, idCursoMoodle, idEnRol, idMatriculaMoodle });
				if (!string.IsNullOrEmpty(query))
				{
					resultado = JsonConvert.DeserializeObject<Dictionary<string, bool>>(query);
				}
				return resultado.Select(x => x.Value).FirstOrDefault();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		/// <summary>
		/// Actualiza en la tabla TmpMatriculasMoodle de V3
		/// </summary>
		/// <param name="idUsuarioMoodle">Id Usuario Moodle</param>
		/// <param name="fechaInicioMatricula">Fecha Inicio Matricula</param>
		/// <param name="fechaFinMatricula">Fecha Fin Matricula</param>
		/// <param name="estadoMatricula">Estado Matricula</param>
		/// <param name="idCursoMoodle">Id Curso Moodle</param>
		/// <param name="idEnRol">Id en Rol</param>
		/// <param name="idMatriculaMoodle">Id Matricula Moodle</param>
		/// <returns>Retorna Bool: True/False</returns>
		public bool ActualizarTmpMatriculasMoodle(int idUsuarioMoodle, DateTime fechaInicioMatricula, DateTime fechaFinMatricula, bool estadoMatricula, int idCursoMoodle, int idEnRol, int idMatriculaMoodle)
		{
			try
			{
				var resultado = new Dictionary<string, bool>();

				string query = _dapper.QuerySPFirstOrDefault("ope.SP_ActualizarTmpMatriculasMoodle", new { idUsuarioMoodle, fechaInicioMatricula, fechaFinMatricula, estadoMatricula, idCursoMoodle, idEnRol, idMatriculaMoodle });
				if (!string.IsNullOrEmpty(query))
				{
					resultado = JsonConvert.DeserializeObject<Dictionary<string, bool>>(query);
				}
				return resultado.Select(x => x.Value).FirstOrDefault();
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}

		}
        /// <summary>
        /// Elimina en la tabla TmpMatriculasMoodle de V3
        /// </summary>
        /// <param name="idMatriculaMoodle">Id Matricula Moodle</param>         
        /// <returns>Retorna Resultado: Dictionary<string, bool></returns>
        public bool EliminarTmpMatriculasMoodle(int idMatriculaMoodle)
		{
			try
			{
				var resultado = new Dictionary<string, bool>();

				string query = _dapper.QuerySPFirstOrDefault("ope.SP_EliminarTmpMatriculasMoodle", new { idMatriculaMoodle });
				if (!string.IsNullOrEmpty(query))
				{
					resultado = JsonConvert.DeserializeObject<Dictionary<string, bool>>(query);
				}
				return resultado.Select(x => x.Value).FirstOrDefault();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}

		/// <summary>
		/// Insertar en t_alumno moodle v3
		/// </summary>
		/// <param name="IdAlumnoIntegra"></param>
		/// <param name="IdAlumnoMoodle"></param>
		/// <param name="UsuarioMoodle"></param>
		/// <param name="PasswordMoodle"></param>
		/// <returns></returns>
		public bool InsertarTAlumno_Moodle(int IdAlumnoIntegra, string IdAlumnoMoodle, string UsuarioMoodle, string PasswordMoodle)
		{
			try
			{
				var resultado = new Dictionary<string, bool>();

				string query = _dapper.QuerySPFirstOrDefault("ope.SP_InsertarTAlumno_Moodle", new { IdAlumnoIntegra, IdAlumnoMoodle, UsuarioMoodle, PasswordMoodle });
				if (!string.IsNullOrEmpty(query))
				{
					resultado = JsonConvert.DeserializeObject<Dictionary<string, bool>>(query);
				}
				return resultado.Select(x => x.Value).FirstOrDefault();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}

        /// Autor: ,Jose Villena
        /// Fecha: 22/03/2021
        /// Version: 1.0
        /// <summary>
        /// Elimina en la tabla TMatAlumnosMoodle de V3
        /// </summary>
        /// <param name="idMatriculaMoodle">Id Matricula Moodle</param> 
        /// <returns>true / false</returns>
        public bool EliminarTMatAlumnosMoodle(int idMatriculaMoodle)
		{
			try
			{
				var resultado = new Dictionary<string, bool>();
				string query = _dapper.QuerySPFirstOrDefault("ope.SP_EliminarPruebaMigracion", new { idMatriculaMoodle });
				if (!string.IsNullOrEmpty(query))
				{
					resultado = JsonConvert.DeserializeObject<Dictionary<string, bool>>(query);
				}
				return resultado.Select(x => x.Value).FirstOrDefault();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        public List<PespecificoPadrePespecificoHijoDTO> ListaPespecificoPadrePespecificoHijo(int idPEspecifico)
        {
            try
            {
                List<PespecificoPadrePespecificoHijoDTO> listaPespecificoPadrePesepcificoHijo = new List<PespecificoPadrePespecificoHijoDTO>();
                var _query = "select Id, PEspecificoPadreId, PEspecificoHijoId from pla.T_PEspecificoPadrePEspecificoHijo where PEspecificoPadreId = @idPEspecifico and Estado = 1";
                var lista = _dapper.QueryDapper(_query, new { idPEspecifico });
                if (!string.IsNullOrEmpty(lista) && !lista.Contains("[]"))
                {
                    listaPespecificoPadrePesepcificoHijo = JsonConvert.DeserializeObject<List<PespecificoPadrePespecificoHijoDTO>>(lista);
                }
                return listaPespecificoPadrePesepcificoHijo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int? IdCursoMoodle(int idEspecifico)
        {
            try
            {
                ValorIntNullDTO IdCursoMoodle = new ValorIntNullDTO();
                var _query = "select Valor from [pla].[V_PEspecificoIdCursoMoodle] where Id = @idEspecifico and Estado = 1";
                var id= _dapper.FirstOrDefault(_query, new { idEspecifico });
                if (!string.IsNullOrEmpty(id) && !id.Contains("null"))
                {
                    IdCursoMoodle = JsonConvert.DeserializeObject<ValorIntNullDTO>(id);
                }
                //IdCursoMoodle = JsonConvert.DeserializeObject<ValorIntNullDTO>(id);
                return IdCursoMoodle.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int? IdUsuarioMoodle(int idAlumno)
        {
            try
            {
                ValorIntNullDTO IdUsuarioMoodle = new ValorIntNullDTO(); 
                var _query = "select Valor from [ope].[V_Talumno_moodle_IdUsuarioMoodle] where id_alumno = @idAlumno";
                var id = _dapper.FirstOrDefault(_query, new { idAlumno });
                if (!string.IsNullOrEmpty(id) && !id.Contains("null"))
                {
                    IdUsuarioMoodle = JsonConvert.DeserializeObject<ValorIntNullDTO>(id);
                }
                //IdUsuarioMoodle = JsonConvert.DeserializeObject<ValorIntNullDTO>(id);                
                return IdUsuarioMoodle.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int? getIdPEspecificoPadre(int idEspecifico)
        {
            try
            {
                ValorIntNullDTO IdUsuarioMoodle = new ValorIntNullDTO();
                var _query = "select Valor from [pla].[V_ConseguirPEspecificoPadre] where PEspecificoHijoId = @idEspecifico";
                var id = _dapper.FirstOrDefault(_query, new { idEspecifico });
                if (!string.IsNullOrEmpty(id) && !id.Contains("null"))
                {
                    IdUsuarioMoodle = JsonConvert.DeserializeObject<ValorIntNullDTO>(id);
                }
                //IdUsuarioMoodle = JsonConvert.DeserializeObject<ValorIntNullDTO>(id);                
                return IdUsuarioMoodle.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// Autor: ,Jashin Salazar
        /// Fecha: 13/09/2021
        /// Version: 2.0
        /// <summary>
        /// Verifica que el curso exista en la nueva aula virtual
        /// </summary>
        /// <param name="idPEspecifico">Id Matricula Moodle</param> 
        /// <returns>true / false</returns>
        public bool ExisteNuevaAulaVirtual(int idPEspecifico)
        {
            try
            {
                var query = "SELECT Id FROM [pla].[V_TPEspecificoNuevoAulaVirtual_DataBasica] WHERE Id = @idPEspecifico";
                var resultado = _dapper.FirstOrDefault(query, new { idPEspecifico });

                return !string.IsNullOrEmpty(resultado) && !resultado.Contains("[]");
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
