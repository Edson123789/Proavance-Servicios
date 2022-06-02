
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class CertificacionPGeneralRepositorio : BaseRepository<TCertificacionPgeneral, CertificacionPGeneralBO>
    {
        #region Metodos Base
        public CertificacionPGeneralRepositorio() : base()
        {
        }
        public CertificacionPGeneralRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CertificacionPGeneralBO> GetBy(Expression<Func<TCertificacionPgeneral, bool>> filter)
        {
            IEnumerable<TCertificacionPgeneral> listado = base.GetBy(filter);
            List<CertificacionPGeneralBO> listadoBO = new List<CertificacionPGeneralBO>();
            foreach (var itemEntidad in listado)
            {
                CertificacionPGeneralBO objetoBO = Mapper.Map<TCertificacionPgeneral, CertificacionPGeneralBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CertificacionPGeneralBO FirstById(int id)
        {
            try
            {
                TCertificacionPgeneral entidad = base.FirstById(id);
                CertificacionPGeneralBO objetoBO = new CertificacionPGeneralBO();
                Mapper.Map<TCertificacionPgeneral, CertificacionPGeneralBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CertificacionPGeneralBO FirstBy(Expression<Func<TCertificacionPgeneral, bool>> filter)
        {
            try
            {
                TCertificacionPgeneral entidad = base.FirstBy(filter);
                CertificacionPGeneralBO objetoBO = Mapper.Map<TCertificacionPgeneral, CertificacionPGeneralBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CertificacionPGeneralBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCertificacionPgeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CertificacionPGeneralBO> listadoBO)
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

        public bool Update(CertificacionPGeneralBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCertificacionPgeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CertificacionPGeneralBO> listadoBO)
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
        private void AsignacionId(TCertificacionPgeneral entidad, CertificacionPGeneralBO objetoBO)
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

        private TCertificacionPgeneral MapeoEntidad(CertificacionPGeneralBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCertificacionPgeneral entidad = new TCertificacionPgeneral();
                entidad = Mapper.Map<CertificacionPGeneralBO, TCertificacionPgeneral>(objetoBO,
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
        /// Obtiene una lista de PGenerales (IDs de PGeneral) dado un ID de Certificacion para ser usados en un multiselect
        /// </summary>
        /// <returns></returns>
        public List<CertificacionPGeneralDTO> ObtenerTodoCertificacionPGeneralPorIdCertificacion(int IdCertificacion)
        {
            try
            {
                List<CertificacionPGeneralDTO> CertificacionPGenerales = new List<CertificacionPGeneralDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, IdCertificacion, IdPGeneral FROM pla.T_CertificacionPGeneral WHERE Estado = 1 AND IdCertificacion=" + IdCertificacion;
                var CertificacionPGeneralesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(CertificacionPGeneralesDB) && !CertificacionPGeneralesDB.Contains("[]"))
                {
                    CertificacionPGenerales = JsonConvert.DeserializeObject<List<CertificacionPGeneralDTO>>(CertificacionPGeneralesDB);
                }
                return CertificacionPGenerales;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
