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
	public class FacebookAnuncioRepositorio : BaseRepository<TFacebookAnuncio, FacebookAnuncioBO>
	{
		#region Metodos Base
		public FacebookAnuncioRepositorio() : base()
		{
		}
		public FacebookAnuncioRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<FacebookAnuncioBO> GetBy(Expression<Func<TFacebookAnuncio, bool>> filter)
		{
			IEnumerable<TFacebookAnuncio> listado = base.GetBy(filter);
			List<FacebookAnuncioBO> listadoBO = new List<FacebookAnuncioBO>();
			foreach (var itemEntidad in listado)
			{
				FacebookAnuncioBO objetoBO = Mapper.Map<TFacebookAnuncio, FacebookAnuncioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public FacebookAnuncioBO FirstById(int id)
		{
			try
			{
				TFacebookAnuncio entidad = base.FirstById(id);
				FacebookAnuncioBO objetoBO = new FacebookAnuncioBO();
				Mapper.Map<TFacebookAnuncio, FacebookAnuncioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public FacebookAnuncioBO FirstBy(Expression<Func<TFacebookAnuncio, bool>> filter)
		{
			try
			{
				TFacebookAnuncio entidad = base.FirstBy(filter);
				FacebookAnuncioBO objetoBO = Mapper.Map<TFacebookAnuncio, FacebookAnuncioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(FacebookAnuncioBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TFacebookAnuncio entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<FacebookAnuncioBO> listadoBO)
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

		public bool Update(FacebookAnuncioBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TFacebookAnuncio entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<FacebookAnuncioBO> listadoBO)
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
		private void AsignacionId(TFacebookAnuncio entidad, FacebookAnuncioBO objetoBO)
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

		private TFacebookAnuncio MapeoEntidad(FacebookAnuncioBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TFacebookAnuncio entidad = new TFacebookAnuncio();
				entidad = Mapper.Map<FacebookAnuncioBO, TFacebookAnuncio>(objetoBO,
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
