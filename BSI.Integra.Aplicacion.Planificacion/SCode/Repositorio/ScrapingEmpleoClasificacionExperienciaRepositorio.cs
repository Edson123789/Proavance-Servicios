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
    public class ScrapingEmpleoClasificacionExperienciaRepositorio : BaseRepository<TScrapingEmpleoClasificacionExperiencia, ScrapingEmpleoClasificacionExperienciaBO>
    {
		#region Metodos Base
		public ScrapingEmpleoClasificacionExperienciaRepositorio() : base()
		{
		}
		public ScrapingEmpleoClasificacionExperienciaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ScrapingEmpleoClasificacionExperienciaBO> GetBy(Expression<Func<TScrapingEmpleoClasificacionExperiencia, bool>> filter)
		{
			IEnumerable<TScrapingEmpleoClasificacionExperiencia> listado = base.GetBy(filter);
			List<ScrapingEmpleoClasificacionExperienciaBO> listadoBO = new List<ScrapingEmpleoClasificacionExperienciaBO>();
			foreach (var itemEntidad in listado)
			{
				ScrapingEmpleoClasificacionExperienciaBO objetoBO = Mapper.Map<TScrapingEmpleoClasificacionExperiencia, ScrapingEmpleoClasificacionExperienciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ScrapingEmpleoClasificacionExperienciaBO FirstById(int id)
		{
			try
			{
				TScrapingEmpleoClasificacionExperiencia entidad = base.FirstById(id);
				ScrapingEmpleoClasificacionExperienciaBO objetoBO = new ScrapingEmpleoClasificacionExperienciaBO();
				Mapper.Map<TScrapingEmpleoClasificacionExperiencia, ScrapingEmpleoClasificacionExperienciaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ScrapingEmpleoClasificacionExperienciaBO FirstBy(Expression<Func<TScrapingEmpleoClasificacionExperiencia, bool>> filter)
		{
			try
			{
				TScrapingEmpleoClasificacionExperiencia entidad = base.FirstBy(filter);
				ScrapingEmpleoClasificacionExperienciaBO objetoBO = Mapper.Map<TScrapingEmpleoClasificacionExperiencia, ScrapingEmpleoClasificacionExperienciaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ScrapingEmpleoClasificacionExperienciaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TScrapingEmpleoClasificacionExperiencia entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ScrapingEmpleoClasificacionExperienciaBO> listadoBO)
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

		public bool Update(ScrapingEmpleoClasificacionExperienciaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TScrapingEmpleoClasificacionExperiencia entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ScrapingEmpleoClasificacionExperienciaBO> listadoBO)
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
		private void AsignacionId(TScrapingEmpleoClasificacionExperiencia entidad, ScrapingEmpleoClasificacionExperienciaBO objetoBO)
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

		private TScrapingEmpleoClasificacionExperiencia MapeoEntidad(ScrapingEmpleoClasificacionExperienciaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TScrapingEmpleoClasificacionExperiencia entidad = new TScrapingEmpleoClasificacionExperiencia();
				entidad = Mapper.Map<ScrapingEmpleoClasificacionExperienciaBO, TScrapingEmpleoClasificacionExperiencia>(objetoBO,
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
