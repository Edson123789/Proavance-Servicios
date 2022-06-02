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
	/// Repositorio: Planificacion/ScrapingEmpleoPatronClasificacion
	/// Autor: Ansoli Espinoza
	/// Fecha: 10-12-2021
	/// <summary>
	/// Repositorio de la clasificación de los anuncios del Scraping de portales de empleo
	/// </summary>
	public class ScrapingEmpleoPatronClasificacionRepositorio : BaseRepository<TScrapingEmpleoPatronClasificacion, ScrapingEmpleoPatronClasificacionBO>
	{
		#region Metodos Base
		public ScrapingEmpleoPatronClasificacionRepositorio() : base()
		{
		}
		public ScrapingEmpleoPatronClasificacionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ScrapingEmpleoPatronClasificacionBO> GetBy(Expression<Func<TScrapingEmpleoPatronClasificacion, bool>> filter)
		{
			IEnumerable<TScrapingEmpleoPatronClasificacion> listado = base.GetBy(filter);
			List<ScrapingEmpleoPatronClasificacionBO> listadoBO = new List<ScrapingEmpleoPatronClasificacionBO>();
			foreach (var itemEntidad in listado)
			{
				ScrapingEmpleoPatronClasificacionBO objetoBO = Mapper.Map<TScrapingEmpleoPatronClasificacion, ScrapingEmpleoPatronClasificacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ScrapingEmpleoPatronClasificacionBO FirstById(int id)
		{
			try
			{
				TScrapingEmpleoPatronClasificacion entidad = base.FirstById(id);
				ScrapingEmpleoPatronClasificacionBO objetoBO = new ScrapingEmpleoPatronClasificacionBO();
				Mapper.Map<TScrapingEmpleoPatronClasificacion, ScrapingEmpleoPatronClasificacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ScrapingEmpleoPatronClasificacionBO FirstBy(Expression<Func<TScrapingEmpleoPatronClasificacion, bool>> filter)
		{
			try
			{
				TScrapingEmpleoPatronClasificacion entidad = base.FirstBy(filter);
				ScrapingEmpleoPatronClasificacionBO objetoBO = Mapper.Map<TScrapingEmpleoPatronClasificacion, ScrapingEmpleoPatronClasificacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ScrapingEmpleoPatronClasificacionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TScrapingEmpleoPatronClasificacion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ScrapingEmpleoPatronClasificacionBO> listadoBO)
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

		public bool Update(ScrapingEmpleoPatronClasificacionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TScrapingEmpleoPatronClasificacion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ScrapingEmpleoPatronClasificacionBO> listadoBO)
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
		private void AsignacionId(TScrapingEmpleoPatronClasificacion entidad, ScrapingEmpleoPatronClasificacionBO objetoBO)
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

		private TScrapingEmpleoPatronClasificacion MapeoEntidad(ScrapingEmpleoPatronClasificacionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TScrapingEmpleoPatronClasificacion entidad = new TScrapingEmpleoPatronClasificacion();
				entidad = Mapper.Map<ScrapingEmpleoPatronClasificacionBO, TScrapingEmpleoPatronClasificacion>(objetoBO,
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
		/// Busca los patrocnes de clasificacion que coinciden con los parametros de busqueda
		/// </summary>
		/// <param name="busqueda">Parametros de busqueda</param>
		/// <returns>Lista de PatronClasificacionEmpleoConsultaDTO</returns>
		/// <exception cref="Exception"></exception>
		public List<PatronClasificacionEmpleoConsultaDTO> BuscarPatron(PatronClasificacionEmpleoBuscarDTO busqueda)
        {
			try
			{
                List<PatronClasificacionEmpleoConsultaDTO> listado = new List<PatronClasificacionEmpleoConsultaDTO>();
                var query = string.Empty;
                query =
					@"SELECT Id, Tipo, IdItem, Item, IdAreaFormacion, IdAreaTrabajo, IdCargo, IdIndustria, Patron
					FROM pla.V_ScrapingEmpleo_DetallePatronClasificacionAnuncio 
                    WHERE Tipo = @Tipo";
                if (busqueda.IdItem != null)
                    query += " AND IdItem = @IdItem";

				var resultado = _dapper.QueryDapper(query, new { Tipo = busqueda.Tipo, IdItem = busqueda.IdItem });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listado = JsonConvert.DeserializeObject<List<PatronClasificacionEmpleoConsultaDTO>>(resultado);
                }

                return listado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
		}
    }
}
