using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
	public class WhatsAppMensajeEnviadoPostulanteRepositorio : BaseRepository<TWhatsAppMensajeEnviadoPostulante, WhatsAppMensajeEnviadoPostulanteBO>
	{
		#region Metodos Base
		public WhatsAppMensajeEnviadoPostulanteRepositorio() : base()
		{
		}
		public WhatsAppMensajeEnviadoPostulanteRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<WhatsAppMensajeEnviadoPostulanteBO> GetBy(Expression<Func<TWhatsAppMensajeEnviadoPostulante, bool>> filter)
		{
			IEnumerable<TWhatsAppMensajeEnviadoPostulante> listado = base.GetBy(filter);
			List<WhatsAppMensajeEnviadoPostulanteBO> listadoBO = new List<WhatsAppMensajeEnviadoPostulanteBO>();
			foreach (var itemEntidad in listado)
			{
				WhatsAppMensajeEnviadoPostulanteBO objetoBO = Mapper.Map<TWhatsAppMensajeEnviadoPostulante, WhatsAppMensajeEnviadoPostulanteBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public WhatsAppMensajeEnviadoPostulanteBO FirstById(int id)
		{
			try
			{
				TWhatsAppMensajeEnviadoPostulante entidad = base.FirstById(id);
				WhatsAppMensajeEnviadoPostulanteBO objetoBO = new WhatsAppMensajeEnviadoPostulanteBO();
				Mapper.Map<TWhatsAppMensajeEnviadoPostulante, WhatsAppMensajeEnviadoPostulanteBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public WhatsAppMensajeEnviadoPostulanteBO FirstBy(Expression<Func<TWhatsAppMensajeEnviadoPostulante, bool>> filter)
		{
			try
			{
				TWhatsAppMensajeEnviadoPostulante entidad = base.FirstBy(filter);
				WhatsAppMensajeEnviadoPostulanteBO objetoBO = Mapper.Map<TWhatsAppMensajeEnviadoPostulante, WhatsAppMensajeEnviadoPostulanteBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(WhatsAppMensajeEnviadoPostulanteBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TWhatsAppMensajeEnviadoPostulante entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<WhatsAppMensajeEnviadoPostulanteBO> listadoBO)
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

		public bool Update(WhatsAppMensajeEnviadoPostulanteBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TWhatsAppMensajeEnviadoPostulante entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<WhatsAppMensajeEnviadoPostulanteBO> listadoBO)
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
		private void AsignacionId(TWhatsAppMensajeEnviadoPostulante entidad, WhatsAppMensajeEnviadoPostulanteBO objetoBO)
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

		private TWhatsAppMensajeEnviadoPostulante MapeoEntidad(WhatsAppMensajeEnviadoPostulanteBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TWhatsAppMensajeEnviadoPostulante entidad = new TWhatsAppMensajeEnviadoPostulante();
				entidad = Mapper.Map<WhatsAppMensajeEnviadoPostulanteBO, TWhatsAppMensajeEnviadoPostulante>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		#endregion

		public List<WhatsAppContactoChatDTO> ChatsGeneradosContactos(int idPersonal)
		{
			try
			{
				List<WhatsAppContactoChatDTO> listaContactos = new List<WhatsAppContactoChatDTO>();
				var _query = string.Empty;
				_query = "SELECT Y.Id AS IdContacto, Y.Nombre1, Y.Nombre2, Y.ApellidoPaterno, Y.ApellidoMaterno, X.WaTo AS NumeroCelular, Y.IdCodigoPais " +
						 "FROM mkt.T_WhatsAppMensajeEnviadoPostulante AS X " +
						 "INNER JOIN mkt.T_Alumno AS Y ON X.IdPais=Y.IdPais AND X.IdAlumno=Y.Id " +
						 "WHERE X.IdPersonal=@idPersonal " +
						 "GROUP BY Y.Nombre1, Y.Nombre2, Y.ApellidoPaterno,Y.ApellidoMaterno,Y.Celular,X.WaTo " +
						 "ORDER BY x.FechaCreacion DESC";

				var CredencialTokenExpiraDB = _dapper.FirstOrDefault(_query, new { idPersonal });
				listaContactos = JsonConvert.DeserializeObject<List<WhatsAppContactoChatDTO>>(CredencialTokenExpiraDB);
				return listaContactos;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public List<WhatsAppMensajesPostulanteDTO> ListaUltimoMensajeChatsPostulante(int idPersonal)
		{
			try
			{
				List<WhatsAppMensajesPostulanteDTO> listaMensajes = new List<WhatsAppMensajesPostulanteDTO>();
				var _query = string.Empty;
				_query = @"
							SELECT wa.Numero, 
								   wa.Mensaje, 
								   wa.IdPersonal, 
								   wa.FechaCreacion, 
								   wa.IdPais, 
								   ISNULL(wa.IdPostulante, 0) IdPostulante,
								   CASE
									   WHEN pos.Nombre IS NULL
									   THEN wa.Numero
									   ELSE RTRIM(CONCAT(pos.Nombre, ' ', pos.ApellidoPaterno, ' ', pos.ApellidoMaterno))
								   END NombrePostulante
							FROM gp.V_UltimoChatWhatsAppPostulante wa
								 LEFT JOIN gp.T_Postulante pos ON wa.IdPostulante = pos.Id
								 LEFT JOIN conf.T_ClasificacionPersona cp ON cp.IdTablaOriginal = pos.Id and cp.IdTipoPersona = 5
							WHERE wa.IdPersonal = @idPersonal
							ORDER BY wa.FechaCreacion DESC;
						";

				var CredencialTokenExpiraDB = _dapper.QueryDapper(_query, new { idPersonal });
				listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppMensajesPostulanteDTO>>(CredencialTokenExpiraDB);
				return listaMensajes;
			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}
		}

		public List<WhatsAppMensajesDTO> ListaUltimoMensajeChatsRecibido(int idPersonal)
		{
			try
			{
				List<WhatsAppMensajesDTO> listaMensajes = new List<WhatsAppMensajesDTO>();
				var _query = string.Empty;
				_query = "SELECT wa.Numero, wa.Mensaje, wa.IdPersonal, wa.FechaCreacion, wa.IdPais, ISNULL(wa.IdAlumno,0) IdAlumno, CASE WHEN al.Nombre1 is null THEN wa.Numero ELSE CONCAT(al.Nombre1,' ',al.ApellidoPaterno) END NombreAlumno " +
						 "FROM mkt.V_UltimoChatWhatsAppContactoRecibido wa " +
						 "LEFT JOIN mkt.T_Alumno al on wa.IdAlumno=al.Id " +
						 "WHERE wa.IdPersonal=@idPersonal Order by wa.FechaCreacion Desc";

				var CredencialTokenExpiraDB = _dapper.QueryDapper(_query, new { idPersonal });
				listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppMensajesDTO>>(CredencialTokenExpiraDB);
				return listaMensajes;
			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene historial de chats de whatsapp de postulantes segun el idpersonal y numero telefonico
		/// </summary>
		/// <param name="idPersonal"></param>
		/// <param name="Numero"></param>
		/// <param name="Area"></param>
		/// <returns></returns>
		public List<WhatsAppMensajesPostulanteDTO> HistorialChatsRecibidoPostulante(int idPersonal, string Numero, string Area)
		{
			try
			{
				List<WhatsAppMensajesPostulanteDTO> listaMensajes = new List<WhatsAppMensajesPostulanteDTO>();
				var _query = string.Empty;

				_query = @"
									SELECT wa.Numero, 
										   wa.Mensaje, 
										   wa.IdPersonal, 
										   wa.FechaCreacion, 
										   wa.IdPais, 
										   ISNULL(wa.IdPostulante, 0) IdPostulante,
										   CASE
											   WHEN pos.Nombre IS NULL
											   THEN wa.Numero
											   ELSE RTRIM(CONCAT(pos.Nombre, ' ', pos.ApellidoPaterno, ' ', pos.ApellidoMaterno))
										   END NombrePostulante
									FROM gp.V_HistorialChatWhatsAppRecibidoPostulante wa
										 LEFT JOIN gp.T_Postulante pos ON wa.IdPostulate = pos.Id
									WHERE wa.IdPersonal = @idPersonal
										  AND wa.Numero = @Numero
									ORDER BY wa.FechaCreacion DESC;
								";
				
				var CredencialTokenExpiraDB = _dapper.QueryDapper(_query, new { idPersonal, Numero });
				listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppMensajesPostulanteDTO>>(CredencialTokenExpiraDB);
				return listaMensajes;
			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene el historial en base al tipo de agenda
		/// </summary>
		/// <param name="idPersonal"></param>
		/// <param name="Numero"></param>
		/// <param name="Area"></param>
		/// <param name="idTipoAgenda"></param>
		/// <returns></returns>
		public List<WhatsAppMensajesDTO> HistorialChatsRecibido(int idPersonal, string Numero, string Area, int idTipoAgenda)
		{
			try
			{
				List<WhatsAppMensajesDTO> listaMensajes = new List<WhatsAppMensajesDTO>();
				var _query = string.Empty;

				if (idTipoAgenda == 3)
				{
					_query = $@"
                                SELECT Numero, 
                                        Mensaje, 
                                        IdPersonal, 
                                        FechaCreacion, 
                                        IdPais, 
                                        IdAlumno, 
                                        NombreAlumno
                                FROM ope.V_ObtenerHistorialChatWhatsAppRecibidoDocente
                                WHERE Numero = @Numero
                                ORDER BY FechaCreacion DESC;
                                ";
				}
				else
				{
					if (Area == "VE")
					{
						_query = "SELECT wa.Numero, wa.Mensaje, wa.IdPersonal, wa.FechaCreacion, wa.IdPais, ISNULL(wa.IdAlumno,0) IdAlumno, CASE WHEN al.Nombre1 is null THEN wa.Numero ELSE CONCAT(al.Nombre1,' ',al.ApellidoPaterno) END NombreAlumno " +
							 "FROM mkt.V_HistorialChatWhatsAppRecibido wa " +
							 "LEFT JOIN mkt.T_Alumno al on wa.IdAlumno=al.Id " +
							 "WHERE wa.IdPersonal=@idPersonal and wa.Numero=@Numero Order by wa.FechaCreacion Desc";
					}
					else if (Area == "OP")
					{
						_query = "SELECT wa.Numero, wa.Mensaje, wa.IdPersonal, wa.FechaCreacion, wa.IdPais, ISNULL(wa.IdAlumno,0) IdAlumno, CASE WHEN al.Nombre1 is null THEN wa.Numero ELSE CONCAT(al.Nombre1,' ',al.ApellidoPaterno) END NombreAlumno " +
							 "FROM mkt.V_HistorialChatWhatsAppRecibido wa " +
							 "LEFT JOIN mkt.T_Alumno al on wa.IdAlumno=al.Id " +
							 "WHERE wa.Numero=@Numero Order by wa.FechaCreacion Desc";
					}
					else
					{
						_query = "SELECT wa.Numero, wa.Mensaje, wa.IdPersonal, wa.FechaCreacion, wa.IdPais, ISNULL(wa.IdAlumno,0) IdAlumno, CASE WHEN al.Nombre1 is null THEN wa.Numero ELSE CONCAT(al.Nombre1,' ',al.ApellidoPaterno) END NombreAlumno " +
							 "FROM mkt.V_HistorialChatWhatsAppRecibido wa " +
							 "LEFT JOIN mkt.T_Alumno al on wa.IdAlumno=al.Id " +
							 "WHERE wa.IdPersonal=@idPersonal and wa.Numero=@Numero Order by wa.FechaCreacion Desc";
					}
				}

				var CredencialTokenExpiraDB = _dapper.QueryDapper(_query, new { idPersonal, Numero });
				listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppMensajesDTO>>(CredencialTokenExpiraDB);
				return listaMensajes;
			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}
		}

		public List<WhatsAppMensajesDTO> ListaUltimoMensajeChatsEnviado(int idPersonal)
		{
			try
			{
				List<WhatsAppMensajesDTO> listaMensajes = new List<WhatsAppMensajesDTO>();
				var _query = string.Empty;
				_query = "SELECT wa.Numero, wa.Mensaje, wa.IdPersonal, wa.FechaCreacion, wa.IdPais, ISNULL(wa.IdAlumno,0) IdAlumno, CASE WHEN al.Nombre1 is null THEN wa.Numero ELSE CONCAT(al.Nombre1,' ',al.ApellidoPaterno) END NombreAlumno " +
						 "FROM mkt.V_UltimoChatWhatsAppContactoEnviado wa " +
						 "LEFT JOIN mkt.T_Alumno al on wa.IdAlumno=al.Id " +
						 "WHERE wa.IdPersonal=@idPersonal Order by wa.FechaCreacion Desc";

				var CredencialTokenExpiraDB = _dapper.QueryDapper(_query, new { idPersonal });
				listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppMensajesDTO>>(CredencialTokenExpiraDB);
				return listaMensajes;
			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene historial de chats de un postulante
		/// </summary>
		/// <param name="idPersonal"></param>
		/// <param name="Numero"></param>
		/// <param name="Area"></param>
		/// <returns></returns>
		public List<WhatsAppHistorialMensajesPostulanteDTO> ListaHistorialMensajeChatPostulante(int idPersonal, string Numero, string Area)
		{
			try
			{
				List<WhatsAppHistorialMensajesPostulanteDTO> listaMensajes = new List<WhatsAppHistorialMensajesPostulanteDTO>();
				var _query = string.Empty;

				if (idPersonal == 0)
				{
					_query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdPostulante,0) IdPostulante,IdPais, Registro, FechaCreacion, NombrePersonal " +
						 "FROM gp.V_HistorialChatWhatsAppPostulante WHERE Numero=@Numero Order by FechaCreacion Asc";
				}
				else
				{
					_query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdPostulante,0) IdPostulante,IdPais, Registro, FechaCreacion, NombrePersonal " +
						 "FROM gp.V_HistorialChatWhatsAppPostulante WHERE IdPersonal=@idPersonal AND Numero=@Numero Order by FechaCreacion Asc";

				}

				var CredencialTokenExpiraDB = _dapper.QueryDapper(_query, new { idPersonal, Numero });
				listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppHistorialMensajesPostulanteDTO>>(CredencialTokenExpiraDB);
				return listaMensajes;
			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}
		}

		public string ObtenerMensajeMultimedia(string WaId)
		{
			try
			{
				WhatsAppHistorialMensajesPostulanteDTO listaMensajes = new WhatsAppHistorialMensajesPostulanteDTO();
				var _query = string.Empty;

				_query = "SELECT Mensaje " +
						"FROM [gp].[V_HistorialChatWhatsAppPostulante] WHERE WaId=@WaId";


				var CredencialTokenExpiraDB = _dapper.FirstOrDefault(_query, new { WaId });
				listaMensajes = JsonConvert.DeserializeObject<WhatsAppHistorialMensajesPostulanteDTO>(CredencialTokenExpiraDB);
				return listaMensajes.Mensaje;
			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene el idpersonal y idpostulante por el numero de celular
		/// </summary>
		/// <param name="Numero"></param>
		/// <returns></returns>
		public PersonalPostulanteDTO ObtenerConversacionNumero(string Numero)
		{
			PersonalPostulanteDTO Conversacion = new PersonalPostulanteDTO();
			string _queryConversacion = @"								
											SELECT mw.IdPersonal, ISNULL(mw.IdPostulante, 0) IdPostulante FROM [gp].[V_TWhatsAppMensajeEnviadoPostulante_ObtenerAsesorPostulante] mw
												 INNER JOIN gp.T_Personal pe ON mw.IdPersonal = pe.Id
											WHERE pe.Activo = 1
												  AND mw.WaTo = @Numero
											ORDER BY mw.FechaCreacion DESC;
										";
			var queryConversacion = _dapper.FirstOrDefault(_queryConversacion, new { Numero });
			if (queryConversacion == null || queryConversacion == "" || queryConversacion == "null")
			{
				return null;
			}
			else
			{
				Conversacion = JsonConvert.DeserializeObject<PersonalPostulanteDTO>(queryConversacion);
				return Conversacion;
			}

		}

		/// <summary>
		/// Obtiene personal gp con menor cantidad de chats
		/// </summary>
		/// <returns></returns>
		public PersonalNumeroMinimoChatDTO ObtenerAsesorGPConMenorChat()
		{
			PersonalNumeroMinimoChatDTO Conversacion = new PersonalNumeroMinimoChatDTO();
			string _query = @"
								SELECT TOP 1 WU.IdPersonal, 
											 ISNULL(CON.NumeroChats, 0) AS NumeroChats
								FROM mkt.T_WhatsAppUsuario WU
									 INNER JOIN gp.T_Personal PER ON WU.IdPersonal = PER.Id AND PER.Activo = 1 AND PER.Estado = 1
									 LEFT JOIN gp.V_UltimoChatWhatsAppPostulanteByAsesores CON ON WU.IdPersonal = CON.IdPersonal
								WHERE WU.Estado = 1
									  AND PER.Rol = 'Gestion de Personas'
								ORDER BY CON.NumeroChats ASC;
							";
			var queryAsesor = _dapper.FirstOrDefault(_query, null);
			if (queryAsesor == null || queryAsesor == "")
			{
				return null;
			}
			else
			{
				Conversacion = JsonConvert.DeserializeObject<PersonalNumeroMinimoChatDTO>(queryAsesor);
				return Conversacion;
			}

		}
		public bool ValidarPlantillasEnviadas(string Plantilla, string Numero)
		{
			PersonalNumeroMinimoChatDTO Conversacion = new PersonalNumeroMinimoChatDTO();
			string _query = "SELECT Id From mkt.T_WhatsAppMensajeEnviadoPostulante Where WaRecipientType='hsm' and WaBody=@Plantilla and WaTo=@Numero ";
			var queryAsesor = _dapper.FirstOrDefault(_query, new { Plantilla, Numero });
			if (queryAsesor == "null" || queryAsesor == "")
			{
				return true;
			}
			else
			{
				//Conversacion = JsonConvert.DeserializeObject<PersonalNumeroMinimoChatDTO>(queryAsesor);
				return false;
			}

		}

		public List<WhatsAppMensajesRecibidosOperacionesDTO> ObtenerMensajesRecibidosOperaciones(int IdPersonal)
		{
			List<WhatsAppMensajesRecibidosOperacionesDTO> Conversacion = new List<WhatsAppMensajesRecibidosOperacionesDTO>();
			string _queryConversacion = "ope.SP_MensajesRecibidosWhatsAppOperacionesVersion";
			var queryConversacion = _dapper.QuerySPDapper(_queryConversacion, new { IdPersonal });
			if (queryConversacion == null || queryConversacion == "")
			{
				return null;
			}
			else
			{
				Conversacion = JsonConvert.DeserializeObject<List<WhatsAppMensajesRecibidosOperacionesDTO>>(queryConversacion);
				return Conversacion;
			}

		}


	}
}
