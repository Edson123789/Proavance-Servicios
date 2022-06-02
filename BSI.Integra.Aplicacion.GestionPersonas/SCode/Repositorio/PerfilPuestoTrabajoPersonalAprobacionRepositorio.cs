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
	public class PerfilPuestoTrabajoPersonalAprobacionRepositorio : BaseRepository<TPerfilPuestoTrabajoPersonalAprobacion, PerfilPuestoTrabajoPersonalAprobacionBO>
	{
		#region Metodos Base
		public PerfilPuestoTrabajoPersonalAprobacionRepositorio() : base()
		{
		}
		public PerfilPuestoTrabajoPersonalAprobacionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PerfilPuestoTrabajoPersonalAprobacionBO> GetBy(Expression<Func<TPerfilPuestoTrabajoPersonalAprobacion, bool>> filter)
		{
			IEnumerable<TPerfilPuestoTrabajoPersonalAprobacion> listado = base.GetBy(filter);
			List<PerfilPuestoTrabajoPersonalAprobacionBO> listadoBO = new List<PerfilPuestoTrabajoPersonalAprobacionBO>();
			foreach (var itemEntidad in listado)
			{
				PerfilPuestoTrabajoPersonalAprobacionBO objetoBO = Mapper.Map<TPerfilPuestoTrabajoPersonalAprobacion, PerfilPuestoTrabajoPersonalAprobacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PerfilPuestoTrabajoPersonalAprobacionBO FirstById(int id)
		{
			try
			{
				TPerfilPuestoTrabajoPersonalAprobacion entidad = base.FirstById(id);
				PerfilPuestoTrabajoPersonalAprobacionBO objetoBO = new PerfilPuestoTrabajoPersonalAprobacionBO();
				Mapper.Map<TPerfilPuestoTrabajoPersonalAprobacion, PerfilPuestoTrabajoPersonalAprobacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PerfilPuestoTrabajoPersonalAprobacionBO FirstBy(Expression<Func<TPerfilPuestoTrabajoPersonalAprobacion, bool>> filter)
		{
			try
			{
				TPerfilPuestoTrabajoPersonalAprobacion entidad = base.FirstBy(filter);
				PerfilPuestoTrabajoPersonalAprobacionBO objetoBO = Mapper.Map<TPerfilPuestoTrabajoPersonalAprobacion, PerfilPuestoTrabajoPersonalAprobacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PerfilPuestoTrabajoPersonalAprobacionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPerfilPuestoTrabajoPersonalAprobacion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PerfilPuestoTrabajoPersonalAprobacionBO> listadoBO)
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

		public bool Update(PerfilPuestoTrabajoPersonalAprobacionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPerfilPuestoTrabajoPersonalAprobacion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PerfilPuestoTrabajoPersonalAprobacionBO> listadoBO)
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
		private void AsignacionId(TPerfilPuestoTrabajoPersonalAprobacion entidad, PerfilPuestoTrabajoPersonalAprobacionBO objetoBO)
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

		private TPerfilPuestoTrabajoPersonalAprobacion MapeoEntidad(PerfilPuestoTrabajoPersonalAprobacionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPerfilPuestoTrabajoPersonalAprobacion entidad = new TPerfilPuestoTrabajoPersonalAprobacion();
				entidad = Mapper.Map<PerfilPuestoTrabajoPersonalAprobacionBO, TPerfilPuestoTrabajoPersonalAprobacion>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<PerfilPuestoTrabajoPersonalAprobacionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TPerfilPuestoTrabajoPersonalAprobacion, bool>>> filters, Expression<Func<TPerfilPuestoTrabajoPersonalAprobacion, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TPerfilPuestoTrabajoPersonalAprobacion> listado = base.GetFiltered(filters, orderBy, ascending);
			List<PerfilPuestoTrabajoPersonalAprobacionBO> listadoBO = new List<PerfilPuestoTrabajoPersonalAprobacionBO>();

			foreach (var itemEntidad in listado)
			{
				PerfilPuestoTrabajoPersonalAprobacionBO objetoBO = Mapper.Map<TPerfilPuestoTrabajoPersonalAprobacion, PerfilPuestoTrabajoPersonalAprobacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion

		/// <summary>
		/// Obtiene lista de personal configurado para aprobar versiones de perfil de puesto de trabajo
		/// </summary>
		/// <returns></returns>
		public List<PerfilPuestoTrabajoPersonalAprobacionDatosDTO> ObtenerPersonalConfigurado()
		{
			try
			{
				List<PerfilPuestoTrabajoPersonalAprobacionDatosDTO> lista = new List<PerfilPuestoTrabajoPersonalAprobacionDatosDTO>();
				var query = "SELECT Id, IdPersonal, Personal, IdPuestoTrabajo, PuestoTrabajo FROM [gp].[V_TPerfilPuestoTrabajoPersonalAprobacion_ObtenerPersonalConfigurado] WHERE Estado = 1";
				var res = _dapper.QueryDapper(query, null);
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<PerfilPuestoTrabajoPersonalAprobacionDatosDTO>>(res);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
