using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
	/// Repositorio: PostulanteCursoPortalNotasHistoricoRepositorio
	/// Autor: Edgar Serruto
	/// Fecha: 02/08/2021
	/// <summary>
	/// Repositorio para de tabla T_PostulanteCursoPortalNotasHistorico
	/// </summary>
	public class PostulanteCursoPortalNotasHistoricoRepositorio : BaseRepository<TPostulanteCursoPortalNotasHistorico, PostulanteCursoPortalNotasHistoricoBO>
	{
		#region Metodos Base
		public PostulanteCursoPortalNotasHistoricoRepositorio() : base()
		{
		}
		public PostulanteCursoPortalNotasHistoricoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PostulanteCursoPortalNotasHistoricoBO> GetBy(Expression<Func<TPostulanteCursoPortalNotasHistorico, bool>> filter)
		{
			IEnumerable<TPostulanteCursoPortalNotasHistorico> listado = base.GetBy(filter);
			List<PostulanteCursoPortalNotasHistoricoBO> listadoBO = new List<PostulanteCursoPortalNotasHistoricoBO>();
			foreach (var itemEntidad in listado)
			{
				PostulanteCursoPortalNotasHistoricoBO objetoBO = Mapper.Map<TPostulanteCursoPortalNotasHistorico, PostulanteCursoPortalNotasHistoricoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PostulanteCursoPortalNotasHistoricoBO FirstById(int id)
		{
			try
			{
				TPostulanteCursoPortalNotasHistorico entidad = base.FirstById(id);
				PostulanteCursoPortalNotasHistoricoBO objetoBO = new PostulanteCursoPortalNotasHistoricoBO();
				Mapper.Map<TPostulanteCursoPortalNotasHistorico, PostulanteCursoPortalNotasHistoricoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PostulanteCursoPortalNotasHistoricoBO FirstBy(Expression<Func<TPostulanteCursoPortalNotasHistorico, bool>> filter)
		{
			try
			{
				TPostulanteCursoPortalNotasHistorico entidad = base.FirstBy(filter);
				PostulanteCursoPortalNotasHistoricoBO objetoBO = Mapper.Map<TPostulanteCursoPortalNotasHistorico, PostulanteCursoPortalNotasHistoricoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PostulanteCursoPortalNotasHistoricoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPostulanteCursoPortalNotasHistorico entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PostulanteCursoPortalNotasHistoricoBO> listadoBO)
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

		public bool Update(PostulanteCursoPortalNotasHistoricoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPostulanteCursoPortalNotasHistorico entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PostulanteCursoPortalNotasHistoricoBO> listadoBO)
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
		private void AsignacionId(TPostulanteCursoPortalNotasHistorico entidad, PostulanteCursoPortalNotasHistoricoBO objetoBO)
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

		private TPostulanteCursoPortalNotasHistorico MapeoEntidad(PostulanteCursoPortalNotasHistoricoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPostulanteCursoPortalNotasHistorico entidad = new TPostulanteCursoPortalNotasHistorico();
				entidad = Mapper.Map<PostulanteCursoPortalNotasHistoricoBO, TPostulanteCursoPortalNotasHistorico>(objetoBO,
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
		/// Repositorio: PostulanteCursoPortalNotasHistoricoRepositorio 
		/// Autor: Edgar Serruto
		/// Fecha: 21/09/2021
		/// <summary>
		/// Obtiene lista de notas de postulante
		/// </summary>
		/// <param name="idAlumno">FK de T_Alumno</param>
		/// <param name="idPespecifico">FK de T_Pespecifico</param>
		/// <returns> List<PostulanteCursoPortalNotasHistoricoDTO> </returns>
		public List<PostulanteCursoPortalNotasHistoricoDTO> ObtenerNotasAnteriores(int idAlumno, int idPespecifico)
		{
			try
			{
				List<PostulanteCursoPortalNotasHistoricoDTO> listaNotas = new List<PostulanteCursoPortalNotasHistoricoDTO>();
				string query = string.Empty;
				query = @"SELECT  
							Id,
							IdPgeneral,
							OrdenFilaCapitulo,
							OrdenFilaSesion,
							GrupoPregunta,
							Calificacion,
							IdUsuario,
							IdAlumno,
							IdPespecifico,
							AccesoPrueba
						FROM gp.V_ObtenerNotasPortal_Postulante WHERE IdAlumno = @IdAlumno AND IdPespecifico = @IdPespecifico";
				var respuesta = _dapper.QueryDapper(query, new { IdAlumno = idAlumno, IdPespecifico = idPespecifico });
				if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
				{
					listaNotas = JsonConvert.DeserializeObject<List<PostulanteCursoPortalNotasHistoricoDTO>>(respuesta);
				}
				return listaNotas;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// Repositorio: PostulanteCursoPortalNotasHistoricoRepositorio 
		/// Autor: Edgar Serruto
		/// Fecha: 21/09/2021
		/// <summary>
		/// Obtiene lista de notas de postulante
		/// </summary>
		/// <param name="idPGeneral">FK de T_Pgeneral</param>
		/// <param name="idUsuario">Id de Usuario de portal</param>
		/// <returns>List<PostulanteVideoVisualizacionDTO></returns>
		public List<PostulanteVideoVisualizacionDTO> ObtenerVisualizacionVideoAnteriores(string idUsuario, int idPGeneral)
		{
			try
			{
				List<PostulanteVideoVisualizacionDTO> listaNotas = new List<PostulanteVideoVisualizacionDTO>();
				string query = string.Empty;
				query = @"SELECT  
							Id,
							IdPGeneral,
							IdPrincipal,
							IdUsuario
						FROM gp.V_ObtenerVisualizacionVideosPortal_Postulante WHERE IdUsuario = @IdUsuario AND IdPGeneral = @IdPGeneral";
				var respuesta = _dapper.QueryDapper(query, new { IdUsuario = idUsuario, IdPGeneral = idPGeneral });
				if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
				{
					listaNotas = JsonConvert.DeserializeObject<List<PostulanteVideoVisualizacionDTO>>(respuesta);
				}
				return listaNotas;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// Repositorio: PostulanteCursoPortalNotasHistoricoRepositorio 
		/// Autor: Edgar Serruto
		/// Fecha: 03/08/2021
		/// <summary>
		/// Elimina físicamente registros anteriores de postulante notas y video
		/// </summary>
		/// <param name="idUsuario">Id de Usuario</param>
		/// <param name="idPGeneral">Id de Programa General </param>
		/// <param name="listaIdNota">Lista de Id de Notas</param>
		/// <param name="listaIdVideo">Lista de Id de Videos</param>
		/// <returns>bool</returns>
		public bool EliminarFisicamenteAnterioresNotas(string idUsuario, int idPGeneral, List<int> listaIdNota, List<int> listaIdVideo)
		{
			try
			{
                if (listaIdNota.Any() && listaIdVideo.Any() && idUsuario.Length > 0 && idPGeneral > 0)
                {
					var listaNotas = new ValorBoolDTO();
					var filtros = new
					{
						IdUsuario = idUsuario,
						IdPGeneral = idPGeneral,
						ListaIdNotas = string.Join(",", listaIdNota.Select(x => x)),
						ListaIdVideo = string.Join(",", listaIdVideo.Select(x => x))
					};
					string sp = "gp.SP_EliminarFisicamenteNotasCursoPortal";
					var respuesta = _dapper.QuerySPFirstOrDefault(sp, filtros);
					if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
					{
						listaNotas = JsonConvert.DeserializeObject<ValorBoolDTO>(respuesta);
					}
					return listaNotas.Valor;
				}
                else
                {
					return false;
                }
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
