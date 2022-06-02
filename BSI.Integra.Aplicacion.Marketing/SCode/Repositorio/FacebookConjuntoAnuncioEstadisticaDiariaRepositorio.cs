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
	public class FacebookConjuntoAnuncioEstadisticaDiariaRepositorio : BaseRepository<TFacebookConjuntoAnuncioEstadisticaDiaria, FacebookConjuntoAnuncioEstadisticaDiariaBO>
	{
		#region Metodos Base
		public FacebookConjuntoAnuncioEstadisticaDiariaRepositorio() : base()
		{
		}
		public FacebookConjuntoAnuncioEstadisticaDiariaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<FacebookConjuntoAnuncioEstadisticaDiariaBO> GetBy(Expression<Func<TFacebookConjuntoAnuncioEstadisticaDiaria, bool>> filter)
		{
			IEnumerable<TFacebookConjuntoAnuncioEstadisticaDiaria> listado = base.GetBy(filter);
			List<FacebookConjuntoAnuncioEstadisticaDiariaBO> listadoBO = new List<FacebookConjuntoAnuncioEstadisticaDiariaBO>();
			foreach (var itemEntidad in listado)
			{
				FacebookConjuntoAnuncioEstadisticaDiariaBO objetoBO = Mapper.Map<TFacebookConjuntoAnuncioEstadisticaDiaria, FacebookConjuntoAnuncioEstadisticaDiariaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public FacebookConjuntoAnuncioEstadisticaDiariaBO FirstById(int id)
		{
			try
			{
				TFacebookConjuntoAnuncioEstadisticaDiaria entidad = base.FirstById(id);
				FacebookConjuntoAnuncioEstadisticaDiariaBO objetoBO = new FacebookConjuntoAnuncioEstadisticaDiariaBO();
				Mapper.Map<TFacebookConjuntoAnuncioEstadisticaDiaria, FacebookConjuntoAnuncioEstadisticaDiariaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public FacebookConjuntoAnuncioEstadisticaDiariaBO FirstBy(Expression<Func<TFacebookConjuntoAnuncioEstadisticaDiaria, bool>> filter)
		{
			try
			{
				TFacebookConjuntoAnuncioEstadisticaDiaria entidad = base.FirstBy(filter);
				FacebookConjuntoAnuncioEstadisticaDiariaBO objetoBO = Mapper.Map<TFacebookConjuntoAnuncioEstadisticaDiaria, FacebookConjuntoAnuncioEstadisticaDiariaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(FacebookConjuntoAnuncioEstadisticaDiariaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TFacebookConjuntoAnuncioEstadisticaDiaria entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<FacebookConjuntoAnuncioEstadisticaDiariaBO> listadoBO)
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

		public bool Update(FacebookConjuntoAnuncioEstadisticaDiariaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TFacebookConjuntoAnuncioEstadisticaDiaria entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<FacebookConjuntoAnuncioEstadisticaDiariaBO> listadoBO)
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
		private void AsignacionId(TFacebookConjuntoAnuncioEstadisticaDiaria entidad, FacebookConjuntoAnuncioEstadisticaDiariaBO objetoBO)
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

		private TFacebookConjuntoAnuncioEstadisticaDiaria MapeoEntidad(FacebookConjuntoAnuncioEstadisticaDiariaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TFacebookConjuntoAnuncioEstadisticaDiaria entidad = new TFacebookConjuntoAnuncioEstadisticaDiaria();
				entidad = Mapper.Map<FacebookConjuntoAnuncioEstadisticaDiariaBO, TFacebookConjuntoAnuncioEstadisticaDiaria>(objetoBO,
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
