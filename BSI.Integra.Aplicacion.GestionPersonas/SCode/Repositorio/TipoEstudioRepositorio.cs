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
	/// Repositorio: TipoEstudioRepositorio
	/// Autor: Luis Huallpa.
	/// Fecha: 16/06/2021
	/// <summary>
	/// Repositorio para de tabla T_TipoEstudio
	/// </summary>
	public class TipoEstudioRepositorio : BaseRepository<TTipoEstudio, TipoEstudioBO>
	{
		#region Metodos Base
		public TipoEstudioRepositorio() : base()
		{
		}
		public TipoEstudioRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<TipoEstudioBO> GetBy(Expression<Func<TTipoEstudio, bool>> filter)
		{
			IEnumerable<TTipoEstudio> listado = base.GetBy(filter);
			List<TipoEstudioBO> listadoBO = new List<TipoEstudioBO>();
			foreach (var itemEntidad in listado)
			{
				TipoEstudioBO objetoBO = Mapper.Map<TTipoEstudio, TipoEstudioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public TipoEstudioBO FirstById(int id)
		{
			try
			{
				TTipoEstudio entidad = base.FirstById(id);
				TipoEstudioBO objetoBO = new TipoEstudioBO();
				Mapper.Map<TTipoEstudio, TipoEstudioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public TipoEstudioBO FirstBy(Expression<Func<TTipoEstudio, bool>> filter)
		{
			try
			{
				TTipoEstudio entidad = base.FirstBy(filter);
				TipoEstudioBO objetoBO = Mapper.Map<TTipoEstudio, TipoEstudioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(TipoEstudioBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TTipoEstudio entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<TipoEstudioBO> listadoBO)
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

		public bool Update(TipoEstudioBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TTipoEstudio entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<TipoEstudioBO> listadoBO)
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
		private void AsignacionId(TTipoEstudio entidad, TipoEstudioBO objetoBO)
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

		private TTipoEstudio MapeoEntidad(TipoEstudioBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TTipoEstudio entidad = new TTipoEstudio();
				entidad = Mapper.Map<TipoEstudioBO, TTipoEstudio>(objetoBO,
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
		/// Repositorio: TipoEstudioRepositorio
		/// Autor: 
		/// Fecha: 16/06/2021
		/// <summary>
		/// Obtiene lista de elementos registrados para combo
		/// </summary>
		/// <returns> List<FiltroIdNombreDTO> </returns>
		public List<FiltroIdNombreDTO> ObtenerListaParaFiltro()
		{
			try
			{
				return this.GetBy(x => true).Select(x => new FiltroIdNombreDTO
				{
					Id = x.Id,
					Nombre = x.Nombre
				}).ToList(); ;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
