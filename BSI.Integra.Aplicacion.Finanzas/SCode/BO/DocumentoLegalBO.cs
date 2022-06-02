using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class DocumentoLegalBO:BaseBO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPais { get; set; }
        public string Url { get; set; }
        public bool? VisualizarAgenda { get; set; }
        public bool? DescargarAgenda { get; set; }
        public int? IdMigracion { get; set; }
        public string Roles { get; set; }

        public int IdTableroComercialCategoriaAsesor { get; set; }
        private readonly integraDBContext _integraDBContext;
        //private readonly PuestoTrabajoRemuneracionRepositorio _repPuestoTrabajoRemuneracion;
        //private readonly PuestoTrabajoRemuneracionDetalleRepositorio _repPuestoTrabajoRemuneracionDetalle;
        public DocumentoLegalBO()
        {
            //_repPuestoTrabajoRemuneracion = new PuestoTrabajoRemuneracionRepositorio();
        }

        public DocumentoLegalBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            //_repPuestoTrabajoRemuneracion = new PuestoTrabajoRemuneracionRepositorio(_integraDBContext);
            //_repPuestoTrabajoRemuneracionDetalle = new PuestoTrabajoRemuneracionDetalleRepositorio(_integraDBContext);
        }
        /// Autor: Jashin Salazar
        /// Fecha: 22/07/2021
        /// Version: 1.0
        /// <summary>
        /// Convierte el archivo a bytes para su descarga
        /// </summary>
        /// <returns> List<DocumentoLegalDTO> </returns>
        public byte[] DescargarArchivoBlob(string url)
        {
            byte[] documento = null;
            WebClient myWebClient = new WebClient();
            documento = myWebClient.DownloadData(url);
            return documento;
        }
        /// Autor: Jashin Salazar
        /// Fecha: 22/07/2021
        /// Version: 1.0
        /// <summary>
        /// Inserta los documentos legales 
        /// </summary>
        /// <returns> List<DocumentoLegalDTO> </returns>
        public bool InsertarDocumentoLegal(DocumentoLegalDTO DocumentoLegal)
        {
            try
            {
                DocumentoLegalAreaTrabajoRepositorio _repAreaDocumento = new DocumentoLegalAreaTrabajoRepositorio(_integraDBContext);
                DocumentoLegalPaisRepositorio _repPaisDocumento = new DocumentoLegalPaisRepositorio(_integraDBContext);
                DocumentoLegalRepositorio _repDocumento = new DocumentoLegalRepositorio(_integraDBContext);
                var Roles = string.Join(",", DocumentoLegal.Roles);
                DocumentoLegalBO documento = new DocumentoLegalBO()
                {
                    Nombre = DocumentoLegal.Nombre,
                    Descripcion = DocumentoLegal.Descripcion,
                    IdPais = DocumentoLegal.IdPais,
                    Url = DocumentoLegal.Url,
                    VisualizarAgenda = DocumentoLegal.VisualizarAgenda,
                    DescargarAgenda = DocumentoLegal.DescargarAgenda,
                    Estado = true,
                    UsuarioCreacion = DocumentoLegal.Usuario,
                    UsuarioModificacion = DocumentoLegal.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Roles = Roles
                };
                _repDocumento.Insert(documento);
                var idDocumento = documento.Id;
                foreach (var item in DocumentoLegal.Areas)
                {
                    DocumentoLegalAreaTrabajoBO areaDocumento = new DocumentoLegalAreaTrabajoBO()
                    {
                        IdDocumentoLegal = idDocumento,
                        IdPersonalAreaTrabajo = item,
                        Estado = true,
                        UsuarioCreacion = DocumentoLegal.Usuario,
                        UsuarioModificacion = DocumentoLegal.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    _repAreaDocumento.Insert(areaDocumento);
                }
                foreach (var item in DocumentoLegal.Paises)
                {
                    DocumentoLegalPaisBO paisDocumento = new DocumentoLegalPaisBO();
                    paisDocumento.IdDocumentoLegal = idDocumento;
                    paisDocumento.IdPais = item;
                    paisDocumento.Estado = true;
                    paisDocumento.UsuarioCreacion = DocumentoLegal.Usuario;
                    paisDocumento.UsuarioModificacion = DocumentoLegal.Usuario;
                    paisDocumento.FechaCreacion = DateTime.Now;
                    paisDocumento.FechaModificacion = DateTime.Now;
                    _repPaisDocumento.Insert(paisDocumento);
                }
                return true;
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
        /// Actualiza los documentos legales 
        /// </summary>
        /// <returns> bool </returns>
        public bool ActualizarDocumentoLegal(DocumentoLegalDTO DocumentoLegal)
        {
            try
            {
                DocumentoLegalAreaTrabajoRepositorio _repAreaDocumento = new DocumentoLegalAreaTrabajoRepositorio(_integraDBContext);
                DocumentoLegalPaisRepositorio _repPaisDocumento = new DocumentoLegalPaisRepositorio(_integraDBContext);
                DocumentoLegalRepositorio _repDocumento = new DocumentoLegalRepositorio(_integraDBContext);
                DocumentoLegalBO documento;
                var Roles = string.Join(",", DocumentoLegal.Roles);
                documento = _repDocumento.FirstById(DocumentoLegal.Id);
                documento.Nombre = DocumentoLegal.Nombre;
                documento.Descripcion = DocumentoLegal.Descripcion;
                documento.IdPais = DocumentoLegal.IdPais;
                documento.Url = DocumentoLegal.Url;
                documento.VisualizarAgenda = DocumentoLegal.VisualizarAgenda;
                documento.DescargarAgenda = DocumentoLegal.DescargarAgenda;
                documento.UsuarioModificacion = DocumentoLegal.Usuario;
                documento.FechaModificacion = DateTime.Now;
                documento.Roles = Roles;
                _repDocumento.Update(documento);
                var idDocumento = documento.Id;
                var areasDocumentoBD = _repAreaDocumento.GetBy(x => x.IdDocumentoLegal == idDocumento).Select(z => z.IdPersonalAreaTrabajo).ToList();
                if (areasDocumentoBD.Count < DocumentoLegal.Areas.Count)
                {
                    var diferencia = DocumentoLegal.Areas.Except(areasDocumentoBD).ToList();
                    //InsertarDiferencia
                    foreach (var item in diferencia)
                    {
                        DocumentoLegalAreaTrabajoBO areaDocumento = new DocumentoLegalAreaTrabajoBO()
                        {
                            IdDocumentoLegal = idDocumento,
                            IdPersonalAreaTrabajo = item,
                            Estado = true,
                            UsuarioCreacion = DocumentoLegal.Usuario,
                            UsuarioModificacion = DocumentoLegal.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        _repAreaDocumento.Insert(areaDocumento);
                    }
                }
                else if (areasDocumentoBD.Count > DocumentoLegal.Areas.Count)
                {
                    var diferencia = areasDocumentoBD.Except(DocumentoLegal.Areas).ToList();
                    //Eliminar diferencia
                    var eliminarItem = _repAreaDocumento.FirstBy(x => x.IdDocumentoLegal == idDocumento && x.IdPersonalAreaTrabajo == diferencia.ElementAt(0));
                    _repAreaDocumento.Delete(eliminarItem.Id, DocumentoLegal.Usuario);
                }
                else if (areasDocumentoBD.Count == 1 && DocumentoLegal.Areas.Count == 1)
                {
                    var diferenciaInsertar = DocumentoLegal.Areas.ToList().Except(areasDocumentoBD).ToList();
                    if (diferenciaInsertar.Count > 0)
                    {
                        var diferenciaEliminar = areasDocumentoBD.Except(DocumentoLegal.Areas).ToList();
                        var eliminarItem = _repAreaDocumento.FirstBy(x => x.IdDocumentoLegal == idDocumento && x.IdPersonalAreaTrabajo == diferenciaEliminar.ElementAt(0));
                        _repAreaDocumento.Delete(eliminarItem.Id, DocumentoLegal.Usuario);
                        foreach (var item in diferenciaInsertar)
                        {
                            DocumentoLegalAreaTrabajoBO areaDocumento = new DocumentoLegalAreaTrabajoBO()
                            {
                                IdDocumentoLegal = idDocumento,
                                IdPersonalAreaTrabajo = item,
                                Estado = true,
                                UsuarioCreacion = DocumentoLegal.Usuario,
                                UsuarioModificacion = DocumentoLegal.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            _repAreaDocumento.Insert(areaDocumento);
                        }
                    }
                }
                var paiseDocumentoBD = _repPaisDocumento.GetBy(x => x.IdDocumentoLegal == idDocumento).Select(z => z.Id).ToList();
                _repPaisDocumento.Delete(paiseDocumentoBD, DocumentoLegal.Usuario);
                foreach(var item in DocumentoLegal.Paises)
                {
                    DocumentoLegalPaisBO paisDocumento = new DocumentoLegalPaisBO();
                    paisDocumento.IdDocumentoLegal = idDocumento;
                    paisDocumento.IdPais = item;
                    paisDocumento.Estado = true;
                    paisDocumento.UsuarioCreacion = DocumentoLegal.Usuario;
                    paisDocumento.UsuarioModificacion = DocumentoLegal.Usuario;
                    paisDocumento.FechaCreacion = DateTime.Now;
                    paisDocumento.FechaModificacion = DateTime.Now;
                    _repPaisDocumento.Insert(paisDocumento);
                }
                return true;
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
        /// Elemina los documentos legales
        /// </summary>
        /// <returns> bool </returns>
        public bool EliminarDocumentoLegal(int IdDocumentoLegal, string Usuario)
        {
            try
            {
                DocumentoLegalAreaTrabajoRepositorio _repAreaDocumento = new DocumentoLegalAreaTrabajoRepositorio(_integraDBContext);
                DocumentoLegalRepositorio _repDocumento = new DocumentoLegalRepositorio(_integraDBContext);
                var areasDocumentoBD = _repAreaDocumento.GetBy(x => x.IdDocumentoLegal == IdDocumentoLegal).ToList();
                foreach (var item in areasDocumentoBD)
                {
                    _repAreaDocumento.Delete(item.Id, Usuario);
                }
                _repDocumento.Delete(IdDocumentoLegal, Usuario);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
