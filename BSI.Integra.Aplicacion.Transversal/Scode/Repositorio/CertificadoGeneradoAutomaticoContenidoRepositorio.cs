using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class CertificadoGeneradoAutomaticoContenidoRepositorio : BaseRepository<TCertificadoGeneradoAutomaticoContenido, CertificadoGeneradoAutomaticoContenidoBO>
    {
        #region Metodos Base
        public CertificadoGeneradoAutomaticoContenidoRepositorio() : base()
        {
        }
        public CertificadoGeneradoAutomaticoContenidoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CertificadoGeneradoAutomaticoContenidoBO> GetBy(Expression<Func<TCertificadoGeneradoAutomaticoContenido, bool>> filter)
        {
            IEnumerable<TCertificadoGeneradoAutomaticoContenido> listado = base.GetBy(filter);
            List<CertificadoGeneradoAutomaticoContenidoBO> listadoBO = new List<CertificadoGeneradoAutomaticoContenidoBO>();
            foreach (var itemEntidad in listado)
            {
                CertificadoGeneradoAutomaticoContenidoBO objetoBO = Mapper.Map<TCertificadoGeneradoAutomaticoContenido, CertificadoGeneradoAutomaticoContenidoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CertificadoGeneradoAutomaticoContenidoBO FirstById(int id)
        {
            try
            {
                TCertificadoGeneradoAutomaticoContenido entidad = base.FirstById(id);
                CertificadoGeneradoAutomaticoContenidoBO objetoBO = new CertificadoGeneradoAutomaticoContenidoBO();
                Mapper.Map<TCertificadoGeneradoAutomaticoContenido, CertificadoGeneradoAutomaticoContenidoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CertificadoGeneradoAutomaticoContenidoBO FirstBy(Expression<Func<TCertificadoGeneradoAutomaticoContenido, bool>> filter)
        {
            try
            {
                TCertificadoGeneradoAutomaticoContenido entidad = base.FirstBy(filter);
                CertificadoGeneradoAutomaticoContenidoBO objetoBO = Mapper.Map<TCertificadoGeneradoAutomaticoContenido, CertificadoGeneradoAutomaticoContenidoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool Insert(CertificadoGeneradoAutomaticoContenidoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCertificadoGeneradoAutomaticoContenido entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CertificadoGeneradoAutomaticoContenidoBO> listadoBO)
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

        public bool Update(CertificadoGeneradoAutomaticoContenidoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCertificadoGeneradoAutomaticoContenido entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CertificadoGeneradoAutomaticoContenidoBO> listadoBO)
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
        private void AsignacionId(TCertificadoGeneradoAutomaticoContenido entidad, CertificadoGeneradoAutomaticoContenidoBO objetoBO)
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

        private TCertificadoGeneradoAutomaticoContenido MapeoEntidad(CertificadoGeneradoAutomaticoContenidoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCertificadoGeneradoAutomaticoContenido entidad = new TCertificadoGeneradoAutomaticoContenido();
                entidad = Mapper.Map<CertificadoGeneradoAutomaticoContenidoBO, TCertificadoGeneradoAutomaticoContenido>(objetoBO,
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
        public List<ContenidoCertificadoSinFondoDTO> ObtenerDatosParaCertificadoSinFondo(int IdMatriculaCabecera)
        {
            var rpta = new List<ContenidoCertificadoSinFondoDTO>();
            string _query = "pla.V_ContenidoCertificadoSinFondo";
            string query = _dapper.QuerySPDapper(_query, new { IdMatriculaCabecera = IdMatriculaCabecera });

            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<List<ContenidoCertificadoSinFondoDTO>>(query);
            }
            return rpta;
        }
        public List<ContenidoCertificadoSinFondoDTO> ObtenerDatosParaCertificadoFisico(int IdCertificadoGeneradoAutomatico)
        {
            var rpta = new List<ContenidoCertificadoSinFondoDTO>();
            string _query = "ope.SP_ContenidoCertificadoFisico";
            string query = _dapper.QuerySPDapper(_query, new { IdCertificadoGeneradoAutomatico });

            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<List<ContenidoCertificadoSinFondoDTO>>(query);
            }
            return rpta;
        }
    }
}
