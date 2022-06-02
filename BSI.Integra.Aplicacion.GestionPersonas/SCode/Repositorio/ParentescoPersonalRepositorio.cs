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
	/// Repositorio: ParentescoPersonalRepositorio
	/// Autor: Luis Huallpa
	/// Fecha: 16/06/2021
	/// <summary>
	/// Repositorio para de tabla T_ParentescoPersonal
	/// </summary>
	public class ParentescoPersonalRepositorio : BaseRepository<TParentescoPersonal, ParentescoPersonalBO>
	{
		#region Metodos Base
		public ParentescoPersonalRepositorio() : base()
		{
		}
		public ParentescoPersonalRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ParentescoPersonalBO> GetBy(Expression<Func<TParentescoPersonal, bool>> filter)
		{
			IEnumerable<TParentescoPersonal> listado = base.GetBy(filter);
			List<ParentescoPersonalBO> listadoBO = new List<ParentescoPersonalBO>();
			foreach (var itemEntidad in listado)
			{
				ParentescoPersonalBO objetoBO = Mapper.Map<TParentescoPersonal, ParentescoPersonalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ParentescoPersonalBO FirstById(int id)
		{
			try
			{
				TParentescoPersonal entidad = base.FirstById(id);
				ParentescoPersonalBO objetoBO = new ParentescoPersonalBO();
				Mapper.Map<TParentescoPersonal, ParentescoPersonalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ParentescoPersonalBO FirstBy(Expression<Func<TParentescoPersonal, bool>> filter)
		{
			try
			{
				TParentescoPersonal entidad = base.FirstBy(filter);
				ParentescoPersonalBO objetoBO = Mapper.Map<TParentescoPersonal, ParentescoPersonalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ParentescoPersonalBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TParentescoPersonal entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ParentescoPersonalBO> listadoBO)
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

		public bool Update(ParentescoPersonalBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TParentescoPersonal entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ParentescoPersonalBO> listadoBO)
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
		private void AsignacionId(TParentescoPersonal entidad, ParentescoPersonalBO objetoBO)
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

		private TParentescoPersonal MapeoEntidad(ParentescoPersonalBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TParentescoPersonal entidad = new TParentescoPersonal();
				entidad = Mapper.Map<ParentescoPersonalBO, TParentescoPersonal>(objetoBO,
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
		/// Repositorio: ParentescoPersonalRepositorio
		/// Autor: 
		/// Fecha: 16/06/2021
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
	}
}
