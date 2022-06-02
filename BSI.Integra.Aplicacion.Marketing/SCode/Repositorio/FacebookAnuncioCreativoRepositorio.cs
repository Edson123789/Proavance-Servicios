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
	public class FacebookAnuncioCreativoRepositorio : BaseRepository<TFacebookAnuncioCreativo, FacebookAnuncioCreativoBO>
	{
		#region Metodos Base
		public FacebookAnuncioCreativoRepositorio() : base()
		{
		}
		public FacebookAnuncioCreativoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<FacebookAnuncioCreativoBO> GetBy(Expression<Func<TFacebookAnuncioCreativo, bool>> filter)
		{
			IEnumerable<TFacebookAnuncioCreativo> listado = base.GetBy(filter);
			List<FacebookAnuncioCreativoBO> listadoBO = new List<FacebookAnuncioCreativoBO>();
			foreach (var itemEntidad in listado)
			{
				FacebookAnuncioCreativoBO objetoBO = Mapper.Map<TFacebookAnuncioCreativo, FacebookAnuncioCreativoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public FacebookAnuncioCreativoBO FirstById(int id)
		{
			try
			{
				TFacebookAnuncioCreativo entidad = base.FirstById(id);
				FacebookAnuncioCreativoBO objetoBO = new FacebookAnuncioCreativoBO();
				Mapper.Map<TFacebookAnuncioCreativo, FacebookAnuncioCreativoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public FacebookAnuncioCreativoBO FirstBy(Expression<Func<TFacebookAnuncioCreativo, bool>> filter)
		{
			try
			{
				TFacebookAnuncioCreativo entidad = base.FirstBy(filter);
				FacebookAnuncioCreativoBO objetoBO = Mapper.Map<TFacebookAnuncioCreativo, FacebookAnuncioCreativoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(FacebookAnuncioCreativoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TFacebookAnuncioCreativo entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<FacebookAnuncioCreativoBO> listadoBO)
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

		public bool Update(FacebookAnuncioCreativoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TFacebookAnuncioCreativo entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<FacebookAnuncioCreativoBO> listadoBO)
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
		private void AsignacionId(TFacebookAnuncioCreativo entidad, FacebookAnuncioCreativoBO objetoBO)
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

		private TFacebookAnuncioCreativo MapeoEntidad(FacebookAnuncioCreativoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TFacebookAnuncioCreativo entidad = new TFacebookAnuncioCreativo();
				entidad = Mapper.Map<FacebookAnuncioCreativoBO, TFacebookAnuncioCreativo>(objetoBO,
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
