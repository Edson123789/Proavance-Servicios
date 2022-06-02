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
	/// Repositorio: NivelIdiomaRepositorio
	/// Autor: Luis Huallpa
	/// Fecha: 16/06/2021
	/// <summary>
	/// Repositorio para de tabla T_NivelIdioma
	/// </summary>
	public class NivelIdiomaRepositorio : BaseRepository<TNivelIdioma, NivelIdiomaBO>
	{
		#region Metodos Base
		public NivelIdiomaRepositorio() : base()
		{
		}
		public NivelIdiomaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<NivelIdiomaBO> GetBy(Expression<Func<TNivelIdioma, bool>> filter)
		{
			IEnumerable<TNivelIdioma> listado = base.GetBy(filter);
			List<NivelIdiomaBO> listadoBO = new List<NivelIdiomaBO>();
			foreach (var itemEntidad in listado)
			{
				NivelIdiomaBO objetoBO = Mapper.Map<TNivelIdioma, NivelIdiomaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public NivelIdiomaBO FirstById(int id)
		{
			try
			{
				TNivelIdioma entidad = base.FirstById(id);
				NivelIdiomaBO objetoBO = new NivelIdiomaBO();
				Mapper.Map<TNivelIdioma, NivelIdiomaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public NivelIdiomaBO FirstBy(Expression<Func<TNivelIdioma, bool>> filter)
		{
			try
			{
				TNivelIdioma entidad = base.FirstBy(filter);
				NivelIdiomaBO objetoBO = Mapper.Map<TNivelIdioma, NivelIdiomaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(NivelIdiomaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TNivelIdioma entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<NivelIdiomaBO> listadoBO)
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

		public bool Update(NivelIdiomaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TNivelIdioma entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<NivelIdiomaBO> listadoBO)
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
		private void AsignacionId(TNivelIdioma entidad, NivelIdiomaBO objetoBO)
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

		private TNivelIdioma MapeoEntidad(NivelIdiomaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TNivelIdioma entidad = new TNivelIdioma();
				entidad = Mapper.Map<NivelIdiomaBO, TNivelIdioma>(objetoBO,
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
		/// Repositorio: NivelIdiomaRepositorio
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
