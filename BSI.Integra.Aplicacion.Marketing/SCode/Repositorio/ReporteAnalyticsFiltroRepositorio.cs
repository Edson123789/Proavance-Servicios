using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
	public class ReporteAnalyticsFiltroRepositorio : BaseRepository<TReporteAnalyticsFiltro, ReporteAnalyticsFiltroBO>
	{
		#region Metodos Base
		public ReporteAnalyticsFiltroRepositorio() : base()
		{
		}
		public ReporteAnalyticsFiltroRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ReporteAnalyticsFiltroBO> GetBy(Expression<Func<TReporteAnalyticsFiltro, bool>> filter)
		{
			IEnumerable<TReporteAnalyticsFiltro> listado = base.GetBy(filter);
			List<ReporteAnalyticsFiltroBO> listadoBO = new List<ReporteAnalyticsFiltroBO>();
			foreach (var itemEntidad in listado)
			{
				ReporteAnalyticsFiltroBO objetoBO = Mapper.Map<TReporteAnalyticsFiltro, ReporteAnalyticsFiltroBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ReporteAnalyticsFiltroBO FirstById(int id)
		{
			try
			{
				TReporteAnalyticsFiltro entidad = base.FirstById(id);
				ReporteAnalyticsFiltroBO objetoBO = new ReporteAnalyticsFiltroBO();
				Mapper.Map<TReporteAnalyticsFiltro, ReporteAnalyticsFiltroBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ReporteAnalyticsFiltroBO FirstBy(Expression<Func<TReporteAnalyticsFiltro, bool>> filter)
		{
			try
			{
				TReporteAnalyticsFiltro entidad = base.FirstBy(filter);
				ReporteAnalyticsFiltroBO objetoBO = Mapper.Map<TReporteAnalyticsFiltro, ReporteAnalyticsFiltroBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ReporteAnalyticsFiltroBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TReporteAnalyticsFiltro entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ReporteAnalyticsFiltroBO> listadoBO)
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

		public bool Update(ReporteAnalyticsFiltroBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TReporteAnalyticsFiltro entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ReporteAnalyticsFiltroBO> listadoBO)
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
		private void AsignacionId(TReporteAnalyticsFiltro entidad, ReporteAnalyticsFiltroBO objetoBO)
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

		private TReporteAnalyticsFiltro MapeoEntidad(ReporteAnalyticsFiltroBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TReporteAnalyticsFiltro entidad = new TReporteAnalyticsFiltro();
				entidad = Mapper.Map<ReporteAnalyticsFiltroBO, TReporteAnalyticsFiltro>(objetoBO,
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
