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
	/// Repositorio: PerfilPuestoTrabajoEstadoSolicitudRepositorio
	/// Autor: Luis Huallpa - Edgar Serruto.
	/// Fecha: 07/09/2021
	/// <summary>
	/// Gestión de tabla T_PerfilPuestoTrabajoEstadoSolicitudRepositorio
	/// </summary>
	public class PerfilPuestoTrabajoEstadoSolicitudRepositorio : BaseRepository<TPerfilPuestoTrabajoEstadoSolicitud, PerfilPuestoTrabajoEstadoSolicitudBO>
	{
		#region Metodos Base
		public PerfilPuestoTrabajoEstadoSolicitudRepositorio() : base()
		{
		}
		public PerfilPuestoTrabajoEstadoSolicitudRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PerfilPuestoTrabajoEstadoSolicitudBO> GetBy(Expression<Func<TPerfilPuestoTrabajoEstadoSolicitud, bool>> filter)
		{
			IEnumerable<TPerfilPuestoTrabajoEstadoSolicitud> listado = base.GetBy(filter);
			List<PerfilPuestoTrabajoEstadoSolicitudBO> listadoBO = new List<PerfilPuestoTrabajoEstadoSolicitudBO>();
			foreach (var itemEntidad in listado)
			{
				PerfilPuestoTrabajoEstadoSolicitudBO objetoBO = Mapper.Map<TPerfilPuestoTrabajoEstadoSolicitud, PerfilPuestoTrabajoEstadoSolicitudBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PerfilPuestoTrabajoEstadoSolicitudBO FirstById(int id)
		{
			try
			{
				TPerfilPuestoTrabajoEstadoSolicitud entidad = base.FirstById(id);
				PerfilPuestoTrabajoEstadoSolicitudBO objetoBO = new PerfilPuestoTrabajoEstadoSolicitudBO();
				Mapper.Map<TPerfilPuestoTrabajoEstadoSolicitud, PerfilPuestoTrabajoEstadoSolicitudBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PerfilPuestoTrabajoEstadoSolicitudBO FirstBy(Expression<Func<TPerfilPuestoTrabajoEstadoSolicitud, bool>> filter)
		{
			try
			{
				TPerfilPuestoTrabajoEstadoSolicitud entidad = base.FirstBy(filter);
				PerfilPuestoTrabajoEstadoSolicitudBO objetoBO = Mapper.Map<TPerfilPuestoTrabajoEstadoSolicitud, PerfilPuestoTrabajoEstadoSolicitudBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PerfilPuestoTrabajoEstadoSolicitudBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPerfilPuestoTrabajoEstadoSolicitud entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PerfilPuestoTrabajoEstadoSolicitudBO> listadoBO)
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

		public bool Update(PerfilPuestoTrabajoEstadoSolicitudBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPerfilPuestoTrabajoEstadoSolicitud entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PerfilPuestoTrabajoEstadoSolicitudBO> listadoBO)
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
		private void AsignacionId(TPerfilPuestoTrabajoEstadoSolicitud entidad, PerfilPuestoTrabajoEstadoSolicitudBO objetoBO)
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

		private TPerfilPuestoTrabajoEstadoSolicitud MapeoEntidad(PerfilPuestoTrabajoEstadoSolicitudBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPerfilPuestoTrabajoEstadoSolicitud entidad = new TPerfilPuestoTrabajoEstadoSolicitud();
				entidad = Mapper.Map<PerfilPuestoTrabajoEstadoSolicitudBO, TPerfilPuestoTrabajoEstadoSolicitud>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<PerfilPuestoTrabajoEstadoSolicitudBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TPerfilPuestoTrabajoEstadoSolicitud, bool>>> filters, Expression<Func<TPerfilPuestoTrabajoEstadoSolicitud, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TPerfilPuestoTrabajoEstadoSolicitud> listado = base.GetFiltered(filters, orderBy, ascending);
			List<PerfilPuestoTrabajoEstadoSolicitudBO> listadoBO = new List<PerfilPuestoTrabajoEstadoSolicitudBO>();

			foreach (var itemEntidad in listado)
			{
				PerfilPuestoTrabajoEstadoSolicitudBO objetoBO = Mapper.Map<TPerfilPuestoTrabajoEstadoSolicitud, PerfilPuestoTrabajoEstadoSolicitudBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion
		/// Repositorio: PerfilPuestoTrabajoEstadoSolicitudRepositorio
		/// Autor: Luis Huallpa - Edgar Serruto.
		/// Fecha: 07/09/2021
		/// <summary>
		/// Obtiene Información de Estados de Solicitud de Perfiles de Puesto de Trabajo
		/// </summary>
		/// <returns>List<FiltroIdNombreDTO></returns>
		public List<FiltroIdNombreDTO> ObtenerPerfilPuestoTrabajoEstadoSolicitud()
		{
			try
			{
				return this.GetBy(x => true).Select(x => new FiltroIdNombreDTO
				{
					Id = x.Id,
					Nombre = x.Nombre
				}).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
