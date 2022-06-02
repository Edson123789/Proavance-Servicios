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
    public class ProgramaGeneralBeneficioRespuestaRepositorio : BaseRepository<TProgramaGeneralBeneficioRespuesta, ProgramaGeneralBeneficioRespuestaBO>
	{
		#region Metodos Base
		public ProgramaGeneralBeneficioRespuestaRepositorio() : base()
		{
		}
		public ProgramaGeneralBeneficioRespuestaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ProgramaGeneralBeneficioRespuestaBO> GetBy(Expression<Func<TProgramaGeneralBeneficioRespuesta, bool>> filter)
		{
			IEnumerable<TProgramaGeneralBeneficioRespuesta> listado = base.GetBy(filter);
			List<ProgramaGeneralBeneficioRespuestaBO> listadoBO = new List<ProgramaGeneralBeneficioRespuestaBO>();
			foreach (var itemEntidad in listado)
			{
				ProgramaGeneralBeneficioRespuestaBO objetoBO = Mapper.Map<TProgramaGeneralBeneficioRespuesta, ProgramaGeneralBeneficioRespuestaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ProgramaGeneralBeneficioRespuestaBO FirstById(int id)
		{
			try
			{
				TProgramaGeneralBeneficioRespuesta entidad = base.FirstById(id);
				ProgramaGeneralBeneficioRespuestaBO objetoBO = new ProgramaGeneralBeneficioRespuestaBO();
				Mapper.Map<TProgramaGeneralBeneficioRespuesta, ProgramaGeneralBeneficioRespuestaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ProgramaGeneralBeneficioRespuestaBO FirstBy(Expression<Func<TProgramaGeneralBeneficioRespuesta, bool>> filter)
		{
			try
			{
				TProgramaGeneralBeneficioRespuesta entidad = base.FirstBy(filter);
				ProgramaGeneralBeneficioRespuestaBO objetoBO = Mapper.Map<TProgramaGeneralBeneficioRespuesta, ProgramaGeneralBeneficioRespuestaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ProgramaGeneralBeneficioRespuestaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TProgramaGeneralBeneficioRespuesta entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ProgramaGeneralBeneficioRespuestaBO> listadoBO)
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

		public bool Update(ProgramaGeneralBeneficioRespuestaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TProgramaGeneralBeneficioRespuesta entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ProgramaGeneralBeneficioRespuestaBO> listadoBO)
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
		private void AsignacionId(TProgramaGeneralBeneficioRespuesta entidad, ProgramaGeneralBeneficioRespuestaBO objetoBO)
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

		private TProgramaGeneralBeneficioRespuesta MapeoEntidad(ProgramaGeneralBeneficioRespuestaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TProgramaGeneralBeneficioRespuesta entidad = new TProgramaGeneralBeneficioRespuesta();
				entidad = Mapper.Map<ProgramaGeneralBeneficioRespuestaBO, TProgramaGeneralBeneficioRespuesta>(objetoBO,
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
