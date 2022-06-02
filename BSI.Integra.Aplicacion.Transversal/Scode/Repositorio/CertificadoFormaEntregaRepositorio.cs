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
    public class CertificadoFormaEntregaRepositorio : BaseRepository<TCertificadoFormaEntrega, CertificadoFormaEntregaBO>
    {
        #region Metodos Base
        public CertificadoFormaEntregaRepositorio() : base()
        {
        }
        public CertificadoFormaEntregaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CertificadoFormaEntregaBO> GetBy(Expression<Func<TCertificadoFormaEntrega, bool>> filter)
        {
            IEnumerable<TCertificadoFormaEntrega> listado = base.GetBy(filter);
            List<CertificadoFormaEntregaBO> listadoBO = new List<CertificadoFormaEntregaBO>();
            foreach (var itemEntidad in listado)
            {
                CertificadoFormaEntregaBO objetoBO = Mapper.Map<TCertificadoFormaEntrega, CertificadoFormaEntregaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CertificadoFormaEntregaBO FirstById(int id)
        {
            try
            {
                TCertificadoFormaEntrega entidad = base.FirstById(id);
                CertificadoFormaEntregaBO objetoBO = new CertificadoFormaEntregaBO();
                Mapper.Map<TCertificadoFormaEntrega, CertificadoFormaEntregaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CertificadoFormaEntregaBO FirstBy(Expression<Func<TCertificadoFormaEntrega, bool>> filter)
        {
            try
            {
                TCertificadoFormaEntrega entidad = base.FirstBy(filter);
                CertificadoFormaEntregaBO objetoBO = Mapper.Map<TCertificadoFormaEntrega, CertificadoFormaEntregaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CertificadoFormaEntregaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCertificadoFormaEntrega entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CertificadoFormaEntregaBO> listadoBO)
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

        public bool Update(CertificadoFormaEntregaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCertificadoFormaEntrega entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CertificadoFormaEntregaBO> listadoBO)
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
        private void AsignacionId(TCertificadoFormaEntrega entidad, CertificadoFormaEntregaBO objetoBO)
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

        private TCertificadoFormaEntrega MapeoEntidad(CertificadoFormaEntregaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCertificadoFormaEntrega entidad = new TCertificadoFormaEntrega();
                entidad = Mapper.Map<CertificadoFormaEntregaBO, TCertificadoFormaEntrega>(objetoBO,
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
