using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using MailChimp.Net.Core.Responses;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
	public class PuestoTrabajoRemuneracionDetalleRepositorio : BaseRepository<TPuestoTrabajoRemuneracionDetalle, PuestoTrabajoRemuneracionDetalleBO>
	{
		#region Metodos Base
		public PuestoTrabajoRemuneracionDetalleRepositorio() : base()
		{
		}
		public PuestoTrabajoRemuneracionDetalleRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PuestoTrabajoRemuneracionDetalleBO> GetBy(Expression<Func<TPuestoTrabajoRemuneracionDetalle, bool>> filter)
		{
			IEnumerable<TPuestoTrabajoRemuneracionDetalle> listado = base.GetBy(filter);
			List<PuestoTrabajoRemuneracionDetalleBO> listadoBO = new List<PuestoTrabajoRemuneracionDetalleBO>();
			foreach (var itemEntidad in listado)
			{
				PuestoTrabajoRemuneracionDetalleBO objetoBO = Mapper.Map<TPuestoTrabajoRemuneracionDetalle, PuestoTrabajoRemuneracionDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PuestoTrabajoRemuneracionDetalleBO FirstById(int id)
		{
			try
			{
				TPuestoTrabajoRemuneracionDetalle entidad = base.FirstById(id);
				PuestoTrabajoRemuneracionDetalleBO objetoBO = new PuestoTrabajoRemuneracionDetalleBO();
				Mapper.Map<TPuestoTrabajoRemuneracionDetalle, PuestoTrabajoRemuneracionDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PuestoTrabajoRemuneracionDetalleBO FirstBy(Expression<Func<TPuestoTrabajoRemuneracionDetalle, bool>> filter)
		{
			try
			{
				TPuestoTrabajoRemuneracionDetalle entidad = base.FirstBy(filter);
				PuestoTrabajoRemuneracionDetalleBO objetoBO = Mapper.Map<TPuestoTrabajoRemuneracionDetalle, PuestoTrabajoRemuneracionDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PuestoTrabajoRemuneracionDetalleBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPuestoTrabajoRemuneracionDetalle entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PuestoTrabajoRemuneracionDetalleBO> listadoBO)
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

		public bool Update(PuestoTrabajoRemuneracionDetalleBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPuestoTrabajoRemuneracionDetalle entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PuestoTrabajoRemuneracionDetalleBO> listadoBO)
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
		private void AsignacionId(TPuestoTrabajoRemuneracionDetalle entidad, PuestoTrabajoRemuneracionDetalleBO objetoBO)
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

		private TPuestoTrabajoRemuneracionDetalle MapeoEntidad(PuestoTrabajoRemuneracionDetalleBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPuestoTrabajoRemuneracionDetalle entidad = new TPuestoTrabajoRemuneracionDetalle();
				entidad = Mapper.Map<PuestoTrabajoRemuneracionDetalleBO, TPuestoTrabajoRemuneracionDetalle>(objetoBO,
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


		/// <summary>
		/// Obtiene la lista de todos los elementos en la tabla PuestoTrabajoRemuneracionVariable
		/// </summary>
		/// <returns></returns>
		public List<PuestoTrabajoRemuneracionDetalleDTO> ObtenerPuestoTrabajoRemuneracionVariableRegistrado(int IdPuestoTrabajoRemuneracion)
		{
			try
			{
				List <PuestoTrabajoRemuneracionDetalleDTO> listaRemuneracionVariable = new List<PuestoTrabajoRemuneracionDetalleDTO>();
				var query = "SELECT * FROM [gp].[V_TPuestoTrabajoRemuneracionVariable_ObtenerRegistro] WHERE Estado = 1 AND IdPuestoTrabajoRemuneracion = @IdPuestoTrabajoRemuneracion";
				var res = _dapper.QueryDapper(query, new { IdPuestoTrabajoRemuneracion });

				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					listaRemuneracionVariable = JsonConvert.DeserializeObject<List<PuestoTrabajoRemuneracionDetalleDTO>>(res);
				}
				return listaRemuneracionVariable;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene la lista de todos los elementos en la tabla PuestoTrabajoRemuneracionVariable
		/// </summary>
		/// <returns></returns>
		public List<FiltroBasicoDTO> ObtenerRemuneracion()
		{
			try
			{
				List<FiltroBasicoDTO> listaRemuneracion = new List<FiltroBasicoDTO>();
				var query = "SELECT Id, Nombre FROM gp.T_RemuneracionTipo WHERE Estado = 1 ";
				var res = _dapper.QueryDapper(query, new {});

				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					listaRemuneracion = JsonConvert.DeserializeObject<List<FiltroBasicoDTO>>(res);
				}
				return listaRemuneracion;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene la lista de todos los elementos en la tabla PuestoTrabajoRemuneracionVariable
		/// </summary>
		/// <returns></returns>
		public List<FiltroBasicoDTO> ObtenerTipoRemuneracion()
		{
			try
			{
				List<FiltroBasicoDTO> listaTipoRemuneracion = new List<FiltroBasicoDTO>();
				var query = "SELECT Id, Nombre FROM gp.T_RemuneracionTipoCobro WHERE Estado = 1 ";
				var res = _dapper.QueryDapper(query, new { });

				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					listaTipoRemuneracion = JsonConvert.DeserializeObject<List<FiltroBasicoDTO>>(res);
				}
				return listaTipoRemuneracion;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene la lista de todos los elementos en la tabla PuestoTrabajoRemuneracionVariable
		/// </summary>
		/// <returns></returns>
		public List<FiltroBasicoDTO> ObtenerClaseRemuneracion()
		{
			try
			{
				List<FiltroBasicoDTO> listaClaseRemuneracion = new List<FiltroBasicoDTO>();
				var query = "SELECT Id, Nombre FROM gp.T_RemuneracionFormaCobro WHERE Estado = 1 ";
				var res = _dapper.QueryDapper(query, new { });

				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					listaClaseRemuneracion = JsonConvert.DeserializeObject<List<FiltroBasicoDTO>>(res);
				}
				return listaClaseRemuneracion;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene la lista de todos los elementos en la tabla PuestoTrabajoRemuneracionVariable
		/// </summary>
		/// <returns></returns>
		public List<FiltroBasicoDTO> ObtenerPeriodoRemuneracion()
		{
			try
			{
				List<FiltroBasicoDTO> listaPeriodoRemuneracion = new List<FiltroBasicoDTO>();
				var query = "SELECT Id, Nombre FROM gp.T_RemuneracionPeriodoCobro WHERE Estado = 1 ";
				var res = _dapper.QueryDapper(query, new { });

				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					listaPeriodoRemuneracion = JsonConvert.DeserializeObject<List<FiltroBasicoDTO>>(res);
				}
				return listaPeriodoRemuneracion;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene la lista de todos los elementos en la tabla PuestoTrabajoRemuneracionVariable
		/// </summary>
		/// <returns></returns>
		public List<FiltroBasicoDTO> ObtenerMonedaParaTableroComercial()
		{
			try
			{
				List<FiltroBasicoDTO> listaMoneda = new List<FiltroBasicoDTO>();
				var query = "SELECT Id, Codigo AS Nombre FROM pla.V_TMoneda_FiltroCodigoMoneda ";
				var res = _dapper.QueryDapper(query, new { });

				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					listaMoneda = JsonConvert.DeserializeObject<List<FiltroBasicoDTO>>(res);
				}
				return listaMoneda;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene la lista de todos los elementos en la tabla PuestoTrabajoRemuneracionVariable
		/// </summary>
		/// <returns></returns>
		public List<FiltroBasicoDTO> ObtenerDescripcionMonetaria()
		{
			try
			{
				List<FiltroBasicoDTO> listaDescripcionMonetaria = new List<FiltroBasicoDTO>();
				var query = "SELECT Id, Nombre FROM gp.T_RemuneracionDescripcionMonetaria WHERE Estado=1 ";
				var res = _dapper.QueryDapper(query, new { });

				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					listaDescripcionMonetaria = JsonConvert.DeserializeObject<List<FiltroBasicoDTO>>(res);
				}
				return listaDescripcionMonetaria;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
	
}
