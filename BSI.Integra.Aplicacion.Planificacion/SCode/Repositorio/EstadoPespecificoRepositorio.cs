using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
	public class EstadoPespecificoRepositorio : BaseRepository<TEstadoPespecifico, EstadoPespecificoBO>
	{
		#region Metodos Base
		public EstadoPespecificoRepositorio() : base()
		{
		}
		public EstadoPespecificoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<EstadoPespecificoBO> GetBy(Expression<Func<TEstadoPespecifico, bool>> filter)
		{
			IEnumerable<TEstadoPespecifico> listado = base.GetBy(filter);
			List<EstadoPespecificoBO> listadoBO = new List<EstadoPespecificoBO>();
			foreach (var itemEntidad in listado)
			{
				EstadoPespecificoBO objetoBO = Mapper.Map<TEstadoPespecifico, EstadoPespecificoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public EstadoPespecificoBO FirstById(int id)
		{
			try
			{
				TEstadoPespecifico entidad = base.FirstById(id);
				EstadoPespecificoBO objetoBO = new EstadoPespecificoBO();
				Mapper.Map<TEstadoPespecifico, EstadoPespecificoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public EstadoPespecificoBO FirstBy(Expression<Func<TEstadoPespecifico, bool>> filter)
		{
			try
			{
				TEstadoPespecifico entidad = base.FirstBy(filter);
				EstadoPespecificoBO objetoBO = Mapper.Map<TEstadoPespecifico, EstadoPespecificoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(EstadoPespecificoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TEstadoPespecifico entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<EstadoPespecificoBO> listadoBO)
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

		public bool Update(EstadoPespecificoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TEstadoPespecifico entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<EstadoPespecificoBO> listadoBO)
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
		private void AsignacionId(TEstadoPespecifico entidad, EstadoPespecificoBO objetoBO)
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

		private TEstadoPespecifico MapeoEntidad(EstadoPespecificoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TEstadoPespecifico entidad = new TEstadoPespecifico();
				entidad = Mapper.Map<EstadoPespecificoBO, TEstadoPespecifico>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None).ForMember(dest => dest.IdMigracion, m => m.Ignore()));

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
		/// Obtiene lista de EstadoPespecifico para combo
		/// </summary>
		/// <returns></returns>
		public IEnumerable<FiltroDTO> ObtenerEstadoPespecificoParaCombo()
		{
			try
			{
				var combo = GetBy(x => x.Estado == true , x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre });
				return combo;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
