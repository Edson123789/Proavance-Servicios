using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
	public class ScrapingAerolineaResultadoRepositorio : BaseRepository<TScrapingAerolineaResultado, ScrapingAerolineaResultadoBO>
	{
		#region Metodos Base
		public ScrapingAerolineaResultadoRepositorio() : base()
		{
		}
		public ScrapingAerolineaResultadoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ScrapingAerolineaResultadoBO> GetBy(Expression<Func<TScrapingAerolineaResultado, bool>> filter)
		{
			IEnumerable<TScrapingAerolineaResultado> listado = base.GetBy(filter);
			List<ScrapingAerolineaResultadoBO> listadoBO = new List<ScrapingAerolineaResultadoBO>();
			foreach (var itemEntidad in listado)
			{
				ScrapingAerolineaResultadoBO objetoBO = Mapper.Map<TScrapingAerolineaResultado, ScrapingAerolineaResultadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ScrapingAerolineaResultadoBO FirstById(int id)
		{
			try
			{
				TScrapingAerolineaResultado entidad = base.FirstById(id);
				ScrapingAerolineaResultadoBO objetoBO = new ScrapingAerolineaResultadoBO();
				Mapper.Map<TScrapingAerolineaResultado, ScrapingAerolineaResultadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ScrapingAerolineaResultadoBO FirstBy(Expression<Func<TScrapingAerolineaResultado, bool>> filter)
		{
			try
			{
				TScrapingAerolineaResultado entidad = base.FirstBy(filter);
				ScrapingAerolineaResultadoBO objetoBO = Mapper.Map<TScrapingAerolineaResultado, ScrapingAerolineaResultadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ScrapingAerolineaResultadoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TScrapingAerolineaResultado entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ScrapingAerolineaResultadoBO> listadoBO)
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

		public bool Update(ScrapingAerolineaResultadoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TScrapingAerolineaResultado entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ScrapingAerolineaResultadoBO> listadoBO)
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
		private void AsignacionId(TScrapingAerolineaResultado entidad, ScrapingAerolineaResultadoBO objetoBO)
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

		private TScrapingAerolineaResultado MapeoEntidad(ScrapingAerolineaResultadoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TScrapingAerolineaResultado entidad = new TScrapingAerolineaResultado();
				entidad = Mapper.Map<ScrapingAerolineaResultadoBO, TScrapingAerolineaResultado>(objetoBO,
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
		/// Este metodo obtiene todos los vuelos encontrados por scraping, mediante IdPespecifico y GrupoSesion
		/// </summary>
		/// <param name="idPespecifico"></param>
		/// <param name="grupoSesion"></param>
		/// <returns></returns>
		public List<ScrapingAerolineaResultadoDTO> ObtenerScrapingAerolineaResultado(int idScrapingAerolineaConfiguracion)
		{
			try
			{
				var query = "SELECT Id, IdScrapingAerolineaConfiguracion, Precio, IdScrapingPagina, IdPespecifico, IdCentroCosto, NroSesionGrupo, NroGrupoCronograma, IdCiudadOrigen, IdCiudadDestino, EsActual, FechaIda, FechaRetorno, CodigoCiudadOrigen, CodigoCiudadDestino FROM pla.V_ScrapingAerolineaResultado_ObtenerResultados WHERE Estado = 1 AND IdScrapingAerolineaConfiguracion = @idScrapingAerolineaConfiguracion";
				var res = _dapper.QueryDapper(query, new { IdScrapingAerolineaConfiguracion = idScrapingAerolineaConfiguracion });

				return JsonConvert.DeserializeObject<List<ScrapingAerolineaResultadoDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
