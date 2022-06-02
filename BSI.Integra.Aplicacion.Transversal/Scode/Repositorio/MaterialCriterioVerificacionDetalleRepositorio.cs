using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;
using BSI.Integra.Aplicacion.Transversal.Helper;
using System.IO;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
	/// Repositorio: Transversal/MaterialCriterioVerificacionDetalle
	/// Autor: Luis Huallpa - Gian Miranda
	/// Fecha: 12/07/2021
	/// <summary>
	/// Repositorio para consultas de ope.T_MaterialCriterioVerificacionDetalle
	/// </summary>
	public class MaterialCriterioVerificacionDetalleRepositorio : BaseRepository<TMaterialCriterioVerificacionDetalle, MaterialCriterioVerificacionDetalleBO>
	{
		#region Metodos Base
		public MaterialCriterioVerificacionDetalleRepositorio() : base()
		{
		}
		public MaterialCriterioVerificacionDetalleRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<MaterialCriterioVerificacionDetalleBO> GetBy(Expression<Func<TMaterialCriterioVerificacionDetalle, bool>> filter)
		{
			IEnumerable<TMaterialCriterioVerificacionDetalle> listado = base.GetBy(filter);
			List<MaterialCriterioVerificacionDetalleBO> listadoBO = new List<MaterialCriterioVerificacionDetalleBO>();
			foreach (var itemEntidad in listado)
			{
				MaterialCriterioVerificacionDetalleBO objetoBO = Mapper.Map<TMaterialCriterioVerificacionDetalle, MaterialCriterioVerificacionDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public MaterialCriterioVerificacionDetalleBO FirstById(int id)
		{
			try
			{
				TMaterialCriterioVerificacionDetalle entidad = base.FirstById(id);
				MaterialCriterioVerificacionDetalleBO objetoBO = new MaterialCriterioVerificacionDetalleBO();
				Mapper.Map<TMaterialCriterioVerificacionDetalle, MaterialCriterioVerificacionDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public MaterialCriterioVerificacionDetalleBO FirstBy(Expression<Func<TMaterialCriterioVerificacionDetalle, bool>> filter)
		{
			try
			{
				TMaterialCriterioVerificacionDetalle entidad = base.FirstBy(filter);
				MaterialCriterioVerificacionDetalleBO objetoBO = Mapper.Map<TMaterialCriterioVerificacionDetalle, MaterialCriterioVerificacionDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(MaterialCriterioVerificacionDetalleBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TMaterialCriterioVerificacionDetalle entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<MaterialCriterioVerificacionDetalleBO> listadoBO)
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

		public bool Update(MaterialCriterioVerificacionDetalleBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TMaterialCriterioVerificacionDetalle entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<MaterialCriterioVerificacionDetalleBO> listadoBO)
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
		private void AsignacionId(TMaterialCriterioVerificacionDetalle entidad, MaterialCriterioVerificacionDetalleBO objetoBO)
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

		private TMaterialCriterioVerificacionDetalle MapeoEntidad(MaterialCriterioVerificacionDetalleBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TMaterialCriterioVerificacionDetalle entidad = new TMaterialCriterioVerificacionDetalle();
				entidad = Mapper.Map<MaterialCriterioVerificacionDetalleBO, TMaterialCriterioVerificacionDetalle>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));
				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		#endregion

		public bool InsertarActualizarRegistroEntregaMaterialAlumno(RegistroEntregaMaterialTipoDTO registro)
		{
			try
			{
				var resultado = new Dictionary<string, bool>();

				string query = _dapper.QuerySPFirstOrDefault("ope.SP_InsertarRegistroMaterialAlumno", new { registro.Id , registro.IdMatriculaCabecera , registro.IdMaterialPEspecificoDetalle , registro.IdEstadoEntregaMaterialAlumno , registro.Usuario });
				if (!string.IsNullOrEmpty(query))
				{
					resultado = JsonConvert.DeserializeObject<Dictionary<string, bool>>(query);
				}
				return resultado.Select(x => x.Value).FirstOrDefault();
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}

		}
	}
}
