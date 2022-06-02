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
	public class PuestoTrabajoDependenciaRepositorio : BaseRepository<TPuestoTrabajoDependencia, PuestoTrabajoDependenciaBO>
	{
		#region Metodos Base
		public PuestoTrabajoDependenciaRepositorio() : base()
		{
		}
		public PuestoTrabajoDependenciaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PuestoTrabajoDependenciaBO> GetBy(Expression<Func<TPuestoTrabajoDependencia, bool>> filter)
		{
			IEnumerable<TPuestoTrabajoDependencia> listado = base.GetBy(filter);
			List<PuestoTrabajoDependenciaBO> listadoBO = new List<PuestoTrabajoDependenciaBO>();
			foreach (var itemEntidad in listado)
			{
				PuestoTrabajoDependenciaBO objetoBO = Mapper.Map<TPuestoTrabajoDependencia, PuestoTrabajoDependenciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PuestoTrabajoDependenciaBO FirstById(int id)
		{
			try
			{
				TPuestoTrabajoDependencia entidad = base.FirstById(id);
				PuestoTrabajoDependenciaBO objetoBO = new PuestoTrabajoDependenciaBO();
				Mapper.Map<TPuestoTrabajoDependencia, PuestoTrabajoDependenciaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PuestoTrabajoDependenciaBO FirstBy(Expression<Func<TPuestoTrabajoDependencia, bool>> filter)
		{
			try
			{
				TPuestoTrabajoDependencia entidad = base.FirstBy(filter);
				PuestoTrabajoDependenciaBO objetoBO = Mapper.Map<TPuestoTrabajoDependencia, PuestoTrabajoDependenciaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PuestoTrabajoDependenciaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPuestoTrabajoDependencia entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PuestoTrabajoDependenciaBO> listadoBO)
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

		public bool Update(PuestoTrabajoDependenciaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPuestoTrabajoDependencia entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PuestoTrabajoDependenciaBO> listadoBO)
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
		private void AsignacionId(TPuestoTrabajoDependencia entidad, PuestoTrabajoDependenciaBO objetoBO)
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

		private TPuestoTrabajoDependencia MapeoEntidad(PuestoTrabajoDependenciaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPuestoTrabajoDependencia entidad = new TPuestoTrabajoDependencia();
				entidad = Mapper.Map<PuestoTrabajoDependenciaBO, TPuestoTrabajoDependencia>(objetoBO,
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
		/// Obtiene lista de dependencias de un determinado perfil puesto de trabajo
		/// </summary>
		/// <returns></returns>
		public List<PuestoTrabajoDependenciaDTO> ObtenerPuestoTrabajoDependencia(int idPerfilPuestoTrabajo)
		{
			try
			{
				List<PuestoTrabajoDependenciaDTO> lista = new List<PuestoTrabajoDependenciaDTO>();
				var _query = "SELECT IdPuestoTrabajoDependencia, PuestoTrabajoDependencia FROM [gp].[V_TPuestoTrabajoDependencia_ObtenerPuestosDependendientes] WHERE Estado = 1 AND IdPerfilPuestoTrabajo = @IdPerfilPuestoTrabajo";
				var res = _dapper.QueryDapper(_query, new { IdPerfilPuestoTrabajo = idPerfilPuestoTrabajo });
				if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
				{
					lista = JsonConvert.DeserializeObject<List<PuestoTrabajoDependenciaDTO>>(res);
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
