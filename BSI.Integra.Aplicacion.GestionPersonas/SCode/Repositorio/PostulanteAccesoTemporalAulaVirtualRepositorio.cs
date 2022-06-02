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
using static BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas.PostulanteAccesoTemporalAulaVirtualDTO;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
	/// Repositorio: GestionPersonas/PostulanteAccesoTemporalAulaVirtual
	/// Autor: Edgar Serruto
	/// Fecha: 21/06/2021
	/// <summary>
	/// Repositorio para de tabla T_PostulanteAccesoTemporalAulaVirtual
	/// </summary>
	public class PostulanteAccesoTemporalAulaVirtualRepositorio : BaseRepository<TPostulanteAccesoTemporalAulaVirtual, PostulanteAccesoTemporalAulaVirtualBO>
	{
		#region Metodos Base
		public PostulanteAccesoTemporalAulaVirtualRepositorio() : base()
		{
		}
		public PostulanteAccesoTemporalAulaVirtualRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PostulanteAccesoTemporalAulaVirtualBO> GetBy(Expression<Func<TPostulanteAccesoTemporalAulaVirtual, bool>> filter)
		{
			IEnumerable<TPostulanteAccesoTemporalAulaVirtual> listado = base.GetBy(filter);
			List<PostulanteAccesoTemporalAulaVirtualBO> listadoBO = new List<PostulanteAccesoTemporalAulaVirtualBO>();
			foreach (var itemEntidad in listado)
			{
				PostulanteAccesoTemporalAulaVirtualBO objetoBO = Mapper.Map<TPostulanteAccesoTemporalAulaVirtual, PostulanteAccesoTemporalAulaVirtualBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PostulanteAccesoTemporalAulaVirtualBO FirstById(int id)
		{
			try
			{
				TPostulanteAccesoTemporalAulaVirtual entidad = base.FirstById(id);
				PostulanteAccesoTemporalAulaVirtualBO objetoBO = new PostulanteAccesoTemporalAulaVirtualBO();
				Mapper.Map<TPostulanteAccesoTemporalAulaVirtual, PostulanteAccesoTemporalAulaVirtualBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PostulanteAccesoTemporalAulaVirtualBO FirstBy(Expression<Func<TPostulanteAccesoTemporalAulaVirtual, bool>> filter)
		{
			try
			{
				TPostulanteAccesoTemporalAulaVirtual entidad = base.FirstBy(filter);
				PostulanteAccesoTemporalAulaVirtualBO objetoBO = Mapper.Map<TPostulanteAccesoTemporalAulaVirtual, PostulanteAccesoTemporalAulaVirtualBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PostulanteAccesoTemporalAulaVirtualBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPostulanteAccesoTemporalAulaVirtual entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PostulanteAccesoTemporalAulaVirtualBO> listadoBO)
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

		public bool Update(PostulanteAccesoTemporalAulaVirtualBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPostulanteAccesoTemporalAulaVirtual entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PostulanteAccesoTemporalAulaVirtualBO> listadoBO)
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
		private void AsignacionId(TPostulanteAccesoTemporalAulaVirtual entidad, PostulanteAccesoTemporalAulaVirtualBO objetoBO)
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

		private TPostulanteAccesoTemporalAulaVirtual MapeoEntidad(PostulanteAccesoTemporalAulaVirtualBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPostulanteAccesoTemporalAulaVirtual entidad = new TPostulanteAccesoTemporalAulaVirtual();
				entidad = Mapper.Map<PostulanteAccesoTemporalAulaVirtualBO, TPostulanteAccesoTemporalAulaVirtual>(objetoBO,
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
		/// Repositorio: PostulanteAccesoTemporalAulaVirtualRepositorio
		/// Autor: Edgar Serruto.
		/// Fecha: 22/06/2021
		/// <summary>
		/// Actualiza en la tabla de los accesos temporales para el postulante en la DB del portal web
		/// </summary>
		/// <param name="idPostulante">Id del postulante que se le va a otorgar los accesos temporales (PK de la tabla gp.T_Postulante)</param>
		/// <param name="idUsuarioPortal">Id del usuario del portal, (PK de la tabla dbo.AspNetUsers del portal web)</param>
		/// <param name="idAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
		/// <param name="idPespecifico"> Id de Programa Específico Relaciado a componente </param>
		/// <returns>Bool</returns>
		public bool ActualizarAccesosTemporalesPortalWeb(int idPostulante, string idUsuarioPortal, int idAlumno, int idPespecifico)
		{
			try
			{
				var resultado = new ValorBoolDTO();
				var query = "gp.SP_GenerarAccesosTemporalesPostulante";
				var respuestaQuery = _dapper.QuerySPFirstOrDefault(query, new { IdPostulante = idPostulante, IdUsuarioPortal = idUsuarioPortal, IdAlumno = idAlumno, IdPespecifico = idPespecifico });
				if (!string.IsNullOrEmpty(respuestaQuery) && !respuestaQuery.Contains("[]"))
				{
					resultado = JsonConvert.DeserializeObject<ValorBoolDTO>(respuestaQuery);
				}
				return true;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// Repositorio: PostulanteAccesoTemporalAulaVirtualRepositorio
		/// Autor: Edgar Serruto.
		/// Fecha: 22/06/2021
		/// <summary>
		/// Obtiene el información de accesos del portal web por el correo
		/// </summary>
		/// <param name="email">Cadena con el Id del usuario del portal web</param>
		/// <returns> RespuestaAccesosPostulanteDTO </returns>
		public RespuestaAccesosPostulanteDTO ObtenerAccesosPortalWebCorreo(string email)
		{
			try
			{
				RespuestaAccesosPostulanteDTO resultado = new RespuestaAccesosPostulanteDTO();
				var query = "[conf].[SP_ObtenerAccesosPortalWebPorCorreo]";
				var respuestaQuery = _dapper.QuerySPFirstOrDefault(query, new { Email = email });
				if (!string.IsNullOrEmpty(respuestaQuery) && respuestaQuery != "null" && !respuestaQuery.Contains("[]"))
				{
					resultado = JsonConvert.DeserializeObject<RespuestaAccesosPostulanteDTO>(respuestaQuery);
				}
				else
				{
					resultado.IdAlumno = 0;
				}
				return resultado;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// Repositorio: PostulanteAccesoTemporalAulaVirtualRepositorio
		/// Autor: Edgar Serruto.
		/// Fecha: 22/06/2021
		/// <summary>
		/// Obtiene informacion de postulante por proceso de selección
		/// </summary>
		/// <param name="condiciones"> Condiciones de Consulta </param>
		/// <returns> List<RespuestaProcesoSeleccionPostulanteDTO> </returns>
		public List<RespuestaProcesoSeleccionPostulanteDTO> ObtenerPostulantesPorCondiciones(string condiciones)
		{
			try
			{
				List<RespuestaProcesoSeleccionPostulanteDTO> listaPostulantes = new List<RespuestaProcesoSeleccionPostulanteDTO>();
				string query = "SELECT DISTINCT IdPostulante, IdProcesoSeleccion FROM gp.V_TPostulanteProcesoSeleccion_ObtenerPostulantePorProcesoSeleccion WHERE " + condiciones;
				var resultado = _dapper.QueryDapper(query, null);
				if (!resultado.Contains("[]") && !string.IsNullOrEmpty(resultado))
				{
					listaPostulantes = JsonConvert.DeserializeObject<List<RespuestaProcesoSeleccionPostulanteDTO>>(resultado);
				}				
				return listaPostulantes;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// Repositorio: PostulanteAccesoTemporalAulaVirtualRepositorio
		/// Autor: Edgar Serruto.
		/// Fecha: 22/06/2021
		/// <summary>
		/// Obtiene informacion de postulante por Etapas de Proceso
		/// </summary>
		/// <param name="condiciones"> Condiciones de Consulta </param>
		/// <returns> List<RespuestaIdPostulanteDTO> </returns>
		public List<RespuestaIdPostulanteDTO> ObtenerPostulantesEtapaPorCondiciones(string condiciones)
		{
			try
			{
				List<RespuestaIdPostulanteDTO> listaPostulantes = new List<RespuestaIdPostulanteDTO>();
				string query = "SELECT DISTINCT IdPostulante FROM gp.V_TEtapaProcesoSeleccionCalificado_ObtenerPostulanteProcesoEtapaEstado WHERE " + condiciones;
				var resultado = _dapper.QueryDapper(query, null);
				if (!resultado.Contains("[]") && !string.IsNullOrEmpty(resultado))
				{
					listaPostulantes = JsonConvert.DeserializeObject<List<RespuestaIdPostulanteDTO>>(resultado);
				}
				return listaPostulantes;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// Repositorio: PostulanteAccesoTemporalAulaVirtualRepositorio
		/// Autor: Edgar Serruto.
		/// Fecha: 22/06/2021
		/// <summary>
		/// Obtiene informacion de postulante por Fecha de Accesos
		/// </summary>
		/// <param name="condiciones">Condiciones de Consulta</param>
		/// <param name="fechaFin">Filtro de Fecha Fin</param>
		/// <param name="fechaInicio">Filtro de Fecha Inicio</param>
		/// <returns>List<AccesosRegistradosPostulanteDTO></returns>
		public List<AccesosRegistradosPostulanteDTO> ObtenerPostulantesAccesosPorCondiciones(string condiciones, DateTime? fechaInicio, DateTime? fechaFin)
		{
			try
			{
				DateTime nuevoInicio = new DateTime(1900, 1, 1, 0, 0, 0);
				DateTime nuevoFin = new DateTime(2200, 1, 1, 0, 0, 0);
				string condicionFinal = string.Empty;
				if (fechaInicio == null && fechaFin == null)
				{
					condicionFinal = "WHERE IdPostulante > 0" + condiciones;
				}
				else
				{
					if (fechaInicio == null)
					{
						fechaInicio = nuevoInicio;						
					}
                    else
                    {
						fechaInicio = fechaInicio.GetValueOrDefault().Date;
					}
					if (fechaFin == null)
					{
						fechaFin = nuevoFin;
					}
                    else
                    {
						fechaFin = fechaFin.GetValueOrDefault().Date;
					}
					condicionFinal = "WHERE (FechaInicio BETWEEN @FechaInicio AND @FechaFin) AND (FechaFin BETWEEN @FechaInicio AND @FechaFin)" + condiciones;
				}
				List<AccesosRegistradosPostulanteDTO> listaPostulantes = new List<AccesosRegistradosPostulanteDTO>();
				string query =
					@"SELECT 
						IdPostulante,
						Postulante,
						NroDocumento,
						IdExamen,
						Examen,
						EstadoAcceso,
						IdProcesoSeleccion,
						Pespecifico,
						FechaInicio,
						FechaFin
					FROM gp.V_TEstadoExamen_ObtenerPostulantesAcessoFecha " + condicionFinal;
				var resultado = _dapper.QueryDapper(query, new { FechaInicio = fechaInicio, FechaFin = fechaFin });
				listaPostulantes = JsonConvert.DeserializeObject<List<AccesosRegistradosPostulanteDTO>>(resultado);
				return listaPostulantes;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
