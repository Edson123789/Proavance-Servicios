using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
	public class ScrapingAerolineaEstadoConsultaRepositorio : BaseRepository<TScrapingAerolineaEstadoConsulta, ScrapingAerolineaEstadoConsultaBO>
	{
		#region Metodos Base
		public ScrapingAerolineaEstadoConsultaRepositorio() : base()
		{
		}
		public ScrapingAerolineaEstadoConsultaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ScrapingAerolineaEstadoConsultaBO> GetBy(Expression<Func<TScrapingAerolineaEstadoConsulta, bool>> filter)
		{
			IEnumerable<TScrapingAerolineaEstadoConsulta> listado = base.GetBy(filter);
			List<ScrapingAerolineaEstadoConsultaBO> listadoBO = new List<ScrapingAerolineaEstadoConsultaBO>();
			foreach (var itemEntidad in listado)
			{
				ScrapingAerolineaEstadoConsultaBO objetoBO = Mapper.Map<TScrapingAerolineaEstadoConsulta, ScrapingAerolineaEstadoConsultaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ScrapingAerolineaEstadoConsultaBO FirstById(int id)
		{
			try
			{
				TScrapingAerolineaEstadoConsulta entidad = base.FirstById(id);
				ScrapingAerolineaEstadoConsultaBO objetoBO = new ScrapingAerolineaEstadoConsultaBO();
				Mapper.Map<TScrapingAerolineaEstadoConsulta, ScrapingAerolineaEstadoConsultaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ScrapingAerolineaEstadoConsultaBO FirstBy(Expression<Func<TScrapingAerolineaEstadoConsulta, bool>> filter)
		{
			try
			{
				TScrapingAerolineaEstadoConsulta entidad = base.FirstBy(filter);
				ScrapingAerolineaEstadoConsultaBO objetoBO = Mapper.Map<TScrapingAerolineaEstadoConsulta, ScrapingAerolineaEstadoConsultaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ScrapingAerolineaEstadoConsultaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TScrapingAerolineaEstadoConsulta entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ScrapingAerolineaEstadoConsultaBO> listadoBO)
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

		public bool Update(ScrapingAerolineaEstadoConsultaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TScrapingAerolineaEstadoConsulta entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ScrapingAerolineaEstadoConsultaBO> listadoBO)
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
		private void AsignacionId(TScrapingAerolineaEstadoConsulta entidad, ScrapingAerolineaEstadoConsultaBO objetoBO)
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

		private TScrapingAerolineaEstadoConsulta MapeoEntidad(ScrapingAerolineaEstadoConsultaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TScrapingAerolineaEstadoConsulta entidad = new TScrapingAerolineaEstadoConsulta();
				entidad = Mapper.Map<ScrapingAerolineaEstadoConsultaBO, TScrapingAerolineaEstadoConsulta>(objetoBO,
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
