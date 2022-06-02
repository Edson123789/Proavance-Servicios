using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
	public class InteraccionChatWhatsAppRepositorio : BaseRepository<TInteraccionChatWhatsApp, InteraccionChatWhatsAppBO>
	{
		#region Metodos Base
		public InteraccionChatWhatsAppRepositorio() : base()
		{
		}
		public InteraccionChatWhatsAppRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<InteraccionChatWhatsAppBO> GetBy(Expression<Func<TInteraccionChatWhatsApp, bool>> filter)
		{
			IEnumerable<TInteraccionChatWhatsApp> listado = base.GetBy(filter);
			List<InteraccionChatWhatsAppBO> listadoBO = new List<InteraccionChatWhatsAppBO>();
			foreach (var itemEntidad in listado)
			{
				InteraccionChatWhatsAppBO objetoBO = Mapper.Map<TInteraccionChatWhatsApp, InteraccionChatWhatsAppBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public InteraccionChatWhatsAppBO FirstById(int id)
		{
			try
			{
				TInteraccionChatWhatsApp entidad = base.FirstById(id);
				InteraccionChatWhatsAppBO objetoBO = new InteraccionChatWhatsAppBO();
				Mapper.Map<TInteraccionChatWhatsApp, InteraccionChatWhatsAppBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public InteraccionChatWhatsAppBO FirstBy(Expression<Func<TInteraccionChatWhatsApp, bool>> filter)
		{
			try
			{
				TInteraccionChatWhatsApp entidad = base.FirstBy(filter);
				InteraccionChatWhatsAppBO objetoBO = Mapper.Map<TInteraccionChatWhatsApp, InteraccionChatWhatsAppBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(InteraccionChatWhatsAppBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TInteraccionChatWhatsApp entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<InteraccionChatWhatsAppBO> listadoBO)
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

		public bool Update(InteraccionChatWhatsAppBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TInteraccionChatWhatsApp entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<InteraccionChatWhatsAppBO> listadoBO)
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
		private void AsignacionId(TInteraccionChatWhatsApp entidad, InteraccionChatWhatsAppBO objetoBO)
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

		private TInteraccionChatWhatsApp MapeoEntidad(InteraccionChatWhatsAppBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TInteraccionChatWhatsApp entidad = new TInteraccionChatWhatsApp();
				entidad = Mapper.Map<InteraccionChatWhatsAppBO, TInteraccionChatWhatsApp>(objetoBO,
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
		public List<CalculoMensajesOportunidadesChatWhatsAppDTO> ObtenerCalculoChatWhatsAppDiario(DateTime fecha)
		{
			try
			{
				var query = "mkt.SP_ChatWhatsAppOportunidad";
				var temp = _dapper.QuerySPDapper(query, new { Fecha = fecha.Date });
				var res = JsonConvert.DeserializeObject<List<CalculoMensajesOportunidadesChatWhatsAppDTO>>(temp);

				return res;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene la informacion de los chats de whatsapp para el reporte de chat whatsapp
		/// </summary>
		/// <param name="chat"></param>
		/// <returns></returns>
		public List<ReporteChatWhatsAppDTO> obtenerReporteChatWhatsApp(FiltrosChatWhatsAppReporteDTO chat)
		{
			try
			{
				var query = "mkt.SP_ReporteChatWhatsApp";
				var temp = _dapper.QuerySPDapper(query, new { Asesor = chat.Asesor, Pais = chat.Pais, FechaInicio = chat.FechaInicio.Date, FechaFin = chat.FechaFin.Date });
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
