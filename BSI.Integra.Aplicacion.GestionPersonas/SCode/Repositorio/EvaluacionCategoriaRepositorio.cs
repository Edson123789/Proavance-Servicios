using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
	public class EvaluacionCategoriaRepositorio : BaseRepository<TEvaluacionCategoria, EvaluacionCategoriaBO>
	{
		#region Metodos Base
		public EvaluacionCategoriaRepositorio() : base()
		{
		}
		public EvaluacionCategoriaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<EvaluacionCategoriaBO> GetBy(Expression<Func<TEvaluacionCategoria, bool>> filter)
		{
			IEnumerable<TEvaluacionCategoria> listado = base.GetBy(filter);
			List<EvaluacionCategoriaBO> listadoBO = new List<EvaluacionCategoriaBO>();
			foreach (var itemEntidad in listado)
			{
				EvaluacionCategoriaBO objetoBO = Mapper.Map<TEvaluacionCategoria, EvaluacionCategoriaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public EvaluacionCategoriaBO FirstById(int id)
		{
			try
			{
				TEvaluacionCategoria entidad = base.FirstById(id);
				EvaluacionCategoriaBO objetoBO = new EvaluacionCategoriaBO();
				Mapper.Map<TEvaluacionCategoria, EvaluacionCategoriaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public EvaluacionCategoriaBO FirstBy(Expression<Func<TEvaluacionCategoria, bool>> filter)
		{
			try
			{
				TEvaluacionCategoria entidad = base.FirstBy(filter);
				EvaluacionCategoriaBO objetoBO = Mapper.Map<TEvaluacionCategoria, EvaluacionCategoriaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(EvaluacionCategoriaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TEvaluacionCategoria entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<EvaluacionCategoriaBO> listadoBO)
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

		public bool Update(EvaluacionCategoriaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TEvaluacionCategoria entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<EvaluacionCategoriaBO> listadoBO)
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
		private void AsignacionId(TEvaluacionCategoria entidad, EvaluacionCategoriaBO objetoBO)
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

		private TEvaluacionCategoria MapeoEntidad(EvaluacionCategoriaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TEvaluacionCategoria entidad = new TEvaluacionCategoria();
				entidad = Mapper.Map<EvaluacionCategoriaBO, TEvaluacionCategoria>(objetoBO,
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

		/// <summary>
		/// Obtiene lista de categoria de Evaluacions registradas
		/// </summary>
		/// <returns></returns>
		public List<FiltroDTO> ObtenerCategoriasEvaluacionRegistradas()
		{
			try
			{
				return this.GetBy(x => x.Estado == true).Select(x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
