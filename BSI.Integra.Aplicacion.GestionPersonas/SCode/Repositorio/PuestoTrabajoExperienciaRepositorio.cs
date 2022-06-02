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
	/// PuestoTrabajoExperienciaRepositorio
	/// Autor: Luis H., Edgar S.
	/// Fecha: 29/01/2021
	/// <summary>
	/// Gestión de Experiencia Requerida de Perfil de Puestos de Trabajo
	/// </summary>
	public class PuestoTrabajoExperienciaRepositorio : BaseRepository<TPuestoTrabajoExperiencia, PuestoTrabajoExperienciaBO>
	{
		#region Metodos Base
		public PuestoTrabajoExperienciaRepositorio() : base()
		{
		}
		public PuestoTrabajoExperienciaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PuestoTrabajoExperienciaBO> GetBy(Expression<Func<TPuestoTrabajoExperiencia, bool>> filter)
		{
			IEnumerable<TPuestoTrabajoExperiencia> listado = base.GetBy(filter);
			List<PuestoTrabajoExperienciaBO> listadoBO = new List<PuestoTrabajoExperienciaBO>();
			foreach (var itemEntidad in listado)
			{
				PuestoTrabajoExperienciaBO objetoBO = Mapper.Map<TPuestoTrabajoExperiencia, PuestoTrabajoExperienciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PuestoTrabajoExperienciaBO FirstById(int id)
		{
			try
			{
				TPuestoTrabajoExperiencia entidad = base.FirstById(id);
				PuestoTrabajoExperienciaBO objetoBO = new PuestoTrabajoExperienciaBO();
				Mapper.Map<TPuestoTrabajoExperiencia, PuestoTrabajoExperienciaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PuestoTrabajoExperienciaBO FirstBy(Expression<Func<TPuestoTrabajoExperiencia, bool>> filter)
		{
			try
			{
				TPuestoTrabajoExperiencia entidad = base.FirstBy(filter);
				PuestoTrabajoExperienciaBO objetoBO = Mapper.Map<TPuestoTrabajoExperiencia, PuestoTrabajoExperienciaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PuestoTrabajoExperienciaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPuestoTrabajoExperiencia entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PuestoTrabajoExperienciaBO> listadoBO)
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

		public bool Update(PuestoTrabajoExperienciaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPuestoTrabajoExperiencia entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PuestoTrabajoExperienciaBO> listadoBO)
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
		private void AsignacionId(TPuestoTrabajoExperiencia entidad, PuestoTrabajoExperienciaBO objetoBO)
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

		private TPuestoTrabajoExperiencia MapeoEntidad(PuestoTrabajoExperienciaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPuestoTrabajoExperiencia entidad = new TPuestoTrabajoExperiencia();
				entidad = Mapper.Map<PuestoTrabajoExperienciaBO, TPuestoTrabajoExperiencia>(objetoBO,
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

		/// PuestoTrabajoExperienciaRepositorio
		/// Autor: Luis H., Edgar S.
		/// Fecha: 29/01/2021
		/// <summary>
		/// Obtiene lista de experiencia de un determinado puesto de trabajo
		/// </summary>
		/// <returns> Obtiene lista de Experiencias por Perfil de Puesto de Trabajo </returns>
		/// <returns> Lista de Objeto DTO : List<PuestoTrabajoExperienciaDTO> </returns>
		public List<PuestoTrabajoExperienciaDTO> ObtenerPuestoTrabajoExperiencia(int idPerfilPuestoTrabajo)
		{
			try
			{
				List<PuestoTrabajoExperienciaDTO> lista = new List<PuestoTrabajoExperienciaDTO>();
				var _query = "SELECT Id, IdPerfilPuestoTrabajo, IdExperiencia, IdTipoExperiencia, Experiencia, TipoExperiencia, NumeroMinimo, Periodo FROM [gp].[V_TPuestoTrabajoExperiencia_ObtenerListaExperiencia] WHERE Estado = 1 AND IdPerfilPuestoTrabajo = @IdPerfilPuestoTrabajo";
				var res = _dapper.QueryDapper(_query, new { IdPerfilPuestoTrabajo = idPerfilPuestoTrabajo });
				if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
				{
					lista = JsonConvert.DeserializeObject<List<PuestoTrabajoExperienciaDTO>>(res);
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
