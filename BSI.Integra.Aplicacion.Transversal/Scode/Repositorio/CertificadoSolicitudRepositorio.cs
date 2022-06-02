using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;
using BSI.Integra.Aplicacion.Transversal.Helper;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class CertificadoSolicitudRepositorio : BaseRepository<TCertificadoSolicitud, CertificadoSolicitudBO>
    {
        #region Metodos Base
        public CertificadoSolicitudRepositorio() : base()
        {
        }
        public CertificadoSolicitudRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CertificadoSolicitudBO> GetBy(Expression<Func<TCertificadoSolicitud, bool>> filter)
        {
            IEnumerable<TCertificadoSolicitud> listado = base.GetBy(filter);
            List<CertificadoSolicitudBO> listadoBO = new List<CertificadoSolicitudBO>();
            foreach (var itemEntidad in listado)
            {
                CertificadoSolicitudBO objetoBO = Mapper.Map<TCertificadoSolicitud, CertificadoSolicitudBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CertificadoSolicitudBO FirstById(int id)
        {
            try
            {
                TCertificadoSolicitud entidad = base.FirstById(id);
                CertificadoSolicitudBO objetoBO = new CertificadoSolicitudBO();
                Mapper.Map<TCertificadoSolicitud, CertificadoSolicitudBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CertificadoSolicitudBO FirstBy(Expression<Func<TCertificadoSolicitud, bool>> filter)
        {
            try
            {
                TCertificadoSolicitud entidad = base.FirstBy(filter);
                CertificadoSolicitudBO objetoBO = Mapper.Map<TCertificadoSolicitud, CertificadoSolicitudBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CertificadoSolicitudBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCertificadoSolicitud entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CertificadoSolicitudBO> listadoBO)
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

        public bool Update(CertificadoSolicitudBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCertificadoSolicitud entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CertificadoSolicitudBO> listadoBO)
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
        private void AsignacionId(TCertificadoSolicitud entidad, CertificadoSolicitudBO objetoBO)
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

        private TCertificadoSolicitud MapeoEntidad(CertificadoSolicitudBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCertificadoSolicitud entidad = new TCertificadoSolicitud();
                entidad = Mapper.Map<CertificadoSolicitudBO, TCertificadoSolicitud>(objetoBO,
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
        public int ObtenerNumeroSolictud(DateTime FechaActual)
        {
            ValorIntDTO rpta = new ValorIntDTO();
            string _query = "Select Max(NumeroSolicitud) AS Valor From ope.T_CertificadoSolicitud where Year(FechaCreacion)=" + FechaActual.Year;
            string query = _dapper.FirstOrDefault(_query,null);
            if (!string.IsNullOrEmpty(query) && !query.Equals("null"))
            {
                rpta = JsonConvert.DeserializeObject<ValorIntDTO>(query);
            }
            return rpta.Valor;
        }
    }
}