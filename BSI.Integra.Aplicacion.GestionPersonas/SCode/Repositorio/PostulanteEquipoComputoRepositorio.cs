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
	public class PostulanteEquipoComputoRepositorio : BaseRepository<TPostulanteEquipoComputo, PostulanteEquipoComputoBO>
	{
		#region Metodos Base
		public PostulanteEquipoComputoRepositorio() : base()
		{
		}
		public PostulanteEquipoComputoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PostulanteEquipoComputoBO> GetBy(Expression<Func<TPostulanteEquipoComputo, bool>> filter)
		{
			IEnumerable<TPostulanteEquipoComputo> listado = base.GetBy(filter);
			List<PostulanteEquipoComputoBO> listadoBO = new List<PostulanteEquipoComputoBO>();
			foreach (var itemEntidad in listado)
			{
				PostulanteEquipoComputoBO objetoBO = Mapper.Map<TPostulanteEquipoComputo, PostulanteEquipoComputoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PostulanteEquipoComputoBO FirstById(int id)
		{
			try
			{
				TPostulanteEquipoComputo entidad = base.FirstById(id);
				PostulanteEquipoComputoBO objetoBO = new PostulanteEquipoComputoBO();
				Mapper.Map<TPostulanteEquipoComputo, PostulanteEquipoComputoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PostulanteEquipoComputoBO FirstBy(Expression<Func<TPostulanteEquipoComputo, bool>> filter)
		{
			try
			{
				TPostulanteEquipoComputo entidad = base.FirstBy(filter);
				PostulanteEquipoComputoBO objetoBO = Mapper.Map<TPostulanteEquipoComputo, PostulanteEquipoComputoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PostulanteEquipoComputoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPostulanteEquipoComputo entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PostulanteEquipoComputoBO> listadoBO)
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

		public bool Update(PostulanteEquipoComputoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPostulanteEquipoComputo entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PostulanteEquipoComputoBO> listadoBO)
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
		private void AsignacionId(TPostulanteEquipoComputo entidad, PostulanteEquipoComputoBO objetoBO)
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

		private TPostulanteEquipoComputo MapeoEntidad(PostulanteEquipoComputoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPostulanteEquipoComputo entidad = new TPostulanteEquipoComputo();
				entidad = Mapper.Map<PostulanteEquipoComputoBO, TPostulanteEquipoComputo>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<PostulanteEquipoComputoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TPostulanteEquipoComputo, bool>>> filters, Expression<Func<TPostulanteEquipoComputo, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TPostulanteEquipoComputo> listado = base.GetFiltered(filters, orderBy, ascending);
			List<PostulanteEquipoComputoBO> listadoBO = new List<PostulanteEquipoComputoBO>();

			foreach (var itemEntidad in listado)
			{
				PostulanteEquipoComputoBO objetoBO = Mapper.Map<TPostulanteEquipoComputo, PostulanteEquipoComputoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion

		/// <summary>
		/// Obtiene equipo de computo del postulante
		/// </summary>
		/// <param name="IdPostulante"></param>
		/// <returns></returns>
		public PostulanteEquipoComputoDTO ObtenerPostulanteEquipoComputo(int idPostulante)
		{
			try
			{
				PostulanteEquipoComputoDTO objeto = new PostulanteEquipoComputoDTO();
				string query = "SELECT Id, IdPostulante, TipoEquipo, MemoriaRam, SistemaOperativo, Procesador, Mouse, Auricular, Camara, EsEquipoTrabajo FROM gp.V_TPostulanteEquipoComputo_ObtenerInformacion WHERE Estado = 1 AND IdPostulante = @IdPOstulante";
				var res = _dapper.FirstOrDefault(query, new { IdPostulante = idPostulante });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					objeto = JsonConvert.DeserializeObject<PostulanteEquipoComputoDTO>(res);
				}
				return objeto;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
