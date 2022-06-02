using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/DocumentoConfiguracionRecepcion")]
    public class DocumentoConfiguracionRecepcionController : BaseController<TDocumentoConfiguracionRecepcion, ValidadoDocumentoConfiguracionRecepcionDTO>
    {
        public DocumentoConfiguracionRecepcionController(IIntegraRepository<TDocumentoConfiguracionRecepcion> repositorio, ILogger<BaseController<TDocumentoConfiguracionRecepcion, ValidadoDocumentoConfiguracionRecepcionDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
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
                DocumentoConfiguracionRecepcionRepositorio _repDocumentoConfiguracionRecepcion = new DocumentoConfiguracionRecepcionRepositorio();
                return Ok(_repDocumentoConfiguracionRecepcion.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] DocumentoConfiguracionRecepcionDTO DocumentoConfiguracionRecepcion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DocumentoConfiguracionRecepcionRepositorio _repDocumentoConfiguracionRecepcion = new DocumentoConfiguracionRecepcionRepositorio();

                DocumentoConfiguracionRecepcionBO DocumentoConfiguracionRecepcionBO = new DocumentoConfiguracionRecepcionBO();
                DocumentoConfiguracionRecepcionBO.IdTipoPersona = DocumentoConfiguracionRecepcion.IdTipoPersona;
                DocumentoConfiguracionRecepcionBO.IdPais = DocumentoConfiguracionRecepcion.IdPais;
                DocumentoConfiguracionRecepcionBO.IdDocumento = DocumentoConfiguracionRecepcion.IdDocumento;
                DocumentoConfiguracionRecepcionBO.IdModalidadCurso = DocumentoConfiguracionRecepcion.IdModalidadCurso;
                DocumentoConfiguracionRecepcionBO.Padre = DocumentoConfiguracionRecepcion.Padre;
                DocumentoConfiguracionRecepcionBO.EsActivo = DocumentoConfiguracionRecepcion.EsActivo;
                DocumentoConfiguracionRecepcionBO.Estado = true;
                DocumentoConfiguracionRecepcionBO.UsuarioCreacion = DocumentoConfiguracionRecepcion.Usuario;
                DocumentoConfiguracionRecepcionBO.UsuarioModificacion = DocumentoConfiguracionRecepcion.Usuario;
                DocumentoConfiguracionRecepcionBO.FechaCreacion = DateTime.Now;
                DocumentoConfiguracionRecepcionBO.FechaModificacion = DateTime.Now;

                return Ok(_repDocumentoConfiguracionRecepcion.Insert(DocumentoConfiguracionRecepcionBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] DocumentoConfiguracionRecepcionDTO DocumentoConfiguracionRecepcion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DocumentoConfiguracionRecepcionRepositorio _repDocumentoConfiguracionRecepcion = new DocumentoConfiguracionRecepcionRepositorio();

                DocumentoConfiguracionRecepcionBO DocumentoConfiguracionRecepcionBO = _repDocumentoConfiguracionRecepcion.FirstById(DocumentoConfiguracionRecepcion.Id);
                DocumentoConfiguracionRecepcionBO.IdTipoPersona = DocumentoConfiguracionRecepcion.IdTipoPersona;
                DocumentoConfiguracionRecepcionBO.IdPais = DocumentoConfiguracionRecepcion.IdPais;
                DocumentoConfiguracionRecepcionBO.IdDocumento = DocumentoConfiguracionRecepcion.IdDocumento;
                DocumentoConfiguracionRecepcionBO.IdModalidadCurso = DocumentoConfiguracionRecepcion.IdModalidadCurso;
                DocumentoConfiguracionRecepcionBO.Padre = DocumentoConfiguracionRecepcion.Padre;
                DocumentoConfiguracionRecepcionBO.EsActivo = DocumentoConfiguracionRecepcion.EsActivo;
                DocumentoConfiguracionRecepcionBO.UsuarioModificacion = DocumentoConfiguracionRecepcion.Usuario;
                DocumentoConfiguracionRecepcionBO.FechaModificacion = DateTime.Now;

                return Ok(_repDocumentoConfiguracionRecepcion.Update(DocumentoConfiguracionRecepcionBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody]  DocumentoConfiguracionRecepcionDTO DocumentoConfiguracionRecepcion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DocumentoConfiguracionRecepcionRepositorio _repDocumentoConfiguracionRecepcion = new DocumentoConfiguracionRecepcionRepositorio();

                    if (_repDocumentoConfiguracionRecepcion.Exist(DocumentoConfiguracionRecepcion.Id))
                    {
                        _repDocumentoConfiguracionRecepcion.Delete(DocumentoConfiguracionRecepcion.Id, DocumentoConfiguracionRecepcion.Usuario);
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

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosDocumentoPaisTipoPersonaModalidad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoPersonaRepositorio _repTipoPersona = new TipoPersonaRepositorio();
                PaisRepositorio _repPais = new PaisRepositorio();
                DocumentoRepositorio _repDocumento = new DocumentoRepositorio();
                ModalidadCursoRepositorio _repModalidadCurso = new ModalidadCursoRepositorio();

                ComboDocumentoConfiguracionRecepcionDTO comboDocumentoConfiguracionRecepcion = new ComboDocumentoConfiguracionRecepcionDTO();
                comboDocumentoConfiguracionRecepcion.ListaTipoPersona = _repTipoPersona.ObtenerListaTipoPersona();
                comboDocumentoConfiguracionRecepcion.ListaPais = _repPais.ObtenerListaPais();
                comboDocumentoConfiguracionRecepcion.ListaDocumento = _repDocumento.ObtenerListaDocumento();
                comboDocumentoConfiguracionRecepcion.ListaModalidadCurso = _repModalidadCurso.ObtenerModalidadCursoFiltro();

                return Ok(comboDocumentoConfiguracionRecepcion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }

    public class ValidadoDocumentoConfiguracionRecepcionDTO : AbstractValidator<TDocumentoConfiguracionRecepcion>
    {
        public static ValidadoDocumentoConfiguracionRecepcionDTO Current = new ValidadoDocumentoConfiguracionRecepcionDTO();
        public ValidadoDocumentoConfiguracionRecepcionDTO()
        {
        }
    }
}
