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
	public class FacebookReaccionPublicacionRepositorio : BaseRepository<TFacebookReaccionPublicacion, FacebookReaccionPublicacionBO>
	{
		#region Metodos Base
		public FacebookReaccionPublicacionRepositorio() : base()
		{
		}
		public FacebookReaccionPublicacionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<FacebookReaccionPublicacionBO> GetBy(Expression<Func<TFacebookReaccionPublicacion, bool>> filter)
		{
			IEnumerable<TFacebookReaccionPublicacion> listado = base.GetBy(filter);
			List<FacebookReaccionPublicacionBO> listadoBO = new List<FacebookReaccionPublicacionBO>();
			foreach (var itemEntidad in listado)
			{
				FacebookReaccionPublicacionBO objetoBO = Mapper.Map<TFacebookReaccionPublicacion, FacebookReaccionPublicacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public FacebookReaccionPublicacionBO FirstById(int id)
		{
			try
			{
				TFacebookReaccionPublicacion entidad = base.FirstById(id);
				FacebookReaccionPublicacionBO objetoBO = new FacebookReaccionPublicacionBO();
				Mapper.Map<TFacebookReaccionPublicacion, FacebookReaccionPublicacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public FacebookReaccionPublicacionBO FirstBy(Expression<Func<TFacebookReaccionPublicacion, bool>> filter)
		{
			try
			{
				TFacebookReaccionPublicacion entidad = base.FirstBy(filter);
				FacebookReaccionPublicacionBO objetoBO = Mapper.Map<TFacebookReaccionPublicacion, FacebookReaccionPublicacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(FacebookReaccionPublicacionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TFacebookReaccionPublicacion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<FacebookReaccionPublicacionBO> listadoBO)
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

		public bool Update(FacebookReaccionPublicacionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TFacebookReaccionPublicacion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<FacebookReaccionPublicacionBO> listadoBO)
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
		private void AsignacionId(TFacebookReaccionPublicacion entidad, FacebookReaccionPublicacionBO objetoBO)
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

		private TFacebookReaccionPublicacion MapeoEntidad(FacebookReaccionPublicacionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TFacebookReaccionPublicacion entidad = new TFacebookReaccionPublicacion();
				entidad = Mapper.Map<FacebookReaccionPublicacionBO, TFacebookReaccionPublicacion>(objetoBO,
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
