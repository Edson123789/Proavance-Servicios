using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
	public class TipoPerfilRepositorio : BaseRepository<TTipoPerfil, TipoPerfilBO>
	{
		#region Metodos Base
		public TipoPerfilRepositorio() : base()
		{
		}
		public TipoPerfilRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<TipoPerfilBO> GetBy(Expression<Func<TTipoPerfil, bool>> filter)
		{
			IEnumerable<TTipoPerfil> listado = base.GetBy(filter);
			List<TipoPerfilBO> listadoBO = new List<TipoPerfilBO>();
			foreach (var itemEntidad in listado)
			{
				TipoPerfilBO objetoBO = Mapper.Map<TTipoPerfil, TipoPerfilBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public TipoPerfilBO FirstById(int id)
		{
			try
			{
				TTipoPerfil entidad = base.FirstById(id);
				TipoPerfilBO objetoBO = new TipoPerfilBO();
				Mapper.Map<TTipoPerfil, TipoPerfilBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public TipoPerfilBO FirstBy(Expression<Func<TTipoPerfil, bool>> filter)
		{
			try
			{
				TTipoPerfil entidad = base.FirstBy(filter);
				TipoPerfilBO objetoBO = Mapper.Map<TTipoPerfil, TipoPerfilBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(TipoPerfilBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TTipoPerfil entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<TipoPerfilBO> listadoBO)
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

		public bool Update(TipoPerfilBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TTipoPerfil entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<TipoPerfilBO> listadoBO)
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
		private void AsignacionId(TTipoPerfil entidad, TipoPerfilBO objetoBO)
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

		private TTipoPerfil MapeoEntidad(TipoPerfilBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TTipoPerfil entidad = new TTipoPerfil();
				entidad = Mapper.Map<TipoPerfilBO, TTipoPerfil>(objetoBO,
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
		/// Obtiene lista de elementos registrados para combo
		/// </summary>
		/// <returns></returns>
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
