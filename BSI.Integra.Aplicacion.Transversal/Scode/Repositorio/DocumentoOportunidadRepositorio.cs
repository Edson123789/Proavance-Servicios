using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class DocumentoOportunidadRepositorio : BaseRepository<TDocumentoOportunidad, DocumentoOportunidadBO>
    {
        #region Metodos Base
        public DocumentoOportunidadRepositorio() : base()
        {
        }
        public DocumentoOportunidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<DocumentoOportunidadBO> GetBy(Expression<Func<TDocumentoOportunidad, bool>> filter)
        {
            IEnumerable<TDocumentoOportunidad> listado = base.GetBy(filter);
            List<DocumentoOportunidadBO> listadoBO = new List<DocumentoOportunidadBO>();
            foreach (var itemEntidad in listado)
            {
                DocumentoOportunidadBO objetoBO = Mapper.Map<TDocumentoOportunidad, DocumentoOportunidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public DocumentoOportunidadBO FirstById(int id)
        {
            try
            {
                TDocumentoOportunidad entidad = base.FirstById(id);
                DocumentoOportunidadBO objetoBO = new DocumentoOportunidadBO();
                Mapper.Map<TDocumentoOportunidad, DocumentoOportunidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DocumentoOportunidadBO FirstBy(Expression<Func<TDocumentoOportunidad, bool>> filter)
        {
            try
            {
                TDocumentoOportunidad entidad = base.FirstBy(filter);
                DocumentoOportunidadBO objetoBO = Mapper.Map<TDocumentoOportunidad, DocumentoOportunidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(DocumentoOportunidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TDocumentoOportunidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<DocumentoOportunidadBO> listadoBO)
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

        public bool Update(DocumentoOportunidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TDocumentoOportunidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<DocumentoOportunidadBO> listadoBO)
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
        private void AsignacionId(TDocumentoOportunidad entidad, DocumentoOportunidadBO objetoBO)
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

        private TDocumentoOportunidad MapeoEntidad(DocumentoOportunidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TDocumentoOportunidad entidad = new TDocumentoOportunidad();
                entidad = Mapper.Map<DocumentoOportunidadBO, TDocumentoOportunidad>(objetoBO,
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
