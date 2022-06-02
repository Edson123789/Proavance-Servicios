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
	public class PostulanteIdiomaRepositorio : BaseRepository<TPostulanteIdioma, PostulanteIdiomaBO>
	{
		#region Metodos Base
		public PostulanteIdiomaRepositorio() : base()
		{
		}
		public PostulanteIdiomaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PostulanteIdiomaBO> GetBy(Expression<Func<TPostulanteIdioma, bool>> filter)
		{
			IEnumerable<TPostulanteIdioma> listado = base.GetBy(filter);
			List<PostulanteIdiomaBO> listadoBO = new List<PostulanteIdiomaBO>();
			foreach (var itemEntidad in listado)
			{
				PostulanteIdiomaBO objetoBO = Mapper.Map<TPostulanteIdioma, PostulanteIdiomaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PostulanteIdiomaBO FirstById(int id)
		{
			try
			{
				TPostulanteIdioma entidad = base.FirstById(id);
				PostulanteIdiomaBO objetoBO = new PostulanteIdiomaBO();
				Mapper.Map<TPostulanteIdioma, PostulanteIdiomaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PostulanteIdiomaBO FirstBy(Expression<Func<TPostulanteIdioma, bool>> filter)
		{
			try
			{
				TPostulanteIdioma entidad = base.FirstBy(filter);
				PostulanteIdiomaBO objetoBO = Mapper.Map<TPostulanteIdioma, PostulanteIdiomaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PostulanteIdiomaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPostulanteIdioma entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PostulanteIdiomaBO> listadoBO)
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

		public bool Update(PostulanteIdiomaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPostulanteIdioma entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PostulanteIdiomaBO> listadoBO)
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
		private void AsignacionId(TPostulanteIdioma entidad, PostulanteIdiomaBO objetoBO)
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

		private TPostulanteIdioma MapeoEntidad(PostulanteIdiomaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPostulanteIdioma entidad = new TPostulanteIdioma();
				entidad = Mapper.Map<PostulanteIdiomaBO, TPostulanteIdioma>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<PostulanteIdiomaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TPostulanteIdioma, bool>>> filters, Expression<Func<TPostulanteIdioma, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TPostulanteIdioma> listado = base.GetFiltered(filters, orderBy, ascending);
			List<PostulanteIdiomaBO> listadoBO = new List<PostulanteIdiomaBO>();

			foreach (var itemEntidad in listado)
			{
				PostulanteIdiomaBO objetoBO = Mapper.Map<TPostulanteIdioma, PostulanteIdiomaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion

		/// <summary>
		/// Obtiene lista de idiomas que estudio el postulante
		/// </summary>
		/// <param name="IdPostulante"></param>
		/// <returns></returns>
		public List<PostulanteIdiomaDTO> ObtenerPostulanteIdioma(int idPostulante)
		{
			try
			{
				List<PostulanteIdiomaDTO> lista = new List<PostulanteIdiomaDTO>();
				string query = "SELECT Id, IdPostulante, Idioma, NivelIdioma FROM gp.V_TPostulanteIdioma_ObtenerInformacion WHERE Estado = 1 AND IdPostulante = @IdPOstulante";
				var res = _dapper.QueryDapper(query, new { IdPostulante = idPostulante });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<PostulanteIdiomaDTO>>(res);
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
