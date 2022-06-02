using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
	public class AdwordsApiVolumenBusquedaRepositorio : BaseRepository<TAdwordsApiVolumenBusqueda, AdwordsApiVolumenBusquedaBO>
	{
		#region Metodos Base
		public AdwordsApiVolumenBusquedaRepositorio() : base()
		{
		}
		public AdwordsApiVolumenBusquedaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<AdwordsApiVolumenBusquedaBO> GetBy(Expression<Func<TAdwordsApiVolumenBusqueda, bool>> filter)
		{
			IEnumerable<TAdwordsApiVolumenBusqueda> listado = base.GetBy(filter);
			List<AdwordsApiVolumenBusquedaBO> listadoBO = new List<AdwordsApiVolumenBusquedaBO>();
			foreach (var itemEntidad in listado)
			{
				AdwordsApiVolumenBusquedaBO objetoBO = Mapper.Map<TAdwordsApiVolumenBusqueda, AdwordsApiVolumenBusquedaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public AdwordsApiVolumenBusquedaBO FirstById(int id)
		{
			try
			{
				TAdwordsApiVolumenBusqueda entidad = base.FirstById(id);
				AdwordsApiVolumenBusquedaBO objetoBO = new AdwordsApiVolumenBusquedaBO();
				Mapper.Map<TAdwordsApiVolumenBusqueda, AdwordsApiVolumenBusquedaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public AdwordsApiVolumenBusquedaBO FirstBy(Expression<Func<TAdwordsApiVolumenBusqueda, bool>> filter)
		{
			try
			{
				TAdwordsApiVolumenBusqueda entidad = base.FirstBy(filter);
				AdwordsApiVolumenBusquedaBO objetoBO = Mapper.Map<TAdwordsApiVolumenBusqueda, AdwordsApiVolumenBusquedaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(AdwordsApiVolumenBusquedaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TAdwordsApiVolumenBusqueda entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<AdwordsApiVolumenBusquedaBO> listadoBO)
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

		public bool Update(AdwordsApiVolumenBusquedaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TAdwordsApiVolumenBusqueda entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<AdwordsApiVolumenBusquedaBO> listadoBO)
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
		private void AsignacionId(TAdwordsApiVolumenBusqueda entidad, AdwordsApiVolumenBusquedaBO objetoBO)
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

		private TAdwordsApiVolumenBusqueda MapeoEntidad(AdwordsApiVolumenBusquedaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TAdwordsApiVolumenBusqueda entidad = new TAdwordsApiVolumenBusqueda();
				entidad = Mapper.Map<AdwordsApiVolumenBusquedaBO, TAdwordsApiVolumenBusqueda>(objetoBO,
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

        public List<AdwordsApiVolumenBusquedaHistoricoDTO> ObtenerHistorico(DateTime fechaInicio, DateTime fechaFin, string[] palabras, int idPais)
        {
            try
            {
                fechaFin = fechaFin.AddMonths(-12);
                List<AdwordsApiVolumenBusquedaHistoricoDTO> items = new List<AdwordsApiVolumenBusquedaHistoricoDTO>();
                var query = "SELECT PalabraClave, PromedioBusqueda, Mes, Anho, IdPais FROM mkt.V_TAdwordsApiPalabraClave_ObtenerVolumenBusqueda WHERE PalabraClave in @palabras " +
                    "AND Anho <= @anhomaximo AND (Mes <= @mesmaximo OR Anho < @anhomaximo)  AND  Anho >= @anhominimo AND (Mes >= @mesminimo OR Anho > @anhominimo) AND IdPais = @idPais ";
                var queryRespuesta = _dapper.QueryDapper(query, new { palabras, anhomaximo = fechaFin.Year, mesmaximo = fechaFin.Month, anhominimo = fechaInicio.Year, mesminimo= fechaInicio.Month, idPais });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<AdwordsApiVolumenBusquedaHistoricoDTO>>(queryRespuesta);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
