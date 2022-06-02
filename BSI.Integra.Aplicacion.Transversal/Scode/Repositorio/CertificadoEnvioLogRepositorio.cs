using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CertificadoEnvioLogRepositorio : BaseRepository<TCertificadoEnvioLog, CertificadoEnvioLogBO>
    {
        #region Metodos Base
        public CertificadoEnvioLogRepositorio() : base()
        {
        }
        public CertificadoEnvioLogRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CertificadoEnvioLogBO> GetBy(Expression<Func<TCertificadoEnvioLog, bool>> filter)
        {
            IEnumerable<TCertificadoEnvioLog> listado = base.GetBy(filter);
            List<CertificadoEnvioLogBO> listadoBO = new List<CertificadoEnvioLogBO>();
            foreach (var itemEntidad in listado)
            {
                CertificadoEnvioLogBO objetoBO = Mapper.Map<TCertificadoEnvioLog, CertificadoEnvioLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CertificadoEnvioLogBO FirstById(int id)
        {
            try
            {
                TCertificadoEnvioLog entidad = base.FirstById(id);
                CertificadoEnvioLogBO objetoBO = new CertificadoEnvioLogBO();
                Mapper.Map<TCertificadoEnvioLog, CertificadoEnvioLogBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CertificadoEnvioLogBO FirstBy(Expression<Func<TCertificadoEnvioLog, bool>> filter)
        {
            try
            {
                TCertificadoEnvioLog entidad = base.FirstBy(filter);
                CertificadoEnvioLogBO objetoBO = Mapper.Map<TCertificadoEnvioLog, CertificadoEnvioLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CertificadoEnvioLogBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCertificadoEnvioLog entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CertificadoEnvioLogBO> listadoBO)
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

        public bool Update(CertificadoEnvioLogBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCertificadoEnvioLog entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CertificadoEnvioLogBO> listadoBO)
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
        private void AsignacionId(TCertificadoEnvioLog entidad, CertificadoEnvioLogBO objetoBO)
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

        private TCertificadoEnvioLog MapeoEntidad(CertificadoEnvioLogBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCertificadoEnvioLog entidad = new TCertificadoEnvioLog();
                entidad = Mapper.Map<CertificadoEnvioLogBO, TCertificadoEnvioLog>(objetoBO,
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

        public List<CertificadoEnvioLogDTO> ObtenerEnvioDigital(int IdCertificadoDetalle)
        {
            try
            {
                List<CertificadoEnvioLogDTO> _certificadoEnvio = new List<CertificadoEnvioLogDTO>();
                var _query = string.Empty;
                _query = "Select Id,FechaEnvio,SoloDigital FROM ope.V_ObtenerCertificadoEnvioDigital WHERE IdCertificadoDetalle = @IdCertificadoDetalle";

                var CertificadoSolicitud = _dapper.QueryDapper(_query, new { IdCertificadoDetalle });

                if (!string.IsNullOrEmpty(CertificadoSolicitud) && !CertificadoSolicitud.Contains("[]"))
                {
                    _certificadoEnvio = JsonConvert.DeserializeObject<List<CertificadoEnvioLogDTO>>(CertificadoSolicitud);
                }

                return _certificadoEnvio;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
