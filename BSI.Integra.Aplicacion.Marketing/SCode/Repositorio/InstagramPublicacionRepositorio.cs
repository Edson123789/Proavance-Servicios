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
	public class InstagramPublicacionRepositorio : BaseRepository<TInstagramPublicacion, InstagramPublicacionBO>
	{
		#region Metodos Base
		public InstagramPublicacionRepositorio() : base()
		{
		}
		public InstagramPublicacionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<InstagramPublicacionBO> GetBy(Expression<Func<TInstagramPublicacion, bool>> filter)
		{
			IEnumerable<TInstagramPublicacion> listado = base.GetBy(filter);
			List<InstagramPublicacionBO> listadoBO = new List<InstagramPublicacionBO>();
			foreach (var itemEntidad in listado)
			{
				InstagramPublicacionBO objetoBO = Mapper.Map<TInstagramPublicacion, InstagramPublicacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public InstagramPublicacionBO FirstById(int id)
		{
			try
			{
				TInstagramPublicacion entidad = base.FirstById(id);
				InstagramPublicacionBO objetoBO = new InstagramPublicacionBO();
				Mapper.Map<TInstagramPublicacion, InstagramPublicacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public InstagramPublicacionBO FirstBy(Expression<Func<TInstagramPublicacion, bool>> filter)
		{
			try
			{
				TInstagramPublicacion entidad = base.FirstBy(filter);
				InstagramPublicacionBO objetoBO = Mapper.Map<TInstagramPublicacion, InstagramPublicacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(InstagramPublicacionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TInstagramPublicacion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<InstagramPublicacionBO> listadoBO)
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

		public bool Update(InstagramPublicacionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TInstagramPublicacion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<InstagramPublicacionBO> listadoBO)
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
		private void AsignacionId(TInstagramPublicacion entidad, InstagramPublicacionBO objetoBO)
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

		private TInstagramPublicacion MapeoEntidad(InstagramPublicacionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TInstagramPublicacion entidad = new TInstagramPublicacion();
				entidad = Mapper.Map<InstagramPublicacionBO, TInstagramPublicacion>(objetoBO,
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
