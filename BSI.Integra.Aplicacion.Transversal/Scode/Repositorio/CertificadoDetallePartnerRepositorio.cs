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
    public class CertificadoDetallePartnerRepositorio : BaseRepository<TCertificadoDetallePartner, CertificadoDetallePartnerBO>
    {
        #region Metodos Base
        public CertificadoDetallePartnerRepositorio() : base()
        {
        }
        public CertificadoDetallePartnerRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CertificadoDetallePartnerBO> GetBy(Expression<Func<TCertificadoDetallePartner, bool>> filter)
        {
            IEnumerable<TCertificadoDetallePartner> listado = base.GetBy(filter);
            List<CertificadoDetallePartnerBO> listadoBO = new List<CertificadoDetallePartnerBO>();
            foreach (var itemEntidad in listado)
            {
                CertificadoDetallePartnerBO objetoBO = Mapper.Map<TCertificadoDetallePartner, CertificadoDetallePartnerBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CertificadoDetallePartnerBO FirstById(int id)
        {
            try
            {
                TCertificadoDetallePartner entidad = base.FirstById(id);
                CertificadoDetallePartnerBO objetoBO = new CertificadoDetallePartnerBO();
                Mapper.Map<TCertificadoDetallePartner, CertificadoDetallePartnerBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CertificadoDetallePartnerBO FirstBy(Expression<Func<TCertificadoDetallePartner, bool>> filter)
        {
            try
            {
                TCertificadoDetallePartner entidad = base.FirstBy(filter);
                CertificadoDetallePartnerBO objetoBO = Mapper.Map<TCertificadoDetallePartner, CertificadoDetallePartnerBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CertificadoDetallePartnerBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCertificadoDetallePartner entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CertificadoDetallePartnerBO> listadoBO)
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

        public bool Update(CertificadoDetallePartnerBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCertificadoDetallePartner entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CertificadoDetallePartnerBO> listadoBO)
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
        private void AsignacionId(TCertificadoDetallePartner entidad, CertificadoDetallePartnerBO objetoBO)
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

        private TCertificadoDetallePartner MapeoEntidad(CertificadoDetallePartnerBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCertificadoDetallePartner entidad = new TCertificadoDetallePartner();
                entidad = Mapper.Map<CertificadoDetallePartnerBO, TCertificadoDetallePartner>(objetoBO,
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
