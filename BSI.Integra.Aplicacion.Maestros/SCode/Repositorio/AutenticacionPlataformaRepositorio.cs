using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Maestros.BO;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
	public class AutenticacionPlataformaRepositorio : BaseRepository<TAutenticacionPlataforma, AutenticacionPlataformaBO>
	{
		#region Metodos Base
		public AutenticacionPlataformaRepositorio() : base()
		{
		}
		public AutenticacionPlataformaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<AutenticacionPlataformaBO> GetBy(Expression<Func<TAutenticacionPlataforma, bool>> filter)
		{
			IEnumerable<TAutenticacionPlataforma> listado = base.GetBy(filter);
			List<AutenticacionPlataformaBO> listadoBO = new List<AutenticacionPlataformaBO>();
			foreach (var itemEntidad in listado)
			{
				AutenticacionPlataformaBO objetoBO = Mapper.Map<TAutenticacionPlataforma, AutenticacionPlataformaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public AutenticacionPlataformaBO FirstById(int id)
		{
			try
			{
				TAutenticacionPlataforma entidad = base.FirstById(id);
				AutenticacionPlataformaBO objetoBO = new AutenticacionPlataformaBO();
				Mapper.Map<TAutenticacionPlataforma, AutenticacionPlataformaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public AutenticacionPlataformaBO FirstBy(Expression<Func<TAutenticacionPlataforma, bool>> filter)
		{
			try
			{
				TAutenticacionPlataforma entidad = base.FirstBy(filter);
				AutenticacionPlataformaBO objetoBO = Mapper.Map<TAutenticacionPlataforma, AutenticacionPlataformaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(AutenticacionPlataformaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TAutenticacionPlataforma entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<AutenticacionPlataformaBO> listadoBO)
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

		public bool Update(AutenticacionPlataformaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TAutenticacionPlataforma entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<AutenticacionPlataformaBO> listadoBO)
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
		private void AsignacionId(TAutenticacionPlataforma entidad, AutenticacionPlataformaBO objetoBO)
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

		private TAutenticacionPlataforma MapeoEntidad(AutenticacionPlataformaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TAutenticacionPlataforma entidad = new TAutenticacionPlataforma();
				entidad = Mapper.Map<AutenticacionPlataformaBO, TAutenticacionPlataforma>(objetoBO,
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
