using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
	public class AdwordsLogRepositorio : BaseRepository<TAdwordsLog, AdwordsLogBO>
	{
		#region Metodos Base
		public AdwordsLogRepositorio() : base()
		{
		}
		public AdwordsLogRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<AdwordsLogBO> GetBy(Expression<Func<TAdwordsLog, bool>> filter)
		{
			IEnumerable<TAdwordsLog> listado = base.GetBy(filter);
			List<AdwordsLogBO> listadoBO = new List<AdwordsLogBO>();
			foreach (var itemEntidad in listado)
			{
				AdwordsLogBO objetoBO = Mapper.Map<TAdwordsLog, AdwordsLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public AdwordsLogBO FirstById(int id)
		{
			try
			{
				TAdwordsLog entidad = base.FirstById(id);
				AdwordsLogBO objetoBO = new AdwordsLogBO();
				Mapper.Map<TAdwordsLog, AdwordsLogBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public AdwordsLogBO FirstBy(Expression<Func<TAdwordsLog, bool>> filter)
		{
			try
			{
				TAdwordsLog entidad = base.FirstBy(filter);
				AdwordsLogBO objetoBO = Mapper.Map<TAdwordsLog, AdwordsLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(AdwordsLogBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TAdwordsLog entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<AdwordsLogBO> listadoBO)
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

		public bool Update(AdwordsLogBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TAdwordsLog entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<AdwordsLogBO> listadoBO)
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
		private void AsignacionId(TAdwordsLog entidad, AdwordsLogBO objetoBO)
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

		private TAdwordsLog MapeoEntidad(AdwordsLogBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TAdwordsLog entidad = new TAdwordsLog();
				entidad = Mapper.Map<AdwordsLogBO, TAdwordsLog>(objetoBO,
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
		/// Obtiene todos los registros logs de apiadwords
		/// </summary>
		/// <returns></returns>
        /// 
		public List<AdwordsLogDTO> ObtenerLogs()
		{
			try
			{
				string _query = "Select Id, Mensaje, Usuario, FechaCreacion FROM mkt.V_TAdwordsLog_ObtenerLogs WHERE Estado = 1 AND FechaCreacion > DATEADD(day, -2, CONVERT (date, SYSDATETIME())) ORDER BY FechaCreacion DESC";
				var Logs = _dapper.QueryDapper(_query, null);
				return JsonConvert.DeserializeObject<List<AdwordsLogDTO>>(Logs);
			}
			catch(Exception Ex)
			{
				throw new Exception(Ex.Message);
			}
		}
	}
}
