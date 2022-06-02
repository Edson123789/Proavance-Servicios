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
    public class ProgramaGeneralMotivacionRespuestaRepositorio : BaseRepository<TProgramaGeneralMotivacionRespuesta, ProgramaGeneralMotivacionRespuestaBO>
	{
		#region Metodos Base
		public ProgramaGeneralMotivacionRespuestaRepositorio() : base()
		{
		}
		public ProgramaGeneralMotivacionRespuestaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ProgramaGeneralMotivacionRespuestaBO> GetBy(Expression<Func<TProgramaGeneralMotivacionRespuesta, bool>> filter)
		{
			IEnumerable<TProgramaGeneralMotivacionRespuesta> listado = base.GetBy(filter);
			List<ProgramaGeneralMotivacionRespuestaBO> listadoBO = new List<ProgramaGeneralMotivacionRespuestaBO>();
			foreach (var itemEntidad in listado)
			{
				ProgramaGeneralMotivacionRespuestaBO objetoBO = Mapper.Map<TProgramaGeneralMotivacionRespuesta, ProgramaGeneralMotivacionRespuestaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ProgramaGeneralMotivacionRespuestaBO FirstById(int id)
		{
			try
			{
				TProgramaGeneralMotivacionRespuesta entidad = base.FirstById(id);
				ProgramaGeneralMotivacionRespuestaBO objetoBO = new ProgramaGeneralMotivacionRespuestaBO();
				Mapper.Map<TProgramaGeneralMotivacionRespuesta, ProgramaGeneralMotivacionRespuestaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ProgramaGeneralMotivacionRespuestaBO FirstBy(Expression<Func<TProgramaGeneralMotivacionRespuesta, bool>> filter)
		{
			try
			{
				TProgramaGeneralMotivacionRespuesta entidad = base.FirstBy(filter);
				ProgramaGeneralMotivacionRespuestaBO objetoBO = Mapper.Map<TProgramaGeneralMotivacionRespuesta, ProgramaGeneralMotivacionRespuestaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ProgramaGeneralMotivacionRespuestaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TProgramaGeneralMotivacionRespuesta entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ProgramaGeneralMotivacionRespuestaBO> listadoBO)
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

		public bool Update(ProgramaGeneralMotivacionRespuestaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TProgramaGeneralMotivacionRespuesta entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ProgramaGeneralMotivacionRespuestaBO> listadoBO)
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
		private void AsignacionId(TProgramaGeneralMotivacionRespuesta entidad, ProgramaGeneralMotivacionRespuestaBO objetoBO)
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

		private TProgramaGeneralMotivacionRespuesta MapeoEntidad(ProgramaGeneralMotivacionRespuestaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TProgramaGeneralMotivacionRespuesta entidad = new TProgramaGeneralMotivacionRespuesta();
				entidad = Mapper.Map<ProgramaGeneralMotivacionRespuestaBO, TProgramaGeneralMotivacionRespuesta>(objetoBO,
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
