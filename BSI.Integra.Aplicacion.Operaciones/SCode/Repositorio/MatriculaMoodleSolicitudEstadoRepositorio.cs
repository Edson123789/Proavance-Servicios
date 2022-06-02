using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
	public class MatriculaMoodleSolicitudEstadoRepositorio : BaseRepository<TMatriculaMoodleSolicitudEstado, MatriculaMoodleSolicitudEstadoBO>
	{
		#region Metodos Base
		public MatriculaMoodleSolicitudEstadoRepositorio() : base()
		{
		}
			public MatriculaMoodleSolicitudEstadoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<MatriculaMoodleSolicitudEstadoBO> GetBy(Expression<Func<TMatriculaMoodleSolicitudEstado, bool>> filter)
		{
			IEnumerable<TMatriculaMoodleSolicitudEstado> listado = base.GetBy(filter);
			List<MatriculaMoodleSolicitudEstadoBO> listadoBO = new List<MatriculaMoodleSolicitudEstadoBO>();
			foreach (var itemEntidad in listado)
			{
				MatriculaMoodleSolicitudEstadoBO objetoBO = Mapper.Map<TMatriculaMoodleSolicitudEstado, MatriculaMoodleSolicitudEstadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public MatriculaMoodleSolicitudEstadoBO FirstById(int id)
		{
			try
			{
				TMatriculaMoodleSolicitudEstado entidad = base.FirstById(id);
				MatriculaMoodleSolicitudEstadoBO objetoBO = new MatriculaMoodleSolicitudEstadoBO();
				Mapper.Map<TMatriculaMoodleSolicitudEstado, MatriculaMoodleSolicitudEstadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public MatriculaMoodleSolicitudEstadoBO FirstBy(Expression<Func<TMatriculaMoodleSolicitudEstado, bool>> filter)
		{
			try
			{
				TMatriculaMoodleSolicitudEstado entidad = base.FirstBy(filter);
				MatriculaMoodleSolicitudEstadoBO objetoBO = Mapper.Map<TMatriculaMoodleSolicitudEstado, MatriculaMoodleSolicitudEstadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(MatriculaMoodleSolicitudEstadoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TMatriculaMoodleSolicitudEstado entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<MatriculaMoodleSolicitudEstadoBO> listadoBO)
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

		public bool Update(MatriculaMoodleSolicitudEstadoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TMatriculaMoodleSolicitudEstado entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<MatriculaMoodleSolicitudEstadoBO> listadoBO)
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
		private void AsignacionId(TMatriculaMoodleSolicitudEstado entidad, MatriculaMoodleSolicitudEstadoBO objetoBO)
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

		private TMatriculaMoodleSolicitudEstado MapeoEntidad(MatriculaMoodleSolicitudEstadoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TMatriculaMoodleSolicitudEstado entidad = new TMatriculaMoodleSolicitudEstado();
				entidad = Mapper.Map<MatriculaMoodleSolicitudEstadoBO, TMatriculaMoodleSolicitudEstado>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<MatriculaMoodleSolicitudEstadoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TMatriculaMoodleSolicitudEstado, bool>>> filters, Expression<Func<TMatriculaMoodleSolicitudEstado, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TMatriculaMoodleSolicitudEstado> listado = base.GetFiltered(filters, orderBy, ascending);
			List<MatriculaMoodleSolicitudEstadoBO> listadoBO = new List<MatriculaMoodleSolicitudEstadoBO>();

			foreach (var itemEntidad in listado)
			{
				MatriculaMoodleSolicitudEstadoBO objetoBO = Mapper.Map<TMatriculaMoodleSolicitudEstado, MatriculaMoodleSolicitudEstadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion

	}
}
