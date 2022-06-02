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
	public class RaTipoCursoRepositorio : BaseRepository<TRaTipoCurso, RaTipoCursoBO>
	{
		#region Metodos Base
		public RaTipoCursoRepositorio() : base()
		{
		}
		public RaTipoCursoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<RaTipoCursoBO> GetBy(Expression<Func<TRaTipoCurso, bool>> filter)
		{
			IEnumerable<TRaTipoCurso> listado = base.GetBy(filter);
			List<RaTipoCursoBO> listadoBO = new List<RaTipoCursoBO>();
			foreach (var itemEntidad in listado)
			{
				RaTipoCursoBO objetoBO = Mapper.Map<TRaTipoCurso, RaTipoCursoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public RaTipoCursoBO FirstById(int id)
		{
			try
			{
				TRaTipoCurso entidad = base.FirstById(id);
				RaTipoCursoBO objetoBO = new RaTipoCursoBO();
				Mapper.Map<TRaTipoCurso, RaTipoCursoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public RaTipoCursoBO FirstBy(Expression<Func<TRaTipoCurso, bool>> filter)
		{
			try
			{
				TRaTipoCurso entidad = base.FirstBy(filter);
				RaTipoCursoBO objetoBO = Mapper.Map<TRaTipoCurso, RaTipoCursoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(RaTipoCursoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TRaTipoCurso entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<RaTipoCursoBO> listadoBO)
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

		public bool Update(RaTipoCursoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TRaTipoCurso entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<RaTipoCursoBO> listadoBO)
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
		private void AsignacionId(TRaTipoCurso entidad, RaTipoCursoBO objetoBO)
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

		private TRaTipoCurso MapeoEntidad(RaTipoCursoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TRaTipoCurso entidad = new TRaTipoCurso();
				entidad = Mapper.Map<RaTipoCursoBO, TRaTipoCurso>(objetoBO,
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

		public List<FiltroGenericoDTO> ObtenerFiltroTipoCurso()
		{
			try
			{
				return GetBy(x => x.Estado == true, x => new FiltroGenericoDTO { Value = x.Id, Text= x.Nombre }).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
