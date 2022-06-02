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
	/// Repositorio: NivelCompetenciaTecnicaRepositorio
	/// Autor: Luis Huallpa Edgar Serruto.
	/// Fecha: 29/01/2021
	/// <summary>
	/// Gestión de Niveles de Competencia
	/// </summary>
	public class NivelCompetenciaTecnicaRepositorio : BaseRepository<TNivelCompetenciaTecnica, NivelCompetenciaTecnicaBO>
	{
		#region Metodos Base
		public NivelCompetenciaTecnicaRepositorio() : base()
		{
		}
		public NivelCompetenciaTecnicaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<NivelCompetenciaTecnicaBO> GetBy(Expression<Func<TNivelCompetenciaTecnica, bool>> filter)
		{
			IEnumerable<TNivelCompetenciaTecnica> listado = base.GetBy(filter);
			List<NivelCompetenciaTecnicaBO> listadoBO = new List<NivelCompetenciaTecnicaBO>();
			foreach (var itemEntidad in listado)
			{
				NivelCompetenciaTecnicaBO objetoBO = Mapper.Map<TNivelCompetenciaTecnica, NivelCompetenciaTecnicaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public NivelCompetenciaTecnicaBO FirstById(int id)
		{
			try
			{
				TNivelCompetenciaTecnica entidad = base.FirstById(id);
				NivelCompetenciaTecnicaBO objetoBO = new NivelCompetenciaTecnicaBO();
				Mapper.Map<TNivelCompetenciaTecnica, NivelCompetenciaTecnicaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public NivelCompetenciaTecnicaBO FirstBy(Expression<Func<TNivelCompetenciaTecnica, bool>> filter)
		{
			try
			{
				TNivelCompetenciaTecnica entidad = base.FirstBy(filter);
				NivelCompetenciaTecnicaBO objetoBO = Mapper.Map<TNivelCompetenciaTecnica, NivelCompetenciaTecnicaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(NivelCompetenciaTecnicaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TNivelCompetenciaTecnica entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<NivelCompetenciaTecnicaBO> listadoBO)
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

		public bool Update(NivelCompetenciaTecnicaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TNivelCompetenciaTecnica entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<NivelCompetenciaTecnicaBO> listadoBO)
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
		private void AsignacionId(TNivelCompetenciaTecnica entidad, NivelCompetenciaTecnicaBO objetoBO)
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

		private TNivelCompetenciaTecnica MapeoEntidad(NivelCompetenciaTecnicaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TNivelCompetenciaTecnica entidad = new TNivelCompetenciaTecnica();
				entidad = Mapper.Map<NivelCompetenciaTecnicaBO, TNivelCompetenciaTecnica>(objetoBO,
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

		/// Repositorio: NivelCompetenciaTecnicaRepositorio
		/// Autor: Edgar S.
		/// Fecha: 29/01/2021
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
