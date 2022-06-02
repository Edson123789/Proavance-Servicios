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
	public class PostulanteExperienciaLogRepositorio : BaseRepository<TPostulanteExperienciaLog, PostulanteExperienciaLogBO>
	{
		#region Metodos Base
		public PostulanteExperienciaLogRepositorio() : base()
		{
		}
		public PostulanteExperienciaLogRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PostulanteExperienciaLogBO> GetBy(Expression<Func<TPostulanteExperienciaLog, bool>> filter)
		{
			IEnumerable<TPostulanteExperienciaLog> listado = base.GetBy(filter);
			List<PostulanteExperienciaLogBO> listadoBO = new List<PostulanteExperienciaLogBO>();
			foreach (var itemEntidad in listado)
			{
				PostulanteExperienciaLogBO objetoBO = Mapper.Map<TPostulanteExperienciaLog, PostulanteExperienciaLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PostulanteExperienciaLogBO FirstById(int id)
		{
			try
			{
				TPostulanteExperienciaLog entidad = base.FirstById(id);
				PostulanteExperienciaLogBO objetoBO = new PostulanteExperienciaLogBO();
				Mapper.Map<TPostulanteExperienciaLog, PostulanteExperienciaLogBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PostulanteExperienciaLogBO FirstBy(Expression<Func<TPostulanteExperienciaLog, bool>> filter)
		{
			try
			{
				TPostulanteExperienciaLog entidad = base.FirstBy(filter);
				PostulanteExperienciaLogBO objetoBO = Mapper.Map<TPostulanteExperienciaLog, PostulanteExperienciaLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PostulanteExperienciaLogBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPostulanteExperienciaLog entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PostulanteExperienciaLogBO> listadoBO)
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

		public bool Update(PostulanteExperienciaLogBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPostulanteExperienciaLog entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PostulanteExperienciaLogBO> listadoBO)
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
		private void AsignacionId(TPostulanteExperienciaLog entidad, PostulanteExperienciaLogBO objetoBO)
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

		private TPostulanteExperienciaLog MapeoEntidad(PostulanteExperienciaLogBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPostulanteExperienciaLog entidad = new TPostulanteExperienciaLog();
				entidad = Mapper.Map<PostulanteExperienciaLogBO, TPostulanteExperienciaLog>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<PostulanteExperienciaLogBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TPostulanteExperienciaLog, bool>>> filters, Expression<Func<TPostulanteExperienciaLog, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TPostulanteExperienciaLog> listado = base.GetFiltered(filters, orderBy, ascending);
			List<PostulanteExperienciaLogBO> listadoBO = new List<PostulanteExperienciaLogBO>();

			foreach (var itemEntidad in listado)
			{
				PostulanteExperienciaLogBO objetoBO = Mapper.Map<TPostulanteExperienciaLog, PostulanteExperienciaLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion
		/// Autor: Jashin Salazar
		/// Fecha: 19/11/2021
		/// Version: 1.0
		/// <summary>
		/// Obtiene lista de historial formacion academica del postulante
		/// </summary>
		/// <param name="idPostulante"> id del postulante</param>
		/// <returns> List<PostulanteFormacionLogDTO>  </returns>
		public List<PostulanteExperienciaLogDTO> ObtenerPostulanteExperienciaLog(int idPostulante)
		{
			try
			{
				List<PostulanteExperienciaLogDTO> lista = new List<PostulanteExperienciaLogDTO>();
				string query = "gp.SP_ObtenerHistorialExperienciaPostulante";
				var res = _dapper.QuerySPDapper(query, new { IdPostulante = idPostulante });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<PostulanteExperienciaLogDTO>>(res);
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
