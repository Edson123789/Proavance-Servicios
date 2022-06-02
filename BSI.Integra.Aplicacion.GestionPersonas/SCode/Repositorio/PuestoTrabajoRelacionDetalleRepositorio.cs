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
	public class PuestoTrabajoRelacionDetalleRepositorio : BaseRepository<TPuestoTrabajoRelacionDetalle, PuestoTrabajoRelacionDetalleBO>
	{
		#region Metodos Base
		public PuestoTrabajoRelacionDetalleRepositorio() : base()
		{
		}
		public PuestoTrabajoRelacionDetalleRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PuestoTrabajoRelacionDetalleBO> GetBy(Expression<Func<TPuestoTrabajoRelacionDetalle, bool>> filter)
		{
			IEnumerable<TPuestoTrabajoRelacionDetalle> listado = base.GetBy(filter);
			List<PuestoTrabajoRelacionDetalleBO> listadoBO = new List<PuestoTrabajoRelacionDetalleBO>();
			foreach (var itemEntidad in listado)
			{
				PuestoTrabajoRelacionDetalleBO objetoBO = Mapper.Map<TPuestoTrabajoRelacionDetalle, PuestoTrabajoRelacionDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PuestoTrabajoRelacionDetalleBO FirstById(int id)
		{
			try
			{
				TPuestoTrabajoRelacionDetalle entidad = base.FirstById(id);
				PuestoTrabajoRelacionDetalleBO objetoBO = new PuestoTrabajoRelacionDetalleBO();
				Mapper.Map<TPuestoTrabajoRelacionDetalle, PuestoTrabajoRelacionDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PuestoTrabajoRelacionDetalleBO FirstBy(Expression<Func<TPuestoTrabajoRelacionDetalle, bool>> filter)
		{
			try
			{
				TPuestoTrabajoRelacionDetalle entidad = base.FirstBy(filter);
				PuestoTrabajoRelacionDetalleBO objetoBO = Mapper.Map<TPuestoTrabajoRelacionDetalle, PuestoTrabajoRelacionDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PuestoTrabajoRelacionDetalleBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPuestoTrabajoRelacionDetalle entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PuestoTrabajoRelacionDetalleBO> listadoBO)
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

		public bool Update(PuestoTrabajoRelacionDetalleBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPuestoTrabajoRelacionDetalle entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PuestoTrabajoRelacionDetalleBO> listadoBO)
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
		private void AsignacionId(TPuestoTrabajoRelacionDetalle entidad, PuestoTrabajoRelacionDetalleBO objetoBO)
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

		private TPuestoTrabajoRelacionDetalle MapeoEntidad(PuestoTrabajoRelacionDetalleBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPuestoTrabajoRelacionDetalle entidad = new TPuestoTrabajoRelacionDetalle();
				entidad = Mapper.Map<PuestoTrabajoRelacionDetalleBO, TPuestoTrabajoRelacionDetalle>(objetoBO,
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
	}
}
