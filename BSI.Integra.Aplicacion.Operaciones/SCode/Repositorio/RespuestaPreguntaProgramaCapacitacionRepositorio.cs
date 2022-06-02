using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
	/// Repositorio: Operaciones/RespuestaPreguntaProgramaCapacitacion
    /// Autor: Luis Huallpa - Jorge Rivera - Gian Miranda
    /// Fecha: 22/02/2021
    /// <summary>
    /// Repositorio para consultas de ope.T_RespuestaPreguntaProgramaCapacitacion
    /// </summary>
	public class RespuestaPreguntaProgramaCapacitacionRepositorio : BaseRepository<TRespuestaPreguntaProgramaCapacitacion, RespuestaPreguntaProgramaCapacitacionBO>
	{
		#region Metodos Base
		public RespuestaPreguntaProgramaCapacitacionRepositorio() : base()
		{
		}
		public RespuestaPreguntaProgramaCapacitacionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<RespuestaPreguntaProgramaCapacitacionBO> GetBy(Expression<Func<TRespuestaPreguntaProgramaCapacitacion, bool>> filter)
		{
			IEnumerable<TRespuestaPreguntaProgramaCapacitacion> listado = base.GetBy(filter);
			List<RespuestaPreguntaProgramaCapacitacionBO> listadoBO = new List<RespuestaPreguntaProgramaCapacitacionBO>();
			foreach (var itemEntidad in listado)
			{
				RespuestaPreguntaProgramaCapacitacionBO objetoBO = Mapper.Map<TRespuestaPreguntaProgramaCapacitacion, RespuestaPreguntaProgramaCapacitacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public RespuestaPreguntaProgramaCapacitacionBO FirstById(int id)
		{
			try
			{
				TRespuestaPreguntaProgramaCapacitacion entidad = base.FirstById(id);
				RespuestaPreguntaProgramaCapacitacionBO objetoBO = new RespuestaPreguntaProgramaCapacitacionBO();
				Mapper.Map<TRespuestaPreguntaProgramaCapacitacion, RespuestaPreguntaProgramaCapacitacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public RespuestaPreguntaProgramaCapacitacionBO FirstBy(Expression<Func<TRespuestaPreguntaProgramaCapacitacion, bool>> filter)
		{
			try
			{
				TRespuestaPreguntaProgramaCapacitacion entidad = base.FirstBy(filter);
				RespuestaPreguntaProgramaCapacitacionBO objetoBO = Mapper.Map<TRespuestaPreguntaProgramaCapacitacion, RespuestaPreguntaProgramaCapacitacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(RespuestaPreguntaProgramaCapacitacionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TRespuestaPreguntaProgramaCapacitacion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<RespuestaPreguntaProgramaCapacitacionBO> listadoBO)
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

		public bool Update(RespuestaPreguntaProgramaCapacitacionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TRespuestaPreguntaProgramaCapacitacion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<RespuestaPreguntaProgramaCapacitacionBO> listadoBO)
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
		private void AsignacionId(TRespuestaPreguntaProgramaCapacitacion entidad, RespuestaPreguntaProgramaCapacitacionBO objetoBO)
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

		private TRespuestaPreguntaProgramaCapacitacion MapeoEntidad(RespuestaPreguntaProgramaCapacitacionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TRespuestaPreguntaProgramaCapacitacion entidad = new TRespuestaPreguntaProgramaCapacitacion();
				entidad = Mapper.Map<RespuestaPreguntaProgramaCapacitacionBO, TRespuestaPreguntaProgramaCapacitacion>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<RespuestaPreguntaProgramaCapacitacionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TRespuestaPreguntaProgramaCapacitacion, bool>>> filters, Expression<Func<TRespuestaPreguntaProgramaCapacitacion, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TRespuestaPreguntaProgramaCapacitacion> listado = base.GetFiltered(filters, orderBy, ascending);
			List<RespuestaPreguntaProgramaCapacitacionBO> listadoBO = new List<RespuestaPreguntaProgramaCapacitacionBO>();

			foreach (var itemEntidad in listado)
			{
				RespuestaPreguntaProgramaCapacitacionBO objetoBO = Mapper.Map<TRespuestaPreguntaProgramaCapacitacion, RespuestaPreguntaProgramaCapacitacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion

		/// <summary>
		/// Obtiene respuestas asociadas a una pregunta
		/// </summary>
		/// <param name="idPreguntaProgramaCapacitacion">Id de la pregunta del programa de capacitacion (PK de la tabla ope.T_PreguntaProgramaCapacitacion)</param>
		/// <returns>Lista de objetos de tipo RespuestaPreguntaProgramaCapacitacionDTO</returns>
		public List<RespuestaPreguntaProgramaCapacitacionDTO> ObtenerRespuestaPreguntaProgramaCapacitacion(int idPreguntaProgramaCapacitacion)
		{
			try
			{
				List<RespuestaPreguntaProgramaCapacitacionDTO> objeto = new List<RespuestaPreguntaProgramaCapacitacionDTO>();
				string query = "SELECT Id, IdPreguntaProgramaCapacitacion, RespuestaCorrecta, NroOrdenRespuesta, NroOrden, EnunciadoRespuesta, Puntaje, FeedbackPositivo, FeedbackNegativo FROM [ope].[V_TRespuestaPreguntaProgramaCapacitacion_ObtenerRespuestasPregunta] WHERE Estado = 1 AND IdPreguntaProgramaCapacitacion = @IdPreguntaProgramaCapacitacion";
				var res = _dapper.QueryDapper(query, new { IdPreguntaProgramaCapacitacion = idPreguntaProgramaCapacitacion });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					objeto = JsonConvert.DeserializeObject<List<RespuestaPreguntaProgramaCapacitacionDTO>>(res);
				}
				return objeto;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene tipo respuesta calificacion para filtro
		/// </summary>
		/// <returns>Lista de objetos de tipo FiltroDTO con los tipos de respuesta</returns>
		public List<FiltroDTO> ObtenerTipoRespuestaCalificacion()
		{
			try
			{
				List<FiltroDTO> objeto = new List<FiltroDTO>();
				var query = "SELECT Id, Nombre FROM gp.V_TTipoRespuestaCalificacion_ObtenerTipoRespuestaCalificacion WHERE Estado = 1";
				var res = _dapper.QueryDapper(query, null);
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					objeto = JsonConvert.DeserializeObject<List<FiltroDTO>>(res);
				}
				return objeto;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public List<ListadoPreguntaPorEstructuraBO> ObtenerlistaPreguntasPorGrupo(int IdPgeneral, string GrupoPregunta)
		{
			try
			{
				List<ListadoPreguntaPorEstructuraBO> objeto = new List<ListadoPreguntaPorEstructuraBO>();
				string query = "SELECT Id, IdPgeneral, OrdenFilaCapitulo, OrdenFilaSesion, GrupoPregunta, IdTipoVista, Segundos, OrdenPreguntaGrupo, EnunciadoPregunta, RespuestaAleatoria, MostrarFeedbackInmediato, MostrarFeedbackPorPregunta, NumeroMaximoIntento,TipoRespuesta FROM pla.V_ListadoPreguntaPorEstructura WHERE IdPgeneral = @IdPgeneral AND GrupoPregunta=@GrupoPregunta";
				var res = _dapper.QueryDapper(query, new { IdPgeneral = IdPgeneral, GrupoPregunta = GrupoPregunta });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					objeto = JsonConvert.DeserializeObject<List<ListadoPreguntaPorEstructuraBO>>(res);
				}
				return objeto;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public List<listaNumeroGruposSesionBO> ObtenerListaPreguntasPorSeccionNroGrupo(int IdPgeneral)
		{
			try
			{
				List<listaNumeroGruposSesionBO> objeto = new List<listaNumeroGruposSesionBO>();
				string query = @"WITH CTE_NumeroGruposSesion AS ( 
								SELECT IdPgeneral, OrdenFilaCapitulo, OrdenFilaSesion, GrupoPregunta 
								FROM pla.V_ListadoPreguntaPorEstructura WHERE OrdenFilaSesion is not null and IdPgeneral = 669 
								GROUP BY IdPgeneral, OrdenFilaCapitulo, OrdenFilaSesion, GrupoPregunta 
								) 
								select IdPgeneral, OrdenFilaCapitulo, OrdenFilaSesion, COUNT(OrdenFilaSesion) AS NumeroGrupos, TipoInteraccion='Evaluacion' 
								from CTE_NumeroGruposSesion 
								group by IdPgeneral, OrdenFilaCapitulo, OrdenFilaSesion
								union 
								select IdPgeneral, OrdenFilaCapitulo, OrdenFilaSesion, COUNT(OrdenFilaSesion) AS NumeroGrupos, TipoInteraccion='Crucigrama' 
								from pla.T_CrucigramaProgramaCapacitacion where Estado=1 and OrdenFilaSesion is not null and IdPGeneral=669 
								group by IdPgeneral, OrdenFilaCapitulo, OrdenFilaSesion";

				var res = _dapper.QueryDapper(query, new { IdPgeneral = IdPgeneral });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					objeto = JsonConvert.DeserializeObject<List<listaNumeroGruposSesionBO>>(res);
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
