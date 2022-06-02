using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
	/// Repositorio: PuestoTrabajoNivelRepositorio
	/// Autor: Edgar Serruto .
	/// Fecha: 15/06/2021
	/// <summary>
	/// Repositorio para de tabla T_PuestoTrabajoNivel
	/// </summary>
	public class PuestoTrabajoNivelRepositorio : BaseRepository<TPuestoTrabajoNivel, PuestoTrabajoNivelBO>
	{
		#region Metodos Base
		public PuestoTrabajoNivelRepositorio() : base()
		{
		}
		public PuestoTrabajoNivelRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PuestoTrabajoNivelBO> GetBy(Expression<Func<TPuestoTrabajoNivel, bool>> filter)
		{
			IEnumerable<TPuestoTrabajoNivel> listado = base.GetBy(filter);
			List<PuestoTrabajoNivelBO> listadoBO = new List<PuestoTrabajoNivelBO>();
			foreach (var itemEntidad in listado)
			{
				PuestoTrabajoNivelBO objetoBO = Mapper.Map<TPuestoTrabajoNivel, PuestoTrabajoNivelBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PuestoTrabajoNivelBO FirstById(int id)
		{
			try
			{
				TPuestoTrabajoNivel entidad = base.FirstById(id);
				PuestoTrabajoNivelBO objetoBO = new PuestoTrabajoNivelBO();
				Mapper.Map<TPuestoTrabajoNivel, PuestoTrabajoNivelBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PuestoTrabajoNivelBO FirstBy(Expression<Func<TPuestoTrabajoNivel, bool>> filter)
		{
			try
			{
				TPuestoTrabajoNivel entidad = base.FirstBy(filter);
				PuestoTrabajoNivelBO objetoBO = Mapper.Map<TPuestoTrabajoNivel, PuestoTrabajoNivelBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PuestoTrabajoNivelBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPuestoTrabajoNivel entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PuestoTrabajoNivelBO> listadoBO)
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

		public bool Update(PuestoTrabajoNivelBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPuestoTrabajoNivel entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PuestoTrabajoNivelBO> listadoBO)
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
		private void AsignacionId(TPuestoTrabajoNivel entidad, PuestoTrabajoNivelBO objetoBO)
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

		private TPuestoTrabajoNivel MapeoEntidad(PuestoTrabajoNivelBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPuestoTrabajoNivel entidad = new TPuestoTrabajoNivel();
				entidad = Mapper.Map<PuestoTrabajoNivelBO, TPuestoTrabajoNivel>(objetoBO,
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
		/// Repositorio: PuestoTrabajoNivelRepositorio
		/// Autor: Edgar Serruto.
		/// Fecha: 15/06/2021
		/// <summary>
		/// Obtiene lista de elementos registrados para combo
		/// </summary>
		/// <returns> List<FiltroIdNombreDTO> </returns>
		public List<FiltroIdNombreDTO> ObtenerListaParaFiltro()
		{
			try
			{
				return this.GetBy(x => true).Select(x => new FiltroIdNombreDTO
				{
					Id = x.Id,
					Nombre = x.Nombre
				}).ToList(); ;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// Repositorio: PuestoTrabajoNivelRepositorio
		/// Autor: Edgar Serruto.
		/// Fecha: 15/06/2021
		/// <summary>
		/// Obtiene la lista de todos los elementos en la tabla T_PuestoTrabajoNivel
		/// </summary>
		/// <returns> List<PuestoTrabajoNivelDTO> </returns>
		public List<PuestoTrabajoNivelDTO> ObtenerPuestoTrabajoNivelRegistrado()
		{
			try
			{
				return this.GetBy(x => x.Estado == true).Select(x => new PuestoTrabajoNivelDTO
				{
					Id = x.Id,
					Nombre = x.Nombre,
					Descripcion = x.Descripcion
				}).ToList(); ;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
