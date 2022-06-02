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
    public class CertificadoReversionEmisionRepositorio : BaseRepository<TCertificadoReversionEmision, CertificadoReversionEmisionBO>
    {
        #region Metodos Base
        public CertificadoReversionEmisionRepositorio() : base()
        {
        }
        public CertificadoReversionEmisionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CertificadoReversionEmisionBO> GetBy(Expression<Func<TCertificadoReversionEmision, bool>> filter)
        {
            IEnumerable<TCertificadoReversionEmision> listado = base.GetBy(filter);
            List<CertificadoReversionEmisionBO> listadoBO = new List<CertificadoReversionEmisionBO>();
            foreach (var itemEntidad in listado)
            {
                CertificadoReversionEmisionBO objetoBO = Mapper.Map<TCertificadoReversionEmision, CertificadoReversionEmisionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CertificadoReversionEmisionBO FirstById(int id)
        {
            try
            {
                TCertificadoReversionEmision entidad = base.FirstById(id);
                CertificadoReversionEmisionBO objetoBO = new CertificadoReversionEmisionBO();
                Mapper.Map<TCertificadoReversionEmision, CertificadoReversionEmisionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CertificadoReversionEmisionBO FirstBy(Expression<Func<TCertificadoReversionEmision, bool>> filter)
        {
            try
            {
                TCertificadoReversionEmision entidad = base.FirstBy(filter);
                CertificadoReversionEmisionBO objetoBO = Mapper.Map<TCertificadoReversionEmision, CertificadoReversionEmisionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CertificadoReversionEmisionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCertificadoReversionEmision entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CertificadoReversionEmisionBO> listadoBO)
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

        public bool Update(CertificadoReversionEmisionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCertificadoReversionEmision entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CertificadoReversionEmisionBO> listadoBO)
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
        private void AsignacionId(TCertificadoReversionEmision entidad, CertificadoReversionEmisionBO objetoBO)
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

        private TCertificadoReversionEmision MapeoEntidad(CertificadoReversionEmisionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCertificadoReversionEmision entidad = new TCertificadoReversionEmision();
                entidad = Mapper.Map<CertificadoReversionEmisionBO, TCertificadoReversionEmision>(objetoBO,
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
