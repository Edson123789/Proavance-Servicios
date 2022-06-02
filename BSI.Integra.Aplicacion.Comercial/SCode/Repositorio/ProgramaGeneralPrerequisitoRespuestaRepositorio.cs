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
    public class ProgramaGeneralPrerequisitoRespuestaRepositorio : BaseRepository<TProgramaGeneralPrerequisitoRespuesta, ProgramaGeneralPrerequisitoRespuestaBO>
	{
		#region Metodos Base
		public ProgramaGeneralPrerequisitoRespuestaRepositorio() : base()
		{
		}
		public ProgramaGeneralPrerequisitoRespuestaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ProgramaGeneralPrerequisitoRespuestaBO> GetBy(Expression<Func<TProgramaGeneralPrerequisitoRespuesta, bool>> filter)
		{
			IEnumerable<TProgramaGeneralPrerequisitoRespuesta> listado = base.GetBy(filter);
			List<ProgramaGeneralPrerequisitoRespuestaBO> listadoBO = new List<ProgramaGeneralPrerequisitoRespuestaBO>();
			foreach (var itemEntidad in listado)
			{
				ProgramaGeneralPrerequisitoRespuestaBO objetoBO = Mapper.Map<TProgramaGeneralPrerequisitoRespuesta, ProgramaGeneralPrerequisitoRespuestaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ProgramaGeneralPrerequisitoRespuestaBO FirstById(int id)
		{
			try
			{
				TProgramaGeneralPrerequisitoRespuesta entidad = base.FirstById(id);
				ProgramaGeneralPrerequisitoRespuestaBO objetoBO = new ProgramaGeneralPrerequisitoRespuestaBO();
				Mapper.Map<TProgramaGeneralPrerequisitoRespuesta, ProgramaGeneralPrerequisitoRespuestaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ProgramaGeneralPrerequisitoRespuestaBO FirstBy(Expression<Func<TProgramaGeneralPrerequisitoRespuesta, bool>> filter)
		{
			try
			{
				TProgramaGeneralPrerequisitoRespuesta entidad = base.FirstBy(filter);
				ProgramaGeneralPrerequisitoRespuestaBO objetoBO = Mapper.Map<TProgramaGeneralPrerequisitoRespuesta, ProgramaGeneralPrerequisitoRespuestaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ProgramaGeneralPrerequisitoRespuestaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TProgramaGeneralPrerequisitoRespuesta entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ProgramaGeneralPrerequisitoRespuestaBO> listadoBO)
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

		public bool Update(ProgramaGeneralPrerequisitoRespuestaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TProgramaGeneralPrerequisitoRespuesta entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ProgramaGeneralPrerequisitoRespuestaBO> listadoBO)
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
		private void AsignacionId(TProgramaGeneralPrerequisitoRespuesta entidad, ProgramaGeneralPrerequisitoRespuestaBO objetoBO)
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

		private TProgramaGeneralPrerequisitoRespuesta MapeoEntidad(ProgramaGeneralPrerequisitoRespuestaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TProgramaGeneralPrerequisitoRespuesta entidad = new TProgramaGeneralPrerequisitoRespuesta();
				entidad = Mapper.Map<ProgramaGeneralPrerequisitoRespuestaBO, TProgramaGeneralPrerequisitoRespuesta>(objetoBO,
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
