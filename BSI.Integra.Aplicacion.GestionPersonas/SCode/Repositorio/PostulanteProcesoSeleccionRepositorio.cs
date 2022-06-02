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
	public class PostulanteProcesoSeleccionRepositorio : BaseRepository<TPostulanteProcesoSeleccion, PostulanteProcesoSeleccionBO>
	{
		#region Metodos Base
		public PostulanteProcesoSeleccionRepositorio() : base()
		{
		}
		public PostulanteProcesoSeleccionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PostulanteProcesoSeleccionBO> GetBy(Expression<Func<TPostulanteProcesoSeleccion, bool>> filter)
		{
			IEnumerable<TPostulanteProcesoSeleccion> listado = base.GetBy(filter);
			List<PostulanteProcesoSeleccionBO> listadoBO = new List<PostulanteProcesoSeleccionBO>();
			foreach (var itemEntidad in listado)
			{
				PostulanteProcesoSeleccionBO objetoBO = Mapper.Map<TPostulanteProcesoSeleccion, PostulanteProcesoSeleccionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PostulanteProcesoSeleccionBO FirstById(int id)
		{
			try
			{
				TPostulanteProcesoSeleccion entidad = base.FirstById(id);
				PostulanteProcesoSeleccionBO objetoBO = new PostulanteProcesoSeleccionBO();
				Mapper.Map<TPostulanteProcesoSeleccion, PostulanteProcesoSeleccionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PostulanteProcesoSeleccionBO FirstBy(Expression<Func<TPostulanteProcesoSeleccion, bool>> filter)
		{
			try
			{
				TPostulanteProcesoSeleccion entidad = base.FirstBy(filter);
				PostulanteProcesoSeleccionBO objetoBO = Mapper.Map<TPostulanteProcesoSeleccion, PostulanteProcesoSeleccionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PostulanteProcesoSeleccionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPostulanteProcesoSeleccion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PostulanteProcesoSeleccionBO> listadoBO)
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

		public bool Update(PostulanteProcesoSeleccionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPostulanteProcesoSeleccion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PostulanteProcesoSeleccionBO> listadoBO)
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
		private void AsignacionId(TPostulanteProcesoSeleccion entidad, PostulanteProcesoSeleccionBO objetoBO)
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

		private TPostulanteProcesoSeleccion MapeoEntidad(PostulanteProcesoSeleccionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPostulanteProcesoSeleccion entidad = new TPostulanteProcesoSeleccion();
				entidad = Mapper.Map<PostulanteProcesoSeleccionBO, TPostulanteProcesoSeleccion>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<PostulanteProcesoSeleccionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TPostulanteProcesoSeleccion, bool>>> filters, Expression<Func<TPostulanteProcesoSeleccion, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TPostulanteProcesoSeleccion> listado = base.GetFiltered(filters, orderBy, ascending);
			List<PostulanteProcesoSeleccionBO> listadoBO = new List<PostulanteProcesoSeleccionBO>();

			foreach (var itemEntidad in listado)
			{
				PostulanteProcesoSeleccionBO objetoBO = Mapper.Map<TPostulanteProcesoSeleccion, PostulanteProcesoSeleccionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion     /// <summary>

		/// Obtiene lista de procesos de seleccion a lso que el postulante se inscribio mediante el idPostulante
		/// </summary>
		/// <param name="idPostulante"></param>
		/// <returns></returns>
		public ProcesoSeleccionInscritoDTO ObtenerProcesoSeleccionInscritoPorId(int idPostulanteProcesoSeleccion)
		{
			try
			{
				var query = "SELECT Id, IdPostulante, Postulante, IdProcesoSeleccion, ProcesoSeleccion, IdPuestoTrabajo, PuestoTrabajo, IdSede, Sede, FechaRegistro FROM [gp].[V_TPostulanteProcesoSeleccion_ObtenerProcesoSeleccionados] WHERE Id = @IdPostulanteProcesoSeleccion AND Estado = 1 ORDER BY FechaRegistro DESC";
				var res = _dapper.FirstOrDefault(query, new { IdPostulanteProcesoSeleccion = idPostulanteProcesoSeleccion });
				return JsonConvert.DeserializeObject<ProcesoSeleccionInscritoDTO>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene datos del postulante junto a su token
		/// </summary>
		/// <param name="idPostulanteProcesoSeleccion"></param>
		/// <returns></returns>
		public PostulanteAccesoProcesoSeleccionDTO ObtenerPostulanteProcesoSeleccion(int idPostulanteProcesoSeleccion)
		{
			try
			{
				var query = "SELECT Id, IdPostulante, Postulante, Dni, Email, ProcesoSeleccion, Token, GuidAccess FROM [gp].[V_TPostulanteProcesoSeleccion_ObtenerPostulanteProceso] WHERE Id = @IdPostulanteProcesoSeleccion AND Estado = 1";
				var res = _dapper.FirstOrDefault(query, new { IdPostulanteProcesoSeleccion = idPostulanteProcesoSeleccion });
				return JsonConvert.DeserializeObject<PostulanteAccesoProcesoSeleccionDTO>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene lista de procesos de seleccion a lso que el postulante se inscribio mediante el idPostulante
		/// </summary>
		/// <param name="idPostulante"></param>
		/// <returns></returns>
		public List<ProcesoSeleccionInscritoDTO> ObtenerProcesoSeleccionInscrito(int idPostulante)
		{
			try
			{
				var query = "SELECT Id, IdPostulante, Postulante, IdProcesoSeleccion, ProcesoSeleccion, IdPuestoTrabajo, PuestoTrabajo, IdSede, Sede, FechaRegistro FROM [gp].[V_TPostulanteProcesoSeleccion_ObtenerProcesoSeleccionados] WHERE IdPostulante = @IdPostulante AND Estado = 1 AND Activo = 1 ORDER BY FechaRegistro DESC";
				var res = _dapper.QueryDapper(query, new { IdPostulante = idPostulante });
				return JsonConvert.DeserializeObject<List<ProcesoSeleccionInscritoDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Elimina los procesos de seleccion asociados
		/// </summary>
		/// <param name="idPostulante"></param>
		/// <returns></returns>
		public bool EliminarProcesoSeleccionAsociado(int idPostulante, int idProcesoSeleccion)
		{
			try
			{
				var resultado = new Dictionary<string, bool>();

				string query = _dapper.QuerySPFirstOrDefault("gp.SP_ProcesoSeleccion_EliminarAsociados", new { IdPostulante = idPostulante, IdProcesoSeleccion = idProcesoSeleccion });
				if (!string.IsNullOrEmpty(query))
				{
					resultado = JsonConvert.DeserializeObject<Dictionary<string, bool>>(query);
				}
				return resultado.Select(x => x.Value).FirstOrDefault();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}


		/// <summary>
		/// Obtiene datos del postulante junto a su token
		/// </summary>
		/// <param name="idPostulanteProcesoSeleccion"></param>
		/// <returns></returns>
		public PostulanteAccesoProcesoSeleccionDTO ObtenerPostulanteProcesoSeleccionPorIdPostulante(int idPostulante)
		{
			try
			{
				var query = "SELECT Id, IdPostulante, Postulante, Dni, Email, ProcesoSeleccion, Token, GuidAccess FROM [gp].[V_TPostulanteProcesoSeleccion_ObtenerPostulanteProceso] WHERE IdPostulante = @IdPostulante AND Estado = 1";
				var res = _dapper.FirstOrDefault(query, new { IdPostulante = idPostulante });
				return JsonConvert.DeserializeObject<PostulanteAccesoProcesoSeleccionDTO>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
