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
	/// Repositorio: CompetenciaTecnicaRepositorio
	/// Autor: Luis Huallpa - Edgar Serruto
	/// Fecha: 07/09/2021
	/// <summary>
	/// Repositorio para de tabla T_CompetenciaTecnica
	/// </summary>
	public class CompetenciaTecnicaRepositorio : BaseRepository<TCompetenciaTecnica, CompetenciaTecnicaBO>
	{
		#region Metodos Base
		public CompetenciaTecnicaRepositorio() : base()
		{
		}
		public CompetenciaTecnicaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<CompetenciaTecnicaBO> GetBy(Expression<Func<TCompetenciaTecnica, bool>> filter)
		{
			IEnumerable<TCompetenciaTecnica> listado = base.GetBy(filter);
			List<CompetenciaTecnicaBO> listadoBO = new List<CompetenciaTecnicaBO>();
			foreach (var itemEntidad in listado)
			{
				CompetenciaTecnicaBO objetoBO = Mapper.Map<TCompetenciaTecnica, CompetenciaTecnicaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public CompetenciaTecnicaBO FirstById(int id)
		{
			try
			{
				TCompetenciaTecnica entidad = base.FirstById(id);
				CompetenciaTecnicaBO objetoBO = new CompetenciaTecnicaBO();
				Mapper.Map<TCompetenciaTecnica, CompetenciaTecnicaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public CompetenciaTecnicaBO FirstBy(Expression<Func<TCompetenciaTecnica, bool>> filter)
		{
			try
			{
				TCompetenciaTecnica entidad = base.FirstBy(filter);
				CompetenciaTecnicaBO objetoBO = Mapper.Map<TCompetenciaTecnica, CompetenciaTecnicaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(CompetenciaTecnicaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TCompetenciaTecnica entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<CompetenciaTecnicaBO> listadoBO)
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

		public bool Update(CompetenciaTecnicaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TCompetenciaTecnica entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<CompetenciaTecnicaBO> listadoBO)
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
		private void AsignacionId(TCompetenciaTecnica entidad, CompetenciaTecnicaBO objetoBO)
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

		private TCompetenciaTecnica MapeoEntidad(CompetenciaTecnicaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TCompetenciaTecnica entidad = new TCompetenciaTecnica();
				entidad = Mapper.Map<CompetenciaTecnicaBO, TCompetenciaTecnica>(objetoBO,
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
		/// Repositorio: CompetenciaTecnicaRepositorio 
		/// Autor: Luis Huallpa - Edgar Serruto
		/// Fecha: 07/09/2021
		/// <summary>
		/// Obtiene lista de cursos complementarios para Filtro
		/// </summary>
		/// <returns>List<CursoComplementarioFiltroDTO></returns>
		public List<CursoComplementarioFiltroDTO> ObtenerListaParaFiltro()
		{
			try
			{
				return this.GetBy(x => true).Select(x => new CursoComplementarioFiltroDTO
				{
					Id = x.Id,
					IdTipoCursoComplementario = x.IdTipoCompetenciaTecnica,
					Nombre = x.Nombre
				}).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// Repositorio: CompetenciaTecnicaRepositorio 
		/// Autor: Luis Huallpa - Edgar Serruto
		/// Fecha: 07/09/2021
		/// <summary>
		/// Obtiene lista de cursos complementarios
		/// </summary>
		/// <returns>List<CursoComplementarioDTO></returns>
		public List<CursoComplementarioDTO> ObtenerListaCursoComplementario()
		{
			try
			{
				List<CursoComplementarioDTO> lista = new List<CursoComplementarioDTO>();
				var query = "SELECT Id, Nombre, IdTipoCursoComplementario, TipoCursoComplementario FROM [gp].[V_TCompetenciaTecnica_ObtenerListaCompetenciaTecnica] WHERE Estado = 1";
				var res = _dapper.QueryDapper(query, null);
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<CursoComplementarioDTO>>(res);
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
