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
	public class FacebookCampanhaRepositorio : BaseRepository<TFacebookCampanha, FacebookCampanhaBO>
	{
		#region Metodos Base
		public FacebookCampanhaRepositorio() : base()
		{
		}
		public FacebookCampanhaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<FacebookCampanhaBO> GetBy(Expression<Func<TFacebookCampanha, bool>> filter)
		{
			IEnumerable<TFacebookCampanha> listado = base.GetBy(filter);
			List<FacebookCampanhaBO> listadoBO = new List<FacebookCampanhaBO>();
			foreach (var itemEntidad in listado)
			{
				FacebookCampanhaBO objetoBO = Mapper.Map<TFacebookCampanha, FacebookCampanhaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public FacebookCampanhaBO FirstById(int id)
		{
			try
			{
				TFacebookCampanha entidad = base.FirstById(id);
				FacebookCampanhaBO objetoBO = new FacebookCampanhaBO();
				Mapper.Map<TFacebookCampanha, FacebookCampanhaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public FacebookCampanhaBO FirstBy(Expression<Func<TFacebookCampanha, bool>> filter)
		{
			try
			{
				TFacebookCampanha entidad = base.FirstBy(filter);
				FacebookCampanhaBO objetoBO = Mapper.Map<TFacebookCampanha, FacebookCampanhaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(FacebookCampanhaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TFacebookCampanha entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<FacebookCampanhaBO> listadoBO)
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

		public bool Update(FacebookCampanhaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TFacebookCampanha entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<FacebookCampanhaBO> listadoBO)
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
		private void AsignacionId(TFacebookCampanha entidad, FacebookCampanhaBO objetoBO)
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

		private TFacebookCampanha MapeoEntidad(FacebookCampanhaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TFacebookCampanha entidad = new TFacebookCampanha();
				entidad = Mapper.Map<FacebookCampanhaBO, TFacebookCampanha>(objetoBO,
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
