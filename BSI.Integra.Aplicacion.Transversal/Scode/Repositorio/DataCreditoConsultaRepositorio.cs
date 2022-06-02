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
    public class DataCreditoConsultaRepositorio : BaseRepository<TDataCreditoConsulta, DataCreditoConsultaBO>
	{
		#region Metodos Base
		public DataCreditoConsultaRepositorio() : base()
		{
		}
		public DataCreditoConsultaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoConsultaBO> GetBy(Expression<Func<TDataCreditoConsulta, bool>> filter)
		{
			IEnumerable<TDataCreditoConsulta> listado = base.GetBy(filter);
			List<DataCreditoConsultaBO> listadoBO = new List<DataCreditoConsultaBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoConsultaBO objetoBO = Mapper.Map<TDataCreditoConsulta, DataCreditoConsultaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoConsultaBO FirstById(int id)
		{
			try
			{
				TDataCreditoConsulta entidad = base.FirstById(id);
				DataCreditoConsultaBO objetoBO = new DataCreditoConsultaBO();
				Mapper.Map<TDataCreditoConsulta, DataCreditoConsultaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoConsultaBO FirstBy(Expression<Func<TDataCreditoConsulta, bool>> filter)
		{
			try
			{
				TDataCreditoConsulta entidad = base.FirstBy(filter);
				DataCreditoConsultaBO objetoBO = Mapper.Map<TDataCreditoConsulta, DataCreditoConsultaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoConsultaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoConsulta entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoConsultaBO> listadoBO)
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

		public bool Update(DataCreditoConsultaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoConsulta entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoConsultaBO> listadoBO)
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
		private void AsignacionId(TDataCreditoConsulta entidad, DataCreditoConsultaBO objetoBO)
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

		private TDataCreditoConsulta MapeoEntidad(DataCreditoConsultaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoConsulta entidad = new TDataCreditoConsulta();
				entidad = Mapper.Map<DataCreditoConsultaBO, TDataCreditoConsulta>(objetoBO,
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
