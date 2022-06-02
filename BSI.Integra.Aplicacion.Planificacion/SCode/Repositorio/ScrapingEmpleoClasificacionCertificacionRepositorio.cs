using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class ScrapingEmpleoClasificacionCertificacionRepositorio : BaseRepository<TScrapingEmpleoClasificacionCertificacion, ScrapingEmpleoClasificacionCertificacionBO>    
    {
		#region Metodos Base
		public ScrapingEmpleoClasificacionCertificacionRepositorio() : base()
		{
		}
		public ScrapingEmpleoClasificacionCertificacionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ScrapingEmpleoClasificacionCertificacionBO> GetBy(Expression<Func<TScrapingEmpleoClasificacionCertificacion, bool>> filter)
		{
			IEnumerable<TScrapingEmpleoClasificacionCertificacion> listado = base.GetBy(filter);
			List<ScrapingEmpleoClasificacionCertificacionBO> listadoBO = new List<ScrapingEmpleoClasificacionCertificacionBO>();
			foreach (var itemEntidad in listado)
			{
				ScrapingEmpleoClasificacionCertificacionBO objetoBO = Mapper.Map<TScrapingEmpleoClasificacionCertificacion, ScrapingEmpleoClasificacionCertificacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ScrapingEmpleoClasificacionCertificacionBO FirstById(int id)
		{
			try
			{
				TScrapingEmpleoClasificacionCertificacion entidad = base.FirstById(id);
				ScrapingEmpleoClasificacionCertificacionBO objetoBO = new ScrapingEmpleoClasificacionCertificacionBO();
				Mapper.Map<TScrapingEmpleoClasificacionCertificacion, ScrapingEmpleoClasificacionCertificacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ScrapingEmpleoClasificacionCertificacionBO FirstBy(Expression<Func<TScrapingEmpleoClasificacionCertificacion, bool>> filter)
		{
			try
			{
				TScrapingEmpleoClasificacionCertificacion entidad = base.FirstBy(filter);
				ScrapingEmpleoClasificacionCertificacionBO objetoBO = Mapper.Map<TScrapingEmpleoClasificacionCertificacion, ScrapingEmpleoClasificacionCertificacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ScrapingEmpleoClasificacionCertificacionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TScrapingEmpleoClasificacionCertificacion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ScrapingEmpleoClasificacionCertificacionBO> listadoBO)
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

		public bool Update(ScrapingEmpleoClasificacionCertificacionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TScrapingEmpleoClasificacionCertificacion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ScrapingEmpleoClasificacionCertificacionBO> listadoBO)
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
		private void AsignacionId(TScrapingEmpleoClasificacionCertificacion entidad, ScrapingEmpleoClasificacionCertificacionBO objetoBO)
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

		private TScrapingEmpleoClasificacionCertificacion MapeoEntidad(ScrapingEmpleoClasificacionCertificacionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TScrapingEmpleoClasificacionCertificacion entidad = new TScrapingEmpleoClasificacionCertificacion();
				entidad = Mapper.Map<ScrapingEmpleoClasificacionCertificacionBO, TScrapingEmpleoClasificacionCertificacion>(objetoBO,
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
