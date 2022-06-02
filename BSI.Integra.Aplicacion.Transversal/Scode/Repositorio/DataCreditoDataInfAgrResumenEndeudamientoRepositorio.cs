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
    public class DataCreditoDataInfAgrResumenEndeudamientoRepositorio : BaseRepository<TDataCreditoDataInfAgrResumenEndeudamiento, DataCreditoDataInfAgrResumenEndeudamientoBO>
	{
		#region Metodos Base
		public DataCreditoDataInfAgrResumenEndeudamientoRepositorio() : base()
		{
		}
		public DataCreditoDataInfAgrResumenEndeudamientoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoDataInfAgrResumenEndeudamientoBO> GetBy(Expression<Func<TDataCreditoDataInfAgrResumenEndeudamiento, bool>> filter)
		{
			IEnumerable<TDataCreditoDataInfAgrResumenEndeudamiento> listado = base.GetBy(filter);
			List<DataCreditoDataInfAgrResumenEndeudamientoBO> listadoBO = new List<DataCreditoDataInfAgrResumenEndeudamientoBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoDataInfAgrResumenEndeudamientoBO objetoBO = Mapper.Map<TDataCreditoDataInfAgrResumenEndeudamiento, DataCreditoDataInfAgrResumenEndeudamientoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoDataInfAgrResumenEndeudamientoBO FirstById(int id)
		{
			try
			{
				TDataCreditoDataInfAgrResumenEndeudamiento entidad = base.FirstById(id);
				DataCreditoDataInfAgrResumenEndeudamientoBO objetoBO = new DataCreditoDataInfAgrResumenEndeudamientoBO();
				Mapper.Map<TDataCreditoDataInfAgrResumenEndeudamiento, DataCreditoDataInfAgrResumenEndeudamientoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoDataInfAgrResumenEndeudamientoBO FirstBy(Expression<Func<TDataCreditoDataInfAgrResumenEndeudamiento, bool>> filter)
		{
			try
			{
				TDataCreditoDataInfAgrResumenEndeudamiento entidad = base.FirstBy(filter);
				DataCreditoDataInfAgrResumenEndeudamientoBO objetoBO = Mapper.Map<TDataCreditoDataInfAgrResumenEndeudamiento, DataCreditoDataInfAgrResumenEndeudamientoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoDataInfAgrResumenEndeudamientoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoDataInfAgrResumenEndeudamiento entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoDataInfAgrResumenEndeudamientoBO> listadoBO)
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

		public bool Update(DataCreditoDataInfAgrResumenEndeudamientoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoDataInfAgrResumenEndeudamiento entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoDataInfAgrResumenEndeudamientoBO> listadoBO)
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
		private void AsignacionId(TDataCreditoDataInfAgrResumenEndeudamiento entidad, DataCreditoDataInfAgrResumenEndeudamientoBO objetoBO)
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

		private TDataCreditoDataInfAgrResumenEndeudamiento MapeoEntidad(DataCreditoDataInfAgrResumenEndeudamientoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoDataInfAgrResumenEndeudamiento entidad = new TDataCreditoDataInfAgrResumenEndeudamiento();
				entidad = Mapper.Map<DataCreditoDataInfAgrResumenEndeudamientoBO, TDataCreditoDataInfAgrResumenEndeudamiento>(objetoBO,
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
