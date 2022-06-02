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
	/// Repositorio: PuestoTrabajoRepositorio
	/// Autor: Luis Huallpa - Edgar S.
	/// Fecha: 29/01/2021
	/// <summary>
	/// Gestión de Puestos de Trabajo y Perfil de Puestos de Trabajo
	/// </summary>
	public class PuestoTrabajoRepositorio : BaseRepository<TPuestoTrabajo, PuestoTrabajoBO>
	{
		#region Metodos Base
		public PuestoTrabajoRepositorio() : base()
		{
		}
		public PuestoTrabajoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PuestoTrabajoBO> GetBy(Expression<Func<TPuestoTrabajo, bool>> filter)
		{
			IEnumerable<TPuestoTrabajo> listado = base.GetBy(filter);
			List<PuestoTrabajoBO> listadoBO = new List<PuestoTrabajoBO>();
			foreach (var itemEntidad in listado)
			{
				PuestoTrabajoBO objetoBO = Mapper.Map<TPuestoTrabajo, PuestoTrabajoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PuestoTrabajoBO FirstById(int id)
		{
			try
			{
				TPuestoTrabajo entidad = base.FirstById(id);
				PuestoTrabajoBO objetoBO = new PuestoTrabajoBO();
				Mapper.Map<TPuestoTrabajo, PuestoTrabajoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PuestoTrabajoBO FirstBy(Expression<Func<TPuestoTrabajo, bool>> filter)
		{
			try
			{
				TPuestoTrabajo entidad = base.FirstBy(filter);
				PuestoTrabajoBO objetoBO = Mapper.Map<TPuestoTrabajo, PuestoTrabajoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PuestoTrabajoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPuestoTrabajo entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PuestoTrabajoBO> listadoBO)
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

		public bool Update(PuestoTrabajoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPuestoTrabajo entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PuestoTrabajoBO> listadoBO)
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
		private void AsignacionId(TPuestoTrabajo entidad, PuestoTrabajoBO objetoBO)
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

		private TPuestoTrabajo MapeoEntidad(PuestoTrabajoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPuestoTrabajo entidad = new TPuestoTrabajo();
				entidad = Mapper.Map<PuestoTrabajoBO, TPuestoTrabajo>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<PuestoTrabajoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TPuestoTrabajo, bool>>> filters, Expression<Func<TPuestoTrabajo, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TPuestoTrabajo> listado = base.GetFiltered(filters, orderBy, ascending);
			List<PuestoTrabajoBO> listadoBO = new List<PuestoTrabajoBO>();

			foreach (var itemEntidad in listado)
			{
				PuestoTrabajoBO objetoBO = Mapper.Map<TPuestoTrabajo, PuestoTrabajoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion
		///Repositorio: PuestoTrabajoRepositorio
		///Autor: Edgar Serruto.
		///Fecha: 25/01/2021
		/// <summary>
		/// Obtiene el Id y Nombre para ComboBox
		/// </summary>
		/// <returns> List<FiltroIdNombreDTO> </returns>
		public List<FiltroIdNombreDTO> GetFiltroIdNombre()
		{
			var lista = GetBy(x => x.Estado == true, y => new FiltroIdNombreDTO
			{
				Id = y.Id,
				Nombre = y.Nombre
			}).ToList();
			return lista;
		}
		///Repositorio: PuestoTrabajoRepositorio
		///Autor: Edgar S.
		///Fecha: 25/01/2021
		/// <summary>
		/// Obtiene lista de puestos de trabajo registrados
		/// </summary>
		/// <returns></returns>
		public List<PuestoTrabajoDTO> ObtenerPuestoTrabajoRegistrado()
		{
			try
			{
				List<PuestoTrabajoDTO> listaPuestoTrabajo = new List<PuestoTrabajoDTO>();
				var query = "SELECT Id, Nombre, IdPersonalAreaTrabajo, PersonalAreaTrabajo, IdPerfilPuestoTrabajo, Objetivo, Descripcion FROM [gp].[V_TPuestoTrabajo_ObtenerPuestoTrabajoRegistrado] WHERE Estado = 1";
				var res = _dapper.QueryDapper(query, null);
				if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
				{
					listaPuestoTrabajo = JsonConvert.DeserializeObject<List<PuestoTrabajoDTO>>(res);
				}
				return listaPuestoTrabajo;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

		}

		///Repositorio: PuestoTrabajoRepositorio
		///Autor: Edgar S.
		///Fecha: 25/01/2021
		/// <summary>
		/// Obtiene lista de puestos de trabajo registrados por Fecha de Modificacion
		/// </summary>
		/// <returns> Devuevle lista de puestos de trabajo, Usuario y Fecha Modificación</returns>
		/// <returns> Lista de Objetos DTO: List<PuestoTrabajoPorFechaDTO> </returns>
		public List<PuestoTrabajoPorFechaDTO> ObtenerPuestoTrabajoRegistradoFechaModificacion()
		{
			try
			{
				List<PuestoTrabajoPorFechaDTO> listaPuestoTrabajo = new List<PuestoTrabajoPorFechaDTO>();
				var query = "SELECT Id, Nombre, IdPersonalAreaTrabajo, PersonalAreaTrabajo, IdPerfilPuestoTrabajo, Objetivo, Descripcion," +
					"PuestoTrabajoFechaModificacion,PuestoTrabajoUsuarioModificacion," +
					"PerfilPuestoTrabajoFechaModificacion,PerfilPuestoTrabajoUsuarioModificacion," +
					"PersonalAreaFechaModificacion,PersonalAreaUsuarioModificacion," +
					"PuestoTrabajoCaracteristicaPersonalFechaModificacion,PuestoTrabajoCaracteristicaPersonalUsuarioModificacion," +
					"PuestoTrabajoCursoComplementarioFechaModificacion,PuestoTrabajoCursoComplementarioUsuarioModificacion," +
					"PuestoTrabajoExperienciaFechaModificacion,PuestoTrabajoExperienciaUsuarioModificacion," +
					"PuestoTrabajoFormacionAcademicaFechaModificacion,PuestoTrabajoFormacionAcademicaUsuarioModificacion," +
					"PuestoTrabajoFuncionFechaModificacion,PuestoTrabajoFuncionUsuarioModificacion," +
					"PuestoTrabajoRelacionFechaModificacion,PuestoTrabajoRelacionUsuarioModificacion," +
					"PuestoTrabajoRelacionDetalleFechaModificacion,PuestoTrabajoRelacionDetalleUsuarioModificacion," +
					"PuestoTrabajoReporteFechaModificacion,PuestoTrabajoReporteUsuarioModificacion," +
					"PuestoTrabajoPuntajeCalificacionFechaModificacion,PuestoTrabajoPuntajeCalificacionUsuarioModificacion, " +
					"ModuloSistemaPuestoTrabajoFechaModificacion,ModuloSistemaPuestoTrabajoUsuarioModificacion " +
					"FROM [gp].[V_TPuestoTrabajo_ObtenerPuestoTrabajoRegistradoFechaModificacion] WHERE Estado = 1";
				var res = _dapper.QueryDapper(query, null);
				if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
				{
					listaPuestoTrabajo = JsonConvert.DeserializeObject<List<PuestoTrabajoPorFechaDTO>>(res);
				}
				return listaPuestoTrabajo;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		///Repositorio: PuestoTrabajoRepositorio
		///Autor: Edgar Serruto.
		///Fecha: 25/01/2021
		/// <summary>
		/// Obtiene registro Id, Nombre para puestos de Trabajo
		/// </summary>
		/// <returns>List<FiltroGenericoDTO></returns>
		public List<FiltroGenericoDTO> ObtenerFiltroPuestoTrabajo()
		{
			try
			{
				return GetBy(x => x.Estado == true, x => new FiltroGenericoDTO { Value = x.Id, Text = x.Nombre }).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
