using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.Operaciones.SCode.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
	public class TipoContratoRepositorio : BaseRepository<TTipoContrato, TipoContratoBO>
	{
		#region Metodos Base
		public TipoContratoRepositorio() : base()
		{
		}
		public TipoContratoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<TipoContratoBO> GetBy(Expression<Func<TTipoContrato, bool>> filter)
		{
			IEnumerable<TTipoContrato> listado = base.GetBy(filter);
			List<TipoContratoBO> listadoBO = new List<TipoContratoBO>();
			foreach (var itemEntidad in listado)
			{
				TipoContratoBO objetoBO = Mapper.Map<TTipoContrato, TipoContratoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public TipoContratoBO FirstById(int id)
		{
			try
			{
				TTipoContrato entidad = base.FirstById(id);
				TipoContratoBO objetoBO = new TipoContratoBO();
				Mapper.Map<TTipoContrato, TipoContratoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public TipoContratoBO FirstBy(Expression<Func<TTipoContrato, bool>> filter)
		{
			try
			{
				TTipoContrato entidad = base.FirstBy(filter);
				TipoContratoBO objetoBO = Mapper.Map<TTipoContrato, TipoContratoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(TipoContratoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TTipoContrato entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<TipoContratoBO> listadoBO)
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

		public bool Update(TipoContratoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TTipoContrato entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<TipoContratoBO> listadoBO)
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
		private void AsignacionId(TTipoContrato entidad, TipoContratoBO objetoBO)
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

		private TTipoContrato MapeoEntidad(TipoContratoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TTipoContrato entidad = new TTipoContrato();
				entidad = Mapper.Map<TipoContratoBO, TTipoContrato>(objetoBO,
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
