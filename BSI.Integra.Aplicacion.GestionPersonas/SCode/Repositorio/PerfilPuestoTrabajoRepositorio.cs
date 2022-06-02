using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
	/// PerfilPuestoTrabajoRepositorio
	/// Autor: Luis H., Edgar S.
	/// Fecha: 29/01/2021
	/// <summary>
	/// Gestión de Perfiles de Puestos de Trabajo
	/// </summary>
	public class PerfilPuestoTrabajoRepositorio : BaseRepository<TPerfilPuestoTrabajo, PerfilPuestoTrabajoBO>
	{
		#region Metodos Base
		public PerfilPuestoTrabajoRepositorio() : base()
		{
		}
		public PerfilPuestoTrabajoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PerfilPuestoTrabajoBO> GetBy(Expression<Func<TPerfilPuestoTrabajo, bool>> filter)
		{
			IEnumerable<TPerfilPuestoTrabajo> listado = base.GetBy(filter);
			List<PerfilPuestoTrabajoBO> listadoBO = new List<PerfilPuestoTrabajoBO>();
			foreach (var itemEntidad in listado)
			{
				PerfilPuestoTrabajoBO objetoBO = Mapper.Map<TPerfilPuestoTrabajo, PerfilPuestoTrabajoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PerfilPuestoTrabajoBO FirstById(int id)
		{
			try
			{
				TPerfilPuestoTrabajo entidad = base.FirstById(id);
				PerfilPuestoTrabajoBO objetoBO = new PerfilPuestoTrabajoBO();
				Mapper.Map<TPerfilPuestoTrabajo, PerfilPuestoTrabajoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PerfilPuestoTrabajoBO FirstBy(Expression<Func<TPerfilPuestoTrabajo, bool>> filter)
		{
			try
			{
				TPerfilPuestoTrabajo entidad = base.FirstBy(filter);
				PerfilPuestoTrabajoBO objetoBO = Mapper.Map<TPerfilPuestoTrabajo, PerfilPuestoTrabajoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PerfilPuestoTrabajoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPerfilPuestoTrabajo entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PerfilPuestoTrabajoBO> listadoBO)
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

		public bool Update(PerfilPuestoTrabajoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPerfilPuestoTrabajo entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PerfilPuestoTrabajoBO> listadoBO)
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
		private void AsignacionId(TPerfilPuestoTrabajo entidad, PerfilPuestoTrabajoBO objetoBO)
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

		private TPerfilPuestoTrabajo MapeoEntidad(PerfilPuestoTrabajoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPerfilPuestoTrabajo entidad = new TPerfilPuestoTrabajo();
				entidad = Mapper.Map<PerfilPuestoTrabajoBO, TPerfilPuestoTrabajo>(objetoBO,
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

		/// PerfilPuestoTrabajoRepositorio
		/// Autor: Luis H., Edgar S.
		/// Fecha: 29/01/2021
		/// <summary>
		/// Obtiene lista de historicos registrados de perfil puesto trabajo
		/// </summary>
		/// <param name="idPuestoTrabajo"></param>
		/// <returns>Retorna Lista de Perfiles Históricos por Puesto de Trabajo</returns>
		/// <returns> Lista de Objeto DTO : List<PerfilPuestoTrabajoDTO> </returns>
		public List<PerfilPuestoTrabajoDTO> ObtenerListaPerfilPuestoTrabajoHistorico(int idPuestoTrabajo)
		{
			try
			{
				List<PerfilPuestoTrabajoDTO> lista = new List<PerfilPuestoTrabajoDTO>();
				var query = "SELECT Id, IdPuestoTrabajo, PuestoTrabajo, Version, Objetivo, Descripcion, Personal_Solicitud, Personal_Aprobacion, FechaSolicitud, FechaAprobacion, IdPerfilPuestoTrabajoEstadoSolicitud, PerfilPuestoTrabajoEstadoSolicitud, Observacion, EsActual FROM [gp].[V_TPerfilPuestoTrabajo_ObtenerPerfilPuestoTrabajoHistorico] WHERE Estado = 1 AND IdPuestoTrabajo = @IdPuestoTrabajo";
				var res = _dapper.QueryDapper(query, new { IdPuestoTrabajo = idPuestoTrabajo });
				if(!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<PerfilPuestoTrabajoDTO>>(res);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}


		/// PerfilPuestoTrabajoRepositorio
		/// Autor: Luis H., Edgar S.
		/// Fecha: 29/01/2021
		/// <summary>
		/// Obtiene la Relaciones de Puestos de Trabajo
		/// </summary>
		/// <param name="idPerfilPuestoTrabajo"></param>
		/// <returns>Retorna Lista de Relaciones por Perfil de Puestos de Trabajo </returns>
		/// <returns> Lista de Objeto DTO : List<PuestoTrabajoRelacionDetalleDTO> </returns>
		public List<PuestoTrabajoRelacionDetalleDTO> ObtenerPuestoTrabajoRelacion(int idPerfilPuestoTrabajo)
		{
			try
			{
				List<PuestoTrabajoRelacionDetalleDTO> lista = new List<PuestoTrabajoRelacionDetalleDTO>();
				var query = "SELECT Id, IdPuestoTrabajoRelacionDetalle, IdPerfilPuestoTrabajo, IdPuestoTrabajo_Dependencia, IdPuestoTrabajo_PuestoACargo, IdPersonalAreaTrabajo, PuestoTrabajo_Dependencia, PuestoTrabajo_PuestoACargo, PersonalAreaTrabajo FROM [gp].[V_TPuestoTrabajoRelacion_ObtenerPuestoTrabajoRelacion] WHERE Estado = 1 AND IdPerfilPuestoTrabajo = @IdPerfilPuestoTrabajo";
				var res = _dapper.QueryDapper(query, new { IdPerfilPuestoTrabajo = idPerfilPuestoTrabajo });
				if(!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<PuestoTrabajoRelacionDetalleDTO>>(res);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

	}
}
