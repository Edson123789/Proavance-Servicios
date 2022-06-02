using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class DataCreditoDataNaturalNacionalRepositorio : BaseRepository<TDataCreditoDataNaturalNacional, DataCreditoDataNaturalNacionalBO>
    {
		#region Metodos Base
		public DataCreditoDataNaturalNacionalRepositorio() : base()
		{
		}
		public DataCreditoDataNaturalNacionalRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoDataNaturalNacionalBO> GetBy(Expression<Func<TDataCreditoDataNaturalNacional, bool>> filter)
		{
			IEnumerable<TDataCreditoDataNaturalNacional> listado = base.GetBy(filter);
			List<DataCreditoDataNaturalNacionalBO> listadoBO = new List<DataCreditoDataNaturalNacionalBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoDataNaturalNacionalBO objetoBO = Mapper.Map<TDataCreditoDataNaturalNacional, DataCreditoDataNaturalNacionalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoDataNaturalNacionalBO FirstById(int id)
		{
			try
			{
				TDataCreditoDataNaturalNacional entidad = base.FirstById(id);
				DataCreditoDataNaturalNacionalBO objetoBO = new DataCreditoDataNaturalNacionalBO();
				Mapper.Map<TDataCreditoDataNaturalNacional, DataCreditoDataNaturalNacionalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoDataNaturalNacionalBO FirstBy(Expression<Func<TDataCreditoDataNaturalNacional, bool>> filter)
		{
			try
			{
				TDataCreditoDataNaturalNacional entidad = base.FirstBy(filter);
				DataCreditoDataNaturalNacionalBO objetoBO = Mapper.Map<TDataCreditoDataNaturalNacional, DataCreditoDataNaturalNacionalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoDataNaturalNacionalBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoDataNaturalNacional entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoDataNaturalNacionalBO> listadoBO)
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

		public bool Update(DataCreditoDataNaturalNacionalBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoDataNaturalNacional entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoDataNaturalNacionalBO> listadoBO)
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
		private void AsignacionId(TDataCreditoDataNaturalNacional entidad, DataCreditoDataNaturalNacionalBO objetoBO)
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

		private TDataCreditoDataNaturalNacional MapeoEntidad(DataCreditoDataNaturalNacionalBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoDataNaturalNacional entidad = new TDataCreditoDataNaturalNacional();
				entidad = Mapper.Map<DataCreditoDataNaturalNacionalBO, TDataCreditoDataNaturalNacional>(objetoBO,
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
	}
}
