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
    public class CertificadoTipoProgramaRepositorio : BaseRepository<TCertificadoTipoPrograma, CertificadoTipoProgramaBO>
    {
        #region Metodos Base
        public CertificadoTipoProgramaRepositorio() : base()
        {
        }
        public CertificadoTipoProgramaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CertificadoTipoProgramaBO> GetBy(Expression<Func<TCertificadoTipoPrograma, bool>> filter)
        {
            IEnumerable<TCertificadoTipoPrograma> listado = base.GetBy(filter);
            List<CertificadoTipoProgramaBO> listadoBO = new List<CertificadoTipoProgramaBO>();
            foreach (var itemEntidad in listado)
            {
                CertificadoTipoProgramaBO objetoBO = Mapper.Map<TCertificadoTipoPrograma, CertificadoTipoProgramaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CertificadoTipoProgramaBO FirstById(int id)
        {
            try
            {
                TCertificadoTipoPrograma entidad = base.FirstById(id);
                CertificadoTipoProgramaBO objetoBO = new CertificadoTipoProgramaBO();
                Mapper.Map<TCertificadoTipoPrograma, CertificadoTipoProgramaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CertificadoTipoProgramaBO FirstBy(Expression<Func<TCertificadoTipoPrograma, bool>> filter)
        {
            try
            {
                TCertificadoTipoPrograma entidad = base.FirstBy(filter);
                CertificadoTipoProgramaBO objetoBO = Mapper.Map<TCertificadoTipoPrograma, CertificadoTipoProgramaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CertificadoTipoProgramaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCertificadoTipoPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CertificadoTipoProgramaBO> listadoBO)
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

        public bool Update(CertificadoTipoProgramaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCertificadoTipoPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CertificadoTipoProgramaBO> listadoBO)
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
        private void AsignacionId(TCertificadoTipoPrograma entidad, CertificadoTipoProgramaBO objetoBO)
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

        private TCertificadoTipoPrograma MapeoEntidad(CertificadoTipoProgramaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCertificadoTipoPrograma entidad = new TCertificadoTipoPrograma();
                entidad = Mapper.Map<CertificadoTipoProgramaBO, TCertificadoTipoPrograma>(objetoBO,
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
                return GetBy(w => w.Estado, y => new FiltroDTO { Id = y.Id, Nombre = y.NombreProgramaCertificado }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
