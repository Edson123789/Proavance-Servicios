using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class DocumentoLegalRepositorio: BaseRepository<TDocumentoLegal, DocumentoLegalBO>
    {
        private readonly integraDBContext _integraDBContext;
        #region Metodos Base
        public DocumentoLegalRepositorio() : base()
        {
        }
        public DocumentoLegalRepositorio(integraDBContext contexto) : base(contexto)
        {
            _integraDBContext = contexto;
        }
        public IEnumerable<DocumentoLegalBO> GetBy(Expression<Func<TDocumentoLegal, bool>> filter)
        {
            IEnumerable<TDocumentoLegal> listado = base.GetBy(filter);
            List<DocumentoLegalBO> listadoBO = new List<DocumentoLegalBO>();
            foreach (var itemEntidad in listado)
            {
                DocumentoLegalBO objetoBO = Mapper.Map<TDocumentoLegal, DocumentoLegalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public DocumentoLegalBO FirstById(int id)
        {
            try
            {
                TDocumentoLegal entidad = base.FirstById(id);
                DocumentoLegalBO objetoBO = new DocumentoLegalBO();
                Mapper.Map<TDocumentoLegal, DocumentoLegalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DocumentoLegalBO FirstBy(Expression<Func<TDocumentoLegal, bool>> filter)
        {
            try
            {
                TDocumentoLegal entidad = base.FirstBy(filter);
                DocumentoLegalBO objetoBO = Mapper.Map<TDocumentoLegal, DocumentoLegalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(DocumentoLegalBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TDocumentoLegal entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<DocumentoLegalBO> listadoBO)
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

        public bool Update(DocumentoLegalBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TDocumentoLegal entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<DocumentoLegalBO> listadoBO)
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
        private void AsignacionId(TDocumentoLegal entidad, DocumentoLegalBO objetoBO)
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

        private TDocumentoLegal MapeoEntidad(DocumentoLegalBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TDocumentoLegal entidad = new TDocumentoLegal();
                entidad = Mapper.Map<DocumentoLegalBO, TDocumentoLegal>(objetoBO,
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
        /// Autor: Jashin Salazar
        /// Fecha: 22/07/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene los documentos legales y los agrupa para mostrar las areas
        /// </summary>
        /// <returns> List<DocumentoLegalDTO> </returns>
        public List<DocumentoLegalV2DTO> ObtenerDocumentoLegal()
        {
            try
            {
                List<DocumentoLegalV2DTO> salidaDocumentos = new List<DocumentoLegalV2DTO>();
                List<DocumentoLegalV2DTO> documentosLegales = new List<DocumentoLegalV2DTO>();
                var query = "SELECT * FROM fin.V_ObtenerDocumentoLegal ORDER BY Id desc";
                var documentoBD = _dapper.QueryDapper(query, null);
                if (!documentoBD.Contains("[]") && !string.IsNullOrEmpty(documentoBD))
                {
                    documentosLegales = JsonConvert.DeserializeObject<List<DocumentoLegalV2DTO>>(documentoBD);
                }
                salidaDocumentos = documentosLegales
                    .GroupBy(x => new { x.Id, x.Nombre, x.Descripcion, x.Url, x.VisualizarAgenda, x.DescargarAgenda, x.Roles })
                    .Select(g =>
                    new DocumentoLegalV2DTO
                    {
                        Id = g.Key.Id,
                        Nombre = g.Key.Nombre,
                        Descripcion = g.Key.Descripcion,
                        Url = g.Key.Url,
                        VisualizarAgenda = g.Key.VisualizarAgenda,
                        DescargarAgenda = g.Key.DescargarAgenda,
                        Roles = g.Key.Roles,
                        Areas = documentosLegales.Where(y => y.Id == g.Key.Id).Select(z => z.Area).ToList()
                    }).ToList();
                query = "SELECT * FROM fin.T_DocumentoLegalPais WHERE IdDocumentoLegal=@Id AND Estado=1";
                foreach (var item in salidaDocumentos)
                {
                    var paisesBD = _dapper.QueryDapper(query, new { Id =item.Id});
                    if (!paisesBD.Contains("[]") && !string.IsNullOrEmpty(paisesBD))
                    {
                        var resultado = JsonConvert.DeserializeObject<List<DocumentoLegalPaisDTO>>(paisesBD);
                        item.PaisesBD = resultado;
                    }
                }
                
                return salidaDocumentos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 22/07/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene los documentos legales para agenda
        /// </summary>
        /// <returns> List<DocumentoLegalDTO> </returns>
        public List<DocumentoLegalDTO> ObtenerDocumentoLegalAgenda(int area, string rol, int idpais)
        {
            try
            {
                List<DocumentoLegalDTO> documentosLegales = new List<DocumentoLegalDTO>();
                var query = "SELECT * FROM fin.V_ObtenerDocumentoLegal WHERE Area="+area+ " AND IdPais IN("+idpais+",0) AND Roles LIKE '%"+rol+"%' ORDER BY Id DESC";
                var documentoBD = _dapper.QueryDapper(query, null);
                if (!documentoBD.Contains("[]") && !string.IsNullOrEmpty(documentoBD))
                {
                    documentosLegales = JsonConvert.DeserializeObject<List<DocumentoLegalDTO>>(documentoBD);
                }
                DocumentoLegalBO doc = new DocumentoLegalBO();
                foreach(var item in documentosLegales)
                {
                    if (item.DescargarAgenda==true)
                    {
                        item.DocumentoByte = doc.DescargarArchivoBlob(item.Url);
                    }
                }
                return documentosLegales;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
    }
}
