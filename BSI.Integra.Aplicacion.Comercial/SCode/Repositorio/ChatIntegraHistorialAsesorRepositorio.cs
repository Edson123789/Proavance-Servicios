using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
	public class ChatIntegraHistorialAsesorRepositorio : BaseRepository<TChatIntegraHistorialAsesor, ChatIntegraHistorialAsesorBO>
	{
		#region Metodos Base
		public ChatIntegraHistorialAsesorRepositorio() : base()
		{
		}
		public ChatIntegraHistorialAsesorRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ChatIntegraHistorialAsesorBO> GetBy(Expression<Func<TChatIntegraHistorialAsesor, bool>> filter)
		{
			IEnumerable<TChatIntegraHistorialAsesor> listado = base.GetBy(filter);
			List<ChatIntegraHistorialAsesorBO> listadoBO = new List<ChatIntegraHistorialAsesorBO>();
			foreach (var itemEntidad in listado)
			{
				ChatIntegraHistorialAsesorBO objetoBO = Mapper.Map<TChatIntegraHistorialAsesor, ChatIntegraHistorialAsesorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ChatIntegraHistorialAsesorBO FirstById(int id)
		{
			try
			{
				TChatIntegraHistorialAsesor entidad = base.FirstById(id);
				ChatIntegraHistorialAsesorBO objetoBO = new ChatIntegraHistorialAsesorBO();
				Mapper.Map<TChatIntegraHistorialAsesor, ChatIntegraHistorialAsesorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ChatIntegraHistorialAsesorBO FirstBy(Expression<Func<TChatIntegraHistorialAsesor, bool>> filter)
		{
			try
			{
				TChatIntegraHistorialAsesor entidad = base.FirstBy(filter);
				ChatIntegraHistorialAsesorBO objetoBO = Mapper.Map<TChatIntegraHistorialAsesor, ChatIntegraHistorialAsesorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ChatIntegraHistorialAsesorBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TChatIntegraHistorialAsesor entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ChatIntegraHistorialAsesorBO> listadoBO)
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

		public bool Update(ChatIntegraHistorialAsesorBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TChatIntegraHistorialAsesor entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ChatIntegraHistorialAsesorBO> listadoBO)
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
		private void AsignacionId(TChatIntegraHistorialAsesor entidad, ChatIntegraHistorialAsesorBO objetoBO)
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

		private TChatIntegraHistorialAsesor MapeoEntidad(ChatIntegraHistorialAsesorBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TChatIntegraHistorialAsesor entidad = new TChatIntegraHistorialAsesor();
				entidad = Mapper.Map<ChatIntegraHistorialAsesorBO, TChatIntegraHistorialAsesor>(objetoBO,
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
		/// Obtiene el asesor asociado a determinado programa mediante idAsesorChatDetalle
		/// </summary>
		/// <param name="idAsesorChatDetalle"></param>
		/// <returns></returns>
		public ChatIntegraHistorialAsesorDTO ObtenerHistoricoDetallesPorAsesorChatDetalle(int idAsesorChatDetalle)
		{
			try
			{
				ChatIntegraHistorialAsesorDTO chatIntegraHistorialAsesor = new ChatIntegraHistorialAsesorDTO();
				var _query = string.Empty;
				_query = "SELECT Id, IdAsesorChatDetalle, IdPersonal, FechaAsignacion, Estado FROM com.V_TChatIntegraHistorialAsesor_ObtenerParaValidarPersonaAsignadaSignalR WHERE Estado = 1 AND IdAsesorChatDetalle = @idAsesorChatDetalle ORDER BY FechaAsignacion DESC, FechaCreacion DESC";
				var chatIntegraHistorialAsesorDB = _dapper.FirstOrDefault(_query, new { idAsesorChatDetalle });
				chatIntegraHistorialAsesor = JsonConvert.DeserializeObject<ChatIntegraHistorialAsesorDTO>(chatIntegraHistorialAsesorDB);
				return chatIntegraHistorialAsesor;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		
		}
	}
}
