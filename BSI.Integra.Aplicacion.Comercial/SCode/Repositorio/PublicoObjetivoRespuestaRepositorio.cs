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
    public class PublicoObjetivoRespuestaRepositorio : BaseRepository<TPublicoObjetivoRespuesta, PublicoObjetivoRespuestaBO>
	{
		#region Metodos Base
		public PublicoObjetivoRespuestaRepositorio() : base()
		{
		}
		public PublicoObjetivoRespuestaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PublicoObjetivoRespuestaBO> GetBy(Expression<Func<TPublicoObjetivoRespuesta, bool>> filter)
		{
			IEnumerable<TPublicoObjetivoRespuesta> listado = base.GetBy(filter);
			List<PublicoObjetivoRespuestaBO> listadoBO = new List<PublicoObjetivoRespuestaBO>();
			foreach (var itemEntidad in listado)
			{
				PublicoObjetivoRespuestaBO objetoBO = Mapper.Map<TPublicoObjetivoRespuesta, PublicoObjetivoRespuestaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PublicoObjetivoRespuestaBO FirstById(int id)
		{
			try
			{
				TPublicoObjetivoRespuesta entidad = base.FirstById(id);
				PublicoObjetivoRespuestaBO objetoBO = new PublicoObjetivoRespuestaBO();
				Mapper.Map<TPublicoObjetivoRespuesta, PublicoObjetivoRespuestaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PublicoObjetivoRespuestaBO FirstBy(Expression<Func<TPublicoObjetivoRespuesta, bool>> filter)
		{
			try
			{
				TPublicoObjetivoRespuesta entidad = base.FirstBy(filter);
				PublicoObjetivoRespuestaBO objetoBO = Mapper.Map<TPublicoObjetivoRespuesta, PublicoObjetivoRespuestaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PublicoObjetivoRespuestaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPublicoObjetivoRespuesta entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PublicoObjetivoRespuestaBO> listadoBO)
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

		public bool Update(PublicoObjetivoRespuestaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPublicoObjetivoRespuesta entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PublicoObjetivoRespuestaBO> listadoBO)
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
		private void AsignacionId(TPublicoObjetivoRespuesta entidad, PublicoObjetivoRespuestaBO objetoBO)
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

		private TPublicoObjetivoRespuesta MapeoEntidad(PublicoObjetivoRespuestaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPublicoObjetivoRespuesta entidad = new TPublicoObjetivoRespuesta();
				entidad = Mapper.Map<PublicoObjetivoRespuestaBO, TPublicoObjetivoRespuesta>(objetoBO,
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
