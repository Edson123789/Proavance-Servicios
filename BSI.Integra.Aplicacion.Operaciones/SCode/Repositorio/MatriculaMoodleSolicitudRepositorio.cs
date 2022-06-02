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
	/// Repositorio: MatriculaMoodleSolicitud
	/// Autor: Jose Villena
	/// Fecha: 01/05/2021
	/// <summary>
	/// Repositorio para consultas de ope.T_MatriculaMoodleSolicitud
	/// </summary>
	public class MatriculaMoodleSolicitudRepositorio : BaseRepository<TMatriculaMoodleSolicitud, MatriculaMoodleSolicitudBO>
	{
		#region Metodos Base
		public MatriculaMoodleSolicitudRepositorio() : base()
		{
		}
		public MatriculaMoodleSolicitudRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<MatriculaMoodleSolicitudBO> GetBy(Expression<Func<TMatriculaMoodleSolicitud, bool>> filter)
		{
			IEnumerable<TMatriculaMoodleSolicitud> listado = base.GetBy(filter);
			List<MatriculaMoodleSolicitudBO> listadoBO = new List<MatriculaMoodleSolicitudBO>();
			foreach (var itemEntidad in listado)
			{
				MatriculaMoodleSolicitudBO objetoBO = Mapper.Map<TMatriculaMoodleSolicitud, MatriculaMoodleSolicitudBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public MatriculaMoodleSolicitudBO FirstById(int id)
		{
			try
			{
				TMatriculaMoodleSolicitud entidad = base.FirstById(id);
				MatriculaMoodleSolicitudBO objetoBO = new MatriculaMoodleSolicitudBO();
				Mapper.Map<TMatriculaMoodleSolicitud, MatriculaMoodleSolicitudBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public MatriculaMoodleSolicitudBO FirstBy(Expression<Func<TMatriculaMoodleSolicitud, bool>> filter)
		{
			try
			{
				TMatriculaMoodleSolicitud entidad = base.FirstBy(filter);
				MatriculaMoodleSolicitudBO objetoBO = Mapper.Map<TMatriculaMoodleSolicitud, MatriculaMoodleSolicitudBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(MatriculaMoodleSolicitudBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TMatriculaMoodleSolicitud entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<MatriculaMoodleSolicitudBO> listadoBO)
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

		public bool Update(MatriculaMoodleSolicitudBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TMatriculaMoodleSolicitud entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<MatriculaMoodleSolicitudBO> listadoBO)
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
		private void AsignacionId(TMatriculaMoodleSolicitud entidad, MatriculaMoodleSolicitudBO objetoBO)
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

		private TMatriculaMoodleSolicitud MapeoEntidad(MatriculaMoodleSolicitudBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TMatriculaMoodleSolicitud entidad = new TMatriculaMoodleSolicitud();
				entidad = Mapper.Map<MatriculaMoodleSolicitudBO, TMatriculaMoodleSolicitud>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<MatriculaMoodleSolicitudBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TMatriculaMoodleSolicitud, bool>>> filters, Expression<Func<TMatriculaMoodleSolicitud, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TMatriculaMoodleSolicitud> listado = base.GetFiltered(filters, orderBy, ascending);
			List<MatriculaMoodleSolicitudBO> listadoBO = new List<MatriculaMoodleSolicitudBO>();

			foreach (var itemEntidad in listado)
			{
				MatriculaMoodleSolicitudBO objetoBO = Mapper.Map<TMatriculaMoodleSolicitud, MatriculaMoodleSolicitudBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion


		///Repositorio: MatriculaMoodleSolicitudRepositorio
		///Autor: Jose Villena.
		///Fecha: 03/05/2021
		/// <summary>
		/// Obtiene la ultima solicitud de matricula realizadas por las coordinadoras de operaciones mediante el idoportunidad
		/// </summary>
		/// <param name="idOportunidad"> Id de Oportunidad </param>
		/// <returns> Lista ultima Solicitud de matrícula moodle: List<MatriculaMoodleSolicitudDTO> </returns>
		public List<MatriculaMoodleSolicitudDTO> ObtenerSolicitudesMatriculaMoodlePorIdOportunidad(int idOportunidad)
		{
			try
			{
				List<MatriculaMoodleSolicitudDTO> solicitudMatriculaMoodle = new List<MatriculaMoodleSolicitudDTO>();
				var query = "SELECT Id, IdOportunidad, IdCursoMoodle, IdUsuarioMoodle, CodigoMatricula, FechaInicioMatricula,FechaFinMatricula, IdMatriculaMoodleSolicitudEstado, MatriculaMoodleSolicitudEstado, UsuarioSolicitud, FechaSolicitud, UsuarioAprobacion, FechaAprobacion, Habilitado FROM [ope].[V_TMatriculaMoodleSolicitud_ObtenerSolicitudMatriculas] WHERE Estado = 1 AND IdOportunidad = @IdOportunidad ORDER BY FechaCreacion DESC";
				var res = _dapper.QueryDapper(query, new { IdOportunidad = idOportunidad });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					solicitudMatriculaMoodle = JsonConvert.DeserializeObject<List<MatriculaMoodleSolicitudDTO>>(res);
				}
				return solicitudMatriculaMoodle;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}


		///Repositorio: MatriculaMoodleSolicitudRepositorio
		///Autor: Jose Villena
		///Fecha: 03/05/2021
		/// <summary>
		/// Obtiene las solicitudes de matricula realizadas por las coordinadoras de operaciones mediante el idoportunidad y idcursomoodle
		/// </summary>
		/// <param name="idOportunidad"> Id Oportunidad </param>
		/// <param name="idCursoMoodle"> Id Curso Moodle </param>
		/// <returns> Lista Solicitudes de matrícula moodle: List<MatriculaMoodleSolicitudDTO> </returns>
		public List<MatriculaMoodleSolicitudDTO> ObtenerSolicitudesMatriculaMoodlePorIdOportunidadIdCursoMoodle(int idOportunidad, int idCursoMoodle)
		{
			try
			{
				List<MatriculaMoodleSolicitudDTO> solicitudMatriculaMoodle = new List<MatriculaMoodleSolicitudDTO>();
				var query = "SELECT Id, IdOportunidad, IdCursoMoodle, IdUsuarioMoodle, CodigoMatricula, FechaInicioMatricula,FechaFinMatricula, IdMatriculaMoodleSolicitudEstado, MatriculaMoodleSolicitudEstado, UsuarioSolicitud, FechaSolicitud, UsuarioAprobacion, FechaAprobacion, NombreCursoMoodle, Habilitado FROM [ope].[V_TMatriculaMoodleSolicitud_ObtenerSolicitudMatriculas] WHERE Estado = 1 AND IdOportunidad = @IdOportunidad AND IdCursoMoodle = @IdCursoMoodle ORDER BY FechaCreacion DESC";
				var res = _dapper.QueryDapper(query, new { IdOportunidad = idOportunidad, IdCursoMoodle = idCursoMoodle });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					solicitudMatriculaMoodle = JsonConvert.DeserializeObject<List<MatriculaMoodleSolicitudDTO>>(res);
				}
				return solicitudMatriculaMoodle;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
