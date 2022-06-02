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
    public class DataCreditoDataInfAgrEvolucionDeudaTrimestreRepositorio : BaseRepository<TDataCreditoDataInfAgrEvolucionDeudaTrimestre, DataCreditoDataInfAgrEvolucionDeudaTrimestreBO>
	{
		#region Metodos Base
		public DataCreditoDataInfAgrEvolucionDeudaTrimestreRepositorio() : base()
		{
		}
		public DataCreditoDataInfAgrEvolucionDeudaTrimestreRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoDataInfAgrEvolucionDeudaTrimestreBO> GetBy(Expression<Func<TDataCreditoDataInfAgrEvolucionDeudaTrimestre, bool>> filter)
		{
			IEnumerable<TDataCreditoDataInfAgrEvolucionDeudaTrimestre> listado = base.GetBy(filter);
			List<DataCreditoDataInfAgrEvolucionDeudaTrimestreBO> listadoBO = new List<DataCreditoDataInfAgrEvolucionDeudaTrimestreBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoDataInfAgrEvolucionDeudaTrimestreBO objetoBO = Mapper.Map<TDataCreditoDataInfAgrEvolucionDeudaTrimestre, DataCreditoDataInfAgrEvolucionDeudaTrimestreBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoDataInfAgrEvolucionDeudaTrimestreBO FirstById(int id)
		{
			try
			{
				TDataCreditoDataInfAgrEvolucionDeudaTrimestre entidad = base.FirstById(id);
				DataCreditoDataInfAgrEvolucionDeudaTrimestreBO objetoBO = new DataCreditoDataInfAgrEvolucionDeudaTrimestreBO();
				Mapper.Map<TDataCreditoDataInfAgrEvolucionDeudaTrimestre, DataCreditoDataInfAgrEvolucionDeudaTrimestreBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoDataInfAgrEvolucionDeudaTrimestreBO FirstBy(Expression<Func<TDataCreditoDataInfAgrEvolucionDeudaTrimestre, bool>> filter)
		{
			try
			{
				TDataCreditoDataInfAgrEvolucionDeudaTrimestre entidad = base.FirstBy(filter);
				DataCreditoDataInfAgrEvolucionDeudaTrimestreBO objetoBO = Mapper.Map<TDataCreditoDataInfAgrEvolucionDeudaTrimestre, DataCreditoDataInfAgrEvolucionDeudaTrimestreBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoDataInfAgrEvolucionDeudaTrimestreBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoDataInfAgrEvolucionDeudaTrimestre entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoDataInfAgrEvolucionDeudaTrimestreBO> listadoBO)
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

		public bool Update(DataCreditoDataInfAgrEvolucionDeudaTrimestreBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoDataInfAgrEvolucionDeudaTrimestre entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoDataInfAgrEvolucionDeudaTrimestreBO> listadoBO)
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
		private void AsignacionId(TDataCreditoDataInfAgrEvolucionDeudaTrimestre entidad, DataCreditoDataInfAgrEvolucionDeudaTrimestreBO objetoBO)
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

		private TDataCreditoDataInfAgrEvolucionDeudaTrimestre MapeoEntidad(DataCreditoDataInfAgrEvolucionDeudaTrimestreBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoDataInfAgrEvolucionDeudaTrimestre entidad = new TDataCreditoDataInfAgrEvolucionDeudaTrimestre();
				entidad = Mapper.Map<DataCreditoDataInfAgrEvolucionDeudaTrimestreBO, TDataCreditoDataInfAgrEvolucionDeudaTrimestre>(objetoBO,
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
