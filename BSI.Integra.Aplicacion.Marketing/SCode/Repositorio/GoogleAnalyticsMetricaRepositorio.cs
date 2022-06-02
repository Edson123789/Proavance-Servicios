using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
	public class GoogleAnalyticsMetricaRepositorio : BaseRepository<TGoogleAnalyticsMetrica, GoogleAnalyticsMetricaBO>
	{
		#region Metodos Base
		public GoogleAnalyticsMetricaRepositorio() : base()
		{
		}
		public GoogleAnalyticsMetricaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<GoogleAnalyticsMetricaBO> GetBy(Expression<Func<TGoogleAnalyticsMetrica, bool>> filter)
		{
			IEnumerable<TGoogleAnalyticsMetrica> listado = base.GetBy(filter);
			List<GoogleAnalyticsMetricaBO> listadoBO = new List<GoogleAnalyticsMetricaBO>();
			foreach (var itemEntidad in listado)
			{
				GoogleAnalyticsMetricaBO objetoBO = Mapper.Map<TGoogleAnalyticsMetrica, GoogleAnalyticsMetricaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public GoogleAnalyticsMetricaBO FirstById(int id)
		{
			try
			{
				TGoogleAnalyticsMetrica entidad = base.FirstById(id);
				GoogleAnalyticsMetricaBO objetoBO = new GoogleAnalyticsMetricaBO();
				Mapper.Map<TGoogleAnalyticsMetrica, GoogleAnalyticsMetricaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public GoogleAnalyticsMetricaBO FirstBy(Expression<Func<TGoogleAnalyticsMetrica, bool>> filter)
		{
			try
			{
				TGoogleAnalyticsMetrica entidad = base.FirstBy(filter);
				GoogleAnalyticsMetricaBO objetoBO = Mapper.Map<TGoogleAnalyticsMetrica, GoogleAnalyticsMetricaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(GoogleAnalyticsMetricaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TGoogleAnalyticsMetrica entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<GoogleAnalyticsMetricaBO> listadoBO)
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

		public bool Update(GoogleAnalyticsMetricaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TGoogleAnalyticsMetrica entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<GoogleAnalyticsMetricaBO> listadoBO)
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
		private void AsignacionId(TGoogleAnalyticsMetrica entidad, GoogleAnalyticsMetricaBO objetoBO)
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

		private TGoogleAnalyticsMetrica MapeoEntidad(GoogleAnalyticsMetricaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TGoogleAnalyticsMetrica entidad = new TGoogleAnalyticsMetrica();
				entidad = Mapper.Map<GoogleAnalyticsMetricaBO, TGoogleAnalyticsMetrica>(objetoBO,
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

	}
}
