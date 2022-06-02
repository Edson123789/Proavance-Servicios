using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    public class DocumentacionPersonalRepositorio : BaseRepository<TDocumentacionPersonal, DocumentacionPersonalBO>
    {
        #region Metodos Base
        public DocumentacionPersonalRepositorio() : base()
        {
        }
        public DocumentacionPersonalRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<DocumentacionPersonalBO> GetBy(Expression<Func<TDocumentacionPersonal, bool>> filter)
        {
            IEnumerable<TDocumentacionPersonal> listado = base.GetBy(filter);
            List<DocumentacionPersonalBO> listadoBO = new List<DocumentacionPersonalBO>();
            foreach (var itemEntidad in listado)
            {
                DocumentacionPersonalBO objetoBO = Mapper.Map<TDocumentacionPersonal, DocumentacionPersonalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public DocumentacionPersonalBO FirstById(int id)
        {
            try
            {
                TDocumentacionPersonal entidad = base.FirstById(id);
                DocumentacionPersonalBO objetoBO = new DocumentacionPersonalBO();
                Mapper.Map<TDocumentacionPersonal, DocumentacionPersonalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DocumentacionPersonalBO FirstBy(Expression<Func<TDocumentacionPersonal, bool>> filter)
        {
            try
            {
                TDocumentacionPersonal entidad = base.FirstBy(filter);
                DocumentacionPersonalBO objetoBO = Mapper.Map<TDocumentacionPersonal, DocumentacionPersonalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(DocumentacionPersonalBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TDocumentacionPersonal entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<DocumentacionPersonalBO> listadoBO)
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

        public bool Update(DocumentacionPersonalBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TDocumentacionPersonal entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<DocumentacionPersonalBO> listadoBO)
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
        private void AsignacionId(TDocumentacionPersonal entidad, DocumentacionPersonalBO objetoBO)
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

        private TDocumentacionPersonal MapeoEntidad(DocumentacionPersonalBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TDocumentacionPersonal entidad = new TDocumentacionPersonal();
                entidad = Mapper.Map<DocumentacionPersonalBO, TDocumentacionPersonal>(objetoBO,
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
