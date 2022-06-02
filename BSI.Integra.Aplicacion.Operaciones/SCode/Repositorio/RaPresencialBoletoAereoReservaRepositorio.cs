using System;
using System.Collections.Generic;
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
	public class RaPresencialBoletoAereoReservaRepositorio : BaseRepository<TRaPresencialBoletoAereoReserva, RaPresencialBoletoAereoReservaBO>
	{
		#region Metodos Base
		public RaPresencialBoletoAereoReservaRepositorio() : base()
		{
		}
		public RaPresencialBoletoAereoReservaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<RaPresencialBoletoAereoReservaBO> GetBy(Expression<Func<TRaPresencialBoletoAereoReserva, bool>> filter)
		{
			IEnumerable<TRaPresencialBoletoAereoReserva> listado = base.GetBy(filter);
			List<RaPresencialBoletoAereoReservaBO> listadoBO = new List<RaPresencialBoletoAereoReservaBO>();
			foreach (var itemEntidad in listado)
			{
				RaPresencialBoletoAereoReservaBO objetoBO = Mapper.Map<TRaPresencialBoletoAereoReserva, RaPresencialBoletoAereoReservaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public RaPresencialBoletoAereoReservaBO FirstById(int id)
		{
			try
			{
				TRaPresencialBoletoAereoReserva entidad = base.FirstById(id);
				RaPresencialBoletoAereoReservaBO objetoBO = new RaPresencialBoletoAereoReservaBO();
				Mapper.Map<TRaPresencialBoletoAereoReserva, RaPresencialBoletoAereoReservaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public RaPresencialBoletoAereoReservaBO FirstBy(Expression<Func<TRaPresencialBoletoAereoReserva, bool>> filter)
		{
			try
			{
				TRaPresencialBoletoAereoReserva entidad = base.FirstBy(filter);
				RaPresencialBoletoAereoReservaBO objetoBO = Mapper.Map<TRaPresencialBoletoAereoReserva, RaPresencialBoletoAereoReservaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(RaPresencialBoletoAereoReservaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TRaPresencialBoletoAereoReserva entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<RaPresencialBoletoAereoReservaBO> listadoBO)
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

		public bool Update(RaPresencialBoletoAereoReservaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TRaPresencialBoletoAereoReserva entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<RaPresencialBoletoAereoReservaBO> listadoBO)
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
		private void AsignacionId(TRaPresencialBoletoAereoReserva entidad, RaPresencialBoletoAereoReservaBO objetoBO)
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

		private TRaPresencialBoletoAereoReserva MapeoEntidad(RaPresencialBoletoAereoReservaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TRaPresencialBoletoAereoReserva entidad = new TRaPresencialBoletoAereoReserva();
				entidad = Mapper.Map<RaPresencialBoletoAereoReservaBO, TRaPresencialBoletoAereoReserva>(objetoBO,
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

		public List<BoletoAereoReservaFiltroDTO> ObtenerListaBoletosAereosReserva(int idRaPresencialBoletoAereo)
		{
			try
			{
				string query = "SELECT Id, IdRaPresencialBoletoAereo, IdRaAerolinea, LinkBoarding, CodigoReserva, NombreAerolinea FROM ope.V_ObtenerBoletosAereosReserva WHERE Estado = 1 and IdRaPresencialBoletoAereo = @IdRaPresencialBoletoAereo";
				var listaBoletoAereoReserva = _dapper.QueryDapper(query, new { IdRaPresencialBoletoAereo = idRaPresencialBoletoAereo });
				return JsonConvert.DeserializeObject<List<BoletoAereoReservaFiltroDTO>>(listaBoletoAereoReserva);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
