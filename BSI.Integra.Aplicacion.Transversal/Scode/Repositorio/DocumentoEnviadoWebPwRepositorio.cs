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

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class DocumentoEnviadoWebPwRepositorio : BaseRepository<TDocumentoEnviadoWebPw, DocumentoEnviadoWebPwBO>
    {
        #region Metodos Base
        public DocumentoEnviadoWebPwRepositorio() : base()
        {
        }
        public DocumentoEnviadoWebPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<DocumentoEnviadoWebPwBO> GetBy(Expression<Func<TDocumentoEnviadoWebPw, bool>> filter)
        {
            IEnumerable<TDocumentoEnviadoWebPw> listado = base.GetBy(filter);
            List<DocumentoEnviadoWebPwBO> listadoBO = new List<DocumentoEnviadoWebPwBO>();
            foreach (var itemEntidad in listado)
            {
                DocumentoEnviadoWebPwBO objetoBO = Mapper.Map<TDocumentoEnviadoWebPw, DocumentoEnviadoWebPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public DocumentoEnviadoWebPwBO FirstById(int id)
        {
            try
            {
                TDocumentoEnviadoWebPw entidad = base.FirstById(id);
                DocumentoEnviadoWebPwBO objetoBO = new DocumentoEnviadoWebPwBO();
                Mapper.Map<TDocumentoEnviadoWebPw, DocumentoEnviadoWebPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DocumentoEnviadoWebPwBO FirstBy(Expression<Func<TDocumentoEnviadoWebPw, bool>> filter)
        {
            try
            {
                TDocumentoEnviadoWebPw entidad = base.FirstBy(filter);
                DocumentoEnviadoWebPwBO objetoBO = Mapper.Map<TDocumentoEnviadoWebPw, DocumentoEnviadoWebPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(DocumentoEnviadoWebPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TDocumentoEnviadoWebPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<DocumentoEnviadoWebPwBO> listadoBO)
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

        public bool Update(DocumentoEnviadoWebPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TDocumentoEnviadoWebPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<DocumentoEnviadoWebPwBO> listadoBO)
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
        private void AsignacionId(TDocumentoEnviadoWebPw entidad, DocumentoEnviadoWebPwBO objetoBO)
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

        private TDocumentoEnviadoWebPw MapeoEntidad(DocumentoEnviadoWebPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TDocumentoEnviadoWebPw entidad = new TDocumentoEnviadoWebPw();
                entidad = Mapper.Map<DocumentoEnviadoWebPwBO, TDocumentoEnviadoWebPw>(objetoBO,
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
