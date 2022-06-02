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
	public class ScrapingAerolineaConfiguracionRepositorio : BaseRepository<TScrapingAerolineaConfiguracion, ScrapingAerolineaConfiguracionBO>
	{
		#region Metodos Base
		public ScrapingAerolineaConfiguracionRepositorio() : base()
		{
		}
		public ScrapingAerolineaConfiguracionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ScrapingAerolineaConfiguracionBO> GetBy(Expression<Func<TScrapingAerolineaConfiguracion, bool>> filter)
		{
			IEnumerable<TScrapingAerolineaConfiguracion> listado = base.GetBy(filter);
			List<ScrapingAerolineaConfiguracionBO> listadoBO = new List<ScrapingAerolineaConfiguracionBO>();
			foreach (var itemEntidad in listado)
			{
				ScrapingAerolineaConfiguracionBO objetoBO = Mapper.Map<TScrapingAerolineaConfiguracion, ScrapingAerolineaConfiguracionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ScrapingAerolineaConfiguracionBO FirstById(int id)
		{
			try
			{
				TScrapingAerolineaConfiguracion entidad = base.FirstById(id);
				ScrapingAerolineaConfiguracionBO objetoBO = new ScrapingAerolineaConfiguracionBO();
				Mapper.Map<TScrapingAerolineaConfiguracion, ScrapingAerolineaConfiguracionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ScrapingAerolineaConfiguracionBO FirstBy(Expression<Func<TScrapingAerolineaConfiguracion, bool>> filter)
		{
			try
			{
				TScrapingAerolineaConfiguracion entidad = base.FirstBy(filter);
				ScrapingAerolineaConfiguracionBO objetoBO = Mapper.Map<TScrapingAerolineaConfiguracion, ScrapingAerolineaConfiguracionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ScrapingAerolineaConfiguracionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TScrapingAerolineaConfiguracion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ScrapingAerolineaConfiguracionBO> listadoBO)
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

		public bool Update(ScrapingAerolineaConfiguracionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TScrapingAerolineaConfiguracion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ScrapingAerolineaConfiguracionBO> listadoBO)
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
		private void AsignacionId(TScrapingAerolineaConfiguracion entidad, ScrapingAerolineaConfiguracionBO objetoBO)
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

		private TScrapingAerolineaConfiguracion MapeoEntidad(ScrapingAerolineaConfiguracionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TScrapingAerolineaConfiguracion entidad = new TScrapingAerolineaConfiguracion();
				entidad = Mapper.Map<ScrapingAerolineaConfiguracionBO, TScrapingAerolineaConfiguracion>(objetoBO,
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
		/// Obtiene onfiguracion de scraping aerolinea mediante el id de configuracion
		/// </summary>
		/// <returns></returns>
		public ScrapingConfiguracionDTO obtenerConfiguracionScraping(int id)
		{
			try
			{
				var query = "SELECT Id, IdCentroCosto, NombreCentroCosto, NroGrupoSesion, NroGrupoCronograma, FechaHoraInicio, FechaHoraFin, IdScrapingAerolineaEstadoConsulta, EstadoConsulta, IdCiudadOrigen, CiudadOrigen, IdCiudadDestino, CiudadDestino, IdCiudadAeropuertoOrigen, CiudadAeropuertoOrigen, IdCiudadAeropuertoDestino, CiudadAeropuertoDestino, FechaIdaProgramada, FechaRetornoProgramada, TipoVuelo, NroFrecuencia, TipoFrecuencia, PrecisionIda, PrecisionRetorno, TienePasajeComprado FROM [pla].[V_ListaConfiguracionScraping] WHERE Estado = 1 AND Id = @id";
				var dapper = _dapper.FirstOrDefault(query, new { Id = id });
				var res = JsonConvert.DeserializeObject<ScrapingConfiguracionDTO>(dapper);
				return res;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
