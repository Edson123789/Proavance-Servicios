using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
	/// Repositorio: Configuracion de Coodinadoras/Operaciones
	/// Autor: Luis Huallpa - Jose Villena
	/// Fecha: 28/05/2021
	/// <summary>
	/// Repositorio para consultas de ope.T_ConfiguracionAsignacionCoordinadorOportunidadOperaciones
	/// </summary>
	public class ConfiguracionAsignacionCoordinadorOportunidadOperacionesRepositorio : BaseRepository<TConfiguracionAsignacionCoordinadorOportunidadOperaciones, ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO>
	{
		#region Metodos Base
		public ConfiguracionAsignacionCoordinadorOportunidadOperacionesRepositorio() : base()
		{
		}
		public ConfiguracionAsignacionCoordinadorOportunidadOperacionesRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO> GetBy(Expression<Func<TConfiguracionAsignacionCoordinadorOportunidadOperaciones, bool>> filter)
		{
			IEnumerable<TConfiguracionAsignacionCoordinadorOportunidadOperaciones> listado = base.GetBy(filter);
			List<ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO> listadoBO = new List<ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO>();
			foreach (var itemEntidad in listado)
			{
				ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO objetoBO = Mapper.Map<TConfiguracionAsignacionCoordinadorOportunidadOperaciones, ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO FirstById(int id)
		{
			try
			{
				TConfiguracionAsignacionCoordinadorOportunidadOperaciones entidad = base.FirstById(id);
				ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO objetoBO = new ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO();
				Mapper.Map<TConfiguracionAsignacionCoordinadorOportunidadOperaciones, ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO FirstBy(Expression<Func<TConfiguracionAsignacionCoordinadorOportunidadOperaciones, bool>> filter)
		{
			try
			{
				TConfiguracionAsignacionCoordinadorOportunidadOperaciones entidad = base.FirstBy(filter);
				ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO objetoBO = Mapper.Map<TConfiguracionAsignacionCoordinadorOportunidadOperaciones, ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TConfiguracionAsignacionCoordinadorOportunidadOperaciones entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO> listadoBO)
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

		public bool Update(ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TConfiguracionAsignacionCoordinadorOportunidadOperaciones entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO> listadoBO)
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
		private void AsignacionId(TConfiguracionAsignacionCoordinadorOportunidadOperaciones entidad, ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO objetoBO)
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

		private TConfiguracionAsignacionCoordinadorOportunidadOperaciones MapeoEntidad(ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TConfiguracionAsignacionCoordinadorOportunidadOperaciones entidad = new TConfiguracionAsignacionCoordinadorOportunidadOperaciones();
				entidad = Mapper.Map<ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO, TConfiguracionAsignacionCoordinadorOportunidadOperaciones>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TConfiguracionAsignacionCoordinadorOportunidadOperaciones, bool>>> filters, Expression<Func<TConfiguracionAsignacionCoordinadorOportunidadOperaciones, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TConfiguracionAsignacionCoordinadorOportunidadOperaciones> listado = base.GetFiltered(filters, orderBy, ascending);
			List<ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO> listadoBO = new List<ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO>();

			foreach (var itemEntidad in listado)
			{
				ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO objetoBO = Mapper.Map<TConfiguracionAsignacionCoordinadorOportunidadOperaciones, ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion

		/// Autor: Jose Villena
		/// Fecha: 28/05/2021
		/// Version: 1.0
		/// <summary>
		/// Obtener Configuracion Coordinadores
		/// </summary>
		/// <param></param>
		/// <returns>List<ConfiguracionCentroCostoCoordinadorDTO></returns>
		public List<ConfiguracionCentroCostoCoordinadorDTO> ObtenerConfiguracionCoordinadores()
		{
			try
			{
				var query = "SELECT Id, IdPersonal, Personal,IdEstadoMatricula,EstadoMatricula,IdSubEstadoMatricula,SubEstadoMatricula, IdCentroCosto, IdProgramaEspecifico, CentroCosto, ProgramaEspecifico, EstadoProgramaEspecifico, Tipo, FechaCreacion, EsAsignado FROM [pla].[V_TConfiguracionAsignacionCoordinadorOportunidadOperaciones_ObtenerConfiguracion] WHERE EsAsignado = 1 ORDER BY FechaCreacion DESC";
				var res = _dapper.QueryDapper(query, null);
				return JsonConvert.DeserializeObject<List<ConfiguracionCentroCostoCoordinadorDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		
		/// Autor: Jose Villena
		/// Fecha: 28/05/2021
		/// Version: 1.0
		/// <summary>
		/// Obtiene todos los centros de costo sin asignacion de coordinadora
		/// </summary>
		/// <param></param>
		/// <returns>List<ConfiguracionCentroCostoCoordinadorDTO></returns>
		public List<ConfiguracionCentroCostoCoordinadorDTO> ObtenerCentroCostoSigAsignacion()
		{
			try
			{
				var query = "SELECT Id, IdPersonal, IdCentroCosto, IdProgramaEspecifico, CentroCosto, ProgramaEspecifico, EstadoProgramaEspecifico, Tipo, FechaCreacion, EsAsignado FROM [pla].[V_TConfiguracionAsignacionCoordinadorOportunidadOperaciones_ObtenerConfiguracion] WHERE EsAsignado = 0 AND  CentroCosto NOT LIKE '%WEBINAR%' ORDER BY FechaCreacion DESC";
				var res = _dapper.QueryDapper(query, null);
				return JsonConvert.DeserializeObject<List<ConfiguracionCentroCostoCoordinadorDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// Autor: Jose Villena
		/// Fecha: 28/05/2021
		/// Version: 1.0
		/// <summary>
		/// Obtiene Centro de Costos Hijo
		/// </summary>
		/// <param name="idCentroCosto">Id de Centro Costo</param>
		/// <returnsList<CentroCostoHijoDTO></returns>
		public List<CentroCostoHijoDTO> ObtenerCentroCostoHijos(int idCentroCosto)
		{
			try
			{
				List<CentroCostoHijoDTO> centroCostoHijo = new List<CentroCostoHijoDTO>();
				var query = "SELECT IdCentroCosto, PEspecificoPadreId, IdCentroCostoHijo, PEspecificoHijoId	FROM [pla].[V_TCentroCosto_ObtenerCentroCostoHijo] WHERE Estado = 1 AND IdCentroCosto = @IdCentroCosto";
				var res = _dapper.QueryDapper(query, new { IdCentroCosto = idCentroCosto });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					centroCostoHijo = JsonConvert.DeserializeObject<List<CentroCostoHijoDTO>>(res);
				}
				return centroCostoHijo;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// Autor: Jose Villena
		/// Fecha: 28/05/2021
		/// Version: 1.0
		/// <summary>
		/// Obtiene configuracion por PEspecifico
		/// </summary>
		/// <param name="idPEspecifico">Id del Programa Especifico</param>
		/// <returnsList<ConfiguracionCoordinadoraCentroCostoDTO></returns>
		public List<ConfiguracionCoordinadoraCentroCostoDTO> ObtenerConfiguracionPorPespecifico(int idPespecifico)
		{
			try
			{
				List<ConfiguracionCoordinadoraCentroCostoDTO> listaUsuarioConfiguracion = new List<ConfiguracionCoordinadoraCentroCostoDTO>();
				var query = "SELECT DISTINCT IdPersonal, UsuarioPersonal,IdEstadoMatricula,IdSubEstadoMatricula FROM [ope].[V_TConfiguracionAsignacionCoordinadorOportunidadOperaciones_ObtenerConfiguracionPorCentroCosto] WHERE Estado = 1 AND IdPEspecifico = @IdPEspecifico";
				var res = _dapper.QueryDapper(query, new { IdPespecifico = idPespecifico});
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					listaUsuarioConfiguracion = JsonConvert.DeserializeObject<List<ConfiguracionCoordinadoraCentroCostoDTO>>(res);
				}
				else
				{
					query = "SELECT DISTINCT IdPersonal, UsuarioPersonal,IdEstadoMatricula,IdSubEstadoMatricula FROM [ope].[V_TConfiguracionAsignacionCoordinadorOportunidadOperaciones_ObtenerConfiguracionPorCentroCosto] WHERE Estado = 1 AND IdPEspecificoHijo = @IdPEspecifico";
					res = _dapper.QueryDapper(query, new { IdPespecifico = idPespecifico });
					if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
					{
						listaUsuarioConfiguracion = JsonConvert.DeserializeObject<List<ConfiguracionCoordinadoraCentroCostoDTO>>(res);
					}
				}
				return listaUsuarioConfiguracion;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// Autor: Carlos Crispin
		/// Fecha: 24/09/2021
		/// Version: 1.0
		/// <summary>
		/// Obtiene los idsubestado de los estados para seguimiento academico,al dia, atrasado
		/// </summary>
		/// <param name="idmatriculaCabecera">Id de la Matricula</param>
		/// <returnsList<ConfiguracionCoordinadoraSubEstadoMatricula></returns>
		public List<ConfiguracionCoordinadoraSubEstadoMatricula> ObtenerSubEstadoPorIdMatricula(int idmatriculaCabecera)
		{
			try
			{
				List<ConfiguracionCoordinadoraSubEstadoMatricula> subestado = new List<ConfiguracionCoordinadoraSubEstadoMatricula>();
				var query = "SELECT IdSubEstado, IdMatriculaCabecera FROM [ope].[V_MatriculaSubEstado]  WHERE  IdMatriculaCabecera = @IdMatriculaCabecera";
				var res = _dapper.QueryDapper(query, new { IdMatriculaCabecera = idmatriculaCabecera });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					subestado = JsonConvert.DeserializeObject<List<ConfiguracionCoordinadoraSubEstadoMatricula>>(res);
				}
				return subestado;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
