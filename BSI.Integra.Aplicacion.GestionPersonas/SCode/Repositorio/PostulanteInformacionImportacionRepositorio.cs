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
	public class PostulanteInformacionImportacionRepositorio : BaseRepository<TPostulanteInformacionImportacion, PostulanteInformacionImportacionBO>
	{
		#region Metodos Base
		public PostulanteInformacionImportacionRepositorio() : base()
		{
		}
		public PostulanteInformacionImportacionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PostulanteInformacionImportacionBO> GetBy(Expression<Func<TPostulanteInformacionImportacion, bool>> filter)
		{
			IEnumerable<TPostulanteInformacionImportacion> listado = base.GetBy(filter);
			List<PostulanteInformacionImportacionBO> listadoBO = new List<PostulanteInformacionImportacionBO>();
			foreach (var itemEntidad in listado)
			{
				PostulanteInformacionImportacionBO objetoBO = Mapper.Map<TPostulanteInformacionImportacion, PostulanteInformacionImportacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PostulanteInformacionImportacionBO FirstById(int id)
		{
			try
			{
				TPostulanteInformacionImportacion entidad = base.FirstById(id);
				PostulanteInformacionImportacionBO objetoBO = new PostulanteInformacionImportacionBO();
				Mapper.Map<TPostulanteInformacionImportacion, PostulanteInformacionImportacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PostulanteInformacionImportacionBO FirstBy(Expression<Func<TPostulanteInformacionImportacion, bool>> filter)
		{
			try
			{
				TPostulanteInformacionImportacion entidad = base.FirstBy(filter);
				PostulanteInformacionImportacionBO objetoBO = Mapper.Map<TPostulanteInformacionImportacion, PostulanteInformacionImportacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PostulanteInformacionImportacionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPostulanteInformacionImportacion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PostulanteInformacionImportacionBO> listadoBO)
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

		public bool Update(PostulanteInformacionImportacionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPostulanteInformacionImportacion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PostulanteInformacionImportacionBO> listadoBO)
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
		private void AsignacionId(TPostulanteInformacionImportacion entidad, PostulanteInformacionImportacionBO objetoBO)
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

		private TPostulanteInformacionImportacion MapeoEntidad(PostulanteInformacionImportacionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPostulanteInformacionImportacion entidad = new TPostulanteInformacionImportacion();
				entidad = Mapper.Map<PostulanteInformacionImportacionBO, TPostulanteInformacionImportacion>(objetoBO,
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
		/// Obtiene lista de registros Cuantitativos de Postulantes Filtrados por Proceso de Seleccion Activos
		/// </summary>
		/// <returns>Lista de Información de Importaciones por Procesos de Seleccion Actuales</returns>
		public List<PostulanteInformacionImportacionDTO> ObtenerRegistrosProcesoSeleccionActivo()
		{
			try
			{
				List<PostulanteInformacionImportacionDTO> listaRegistroImportacion = new List<PostulanteInformacionImportacionDTO>();
				var query = "SELECT Id, IdProcesoSeleccion, ProcesoSeleccion, CantidadTotal, CantidadPrimerCriterio, CantidadSegundoCriterio, CantidadFinal, UsuarioModificacion, FechaModificacion FROM gp.V_ObtenerRegistroImportacionProcesoSeleccion WHERE Estado = 1";
				var res = _dapper.QueryDapper(query, null);

				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					listaRegistroImportacion = JsonConvert.DeserializeObject<List<PostulanteInformacionImportacionDTO>>(res);
				}
				return listaRegistroImportacion;

			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

	}
}
