using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
	public class PostulanteInformacionImportacionLogRepositorio : BaseRepository<TPostulanteInformacionImportacionLog, PostulanteInformacionImportacionLogBO>
	{
		#region Metodos Base
		public PostulanteInformacionImportacionLogRepositorio() : base()
		{
		}
		public PostulanteInformacionImportacionLogRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PostulanteInformacionImportacionLogBO> GetBy(Expression<Func<TPostulanteInformacionImportacionLog, bool>> filter)
		{
			IEnumerable<TPostulanteInformacionImportacionLog> listado = base.GetBy(filter);
			List<PostulanteInformacionImportacionLogBO> listadoBO = new List<PostulanteInformacionImportacionLogBO>();
			foreach (var itemEntidad in listado)
			{
				PostulanteInformacionImportacionLogBO objetoBO = Mapper.Map<TPostulanteInformacionImportacionLog, PostulanteInformacionImportacionLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PostulanteInformacionImportacionLogBO FirstById(int id)
		{
			try
			{
				TPostulanteInformacionImportacionLog entidad = base.FirstById(id);
				PostulanteInformacionImportacionLogBO objetoBO = new PostulanteInformacionImportacionLogBO();
				Mapper.Map<TPostulanteInformacionImportacionLog, PostulanteInformacionImportacionLogBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PostulanteInformacionImportacionLogBO FirstBy(Expression<Func<TPostulanteInformacionImportacionLog, bool>> filter)
		{
			try
			{
				TPostulanteInformacionImportacionLog entidad = base.FirstBy(filter);
				PostulanteInformacionImportacionLogBO objetoBO = Mapper.Map<TPostulanteInformacionImportacionLog, PostulanteInformacionImportacionLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PostulanteInformacionImportacionLogBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPostulanteInformacionImportacionLog entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PostulanteInformacionImportacionLogBO> listadoBO)
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

		public bool Update(PostulanteInformacionImportacionLogBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPostulanteInformacionImportacionLog entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PostulanteInformacionImportacionLogBO> listadoBO)
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
		private void AsignacionId(TPostulanteInformacionImportacionLog entidad, PostulanteInformacionImportacionLogBO objetoBO)
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

		private TPostulanteInformacionImportacionLog MapeoEntidad(PostulanteInformacionImportacionLogBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPostulanteInformacionImportacionLog entidad = new TPostulanteInformacionImportacionLog();
				entidad = Mapper.Map<PostulanteInformacionImportacionLogBO, TPostulanteInformacionImportacionLog>(objetoBO,
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
