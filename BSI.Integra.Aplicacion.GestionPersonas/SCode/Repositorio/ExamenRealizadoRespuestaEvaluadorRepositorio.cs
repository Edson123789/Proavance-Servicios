using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
	public class ExamenRealizadoRespuestaEvaluadorRepositorio : BaseRepository<TExamenRealizadoRespuestaEvaluador, ExamenRealizadoRespuestaEvaluadorBO>
	{
		#region Metodos Base
		public ExamenRealizadoRespuestaEvaluadorRepositorio() : base()
		{
		}
		public ExamenRealizadoRespuestaEvaluadorRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ExamenRealizadoRespuestaEvaluadorBO> GetBy(Expression<Func<TExamenRealizadoRespuestaEvaluador, bool>> filter)
		{
			IEnumerable<TExamenRealizadoRespuestaEvaluador> listado = base.GetBy(filter);
			List<ExamenRealizadoRespuestaEvaluadorBO> listadoBO = new List<ExamenRealizadoRespuestaEvaluadorBO>();
			foreach (var itemEntidad in listado)
			{
				ExamenRealizadoRespuestaEvaluadorBO objetoBO = Mapper.Map<TExamenRealizadoRespuestaEvaluador, ExamenRealizadoRespuestaEvaluadorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ExamenRealizadoRespuestaEvaluadorBO FirstById(int id)
		{
			try
			{
				TExamenRealizadoRespuestaEvaluador entidad = base.FirstById(id);
				ExamenRealizadoRespuestaEvaluadorBO objetoBO = new ExamenRealizadoRespuestaEvaluadorBO();
				Mapper.Map<TExamenRealizadoRespuestaEvaluador, ExamenRealizadoRespuestaEvaluadorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ExamenRealizadoRespuestaEvaluadorBO FirstBy(Expression<Func<TExamenRealizadoRespuestaEvaluador, bool>> filter)
		{
			try
			{
				TExamenRealizadoRespuestaEvaluador entidad = base.FirstBy(filter);
				ExamenRealizadoRespuestaEvaluadorBO objetoBO = Mapper.Map<TExamenRealizadoRespuestaEvaluador, ExamenRealizadoRespuestaEvaluadorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ExamenRealizadoRespuestaEvaluadorBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TExamenRealizadoRespuestaEvaluador entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ExamenRealizadoRespuestaEvaluadorBO> listadoBO)
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

		public bool Update(ExamenRealizadoRespuestaEvaluadorBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TExamenRealizadoRespuestaEvaluador entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ExamenRealizadoRespuestaEvaluadorBO> listadoBO)
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
		private void AsignacionId(TExamenRealizadoRespuestaEvaluador entidad, ExamenRealizadoRespuestaEvaluadorBO objetoBO)
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

		private TExamenRealizadoRespuestaEvaluador MapeoEntidad(ExamenRealizadoRespuestaEvaluadorBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TExamenRealizadoRespuestaEvaluador entidad = new TExamenRealizadoRespuestaEvaluador();
				entidad = Mapper.Map<ExamenRealizadoRespuestaEvaluadorBO, TExamenRealizadoRespuestaEvaluador>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<ExamenRealizadoRespuestaEvaluadorBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TExamenRealizadoRespuestaEvaluador, bool>>> filters, Expression<Func<TExamenRealizadoRespuestaEvaluador, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TExamenRealizadoRespuestaEvaluador> listado = base.GetFiltered(filters, orderBy, ascending);
			List<ExamenRealizadoRespuestaEvaluadorBO> listadoBO = new List<ExamenRealizadoRespuestaEvaluadorBO>();

			foreach (var itemEntidad in listado)
			{
				ExamenRealizadoRespuestaEvaluadorBO objetoBO = Mapper.Map<TExamenRealizadoRespuestaEvaluador, ExamenRealizadoRespuestaEvaluadorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion
	}
}
