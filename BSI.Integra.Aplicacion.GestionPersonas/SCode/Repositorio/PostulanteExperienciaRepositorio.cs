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
	public class PostulanteExperienciaRepositorio : BaseRepository<TPostulanteExperiencia, PostulanteExperienciaBO>
	{
		#region Metodos Base
		public PostulanteExperienciaRepositorio() : base()
		{
		}
		public PostulanteExperienciaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PostulanteExperienciaBO> GetBy(Expression<Func<TPostulanteExperiencia, bool>> filter)
		{
			IEnumerable<TPostulanteExperiencia> listado = base.GetBy(filter);
			List<PostulanteExperienciaBO> listadoBO = new List<PostulanteExperienciaBO>();
			foreach (var itemEntidad in listado)
			{
				PostulanteExperienciaBO objetoBO = Mapper.Map<TPostulanteExperiencia, PostulanteExperienciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PostulanteExperienciaBO FirstById(int id)
		{
			try
			{
				TPostulanteExperiencia entidad = base.FirstById(id);
				PostulanteExperienciaBO objetoBO = new PostulanteExperienciaBO();
				Mapper.Map<TPostulanteExperiencia, PostulanteExperienciaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PostulanteExperienciaBO FirstBy(Expression<Func<TPostulanteExperiencia, bool>> filter)
		{
			try
			{
				TPostulanteExperiencia entidad = base.FirstBy(filter);
				PostulanteExperienciaBO objetoBO = Mapper.Map<TPostulanteExperiencia, PostulanteExperienciaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PostulanteExperienciaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPostulanteExperiencia entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PostulanteExperienciaBO> listadoBO)
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

		public bool Update(PostulanteExperienciaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPostulanteExperiencia entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PostulanteExperienciaBO> listadoBO)
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
		private void AsignacionId(TPostulanteExperiencia entidad, PostulanteExperienciaBO objetoBO)
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

		private TPostulanteExperiencia MapeoEntidad(PostulanteExperienciaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPostulanteExperiencia entidad = new TPostulanteExperiencia();
				entidad = Mapper.Map<PostulanteExperienciaBO, TPostulanteExperiencia>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<PostulanteExperienciaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TPostulanteExperiencia, bool>>> filters, Expression<Func<TPostulanteExperiencia, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TPostulanteExperiencia> listado = base.GetFiltered(filters, orderBy, ascending);
			List<PostulanteExperienciaBO> listadoBO = new List<PostulanteExperienciaBO>();

			foreach (var itemEntidad in listado)
			{
				PostulanteExperienciaBO objetoBO = Mapper.Map<TPostulanteExperiencia, PostulanteExperienciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion

		/// <summary>
		/// Obtiene lista de experiencia laboral profesional del postulante
		/// </summary>
		/// <param name="IdPostulante"></param>
		/// <returns></returns>
		public List<PostulanteExperienciaDTO> ObtenerPostulanteExperiencia(int idPostulante)
		{
			try
			{
				List<PostulanteExperienciaDTO> lista = new List<PostulanteExperienciaDTO>();
				string query = "SELECT Id, IdPostulante, Empresa, Cargo, AreaTrabajo, Industria, FechaInicio, FechaFin, NombreJefe, NumeroJefe, AlaActualidad, EsUltimoEmpleo, Salario, Funcion, MesesExperiencia FROM gp.V_TPostulanteExperiencia_ObtenerInformacion WHERE Estado = 1 AND IdPostulante = @IdPOstulante ORDER BY AlaActualidad DESC, FechaFin DESC";
				var res = _dapper.QueryDapper(query, new { IdPostulante = idPostulante });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<PostulanteExperienciaDTO>>(res);
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
