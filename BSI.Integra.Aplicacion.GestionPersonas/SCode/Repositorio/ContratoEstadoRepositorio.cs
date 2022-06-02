using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
	/// Repositorio: ContratoEstadoRepositorio
	/// Autor: Edgar Serruto
	/// Fecha: 16/06/2021
	/// <summary>
	/// Repositorio para de tabla T_ContratoEstado
	/// </summary>
	public class ContratoEstadoRepositorio : BaseRepository<TContratoEstado, ContratoEstadoBO>
	{
		#region Metodos Base
		public ContratoEstadoRepositorio() : base()
		{
		}
		public ContratoEstadoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ContratoEstadoBO> GetBy(Expression<Func<TContratoEstado, bool>> filter)
		{
			IEnumerable<TContratoEstado> listado = base.GetBy(filter);
			List<ContratoEstadoBO> listadoBO = new List<ContratoEstadoBO>();
			foreach (var itemEntidad in listado)
			{
				ContratoEstadoBO objetoBO = Mapper.Map<TContratoEstado, ContratoEstadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ContratoEstadoBO FirstById(int id)
		{
			try
			{
				TContratoEstado entidad = base.FirstById(id);
				ContratoEstadoBO objetoBO = new ContratoEstadoBO();
				Mapper.Map<TContratoEstado, ContratoEstadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ContratoEstadoBO FirstBy(Expression<Func<TContratoEstado, bool>> filter)
		{
			try
			{
				TContratoEstado entidad = base.FirstBy(filter);
				ContratoEstadoBO objetoBO = Mapper.Map<TContratoEstado, ContratoEstadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ContratoEstadoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TContratoEstado entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ContratoEstadoBO> listadoBO)
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

		public bool Update(ContratoEstadoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TContratoEstado entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ContratoEstadoBO> listadoBO)
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
		private void AsignacionId(TContratoEstado entidad, ContratoEstadoBO objetoBO)
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

		private TContratoEstado MapeoEntidad(ContratoEstadoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TContratoEstado entidad = new TContratoEstado();
				entidad = Mapper.Map<ContratoEstadoBO, TContratoEstado>(objetoBO,
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
		/// Repositorio: ContratoEstadoRepositorio
		/// Autor: Edgar Serruto
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
		/// Repositorio: ContratoEstadoRepositorio
		/// Autor: Luis Huallpa - Edgar Serruto
		/// Fecha: 07/09/2021
		/// <summary>
		/// Obtiene reigstros de Estado de Contrato
		/// </summary>
		/// <returns>List<ContratoEstadoDTO></returns>
		public List<ContratoEstadoDTO> ObtenerContratoEstadoRegistrado()
		{
			try
			{
				return this.GetBy(x => x.Estado == true).Select(x => new ContratoEstadoDTO
				{
					Id = x.Id,
					Nombre = x.Nombre,
				}).ToList(); ;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
