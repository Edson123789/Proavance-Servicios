using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using static BSI.Integra.Aplicacion.Transversal.Helper.SubirArchivo;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/DocumentoRecepcionado")]
    public class DocumentoRecepcionadoController : BaseController<TDocumentoRecepcionado, ValidadoDocumentoRecepcionadoDTO>
    {
        public DocumentoRecepcionadoController(IIntegraRepository<TDocumentoRecepcionado> repositorio, ILogger<BaseController<TDocumentoRecepcionado, ValidadoDocumentoRecepcionadoDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboTipoPersona()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoPersonaRepositorio _repTipoPersona = new TipoPersonaRepositorio();
                return Ok(_repTipoPersona.ObtenerListaTipoPersona());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboPais()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PaisRepositorio _repPais = new PaisRepositorio();
                return Ok(_repPais.ObtenerListaPais());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboModalidadCurso()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ModalidadCursoRepositorio _repModalidadCurso = new ModalidadCursoRepositorio();
                return Ok(_repModalidadCurso.ObtenerModalidadCursoFiltro());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DocumentoRecepcionadoRepositorio _repDocumentoRecepcionado = new DocumentoRecepcionadoRepositorio();
                return Ok(_repDocumentoRecepcionado.ObtenerTodoDocumentoRecepcionado());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar(DocumentoRecepcionadoDTO DocumentoRecepcionado, [FromForm] IFormFile File)
        {
            var parametros = "";      
            var parametros1 = "";      
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                parametros = JsonConvert.SerializeObject(DocumentoRecepcionado);
                parametros1 = JsonConvert.SerializeObject(File);
                DocumentoRecepcionadoRepositorio _repDocumentoRecepcionado = new DocumentoRecepcionadoRepositorio();
                DocumentoRecepcionadoBO documentoRecepcionadoBO = new DocumentoRecepcionadoBO();
                SubirArchivo archivo = new SubirArchivo();

                documentoRecepcionadoBO.IdPersonaTipoPersona = DocumentoRecepcionado.IdPersonaTipoPersona;
                documentoRecepcionadoBO.IdDocumento = DocumentoRecepcionado.IdDocumento;
                documentoRecepcionadoBO.IdPespecifico = DocumentoRecepcionado.IdPespecifico;
                documentoRecepcionadoBO.NombreArchivo = string.Concat(DocumentoRecepcionado.IdPersonaTipoPersona, "-", DocumentoRecepcionado.IdPespecifico,"-", DocumentoRecepcionado.IdPespecifico,"-", File.FileName);
                documentoRecepcionadoBO.Ruta = @"C:\Temp\RegistroDocumento\";
                documentoRecepcionadoBO.MimeTypeArchivo = File.ContentType;
                documentoRecepcionadoBO.Estado = true;
                documentoRecepcionadoBO.UsuarioCreacion = DocumentoRecepcionado.Usuario;
                documentoRecepcionadoBO.UsuarioModificacion = DocumentoRecepcionado.Usuario;
                documentoRecepcionadoBO.FechaCreacion = DateTime.Now;
                documentoRecepcionadoBO.FechaModificacion = DateTime.Now;

                var rutaArchivo = documentoRecepcionadoBO.Ruta + documentoRecepcionadoBO.NombreArchivo;
                archivo.AlmacenarArchivo(rutaArchivo, TipoInteraccionArchivo.Crear, File);

                return Ok(_repDocumentoRecepcionado.Insert(documentoRecepcionadoBO));
            }
            catch (Exception ex)
            {
                return BadRequest(parametros+ "XXXXXXXXXXXXXXX"+ parametros1);
            }                      
        }

       

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar(DocumentoRecepcionadoDTO DocumentoRecepcionado, [FromForm] IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DocumentoRecepcionadoRepositorio _repDocumentoRecepcionado = new DocumentoRecepcionadoRepositorio();
                SubirArchivo archivo = new SubirArchivo();

                DocumentoRecepcionadoBO documentoRecepcionadoBO = _repDocumentoRecepcionado.FirstById(DocumentoRecepcionado.Id);

                if (documentoRecepcionadoBO.NombreArchivo != string.Concat(DocumentoRecepcionado.IdPersonaTipoPersona, "-", file.FileName))
                {
                    archivo.AlmacenarArchivo(documentoRecepcionadoBO.NombreArchivo, TipoInteraccionArchivo.Eliminar);
                }

                //documentoRecepcionadoBO.IdPersonaTipoPersona = DocumentoRecepcionado.IdPersonaTipoPersona;
                //documentoRecepcionadoBO.IdDocumento = DocumentoRecepcionado.IdDocumento;
                //documentoRecepcionadoBO.IdPespecifico = DocumentoRecepcionado.IdPespecifico;
                documentoRecepcionadoBO.NombreArchivo = string.Concat(DocumentoRecepcionado.IdPersonaTipoPersona, "-", file.FileName);
                documentoRecepcionadoBO.Ruta = @"C:\Temp\RegistroDocumento\";
                documentoRecepcionadoBO.MimeTypeArchivo = file.ContentType;
                documentoRecepcionadoBO.UsuarioModificacion = DocumentoRecepcionado.Usuario;
                documentoRecepcionadoBO.FechaModificacion = DateTime.Now;

            
                archivo.AlmacenarArchivo(documentoRecepcionadoBO.NombreArchivo + documentoRecepcionadoBO.Ruta, TipoInteraccionArchivo.Actualizar, file);

                return Ok( _repDocumentoRecepcionado.Update(documentoRecepcionadoBO));
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody]  DocumentoRecepcionadoDTO DocumentoRecepcionado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DocumentoRecepcionadoRepositorio _repDocumentoRecepcionado = new DocumentoRecepcionadoRepositorio();
                    SubirArchivo archivo = new SubirArchivo();
                    DocumentoRecepcionadoBO documentoRecepcionadoBO = _repDocumentoRecepcionado.FirstById(DocumentoRecepcionado.Id);

                    if (_repDocumentoRecepcionado.Exist(DocumentoRecepcionado.Id))
                    {
                        _repDocumentoRecepcionado.Delete(DocumentoRecepcionado.Id, DocumentoRecepcionado.Usuario);
                        try
                        {
                            archivo.AlmacenarArchivo(documentoRecepcionadoBO.NombreArchivo, TipoInteraccionArchivo.Eliminar);
                        }
                        catch (Exception)
                        {
                        }
                        scope.Complete();
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("Registro no existente");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdDocumentoRecepcionado}")]
        [HttpGet]
        public ActionResult DescargarDocumento(int IdDocumentoRecepcionado)
        
{
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DocumentoRecepcionadoRepositorio _repDocumentoRecepcionado = new DocumentoRecepcionadoRepositorio();
                if (_repDocumentoRecepcionado.Exist(IdDocumentoRecepcionado))
                {
                    var documentoRecepcionadoBO = _repDocumentoRecepcionado.FirstById(IdDocumentoRecepcionado);
                    var pathFile = string.Concat(documentoRecepcionadoBO.Ruta, documentoRecepcionadoBO.NombreArchivo);
                    if (System.IO.File.Exists(pathFile))
                    {
                        byte[] archivo = System.IO.File.ReadAllBytes(pathFile);
                        return File(archivo, documentoRecepcionadoBO.MimeTypeArchivo, documentoRecepcionadoBO.NombreArchivo);
                    }
                    else {
                        return BadRequest("El archivo " + documentoRecepcionadoBO.NombreArchivo + " no existe ");
                    }
                }
                else
                {
                    return BadRequest("Registro no existente");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdTipoPersona}/{IdPEspecifico}")]
        [HttpGet]
        public ActionResult ObtenerPersonaFiltro(int IdTipoPersona, int IdPEspecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
                return Ok(new { listadoPersonas = _repPEspecifico.ObtenerListaPersonaPorPEspecifico(IdTipoPersona, IdPEspecifico) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdTipoPersona}/{IdPais}/{IdModalidadCurso}")]
        [HttpGet]
        public ActionResult ObtenerDocumentoFiltro(int IdTipoPersona,int IdPais, int IdModalidadCurso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DocumentoConfiguracionRecepcionRepositorio _repDocumentoConfiguracionRecepcion = new DocumentoConfiguracionRecepcionRepositorio();
                return Ok(new { listadoDocumentos = _repDocumentoConfiguracionRecepcion.ObtenerListaDocumentoFiltro(IdTipoPersona, IdPais, IdModalidadCurso)});
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

       

        [Route("[action]/{IdPEspecifico}")]
        [HttpGet]
        public ActionResult ObtenerPaisPorIdPEspecifico(int IdPEspecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
                return Ok(_repPEspecifico.ObtenerPaisPorIdPEspecifico(IdPEspecifico));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdPersonaTipoPersona}")]
        [HttpGet]
        public ActionResult ObtenerDocumentoPorIdPersonaTipoPersona(int IdPersonaTipoPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SubirArchivo archivo = new SubirArchivo();
                DocumentoRecepcionadoRepositorio _repDocumentoRecepcionado = new DocumentoRecepcionadoRepositorio();
                TodoDocumentoRecepcionadoDTO todoDocumentoRecepcionado = new TodoDocumentoRecepcionadoDTO();
                var ListaDocumentoPersona = _repDocumentoRecepcionado.ObtenerDocumentoPorIdPersonaTipoPersona(IdPersonaTipoPersona);
                foreach (var item in ListaDocumentoPersona)
                {
                    item.ExisteArchivo = archivo.ExisteArchivo(todoDocumentoRecepcionado.NombreArchivo);
                }
                return Ok(ListaDocumentoPersona);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerTodosPEspecificoAutoCompleto([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                PespecificoRepositorio personalRepositorio = new PespecificoRepositorio();
                if (Filtros.Count() > 0 && (Filtros != null && Filtros["Filtros"] != null))
                {
                    var lista = personalRepositorio.ObtenerListaPEspecificoAutocompleto(Filtros["Filtros"].ToString());
                    return Ok(lista);
                }
                return Ok(new { });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }

    public class ValidadoDocumentoRecepcionadoDTO : AbstractValidator<TDocumentoRecepcionado>
    {
        public static ValidadoDocumentoRecepcionadoDTO Current = new ValidadoDocumentoRecepcionadoDTO();
        public ValidadoDocumentoRecepcionadoDTO()
        {
        }
    }
}
