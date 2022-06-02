using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
	/// Repositorio: Planificacion/ScrapingEmpleoResultadoClasificacion
	/// Autor: Ansoli Espinoza
	/// Fecha: 09-12-2021
	/// <summary>
	/// Repositorio de la clasificación de Scraping de portales de empleo
	/// </summary>
	public class ScrapingEmpleoResultadoClasificacionRepositorio : BaseRepository<TScrapingEmpleoResultadoClasificacion, ScrapingEmpleoResultadoClasificacionBO>
	{
		#region Metodos Base
		public ScrapingEmpleoResultadoClasificacionRepositorio() : base()
		{
		}
		public ScrapingEmpleoResultadoClasificacionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ScrapingEmpleoResultadoClasificacionBO> GetBy(Expression<Func<TScrapingEmpleoResultadoClasificacion, bool>> filter)
		{
			IEnumerable<TScrapingEmpleoResultadoClasificacion> listado = base.GetBy(filter);
			List<ScrapingEmpleoResultadoClasificacionBO> listadoBO = new List<ScrapingEmpleoResultadoClasificacionBO>();
			foreach (var itemEntidad in listado)
			{
				ScrapingEmpleoResultadoClasificacionBO objetoBO = Mapper.Map<TScrapingEmpleoResultadoClasificacion, ScrapingEmpleoResultadoClasificacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ScrapingEmpleoResultadoClasificacionBO FirstById(int id)
		{
			try
			{
				TScrapingEmpleoResultadoClasificacion entidad = base.FirstById(id);
				ScrapingEmpleoResultadoClasificacionBO objetoBO = new ScrapingEmpleoResultadoClasificacionBO();
				Mapper.Map<TScrapingEmpleoResultadoClasificacion, ScrapingEmpleoResultadoClasificacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ScrapingEmpleoResultadoClasificacionBO FirstBy(Expression<Func<TScrapingEmpleoResultadoClasificacion, bool>> filter)
		{
			try
			{
				TScrapingEmpleoResultadoClasificacion entidad = base.FirstBy(filter);
				ScrapingEmpleoResultadoClasificacionBO objetoBO = Mapper.Map<TScrapingEmpleoResultadoClasificacion, ScrapingEmpleoResultadoClasificacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ScrapingEmpleoResultadoClasificacionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TScrapingEmpleoResultadoClasificacion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ScrapingEmpleoResultadoClasificacionBO> listadoBO)
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

		public bool Update(ScrapingEmpleoResultadoClasificacionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TScrapingEmpleoResultadoClasificacion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ScrapingEmpleoResultadoClasificacionBO> listadoBO)
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
		private void AsignacionId(TScrapingEmpleoResultadoClasificacion entidad, ScrapingEmpleoResultadoClasificacionBO objetoBO)
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

		private TScrapingEmpleoResultadoClasificacion MapeoEntidad(ScrapingEmpleoResultadoClasificacionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TScrapingEmpleoResultadoClasificacion entidad = new TScrapingEmpleoResultadoClasificacion();
				entidad = Mapper.Map<ScrapingEmpleoResultadoClasificacionBO, TScrapingEmpleoResultadoClasificacion>(objetoBO,
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
