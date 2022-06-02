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
	public class GoogleAnalyticsSegmentoRepositorio : BaseRepository<TGoogleAnalyticsSegmento, GoogleAnalyticsSegmentoBO>
	{
		#region Metodos Base
		public GoogleAnalyticsSegmentoRepositorio() : base()
		{
		}
		public GoogleAnalyticsSegmentoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<GoogleAnalyticsSegmentoBO> GetBy(Expression<Func<TGoogleAnalyticsSegmento, bool>> filter)
		{
			IEnumerable<TGoogleAnalyticsSegmento> listado = base.GetBy(filter);
			List<GoogleAnalyticsSegmentoBO> listadoBO = new List<GoogleAnalyticsSegmentoBO>();
			foreach (var itemEntidad in listado)
			{
				GoogleAnalyticsSegmentoBO objetoBO = Mapper.Map<TGoogleAnalyticsSegmento, GoogleAnalyticsSegmentoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public GoogleAnalyticsSegmentoBO FirstById(int id)
		{
			try
			{
				TGoogleAnalyticsSegmento entidad = base.FirstById(id);
				GoogleAnalyticsSegmentoBO objetoBO = new GoogleAnalyticsSegmentoBO();
				Mapper.Map<TGoogleAnalyticsSegmento, GoogleAnalyticsSegmentoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public GoogleAnalyticsSegmentoBO FirstBy(Expression<Func<TGoogleAnalyticsSegmento, bool>> filter)
		{
			try
			{
				TGoogleAnalyticsSegmento entidad = base.FirstBy(filter);
				GoogleAnalyticsSegmentoBO objetoBO = Mapper.Map<TGoogleAnalyticsSegmento, GoogleAnalyticsSegmentoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(GoogleAnalyticsSegmentoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TGoogleAnalyticsSegmento entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<GoogleAnalyticsSegmentoBO> listadoBO)
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

		public bool Update(GoogleAnalyticsSegmentoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TGoogleAnalyticsSegmento entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<GoogleAnalyticsSegmentoBO> listadoBO)
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
		private void AsignacionId(TGoogleAnalyticsSegmento entidad, GoogleAnalyticsSegmentoBO objetoBO)
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

		private TGoogleAnalyticsSegmento MapeoEntidad(GoogleAnalyticsSegmentoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TGoogleAnalyticsSegmento entidad = new TGoogleAnalyticsSegmento();
				entidad = Mapper.Map<GoogleAnalyticsSegmentoBO, TGoogleAnalyticsSegmento>(objetoBO,
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
