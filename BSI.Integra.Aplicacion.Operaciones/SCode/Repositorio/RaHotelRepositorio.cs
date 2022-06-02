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
	public class RaHotelRepositorio : BaseRepository<TRaHotel, RaHotelBO>
	{
		#region Metodos Base
		public RaHotelRepositorio() : base()
		{
		}
		public RaHotelRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<RaHotelBO> GetBy(Expression<Func<TRaHotel, bool>> filter)
		{
			IEnumerable<TRaHotel> listado = base.GetBy(filter);
			List<RaHotelBO> listadoBO = new List<RaHotelBO>();
			foreach (var itemEntidad in listado)
			{
				RaHotelBO objetoBO = Mapper.Map<TRaHotel, RaHotelBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public RaHotelBO FirstById(int id)
		{
			try
			{
				TRaHotel entidad = base.FirstById(id);
				RaHotelBO objetoBO = new RaHotelBO();
				Mapper.Map<TRaHotel, RaHotelBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public RaHotelBO FirstBy(Expression<Func<TRaHotel, bool>> filter)
		{
			try
			{
				TRaHotel entidad = base.FirstBy(filter);
				RaHotelBO objetoBO = Mapper.Map<TRaHotel, RaHotelBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(RaHotelBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TRaHotel entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<RaHotelBO> listadoBO)
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

		public bool Update(RaHotelBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TRaHotel entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<RaHotelBO> listadoBO)
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
		private void AsignacionId(TRaHotel entidad, RaHotelBO objetoBO)
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

		private TRaHotel MapeoEntidad(RaHotelBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TRaHotel entidad = new TRaHotel();
				entidad = Mapper.Map<RaHotelBO, TRaHotel>(objetoBO,
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
		/// Obtiene lista de hoteles registrados en el sistema
		/// </summary>
		/// <returns></returns>
		public List<HotelDTO> ObtenerListaHoteles()
		{
			try
			{
				string query = "SELECT Id, Nombre, Telefono, IdCiudad, Ciudad, Direccion, CubrimosDesayuno, IdRaSede FROM ope.V_ObtenerListaHoteles WHERE Estado = 1";
				var listaHoteles= _dapper.QueryDapper(query,null);
				return JsonConvert.DeserializeObject<List<HotelDTO>>(listaHoteles);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public List<CiudadFiltroDTO> ObtenerCiudades()
		{
			try
			{
				string query = "SELECT Id, Nombre, Codigo FROM pla.V_TCiudad_Filtro WHERE Estado = 1";
				var listaCiudades = _dapper.QueryDapper(query, null);
				return JsonConvert.DeserializeObject<List<CiudadFiltroDTO>>(listaCiudades);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public List<FiltroGenericoDTO> ObtenerHotelesParaCombo()
		{
			try
			{
				return GetBy(x => x.Estado == true, x => new FiltroGenericoDTO { Value = x.Id, Text = x.Nombre}).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
