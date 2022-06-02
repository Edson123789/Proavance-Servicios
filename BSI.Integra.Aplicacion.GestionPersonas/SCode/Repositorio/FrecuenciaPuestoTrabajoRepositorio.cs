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
	/// Repositorio: FrecuenciaPuestoTrabajoRepositorio
	/// Autor: Edgar S.
	/// Fecha: 29/01/2021
	/// <summary>
	/// Gestión de Frecuencia de Puestos de Trabajo
	/// </summary>
	public class FrecuenciaPuestoTrabajoRepositorio : BaseRepository<TFrecuenciaPuestoTrabajo, FrecuenciaPuestoTrabajoBO>
	{
		#region Metodos Base
		public FrecuenciaPuestoTrabajoRepositorio() : base()
		{
		}
		public FrecuenciaPuestoTrabajoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<FrecuenciaPuestoTrabajoBO> GetBy(Expression<Func<TFrecuenciaPuestoTrabajo, bool>> filter)
		{
			IEnumerable<TFrecuenciaPuestoTrabajo> listado = base.GetBy(filter);
			List<FrecuenciaPuestoTrabajoBO> listadoBO = new List<FrecuenciaPuestoTrabajoBO>();
			foreach (var itemEntidad in listado)
			{
				FrecuenciaPuestoTrabajoBO objetoBO = Mapper.Map<TFrecuenciaPuestoTrabajo, FrecuenciaPuestoTrabajoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public FrecuenciaPuestoTrabajoBO FirstById(int id)
		{
			try
			{
				TFrecuenciaPuestoTrabajo entidad = base.FirstById(id);
				FrecuenciaPuestoTrabajoBO objetoBO = new FrecuenciaPuestoTrabajoBO();
				Mapper.Map<TFrecuenciaPuestoTrabajo, FrecuenciaPuestoTrabajoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public FrecuenciaPuestoTrabajoBO FirstBy(Expression<Func<TFrecuenciaPuestoTrabajo, bool>> filter)
		{
			try
			{
				TFrecuenciaPuestoTrabajo entidad = base.FirstBy(filter);
				FrecuenciaPuestoTrabajoBO objetoBO = Mapper.Map<TFrecuenciaPuestoTrabajo, FrecuenciaPuestoTrabajoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(FrecuenciaPuestoTrabajoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TFrecuenciaPuestoTrabajo entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<FrecuenciaPuestoTrabajoBO> listadoBO)
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

		public bool Update(FrecuenciaPuestoTrabajoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TFrecuenciaPuestoTrabajo entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<FrecuenciaPuestoTrabajoBO> listadoBO)
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
		private void AsignacionId(TFrecuenciaPuestoTrabajo entidad, FrecuenciaPuestoTrabajoBO objetoBO)
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

		private TFrecuenciaPuestoTrabajo MapeoEntidad(FrecuenciaPuestoTrabajoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TFrecuenciaPuestoTrabajo entidad = new TFrecuenciaPuestoTrabajo();
				entidad = Mapper.Map<FrecuenciaPuestoTrabajoBO, TFrecuenciaPuestoTrabajo>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<FrecuenciaPuestoTrabajoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TFrecuenciaPuestoTrabajo, bool>>> filters, Expression<Func<TFrecuenciaPuestoTrabajo, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TFrecuenciaPuestoTrabajo> listado = base.GetFiltered(filters, orderBy, ascending);
			List<FrecuenciaPuestoTrabajoBO> listadoBO = new List<FrecuenciaPuestoTrabajoBO>();

			foreach (var itemEntidad in listado)
			{
				FrecuenciaPuestoTrabajoBO objetoBO = Mapper.Map<TFrecuenciaPuestoTrabajo, FrecuenciaPuestoTrabajoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion

		/// Repositorio: FrecuenciaPuestoTrabajoRepositorio
		/// Autor: Edgar S.
		/// Fecha: 29/01/2021
		/// <summary>
		/// Obtiene lista de frecuencia puesto trabajo
		/// </summary>
		/// <returns> Lista de Frecuencia de Puesto de Trabajo </returns>
		/// <returns> Lista de objeto DTO : List<FrecuenciaPuestoTrabajoDTO> </returns>
		public List<FrecuenciaPuestoTrabajoDTO> ObtenerFrecuenciaPuestoTrabajo()
		{
			try
			{
				return this.GetBy(x => true).Select(x => new FrecuenciaPuestoTrabajoDTO
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
