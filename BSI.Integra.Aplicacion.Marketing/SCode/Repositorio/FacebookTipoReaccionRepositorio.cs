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
	public class FacebookTipoReaccionRepositorio : BaseRepository<TFacebookTipoReaccion, FacebookTipoReaccionBO>
	{
		#region Metodos Base
		public FacebookTipoReaccionRepositorio() : base()
		{
		}
		public FacebookTipoReaccionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<FacebookTipoReaccionBO> GetBy(Expression<Func<TFacebookTipoReaccion, bool>> filter)
		{
			IEnumerable<TFacebookTipoReaccion> listado = base.GetBy(filter);
			List<FacebookTipoReaccionBO> listadoBO = new List<FacebookTipoReaccionBO>();
			foreach (var itemEntidad in listado)
			{
				FacebookTipoReaccionBO objetoBO = Mapper.Map<TFacebookTipoReaccion, FacebookTipoReaccionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public FacebookTipoReaccionBO FirstById(int id)
		{
			try
			{
				TFacebookTipoReaccion entidad = base.FirstById(id);
				FacebookTipoReaccionBO objetoBO = new FacebookTipoReaccionBO();
				Mapper.Map<TFacebookTipoReaccion, FacebookTipoReaccionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public FacebookTipoReaccionBO FirstBy(Expression<Func<TFacebookTipoReaccion, bool>> filter)
		{
			try
			{
				TFacebookTipoReaccion entidad = base.FirstBy(filter);
				FacebookTipoReaccionBO objetoBO = Mapper.Map<TFacebookTipoReaccion, FacebookTipoReaccionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(FacebookTipoReaccionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TFacebookTipoReaccion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<FacebookTipoReaccionBO> listadoBO)
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

		public bool Update(FacebookTipoReaccionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TFacebookTipoReaccion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<FacebookTipoReaccionBO> listadoBO)
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
		private void AsignacionId(TFacebookTipoReaccion entidad, FacebookTipoReaccionBO objetoBO)
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

		private TFacebookTipoReaccion MapeoEntidad(FacebookTipoReaccionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TFacebookTipoReaccion entidad = new TFacebookTipoReaccion();
				entidad = Mapper.Map<FacebookTipoReaccionBO, TFacebookTipoReaccion>(objetoBO,
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
