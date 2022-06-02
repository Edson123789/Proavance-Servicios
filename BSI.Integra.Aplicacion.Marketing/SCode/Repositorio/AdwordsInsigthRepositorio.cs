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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.Util.Reports;
using Google.Api.Ads.AdWords.Util.Reports.v201809;
using Google.Api.Ads.AdWords.v201809;
using Google.Api.Ads.Common.Util.Reports;
using Mandrill;
using Mandrill.Models;
using Mandrill.Requests.Messages;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Xml;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
	public class AdwordsInsigthRepositorio : BaseRepository<TAdwordInsigth, AdwordInsigthBO>
	{
		#region Metodos Base
		public AdwordsInsigthRepositorio() : base()
		{
		}
		public AdwordsInsigthRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<AdwordInsigthBO> GetBy(Expression<Func<TAdwordInsigth, bool>> filter)
		{
			IEnumerable<TAdwordInsigth> listado = base.GetBy(filter);
			List<AdwordInsigthBO> listadoBO = new List<AdwordInsigthBO>();
			foreach (var itemEntidad in listado)
			{
				AdwordInsigthBO objetoBO = Mapper.Map<TAdwordInsigth, AdwordInsigthBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public AdwordInsigthBO FirstById(int id)
		{
			try
			{
				TAdwordInsigth entidad = base.FirstById(id);
				AdwordInsigthBO objetoBO = new AdwordInsigthBO();
				Mapper.Map<TAdwordInsigth, AdwordInsigthBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public AdwordInsigthBO FirstBy(Expression<Func<TAdwordInsigth, bool>> filter)
		{
			try
			{
				TAdwordInsigth entidad = base.FirstBy(filter);
				AdwordInsigthBO objetoBO = Mapper.Map<TAdwordInsigth, AdwordInsigthBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(AdwordInsigthBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TAdwordInsigth entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<AdwordInsigthBO> listadoBO)
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

		public bool Update(AdwordInsigthBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TAdwordInsigth entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<AdwordInsigthBO> listadoBO)
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
		private void AsignacionId(TAdwordInsigth entidad, AdwordInsigthBO objetoBO)
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

		private TAdwordInsigth MapeoEntidad(AdwordInsigthBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TAdwordInsigth entidad = new TAdwordInsigth();
				entidad = Mapper.Map<AdwordInsigthBO, TAdwordInsigth>(objetoBO,
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
