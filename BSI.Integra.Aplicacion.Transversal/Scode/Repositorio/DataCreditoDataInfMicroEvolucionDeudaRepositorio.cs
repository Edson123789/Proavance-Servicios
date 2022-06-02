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
    public class DataCreditoDataInfMicroEvolucionDeudaRepositorio : BaseRepository<TDataCreditoDataInfMicroEvolucionDeuda, DataCreditoDataInfMicroEvolucionDeudaBO>
	{
		#region Metodos Base
		public DataCreditoDataInfMicroEvolucionDeudaRepositorio() : base()
		{
		}
		public DataCreditoDataInfMicroEvolucionDeudaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoDataInfMicroEvolucionDeudaBO> GetBy(Expression<Func<TDataCreditoDataInfMicroEvolucionDeuda, bool>> filter)
		{
			IEnumerable<TDataCreditoDataInfMicroEvolucionDeuda> listado = base.GetBy(filter);
			List<DataCreditoDataInfMicroEvolucionDeudaBO> listadoBO = new List<DataCreditoDataInfMicroEvolucionDeudaBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoDataInfMicroEvolucionDeudaBO objetoBO = Mapper.Map<TDataCreditoDataInfMicroEvolucionDeuda, DataCreditoDataInfMicroEvolucionDeudaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoDataInfMicroEvolucionDeudaBO FirstById(int id)
		{
			try
			{
				TDataCreditoDataInfMicroEvolucionDeuda entidad = base.FirstById(id);
				DataCreditoDataInfMicroEvolucionDeudaBO objetoBO = new DataCreditoDataInfMicroEvolucionDeudaBO();
				Mapper.Map<TDataCreditoDataInfMicroEvolucionDeuda, DataCreditoDataInfMicroEvolucionDeudaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoDataInfMicroEvolucionDeudaBO FirstBy(Expression<Func<TDataCreditoDataInfMicroEvolucionDeuda, bool>> filter)
		{
			try
			{
				TDataCreditoDataInfMicroEvolucionDeuda entidad = base.FirstBy(filter);
				DataCreditoDataInfMicroEvolucionDeudaBO objetoBO = Mapper.Map<TDataCreditoDataInfMicroEvolucionDeuda, DataCreditoDataInfMicroEvolucionDeudaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoDataInfMicroEvolucionDeudaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoDataInfMicroEvolucionDeuda entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoDataInfMicroEvolucionDeudaBO> listadoBO)
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

		public bool Update(DataCreditoDataInfMicroEvolucionDeudaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoDataInfMicroEvolucionDeuda entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoDataInfMicroEvolucionDeudaBO> listadoBO)
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
		private void AsignacionId(TDataCreditoDataInfMicroEvolucionDeuda entidad, DataCreditoDataInfMicroEvolucionDeudaBO objetoBO)
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

		private TDataCreditoDataInfMicroEvolucionDeuda MapeoEntidad(DataCreditoDataInfMicroEvolucionDeudaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoDataInfMicroEvolucionDeuda entidad = new TDataCreditoDataInfMicroEvolucionDeuda();
				entidad = Mapper.Map<DataCreditoDataInfMicroEvolucionDeudaBO, TDataCreditoDataInfMicroEvolucionDeuda>(objetoBO,
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
