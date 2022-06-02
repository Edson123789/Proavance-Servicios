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
	public class RaPresencialBoletoAereoRepositorio : BaseRepository<TRaPresencialBoletoAereo, RaPresencialBoletoAereoBO>
	{
		#region Metodos Base
		public RaPresencialBoletoAereoRepositorio() : base()
		{
		}
		public RaPresencialBoletoAereoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<RaPresencialBoletoAereoBO> GetBy(Expression<Func<TRaPresencialBoletoAereo, bool>> filter)
		{
			IEnumerable<TRaPresencialBoletoAereo> listado = base.GetBy(filter);
			List<RaPresencialBoletoAereoBO> listadoBO = new List<RaPresencialBoletoAereoBO>();
			foreach (var itemEntidad in listado)
			{
				RaPresencialBoletoAereoBO objetoBO = Mapper.Map<TRaPresencialBoletoAereo, RaPresencialBoletoAereoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public RaPresencialBoletoAereoBO FirstById(int id)
		{
			try
			{
				TRaPresencialBoletoAereo entidad = base.FirstById(id);
				RaPresencialBoletoAereoBO objetoBO = new RaPresencialBoletoAereoBO();
				Mapper.Map<TRaPresencialBoletoAereo, RaPresencialBoletoAereoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public RaPresencialBoletoAereoBO FirstBy(Expression<Func<TRaPresencialBoletoAereo, bool>> filter)
		{
			try
			{
				TRaPresencialBoletoAereo entidad = base.FirstBy(filter);
				RaPresencialBoletoAereoBO objetoBO = Mapper.Map<TRaPresencialBoletoAereo, RaPresencialBoletoAereoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(RaPresencialBoletoAereoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TRaPresencialBoletoAereo entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<RaPresencialBoletoAereoBO> listadoBO)
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

		public bool Update(RaPresencialBoletoAereoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TRaPresencialBoletoAereo entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<RaPresencialBoletoAereoBO> listadoBO)
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
		private void AsignacionId(TRaPresencialBoletoAereo entidad, RaPresencialBoletoAereoBO objetoBO)
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

		private TRaPresencialBoletoAereo MapeoEntidad(RaPresencialBoletoAereoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TRaPresencialBoletoAereo entidad = new TRaPresencialBoletoAereo();
				entidad = Mapper.Map<RaPresencialBoletoAereoBO, TRaPresencialBoletoAereo>(objetoBO,
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

		public BoletoAereoFiltroDTO ObtenerBoletoAereo(int idRaSesion)
		{
			try
			{
				string query = "SELECT Id, IdRaSesion, IdRaHotel, IdRaMovilidad, FechaCoordinacionEstadia, NombreHotel, NombreMovilidad, TipoMovilidad FROM ope.V_DatosBoletoAereo WHERE Estado = 1 and IdRaSesion = @IdRaSesion";
				var boletoAereo = _dapper.FirstOrDefault(query, new { IdRaSesion = idRaSesion });
				return JsonConvert.DeserializeObject<BoletoAereoFiltroDTO>(boletoAereo);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
