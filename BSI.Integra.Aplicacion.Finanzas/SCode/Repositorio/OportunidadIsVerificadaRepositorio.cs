using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
	public class OportunidadIsVerificadaRepositorio : BaseRepository<TOportunidadIsVerificada, OportunidadIsVerificadaBO>
	{
		#region Metodos Base
		public OportunidadIsVerificadaRepositorio() : base()
		{
		}
		public OportunidadIsVerificadaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<OportunidadIsVerificadaBO> GetBy(Expression<Func<TOportunidadIsVerificada, bool>> filter)
		{
			IEnumerable<TOportunidadIsVerificada> listado = base.GetBy(filter);
			List<OportunidadIsVerificadaBO> listadoBO = new List<OportunidadIsVerificadaBO>();
			foreach (var itemEntidad in listado)
			{
				OportunidadIsVerificadaBO objetoBO = Mapper.Map<TOportunidadIsVerificada, OportunidadIsVerificadaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public OportunidadIsVerificadaBO FirstById(int id)
		{
			try
			{
				TOportunidadIsVerificada entidad = base.FirstById(id);
				OportunidadIsVerificadaBO objetoBO = new OportunidadIsVerificadaBO();
				Mapper.Map<TOportunidadIsVerificada, OportunidadIsVerificadaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public OportunidadIsVerificadaBO FirstBy(Expression<Func<TOportunidadIsVerificada, bool>> filter)
		{
			try
			{
				TOportunidadIsVerificada entidad = base.FirstBy(filter);
				OportunidadIsVerificadaBO objetoBO = Mapper.Map<TOportunidadIsVerificada, OportunidadIsVerificadaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(OportunidadIsVerificadaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TOportunidadIsVerificada entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<OportunidadIsVerificadaBO> listadoBO)
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

		public bool Update(OportunidadIsVerificadaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TOportunidadIsVerificada entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<OportunidadIsVerificadaBO> listadoBO)
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
		private void AsignacionId(TOportunidadIsVerificada entidad, OportunidadIsVerificadaBO objetoBO)
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

		private TOportunidadIsVerificada MapeoEntidad(OportunidadIsVerificadaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TOportunidadIsVerificada entidad = new TOportunidadIsVerificada();
				entidad = Mapper.Map<OportunidadIsVerificadaBO, TOportunidadIsVerificada>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<OportunidadIsVerificadaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TOportunidadIsVerificada, bool>>> filters, Expression<Func<TOportunidadIsVerificada, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TOportunidadIsVerificada> listado = base.GetFiltered(filters, orderBy, ascending);
			List<OportunidadIsVerificadaBO> listadoBO = new List<OportunidadIsVerificadaBO>();

			foreach (var itemEntidad in listado)
			{
				OportunidadIsVerificadaBO objetoBO = Mapper.Map<TOportunidadIsVerificada, OportunidadIsVerificadaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion

		/// <summary>
		/// Obtiene todas las oportunidades en fase IS o M con estado de matricula matriculado y sin coordinador academico asignado
		/// </summary>
		/// <returns></returns>
		public List<OportunidadIsVerificadaDTO> ObtenerOportunidadIsVerificadaSinPeriodo()
		{
			try
			{
				var query = "SELECT IdOportunidad, Asesor, Alumno, CentroCosto, CodigoFaseOportunidad, CodigoMatricula, IdMatriculaCabecera, Verificado, UltimaFechaProgramada, FechaCambioIs FROM [fin].[V_ObtenerOportunidadesVerificarISM] WHERE EstadoMatricula = 'matriculado' AND UsuarioCoordinadorAcademico = '0' AND RowNumber = 1";
				var res = _dapper.QueryDapper(query,null);
				return JsonConvert.DeserializeObject<List<OportunidadIsVerificadaDTO>>(res);

			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene todas las oportunidades en fase IS o M con estado de matricula matriculado y sin coordinador academico asignado por FechaInicio y FecheFin seleccionado
		/// </summary>
		/// <param name="fechaInicio"></param>
		/// <param name="fechaFin"></param>
		/// <returns></returns>
		public List<OportunidadIsVerificadaDTO> ObtenerOportunidadIsVerificadaConPeriodo(DateTime fechaInicio, DateTime fechaFin)
		{
			try
			{
				DateTime newFechaFin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day, 23, 59, 59);
				var query = "SELECT IdOportunidad, Asesor, Alumno, CentroCosto, CodigoFaseOportunidad, CodigoMatricula, IdMatriculaCabecera, Verificado, UltimaFechaProgramada, FechaCambioIs FROM [fin].[V_ObtenerOportunidadesVerificarISM] WHERE FechaMatricula >= @FechaInicio AND FechaMatricula <= @FechaFin AND EstadoMatricula = 'matriculado' AND UsuarioCoordinadorAcademico = '0' AND RowNumber = 1";
				var res = _dapper.QueryDapper(query, new { FechaInicio = fechaInicio, FechaFin = newFechaFin });
				return JsonConvert.DeserializeObject<List<OportunidadIsVerificadaDTO>>(res);

			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene oportunidad IS Verificada
		/// </summary>
		/// <param name="idOportunidadIsVerificada"></param>
		/// <returns></returns>
		public List<OportunidadesVerificadasDTO> ObtenerOportunidadesVerificadas()
		{
			try
			{
				var query = "SELECT Coordinador, Alumno, CentroCosto, FaseOportunidad, CodigoMatricula FROM [fin].[V_TOportunidadIsVerificada_ObtenerOportunidadesVerificadas] WHERE Estado = 1";
				var res = _dapper.QueryDapper(query, null);
				return JsonConvert.DeserializeObject<List<OportunidadesVerificadasDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
