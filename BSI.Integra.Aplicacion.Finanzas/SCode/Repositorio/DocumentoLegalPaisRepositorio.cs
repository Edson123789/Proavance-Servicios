using AutoMapper;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class DocumentoLegalPaisRepositorio : BaseRepository<TDocumentoLegalPais, DocumentoLegalPaisBO>
    {
        #region Metodos Base
        public DocumentoLegalPaisRepositorio() : base()
        {
        }
        public DocumentoLegalPaisRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<DocumentoLegalPaisBO> GetBy(Expression<Func<TDocumentoLegalPais, bool>> filter)
        {
            IEnumerable<TDocumentoLegalPais> listado = base.GetBy(filter);
            List<DocumentoLegalPaisBO> listadoBO = new List<DocumentoLegalPaisBO>();
            foreach (var itemEntidad in listado)
            {
                DocumentoLegalPaisBO objetoBO = Mapper.Map<TDocumentoLegalPais, DocumentoLegalPaisBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public DocumentoLegalPaisBO FirstById(int id)
        {
            try
            {
                TDocumentoLegalPais entidad = base.FirstById(id);
                DocumentoLegalPaisBO objetoBO = new DocumentoLegalPaisBO();
                Mapper.Map<TDocumentoLegalPais, DocumentoLegalPaisBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DocumentoLegalPaisBO FirstBy(Expression<Func<TDocumentoLegalPais, bool>> filter)
        {
            try
            {
                TDocumentoLegalPais entidad = base.FirstBy(filter);
                DocumentoLegalPaisBO objetoBO = Mapper.Map<TDocumentoLegalPais, DocumentoLegalPaisBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(DocumentoLegalPaisBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TDocumentoLegalPais entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<DocumentoLegalPaisBO> listadoBO)
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

        public bool Update(DocumentoLegalPaisBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TDocumentoLegalPais entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<DocumentoLegalPaisBO> listadoBO)
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
        private void AsignacionId(TDocumentoLegalPais entidad, DocumentoLegalPaisBO objetoBO)
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

        private TDocumentoLegalPais MapeoEntidad(DocumentoLegalPaisBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TDocumentoLegalPais entidad = new TDocumentoLegalPais();
                entidad = Mapper.Map<DocumentoLegalPaisBO, TDocumentoLegalPais>(objetoBO,
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
