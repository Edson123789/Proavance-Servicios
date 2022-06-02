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
	/// Repositorio: TipoCompetenciaTecnicaRepositorio
	/// Autor: Edgar S.
	/// Fecha: 29/01/2021
	/// <summary>
	/// Gestión de Competencias Técnicas
	/// </summary>
	public class TipoCompetenciaTecnicaRepositorio : BaseRepository<TTipoCompetenciaTecnica, TipoCompetenciaTecnicaBO>
	{
		#region Metodos Base
		public TipoCompetenciaTecnicaRepositorio() : base()
		{
		}
		public TipoCompetenciaTecnicaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<TipoCompetenciaTecnicaBO> GetBy(Expression<Func<TTipoCompetenciaTecnica, bool>> filter)
		{
			IEnumerable<TTipoCompetenciaTecnica> listado = base.GetBy(filter);
			List<TipoCompetenciaTecnicaBO> listadoBO = new List<TipoCompetenciaTecnicaBO>();
			foreach (var itemEntidad in listado)
			{
				TipoCompetenciaTecnicaBO objetoBO = Mapper.Map<TTipoCompetenciaTecnica, TipoCompetenciaTecnicaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public TipoCompetenciaTecnicaBO FirstById(int id)
		{
			try
			{
				TTipoCompetenciaTecnica entidad = base.FirstById(id);
				TipoCompetenciaTecnicaBO objetoBO = new TipoCompetenciaTecnicaBO();
				Mapper.Map<TTipoCompetenciaTecnica, TipoCompetenciaTecnicaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public TipoCompetenciaTecnicaBO FirstBy(Expression<Func<TTipoCompetenciaTecnica, bool>> filter)
		{
			try
			{
				TTipoCompetenciaTecnica entidad = base.FirstBy(filter);
				TipoCompetenciaTecnicaBO objetoBO = Mapper.Map<TTipoCompetenciaTecnica, TipoCompetenciaTecnicaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(TipoCompetenciaTecnicaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TTipoCompetenciaTecnica entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<TipoCompetenciaTecnicaBO> listadoBO)
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

		public bool Update(TipoCompetenciaTecnicaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TTipoCompetenciaTecnica entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<TipoCompetenciaTecnicaBO> listadoBO)
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
		private void AsignacionId(TTipoCompetenciaTecnica entidad, TipoCompetenciaTecnicaBO objetoBO)
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

		private TTipoCompetenciaTecnica MapeoEntidad(TipoCompetenciaTecnicaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TTipoCompetenciaTecnica entidad = new TTipoCompetenciaTecnica();
				entidad = Mapper.Map<TipoCompetenciaTecnicaBO, TTipoCompetenciaTecnica>(objetoBO,
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
		/// Repositorio: TipoCompetenciaTecnicaRepositorio
		/// Autor: Edgar Serruto.
		/// Fecha: 07/09/2021
		/// <summary>
		/// Obtiene lista de elementos registrados para combo
		/// </summary>
		/// <returns>List<TipoCursoComplementarioDTO></returns>
		public List<TipoCursoComplementarioDTO> ObtenerListaParaFiltro()
		{
			try
			{
				return this.GetBy(x => true).Select(x => new TipoCursoComplementarioDTO
				{
					IdTipoCursoComplementario = x.Id,
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
