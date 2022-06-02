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
    public class DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioRepositorio : BaseRepository<TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio, DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioBO>
	{
		#region Metodos Base
		public DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioRepositorio() : base()
		{
		}
		public DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioBO> GetBy(Expression<Func<TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio, bool>> filter)
		{
			IEnumerable<TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio> listado = base.GetBy(filter);
			List<DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioBO> listadoBO = new List<DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioBO objetoBO = Mapper.Map<TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio, DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioBO FirstById(int id)
		{
			try
			{
				TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio entidad = base.FirstById(id);
				DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioBO objetoBO = new DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioBO();
				Mapper.Map<TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio, DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioBO FirstBy(Expression<Func<TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio, bool>> filter)
		{
			try
			{
				TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio entidad = base.FirstBy(filter);
				DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioBO objetoBO = Mapper.Map<TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio, DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioBO> listadoBO)
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

		public bool Update(DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioBO> listadoBO)
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
		private void AsignacionId(TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio entidad, DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioBO objetoBO)
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

		private TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio MapeoEntidad(DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio entidad = new TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio();
				entidad = Mapper.Map<DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioBO, TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio>(objetoBO,
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
