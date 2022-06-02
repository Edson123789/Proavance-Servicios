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
	public class PuestoTrabajoPuestoACargoRepositorio : BaseRepository<TPuestoTrabajoPuestoAcargo, PuestoTrabajoPuestoACargoBO>
	{
		#region Metodos Base
		public PuestoTrabajoPuestoACargoRepositorio() : base()
		{
		}
		public PuestoTrabajoPuestoACargoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PuestoTrabajoPuestoACargoBO> GetBy(Expression<Func<TPuestoTrabajoPuestoAcargo, bool>> filter)
		{
			IEnumerable<TPuestoTrabajoPuestoAcargo> listado = base.GetBy(filter);
			List<PuestoTrabajoPuestoACargoBO> listadoBO = new List<PuestoTrabajoPuestoACargoBO>();
			foreach (var itemEntidad in listado)
			{
				PuestoTrabajoPuestoACargoBO objetoBO = Mapper.Map<TPuestoTrabajoPuestoAcargo, PuestoTrabajoPuestoACargoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PuestoTrabajoPuestoACargoBO FirstById(int id)
		{
			try
			{
				TPuestoTrabajoPuestoAcargo entidad = base.FirstById(id);
				PuestoTrabajoPuestoACargoBO objetoBO = new PuestoTrabajoPuestoACargoBO();
				Mapper.Map<TPuestoTrabajoPuestoAcargo, PuestoTrabajoPuestoACargoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PuestoTrabajoPuestoACargoBO FirstBy(Expression<Func<TPuestoTrabajoPuestoAcargo, bool>> filter)
		{
			try
			{
				TPuestoTrabajoPuestoAcargo entidad = base.FirstBy(filter);
				PuestoTrabajoPuestoACargoBO objetoBO = Mapper.Map<TPuestoTrabajoPuestoAcargo, PuestoTrabajoPuestoACargoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PuestoTrabajoPuestoACargoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPuestoTrabajoPuestoAcargo entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PuestoTrabajoPuestoACargoBO> listadoBO)
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

		public bool Update(PuestoTrabajoPuestoACargoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPuestoTrabajoPuestoAcargo entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PuestoTrabajoPuestoACargoBO> listadoBO)
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
		private void AsignacionId(TPuestoTrabajoPuestoAcargo entidad, PuestoTrabajoPuestoACargoBO objetoBO)
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

		private TPuestoTrabajoPuestoAcargo MapeoEntidad(PuestoTrabajoPuestoACargoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPuestoTrabajoPuestoAcargo entidad = new TPuestoTrabajoPuestoAcargo();
				entidad = Mapper.Map<PuestoTrabajoPuestoACargoBO, TPuestoTrabajoPuestoAcargo>(objetoBO,
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
		/// Obtiene lista de relaciones internas de un determinado puesto de trabajo
		/// </summary>
		/// <returns></returns>
		public List<PuestoTrabajoPuestoACargoDTO> ObtenerPuestoTrabajoPuestoACargo(int idPerfilPuestoTrabajo)
		{
			try
			{
				List<PuestoTrabajoPuestoACargoDTO> lista = new List<PuestoTrabajoPuestoACargoDTO>();
				var _query = "SELECT IdPuestoTrabajoPuestoACargo, PuestoTrabajoPuestoACargo FROM [gp].[V_TPuestoTrabajoPuestoTrabajoPuestoACargo_ObtenerPuestoACargo] WHERE Estado = 1 AND IdPerfilPuestoTrabajo = @IdPerfilPuestoTrabajo";
				var res = _dapper.QueryDapper(_query, new { IdPerfilPuestoTrabajo = idPerfilPuestoTrabajo });
				if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
				{
					lista = JsonConvert.DeserializeObject<List<PuestoTrabajoPuestoACargoDTO>>(res);
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
