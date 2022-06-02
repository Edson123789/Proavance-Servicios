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
	public class InstagramUsuarioRepositorio : BaseRepository<TInstagramUsuario, InstagramUsuarioBO>
	{
		#region Metodos Base
		public InstagramUsuarioRepositorio() : base()
		{
		}
		public InstagramUsuarioRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<InstagramUsuarioBO> GetBy(Expression<Func<TInstagramUsuario, bool>> filter)
		{
			IEnumerable<TInstagramUsuario> listado = base.GetBy(filter);
			List<InstagramUsuarioBO> listadoBO = new List<InstagramUsuarioBO>();
			foreach (var itemEntidad in listado)
			{
				InstagramUsuarioBO objetoBO = Mapper.Map<TInstagramUsuario, InstagramUsuarioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public InstagramUsuarioBO FirstById(int id)
		{
			try
			{
				TInstagramUsuario entidad = base.FirstById(id);
				InstagramUsuarioBO objetoBO = new InstagramUsuarioBO();
				Mapper.Map<TInstagramUsuario, InstagramUsuarioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public InstagramUsuarioBO FirstBy(Expression<Func<TInstagramUsuario, bool>> filter)
		{
			try
			{
				TInstagramUsuario entidad = base.FirstBy(filter);
				InstagramUsuarioBO objetoBO = Mapper.Map<TInstagramUsuario, InstagramUsuarioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(InstagramUsuarioBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TInstagramUsuario entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<InstagramUsuarioBO> listadoBO)
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

		public bool Update(InstagramUsuarioBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TInstagramUsuario entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<InstagramUsuarioBO> listadoBO)
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
		private void AsignacionId(TInstagramUsuario entidad, InstagramUsuarioBO objetoBO)
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

		private TInstagramUsuario MapeoEntidad(InstagramUsuarioBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TInstagramUsuario entidad = new TInstagramUsuario();
				entidad = Mapper.Map<InstagramUsuarioBO, TInstagramUsuario>(objetoBO,
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
