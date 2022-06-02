using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class CronogramaDetalleCambioRepositorio : BaseRepository<TCronogramaDetalleCambio, CronogramaDetalleCambioBO>
    {
        #region Metodos Base
        public CronogramaDetalleCambioRepositorio() : base()
        {
        }
        public CronogramaDetalleCambioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CronogramaDetalleCambioBO> GetBy(Expression<Func<TCronogramaDetalleCambio, bool>> filter)
        {
            IEnumerable<TCronogramaDetalleCambio> listado = base.GetBy(filter);
            List<CronogramaDetalleCambioBO> listadoBO = new List<CronogramaDetalleCambioBO>();
            foreach (var itemEntidad in listado)
            {
                CronogramaDetalleCambioBO objetoBO = Mapper.Map<TCronogramaDetalleCambio, CronogramaDetalleCambioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CronogramaDetalleCambioBO FirstById(int id)
        {
            try
            {
                TCronogramaDetalleCambio entidad = base.FirstById(id);
                CronogramaDetalleCambioBO objetoBO = new CronogramaDetalleCambioBO();
                Mapper.Map<TCronogramaDetalleCambio, CronogramaDetalleCambioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CronogramaDetalleCambioBO FirstBy(Expression<Func<TCronogramaDetalleCambio, bool>> filter)
        {
            try
            {
                TCronogramaDetalleCambio entidad = base.FirstBy(filter);
                CronogramaDetalleCambioBO objetoBO = Mapper.Map<TCronogramaDetalleCambio, CronogramaDetalleCambioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CronogramaDetalleCambioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCronogramaDetalleCambio entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CronogramaDetalleCambioBO> listadoBO)
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

        public bool Update(CronogramaDetalleCambioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCronogramaDetalleCambio entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CronogramaDetalleCambioBO> listadoBO)
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
        private void AsignacionId(TCronogramaDetalleCambio entidad, CronogramaDetalleCambioBO objetoBO)
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

        private TCronogramaDetalleCambio MapeoEntidad(CronogramaDetalleCambioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCronogramaDetalleCambio entidad = new TCronogramaDetalleCambio();
                entidad = Mapper.Map<CronogramaDetalleCambioBO, TCronogramaDetalleCambio>(objetoBO,
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
        /// Obtiene los cambios pendientes por matricula y version
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <param name="version"></param>
        public List<CambioCronogramaDTO> ObtenerCambiosPendientes(int idMatriculaCabecera, int version) {
            try
            {
                List<CambioCronogramaDTO> cambiosCronograma = new List<CambioCronogramaDTO>();
                var query = "SELECT TipoModificacion, SubTipo, EmailAprueba, EmailSolicita FROM fin.ObtenerCambiosPendientesPorMatricula WHERE  IdMatriculaCabecera = @idMatriculaCabecera AND Version = @version AND EstadoCronogramaDetalleCambio = 1 AND EstadoCronogramaDetalleCambio = 1 AND EstadoCronogramaCabeceraCambio = 1 AND EstadoCronogramaTipoModificacion = 1 AND EstadoCronogramaSubTipoModificacion = 1 AND EstadoPersonalAprobado = 1 AND EstadoPersonalSolicitante = 1";
                var cambiosCronogramasDB = _dapper.QueryDapper(query,new { idMatriculaCabecera, version });
                if (!string.IsNullOrEmpty(cambiosCronogramasDB) && !cambiosCronogramasDB.Contains("[]"))
                {
                    cambiosCronograma = JsonConvert.DeserializeObject<List<CambioCronogramaDTO>>(cambiosCronogramasDB);
                }
                return cambiosCronograma;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
