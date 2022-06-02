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
	/// Repositorio: TipoExperienciaRepositorio
	/// Autor: Luis Huallpa
	/// Fecha: 25/09/2021
	/// <summary>
	/// Gestión de T_TipoExperiencia
	/// </summary>
	public class TipoExperienciaRepositorio : BaseRepository<TTipoExperiencia, TipoExperienciaBO>
	{
		#region Metodos Base
		public TipoExperienciaRepositorio() : base()
		{
		}
		public TipoExperienciaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<TipoExperienciaBO> GetBy(Expression<Func<TTipoExperiencia, bool>> filter)
		{
			IEnumerable<TTipoExperiencia> listado = base.GetBy(filter);
			List<TipoExperienciaBO> listadoBO = new List<TipoExperienciaBO>();
			foreach (var itemEntidad in listado)
			{
				TipoExperienciaBO objetoBO = Mapper.Map<TTipoExperiencia, TipoExperienciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public TipoExperienciaBO FirstById(int id)
		{
			try
			{
				TTipoExperiencia entidad = base.FirstById(id);
				TipoExperienciaBO objetoBO = new TipoExperienciaBO();
				Mapper.Map<TTipoExperiencia, TipoExperienciaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public TipoExperienciaBO FirstBy(Expression<Func<TTipoExperiencia, bool>> filter)
		{
			try
			{
				TTipoExperiencia entidad = base.FirstBy(filter);
				TipoExperienciaBO objetoBO = Mapper.Map<TTipoExperiencia, TipoExperienciaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(TipoExperienciaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TTipoExperiencia entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<TipoExperienciaBO> listadoBO)
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

		public bool Update(TipoExperienciaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TTipoExperiencia entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<TipoExperienciaBO> listadoBO)
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
		private void AsignacionId(TTipoExperiencia entidad, TipoExperienciaBO objetoBO)
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

		private TTipoExperiencia MapeoEntidad(TipoExperienciaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TTipoExperiencia entidad = new TTipoExperiencia();
				entidad = Mapper.Map<TipoExperienciaBO, TTipoExperiencia>(objetoBO,
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

		/// Repositorio: TipoExperienciaRepositorio
		/// Autor: Luis Huallpa
		/// Fecha: 25/09/2021
		/// <summary>
		/// Obtiene lista de elementos registrados para combo
		/// </summary>
		/// <returns>List<FiltroIdNombreDTO></returns>
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
