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
	public class RaCentroCostoRepositorio : BaseRepository<TRaCentroCosto, RaCentroCostoBO>
	{
		#region Metodos Base
		public RaCentroCostoRepositorio() : base()
		{
		}
		public RaCentroCostoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<RaCentroCostoBO> GetBy(Expression<Func<TRaCentroCosto, bool>> filter)
		{
			IEnumerable<TRaCentroCosto> listado = base.GetBy(filter);
			List<RaCentroCostoBO> listadoBO = new List<RaCentroCostoBO>();
			foreach (var itemEntidad in listado)
			{
				RaCentroCostoBO objetoBO = Mapper.Map<TRaCentroCosto, RaCentroCostoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public RaCentroCostoBO FirstById(int id)
		{
			try
			{
				TRaCentroCosto entidad = base.FirstById(id);
				RaCentroCostoBO objetoBO = new RaCentroCostoBO();
				Mapper.Map<TRaCentroCosto, RaCentroCostoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public RaCentroCostoBO FirstBy(Expression<Func<TRaCentroCosto, bool>> filter)
		{
			try
			{
				TRaCentroCosto entidad = base.FirstBy(filter);
				RaCentroCostoBO objetoBO = Mapper.Map<TRaCentroCosto, RaCentroCostoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(RaCentroCostoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TRaCentroCosto entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<RaCentroCostoBO> listadoBO)
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

		public bool Update(RaCentroCostoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TRaCentroCosto entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<RaCentroCostoBO> listadoBO)
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
		private void AsignacionId(TRaCentroCosto entidad, RaCentroCostoBO objetoBO)
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

		private TRaCentroCosto MapeoEntidad(RaCentroCostoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TRaCentroCosto entidad = new TRaCentroCosto();
				entidad = Mapper.Map<RaCentroCostoBO, TRaCentroCosto>(objetoBO,
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
		/// Valida si existe un centro costo con el mismo nombre
		/// </summary>
		/// <param name="nombre"></param>
		/// <returns></returns>
		public bool ExistePorNombre(string nombre)
		{
			try
			{
				if (this.GetBy(x => x.NombreCentroCosto == nombre.Trim()).ToList().Count() > 0)
				{
					return true;
				}
				return false;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene los centro de costo con asistencia mensual
		/// </summary>
		/// <returns></returns>
		public List<CentroCostoListadoMensualDTO> ListadoCentroCostoConAsistenciaMensual()
		{
			try
			{
				List<CentroCostoListadoMensualDTO> centrosCostoAsistenciaMensual = new List<CentroCostoListadoMensualDTO>();
				//var query = "SELECT DISTINCT IdCentroCosto, NombreCentroCosto FROM ope.V_ObtenerListadoCentroCostoAsistencialMensual WHERE MONTH(SesionFecha) = MONTH(GETDATE()) AND YEAR(SesionFecha) = YEAR((GETDATE())) AND NombreCentroCosto LIKE '% ONLINE%'";
				var query = "SELECT DISTINCT IdCentroCosto, NombreCentroCosto FROM ope.V_ObtenerListadoCentroCostoAsistencialMensual WHERE MONTH(SesionFecha) = MONTH(GETDATE()) AND YEAR(SesionFecha) = YEAR((GETDATE()))";
				var centrosCostoAsistenciaMensualDB = _dapper.QueryDapper(query, null);
				if (!string.IsNullOrEmpty(centrosCostoAsistenciaMensualDB) && !centrosCostoAsistenciaMensualDB.Contains("[]"))
				{
					centrosCostoAsistenciaMensual = JsonConvert.DeserializeObject<List<CentroCostoListadoMensualDTO>>(centrosCostoAsistenciaMensualDB);
				}
				return centrosCostoAsistenciaMensual.Distinct().ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene un listado con el Id y Nombre del centro de costo
		/// </summary>
		/// <returns></returns>
		public List<FiltroDTO> ObtenerFiltro()
		{
			try
			{
				return this.GetBy(x => x.Estado, x => new FiltroDTO { Id = x.Id, Nombre = x.NombreCentroCosto }).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
