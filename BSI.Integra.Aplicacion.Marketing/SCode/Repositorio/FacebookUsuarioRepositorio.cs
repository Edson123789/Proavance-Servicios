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
	public class FacebookUsuarioRepositorio : BaseRepository<TFacebookUsuario, FacebookUsuarioBO>
	{
		#region Metodos Base
		public FacebookUsuarioRepositorio() : base()
		{
		}
		public FacebookUsuarioRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<FacebookUsuarioBO> GetBy(Expression<Func<TFacebookUsuario, bool>> filter)
		{
			IEnumerable<TFacebookUsuario> listado = base.GetBy(filter);
			List<FacebookUsuarioBO> listadoBO = new List<FacebookUsuarioBO>();
			foreach (var itemEntidad in listado)
			{
				FacebookUsuarioBO objetoBO = Mapper.Map<TFacebookUsuario, FacebookUsuarioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public FacebookUsuarioBO FirstById(int id)
		{
			try
			{
				TFacebookUsuario entidad = base.FirstById(id);
				FacebookUsuarioBO objetoBO = new FacebookUsuarioBO();
				Mapper.Map<TFacebookUsuario, FacebookUsuarioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public FacebookUsuarioBO FirstBy(Expression<Func<TFacebookUsuario, bool>> filter)
		{
			try
			{
				TFacebookUsuario entidad = base.FirstBy(filter);
				FacebookUsuarioBO objetoBO = Mapper.Map<TFacebookUsuario, FacebookUsuarioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(FacebookUsuarioBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TFacebookUsuario entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<FacebookUsuarioBO> listadoBO)
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

		public bool Update(FacebookUsuarioBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TFacebookUsuario entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<FacebookUsuarioBO> listadoBO)
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
		private void AsignacionId(TFacebookUsuario entidad, FacebookUsuarioBO objetoBO)
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

		private TFacebookUsuario MapeoEntidad(FacebookUsuarioBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TFacebookUsuario entidad = new TFacebookUsuario();
				entidad = Mapper.Map<FacebookUsuarioBO, TFacebookUsuario>(objetoBO,
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
