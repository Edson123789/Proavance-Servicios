using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    /// Repositorio: AsesorChatDetalleRepositorio
    /// Autor: Gian Miranda
    /// Fecha: 15/04/2021
    /// <summary>
    /// Gestion con la base de datos de la tabla com.T_AsesorChatDetalle
    /// </summary>
    public class AsesorChatDetalleRepositorio : BaseRepository<TAsesorChatDetalle, AsesorChatDetalleBO>
    {
        #region Metodos Base
        public AsesorChatDetalleRepositorio() : base()
        {
        }
        public AsesorChatDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsesorChatDetalleBO> GetBy(Expression<Func<TAsesorChatDetalle, bool>> filter)
        {
            IEnumerable<TAsesorChatDetalle> listado = base.GetBy(filter);
            List<AsesorChatDetalleBO> listadoBO = new List<AsesorChatDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                AsesorChatDetalleBO objetoBO = Mapper.Map<TAsesorChatDetalle, AsesorChatDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsesorChatDetalleBO FirstById(int id)
        {
            try
            {
                TAsesorChatDetalle entidad = base.FirstById(id);
                AsesorChatDetalleBO objetoBO = new AsesorChatDetalleBO();
                Mapper.Map<TAsesorChatDetalle, AsesorChatDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsesorChatDetalleBO FirstBy(Expression<Func<TAsesorChatDetalle, bool>> filter)
        {
            try
            {
                TAsesorChatDetalle entidad = base.FirstBy(filter);
                AsesorChatDetalleBO objetoBO = Mapper.Map<TAsesorChatDetalle, AsesorChatDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AsesorChatDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAsesorChatDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AsesorChatDetalleBO> listadoBO)
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

        public bool Update(AsesorChatDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAsesorChatDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AsesorChatDetalleBO> listadoBO)
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
        private void AsignacionId(TAsesorChatDetalle entidad, AsesorChatDetalleBO objetoBO)
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

        private TAsesorChatDetalle MapeoEntidad(AsesorChatDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsesorChatDetalle entidad = new TAsesorChatDetalle();
                entidad = Mapper.Map<AsesorChatDetalleBO, TAsesorChatDetalle>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<AsesorChatDetalleBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TAsesorChatDetalle, bool>>> filters, Expression<Func<TAsesorChatDetalle, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TAsesorChatDetalle> listado = base.GetFiltered(filters, orderBy, ascending);
            List<AsesorChatDetalleBO> listadoBO = new List<AsesorChatDetalleBO>();

            foreach (var itemEntidad in listado)
            {
                AsesorChatDetalleBO objetoBO = Mapper.Map<TAsesorChatDetalle, AsesorChatDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene la lista de pais asignados para un asesorChatDetalle
        /// </summary>
        /// <param name="idAsesorChat">Id del asesorChat (PK de la tabla com.T_AsesorChat)</param>
        /// <returns>Lista de objetos de clase IdDTO</returns>
        public List<IdDTO> ObtenerPaisesPorIdAsesorChat(int idAsesorChat)
        {
            try
            {
                List<IdDTO> asesorChats = new List<IdDTO>();
                var _query = "SELECT IdPais AS Id FROM com.V_TAsesorChatDetalle_ObtenerPaisAgrupadosPorAsesorChatDetalle WHERE Estado = 1 AND IdAsesorChat = @idAsesorChat GROUP BY IdPais";
                var asesorChatsDB = _dapper.QueryDapper(_query, new { idAsesorChat });

                if (!string.IsNullOrEmpty(asesorChatsDB) && !asesorChatsDB.Contains("[]"))
                {
                    asesorChats = JsonConvert.DeserializeObject<List<IdDTO>>(asesorChatsDB);
                }
                return asesorChats;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de programa generales asignados por asesorChatDetalle
        /// </summary>
        /// <param name="idAsesorChat">Id del asesorChat (PK de la tabla com.T_AsesorChat)</param>
        /// <returns>Lista de objetos de clase IdDTO</returns>
        public List<IdDTO> ObtenerProgramasGeneralesPorIdAsesorChat(int idAsesorChat)
        {
            try
            {
                List<IdDTO> asesorChats = new List<IdDTO>();
                var _query = "SELECT IdPGeneral AS Id FROM com.V_TAsesorChatDetalle_ObtenerProgramaGeneralAgrupadosPorAsesorChatDetalle WHERE estado = 1 AND IdAsesorChat = @idAsesorChat GROUP BY IdPGeneral";
                var asesorChatsDB = _dapper.QueryDapper(_query, new { idAsesorChat });
                if (!string.IsNullOrEmpty(asesorChatsDB) && !asesorChatsDB.Contains("[]"))
                {
                    asesorChats = JsonConvert.DeserializeObject<List<IdDTO>>(asesorChatsDB);
                }
                return asesorChats;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de area capacitacion asignados por asesorChatDetalle
        /// </summary>
        /// <param name="idAsesorChat">Id del asesorChat (PK de la tabla com.T_AsesorChat)</param>
        /// <returns>Lista de objetos de clase IdDTO</returns>
        public List<IdDTO> ObtenerAreasCapacitacionPorIdAsesorChat(int idAsesorChat)
        {
            try
            {
                List<IdDTO> asesorChats = new List<IdDTO>();
                var _query = "SELECT IdAreaCapacitacion AS Id FROM com.V_TAsesorChatDetalle_ObtenerAreaCapacitacionAgrupadosPorAsesorChatDetalle WHERE Estado = 1 AND IdAsesorChat = @idAsesorChat GROUP BY IdAreaCapacitacion";
                var asesorChatsDB = _dapper.QueryDapper(_query, new { idAsesorChat });
                if (!string.IsNullOrEmpty(asesorChatsDB) && !asesorChatsDB.Contains("[]"))
                {
                    asesorChats = JsonConvert.DeserializeObject<List<IdDTO>>(asesorChatsDB);
                }
                return asesorChats;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de sub area capacitacion asignados por asesorChatDetalle
        /// </summary>
        /// <param name="idAsesorChat">Id del asesorChat (PK de la tabla com.T_AsesorChat)</param>
        /// <returns>Lista de objetos de clase IdDTO</returns>
        public List<IdDTO> ObtenerSubAreasCapacitacionPorIdAsesorChat(int idAsesorChat)
        {
            List<IdDTO> asesorChats = new List<IdDTO>();
            var _query = "SELECT IdSubAreaCapacitacion AS Id FROM com.V_TAsesorChatDetalle_ObtenerSubAreaCapacitacionAgrupadosPorAsesorChatDetalle where Estado = 1 and  IdAsesorChat = @idAsesorChat GROUP BY IdSubAreaCapacitacion";
            var asesorChatsDB = _dapper.QueryDapper(_query, new { idAsesorChat });
            if (!string.IsNullOrEmpty(asesorChatsDB) && !asesorChatsDB.Contains("[]"))
            {
                asesorChats = JsonConvert.DeserializeObject<List<IdDTO>>(asesorChatsDB);
            }
            return asesorChats;
        }

        /// <summary>
        /// Actualizar todos los idAsesorChat que coincidan con el los paises y programas generales enviados
        /// </summary>
        /// <param name="idAsesorChat"></param>
        /// <param name="usuario"></param>
        /// <param name="listaProgramas"></param>
        /// <param name="listaPaises"></param>
        /// <returns></returns>
        public void ActualizarAsesorChatPorAsesorChatPaisProgramaGeneral(int idAsesorChat, string usuario, string listaProgramas, string listaPaises)
        {
            try
            {
                List<AsesorChatDetalleBO> asesorChats = new List<AsesorChatDetalleBO>();
                _dapper.QuerySPDapper("com.SP_ActualizarAsesoresChatInsertarChatHistorialAsesor", new { idAsesorChat, usuario, listaProgramas, listaPaises });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Actualizar el asesor chat detalle e inserta un log en la tabla com.T_ChatIntegraHistorialAsesor
        /// </summary>
        /// <param name="idAsesorChat">Id del asesor chat enlazado a la configuracion (PK de la tabla com.T_AsesorChat)</param>
        /// <param name="idPersonal">Id del personal asignado a la configuracion (PK de la tabla gp.T_Personal)</param>
        /// <param name="usuario">Cadena con el usuario que ejecuto la actualizacion</param>
        /// <param name="listaProgramas">Lista de indices de los programas generales que estan habilitados para esa configuracion (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="listaPaises">Lista de indices de los paises que estan habilitados para esa configuracion (PK de la tabla conf.T_Pais)</param>
        /// <returns></returns>
        public void ActualizarAsesorChaDetalleYLog(int idAsesorChat, int idPersonal, string usuario, string listaProgramas, string listaPaises)
        {
            try
            {
                _dapper.QuerySPDapper("com.SP_ActualizarAsesorChatDetalle_Log", new { idAsesorChat, idPersonal, usuario, listaProgramas, listaPaises });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Actualiza a 0 el idAsesorChat en la tabla com.T_AsesorChatDetalle
        /// </summary>
        /// <param name="idAsesorChat">Id del asesorchat (PK de la tabla com.T_AsesorChat)</param>
        /// <param name="nombreUsuario">Nombre del usuario que ejecuta el eliminado</param>
        /// <returns></returns>
        public void EliminarAsesorChatDetalle(int idAsesorChat, string nombreUsuario) {
            try
            {
                _dapper.QuerySPDapper("com.SP_EliminarAsesorChatDetalle", new { idAsesorChat , nombreUsuario });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		/// <summary>
		/// Obtiene un AsesorChatDetalle filtrado por pais y programa general
		/// </summary>
		/// <param name="idPais"></param>
		/// <param name="idProgramaGeneral"></param>
		/// <returns></returns>
		public AsesorChatDetallePaisDTO ObtenerAsesorChatDetallePorPaisyProgramaGeneral(int idPais, int idProgramaGeneral)
		{
			try
			{

				AsesorChatDetallePaisDTO asesorChatDetalleBO = new AsesorChatDetallePaisDTO();
				var _query = string.Empty;
				_query = "SELECT Id, IdAsesorChat, IdPais, IdPGeneral, NombreAsesor FROM com.V_TAsesorChatDetalle_ObtenerParaValidarPorChatSignalR WHERE Estado = 1  AND IdPais = @idPais and IdPGeneral = @idProgramaGeneral";
				var asesorChatDetalleDB = _dapper.FirstOrDefault(_query, new { idPais, idProgramaGeneral });
				asesorChatDetalleBO = JsonConvert.DeserializeObject<AsesorChatDetallePaisDTO>(asesorChatDetalleDB);
				return asesorChatDetalleBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        /// Autor: Jose Villena
        /// Fecha: 05-25-2021
        /// Version: 1.0
        /// <summary>
        /// Obtener Id personal Soporte
        /// </summary>        
        /// <returns>Objeto: List<IdPersonalSoporteChatDTO></returns>
        public List<IdPersonalSoporteChatDTO> ObtenerIdPersonalSoporte()
        {
            try
            {

                List<IdPersonalSoporteChatDTO> asesorIdChatDetalle = new List<IdPersonalSoporteChatDTO>();
                var query = string.Empty;
                query = "SELECT IdPersonal,PorDefecto,IdPGeneral FROM ope.T_PersonalChatSoporte WHERE Estado=1";
                var asesorChatDetalleDB = _dapper.QueryDapper(query,null);
                asesorIdChatDetalle = JsonConvert.DeserializeObject<List<IdPersonalSoporteChatDTO>>(asesorChatDetalleDB);
                return asesorIdChatDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 05-25-2021
        /// Version: 1.0
        /// <summary>
        /// Obtener Id alumno para chat Soporte
        /// </summary>        
        /// <param name="correo"> Correo Alumno </param>
        /// <returns>Objeto: IdAlumnoSoporteChatDTO</returns>
        public IdAlumnoSoporteChatDTO ObtenerIdAlumnoChatSoporte(string correo)
        {
            try
            {
                IdAlumnoSoporteChatDTO idAlumnoChatSoporte = new IdAlumnoSoporteChatDTO();
                var idAlumnoChatSoporteDB = _dapper.QuerySPDapper("[pla].[SP_ObtenerIdAlumnoChatSoporte]", new { Correo = correo });
                idAlumnoChatSoporteDB = idAlumnoChatSoporteDB.Replace("[", "").Replace("]", "");

                if (!string.IsNullOrEmpty(idAlumnoChatSoporteDB) && !idAlumnoChatSoporteDB.Contains("[]"))
                {

                    idAlumnoChatSoporte = JsonConvert.DeserializeObject<IdAlumnoSoporteChatDTO>(idAlumnoChatSoporteDB);
                }
                return idAlumnoChatSoporte;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de programas asignados por asesor
        /// </summary>
        /// <param name="idPersonal"></param>
        public List<AsesorChatPGeneralDTO> ObtenerListaProgramasAsignadosPorAsesor(int idPersonal)
		{
			try
			{
				List<AsesorChatPGeneralDTO> asesorChats = new List<AsesorChatPGeneralDTO>();
				string _query = string.Empty;
				_query = "SELECT Id, IdPais, IdPGeneral FROM com.V_ObtenerProgramasGeneralesAsesor WHERE Estado = 1 and IdPersonal = @idPersonal";
				var asesorChatsDB = _dapper.QueryDapper(_query, new { idPersonal });
				asesorChats = JsonConvert.DeserializeObject<List<AsesorChatPGeneralDTO>>(asesorChatsDB);
				return asesorChats;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        public AsesorAsociadoCentroCostoMessengerDTO ObtenerAsesoresPorCentroCostoMessenger(int IdCentroCosto)
        {
            try
            {
                string _queryPrograma = "Select IdPersonal, IdCentroCosto, Area From [com].[V_ObtenerChatAsignadosParaFacebookMessenger] Where IdCentroCosto=@IdCentroCosto";
                var queryPrograma = _dapper.FirstOrDefault(_queryPrograma, new { IdCentroCosto });
                var asesores = JsonConvert.DeserializeObject<AsesorAsociadoCentroCostoMessengerDTO>(queryPrograma);

                return asesores;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
