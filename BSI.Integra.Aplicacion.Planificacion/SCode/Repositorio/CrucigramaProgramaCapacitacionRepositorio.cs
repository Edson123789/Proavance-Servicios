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
    /// Autor: Luis Huallpa - Jorge Rivera - Gian Miranda
    /// Fecha: 22/02/2021
    /// <summary>
    /// Repositorio para consultas de pla.T_CrucigramaProgramaCapacitacion
	/// </summary>
	public class CrucigramaProgramaCapacitacionRepositorio : BaseRepository<TCrucigramaProgramaCapacitacion, CrucigramaProgramaCapacitacionBO>
	{
		#region Metodos Base
		public CrucigramaProgramaCapacitacionRepositorio() : base()
		{
		}
		public CrucigramaProgramaCapacitacionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<CrucigramaProgramaCapacitacionBO> GetBy(Expression<Func<TCrucigramaProgramaCapacitacion, bool>> filter)
		{
			IEnumerable<TCrucigramaProgramaCapacitacion> listado = base.GetBy(filter);
			List<CrucigramaProgramaCapacitacionBO> listadoBO = new List<CrucigramaProgramaCapacitacionBO>();
			foreach (var itemEntidad in listado)
			{
				CrucigramaProgramaCapacitacionBO objetoBO = Mapper.Map<TCrucigramaProgramaCapacitacion, CrucigramaProgramaCapacitacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public CrucigramaProgramaCapacitacionBO FirstById(int id)
		{
			try
			{
				TCrucigramaProgramaCapacitacion entidad = base.FirstById(id);
				CrucigramaProgramaCapacitacionBO objetoBO = new CrucigramaProgramaCapacitacionBO();
				Mapper.Map<TCrucigramaProgramaCapacitacion, CrucigramaProgramaCapacitacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public CrucigramaProgramaCapacitacionBO FirstBy(Expression<Func<TCrucigramaProgramaCapacitacion, bool>> filter)
		{
			try
			{
				TCrucigramaProgramaCapacitacion entidad = base.FirstBy(filter);
				CrucigramaProgramaCapacitacionBO objetoBO = Mapper.Map<TCrucigramaProgramaCapacitacion, CrucigramaProgramaCapacitacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(CrucigramaProgramaCapacitacionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TCrucigramaProgramaCapacitacion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<CrucigramaProgramaCapacitacionBO> listadoBO)
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

		public bool Update(CrucigramaProgramaCapacitacionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TCrucigramaProgramaCapacitacion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<CrucigramaProgramaCapacitacionBO> listadoBO)
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
		private void AsignacionId(TCrucigramaProgramaCapacitacion entidad, CrucigramaProgramaCapacitacionBO objetoBO)
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

		private TCrucigramaProgramaCapacitacion MapeoEntidad(CrucigramaProgramaCapacitacionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TCrucigramaProgramaCapacitacion entidad = new TCrucigramaProgramaCapacitacion();
				entidad = Mapper.Map<CrucigramaProgramaCapacitacionBO, TCrucigramaProgramaCapacitacion>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<CrucigramaProgramaCapacitacionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TCrucigramaProgramaCapacitacion, bool>>> filters, Expression<Func<TCrucigramaProgramaCapacitacion, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TCrucigramaProgramaCapacitacion> listado = base.GetFiltered(filters, orderBy, ascending);
			List<CrucigramaProgramaCapacitacionBO> listadoBO = new List<CrucigramaProgramaCapacitacionBO>();

			foreach (var itemEntidad in listado)
			{
				CrucigramaProgramaCapacitacionBO objetoBO = Mapper.Map<TCrucigramaProgramaCapacitacion, CrucigramaProgramaCapacitacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion


		/// <summary>
		/// Obtiene todos los crucigramas de programa de capacitacion registrados en el sistema
		/// </summary>
		/// <returns>Lista de objetos de tipo (CrucigramaProgramaCapacitacionDTO)</returns>
		public List<CrucigramaProgramaCapacitacionDTO> ObtenerCrucigramasRegistrados()
		{
			try
			{
				List<CrucigramaProgramaCapacitacionDTO> objeto = new List<CrucigramaProgramaCapacitacionDTO>();
				var query = "SELECT Id, CodigoCrucigrama, IdPGeneral, IdPEspecifico, PGeneral, IdCapitulo, IdSesion, IdTipoMarcador, ValorMarcador, CantidadFila, CantidadColumna FROM [pla].[V_TCrucicramaProgramaCapacitacion_ObtenerCrucigramasRegistrados] WHERE Estado = 1";
				var res = _dapper.QueryDapper(query, null);
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					objeto = JsonConvert.DeserializeObject<List<CrucigramaProgramaCapacitacionDTO>>(res);
				}
				return objeto;
				
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene los crugramas de programas de capacitacion por sesion
		/// </summary>
		/// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
		/// <param name="idCapitulo">Id del capitulo></param>
		/// <param name="idSesion">Id de la sesion</param>
		/// <returns>Lista de objetos de tipo CrucigramaProgramaCapacitacionDetalleDTO</returns>
		public CrucigramaProgramaCapacitacionBO ObtenerCrucigramaProgramaCapacitacionSesion(int idPGeneral, int idCapitulo, int idSesion)
		{
			try
			{
				CrucigramaProgramaCapacitacionBO objeto = new CrucigramaProgramaCapacitacionBO();
				var query = "SELECT Id, IdPgeneral, OrdenFilaCapitulo, OrdenFilaSesion, CodigoCrucigrama FROM pla.T_CrucigramaProgramaCapacitacion WHERE Estado = 1 AND IdPGeneral=@IdPGeneral AND OrdenFilaCapitulo=@IdCapitulo AND OrdenFilaSesion=@IdSesion";
				var res = _dapper.FirstOrDefault(query, new { IdPGeneral = idPGeneral, IdCapitulo = idCapitulo, IdSesion = idSesion });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					objeto = JsonConvert.DeserializeObject<CrucigramaProgramaCapacitacionBO>(res);
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
