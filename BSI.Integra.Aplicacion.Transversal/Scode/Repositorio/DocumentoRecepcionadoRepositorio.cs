using System;
using System.Collections.Generic;
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
    public class DocumentoRecepcionadoRepositorio : BaseRepository<TDocumentoRecepcionado, DocumentoRecepcionadoBO>
    {
        #region Metodos Base
        public DocumentoRecepcionadoRepositorio() : base()
        {
        }
        public DocumentoRecepcionadoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<DocumentoRecepcionadoBO> GetBy(Expression<Func<TDocumentoRecepcionado, bool>> filter)
        {
            IEnumerable<TDocumentoRecepcionado> listado = base.GetBy(filter);
            List<DocumentoRecepcionadoBO> listadoBO = new List<DocumentoRecepcionadoBO>();
            foreach (var itemEntidad in listado)
            {
                DocumentoRecepcionadoBO objetoBO = Mapper.Map<TDocumentoRecepcionado, DocumentoRecepcionadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public DocumentoRecepcionadoBO FirstById(int id)
        {
            try
            {
                TDocumentoRecepcionado entidad = base.FirstById(id);
                DocumentoRecepcionadoBO objetoBO = new DocumentoRecepcionadoBO();
                Mapper.Map<TDocumentoRecepcionado, DocumentoRecepcionadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DocumentoRecepcionadoBO FirstBy(Expression<Func<TDocumentoRecepcionado, bool>> filter)
        {
            try
            {
                TDocumentoRecepcionado entidad = base.FirstBy(filter);
                DocumentoRecepcionadoBO objetoBO = Mapper.Map<TDocumentoRecepcionado, DocumentoRecepcionadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(DocumentoRecepcionadoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TDocumentoRecepcionado entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<DocumentoRecepcionadoBO> listadoBO)
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

        public bool Update(DocumentoRecepcionadoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TDocumentoRecepcionado entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<DocumentoRecepcionadoBO> listadoBO)
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
        private void AsignacionId(TDocumentoRecepcionado entidad, DocumentoRecepcionadoBO objetoBO)
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

        private TDocumentoRecepcionado MapeoEntidad(DocumentoRecepcionadoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TDocumentoRecepcionado entidad = new TDocumentoRecepcionado();
                entidad = Mapper.Map<DocumentoRecepcionadoBO, TDocumentoRecepcionado>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<DocumentoRecepcionadoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TDocumentoRecepcionado, bool>>> filters, Expression<Func<TDocumentoRecepcionado, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TDocumentoRecepcionado> listado = base.GetFiltered(filters, orderBy, ascending);
            List<DocumentoRecepcionadoBO> listadoBO = new List<DocumentoRecepcionadoBO>();

            foreach (var itemEntidad in listado)
            {
                DocumentoRecepcionadoBO objetoBO = Mapper.Map<TDocumentoRecepcionado, DocumentoRecepcionadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Se obtiene todos los registros sin los campos de auditoria
        /// </summary>
        /// <returns></returns>
        public List<TodoDocumentoRecepcionadoDTO> ObtenerTodoDocumentoRecepcionado()
        {
            try
            {
                var _query = "SELECT  Id, IdPersonaTipoPersona, Nombre, IdDocumento, NombreDocumento, IdPEspecifico, NombrePEspecifico, NombreArchivo FROM conf.V_ObtenerTodoDocumentoRecepcionado WHERE EstadoDocumentoRecepcionado = 1";
                var documentoRecepcionadoDB = _dapper.QueryDapper(_query, null);
                return JsonConvert.DeserializeObject<List<TodoDocumentoRecepcionadoDTO>>(documentoRecepcionadoDB);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Se obtiene todos los registros de documentosrecepcionados filtrados por el idPersonaTipoPersona
        /// </summary>
        /// <param name="idPersonaTipoPersona"></param>
        /// <returns></returns>
        public List<TodoDocumentoRecepcionadoDTO> ObtenerDocumentoPorIdPersonaTipoPersona(int idPersonaTipoPersona)
        {
            try
            {
                var _query = "SELECT  Id, IdPersonaTipoPersona, Nombre, IdDocumento, NombreDocumento, IdPEspecifico, NombrePEspecifico, NombreArchivo FROM conf.V_ObtenerTodoDocumentoRecepcionado WHERE IdPersonaTipoPersona = @idPersonaTipoPersona AND EstadoDocumentoRecepcionado = 1";
                var documentoRecepcionadoDB = _dapper.QueryDapper(_query, new { idPersonaTipoPersona });
                return JsonConvert.DeserializeObject<List<TodoDocumentoRecepcionadoDTO>>(documentoRecepcionadoDB);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
