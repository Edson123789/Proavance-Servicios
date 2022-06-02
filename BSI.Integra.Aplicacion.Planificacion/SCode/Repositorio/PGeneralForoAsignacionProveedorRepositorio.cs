using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.SCode.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Repositorio
{
	/// Repositorio: Planificacion/PGeneralForoAsignacionProveedor
	/// Autor: Edgar Serruto
	/// Fecha: 24/06/2021
	/// <summary>
	/// Gestión de T_PGeneralForoAsignacionProveedor
	/// </summary>
	public class PGeneralForoAsignacionProveedorRepositorio : BaseRepository<TPgeneralForoAsignacionProveedor, PGeneralForoAsignacionProveedorBO>
	{
		#region Metodos Base
		public PGeneralForoAsignacionProveedorRepositorio() : base()
		{
		}
		public PGeneralForoAsignacionProveedorRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PGeneralForoAsignacionProveedorBO> GetBy(Expression<Func<TPgeneralForoAsignacionProveedor, bool>> filter)
		{
			IEnumerable<TPgeneralForoAsignacionProveedor> listado = base.GetBy(filter);
			List<PGeneralForoAsignacionProveedorBO> listadoBO = new List<PGeneralForoAsignacionProveedorBO>();
			foreach (var itemEntidad in listado)
			{
				PGeneralForoAsignacionProveedorBO objetoBO = Mapper.Map<TPgeneralForoAsignacionProveedor, PGeneralForoAsignacionProveedorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PGeneralForoAsignacionProveedorBO FirstById(int id)
		{
			try
			{
				TPgeneralForoAsignacionProveedor entidad = base.FirstById(id);
				PGeneralForoAsignacionProveedorBO objetoBO = new PGeneralForoAsignacionProveedorBO();
				Mapper.Map<TPgeneralForoAsignacionProveedor, PGeneralForoAsignacionProveedorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PGeneralForoAsignacionProveedorBO FirstBy(Expression<Func<TPgeneralForoAsignacionProveedor, bool>> filter)
		{
			try
			{
				TPgeneralForoAsignacionProveedor entidad = base.FirstBy(filter);
				PGeneralForoAsignacionProveedorBO objetoBO = Mapper.Map<TPgeneralForoAsignacionProveedor, PGeneralForoAsignacionProveedorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public bool Insert(PGeneralForoAsignacionProveedorBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPgeneralForoAsignacionProveedor entidad = MapeoEntidad(objetoBO);

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
		public bool Insert(IEnumerable<PGeneralForoAsignacionProveedorBO> listadoBO)
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
		public bool Update(PGeneralForoAsignacionProveedorBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPgeneralForoAsignacionProveedor entidad = MapeoEntidad(objetoBO);

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
		public bool Update(IEnumerable<PGeneralForoAsignacionProveedorBO> listadoBO)
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
		private void AsignacionId(TPgeneralForoAsignacionProveedor entidad, PGeneralForoAsignacionProveedorBO objetoBO)
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
		private TPgeneralForoAsignacionProveedor MapeoEntidad(PGeneralForoAsignacionProveedorBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPgeneralForoAsignacionProveedor entidad = new TPgeneralForoAsignacionProveedor();
				entidad = Mapper.Map<PGeneralForoAsignacionProveedorBO, TPgeneralForoAsignacionProveedor>(objetoBO,
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