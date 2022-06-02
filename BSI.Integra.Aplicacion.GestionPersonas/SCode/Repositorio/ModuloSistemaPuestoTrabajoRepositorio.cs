///Repositorio: ModuloSistemaPuestoTrabajo
///Autor: Edgar S.
///Fecha: 19/01/2021
///<summary>
///Repositorio de T_ModuloSistemaPuestoTrabajo
///</summary>

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
	/// Repositorio: ModuloSistemaPuestoTrabajoRepositorio
	/// Autor: Edgar Serruto
	/// Fecha: 16/06/2021
	/// <summary>
	/// Repositorio para de tabla T_ModuloSistemaPuestoTrabajo
	/// </summary>
	public class ModuloSistemaPuestoTrabajoRepositorio : BaseRepository<TModuloSistemaPuestoTrabajo, ModuloSistemaPuestoTrabajoBO>
	{
		#region Metodos Base
		public ModuloSistemaPuestoTrabajoRepositorio() : base()
		{
		}
		public ModuloSistemaPuestoTrabajoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ModuloSistemaPuestoTrabajoBO> GetBy(Expression<Func<TModuloSistemaPuestoTrabajo, bool>> filter)
		{
			IEnumerable<TModuloSistemaPuestoTrabajo> listado = base.GetBy(filter);
			List<ModuloSistemaPuestoTrabajoBO> listadoBO = new List<ModuloSistemaPuestoTrabajoBO>();
			foreach (var itemEntidad in listado)
			{
				ModuloSistemaPuestoTrabajoBO objetoBO = Mapper.Map<TModuloSistemaPuestoTrabajo, ModuloSistemaPuestoTrabajoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ModuloSistemaPuestoTrabajoBO FirstById(int id)
		{
			try
			{
				TModuloSistemaPuestoTrabajo entidad = base.FirstById(id);
				ModuloSistemaPuestoTrabajoBO objetoBO = new ModuloSistemaPuestoTrabajoBO();
				Mapper.Map<TModuloSistemaPuestoTrabajo, ModuloSistemaPuestoTrabajoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ModuloSistemaPuestoTrabajoBO FirstBy(Expression<Func<TModuloSistemaPuestoTrabajo, bool>> filter)
		{
			try
			{
				TModuloSistemaPuestoTrabajo entidad = base.FirstBy(filter);
				ModuloSistemaPuestoTrabajoBO objetoBO = Mapper.Map<TModuloSistemaPuestoTrabajo, ModuloSistemaPuestoTrabajoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ModuloSistemaPuestoTrabajoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TModuloSistemaPuestoTrabajo entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ModuloSistemaPuestoTrabajoBO> listadoBO)
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

		public bool Update(ModuloSistemaPuestoTrabajoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TModuloSistemaPuestoTrabajo entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ModuloSistemaPuestoTrabajoBO> listadoBO)
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
		private void AsignacionId(TModuloSistemaPuestoTrabajo entidad, ModuloSistemaPuestoTrabajoBO objetoBO)
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

		private TModuloSistemaPuestoTrabajo MapeoEntidad(ModuloSistemaPuestoTrabajoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TModuloSistemaPuestoTrabajo entidad = new TModuloSistemaPuestoTrabajo();
				entidad = Mapper.Map<ModuloSistemaPuestoTrabajoBO, TModuloSistemaPuestoTrabajo>(objetoBO,
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
	}
}
