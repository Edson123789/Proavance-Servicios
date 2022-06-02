using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class DocumentoMarketingRepositorio : BaseRepository<TDocumentoMarketing, DocumentoMarketingBO>
    {
        #region Metodos Base
        public DocumentoMarketingRepositorio() : base()
        {
        }
        public DocumentoMarketingRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<DocumentoMarketingBO> GetBy(Expression<Func<TDocumentoMarketing, bool>> filter)
        {
            IEnumerable<TDocumentoMarketing> listado = base.GetBy(filter);
            List<DocumentoMarketingBO> listadoBO = new List<DocumentoMarketingBO>();
            foreach (var itemEntidad in listado)
            {
                DocumentoMarketingBO objetoBO = Mapper.Map<TDocumentoMarketing, DocumentoMarketingBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public DocumentoMarketingBO FirstById(int id)
        {
            try
            {
                TDocumentoMarketing entidad = base.FirstById(id);
                DocumentoMarketingBO objetoBO = new DocumentoMarketingBO();
                Mapper.Map<TDocumentoMarketing, DocumentoMarketingBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DocumentoMarketingBO FirstBy(Expression<Func<TDocumentoMarketing, bool>> filter)
        {
            try
            {
                TDocumentoMarketing entidad = base.FirstBy(filter);
                DocumentoMarketingBO objetoBO = Mapper.Map<TDocumentoMarketing, DocumentoMarketingBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(DocumentoMarketingBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TDocumentoMarketing entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<DocumentoMarketingBO> listadoBO)
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

        public bool Update(DocumentoMarketingBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TDocumentoMarketing entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<DocumentoMarketingBO> listadoBO)
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
        private void AsignacionId(TDocumentoMarketing entidad, DocumentoMarketingBO objetoBO)
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

        private TDocumentoMarketing MapeoEntidad(DocumentoMarketingBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TDocumentoMarketing entidad = new TDocumentoMarketing();
                entidad = Mapper.Map<DocumentoMarketingBO, TDocumentoMarketing>(objetoBO,
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
