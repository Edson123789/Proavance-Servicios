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
    public class ProgramaGeneralProblemaDetalleSolucionRespuestaRepositorio : BaseRepository<TProgramaGeneralProblemaDetalleSolucionRespuesta, ProgramaGeneralProblemaDetalleSolucionRespuestaBO>
	{
		#region Metodos Base
		public ProgramaGeneralProblemaDetalleSolucionRespuestaRepositorio() : base()
		{
		}
		public ProgramaGeneralProblemaDetalleSolucionRespuestaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ProgramaGeneralProblemaDetalleSolucionRespuestaBO> GetBy(Expression<Func<TProgramaGeneralProblemaDetalleSolucionRespuesta, bool>> filter)
		{
			IEnumerable<TProgramaGeneralProblemaDetalleSolucionRespuesta> listado = base.GetBy(filter);
			List<ProgramaGeneralProblemaDetalleSolucionRespuestaBO> listadoBO = new List<ProgramaGeneralProblemaDetalleSolucionRespuestaBO>();
			foreach (var itemEntidad in listado)
			{
				ProgramaGeneralProblemaDetalleSolucionRespuestaBO objetoBO = Mapper.Map<TProgramaGeneralProblemaDetalleSolucionRespuesta, ProgramaGeneralProblemaDetalleSolucionRespuestaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ProgramaGeneralProblemaDetalleSolucionRespuestaBO FirstById(int id)
		{
			try
			{
				TProgramaGeneralProblemaDetalleSolucionRespuesta entidad = base.FirstById(id);
				ProgramaGeneralProblemaDetalleSolucionRespuestaBO objetoBO = new ProgramaGeneralProblemaDetalleSolucionRespuestaBO();
				Mapper.Map<TProgramaGeneralProblemaDetalleSolucionRespuesta, ProgramaGeneralProblemaDetalleSolucionRespuestaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ProgramaGeneralProblemaDetalleSolucionRespuestaBO FirstBy(Expression<Func<TProgramaGeneralProblemaDetalleSolucionRespuesta, bool>> filter)
		{
			try
			{
				TProgramaGeneralProblemaDetalleSolucionRespuesta entidad = base.FirstBy(filter);
				ProgramaGeneralProblemaDetalleSolucionRespuestaBO objetoBO = Mapper.Map<TProgramaGeneralProblemaDetalleSolucionRespuesta, ProgramaGeneralProblemaDetalleSolucionRespuestaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ProgramaGeneralProblemaDetalleSolucionRespuestaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TProgramaGeneralProblemaDetalleSolucionRespuesta entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ProgramaGeneralProblemaDetalleSolucionRespuestaBO> listadoBO)
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

		public bool Update(ProgramaGeneralProblemaDetalleSolucionRespuestaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TProgramaGeneralProblemaDetalleSolucionRespuesta entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ProgramaGeneralProblemaDetalleSolucionRespuestaBO> listadoBO)
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
		private void AsignacionId(TProgramaGeneralProblemaDetalleSolucionRespuesta entidad, ProgramaGeneralProblemaDetalleSolucionRespuestaBO objetoBO)
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

		private TProgramaGeneralProblemaDetalleSolucionRespuesta MapeoEntidad(ProgramaGeneralProblemaDetalleSolucionRespuestaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TProgramaGeneralProblemaDetalleSolucionRespuesta entidad = new TProgramaGeneralProblemaDetalleSolucionRespuesta();
				entidad = Mapper.Map<ProgramaGeneralProblemaDetalleSolucionRespuestaBO, TProgramaGeneralProblemaDetalleSolucionRespuesta>(objetoBO,
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
