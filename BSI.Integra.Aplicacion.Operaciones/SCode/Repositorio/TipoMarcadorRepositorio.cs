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
	/// Repositorio: Operaciones/TipoMarcador
    /// Autor: Luis Huallpa - Gian Miranda
    /// Fecha: 22/02/2021
    /// <summary>
    /// Repositorio para consultas de ope.T_TipoMarcador
    /// </summary>
	public class TipoMarcadorRepositorio : BaseRepository<TTipoMarcador, TipoMarcadorBO>
	{
		#region Metodos Base
		public TipoMarcadorRepositorio() : base()
		{
		}
		public TipoMarcadorRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<TipoMarcadorBO> GetBy(Expression<Func<TTipoMarcador, bool>> filter)
		{
			IEnumerable<TTipoMarcador> listado = base.GetBy(filter);
			List<TipoMarcadorBO> listadoBO = new List<TipoMarcadorBO>();
			foreach (var itemEntidad in listado)
			{
				TipoMarcadorBO objetoBO = Mapper.Map<TTipoMarcador, TipoMarcadorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public TipoMarcadorBO FirstById(int id)
		{
			try
			{
				TTipoMarcador entidad = base.FirstById(id);
				TipoMarcadorBO objetoBO = new TipoMarcadorBO();
				Mapper.Map<TTipoMarcador, TipoMarcadorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public TipoMarcadorBO FirstBy(Expression<Func<TTipoMarcador, bool>> filter)
		{
			try
			{
				TTipoMarcador entidad = base.FirstBy(filter);
				TipoMarcadorBO objetoBO = Mapper.Map<TTipoMarcador, TipoMarcadorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(TipoMarcadorBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TTipoMarcador entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<TipoMarcadorBO> listadoBO)
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

		public bool Update(TipoMarcadorBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TTipoMarcador entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<TipoMarcadorBO> listadoBO)
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
		private void AsignacionId(TTipoMarcador entidad, TipoMarcadorBO objetoBO)
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

		private TTipoMarcador MapeoEntidad(TipoMarcadorBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TTipoMarcador entidad = new TTipoMarcador();
				entidad = Mapper.Map<TipoMarcadorBO, TTipoMarcador>(objetoBO,
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
		/// Obtiene tipo marcador para combo
		/// </summary>
		/// <returns>Lista de objetos de tipo FiltroDTO con los tipos de marcadores</returns>
		public List<FiltroDTO> ObtenerTipoMarcador()
		{
			try
			{
				return this.GetBy(x => x.Estado == true).Select(x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList(); ;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

	}
}
