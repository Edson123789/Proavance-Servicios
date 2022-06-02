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
	public class PreguntaRepositorio : BaseRepository<TPregunta, PreguntaBO>
	{
		#region Metodos Base
		public PreguntaRepositorio() : base()
		{
		}
		public PreguntaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PreguntaBO> GetBy(Expression<Func<TPregunta, bool>> filter)
		{
			IEnumerable<TPregunta> listado = base.GetBy(filter);
			List<PreguntaBO> listadoBO = new List<PreguntaBO>();
			foreach (var itemEntidad in listado)
			{
				PreguntaBO objetoBO = Mapper.Map<TPregunta, PreguntaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PreguntaBO FirstById(int id)
		{
			try
			{
				TPregunta entidad = base.FirstById(id);
				PreguntaBO objetoBO = new PreguntaBO();
				Mapper.Map<TPregunta, PreguntaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PreguntaBO FirstBy(Expression<Func<TPregunta, bool>> filter)
		{
			try
			{
				TPregunta entidad = base.FirstBy(filter);
				PreguntaBO objetoBO = Mapper.Map<TPregunta, PreguntaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PreguntaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPregunta entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PreguntaBO> listadoBO)
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

		public bool Update(PreguntaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPregunta entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PreguntaBO> listadoBO)
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
		private void AsignacionId(TPregunta entidad, PreguntaBO objetoBO)
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

		private TPregunta MapeoEntidad(PreguntaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPregunta entidad = new TPregunta();
				entidad = Mapper.Map<PreguntaBO, TPregunta>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<PreguntaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TPregunta, bool>>> filters, Expression<Func<TPregunta, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TPregunta> listado = base.GetFiltered(filters, orderBy, ascending);
			List<PreguntaBO> listadoBO = new List<PreguntaBO>();

			foreach (var itemEntidad in listado)
			{
				PreguntaBO objetoBO = Mapper.Map<TPregunta, PreguntaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion

		/// <summary>
		/// Obtiene todas las preguntas registradas en el sistema
		/// </summary>
		/// <returns></returns>
		public List<PreguntaRegistradaDTO> ObtenerPreguntasRegistradas()
		{
			try
			{
				var query = "SELECT Id, Enunciado, IdTipoRespuesta, IdPreguntaTipo, MinutosPorPregunta, RespuestaAleatoria, ActivarFeedBackRespuestaCorrecta, ActivarFeedBackRespuestaIncorrecta, MostrarFeedbackInmediato, MostrarFeedbackPorPregunta, NumeroMaximoIntento, ActivarFeedbackMaximoIntento, MensajeFeedbackIntento, IdExamen, ComponenteExamen, IdTipoRespuestaCalificacion, FactorRespuesta, IdPreguntaCategoria FROM [gp].[V_TPregunta_ObtenerPreguntasRegistradas] WHERE Estado = 1 AND RowNumber = 1";
				var res = _dapper.QueryDapper(query, null);
				return JsonConvert.DeserializeObject<List<PreguntaRegistradaDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
