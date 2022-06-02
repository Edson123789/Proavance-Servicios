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
	/// Repositorio: MotivoInactividadRepositorio
	/// Autor: Edgar Serruto.
	/// Fecha: 18/03/2021
	/// <summary>
	/// Gestión de Motivo de Inactividad
	/// </summary>
	public class MotivoInactividadRepositorio : BaseRepository<TMotivoInactividad, MotivoInactividadBO>
	{
		#region Metodos Base
		public MotivoInactividadRepositorio() : base()
		{
		}
		public MotivoInactividadRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<MotivoInactividadBO> GetBy(Expression<Func<TMotivoInactividad, bool>> filter)
		{
			IEnumerable<TMotivoInactividad> listado = base.GetBy(filter);
			List<MotivoInactividadBO> listadoBO = new List<MotivoInactividadBO>();
			foreach (var itemEntidad in listado)
			{
				MotivoInactividadBO objetoBO = Mapper.Map<TMotivoInactividad, MotivoInactividadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public MotivoInactividadBO FirstById(int id)
		{
			try
			{
				TMotivoInactividad entidad = base.FirstById(id);
				MotivoInactividadBO objetoBO = new MotivoInactividadBO();
				Mapper.Map<TMotivoInactividad, MotivoInactividadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public MotivoInactividadBO FirstBy(Expression<Func<TMotivoInactividad, bool>> filter)
		{
			try
			{
				TMotivoInactividad entidad = base.FirstBy(filter);
				MotivoInactividadBO objetoBO = Mapper.Map<TMotivoInactividad, MotivoInactividadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(MotivoInactividadBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TMotivoInactividad entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<MotivoInactividadBO> listadoBO)
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

		public bool Update(MotivoInactividadBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TMotivoInactividad entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<MotivoInactividadBO> listadoBO)
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
		private void AsignacionId(TMotivoInactividad entidad, MotivoInactividadBO objetoBO)
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

		private TMotivoInactividad MapeoEntidad(MotivoInactividadBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TMotivoInactividad entidad = new TMotivoInactividad();
				entidad = Mapper.Map<MotivoInactividadBO, TMotivoInactividad>(objetoBO,
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
		///Repositorio: MotivoInactividadRepositorio
		///Autor: Edgar S.
		///Fecha: 18/03/2021
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
