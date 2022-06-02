using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
    public class ProgramaGeneralCertificacionRespuestaRepositorio : BaseRepository<TProgramaGeneralCertificacionRespuesta, ProgramaGeneralCertificacionRespuestaBO>
	{
		#region Metodos Base
		public ProgramaGeneralCertificacionRespuestaRepositorio() : base()
		{
		}
		public ProgramaGeneralCertificacionRespuestaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ProgramaGeneralCertificacionRespuestaBO> GetBy(Expression<Func<TProgramaGeneralCertificacionRespuesta, bool>> filter)
		{
			IEnumerable<TProgramaGeneralCertificacionRespuesta> listado = base.GetBy(filter);
			List<ProgramaGeneralCertificacionRespuestaBO> listadoBO = new List<ProgramaGeneralCertificacionRespuestaBO>();
			foreach (var itemEntidad in listado)
			{
				ProgramaGeneralCertificacionRespuestaBO objetoBO = Mapper.Map<TProgramaGeneralCertificacionRespuesta, ProgramaGeneralCertificacionRespuestaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ProgramaGeneralCertificacionRespuestaBO FirstById(int id)
		{
			try
			{
				TProgramaGeneralCertificacionRespuesta entidad = base.FirstById(id);
				ProgramaGeneralCertificacionRespuestaBO objetoBO = new ProgramaGeneralCertificacionRespuestaBO();
				Mapper.Map<TProgramaGeneralCertificacionRespuesta, ProgramaGeneralCertificacionRespuestaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ProgramaGeneralCertificacionRespuestaBO FirstBy(Expression<Func<TProgramaGeneralCertificacionRespuesta, bool>> filter)
		{
			try
			{
				TProgramaGeneralCertificacionRespuesta entidad = base.FirstBy(filter);
				ProgramaGeneralCertificacionRespuestaBO objetoBO = Mapper.Map<TProgramaGeneralCertificacionRespuesta, ProgramaGeneralCertificacionRespuestaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ProgramaGeneralCertificacionRespuestaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TProgramaGeneralCertificacionRespuesta entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ProgramaGeneralCertificacionRespuestaBO> listadoBO)
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

		public bool Update(ProgramaGeneralCertificacionRespuestaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TProgramaGeneralCertificacionRespuesta entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ProgramaGeneralCertificacionRespuestaBO> listadoBO)
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
		private void AsignacionId(TProgramaGeneralCertificacionRespuesta entidad, ProgramaGeneralCertificacionRespuestaBO objetoBO)
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

		private TProgramaGeneralCertificacionRespuesta MapeoEntidad(ProgramaGeneralCertificacionRespuestaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TProgramaGeneralCertificacionRespuesta entidad = new TProgramaGeneralCertificacionRespuesta();
				entidad = Mapper.Map<ProgramaGeneralCertificacionRespuestaBO, TProgramaGeneralCertificacionRespuesta>(objetoBO,
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
