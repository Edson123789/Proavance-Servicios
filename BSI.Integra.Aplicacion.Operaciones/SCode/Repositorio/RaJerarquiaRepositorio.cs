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
	public class RaJerarquiaRepositorio : BaseRepository<TRaJerarquia, RaJerarquiaBO>
	{
		#region Metodos Base
		public RaJerarquiaRepositorio() : base()
		{
		}
		public RaJerarquiaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<RaJerarquiaBO> GetBy(Expression<Func<TRaJerarquia, bool>> filter)
		{
			IEnumerable<TRaJerarquia> listado = base.GetBy(filter);
			List<RaJerarquiaBO> listadoBO = new List<RaJerarquiaBO>();
			foreach (var itemEntidad in listado)
			{
				RaJerarquiaBO objetoBO = Mapper.Map<TRaJerarquia, RaJerarquiaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public RaJerarquiaBO FirstById(int id)
		{
			try
			{
				TRaJerarquia entidad = base.FirstById(id);
				RaJerarquiaBO objetoBO = new RaJerarquiaBO();
				Mapper.Map<TRaJerarquia, RaJerarquiaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public RaJerarquiaBO FirstBy(Expression<Func<TRaJerarquia, bool>> filter)
		{
			try
			{
				TRaJerarquia entidad = base.FirstBy(filter);
				RaJerarquiaBO objetoBO = Mapper.Map<TRaJerarquia, RaJerarquiaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(RaJerarquiaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TRaJerarquia entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<RaJerarquiaBO> listadoBO)
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

		public bool Update(RaJerarquiaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TRaJerarquia entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<RaJerarquiaBO> listadoBO)
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
		private void AsignacionId(TRaJerarquia entidad, RaJerarquiaBO objetoBO)
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

		private TRaJerarquia MapeoEntidad(RaJerarquiaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TRaJerarquia entidad = new TRaJerarquia();
				entidad = Mapper.Map<RaJerarquiaBO, TRaJerarquia>(objetoBO,
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
		/// Obtiene listas de jerarquias para los combos
		/// </summary>
		/// <returns></returns>
		public List<FiltroJerarquiaDTO> ObtenerJerarquias()
		{
			try
			{
				string query = "";
				query = "SELECT Id, IdJefe, IdSubordinado, NombresSubordinado, NombresJefe FROM ope.V_ObtenerNombresUsuarioSubordinadosJefes WHERE Estado = 1";
				var listaJerarquia = _dapper.QueryDapper(query, null);
				return JsonConvert.DeserializeObject<List<FiltroJerarquiaDTO>>(listaJerarquia);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
