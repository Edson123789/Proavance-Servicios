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
    public class CertificadoTipoRepositorio : BaseRepository<TCertificadoTipo, CertificadoTipoBO>
    {
        #region Metodos Base
        public CertificadoTipoRepositorio() : base()
        {
        }
        public CertificadoTipoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CertificadoTipoBO> GetBy(Expression<Func<TCertificadoTipo, bool>> filter)
        {
            IEnumerable<TCertificadoTipo> listado = base.GetBy(filter);
            List<CertificadoTipoBO> listadoBO = new List<CertificadoTipoBO>();
            foreach (var itemEntidad in listado)
            {
                CertificadoTipoBO objetoBO = Mapper.Map<TCertificadoTipo, CertificadoTipoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CertificadoTipoBO FirstById(int id)
        {
            try
            {
                TCertificadoTipo entidad = base.FirstById(id);
                CertificadoTipoBO objetoBO = new CertificadoTipoBO();
                Mapper.Map<TCertificadoTipo, CertificadoTipoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CertificadoTipoBO FirstBy(Expression<Func<TCertificadoTipo, bool>> filter)
        {
            try
            {
                TCertificadoTipo entidad = base.FirstBy(filter);
                CertificadoTipoBO objetoBO = Mapper.Map<TCertificadoTipo, CertificadoTipoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CertificadoTipoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCertificadoTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CertificadoTipoBO> listadoBO)
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

        public bool Update(CertificadoTipoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCertificadoTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CertificadoTipoBO> listadoBO)
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
        private void AsignacionId(TCertificadoTipo entidad, CertificadoTipoBO objetoBO)
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

        private TCertificadoTipo MapeoEntidad(CertificadoTipoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCertificadoTipo entidad = new TCertificadoTipo();
                entidad = Mapper.Map<CertificadoTipoBO, TCertificadoTipo>(objetoBO,
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

        public List<FiltroDTO> ObtenerFiltro()
        {
            try
            {
                var Lista = GetBy(w => w.Estado, y => new FiltroDTO { Id = y.Id, Nombre = y.Nombre }).ToList();
                return Lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        
        }
    }
}
