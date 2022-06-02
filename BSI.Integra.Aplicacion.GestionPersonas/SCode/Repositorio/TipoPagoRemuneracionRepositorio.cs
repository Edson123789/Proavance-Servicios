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
	/// Repositorio: TipoPagoRemuneracionRepositorio
	/// Autor: Luis Huallpa
	/// Fecha: 16/06/2021
	/// <summary>
	/// Repositorio para de tabla T_TipoPagoRemuneracion
	/// </summary>
	public class TipoPagoRemuneracionRepositorio : BaseRepository<TTipoPagoRemuneracion, TipoPagoRemuneracionBO>
	{
		#region Metodos Base
		public TipoPagoRemuneracionRepositorio() : base()
		{
		}
		public TipoPagoRemuneracionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<TipoPagoRemuneracionBO> GetBy(Expression<Func<TTipoPagoRemuneracion, bool>> filter)
		{
			IEnumerable<TTipoPagoRemuneracion> listado = base.GetBy(filter);
			List<TipoPagoRemuneracionBO> listadoBO = new List<TipoPagoRemuneracionBO>();
			foreach (var itemEntidad in listado)
			{
				TipoPagoRemuneracionBO objetoBO = Mapper.Map<TTipoPagoRemuneracion, TipoPagoRemuneracionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public TipoPagoRemuneracionBO FirstById(int id)
		{
			try
			{
				TTipoPagoRemuneracion entidad = base.FirstById(id);
				TipoPagoRemuneracionBO objetoBO = new TipoPagoRemuneracionBO();
				Mapper.Map<TTipoPagoRemuneracion, TipoPagoRemuneracionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public TipoPagoRemuneracionBO FirstBy(Expression<Func<TTipoPagoRemuneracion, bool>> filter)
		{
			try
			{
				TTipoPagoRemuneracion entidad = base.FirstBy(filter);
				TipoPagoRemuneracionBO objetoBO = Mapper.Map<TTipoPagoRemuneracion, TipoPagoRemuneracionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(TipoPagoRemuneracionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TTipoPagoRemuneracion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<TipoPagoRemuneracionBO> listadoBO)
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

		public bool Update(TipoPagoRemuneracionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TTipoPagoRemuneracion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<TipoPagoRemuneracionBO> listadoBO)
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
		private void AsignacionId(TTipoPagoRemuneracion entidad, TipoPagoRemuneracionBO objetoBO)
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

		private TTipoPagoRemuneracion MapeoEntidad(TipoPagoRemuneracionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TTipoPagoRemuneracion entidad = new TTipoPagoRemuneracion();
				entidad = Mapper.Map<TipoPagoRemuneracionBO, TTipoPagoRemuneracion>(objetoBO,
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
		/// Repositorio: TipoPagoRemuneracionRepositorio
		/// Autor: 
		/// Fecha: 16/06/2021
		/// <summary>
		/// Obtiene el Id y Nombre para ComboBox
		/// </summary>
		/// <returns> List<FiltroIdNombreDTO> </returns>
		public List<FiltroIdNombreDTO> GetFiltroIdNombre()
		{
			var lista = GetBy(x => true, y => new FiltroIdNombreDTO
			{
				Id = y.Id,
				Nombre = y.Nombre
			}).ToList();
			return lista;
		}
	}
}
