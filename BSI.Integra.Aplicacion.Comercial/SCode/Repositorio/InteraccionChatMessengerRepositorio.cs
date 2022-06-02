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

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
	public class InteraccionChatMessengerRepositorio : BaseRepository<TInteraccionChatMessenger, InteraccionChatMessengerBO>
	{
		#region Metodos Base
		public InteraccionChatMessengerRepositorio() : base()
		{
		}
		public InteraccionChatMessengerRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<InteraccionChatMessengerBO> GetBy(Expression<Func<TInteraccionChatMessenger, bool>> filter)
		{
			IEnumerable<TInteraccionChatMessenger> listado = base.GetBy(filter);
			List<InteraccionChatMessengerBO> listadoBO = new List<InteraccionChatMessengerBO>();
			foreach (var itemEntidad in listado)
			{
				InteraccionChatMessengerBO objetoBO = Mapper.Map<TInteraccionChatMessenger, InteraccionChatMessengerBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public InteraccionChatMessengerBO FirstById(int id)
		{
			try
			{
				TInteraccionChatMessenger entidad = base.FirstById(id);
				InteraccionChatMessengerBO objetoBO = new InteraccionChatMessengerBO();
				Mapper.Map<TInteraccionChatMessenger, InteraccionChatMessengerBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public InteraccionChatMessengerBO FirstBy(Expression<Func<TInteraccionChatMessenger, bool>> filter)
		{
			try
			{
				TInteraccionChatMessenger entidad = base.FirstBy(filter);
				InteraccionChatMessengerBO objetoBO = Mapper.Map<TInteraccionChatMessenger, InteraccionChatMessengerBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(InteraccionChatMessengerBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TInteraccionChatMessenger entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<InteraccionChatMessengerBO> listadoBO)
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

		public bool Update(InteraccionChatMessengerBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TInteraccionChatMessenger entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<InteraccionChatMessengerBO> listadoBO)
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
		private void AsignacionId(TInteraccionChatMessenger entidad, InteraccionChatMessengerBO objetoBO)
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

		private TInteraccionChatMessenger MapeoEntidad(InteraccionChatMessengerBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TInteraccionChatMessenger entidad = new TInteraccionChatMessenger();
				entidad = Mapper.Map<InteraccionChatMessengerBO, TInteraccionChatMessenger>(objetoBO,
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
		/// Obtiene conversaciones de chat de whatsapp en una determinada fecha para su calculo de tiempo de repuesta
		/// </summary>
		/// <param name="fecha"></param>
		/// <returns></returns>
		public List<CalculoMensajesOportunidadesMessengerDTO> ObtenerCalculoChatMessengerDiario(DateTime fecha)
		{
			try
			{
				var query = "com.SP_ObtenerMensajesChatsMessenger";
				var temp = _dapper.QuerySPDapper(query, new { Fecha = fecha.Date });
				var res = JsonConvert.DeserializeObject<List<CalculoMensajesOportunidadesMessengerDTO>>(temp);

				return res;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene la informacion de los chats de whatsapp para el reporte de chat messenger
		/// </summary>
		/// <param name="chat"></param>
		/// <returns></returns>
		public List<ReporteChatWhatsAppDTO> obtenerReporteChatMessenger(FiltrosChatMessengerReporteDTO chat)
		{
			try
			{
				var query = "mkt.SP_ReporteChatMessenger";
				var temp = _dapper.QuerySPDapper(query, new { Asesor = chat.Asesor, FechaInicio = chat.FechaInicio.Date, FechaFin = chat.FechaFin.Date });
				var res = JsonConvert.DeserializeObject<List<ReporteChatWhatsAppDTO>>(temp);

				return res;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene el nombre de mes
		/// </summary>
		/// <param name="mes"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Obtiene numero de semana de una fecha determinada
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		public int ObtenerNumeroSemana(DateTime date)
		{
			CultureInfo ciCurr = CultureInfo.CurrentCulture;
			int weekNum = ciCurr.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
			return weekNum;
		}
	}
}
