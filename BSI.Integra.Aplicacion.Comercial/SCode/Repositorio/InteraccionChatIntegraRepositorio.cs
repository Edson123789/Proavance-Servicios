using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using static BSI.Integra.Aplicacion.Comercial.BO.InteraccionChatIntegraBO;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
    public class InteraccionChatIntegraRepositorio : BaseRepository<TInteraccionChatIntegra, InteraccionChatIntegraBO>
    {
        #region Metodos Base
        public InteraccionChatIntegraRepositorio() : base()
        {
        }
        public InteraccionChatIntegraRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<InteraccionChatIntegraBO> GetBy(Expression<Func<TInteraccionChatIntegra, bool>> filter)
        {
            IEnumerable<TInteraccionChatIntegra> listado = base.GetBy(filter);
            List<InteraccionChatIntegraBO> listadoBO = new List<InteraccionChatIntegraBO>();
            foreach (var itemEntidad in listado)
            {
                InteraccionChatIntegraBO objetoBO = Mapper.Map<TInteraccionChatIntegra, InteraccionChatIntegraBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public InteraccionChatIntegraBO FirstById(int id)
        {
            try
            {
                TInteraccionChatIntegra entidad = base.FirstById(id);
                InteraccionChatIntegraBO objetoBO = new InteraccionChatIntegraBO();
                Mapper.Map<TInteraccionChatIntegra, InteraccionChatIntegraBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public InteraccionChatIntegraBO FirstBy(Expression<Func<TInteraccionChatIntegra, bool>> filter)
        {
            try
            {
                TInteraccionChatIntegra entidad = base.FirstBy(filter);
                InteraccionChatIntegraBO objetoBO = Mapper.Map<TInteraccionChatIntegra, InteraccionChatIntegraBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(InteraccionChatIntegraBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TInteraccionChatIntegra entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<InteraccionChatIntegraBO> listadoBO)
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

        public bool Update(InteraccionChatIntegraBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TInteraccionChatIntegra entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<InteraccionChatIntegraBO> listadoBO)
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
        private void AsignacionId(TInteraccionChatIntegra entidad, InteraccionChatIntegraBO objetoBO)
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

        private TInteraccionChatIntegra MapeoEntidad(InteraccionChatIntegraBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TInteraccionChatIntegra entidad = new TInteraccionChatIntegra();
                entidad = Mapper.Map<InteraccionChatIntegraBO, TInteraccionChatIntegra>(objetoBO,
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
        /// Obtiene una InteraccionChatIntegra filtrado por connectionId(IdChatSession)
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public InteraccionChatIntegraSignalRDTO ObtenerInteraccionChatIntegraPorConnectionId(string connectionId)
        {
			try
			{
				InteraccionChatIntegraSignalRDTO interaccionChatIntegra = new InteraccionChatIntegraSignalRDTO();
				var _query = string.Empty;
				_query = "SELECT Id, IdAlumno, Pais, Ciudad, IdChatSession, NroMensajes, NroPalabrasVisitor FROM com.V_TInteraccionChatIntegra_ObtenerInteraccionChatIntegraSignalR WHERE Estado = 1 AND IdchatSession = @connectionId";
				var interaccionChatIntegraDB = _dapper.FirstOrDefault(_query, new { connectionId });
				interaccionChatIntegra = JsonConvert.DeserializeObject<InteraccionChatIntegraSignalRDTO>(interaccionChatIntegraDB);
				return interaccionChatIntegra;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
            
        }

		/// <summary>
		/// Obtiene las interacciones de chat mediante el idchatsession
		/// </summary>
		/// <param name="listIdsChatSession"></param>
		/// <returns></returns>
		public List<InteraccionChatIntegraDTO> ObtenerInteraccionesPorIdsSessionChat(string listIdsChatSession)
		{
			try
			{
				List<InteraccionChatIntegraDTO> interaccionChat = new List<InteraccionChatIntegraDTO>();
				var _query = string.Empty;
				_query = "SELECT Id, IdChatIntegraHistorialAsesor, IdAlumno, IdContactoPortalSegmento, IdTipoInteraccion, IdPGeneral, IdSubAreaCapacitacion, " +
					"IdAreaCapacitacion, Ip, Pais, Region, Ciudad, Duracion, NroMensajes, NroPalabrasVisitor, NroPalabrasAgente, UsuarioTiempoRespuestaMaximo, " +
					"UsuarioTiempoRespuestaPromedio, FechaInicio, FechaFin, Leido, Plataforma, Navegador, UrlFrom, UrlTo, IdEstadoChat, IdConjuntoAnuncio, IdChatSession, " +
					"IdFaseOportunidad_PortalWeb, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion FROM com.V_TInteraccionChatIntegra_Chat WHERE IdChatSession in " + listIdsChatSession;
				var asesorChatDetalleDB = _dapper.QueryDapper(_query, null);
				interaccionChat = JsonConvert.DeserializeObject<List<InteraccionChatIntegraDTO>>(asesorChatDetalleDB);
				return interaccionChat;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
	
		}
		/// <summary>
		/// Obtiene idAlumnomendiante el id de interaccion de chat intengra
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public int ObtenerIdContactoPorIdInteraccionChatIntegra(int id)
		{
			try
			{
				InteraccionChatIntegra asesorChatDetalleBO = new InteraccionChatIntegra();
				var _query = string.Empty;
				_query = "select Id,IdAlumno from com.T_InteraccionChatIntegra where Id = @id";
				var asesorChatDetalleDB = _dapper.FirstOrDefault(_query, new { id });
                asesorChatDetalleBO = JsonConvert.DeserializeObject<InteraccionChatIntegra>(asesorChatDetalleDB);
				return asesorChatDetalleBO.IdAlumno;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene reporte de chats
		/// </summary>
		/// <param name="chat"></param>
		/// <returns></returns>
		public List<ReporteChatIntegraDTO> GenerarReporteChat(ChatReporteDTO chat)
		{
			try
			{
				DateTime FechaInicio = chat.FechaInicio.AddHours(-5);
				DateTime FechaFin = chat.FechaFin.AddHours(-5);
				var query = _dapper.QuerySPDapper("com.SP_ReporteChat", new { chat.Areas, chat.CentroCosto, chat.Asesor, FechaInicio, FechaFin, chat.Pais });
				//switch (chat.Desglose)
				//{
				//	case 1: //Dia
				//		query = _dapper.QuerySPDapper("com.SP_ReporteChat2", new { chat.Areas, chat.CentroCosto, chat.Asesor, FechaInicio, FechaFin, chat.Pais });
				//		break;
				//	case 2: //Semana
				//		query = _dapper.QuerySPDapper("com.SP_ReporteChatSemana", new { chat.Areas, chat.CentroCosto, chat.Asesor, FechaInicio, FechaFin, chat.Pais });
				//		break;
				//	case 3: //Mes
				//		query = _dapper.QuerySPDapper("com.SP_ReporteChatMes", new { chat.Areas, chat.CentroCosto, chat.Asesor, FechaInicio, FechaFin, chat.Pais });
				//		break;
				//	default:
				//		query = _dapper.QuerySPDapper("com.SP_ReporteChat2", new { chat.Areas, chat.CentroCosto, chat.Asesor, FechaInicio, FechaFin, chat.Pais });
				//		break;
				//}
				var res = JsonConvert.DeserializeObject<List<ReporteChatIntegraDTO>>(query);

				return res;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// <summary>
		/// Obtiene reporte de oportunidades generadas por chat
		/// </summary>
		/// <param name="chat"></param>
		/// <returns></returns>
		public List<ReporteChatIntegraDTO> GenerarReporteChatSinChat(ChatReporteDTO chat)
		{
			try
			{
				DateTime FechaInicio = chat.FechaInicio.AddHours(-5);
				DateTime FechaFin = chat.FechaFin.AddHours(-5);

				var query = _dapper.QuerySPDapper("com.SP_ReporteChatOportunidadSinChats", new { chat.Areas, chat.CentroCosto, chat.Asesor, FechaInicio, FechaFin });
				var res = JsonConvert.DeserializeObject<List<ReporteChatIntegraDTO>>(query);

				return res;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene registro de errores durante los chats
		/// </summary>
		/// <param name="chat"></param>
		/// <returns></returns>
		public List<ReporteChatLogDTO> GenerarReporteChatLog(ChatReporteDTO chat)
		{
			try
			{
				DateTime FechaInicio = chat.FechaInicio.AddHours(-5);
				DateTime FechaFin = chat.FechaFin.AddHours(-5);
				var query = _dapper.QuerySPDapper("com.SP_ReporteChatSeguimiento", new { chat.Areas, chat.CentroCosto, chat.Asesor, FechaInicio, FechaFin });
				var res = JsonConvert.DeserializeObject<List<ReporteChatLogDTO>>(query);
				return res;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public string ObtenerNombreMes(int mes)
		{
			try
			{
				switch (mes)
				{
					case 1:
						return "Enero";
					case 2:
						return "Febrero";
					case 3:
						return "Marzo";
					case 4:
						return "Abril";
					case 5:
						return "Mayo";
					case 6:
						return "Junio";
					case 7:
						return "Julio";
					case 8:
						return "Agosto";
					case 9:
						return "Setiembre";
					case 10:
						return "Octubre";
					case 11:
						return "Noviembre";
					case 12:
						return "Diciembre";
					default:
						return "";
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public int ObtenerNumeroSemana(DateTime date)
		{
			CultureInfo ciCurr = CultureInfo.CurrentCulture;
			int weekNum = ciCurr.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
			return weekNum;
		}
	}
}
