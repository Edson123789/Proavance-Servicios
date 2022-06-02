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
    public class CompromisoAlumnoRepositorio : BaseRepository<TCompromisoAlumno, CompromisoAlumnoBO>
    {
		#region Metodos Base
		public CompromisoAlumnoRepositorio() : base()
		{
		}
		public CompromisoAlumnoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<CompromisoAlumnoBO> GetBy(Expression<Func<TCompromisoAlumno, bool>> filter)
		{
			IEnumerable<TCompromisoAlumno> listado = base.GetBy(filter);
			List<CompromisoAlumnoBO> listadoBO = new List<CompromisoAlumnoBO>();
			foreach (var itemEntidad in listado)
			{
				CompromisoAlumnoBO objetoBO = Mapper.Map<TCompromisoAlumno, CompromisoAlumnoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public CompromisoAlumnoBO FirstById(int id)
		{
			try
			{
				TCompromisoAlumno entidad = base.FirstById(id);
				CompromisoAlumnoBO objetoBO = new CompromisoAlumnoBO();
				Mapper.Map<TCompromisoAlumno, CompromisoAlumnoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public CompromisoAlumnoBO FirstBy(Expression<Func<TCompromisoAlumno, bool>> filter)
		{
			try
			{
				TCompromisoAlumno entidad = base.FirstBy(filter);
				CompromisoAlumnoBO objetoBO = Mapper.Map<TCompromisoAlumno, CompromisoAlumnoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(CompromisoAlumnoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TCompromisoAlumno entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<CompromisoAlumnoBO> listadoBO)
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

		public bool Update(CompromisoAlumnoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TCompromisoAlumno entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<CompromisoAlumnoBO> listadoBO)
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
		private void AsignacionId(TCompromisoAlumno entidad, CompromisoAlumnoBO objetoBO)
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

		private TCompromisoAlumno MapeoEntidad(CompromisoAlumnoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TCompromisoAlumno entidad = new TCompromisoAlumno();
				entidad = Mapper.Map<CompromisoAlumnoBO, TCompromisoAlumno>(objetoBO,
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
