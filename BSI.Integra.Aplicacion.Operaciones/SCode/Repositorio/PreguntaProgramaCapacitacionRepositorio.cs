using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
	/// Repositorio: Operaciones/PreguntaProgramaCapacitacion
    /// Autor: Luis Huallpa - Gian Miranda
    /// Fecha: 22/02/2021
    /// <summary>
    /// Repositorio para consultas de ope.T_PreguntaProgramaCapacitacion
	/// </summary>
	public class PreguntaProgramaCapacitacionRepositorio : BaseRepository<TPreguntaProgramaCapacitacion, PreguntaProgramaCapacitacionBO>
	{
		#region Metodos Base
		public PreguntaProgramaCapacitacionRepositorio() : base()
		{
		}
		public PreguntaProgramaCapacitacionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PreguntaProgramaCapacitacionBO> GetBy(Expression<Func<TPreguntaProgramaCapacitacion, bool>> filter)
		{
			IEnumerable<TPreguntaProgramaCapacitacion> listado = base.GetBy(filter);
			List<PreguntaProgramaCapacitacionBO> listadoBO = new List<PreguntaProgramaCapacitacionBO>();
			foreach (var itemEntidad in listado)
			{
				PreguntaProgramaCapacitacionBO objetoBO = Mapper.Map<TPreguntaProgramaCapacitacion, PreguntaProgramaCapacitacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PreguntaProgramaCapacitacionBO FirstById(int id)
		{
			try
			{
				TPreguntaProgramaCapacitacion entidad = base.FirstById(id);
				PreguntaProgramaCapacitacionBO objetoBO = new PreguntaProgramaCapacitacionBO();
				Mapper.Map<TPreguntaProgramaCapacitacion, PreguntaProgramaCapacitacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PreguntaProgramaCapacitacionBO FirstBy(Expression<Func<TPreguntaProgramaCapacitacion, bool>> filter)
		{
			try
			{
				TPreguntaProgramaCapacitacion entidad = base.FirstBy(filter);
				PreguntaProgramaCapacitacionBO objetoBO = Mapper.Map<TPreguntaProgramaCapacitacion, PreguntaProgramaCapacitacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PreguntaProgramaCapacitacionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPreguntaProgramaCapacitacion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PreguntaProgramaCapacitacionBO> listadoBO)
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

		public bool Update(PreguntaProgramaCapacitacionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPreguntaProgramaCapacitacion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PreguntaProgramaCapacitacionBO> listadoBO)
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
		private void AsignacionId(TPreguntaProgramaCapacitacion entidad, PreguntaProgramaCapacitacionBO objetoBO)
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

		private TPreguntaProgramaCapacitacion MapeoEntidad(PreguntaProgramaCapacitacionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPreguntaProgramaCapacitacion entidad = new TPreguntaProgramaCapacitacion();
				entidad = Mapper.Map<PreguntaProgramaCapacitacionBO, TPreguntaProgramaCapacitacion>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<PreguntaProgramaCapacitacionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TPreguntaProgramaCapacitacion, bool>>> filters, Expression<Func<TPreguntaProgramaCapacitacion, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TPreguntaProgramaCapacitacion> listado = base.GetFiltered(filters, orderBy, ascending);
			List<PreguntaProgramaCapacitacionBO> listadoBO = new List<PreguntaProgramaCapacitacionBO>();

			foreach (var itemEntidad in listado)
			{
				PreguntaProgramaCapacitacionBO objetoBO = Mapper.Map<TPreguntaProgramaCapacitacion, PreguntaProgramaCapacitacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion

		/// <summary>
		/// Obtiene todas las preguntas de programa de capacitacion registradas en el sistema
		/// </summary>
		/// <returns>Lista de objetos de tipo PreguntaProgramaCapacitacionRegistradaDTO</returns>
		public List<PreguntaProgramaCapacitacionRegistradaDTO> ObtenerPreguntasRegistradas()
		{
			try
			{
				var query = "SELECT Id, Enunciado, IdTipoRespuesta, IdPreguntaTipo, MinutosPorPregunta, RespuestaAleatoria, ActivarFeedBackRespuestaCorrecta, ActivarFeedBackRespuestaIncorrecta, MostrarFeedbackInmediato, MostrarFeedbackPorPregunta, NumeroMaximoIntento, ActivarFeedbackMaximoIntento, MensajeFeedbackIntento, IdPGeneral, IdPEspecifico, PGeneral, IdTipoRespuestaCalificacion, FactorRespuesta, IdCapitulo, IdSesion, GrupoPregunta, IdTipoMarcador, ValorMarcador, OrdenPreguntaGrupo, IdPreguntaIntento FROM [ope].[V_TPreguntaProgramaCapacitacion_ObtenerPreguntasRegistradas] WHERE Estado = 1 AND RowNumber = 1";
				var res = _dapper.QueryDapper(query, null);
				return JsonConvert.DeserializeObject<List<PreguntaProgramaCapacitacionRegistradaDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
