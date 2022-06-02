using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
	public class RaPresencialBoletoAereoDetalleRepositorio : BaseRepository<TRaPresencialBoletoAereoDetalle, RaPresencialBoletoAereoDetalleBO>
	{
		#region Metodos Base
		public RaPresencialBoletoAereoDetalleRepositorio() : base()
		{
		}
		public RaPresencialBoletoAereoDetalleRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<RaPresencialBoletoAereoDetalleBO> GetBy(Expression<Func<TRaPresencialBoletoAereoDetalle, bool>> filter)
		{
			IEnumerable<TRaPresencialBoletoAereoDetalle> listado = base.GetBy(filter);
			List<RaPresencialBoletoAereoDetalleBO> listadoBO = new List<RaPresencialBoletoAereoDetalleBO>();
			foreach (var itemEntidad in listado)
			{
				RaPresencialBoletoAereoDetalleBO objetoBO = Mapper.Map<TRaPresencialBoletoAereoDetalle, RaPresencialBoletoAereoDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public RaPresencialBoletoAereoDetalleBO FirstById(int id)
		{
			try
			{
				TRaPresencialBoletoAereoDetalle entidad = base.FirstById(id);
				RaPresencialBoletoAereoDetalleBO objetoBO = new RaPresencialBoletoAereoDetalleBO();
				Mapper.Map<TRaPresencialBoletoAereoDetalle, RaPresencialBoletoAereoDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public RaPresencialBoletoAereoDetalleBO FirstBy(Expression<Func<TRaPresencialBoletoAereoDetalle, bool>> filter)
		{
			try
			{
				TRaPresencialBoletoAereoDetalle entidad = base.FirstBy(filter);
				RaPresencialBoletoAereoDetalleBO objetoBO = Mapper.Map<TRaPresencialBoletoAereoDetalle, RaPresencialBoletoAereoDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(RaPresencialBoletoAereoDetalleBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TRaPresencialBoletoAereoDetalle entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<RaPresencialBoletoAereoDetalleBO> listadoBO)
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

		public bool Update(RaPresencialBoletoAereoDetalleBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TRaPresencialBoletoAereoDetalle entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<RaPresencialBoletoAereoDetalleBO> listadoBO)
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
		private void AsignacionId(TRaPresencialBoletoAereoDetalle entidad, RaPresencialBoletoAereoDetalleBO objetoBO)
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

		private TRaPresencialBoletoAereoDetalle MapeoEntidad(RaPresencialBoletoAereoDetalleBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TRaPresencialBoletoAereoDetalle entidad = new TRaPresencialBoletoAereoDetalle();
				entidad = Mapper.Map<RaPresencialBoletoAereoDetalleBO, TRaPresencialBoletoAereoDetalle>(objetoBO,
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
		public List<BoletoAereoDetalleDTO> ObtenerBoletoAereoDetalle(int idBoletoAereo)
		{
			try
			{
				string query = "SELECT Id, IdRaPresencialBoletoAereo, IdRaAerolinea, Aerolinea, Fecha, Origen, HoraSalida, Destino, HoraLlegada, NumeroVuelo, Tipo, IdRaTipoBoletoAereo, CodigoReserva FROM ope.V_DatosBoletoAereoDetalle WHERE Estado = 1 AND IdRaPresencialBoletoAereo = @IdBoletoAereo";
				var detalleBoletoAereo = _dapper.QueryDapper(query, new { IdBoletoAereo = idBoletoAereo });
				return JsonConvert.DeserializeObject<List<BoletoAereoDetalleDTO>>(detalleBoletoAereo);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
