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
	public class CursoInformaticaRepositorio : BaseRepository<TCursoInformatica, CursoInformaticaBO>
	{
		#region Metodos Base
		public CursoInformaticaRepositorio() : base()
		{
		}
		public CursoInformaticaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<CursoInformaticaBO> GetBy(Expression<Func<TCursoInformatica, bool>> filter)
		{
			IEnumerable<TCursoInformatica> listado = base.GetBy(filter);
			List<CursoInformaticaBO> listadoBO = new List<CursoInformaticaBO>();
			foreach (var itemEntidad in listado)
			{
				CursoInformaticaBO objetoBO = Mapper.Map<TCursoInformatica, CursoInformaticaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public CursoInformaticaBO FirstById(int id)
		{
			try
			{
				TCursoInformatica entidad = base.FirstById(id);
				CursoInformaticaBO objetoBO = new CursoInformaticaBO();
				Mapper.Map<TCursoInformatica, CursoInformaticaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public CursoInformaticaBO FirstBy(Expression<Func<TCursoInformatica, bool>> filter)
		{
			try
			{
				TCursoInformatica entidad = base.FirstBy(filter);
				CursoInformaticaBO objetoBO = Mapper.Map<TCursoInformatica, CursoInformaticaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(CursoInformaticaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TCursoInformatica entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<CursoInformaticaBO> listadoBO)
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

		public bool Update(CursoInformaticaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TCursoInformatica entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<CursoInformaticaBO> listadoBO)
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
		private void AsignacionId(TCursoInformatica entidad, CursoInformaticaBO objetoBO)
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

		private TCursoInformatica MapeoEntidad(CursoInformaticaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TCursoInformatica entidad = new TCursoInformatica();
				entidad = Mapper.Map<CursoInformaticaBO, TCursoInformatica>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<CursoInformaticaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TCursoInformatica, bool>>> filters, Expression<Func<TCursoInformatica, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TCursoInformatica> listado = base.GetFiltered(filters, orderBy, ascending);
			List<CursoInformaticaBO> listadoBO = new List<CursoInformaticaBO>();

			foreach (var itemEntidad in listado)
			{
				CursoInformaticaBO objetoBO = Mapper.Map<TCursoInformatica, CursoInformaticaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion

		/// <summary>
		/// Obtiene lista de cursos de informatica registrados
		/// </summary>
		/// <returns></returns>
		public List<CursoInformaticaDTO> ObtenerCursoInformatica()
		{
			try
			{
				return this.GetBy(x => true).Select(x => new CursoInformaticaDTO
				{
					Id = x.Id,
					Nombre = x.Nombre
				}).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
