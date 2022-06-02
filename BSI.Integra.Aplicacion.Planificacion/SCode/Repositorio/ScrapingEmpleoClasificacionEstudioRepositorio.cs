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
    public class ScrapingEmpleoClasificacionEstudioRepositorio : BaseRepository<TScrapingEmpleoClasificacionEstudio, ScrapingEmpleoClasificacionEstudioBO>
    {
		#region Metodos Base
		public ScrapingEmpleoClasificacionEstudioRepositorio() : base()
		{
		}
		public ScrapingEmpleoClasificacionEstudioRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ScrapingEmpleoClasificacionEstudioBO> GetBy(Expression<Func<TScrapingEmpleoClasificacionEstudio, bool>> filter)
		{
			IEnumerable<TScrapingEmpleoClasificacionEstudio> listado = base.GetBy(filter);
			List<ScrapingEmpleoClasificacionEstudioBO> listadoBO = new List<ScrapingEmpleoClasificacionEstudioBO>();
			foreach (var itemEntidad in listado)
			{
				ScrapingEmpleoClasificacionEstudioBO objetoBO = Mapper.Map<TScrapingEmpleoClasificacionEstudio, ScrapingEmpleoClasificacionEstudioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ScrapingEmpleoClasificacionEstudioBO FirstById(int id)
		{
			try
			{
				TScrapingEmpleoClasificacionEstudio entidad = base.FirstById(id);
				ScrapingEmpleoClasificacionEstudioBO objetoBO = new ScrapingEmpleoClasificacionEstudioBO();
				Mapper.Map<TScrapingEmpleoClasificacionEstudio, ScrapingEmpleoClasificacionEstudioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ScrapingEmpleoClasificacionEstudioBO FirstBy(Expression<Func<TScrapingEmpleoClasificacionEstudio, bool>> filter)
		{
			try
			{
				TScrapingEmpleoClasificacionEstudio entidad = base.FirstBy(filter);
				ScrapingEmpleoClasificacionEstudioBO objetoBO = Mapper.Map<TScrapingEmpleoClasificacionEstudio, ScrapingEmpleoClasificacionEstudioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ScrapingEmpleoClasificacionEstudioBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TScrapingEmpleoClasificacionEstudio entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ScrapingEmpleoClasificacionEstudioBO> listadoBO)
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

		public bool Update(ScrapingEmpleoClasificacionEstudioBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TScrapingEmpleoClasificacionEstudio entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ScrapingEmpleoClasificacionEstudioBO> listadoBO)
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
		private void AsignacionId(TScrapingEmpleoClasificacionEstudio entidad, ScrapingEmpleoClasificacionEstudioBO objetoBO)
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

		private TScrapingEmpleoClasificacionEstudio MapeoEntidad(ScrapingEmpleoClasificacionEstudioBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TScrapingEmpleoClasificacionEstudio entidad = new TScrapingEmpleoClasificacionEstudio();
				entidad = Mapper.Map<ScrapingEmpleoClasificacionEstudioBO, TScrapingEmpleoClasificacionEstudio>(objetoBO,
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
