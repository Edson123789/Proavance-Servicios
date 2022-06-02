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
    public class DocumentoConfiguracionRecepcionRepositorio : BaseRepository<TDocumentoConfiguracionRecepcion, DocumentoConfiguracionRecepcionBO>
    {
        #region Metodos Base
        public DocumentoConfiguracionRecepcionRepositorio() : base()
        {
        }
        public DocumentoConfiguracionRecepcionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<DocumentoConfiguracionRecepcionBO> GetBy(Expression<Func<TDocumentoConfiguracionRecepcion, bool>> filter)
        {
            IEnumerable<TDocumentoConfiguracionRecepcion> listado = base.GetBy(filter);
            List<DocumentoConfiguracionRecepcionBO> listadoBO = new List<DocumentoConfiguracionRecepcionBO>();
            foreach (var itemEntidad in listado)
            {
                DocumentoConfiguracionRecepcionBO objetoBO = Mapper.Map<TDocumentoConfiguracionRecepcion, DocumentoConfiguracionRecepcionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public DocumentoConfiguracionRecepcionBO FirstById(int id)
        {
            try
            {
                TDocumentoConfiguracionRecepcion entidad = base.FirstById(id);
                DocumentoConfiguracionRecepcionBO objetoBO = new DocumentoConfiguracionRecepcionBO();
                Mapper.Map<TDocumentoConfiguracionRecepcion, DocumentoConfiguracionRecepcionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DocumentoConfiguracionRecepcionBO FirstBy(Expression<Func<TDocumentoConfiguracionRecepcion, bool>> filter)
        {
            try
            {
                TDocumentoConfiguracionRecepcion entidad = base.FirstBy(filter);
                DocumentoConfiguracionRecepcionBO objetoBO = Mapper.Map<TDocumentoConfiguracionRecepcion, DocumentoConfiguracionRecepcionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(DocumentoConfiguracionRecepcionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TDocumentoConfiguracionRecepcion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<DocumentoConfiguracionRecepcionBO> listadoBO)
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

        public bool Update(DocumentoConfiguracionRecepcionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TDocumentoConfiguracionRecepcion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<DocumentoConfiguracionRecepcionBO> listadoBO)
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
        private void AsignacionId(TDocumentoConfiguracionRecepcion entidad, DocumentoConfiguracionRecepcionBO objetoBO)
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

        private TDocumentoConfiguracionRecepcion MapeoEntidad(DocumentoConfiguracionRecepcionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TDocumentoConfiguracionRecepcion entidad = new TDocumentoConfiguracionRecepcion();
                entidad = Mapper.Map<DocumentoConfiguracionRecepcionBO, TDocumentoConfiguracionRecepcion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<DocumentoConfiguracionRecepcionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TDocumentoConfiguracionRecepcion, bool>>> filters, Expression<Func<TDocumentoConfiguracionRecepcion, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TDocumentoConfiguracionRecepcion> listado = base.GetFiltered(filters, orderBy, ascending);
            List<DocumentoConfiguracionRecepcionBO> listadoBO = new List<DocumentoConfiguracionRecepcionBO>();

            foreach (var itemEntidad in listado)
            {
                DocumentoConfiguracionRecepcionBO objetoBO = Mapper.Map<TDocumentoConfiguracionRecepcion, DocumentoConfiguracionRecepcionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        ///  Obtiene todos los registros sin los campos de auditoria.
        /// </summary>
        /// <returns></returns>
        public List<DocumentoConfiguracionRecepcionDatosDTO> ObtenerTodoGrid()
        {
            try
            {
                List<DocumentoConfiguracionRecepcionDatosDTO> documentoConfiguracionRecepion = new List<DocumentoConfiguracionRecepcionDatosDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, IdTipoPersona, NombreTipoPersona, IdPais, NombrePais, IdDocumento, NombreDocumento, IdModalidadCurso, NombreModalidadCurso, Padre, EsActivo Nombre FROM conf.V_ObtenerListaDocumentoConfiguracionRecepcion WHERE  Estado = 1 ";
                var documentoConfiguracionRecepionDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(documentoConfiguracionRecepionDB) && !documentoConfiguracionRecepionDB.Contains("[]"))
                {
                    documentoConfiguracionRecepion = JsonConvert.DeserializeObject<List<DocumentoConfiguracionRecepcionDatosDTO>>(documentoConfiguracionRecepionDB);
                }
                return documentoConfiguracionRecepion;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Obtiene la lista de documentos filtrados por el IdTipoPersona, IdPais, IdModalidadCurso
        /// </summary>
        /// <param name="idTipoPersona"></param>
        /// <param name="idPais"></param>
        /// <param name="idModalidadCurso"></param>
        /// <returns></returns>
        public List<ListaDocumentoFiltroDTO> ObtenerListaDocumentoFiltro(int idTipoPersona, int idPais, int idModalidadCurso)
        {
            try
            {
                var _query = "SELECT IdDocumento, NombreDocumento, EsActivo  FROM conf.V_ObtenerDocumentoFiltro WHERE IdTipoPersona = @idTipoPersona AND IdPais = @idPais AND IdModalidadCurso = @idModalidadCurso AND  EstadoDocumentoConfiguracionRecepcion = 1 AND EstadoDocumento = 1";
                var queryPEspecifico = _dapper.QueryDapper(_query, new { idTipoPersona, idPais, idModalidadCurso });
                return JsonConvert.DeserializeObject<List<ListaDocumentoFiltroDTO>>(queryPEspecifico);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
