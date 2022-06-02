using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
	public class PostulanteFormacionLogRepositorio : BaseRepository<TPostulanteFormacionLog, PostulanteFormacionLogBO>
	{
		#region Metodos Base
		public PostulanteFormacionLogRepositorio() : base()
		{
		}
		public PostulanteFormacionLogRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PostulanteFormacionLogBO> GetBy(Expression<Func<TPostulanteFormacionLog, bool>> filter)
		{
			IEnumerable<TPostulanteFormacionLog> listado = base.GetBy(filter);
			List<PostulanteFormacionLogBO> listadoBO = new List<PostulanteFormacionLogBO>();
			foreach (var itemEntidad in listado)
			{
				PostulanteFormacionLogBO objetoBO = Mapper.Map<TPostulanteFormacionLog, PostulanteFormacionLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PostulanteFormacionLogBO FirstById(int id)
		{
			try
			{
				TPostulanteFormacionLog entidad = base.FirstById(id);
				PostulanteFormacionLogBO objetoBO = new PostulanteFormacionLogBO();
				Mapper.Map<TPostulanteFormacionLog, PostulanteFormacionLogBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PostulanteFormacionLogBO FirstBy(Expression<Func<TPostulanteFormacionLog, bool>> filter)
		{
			try
			{
				TPostulanteFormacionLog entidad = base.FirstBy(filter);
				PostulanteFormacionLogBO objetoBO = Mapper.Map<TPostulanteFormacionLog, PostulanteFormacionLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PostulanteFormacionLogBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPostulanteFormacionLog entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PostulanteFormacionLogBO> listadoBO)
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

		public bool Update(PostulanteFormacionLogBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPostulanteFormacionLog entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PostulanteFormacionLogBO> listadoBO)
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
		private void AsignacionId(TPostulanteFormacionLog entidad, PostulanteFormacionLogBO objetoBO)
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

		private TPostulanteFormacionLog MapeoEntidad(PostulanteFormacionLogBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPostulanteFormacionLog entidad = new TPostulanteFormacionLog();
				entidad = Mapper.Map<PostulanteFormacionLogBO, TPostulanteFormacionLog>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<PostulanteFormacionLogBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TPostulanteFormacionLog, bool>>> filters, Expression<Func<TPostulanteFormacionLog, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TPostulanteFormacionLog> listado = base.GetFiltered(filters, orderBy, ascending);
			List<PostulanteFormacionLogBO> listadoBO = new List<PostulanteFormacionLogBO>();

			foreach (var itemEntidad in listado)
			{
				PostulanteFormacionLogBO objetoBO = Mapper.Map<TPostulanteFormacionLog, PostulanteFormacionLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
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
		public List<PostulanteFormacionLogDTO> ObtenerPostulanteFormacionLog(int idPostulante)
		{
			try
			{
				List<PostulanteFormacionLogDTO> lista = new List<PostulanteFormacionLogDTO>();
				string query = "gp.SP_ObtenerHistorialFormacionPostulante";
				var res = _dapper.QuerySPDapper(query, new { IdPostulante = idPostulante });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<PostulanteFormacionLogDTO>>(res);
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
