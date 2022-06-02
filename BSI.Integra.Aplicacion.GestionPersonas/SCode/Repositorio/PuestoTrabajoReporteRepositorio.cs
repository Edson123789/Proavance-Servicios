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
    /// Repositorio: PuestoTrabajoReporteRepositorio
    /// Autor: Luis H., Edgar S.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de Reportes de Perfil de Puestos de Trabajo
    /// </summary>
    public class PuestoTrabajoReporteRepositorio : BaseRepository<TPuestoTrabajoReporte, PuestoTrabajoReporteBO>
    {
        #region Metodos Base
        public PuestoTrabajoReporteRepositorio() : base()
        {
        }
        public PuestoTrabajoReporteRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PuestoTrabajoReporteBO> GetBy(Expression<Func<TPuestoTrabajoReporte, bool>> filter)
        {
            IEnumerable<TPuestoTrabajoReporte> listado = base.GetBy(filter);
            List<PuestoTrabajoReporteBO> listadoBO = new List<PuestoTrabajoReporteBO>();
            foreach (var itemEntidad in listado)
            {
                PuestoTrabajoReporteBO objetoBO = Mapper.Map<TPuestoTrabajoReporte, PuestoTrabajoReporteBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PuestoTrabajoReporteBO FirstById(int id)
        {
            try
            {
                TPuestoTrabajoReporte entidad = base.FirstById(id);
                PuestoTrabajoReporteBO objetoBO = new PuestoTrabajoReporteBO();
                Mapper.Map<TPuestoTrabajoReporte, PuestoTrabajoReporteBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PuestoTrabajoReporteBO FirstBy(Expression<Func<TPuestoTrabajoReporte, bool>> filter)
        {
            try
            {
                TPuestoTrabajoReporte entidad = base.FirstBy(filter);
                PuestoTrabajoReporteBO objetoBO = Mapper.Map<TPuestoTrabajoReporte, PuestoTrabajoReporteBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PuestoTrabajoReporteBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPuestoTrabajoReporte entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PuestoTrabajoReporteBO> listadoBO)
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

        public bool Update(PuestoTrabajoReporteBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPuestoTrabajoReporte entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PuestoTrabajoReporteBO> listadoBO)
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
        private void AsignacionId(TPuestoTrabajoReporte entidad, PuestoTrabajoReporteBO objetoBO)
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

        private TPuestoTrabajoReporte MapeoEntidad(PuestoTrabajoReporteBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPuestoTrabajoReporte entidad = new TPuestoTrabajoReporte();
                entidad = Mapper.Map<PuestoTrabajoReporteBO, TPuestoTrabajoReporte>(objetoBO,
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

        /// Repositorio: PuestoTrabajoReporteRepositorio
        /// Autor: Luis H., Edgar S.
        /// Fecha: 29/01/2021
        /// <summary>
        /// Obtiene lista de reportes de un determinado puesto de trabajo
        /// </summary>
        /// <returns> Lista de Objeto DTO: List<PuestoTrabajoReporteDTO> </returns>
        public List<PuestoTrabajoReporteDTO> ObtenerPuestoTrabajoReporte(int idPerfilPuestoTrabajo)
		{
			try
			{
				List<PuestoTrabajoReporteDTO> lista = new List<PuestoTrabajoReporteDTO>();
				var _query = "SELECT Id, IdPerfilPuestoTrabajo, NroOrden, Reporte, IdFrecuenciaPuestoTrabajo, FrecuenciaPuestoTrabajo FROM [gp].[V_TPuestoTrabajoReporte_ObtenerPuestoTrabajoReporte] WHERE Estado = 1 AND IdPerfilPuestoTrabajo = @IdPerfilPuestoTrabajo";
				var res = _dapper.QueryDapper(_query, new { IdPerfilPuestoTrabajo = idPerfilPuestoTrabajo });
				if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
				{
					lista = JsonConvert.DeserializeObject<List<PuestoTrabajoReporteDTO>>(res);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
