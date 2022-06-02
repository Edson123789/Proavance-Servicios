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
    /// Repositorio: Planificacion/ScrapingPaginaRepositorio
    /// Autor: Ansoli Espinoza
    /// Fecha: 21-01-2021
    /// <summary>
    /// Repositorio de las paginas con Scraping
    /// </summary>
	public class ScrapingPaginaRepositorio : BaseRepository<TScrapingPagina, ScrapingPaginaBO>
	{
		#region Metodos Base
		public ScrapingPaginaRepositorio() : base()
		{
		}
		public ScrapingPaginaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ScrapingPaginaBO> GetBy(Expression<Func<TScrapingPagina, bool>> filter)
		{
			IEnumerable<TScrapingPagina> listado = base.GetBy(filter);
			List<ScrapingPaginaBO> listadoBO = new List<ScrapingPaginaBO>();
			foreach (var itemEntidad in listado)
			{
				ScrapingPaginaBO objetoBO = Mapper.Map<TScrapingPagina, ScrapingPaginaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ScrapingPaginaBO FirstById(int id)
		{
			try
			{
				TScrapingPagina entidad = base.FirstById(id);
				ScrapingPaginaBO objetoBO = new ScrapingPaginaBO();
				Mapper.Map<TScrapingPagina, ScrapingPaginaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ScrapingPaginaBO FirstBy(Expression<Func<TScrapingPagina, bool>> filter)
		{
			try
			{
				TScrapingPagina entidad = base.FirstBy(filter);
				ScrapingPaginaBO objetoBO = Mapper.Map<TScrapingPagina, ScrapingPaginaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ScrapingPaginaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TScrapingPagina entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ScrapingPaginaBO> listadoBO)
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

		public bool Update(ScrapingPaginaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TScrapingPagina entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ScrapingPaginaBO> listadoBO)
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
		private void AsignacionId(TScrapingPagina entidad, ScrapingPaginaBO objetoBO)
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

		private TScrapingPagina MapeoEntidad(ScrapingPaginaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TScrapingPagina entidad = new TScrapingPagina();
				entidad = Mapper.Map<ScrapingPaginaBO, TScrapingPagina>(objetoBO,
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
