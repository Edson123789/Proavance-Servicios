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
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
	public class PuestoTrabajoRemuneracionRepositorio : BaseRepository<TPuestoTrabajoRemuneracion, PuestoTrabajoRemuneracionBO>
	{
		#region Metodos Base
		public PuestoTrabajoRemuneracionRepositorio() : base()
		{
		}
		public PuestoTrabajoRemuneracionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PuestoTrabajoRemuneracionBO> GetBy(Expression<Func<TPuestoTrabajoRemuneracion, bool>> filter)
		{
			IEnumerable<TPuestoTrabajoRemuneracion> listado = base.GetBy(filter);
			List<PuestoTrabajoRemuneracionBO> listadoBO = new List<PuestoTrabajoRemuneracionBO>();
			foreach (var itemEntidad in listado)
			{
				PuestoTrabajoRemuneracionBO objetoBO = Mapper.Map<TPuestoTrabajoRemuneracion, PuestoTrabajoRemuneracionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PuestoTrabajoRemuneracionBO FirstById(int id)
		{
			try
			{
				TPuestoTrabajoRemuneracion entidad = base.FirstById(id);
				PuestoTrabajoRemuneracionBO objetoBO = new PuestoTrabajoRemuneracionBO();
				Mapper.Map<TPuestoTrabajoRemuneracion, PuestoTrabajoRemuneracionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PuestoTrabajoRemuneracionBO FirstById(int id, integraDBContext _integraDBContext)
		{
			try
			{
				TPuestoTrabajoRemuneracion entidad = base.FirstById(id);
				PuestoTrabajoRemuneracionBO objetoBO = new PuestoTrabajoRemuneracionBO(_integraDBContext);
				Mapper.Map<TPuestoTrabajoRemuneracion, PuestoTrabajoRemuneracionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PuestoTrabajoRemuneracionBO FirstBy(Expression<Func<TPuestoTrabajoRemuneracion, bool>> filter)
		{
			try
			{
				TPuestoTrabajoRemuneracion entidad = base.FirstBy(filter);
				PuestoTrabajoRemuneracionBO objetoBO = Mapper.Map<TPuestoTrabajoRemuneracion, PuestoTrabajoRemuneracionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PuestoTrabajoRemuneracionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPuestoTrabajoRemuneracion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PuestoTrabajoRemuneracionBO> listadoBO)
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

		public bool Update(PuestoTrabajoRemuneracionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPuestoTrabajoRemuneracion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PuestoTrabajoRemuneracionBO> listadoBO)
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
		private void AsignacionId(TPuestoTrabajoRemuneracion entidad, PuestoTrabajoRemuneracionBO objetoBO)
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

		private TPuestoTrabajoRemuneracion MapeoEntidad(PuestoTrabajoRemuneracionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPuestoTrabajoRemuneracion entidad = new TPuestoTrabajoRemuneracion();
				entidad = Mapper.Map<PuestoTrabajoRemuneracionBO, TPuestoTrabajoRemuneracion>(objetoBO,
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
		/// Obtiene la lista de todos los elementos en la tabla PuestoTrabajoRemuneracion
		/// </summary>
		/// <returns></returns>
		public List<PuestoTrabajoRemuneracionDTO> ObtenerPuestoTrabajoRemuneracionRegistrado()
		{
			try
			{
				List<PuestoTrabajoRemuneracionDTO> listaPuestoTrabajoRemuneracion = new List<PuestoTrabajoRemuneracionDTO>();
				var query = "SELECT * FROM [gp].[V_TPuestoTrabajoRemuneracion_ObtenerRegistro] WHERE Estado = 1";
				var res = _dapper.QueryDapper(query, null);

				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					listaPuestoTrabajoRemuneracion = JsonConvert.DeserializeObject<List<PuestoTrabajoRemuneracionDTO>>(res);
				}
				return listaPuestoTrabajoRemuneracion;

			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene la categoria para el combo box
		/// </summary>
		/// <returns></returns>
		public List<FiltroBasicoDTO> ObtenerCategoria()
		{
			try
			{
				List<FiltroBasicoDTO> listaCategoriaTableroComercial = new List<FiltroBasicoDTO>();
				//var query = "SELECT Id,Nombre FROM com.T_TableroComercialCategoriaAsesor WHERE Estado = 1";
				var query = "SELECT Id,Nombre FROM com.T_TableroComercialCategoriaAsesor WHERE Estado = 1";
				var res = _dapper.QueryDapper(query, null);

				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					listaCategoriaTableroComercial = JsonConvert.DeserializeObject<List<FiltroBasicoDTO>>(res);
				}
				return listaCategoriaTableroComercial;

			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene la categoria para el combo box
		/// </summary>
		/// <returns></returns>
		public bool EliminarExistentes(int Id)
		{
			try
			{
				//List<FiltroBasicoDTO> listaCategoriaTableroComercial = new List<FiltroBasicoDTO>();
				bool respuesta = false;
				var query = "gp.SP_EliminarPuestoTrabajoRemuneracion";
				var res = _dapper.QuerySPDapper(query, new { id =Id});

				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					respuesta = true;
				}
				return respuesta;

			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

	}
}
