using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
	/// Repositorio: TipoSangreRepositorio
	/// Autor: Edgar Serruto
	/// Fecha: 16/06/2021
	/// <summary>
	/// Repositorio para de tabla T_TipoSangre
	/// </summary>
	public class TipoSangreRepositorio : BaseRepository<TTipoSangre, TipoSangreBO>
	{
		#region Metodos Base
		public TipoSangreRepositorio() : base()
		{
		}
		public TipoSangreRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<TipoSangreBO> GetBy(Expression<Func<TTipoSangre, bool>> filter)
		{
			IEnumerable<TTipoSangre> listado = base.GetBy(filter);
			List<TipoSangreBO> listadoBO = new List<TipoSangreBO>();
			foreach (var itemEntidad in listado)
			{
				TipoSangreBO objetoBO = Mapper.Map<TTipoSangre, TipoSangreBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public TipoSangreBO FirstById(int id)
		{
			try
			{
				TTipoSangre entidad = base.FirstById(id);
				TipoSangreBO objetoBO = new TipoSangreBO();
				Mapper.Map<TTipoSangre, TipoSangreBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public TipoSangreBO FirstBy(Expression<Func<TTipoSangre, bool>> filter)
		{
			try
			{
				TTipoSangre entidad = base.FirstBy(filter);
				TipoSangreBO objetoBO = Mapper.Map<TTipoSangre, TipoSangreBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(TipoSangreBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TTipoSangre entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<TipoSangreBO> listadoBO)
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

		public bool Update(TipoSangreBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TTipoSangre entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<TipoSangreBO> listadoBO)
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
		private void AsignacionId(TTipoSangre entidad, TipoSangreBO objetoBO)
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

		private TTipoSangre MapeoEntidad(TipoSangreBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TTipoSangre entidad = new TTipoSangre();
				entidad = Mapper.Map<TipoSangreBO, TTipoSangre>(objetoBO,
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
		/// Repositorio: TipoSangreRepositorio 
		/// Autor: Edgar Serruto
		/// Fecha: 16/06/2021
		/// <summary>
		/// Obtiene lista de elementos registrados para combo
		/// </summary>
		/// <returns>List<FiltroIdNombreDTO></returns>
		public List<FiltroIdNombreDTO> ObtenerListaParaFiltro()
		{
			try
			{
				return this.GetBy(x => true).Select(x => new FiltroIdNombreDTO
				{
					Id = x.Id,
					Nombre = x.TipoSangre
				}).ToList(); ;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// Repositorio: TipoSangreRepositorio 
		/// Autor: Edgar Serruto
		/// Fecha: 07/09/2021
		/// <summary>
		/// Obtiene todos los registros de Tipo de Sangre
		/// </summary>
		/// <returns>List<TipoSangreDTO></returns>
		public List<TipoSangreDTO> ObtenerTipoSangreRegistrado()
		{
			try
			{
				return this.GetBy(x => x.Estado == true).Select(x => new TipoSangreDTO
				{
					Id = x.Id,
					TipoSangre = x.TipoSangre,
					Comentario = x.Comentario
				}).ToList(); ;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
