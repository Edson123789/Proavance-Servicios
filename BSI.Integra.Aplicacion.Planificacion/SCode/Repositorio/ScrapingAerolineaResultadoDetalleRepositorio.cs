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
	public class ScrapingAerolineaResultadoDetalleRepositorio : BaseRepository<TScrapingAerolineaResultadoDetalle, ScrapingAerolineaResultadoDetalleBO>
	{
		#region Metodos Base
		public ScrapingAerolineaResultadoDetalleRepositorio() : base()
		{
		}
		public ScrapingAerolineaResultadoDetalleRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ScrapingAerolineaResultadoDetalleBO> GetBy(Expression<Func<TScrapingAerolineaResultadoDetalle, bool>> filter)
		{
			IEnumerable<TScrapingAerolineaResultadoDetalle> listado = base.GetBy(filter);
			List<ScrapingAerolineaResultadoDetalleBO> listadoBO = new List<ScrapingAerolineaResultadoDetalleBO>();
			foreach (var itemEntidad in listado)
			{
				ScrapingAerolineaResultadoDetalleBO objetoBO = Mapper.Map<TScrapingAerolineaResultadoDetalle, ScrapingAerolineaResultadoDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ScrapingAerolineaResultadoDetalleBO FirstById(int id)
		{
			try
			{
				TScrapingAerolineaResultadoDetalle entidad = base.FirstById(id);
				ScrapingAerolineaResultadoDetalleBO objetoBO = new ScrapingAerolineaResultadoDetalleBO();
				Mapper.Map<TScrapingAerolineaResultadoDetalle, ScrapingAerolineaResultadoDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ScrapingAerolineaResultadoDetalleBO FirstBy(Expression<Func<TScrapingAerolineaResultadoDetalle, bool>> filter)
		{
			try
			{
				TScrapingAerolineaResultadoDetalle entidad = base.FirstBy(filter);
				ScrapingAerolineaResultadoDetalleBO objetoBO = Mapper.Map<TScrapingAerolineaResultadoDetalle, ScrapingAerolineaResultadoDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ScrapingAerolineaResultadoDetalleBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TScrapingAerolineaResultadoDetalle entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ScrapingAerolineaResultadoDetalleBO> listadoBO)
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

		public bool Update(ScrapingAerolineaResultadoDetalleBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TScrapingAerolineaResultadoDetalle entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ScrapingAerolineaResultadoDetalleBO> listadoBO)
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
		private void AsignacionId(TScrapingAerolineaResultadoDetalle entidad, ScrapingAerolineaResultadoDetalleBO objetoBO)
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

		private TScrapingAerolineaResultadoDetalle MapeoEntidad(ScrapingAerolineaResultadoDetalleBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TScrapingAerolineaResultadoDetalle entidad = new TScrapingAerolineaResultadoDetalle();
				entidad = Mapper.Map<ScrapingAerolineaResultadoDetalleBO, TScrapingAerolineaResultadoDetalle>(objetoBO,
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
		/// Este metodo obtiene todos los vuelos encontrados por scraping, mediante IdScrapingAerolineaResultado
		/// </summary>
		/// <param name="idPespecifico"></param>
		/// <param name="grupoSesion"></param>
		/// <returns></returns>
		public List<ScrapingAerolineaResultadoDetalleDTO> ObtenerScrapingAerolineaResultadoDetalle(int idScrapingAerolineaResultado)
		{
			try
			{
				var query = "SELECT Id, IdScrapingAerolineaResultado, NroVuelo, IdProveedor, NombreAerolinea, IdVueloTipoTramo, VueloTipoTramo, IdCiudadOrigen, IdCiudadDestino, EsIda, FechaSalida, FechaLlegada, Clase, AplicaMochila, AplicaEquipajeMano, AplicaEquipajeBodega, DuracionMinuto, CodigoCiudadOrigen, CodigoCiudadDestino FROM pla.V_ScrapingAerolineaResultadoDetalle_ObtenerDetalles WHERE Estado = 1 AND IdScrapingAerolineaResultado = @IdScrapingAerolineaResultado";
				var res = _dapper.QueryDapper(query, new { IdScrapingAerolineaResultado = idScrapingAerolineaResultado });

				return JsonConvert.DeserializeObject<List<ScrapingAerolineaResultadoDetalleDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public List<ScrapingAerolineaResultadoDetalleEscalaDTO> ObtenerDetalleEscalas(int idPadre)
		{
			try
			{
				var query = "SELECT Id,IdPadre,IdScrapingAerolineaResultado,NroVuelo,NombreAerolinea,IdProveedor,NombreCiudadOrigen,NombreCiudadDestino,FechaSalida,FechaLlegada,Clase,DuracionMinuto FROM pla.V_TScrapingAerolineaResultadoDetalle_ObtenerEscalas WHERE Estado = 1 AND IdPadre = @IdPadre";
				var tmp = _dapper.QueryDapper(query, new { IdPadre = idPadre});
				var res = JsonConvert.DeserializeObject<List<ScrapingAerolineaResultadoDetalleEscalaDTO>>(tmp);
				return res;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
