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
	public class RespuestaPreguntaRepositorio : BaseRepository<TRespuestaPregunta, RespuestaPreguntaBO>
	{
		#region Metodos Base
		public RespuestaPreguntaRepositorio() : base()
		{
		}
		public RespuestaPreguntaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<RespuestaPreguntaBO> GetBy(Expression<Func<TRespuestaPregunta, bool>> filter)
		{
			IEnumerable<TRespuestaPregunta> listado = base.GetBy(filter);
			List<RespuestaPreguntaBO> listadoBO = new List<RespuestaPreguntaBO>();
			foreach (var itemEntidad in listado)
			{
				RespuestaPreguntaBO objetoBO = Mapper.Map<TRespuestaPregunta, RespuestaPreguntaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public RespuestaPreguntaBO FirstById(int id)
		{
			try
			{
				TRespuestaPregunta entidad = base.FirstById(id);
				RespuestaPreguntaBO objetoBO = new RespuestaPreguntaBO();
				Mapper.Map<TRespuestaPregunta, RespuestaPreguntaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public RespuestaPreguntaBO FirstBy(Expression<Func<TRespuestaPregunta, bool>> filter)
		{
			try
			{
				TRespuestaPregunta entidad = base.FirstBy(filter);
				RespuestaPreguntaBO objetoBO = Mapper.Map<TRespuestaPregunta, RespuestaPreguntaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(RespuestaPreguntaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TRespuestaPregunta entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<RespuestaPreguntaBO> listadoBO)
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

		public bool Update(RespuestaPreguntaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TRespuestaPregunta entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<RespuestaPreguntaBO> listadoBO)
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
		private void AsignacionId(TRespuestaPregunta entidad, RespuestaPreguntaBO objetoBO)
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

		private TRespuestaPregunta MapeoEntidad(RespuestaPreguntaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TRespuestaPregunta entidad = new TRespuestaPregunta();
				entidad = Mapper.Map<RespuestaPreguntaBO, TRespuestaPregunta>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<RespuestaPreguntaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TRespuestaPregunta, bool>>> filters, Expression<Func<TRespuestaPregunta, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TRespuestaPregunta> listado = base.GetFiltered(filters, orderBy, ascending);
			List<RespuestaPreguntaBO> listadoBO = new List<RespuestaPreguntaBO>();

			foreach (var itemEntidad in listado)
			{
				RespuestaPreguntaBO objetoBO = Mapper.Map<TRespuestaPregunta, RespuestaPreguntaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion

		/// <summary>
		/// Obtiene respuestas asociadas a una pregunta
		/// </summary>
		/// <param name="idExamen"></param>
		/// <returns></returns>
		public List<RespuestaPreguntaDTO> ObtenerRespuestaPregunta(int idPregunta)
		{
			try
			{
				List<RespuestaPreguntaDTO> objeto = new List<RespuestaPreguntaDTO>();
				string query = "SELECT Id, IdPregunta, RespuestaCorrecta, NroOrdenRespuesta, NroOrden, EnunciadoRespuesta, Puntaje, FeedbackPositivo, FeedbackNegativo FROM [gp].[V_TRespuestaPregunta_ObtenerRespuestasPregunta] WHERE Estado = 1 AND IdPregunta = @IdPregunta";
				var res = _dapper.QueryDapper(query, new { IdPregunta = idPregunta });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					objeto = JsonConvert.DeserializeObject<List<RespuestaPreguntaDTO>>(res);
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
		/// <returns></returns>
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
	}
}
