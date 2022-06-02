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
	/// Repositorio: IdiomaRepositorio
	/// Autor: Luis Huallpa
	/// Fecha: 16/06/2021
	/// <summary>
	/// Repositorio para de tabla T_Idioma
	/// </summary>
	public class IdiomaRepositorio : BaseRepository<TIdioma, IdiomaBO>
	{
		#region Metodos Base
		public IdiomaRepositorio() : base()
		{
		}
		public IdiomaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<IdiomaBO> GetBy(Expression<Func<TIdioma, bool>> filter)
		{
			IEnumerable<TIdioma> listado = base.GetBy(filter);
			List<IdiomaBO> listadoBO = new List<IdiomaBO>();
			foreach (var itemEntidad in listado)
			{
				IdiomaBO objetoBO = Mapper.Map<TIdioma, IdiomaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public IdiomaBO FirstById(int id)
		{
			try
			{
				TIdioma entidad = base.FirstById(id);
				IdiomaBO objetoBO = new IdiomaBO();
				Mapper.Map<TIdioma, IdiomaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public IdiomaBO FirstBy(Expression<Func<TIdioma, bool>> filter)
		{
			try
			{
				TIdioma entidad = base.FirstBy(filter);
				IdiomaBO objetoBO = Mapper.Map<TIdioma, IdiomaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(IdiomaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TIdioma entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<IdiomaBO> listadoBO)
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

		public bool Update(IdiomaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TIdioma entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<IdiomaBO> listadoBO)
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
		private void AsignacionId(TIdioma entidad, IdiomaBO objetoBO)
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

		private TIdioma MapeoEntidad(IdiomaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TIdioma entidad = new TIdioma();
				entidad = Mapper.Map<IdiomaBO, TIdioma>(objetoBO,
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
		/// Repositorio: IdiomaRepositorio
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
