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
	/// Repositorio: Planificacion/ScrapingPortalEmpleoResultado
	/// Autor: Ansoli Espinoza
	/// Fecha: 18-01-2021
	/// <summary>
	/// Repositorio del Scraping de portales de empleo
	/// </summary>
	public class ScrapingPortalEmpleoResultadoRepositorio : BaseRepository<TScrapingPortalEmpleoResultado, ScrapingPortalEmpleoResultadoBO>
	{
		#region Metodos Base
		public ScrapingPortalEmpleoResultadoRepositorio() : base()
		{
		}
		public ScrapingPortalEmpleoResultadoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ScrapingPortalEmpleoResultadoBO> GetBy(Expression<Func<TScrapingPortalEmpleoResultado, bool>> filter)
		{
			IEnumerable<TScrapingPortalEmpleoResultado> listado = base.GetBy(filter);
			List<ScrapingPortalEmpleoResultadoBO> listadoBO = new List<ScrapingPortalEmpleoResultadoBO>();
			foreach (var itemEntidad in listado)
			{
				ScrapingPortalEmpleoResultadoBO objetoBO = Mapper.Map<TScrapingPortalEmpleoResultado, ScrapingPortalEmpleoResultadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ScrapingPortalEmpleoResultadoBO FirstById(int id)
		{
			try
			{
				TScrapingPortalEmpleoResultado entidad = base.FirstById(id);
				ScrapingPortalEmpleoResultadoBO objetoBO = new ScrapingPortalEmpleoResultadoBO();
				Mapper.Map<TScrapingPortalEmpleoResultado, ScrapingPortalEmpleoResultadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ScrapingPortalEmpleoResultadoBO FirstBy(Expression<Func<TScrapingPortalEmpleoResultado, bool>> filter)
		{
			try
			{
				TScrapingPortalEmpleoResultado entidad = base.FirstBy(filter);
				ScrapingPortalEmpleoResultadoBO objetoBO = Mapper.Map<TScrapingPortalEmpleoResultado, ScrapingPortalEmpleoResultadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ScrapingPortalEmpleoResultadoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TScrapingPortalEmpleoResultado entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ScrapingPortalEmpleoResultadoBO> listadoBO)
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

		public bool Update(ScrapingPortalEmpleoResultadoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TScrapingPortalEmpleoResultado entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ScrapingPortalEmpleoResultadoBO> listadoBO)
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
		private void AsignacionId(TScrapingPortalEmpleoResultado entidad, ScrapingPortalEmpleoResultadoBO objetoBO)
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

		private TScrapingPortalEmpleoResultado MapeoEntidad(ScrapingPortalEmpleoResultadoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TScrapingPortalEmpleoResultado entidad = new TScrapingPortalEmpleoResultado();
				entidad = Mapper.Map<ScrapingPortalEmpleoResultadoBO, TScrapingPortalEmpleoResultado>(objetoBO,
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

		/// Autor: Ansoli Espinoza
		/// Fecha: 18-01-2021
		/// Version: 1.0
		/// <summary>
		/// Obtiene las Cabeceras del scraping de Portales de Empleo
		/// </summary>
        /// <param name="filtro">Filtro del formulario</param>
        /// <returns>Listado de las Cabeceras del scraping de Portales de Empleo</returns>
		public List<ScrapinPortalEmpleoCabeceraDTO> ObtenerCabeceras(FiltroScrapingPortalEmpleoDTO filtro)
        {
            try
            {
                List<ScrapinPortalEmpleoCabeceraDTO> listado = new List<ScrapinPortalEmpleoCabeceraDTO>();
                var query = string.Empty;
                query =
					"SELECT Id, IdScrapingPagina, NombrePortal, TituloAnuncio, FechaAnuncio, Puesto, Empresa, Salario FROM pla.V_ScrapinPortalEmpleo_ObtenerCabeceras " +
                    "WHERE CONVERT(date, FechaAnuncio) >= CONVERT(date, @FechaInicio) AND CONVERT(date, FechaAnuncio) <= CONVERT(date, @FechaFin)";
                if (filtro.IdScrapingPagina != null && filtro.IdScrapingPagina.Count > 0)
                {
                    filtro.Puesto = filtro.Puesto.Trim();
                    query += " AND IdScrapingPagina in @IdScrapingPagina ";
                }

                if (!string.IsNullOrEmpty(filtro.Puesto))
                {
                    filtro.Puesto = filtro.Puesto.Trim();
                    query += " AND Puesto LIKE @Puesto ";
                }
                if (!string.IsNullOrEmpty(filtro.Descripcion))
                {
                    filtro.Descripcion = filtro.Descripcion.Trim();
                    query += " AND Descripcion LIKE @Descripcion ";
                }

                bool? parametroClasificacion = null;
                if (filtro.EstadoClasificacion != null && filtro.EstadoClasificacion > 0)
                {
                    parametroClasificacion = filtro.EstadoClasificacion == 0 ? false : true;
                    query += " AND EsClasificado = @EsClasificado ";
                }

				var resultado = _dapper.QueryDapper(query,
                    new
                    {
                        FechaInicio = filtro.FechaInicio, FechaFin = filtro.FechaFin,
                        IdScrapingPagina = filtro.IdScrapingPagina,
                        Puesto = "%" + filtro.Puesto + "%",
                        Descripcion = "%" + filtro.Descripcion + "%",
                        EsClasificado = parametroClasificacion
					});
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listado = JsonConvert.DeserializeObject<List<ScrapinPortalEmpleoCabeceraDTO>>(resultado);
                }

                return listado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

		/// Autor: Ansoli Espinoza
		/// Fecha: 18-01-2021
		/// Version: 1.0
        /// <summary>
        /// Obtiene el detalle de el Scraping de portal de Empleo por el Id enviado
        /// </summary>
        /// <param name="id">Id del Anuncio</param>
        /// <returns>El detalle de scraping de Portal de Empleo consultado</returns>
		public ScrapinPortalEmpleoDetalleDTO ObtenerDetalle(int id)
        {
            try
            {
                ScrapinPortalEmpleoDetalleDTO listado = new ScrapinPortalEmpleoDetalleDTO();
                var query = string.Empty;
                query =
                    "SELECT Id, IdScrapingPagina, NombrePortal, TituloAnuncio, Url, FechaAnuncio, Puesto, Empresa, Ubicacion, Jornada, " +
                    "TipoContrato, Salario, Descripcion, DescripcionHTML, Error, Modalidad " +
					"FROM pla.V_ScrapinPortalEmpleo_ObtenerDetalle WHERE Id = @Id";
                var resultado = _dapper.FirstOrDefault(query, new {Id = id});
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listado = JsonConvert.DeserializeObject<ScrapinPortalEmpleoDetalleDTO>(resultado);
                }

                return listado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

		/// <summary>
		/// Obtiene el listado de Clsificaciones de un anuncio por Id
		/// </summary>
		/// <param name="id">Id del anuncio</param>
		/// <returns>Lista de ScrapinPortalEmpleoClasificacionAgrupadaDTO</returns>
		/// <exception cref="Exception"></exception>
		public List<ScrapinPortalEmpleoClasificacionAgrupadaDTO> ObtenerClasificacionAgrupada(int id)
        {
			try
            {
                List<ScrapinPortalEmpleoClasificacionAgrupadaDTO> listado = new List<ScrapinPortalEmpleoClasificacionAgrupadaDTO>();
                var query = string.Empty;
                query =
					@"SELECT IdScrapingPortalEmpleoResultado, IdAreaTrabajo, AreaTrabajo, IdAreaFormacion, AreaFormacion, IdCargo, Cargo, IdIndustria, Industria
					FROM pla.V_ScrapingEmpleo_DetalleAgrupadoClasificacionAnuncio 
                    WHERE IdScrapingPortalEmpleoResultado = @IdScrapingPortalEmpleoResultado";
                var resultado = _dapper.QueryDapper(query, new { IdScrapingPortalEmpleoResultado = id });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listado = JsonConvert.DeserializeObject<List<ScrapinPortalEmpleoClasificacionAgrupadaDTO>>(resultado);
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
