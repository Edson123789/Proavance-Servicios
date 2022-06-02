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
	public class ProcesoSeleccionRepositorio : BaseRepository<TProcesoSeleccion, ProcesoSeleccionBO>
	{
		#region Metodos Base
		public ProcesoSeleccionRepositorio() : base()
		{
		}
		public ProcesoSeleccionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ProcesoSeleccionBO> GetBy(Expression<Func<TProcesoSeleccion, bool>> filter)
		{
			IEnumerable<TProcesoSeleccion> listado = base.GetBy(filter);
			List<ProcesoSeleccionBO> listadoBO = new List<ProcesoSeleccionBO>();
			foreach (var itemEntidad in listado)
			{
				ProcesoSeleccionBO objetoBO = Mapper.Map<TProcesoSeleccion, ProcesoSeleccionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ProcesoSeleccionBO FirstById(int id)
		{
			try
			{
				TProcesoSeleccion entidad = base.FirstById(id);
				ProcesoSeleccionBO objetoBO = new ProcesoSeleccionBO();
				Mapper.Map<TProcesoSeleccion, ProcesoSeleccionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ProcesoSeleccionBO FirstBy(Expression<Func<TProcesoSeleccion, bool>> filter)
		{
			try
			{
				TProcesoSeleccion entidad = base.FirstBy(filter);
				ProcesoSeleccionBO objetoBO = Mapper.Map<TProcesoSeleccion, ProcesoSeleccionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ProcesoSeleccionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TProcesoSeleccion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ProcesoSeleccionBO> listadoBO)
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

		public bool Update(ProcesoSeleccionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TProcesoSeleccion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ProcesoSeleccionBO> listadoBO)
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
		private void AsignacionId(TProcesoSeleccion entidad, ProcesoSeleccionBO objetoBO)
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

		private TProcesoSeleccion MapeoEntidad(ProcesoSeleccionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TProcesoSeleccion entidad = new TProcesoSeleccion();
				entidad = Mapper.Map<ProcesoSeleccionBO, TProcesoSeleccion>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<ProcesoSeleccionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TProcesoSeleccion, bool>>> filters, Expression<Func<TProcesoSeleccion, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TProcesoSeleccion> listado = base.GetFiltered(filters, orderBy, ascending);
			List<ProcesoSeleccionBO> listadoBO = new List<ProcesoSeleccionBO>();

			foreach (var itemEntidad in listado)
			{
				ProcesoSeleccionBO objetoBO = Mapper.Map<TProcesoSeleccion, ProcesoSeleccionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion

		/// <summary>
		/// Obtiene lista de procesos de seleccion activos
		/// </summary>
		/// <returns></returns>
		public List<ProcesoSeleccionDTO> ObtenerProcesoSeleccion()
		{
			try
			{
				var query = "SELECT Id, Nombre, IdPuestoTrabajo, PuestoTrabajo, Codigo, Url, Activo, IdSede, Sede FROM [gp].[V_TProcesoSelecciom_ObtenerProcesoSeleccion] WHERE Estado = 1 AND Activo = 1 ORDER BY FechaCreacion DESC";
				var res = _dapper.QueryDapper(query, null);
				return JsonConvert.DeserializeObject<List<ProcesoSeleccionDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public List<FiltroDTO> ObtenerEstadoProcesoSeleccion()
		{
			try
			{
				var query = "SELECT Id, Nombre FROM GP.V_TEstadoProcesoSeleccion_ObtenerEstadoProcesoSeleccion WHERE Estado = 1";
				var res = _dapper.QueryDapper(query, null);
				return JsonConvert.DeserializeObject<List<FiltroDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}


        public List<FiltroBasicoDTO> ObtenerEstadoEtapaProcesoSeleccion()
        {
            try
            {
                var query = "SELECT Id, Nombre FROM GP.T_EstadoEtapaProcesoSeleccion WHERE Estado = 1";
                var res = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<FiltroBasicoDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ConfigurarProcesoSeleccionDTO> ObtenerConfiguracionProcesoSeleccion()
        {
            try
            {
                var query = "SELECT Id,Nombre,IdPuestoTrabajo,PuestoTrabajo,IdSede,Sede,Codigo,Url,Activo,FechaInicioProceso,FechaFinProceso FROM GP.V_ObtenerProcesoSeleccion ";
                var res = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<ConfigurarProcesoSeleccionDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		public List<FiltroDTO> ObtenerProcesoSeleccionParaCombo()
		{
			try
			{
				return this.GetBy(x => x.Estado == true).Select(x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        public List<FiltroBasicoDTO> ObtenerProcesoSeleccion_FiltroBasico()
        {
            try
            {
                return this.GetBy(x => x.Estado == true).Select(x => new FiltroBasicoDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<FiltroBasicoDTO> ObtenerCodigoNombreProcesoSeleccion()
        {
            try
            {
                var query = "SELECT Id,concat(Codigo,' - ',Nombre) as Nombre FROM GP.V_ObtenerProcesoSeleccion where Activo=1";
                var res = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<FiltroBasicoDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		/// Repositorio: ProcesoSeleccionRepositorio
		/// Autor: Britsel C.
		/// Fecha: 24/05/2021
		/// <summary>
		/// Obtiene lista de proceso de seleccion junto a convocatorias asociadas
		/// </summary>
		/// <returns> List<ProcesoSeleccionConvocatoriaDTO> </returns>
		public List<ProcesoSeleccionConvocatoriaDTO> ObtenerProcesoSeleccionConvocatoria()
		{
			try
			{
				var query = "SELECT Id, Nombre, Codigo, IdConvocatoriaPersonal, CodigoConvocatoriaPersonal FROM [gp].[V_TProcesoSeleccion_ObtenerProcesoSeleccionConvocatorias] WHERE Estado = 1 AND Activo = 1";
				var res = _dapper.QueryDapper(query, null);
				return JsonConvert.DeserializeObject<List<ProcesoSeleccionConvocatoriaDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        public List<ReporteAnalisisProcesoSeleccionDTO> ObtenerReporteAnalisisProcesoSeleccion(FiltroAnalisisProcesoSeleccionDTO filtro)
        {
            try
            {

                var filtros = new
                {                    
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin,
                    IdProcesoSeleccion = filtro.IdProcesoSeleccion,
                };

                List<ReporteAnalisisProcesoSeleccionDTO> analisisProcesoSeleccion = new List<ReporteAnalisisProcesoSeleccionDTO>();
                string query = string.Empty;
                query = "gp.SP_ReporteAnalisisProcesoSeleccion";
                var PostulanteDB = _dapper.QuerySPDapper(query, filtros);
                if (!string.IsNullOrEmpty(PostulanteDB) && !PostulanteDB.Contains("[]"))
                {
                    analisisProcesoSeleccion = JsonConvert.DeserializeObject<List<ReporteAnalisisProcesoSeleccionDTO>>(PostulanteDB);
                }
                return analisisProcesoSeleccion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ReporteAnalisisProcesoSeleccionDTO> ObtenerReporteAnalisisProcesoSeleccion_V2(FiltroAnalisisProcesoSeleccionDTO filtro)
        {
            try
            {

                var filtros = new
                {
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin,
                    IdProcesoSeleccion = filtro.IdProcesoSeleccion,
                };

                List<ReporteAnalisisProcesoSeleccionDTO> analisisProcesoSeleccion = new List<ReporteAnalisisProcesoSeleccionDTO>();
                string query = string.Empty;
                query = "gp.SP_ReporteAnalisisProcesoSeleccion_V2";
                var PostulanteDB = _dapper.QuerySPDapper(query, filtros);
                if (!string.IsNullOrEmpty(PostulanteDB) && !PostulanteDB.Contains("[]"))
                {
                    analisisProcesoSeleccion = JsonConvert.DeserializeObject<List<ReporteAnalisisProcesoSeleccionDTO>>(PostulanteDB);
                }
                return analisisProcesoSeleccion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
		/// Autor: Jashin Salazar
		/// Fecha: 05/01/2021
		/// Version: 1.0
		/// <summary>
		/// Obtiene lista de procesos de seleccion activos
		/// </summary>
		/// <returns></returns>
		public List<ProcesoSeleccionDTO> ObtenerProcesoSeleccionTotal()
		{
			try
			{
				var query = "SELECT Id, Nombre, IdPuestoTrabajo, PuestoTrabajo, Codigo, Url, Activo, IdSede, Sede FROM [gp].[V_TProcesoSelecciom_ObtenerProcesoSeleccion] WHERE Estado = 1 ORDER BY FechaCreacion DESC";
				var res = _dapper.QueryDapper(query, null);
				return JsonConvert.DeserializeObject<List<ProcesoSeleccionDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
