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
    [Route("api/Documento")]
    public class DocumentoController : BaseController<TDocumento, ValidadoDocumentoDTO>
    {
        public DocumentoController(IIntegraRepository<TDocumento> repositorio, ILogger<BaseController<TDocumento, ValidadoDocumentoDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
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
                DocumentoRepositorio _repDocumento = new DocumentoRepositorio();
                return Ok(_repDocumento.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] DocumentoDTO Documento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DocumentoRepositorio _repDocumento = new DocumentoRepositorio(); 

                DocumentoBO documentoBO = new DocumentoBO();
                documentoBO.Nombre = Documento.Nombre;
                documentoBO.Descripcion = Documento.Descripcion;
                documentoBO.Estado = true;
                documentoBO.UsuarioCreacion = Documento.Usuario;
                documentoBO.UsuarioModificacion = Documento.Usuario;
                documentoBO.FechaCreacion = DateTime.Now;
                documentoBO.FechaModificacion = DateTime.Now;

                return Ok(_repDocumento.Insert(documentoBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] DocumentoDTO Documento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DocumentoRepositorio _repDocumento = new DocumentoRepositorio(); 

                DocumentoBO documentoBO = _repDocumento.FirstById(Documento.Id);
                documentoBO.Nombre = Documento.Nombre;
                documentoBO.Descripcion = Documento.Descripcion;
                documentoBO.UsuarioModificacion = Documento.Usuario;
                documentoBO.FechaModificacion = DateTime.Now;

                return Ok(_repDocumento.Update(documentoBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] DocumentoDTO Documento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DocumentoRepositorio _repDocumento = new DocumentoRepositorio(); 

                    if (_repDocumento.Exist(Documento.Id))
                    {
                        _repDocumento.Delete(Documento.Id, Documento.Usuario);
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
	}
	
	public class ValidadoDocumentoDTO : AbstractValidator<TDocumento>
    {
        public static ValidadoDocumentoDTO Current = new ValidadoDocumentoDTO();
        public ValidadoDocumentoDTO()
        {
            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio");
        }
    }
}
