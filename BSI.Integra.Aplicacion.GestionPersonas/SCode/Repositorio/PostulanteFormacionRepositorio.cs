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
	public class PostulanteFormacionRepositorio : BaseRepository<TPostulanteFormacion, PostulanteFormacionBO>
	{
		#region Metodos Base
		public PostulanteFormacionRepositorio() : base()
		{
		}
		public PostulanteFormacionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PostulanteFormacionBO> GetBy(Expression<Func<TPostulanteFormacion, bool>> filter)
		{
			IEnumerable<TPostulanteFormacion> listado = base.GetBy(filter);
			List<PostulanteFormacionBO> listadoBO = new List<PostulanteFormacionBO>();
			foreach (var itemEntidad in listado)
			{
				PostulanteFormacionBO objetoBO = Mapper.Map<TPostulanteFormacion, PostulanteFormacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PostulanteFormacionBO FirstById(int id)
		{
			try
			{
				TPostulanteFormacion entidad = base.FirstById(id);
				PostulanteFormacionBO objetoBO = new PostulanteFormacionBO();
				Mapper.Map<TPostulanteFormacion, PostulanteFormacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PostulanteFormacionBO FirstBy(Expression<Func<TPostulanteFormacion, bool>> filter)
		{
			try
			{
				TPostulanteFormacion entidad = base.FirstBy(filter);
				PostulanteFormacionBO objetoBO = Mapper.Map<TPostulanteFormacion, PostulanteFormacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PostulanteFormacionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPostulanteFormacion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PostulanteFormacionBO> listadoBO)
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

		public bool Update(PostulanteFormacionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPostulanteFormacion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PostulanteFormacionBO> listadoBO)
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
		private void AsignacionId(TPostulanteFormacion entidad, PostulanteFormacionBO objetoBO)
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

		private TPostulanteFormacion MapeoEntidad(PostulanteFormacionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPostulanteFormacion entidad = new TPostulanteFormacion();
				entidad = Mapper.Map<PostulanteFormacionBO, TPostulanteFormacion>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<PostulanteFormacionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TPostulanteFormacion, bool>>> filters, Expression<Func<TPostulanteFormacion, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TPostulanteFormacion> listado = base.GetFiltered(filters, orderBy, ascending);
			List<PostulanteFormacionBO> listadoBO = new List<PostulanteFormacionBO>();

			foreach (var itemEntidad in listado)
			{
				PostulanteFormacionBO objetoBO = Mapper.Map<TPostulanteFormacion, PostulanteFormacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion

		/// <summary>
		/// Obtiene lista de formacion academica del postulante
		/// </summary>
		/// <param name="IdPostulante"></param>
		/// <returns></returns>
		public List<PostulanteFormacionDTO> ObtenerPostulanteFormacion(int idPostulante)
		{
			try
			{
				List<PostulanteFormacionDTO> lista = new List<PostulanteFormacionDTO>();
				string query = "SELECT Id, IdPostulante, CentroEstudio, TipoEstudio, AreaFormacion, EstadoEstudio, FechaInicio, FechaFin, AlaActualidad, TurnoEstudio FROM gp.V_TPostulanteFormacion_ObtenerInformacion WHERE Estado = 1 AND IdPostulante = @IdPOstulante ORDER BY AlaActualidad DESC, FechaFin DESC, FechaInicio DESC";
				var res = _dapper.QueryDapper(query, new { IdPostulante = idPostulante });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<PostulanteFormacionDTO>>(res);
				}
				return lista;
			}
			catch (Exception e )
			{
				throw new Exception(e.Message);
			}
		}
		/// Autor: Jashin Salazar
		/// Fecha: 19/11/2021
		/// Version: 1.0
		/// <summary>
		/// Obtiene lista de formacion academica del postulante
		/// </summary>
		/// <param name="idPostulante"> id del postulante</param>
		/// <returns></returns>
		public List<PostulanteFormacionBO> ObtenerPostulanteFormacionV2(int idPostulante)
		{
			try
			{
				List<PostulanteFormacionBO> lista = new List<PostulanteFormacionBO>();
				string query = "SELECT * FROM gp.V_TPostulante_ObtenerFormacionPostulante WHERE IdPostulante=@IdPostulante";
				var res = _dapper.QueryDapper(query, new { IdPostulante = idPostulante });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<PostulanteFormacionBO>>(res);
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
