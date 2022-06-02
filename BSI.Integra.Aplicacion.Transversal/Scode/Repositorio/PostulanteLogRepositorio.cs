using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
	public class PostulanteLogRepositorio : BaseRepository<TPostulanteLog, PostulanteLogBO>
	{
		#region Metodos Base
		public PostulanteLogRepositorio() : base()
		{
		}
		public PostulanteLogRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PostulanteLogBO> GetBy(Expression<Func<TPostulanteLog, bool>> filter)
		{
			IEnumerable<TPostulanteLog> listado = base.GetBy(filter);
			List<PostulanteLogBO> listadoBO = new List<PostulanteLogBO>();
			foreach (var itemEntidad in listado)
			{
				PostulanteLogBO objetoBO = Mapper.Map<TPostulanteLog, PostulanteLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PostulanteLogBO FirstById(int id)
		{
			try
			{
				TPostulanteLog entidad = base.FirstById(id);
				PostulanteLogBO objetoBO = new PostulanteLogBO();
				Mapper.Map<TPostulanteLog, PostulanteLogBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PostulanteLogBO FirstBy(Expression<Func<TPostulanteLog, bool>> filter)
		{
			try
			{
				TPostulanteLog entidad = base.FirstBy(filter);
				PostulanteLogBO objetoBO = Mapper.Map<TPostulanteLog, PostulanteLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PostulanteLogBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPostulanteLog entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PostulanteLogBO> listadoBO)
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

		public bool Update(PostulanteLogBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPostulanteLog entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PostulanteLogBO> listadoBO)
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
		private void AsignacionId(TPostulanteLog entidad, PostulanteLogBO objetoBO)
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

		private TPostulanteLog MapeoEntidad(PostulanteLogBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPostulanteLog entidad = new TPostulanteLog();
				entidad = Mapper.Map<PostulanteLogBO, TPostulanteLog>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<PostulanteLogBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TPostulanteLog, bool>>> filters, Expression<Func<TPostulanteLog, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TPostulanteLog> listado = base.GetFiltered(filters, orderBy, ascending);
			List<PostulanteLogBO> listadoBO = new List<PostulanteLogBO>();

			foreach (var itemEntidad in listado)
			{
				PostulanteLogBO objetoBO = Mapper.Map<TPostulanteLog, PostulanteLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion
	}
}
