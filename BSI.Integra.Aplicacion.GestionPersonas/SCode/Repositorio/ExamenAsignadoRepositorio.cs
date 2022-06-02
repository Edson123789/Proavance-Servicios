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
	/// Repositorio: ExamenAsignadoRepositorio
	/// Autor: Britsel C., Luis H., Edgar S.
	/// Fecha: 29/01/2021
	/// <summary>
	/// Gestión de Examenes asignados por Proceso de Seleccion
	/// </summary>
	public class ExamenAsignadoRepositorio : BaseRepository<TExamenAsignado, ExamenAsignadoBO>
	{
		#region Metodos Base
		public ExamenAsignadoRepositorio() : base()
		{
		}
		public ExamenAsignadoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ExamenAsignadoBO> GetBy(Expression<Func<TExamenAsignado, bool>> filter)
		{
			IEnumerable<TExamenAsignado> listado = base.GetBy(filter);
			List<ExamenAsignadoBO> listadoBO = new List<ExamenAsignadoBO>();
			foreach (var itemEntidad in listado)
			{
				ExamenAsignadoBO objetoBO = Mapper.Map<TExamenAsignado, ExamenAsignadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ExamenAsignadoBO FirstById(int id)
		{
			try
			{
				TExamenAsignado entidad = base.FirstById(id);
				ExamenAsignadoBO objetoBO = new ExamenAsignadoBO();
				Mapper.Map<TExamenAsignado, ExamenAsignadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ExamenAsignadoBO FirstBy(Expression<Func<TExamenAsignado, bool>> filter)
		{
			try
			{
				TExamenAsignado entidad = base.FirstBy(filter);
				ExamenAsignadoBO objetoBO = Mapper.Map<TExamenAsignado, ExamenAsignadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ExamenAsignadoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TExamenAsignado entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ExamenAsignadoBO> listadoBO)
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

		public bool Update(ExamenAsignadoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TExamenAsignado entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ExamenAsignadoBO> listadoBO)
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
		private void AsignacionId(TExamenAsignado entidad, ExamenAsignadoBO objetoBO)
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

		private TExamenAsignado MapeoEntidad(ExamenAsignadoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TExamenAsignado entidad = new TExamenAsignado();
				entidad = Mapper.Map<ExamenAsignadoBO, TExamenAsignado>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<ExamenAsignadoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TExamenAsignado, bool>>> filters, Expression<Func<TExamenAsignado, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TExamenAsignado> listado = base.GetFiltered(filters, orderBy, ascending);
			List<ExamenAsignadoBO> listadoBO = new List<ExamenAsignadoBO>();

			foreach (var itemEntidad in listado)
			{
				ExamenAsignadoBO objetoBO = Mapper.Map<TExamenAsignado, ExamenAsignadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion

		public List<ConfiguracionAsignacionExamenDTO> ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccion(int idProcesoSeleccion)
		{
			try
			{
				var query = "SELECT Id, IdProcesoSeleccion, IdExamen, NroOrden FROM [gp].[V_TConfiguracionAsignacionExamen] WHERE IdProcesoSeleccion = @IdProcesoSeleccion AND Estado = 1";
				var res = _dapper.QueryDapper(query, new { IdProcesoSeleccion = idProcesoSeleccion });
				return JsonConvert.DeserializeObject<List<ConfiguracionAsignacionExamenDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public List<ConfiguracionAsignacionExamenV2DTO> ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccionV2(int idProcesoSeleccion)
		{
			try
			{
				var query = "SELECT Id, IdProcesoSeleccion, IdEvaluacion, IdExamen, NroOrden FROM [gp].[V_TConfiguracionAsignacionExamenV2] WHERE IdProcesoSeleccion = @IdProcesoSeleccion AND Estado = 1 AND EsCalificadoPorPostulante = 1";
				var res = _dapper.QueryDapper(query, new { IdProcesoSeleccion = idProcesoSeleccion });
				return JsonConvert.DeserializeObject<List<ConfiguracionAsignacionExamenV2DTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
