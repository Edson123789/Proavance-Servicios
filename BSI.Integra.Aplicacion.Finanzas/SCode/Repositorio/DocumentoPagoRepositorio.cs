using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class DocumentoPagoRepositorio : BaseRepository<TDocumentoPago, DocumentoPagoBO>
    {
        #region Metodos Base
        public DocumentoPagoRepositorio() : base()
        {
        }
        public DocumentoPagoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<DocumentoPagoBO> GetBy(Expression<Func<TDocumentoPago, bool>> filter)
        {
            IEnumerable<TDocumentoPago> listado = base.GetBy(filter);
            List<DocumentoPagoBO> listadoBO = new List<DocumentoPagoBO>();
            foreach (var itemEntidad in listado)
            {
                DocumentoPagoBO objetoBO = Mapper.Map<TDocumentoPago, DocumentoPagoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public DocumentoPagoBO FirstById(int id)
        {
            try
            {
                TDocumentoPago entidad = base.FirstById(id);
                DocumentoPagoBO objetoBO = new DocumentoPagoBO();
                Mapper.Map<TDocumentoPago, DocumentoPagoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DocumentoPagoBO FirstBy(Expression<Func<TDocumentoPago, bool>> filter)
        {
            try
            {
                TDocumentoPago entidad = base.FirstBy(filter);
                DocumentoPagoBO objetoBO = Mapper.Map<TDocumentoPago, DocumentoPagoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(DocumentoPagoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TDocumentoPago entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<DocumentoPagoBO> listadoBO)
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

        public bool Update(DocumentoPagoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TDocumentoPago entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<DocumentoPagoBO> listadoBO)
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
        private void AsignacionId(TDocumentoPago entidad, DocumentoPagoBO objetoBO)
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

        private TDocumentoPago MapeoEntidad(DocumentoPagoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TDocumentoPago entidad = new TDocumentoPago();
                entidad = Mapper.Map<DocumentoPagoBO, TDocumentoPago>(objetoBO,
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

        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <returns></returns>
        public List<DocumentoPagoDTO> ObtenerTodoDocumentoPago()
        {
            try
            {
                var lista = GetBy(x => true, y => new DocumentoPagoDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                }).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
