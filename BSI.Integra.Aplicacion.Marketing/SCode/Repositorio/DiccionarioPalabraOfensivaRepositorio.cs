using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Marketing;
using BSI.Integra.Aplicacion.Marketing.SCode.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
	/// Repositorio: DiccionarioPalabraOfensivaRepositorio
	/// Autor: Edgar S.
	/// Fecha: 03/03/2021
	/// <summary>
	/// Gestión de Diccionario de Palabras Ofensivas
	/// </summary>
	public class DiccionarioPalabraOfensivaRepositorio : BaseRepository<TDiccionarioPalabraOfensiva, DiccionarioPalabraOfensivaBO>
	{
		#region Metodos Base
		public DiccionarioPalabraOfensivaRepositorio() : base()
		{
		}
		public DiccionarioPalabraOfensivaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DiccionarioPalabraOfensivaBO> GetBy(Expression<Func<TDiccionarioPalabraOfensiva, bool>> filter)
		{
			IEnumerable<TDiccionarioPalabraOfensiva> listado = base.GetBy(filter);
			List<DiccionarioPalabraOfensivaBO> listadoBO = new List<DiccionarioPalabraOfensivaBO>();
			foreach (var itemEntidad in listado)
			{
				DiccionarioPalabraOfensivaBO objetoBO = Mapper.Map<TDiccionarioPalabraOfensiva, DiccionarioPalabraOfensivaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DiccionarioPalabraOfensivaBO FirstById(int id)
		{
			try
			{
				TDiccionarioPalabraOfensiva entidad = base.FirstById(id);
				DiccionarioPalabraOfensivaBO objetoBO = new DiccionarioPalabraOfensivaBO();
				Mapper.Map<TDiccionarioPalabraOfensiva, DiccionarioPalabraOfensivaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DiccionarioPalabraOfensivaBO FirstBy(Expression<Func<TDiccionarioPalabraOfensiva, bool>> filter)
		{
			try
			{
				TDiccionarioPalabraOfensiva entidad = base.FirstBy(filter);
				DiccionarioPalabraOfensivaBO objetoBO = Mapper.Map<TDiccionarioPalabraOfensiva, DiccionarioPalabraOfensivaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DiccionarioPalabraOfensivaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDiccionarioPalabraOfensiva entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DiccionarioPalabraOfensivaBO> listadoBO)
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

		public bool Update(DiccionarioPalabraOfensivaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDiccionarioPalabraOfensiva entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DiccionarioPalabraOfensivaBO> listadoBO)
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
		private void AsignacionId(TDiccionarioPalabraOfensiva entidad, DiccionarioPalabraOfensivaBO objetoBO)
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

		private TDiccionarioPalabraOfensiva MapeoEntidad(DiccionarioPalabraOfensivaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDiccionarioPalabraOfensiva entidad = new TDiccionarioPalabraOfensiva();
				entidad = Mapper.Map<DiccionarioPalabraOfensivaBO, TDiccionarioPalabraOfensiva>(objetoBO,
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


		///Repositorio: DiccionarioPalabraOfensivaRepositorio
		///Autor: Edgar S.
		///Fecha: 08/03/2021
		/// <summary>
		/// Obtiene lista de Palabras Filtradas Chat Portal
		/// </summary>
		/// <param></param>
		/// <returns> List<PalabrasOfensivasEncontradasDTO> </returns>
		public List<PalabrasOfensivasEncontradasDTO> ObtenerPalabrasOfensivasFiltradas()
		{
			try
			{
				List<PalabrasOfensivasEncontradasDTO> listaPalabrasFiltradas = new List<PalabrasOfensivasEncontradasDTO>();
				string query = string.Empty;
				query = "SELECT Id,IdOrigenMensaje,IdAlumno,DatosVisitante,Fecha,Mensaje,IdPersonal, DatosAsesor FROM com.V_ObtenerPalabrasOfensivasFiltradas WHERE Estado = 1 AND MensajeOfensivo = 1 AND IdRemitente = 'visitante' ORDER BY Fecha DESC";
				var respuesta = _dapper.QueryDapper(query, null);
				if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
				{
					listaPalabrasFiltradas = JsonConvert.DeserializeObject<List<PalabrasOfensivasEncontradasDTO>>(respuesta);
				}
				return listaPalabrasFiltradas;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}


		///Repositorio: DiccionarioPalabraOfensivaRepositorio
		///Autor: Edgar S.
		///Fecha: 24/04/2021
		/// <summary>
		/// Obtiene Id de Asesores por IdChat de Portal
		/// </summary>
		/// <param name="listaId"> lista de Id de chats de Portal </param>
		/// <returns> List<IdAsesorChatDTO> </returns>
		public List<IdAsesorChatDTO> ObtenerAsesorPorIdChatPortal(string listaId)
		{
			try
			{
				List<IdAsesorChatDTO> listaIdAsesorChat = new List<IdAsesorChatDTO>();
				string query = string.Empty;
				query = "SELECT Id,IdPersonal FROM [com].[V_TChatDetalleIntegra_ObtenerAsesorPorIdChat] WHERE Estado = 1 AND Id IN ("+ listaId + ")";
				var respuesta = _dapper.QueryDapper(query, null);
				if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
				{
					listaIdAsesorChat = JsonConvert.DeserializeObject<List<IdAsesorChatDTO>>(respuesta);
				}
				return listaIdAsesorChat;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}


		///Repositorio: DiccionarioPalabraOfensivaRepositorio
		///Autor: Edgar S.
		///Fecha: 24/04/2021
		/// <summary>
		/// Obtiene Id de Asesores por IdChat de WhatsApp
		/// </summary>
		/// <param name="listaId"> lista de Id de chats de WhatsApp </param>
		/// <returns> List<IdAsesorChatDTO> </returns>
		public List<IdAsesorChatDTO> ObtenerAsesorPorIdChatWhatsApp(string listaId)
		{
			try
			{
				List<IdAsesorChatDTO> listaIdAsesorChat = new List<IdAsesorChatDTO>();
				string query = string.Empty;
				query = "SELECT Id,IdPersonal FROM [com].[V_TWhatsAppMensajeRecibido_ObtenerAsesorPorIdChat] WHERE Estado = 1 AND Id IN (" + listaId + ")";
				var respuesta = _dapper.QueryDapper(query, null);
				if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
				{
					listaIdAsesorChat = JsonConvert.DeserializeObject<List<IdAsesorChatDTO>>(respuesta);
				}
				return listaIdAsesorChat;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
