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
	/// Repositorio: ExperienciaRepositorio
	/// Autor: Edgar S.
	/// Fecha: 29/01/2021
	/// <summary>
	/// Gestión de Experiencia de Personal
	/// </summary>
	public class ExperienciaRepositorio : BaseRepository<TExperiencia, ExperienciaBO>
	{
		#region Metodos Base
		public ExperienciaRepositorio() : base()
		{
		}
		public ExperienciaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ExperienciaBO> GetBy(Expression<Func<TExperiencia, bool>> filter)
		{
			IEnumerable<TExperiencia> listado = base.GetBy(filter);
			List<ExperienciaBO> listadoBO = new List<ExperienciaBO>();
			foreach (var itemEntidad in listado)
			{
				ExperienciaBO objetoBO = Mapper.Map<TExperiencia, ExperienciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ExperienciaBO FirstById(int id)
		{
			try
			{
				TExperiencia entidad = base.FirstById(id);
				ExperienciaBO objetoBO = new ExperienciaBO();
				Mapper.Map<TExperiencia, ExperienciaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ExperienciaBO FirstBy(Expression<Func<TExperiencia, bool>> filter)
		{
			try
			{
				TExperiencia entidad = base.FirstBy(filter);
				ExperienciaBO objetoBO = Mapper.Map<TExperiencia, ExperienciaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ExperienciaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TExperiencia entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ExperienciaBO> listadoBO)
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

		public bool Update(ExperienciaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TExperiencia entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ExperienciaBO> listadoBO)
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
		private void AsignacionId(TExperiencia entidad, ExperienciaBO objetoBO)
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

		private TExperiencia MapeoEntidad(ExperienciaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TExperiencia entidad = new TExperiencia();
				entidad = Mapper.Map<ExperienciaBO, TExperiencia>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<ExperienciaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TExperiencia, bool>>> filters, Expression<Func<TExperiencia, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TExperiencia> listado = base.GetFiltered(filters, orderBy, ascending);
			List<ExperienciaBO> listadoBO = new List<ExperienciaBO>();

			foreach (var itemEntidad in listado)
			{
				ExperienciaBO objetoBO = Mapper.Map<TExperiencia, ExperienciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion
		/// Repositorio: ExperienciaRepositorio
		/// Autor: Britsel Calluchi - Luis Huallpa.
		/// Fecha: 08/09/2021
		/// <summary>
		/// Obtiene lista de experiencia registrada
		/// </summary>
		/// <returns>List<ExperienciaDTO></returns>
		public List<ExperienciaDTO> ObtenerExperiencia()
		{
			try
			{
				List<ExperienciaDTO> lista = new List<ExperienciaDTO>();
				var query = "SELECT Id, Nombre, IdAreaTrabajo, AreaTrabajo FROM gp.V_TExperiencia_ObtenerListaExperiencia WHERE Estado = 1";
				var res = _dapper.QueryDapper(query, null);
				if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
				{
					lista = JsonConvert.DeserializeObject<List<ExperienciaDTO>>(res);
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
