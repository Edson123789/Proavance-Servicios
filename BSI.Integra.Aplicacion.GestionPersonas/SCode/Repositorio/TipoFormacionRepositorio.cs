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
	/// Repositorio: TipoFormacionRepositorio
	/// Autor: Edgar S.
	/// Fecha: 29/01/2021
	/// <summary>
	/// Gestión de Tipo de Formación
	/// </summary>
	public class TipoFormacionRepositorio : BaseRepository<TTipoFormacion, TipoFormacionBO>
	{
		#region Metodos Base
		public TipoFormacionRepositorio() : base()
		{
		}
		public TipoFormacionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<TipoFormacionBO> GetBy(Expression<Func<TTipoFormacion, bool>> filter)
		{
			IEnumerable<TTipoFormacion> listado = base.GetBy(filter);
			List<TipoFormacionBO> listadoBO = new List<TipoFormacionBO>();
			foreach (var itemEntidad in listado)
			{
				TipoFormacionBO objetoBO = Mapper.Map<TTipoFormacion, TipoFormacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public TipoFormacionBO FirstById(int id)
		{
			try
			{
				TTipoFormacion entidad = base.FirstById(id);
				TipoFormacionBO objetoBO = new TipoFormacionBO();
				Mapper.Map<TTipoFormacion, TipoFormacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public TipoFormacionBO FirstBy(Expression<Func<TTipoFormacion, bool>> filter)
		{
			try
			{
				TTipoFormacion entidad = base.FirstBy(filter);
				TipoFormacionBO objetoBO = Mapper.Map<TTipoFormacion, TipoFormacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(TipoFormacionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TTipoFormacion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<TipoFormacionBO> listadoBO)
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

		public bool Update(TipoFormacionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TTipoFormacion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<TipoFormacionBO> listadoBO)
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
		private void AsignacionId(TTipoFormacion entidad, TipoFormacionBO objetoBO)
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

		private TTipoFormacion MapeoEntidad(TipoFormacionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TTipoFormacion entidad = new TTipoFormacion();
				entidad = Mapper.Map<TipoFormacionBO, TTipoFormacion>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<TipoFormacionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TTipoFormacion, bool>>> filters, Expression<Func<TTipoFormacion, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TTipoFormacion> listado = base.GetFiltered(filters, orderBy, ascending);
			List<TipoFormacionBO> listadoBO = new List<TipoFormacionBO>();

			foreach (var itemEntidad in listado)
			{
				TipoFormacionBO objetoBO = Mapper.Map<TTipoFormacion, TipoFormacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion
		/// Repositorio: TipoFormacionRepositorio
		/// Autor: Britsel Calluchi - Luis Huallpa.
		/// Fecha: 29/01/2021
		/// <summary>
		/// Obtiene lista de tipo de formación académica
		/// </summary>
		/// <returns>List<TipoFormacionAcademicaDTO></returns>
		public List<TipoFormacionAcademicaDTO> ObtenerTipoFormacion()
		{
			try
			{
				return this.GetBy(x => true).Select(x => new TipoFormacionAcademicaDTO
				{
					IdTipoFormacion = x.Id,
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
