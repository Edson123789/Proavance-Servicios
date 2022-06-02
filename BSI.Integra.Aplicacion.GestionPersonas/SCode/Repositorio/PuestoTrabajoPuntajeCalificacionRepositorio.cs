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
	///PuestoTrabajoPuntajeCalificacionRepositorio
	/// Autor: Luis H., Edgar S.
	/// Fecha: 29/01/2021
	/// <summary>
	/// Gestión de Puntaje por Perfiles de Puestos de Trabajo
	/// </summary>
	public class PuestoTrabajoPuntajeCalificacionRepositorio : BaseRepository<TPuestoTrabajoPuntajeCalificacion, PuestoTrabajoPuntajeCalificacionBO>
	{
		#region Metodos Base
		public PuestoTrabajoPuntajeCalificacionRepositorio() : base()
		{
		}
		public PuestoTrabajoPuntajeCalificacionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		//public IEnumerable<PuestoTrabajoPuntajeCalificacionBO> GetBy(Expression<Func<TPuestoTrabajoPuntajeCalificacion, bool>> filter)
		//{
		//    IEnumerable<TPuestoTrabajoPuntajeCalificacion> listado = base.GetBy(filter);
		//    List<PuestoTrabajoPuntajeCalificacionBO> listadoBO = new List<PuestoTrabajoPuntajeCalificacionBO>();
		//    foreach (var itemEntidad in listado)
		//    {
		//        PuestoTrabajoPuntajeCalificacionBO objetoBO = Mapper.Map<TPuestoTrabajoPuntajeCalificacion, PuestoTrabajoPuntajeCalificacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
		//        listadoBO.Add(objetoBO);
		//    }

		//    return listadoBO;
		//}
		public PuestoTrabajoPuntajeCalificacionBO FirstById(int id)
		{
			try
			{
				TPuestoTrabajoPuntajeCalificacion entidad = base.FirstById(id);
				PuestoTrabajoPuntajeCalificacionBO objetoBO = new PuestoTrabajoPuntajeCalificacionBO();
				Mapper.Map<TPuestoTrabajoPuntajeCalificacion, PuestoTrabajoPuntajeCalificacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PuestoTrabajoPuntajeCalificacionBO FirstBy(Expression<Func<TPuestoTrabajoPuntajeCalificacion, bool>> filter)
		{
			try
			{
				TPuestoTrabajoPuntajeCalificacion entidad = base.FirstBy(filter);
				PuestoTrabajoPuntajeCalificacionBO objetoBO = Mapper.Map<TPuestoTrabajoPuntajeCalificacion, PuestoTrabajoPuntajeCalificacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PuestoTrabajoPuntajeCalificacionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPuestoTrabajoPuntajeCalificacion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PuestoTrabajoPuntajeCalificacionBO> listadoBO)
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

		public bool Update(PuestoTrabajoPuntajeCalificacionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPuestoTrabajoPuntajeCalificacion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PuestoTrabajoPuntajeCalificacionBO> listadoBO)
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
		private void AsignacionId(TPuestoTrabajoPuntajeCalificacion entidad, PuestoTrabajoPuntajeCalificacionBO objetoBO)
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

		private TPuestoTrabajoPuntajeCalificacion MapeoEntidad(PuestoTrabajoPuntajeCalificacionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPuestoTrabajoPuntajeCalificacion entidad = new TPuestoTrabajoPuntajeCalificacion();
				entidad = Mapper.Map<PuestoTrabajoPuntajeCalificacionBO, TPuestoTrabajoPuntajeCalificacion>(objetoBO,
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

		///PuestoTrabajoPuntajeCalificacionRepositorio
		/// Autor: Luis H., Edgar S.
		/// Fecha: 29/01/2021
		/// <summary>
		/// Obtiene lista de evaluaciones segun esten configuradas por grupos de componentes o componentes
		/// </summary>
		/// <returns> Lista de evaluaciones segun esten configuradas por grupos de componentes o componentes </returns>
		/// <returns> Lista de objeto DTO :  List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO> </returns>
		public List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO> ObtenerNombreEvaluacionPuntaje()
		{
			try
			{
				List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO> EvaluacionGrupo = new List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO>();
				var query = "SELECT CalificacionTotal, IdEvaluacion, NombreEvaluacion, IdGrupo, NombreGrupo, IdComponente, NombreComponente FROM [gp].[V_ObtenerEvaluacionesProcesoSeleccion] WHERE Estado = 1";
				var res = _dapper.QueryDapper(query, null);
				if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
				{
					EvaluacionGrupo = JsonConvert.DeserializeObject<List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO>>(res);
				}
				return EvaluacionGrupo;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
