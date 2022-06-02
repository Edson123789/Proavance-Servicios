using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
	public class ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleRepositorio : BaseRepository<TConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalle, ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO>
	{
		#region Metodos Base
		public ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleRepositorio() : base()
		{
		}
		public ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO> GetBy(Expression<Func<TConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalle, bool>> filter)
		{
			IEnumerable<TConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalle> listado = base.GetBy(filter);
			List<ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO> listadoBO = new List<ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO>();
			foreach (var itemEntidad in listado)
			{
				ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO objetoBO = Mapper.Map<TConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalle, ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO FirstById(int id)
		{
			try
			{
				TConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalle entidad = base.FirstById(id);
				ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO objetoBO = new ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO();
				Mapper.Map<TConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalle, ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO FirstBy(Expression<Func<TConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalle, bool>> filter)
		{
			try
			{
				TConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalle entidad = base.FirstBy(filter);
				ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO objetoBO = Mapper.Map<TConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalle, ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalle entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO> listadoBO)
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

		public bool Update(ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalle entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO> listadoBO)
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
		private void AsignacionId(TConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalle entidad, ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO objetoBO)
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

		private TConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalle MapeoEntidad(ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalle entidad = new TConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalle();
				entidad = Mapper.Map<ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO, TConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalle>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalle, bool>>> filters, Expression<Func<TConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalle, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalle> listado = base.GetFiltered(filters, orderBy, ascending);
			List<ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO> listadoBO = new List<ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO>();

			foreach (var itemEntidad in listado)
			{
				ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO objetoBO = Mapper.Map<TConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalle, ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion


	}
}