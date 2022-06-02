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
    public class DataCreditoDataInfMicroEndeudamientoActualRepositorio : BaseRepository<TDataCreditoDataInfMicroEndeudamientoActual, DataCreditoDataInfMicroEndeudamientoActualBO>
	{
		#region Metodos Base
		public DataCreditoDataInfMicroEndeudamientoActualRepositorio() : base()
		{
		}
		public DataCreditoDataInfMicroEndeudamientoActualRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoDataInfMicroEndeudamientoActualBO> GetBy(Expression<Func<TDataCreditoDataInfMicroEndeudamientoActual, bool>> filter)
		{
			IEnumerable<TDataCreditoDataInfMicroEndeudamientoActual> listado = base.GetBy(filter);
			List<DataCreditoDataInfMicroEndeudamientoActualBO> listadoBO = new List<DataCreditoDataInfMicroEndeudamientoActualBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoDataInfMicroEndeudamientoActualBO objetoBO = Mapper.Map<TDataCreditoDataInfMicroEndeudamientoActual, DataCreditoDataInfMicroEndeudamientoActualBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoDataInfMicroEndeudamientoActualBO FirstById(int id)
		{
			try
			{
				TDataCreditoDataInfMicroEndeudamientoActual entidad = base.FirstById(id);
				DataCreditoDataInfMicroEndeudamientoActualBO objetoBO = new DataCreditoDataInfMicroEndeudamientoActualBO();
				Mapper.Map<TDataCreditoDataInfMicroEndeudamientoActual, DataCreditoDataInfMicroEndeudamientoActualBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoDataInfMicroEndeudamientoActualBO FirstBy(Expression<Func<TDataCreditoDataInfMicroEndeudamientoActual, bool>> filter)
		{
			try
			{
				TDataCreditoDataInfMicroEndeudamientoActual entidad = base.FirstBy(filter);
				DataCreditoDataInfMicroEndeudamientoActualBO objetoBO = Mapper.Map<TDataCreditoDataInfMicroEndeudamientoActual, DataCreditoDataInfMicroEndeudamientoActualBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoDataInfMicroEndeudamientoActualBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoDataInfMicroEndeudamientoActual entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoDataInfMicroEndeudamientoActualBO> listadoBO)
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

		public bool Update(DataCreditoDataInfMicroEndeudamientoActualBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoDataInfMicroEndeudamientoActual entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoDataInfMicroEndeudamientoActualBO> listadoBO)
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
		private void AsignacionId(TDataCreditoDataInfMicroEndeudamientoActual entidad, DataCreditoDataInfMicroEndeudamientoActualBO objetoBO)
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

		private TDataCreditoDataInfMicroEndeudamientoActual MapeoEntidad(DataCreditoDataInfMicroEndeudamientoActualBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoDataInfMicroEndeudamientoActual entidad = new TDataCreditoDataInfMicroEndeudamientoActual();
				entidad = Mapper.Map<DataCreditoDataInfMicroEndeudamientoActualBO, TDataCreditoDataInfMicroEndeudamientoActual>(objetoBO,
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
