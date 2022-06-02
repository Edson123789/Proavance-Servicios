using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.DTOs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
	public class ChatDetalleIntegraRepositorio : BaseRepository<TChatDetalleIntegra, ChatDetalleIntegraBO>
	{
		#region Metodos Base
		public ChatDetalleIntegraRepositorio() : base()
		{
		}
		public ChatDetalleIntegraRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ChatDetalleIntegraBO> GetBy(Expression<Func<TChatDetalleIntegra, bool>> filter)
		{
			IEnumerable<TChatDetalleIntegra> listado = base.GetBy(filter);
			List<ChatDetalleIntegraBO> listadoBO = new List<ChatDetalleIntegraBO>();
			foreach (var itemEntidad in listado)
			{
				ChatDetalleIntegraBO objetoBO = Mapper.Map<TChatDetalleIntegra, ChatDetalleIntegraBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ChatDetalleIntegraBO FirstById(int id)
		{
			try
			{
				TChatDetalleIntegra entidad = base.FirstById(id);
				ChatDetalleIntegraBO objetoBO = new ChatDetalleIntegraBO();
				Mapper.Map<TChatDetalleIntegra, ChatDetalleIntegraBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ChatDetalleIntegraBO FirstBy(Expression<Func<TChatDetalleIntegra, bool>> filter)
		{
			try
			{
				TChatDetalleIntegra entidad = base.FirstBy(filter);
				ChatDetalleIntegraBO objetoBO = Mapper.Map<TChatDetalleIntegra, ChatDetalleIntegraBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ChatDetalleIntegraBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TChatDetalleIntegra entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ChatDetalleIntegraBO> listadoBO)
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

		public bool Update(ChatDetalleIntegraBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TChatDetalleIntegra entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ChatDetalleIntegraBO> listadoBO)
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
		private void AsignacionId(TChatDetalleIntegra entidad, ChatDetalleIntegraBO objetoBO)
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

		private TChatDetalleIntegra MapeoEntidad(ChatDetalleIntegraBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TChatDetalleIntegra entidad = new TChatDetalleIntegra();
				entidad = Mapper.Map<ChatDetalleIntegraBO, TChatDetalleIntegra>(objetoBO,
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
		/// Obtiene un listado de ChatDetalleIntegra filtrado por idInteraccionChatIntegra
		/// </summary>
		/// <param name="idsInteraccionChatIntegra"></param>
		/// <returns></returns>
		public List<ChatDetalleIntegraDTO> ObtenerChatDetalleIntegraPorIdsInteraccionChatIntegra(string idsInteraccionChatIntegra)
		{
			try
			{
				List<ChatDetalleIntegraDTO> ChatDetallesIntegra = new List<ChatDetalleIntegraDTO>();
				var _query = string.Empty;
				_query = "select IdInteraccionChatIntegra, NombreRemitente, IdRemitente, Mensaje, Fecha from com.V_TChatDetalleIntegra_ObtenerDetalle where IdInteraccionChatIntegra in " + idsInteraccionChatIntegra;
				var ChatDetallesIntegraDB = _dapper.QueryDapper(_query, null);
				ChatDetallesIntegra = JsonConvert.DeserializeObject<List<ChatDetalleIntegraDTO>>(ChatDetallesIntegraDB);
				return ChatDetallesIntegra;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
