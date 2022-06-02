using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
	public class FacebookAudienciaAlumnoRepositorio : BaseRepository<TFacebookAudienciaAlumno, FacebookAudienciaAlumnoBO>
	{
		#region Metodos Base
		public FacebookAudienciaAlumnoRepositorio() : base()
		{
		}
		public FacebookAudienciaAlumnoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<FacebookAudienciaAlumnoBO> GetBy(Expression<Func<TFacebookAudienciaAlumno, bool>> filter)
		{
			IEnumerable<TFacebookAudienciaAlumno> listado = base.GetBy(filter);
			List<FacebookAudienciaAlumnoBO> listadoBO = new List<FacebookAudienciaAlumnoBO>();
			foreach (var itemEntidad in listado)
			{
				FacebookAudienciaAlumnoBO objetoBO = Mapper.Map<TFacebookAudienciaAlumno, FacebookAudienciaAlumnoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public FacebookAudienciaAlumnoBO FirstById(int id)
		{
			try
			{
				TFacebookAudienciaAlumno entidad = base.FirstById(id);
				FacebookAudienciaAlumnoBO objetoBO = new FacebookAudienciaAlumnoBO();
				Mapper.Map<TFacebookAudienciaAlumno, FacebookAudienciaAlumnoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public FacebookAudienciaAlumnoBO FirstBy(Expression<Func<TFacebookAudienciaAlumno, bool>> filter)
		{
			try
			{
				TFacebookAudienciaAlumno entidad = base.FirstBy(filter);
				FacebookAudienciaAlumnoBO objetoBO = Mapper.Map<TFacebookAudienciaAlumno, FacebookAudienciaAlumnoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(FacebookAudienciaAlumnoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TFacebookAudienciaAlumno entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<FacebookAudienciaAlumnoBO> listadoBO)
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

		public bool Update(FacebookAudienciaAlumnoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TFacebookAudienciaAlumno entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<FacebookAudienciaAlumnoBO> listadoBO)
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
		private void AsignacionId(TFacebookAudienciaAlumno entidad, FacebookAudienciaAlumnoBO objetoBO)
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

		private TFacebookAudienciaAlumno MapeoEntidad(FacebookAudienciaAlumnoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TFacebookAudienciaAlumno entidad = new TFacebookAudienciaAlumno();
				entidad = Mapper.Map<FacebookAudienciaAlumnoBO, TFacebookAudienciaAlumno>(objetoBO,
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
