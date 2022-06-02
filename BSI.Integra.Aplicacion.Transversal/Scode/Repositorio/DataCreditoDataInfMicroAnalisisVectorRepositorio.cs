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
    public class DataCreditoDataInfMicroAnalisisVectorRepositorio : BaseRepository<TDataCreditoDataInfMicroAnalisisVector, DataCreditoDataInfMicroAnalisisVectorBO>
	{
		#region Metodos Base
		public DataCreditoDataInfMicroAnalisisVectorRepositorio() : base()
		{
		}
		public DataCreditoDataInfMicroAnalisisVectorRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoDataInfMicroAnalisisVectorBO> GetBy(Expression<Func<TDataCreditoDataInfMicroAnalisisVector, bool>> filter)
		{
			IEnumerable<TDataCreditoDataInfMicroAnalisisVector> listado = base.GetBy(filter);
			List<DataCreditoDataInfMicroAnalisisVectorBO> listadoBO = new List<DataCreditoDataInfMicroAnalisisVectorBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoDataInfMicroAnalisisVectorBO objetoBO = Mapper.Map<TDataCreditoDataInfMicroAnalisisVector, DataCreditoDataInfMicroAnalisisVectorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoDataInfMicroAnalisisVectorBO FirstById(int id)
		{
			try
			{
				TDataCreditoDataInfMicroAnalisisVector entidad = base.FirstById(id);
				DataCreditoDataInfMicroAnalisisVectorBO objetoBO = new DataCreditoDataInfMicroAnalisisVectorBO();
				Mapper.Map<TDataCreditoDataInfMicroAnalisisVector, DataCreditoDataInfMicroAnalisisVectorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoDataInfMicroAnalisisVectorBO FirstBy(Expression<Func<TDataCreditoDataInfMicroAnalisisVector, bool>> filter)
		{
			try
			{
				TDataCreditoDataInfMicroAnalisisVector entidad = base.FirstBy(filter);
				DataCreditoDataInfMicroAnalisisVectorBO objetoBO = Mapper.Map<TDataCreditoDataInfMicroAnalisisVector, DataCreditoDataInfMicroAnalisisVectorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoDataInfMicroAnalisisVectorBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoDataInfMicroAnalisisVector entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoDataInfMicroAnalisisVectorBO> listadoBO)
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

		public bool Update(DataCreditoDataInfMicroAnalisisVectorBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoDataInfMicroAnalisisVector entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoDataInfMicroAnalisisVectorBO> listadoBO)
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
		private void AsignacionId(TDataCreditoDataInfMicroAnalisisVector entidad, DataCreditoDataInfMicroAnalisisVectorBO objetoBO)
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

		private TDataCreditoDataInfMicroAnalisisVector MapeoEntidad(DataCreditoDataInfMicroAnalisisVectorBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoDataInfMicroAnalisisVector entidad = new TDataCreditoDataInfMicroAnalisisVector();
				entidad = Mapper.Map<DataCreditoDataInfMicroAnalisisVectorBO, TDataCreditoDataInfMicroAnalisisVector>(objetoBO,
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
