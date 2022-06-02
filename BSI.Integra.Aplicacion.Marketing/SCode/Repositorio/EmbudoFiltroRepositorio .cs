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
	public class EmbudoFiltroRepositorio : BaseRepository<TEmbudoFiltro, EmbudoFiltroBO>
	{
		#region Metodos Base
		public EmbudoFiltroRepositorio() : base()
		{
		}
		public EmbudoFiltroRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<EmbudoFiltroBO> GetBy(Expression<Func<TEmbudoFiltro, bool>> filter)
		{
			IEnumerable<TEmbudoFiltro> listado = base.GetBy(filter);
			List<EmbudoFiltroBO> listadoBO = new List<EmbudoFiltroBO>();
			foreach (var itemEntidad in listado)
			{
				EmbudoFiltroBO objetoBO = Mapper.Map<TEmbudoFiltro, EmbudoFiltroBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public EmbudoFiltroBO FirstById(int id)
		{
			try
			{
				TEmbudoFiltro entidad = base.FirstById(id);
				EmbudoFiltroBO objetoBO = new EmbudoFiltroBO();
				Mapper.Map<TEmbudoFiltro, EmbudoFiltroBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public EmbudoFiltroBO FirstBy(Expression<Func<TEmbudoFiltro, bool>> filter)
		{
			try
			{
				TEmbudoFiltro entidad = base.FirstBy(filter);
				EmbudoFiltroBO objetoBO = Mapper.Map<TEmbudoFiltro, EmbudoFiltroBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(EmbudoFiltroBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TEmbudoFiltro entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<EmbudoFiltroBO> listadoBO)
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

		public bool Update(EmbudoFiltroBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TEmbudoFiltro entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<EmbudoFiltroBO> listadoBO)
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
		private void AsignacionId(TEmbudoFiltro entidad, EmbudoFiltroBO objetoBO)
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

		private TEmbudoFiltro MapeoEntidad(EmbudoFiltroBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TEmbudoFiltro entidad = new TEmbudoFiltro();
				entidad = Mapper.Map<EmbudoFiltroBO, TEmbudoFiltro>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None).ForMember(dest => dest.IdMigracion, m => m.Ignore()));

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
