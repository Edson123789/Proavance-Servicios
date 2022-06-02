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
	public class GoogleAnalyticsReportePaginaRepositorio : BaseRepository<TGoogleAnalyticsReportePagina, GoogleAnalyticsReportePaginaBO>
	{
		#region Metodos Base
		public GoogleAnalyticsReportePaginaRepositorio() : base()
		{
		}
		public GoogleAnalyticsReportePaginaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<GoogleAnalyticsReportePaginaBO> GetBy(Expression<Func<TGoogleAnalyticsReportePagina, bool>> filter)
		{
			IEnumerable<TGoogleAnalyticsReportePagina> listado = base.GetBy(filter);
			List<GoogleAnalyticsReportePaginaBO> listadoBO = new List<GoogleAnalyticsReportePaginaBO>();
			foreach (var itemEntidad in listado)
			{
				GoogleAnalyticsReportePaginaBO objetoBO = Mapper.Map<TGoogleAnalyticsReportePagina, GoogleAnalyticsReportePaginaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public GoogleAnalyticsReportePaginaBO FirstById(int id)
		{
			try
			{
				TGoogleAnalyticsReportePagina entidad = base.FirstById(id);
				GoogleAnalyticsReportePaginaBO objetoBO = new GoogleAnalyticsReportePaginaBO();
				Mapper.Map<TGoogleAnalyticsReportePagina, GoogleAnalyticsReportePaginaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public GoogleAnalyticsReportePaginaBO FirstBy(Expression<Func<TGoogleAnalyticsReportePagina, bool>> filter)
		{
			try
			{
				TGoogleAnalyticsReportePagina entidad = base.FirstBy(filter);
				GoogleAnalyticsReportePaginaBO objetoBO = Mapper.Map<TGoogleAnalyticsReportePagina, GoogleAnalyticsReportePaginaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(GoogleAnalyticsReportePaginaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TGoogleAnalyticsReportePagina entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<GoogleAnalyticsReportePaginaBO> listadoBO)
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

		public bool Update(GoogleAnalyticsReportePaginaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TGoogleAnalyticsReportePagina entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<GoogleAnalyticsReportePaginaBO> listadoBO)
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
		private void AsignacionId(TGoogleAnalyticsReportePagina entidad, GoogleAnalyticsReportePaginaBO objetoBO)
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

		private TGoogleAnalyticsReportePagina MapeoEntidad(GoogleAnalyticsReportePaginaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TGoogleAnalyticsReportePagina entidad = new TGoogleAnalyticsReportePagina();
				entidad = Mapper.Map<GoogleAnalyticsReportePaginaBO, TGoogleAnalyticsReportePagina>(objetoBO,
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
