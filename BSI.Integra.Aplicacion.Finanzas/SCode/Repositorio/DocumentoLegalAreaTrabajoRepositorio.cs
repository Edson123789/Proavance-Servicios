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
    public class DocumentoLegalAreaTrabajoRepositorio: BaseRepository<TDocumentoLegalAreaTrabajo, DocumentoLegalAreaTrabajoBO>
    {
        #region Metodos Base
        public DocumentoLegalAreaTrabajoRepositorio() : base()
        {
        }
        public DocumentoLegalAreaTrabajoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<DocumentoLegalAreaTrabajoBO> GetBy(Expression<Func<TDocumentoLegalAreaTrabajo, bool>> filter)
        {
            IEnumerable<TDocumentoLegalAreaTrabajo> listado = base.GetBy(filter);
            List<DocumentoLegalAreaTrabajoBO> listadoBO = new List<DocumentoLegalAreaTrabajoBO>();
            foreach (var itemEntidad in listado)
            {
                DocumentoLegalAreaTrabajoBO objetoBO = Mapper.Map<TDocumentoLegalAreaTrabajo, DocumentoLegalAreaTrabajoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public DocumentoLegalAreaTrabajoBO FirstById(int id)
        {
            try
            {
                TDocumentoLegalAreaTrabajo entidad = base.FirstById(id);
                DocumentoLegalAreaTrabajoBO objetoBO = new DocumentoLegalAreaTrabajoBO();
                Mapper.Map<TDocumentoLegalAreaTrabajo, DocumentoLegalAreaTrabajoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DocumentoLegalAreaTrabajoBO FirstBy(Expression<Func<TDocumentoLegalAreaTrabajo, bool>> filter)
        {
            try
            {
                TDocumentoLegalAreaTrabajo entidad = base.FirstBy(filter);
                DocumentoLegalAreaTrabajoBO objetoBO = Mapper.Map<TDocumentoLegalAreaTrabajo, DocumentoLegalAreaTrabajoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(DocumentoLegalAreaTrabajoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TDocumentoLegalAreaTrabajo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<DocumentoLegalAreaTrabajoBO> listadoBO)
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

        public bool Update(DocumentoLegalAreaTrabajoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TDocumentoLegalAreaTrabajo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<DocumentoLegalAreaTrabajoBO> listadoBO)
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
        private void AsignacionId(TDocumentoLegalAreaTrabajo entidad, DocumentoLegalAreaTrabajoBO objetoBO)
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

        private TDocumentoLegalAreaTrabajo MapeoEntidad(DocumentoLegalAreaTrabajoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TDocumentoLegalAreaTrabajo entidad = new TDocumentoLegalAreaTrabajo();
                entidad = Mapper.Map<DocumentoLegalAreaTrabajoBO, TDocumentoLegalAreaTrabajo>(objetoBO,
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
