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
	public class GoogleAnalyticsReporteDetalleRepositorio : BaseRepository<TGoogleAnalyticsReporteDetalle, GoogleAnalyticsReporteDetalleBO>
	{
		#region Metodos Base
		public GoogleAnalyticsReporteDetalleRepositorio() : base()
		{
		}
		public GoogleAnalyticsReporteDetalleRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<GoogleAnalyticsReporteDetalleBO> GetBy(Expression<Func<TGoogleAnalyticsReporteDetalle, bool>> filter)
		{
			IEnumerable<TGoogleAnalyticsReporteDetalle> listado = base.GetBy(filter);
			List<GoogleAnalyticsReporteDetalleBO> listadoBO = new List<GoogleAnalyticsReporteDetalleBO>();
			foreach (var itemEntidad in listado)
			{
				GoogleAnalyticsReporteDetalleBO objetoBO = Mapper.Map<TGoogleAnalyticsReporteDetalle, GoogleAnalyticsReporteDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public GoogleAnalyticsReporteDetalleBO FirstById(int id)
		{
			try
			{
				TGoogleAnalyticsReporteDetalle entidad = base.FirstById(id);
				GoogleAnalyticsReporteDetalleBO objetoBO = new GoogleAnalyticsReporteDetalleBO();
				Mapper.Map<TGoogleAnalyticsReporteDetalle, GoogleAnalyticsReporteDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public GoogleAnalyticsReporteDetalleBO FirstBy(Expression<Func<TGoogleAnalyticsReporteDetalle, bool>> filter)
		{
			try
			{
				TGoogleAnalyticsReporteDetalle entidad = base.FirstBy(filter);
				GoogleAnalyticsReporteDetalleBO objetoBO = Mapper.Map<TGoogleAnalyticsReporteDetalle, GoogleAnalyticsReporteDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(GoogleAnalyticsReporteDetalleBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TGoogleAnalyticsReporteDetalle entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<GoogleAnalyticsReporteDetalleBO> listadoBO)
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

		public bool Update(GoogleAnalyticsReporteDetalleBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TGoogleAnalyticsReporteDetalle entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<GoogleAnalyticsReporteDetalleBO> listadoBO)
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
		private void AsignacionId(TGoogleAnalyticsReporteDetalle entidad, GoogleAnalyticsReporteDetalleBO objetoBO)
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

		private TGoogleAnalyticsReporteDetalle MapeoEntidad(GoogleAnalyticsReporteDetalleBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TGoogleAnalyticsReporteDetalle entidad = new TGoogleAnalyticsReporteDetalle();
				entidad = Mapper.Map<GoogleAnalyticsReporteDetalleBO, TGoogleAnalyticsReporteDetalle>(objetoBO,
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
