using System;
using System.Collections.Generic;
using System.Linq;
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
	/// Repositorio: Planificacion/CrucigramaProgramaCapacitacion
    /// Autor: Luis Huallpa - Gian Miranda
    /// Fecha: 22/02/2021
    /// <summary>
    /// Repositorio para consultas de pla.T_CrucigramaProgramaCapacitacion
	/// </summary>
	public class CrucigramaProgramaCapacitacionDetalleRepositorio : BaseRepository<TCrucigramaProgramaCapacitacionDetalle, CrucigramaProgramaCapacitacionDetalleBO>
	{
		#region Metodos Base
		public CrucigramaProgramaCapacitacionDetalleRepositorio() : base()
		{
		}
		public CrucigramaProgramaCapacitacionDetalleRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<CrucigramaProgramaCapacitacionDetalleBO> GetBy(Expression<Func<TCrucigramaProgramaCapacitacionDetalle, bool>> filter)
		{
			IEnumerable<TCrucigramaProgramaCapacitacionDetalle> listado = base.GetBy(filter);
			List<CrucigramaProgramaCapacitacionDetalleBO> listadoBO = new List<CrucigramaProgramaCapacitacionDetalleBO>();
			foreach (var itemEntidad in listado)
			{
				CrucigramaProgramaCapacitacionDetalleBO objetoBO = Mapper.Map<TCrucigramaProgramaCapacitacionDetalle, CrucigramaProgramaCapacitacionDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public CrucigramaProgramaCapacitacionDetalleBO FirstById(int id)
		{
			try
			{
				TCrucigramaProgramaCapacitacionDetalle entidad = base.FirstById(id);
				CrucigramaProgramaCapacitacionDetalleBO objetoBO = new CrucigramaProgramaCapacitacionDetalleBO();
				Mapper.Map<TCrucigramaProgramaCapacitacionDetalle, CrucigramaProgramaCapacitacionDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public CrucigramaProgramaCapacitacionDetalleBO FirstBy(Expression<Func<TCrucigramaProgramaCapacitacionDetalle, bool>> filter)
		{
			try
			{
				TCrucigramaProgramaCapacitacionDetalle entidad = base.FirstBy(filter);
				CrucigramaProgramaCapacitacionDetalleBO objetoBO = Mapper.Map<TCrucigramaProgramaCapacitacionDetalle, CrucigramaProgramaCapacitacionDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(CrucigramaProgramaCapacitacionDetalleBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TCrucigramaProgramaCapacitacionDetalle entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<CrucigramaProgramaCapacitacionDetalleBO> listadoBO)
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

		public bool Update(CrucigramaProgramaCapacitacionDetalleBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TCrucigramaProgramaCapacitacionDetalle entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<CrucigramaProgramaCapacitacionDetalleBO> listadoBO)
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
		private void AsignacionId(TCrucigramaProgramaCapacitacionDetalle entidad, CrucigramaProgramaCapacitacionDetalleBO objetoBO)
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

		private TCrucigramaProgramaCapacitacionDetalle MapeoEntidad(CrucigramaProgramaCapacitacionDetalleBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TCrucigramaProgramaCapacitacionDetalle entidad = new TCrucigramaProgramaCapacitacionDetalle();
				entidad = Mapper.Map<CrucigramaProgramaCapacitacionDetalleBO, TCrucigramaProgramaCapacitacionDetalle>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<CrucigramaProgramaCapacitacionDetalleBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TCrucigramaProgramaCapacitacionDetalle, bool>>> filters, Expression<Func<TCrucigramaProgramaCapacitacionDetalle, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TCrucigramaProgramaCapacitacionDetalle> listado = base.GetFiltered(filters, orderBy, ascending);
			List<CrucigramaProgramaCapacitacionDetalleBO> listadoBO = new List<CrucigramaProgramaCapacitacionDetalleBO>();

			foreach (var itemEntidad in listado)
			{
				CrucigramaProgramaCapacitacionDetalleBO objetoBO = Mapper.Map<TCrucigramaProgramaCapacitacionDetalle, CrucigramaProgramaCapacitacionDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion

		/// <summary>
		/// Obtiene respuestas asociadas a una pregunta
		/// </summary>
		/// <param name="idCrucigramaProgramaCapacitacion">Id del crucigrama del programa de capacitacion (PK de la tabla pla.T_CrucigramaProgramaCapacitacion)</param>
		/// <returns>Lista de objetos de tipo CrucigramaProgramaCapacitacionDetalleDTO</returns>
		public List<CrucigramaProgramaCapacitacionDetalleDTO> ObtenerRespuestaCrucigramaProgramaCapacitacion(int idCrucigramaProgramaCapacitacion)
		{
			try
			{
				List<CrucigramaProgramaCapacitacionDetalleDTO> objeto = new List<CrucigramaProgramaCapacitacionDetalleDTO>();
				string query = "SELECT Id, NumeroPalabra, Palabra, Definicion, Tipo, ColumnaInicio, FilaInicio FROM [pla].[V_TCrucigramaProgramaCapacitacionDetalle_ObtenerRespuestasCrucigrama] WHERE Estado = 1 AND IdCrucigramaProgramaCapacitacion = @IdCrucigramaProgramaCapacitacion";
				var res = _dapper.QueryDapper(query, new { IdCrucigramaProgramaCapacitacion = idCrucigramaProgramaCapacitacion });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					objeto = JsonConvert.DeserializeObject<List<CrucigramaProgramaCapacitacionDetalleDTO>>(res);
				}
				return objeto;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
