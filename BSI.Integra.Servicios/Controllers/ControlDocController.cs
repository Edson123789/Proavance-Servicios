using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ControlDocumento")]
    public class ControlDocController : BaseController<TControlDoc, ValidadorControlDocDTO>
    {
        public ControlDocController(IIntegraRepository<TControlDoc> repositorio, ILogger<BaseController<TControlDoc, ValidadorControlDocDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerAlumnoPorValor([FromBody] Dictionary<string, string> Valor)
        {
            try
            {
                if (Valor != null && Valor.Count > 0)
                {
                    AlumnoRepositorio _repAlumno = new AlumnoRepositorio();
                    var alumnosTemp = _repAlumno.ObtenerTodoFiltroAutoComplete(Valor["Valor"]);
                    return Ok(alumnosTemp);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCodigoMatricula([FromBody] Dictionary<string, string> Valor) //CodigoMatricula
        {
            try
            {
                if (Valor != null && Valor.Count > 0)
                {
                    MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                    return Ok(_repMatriculaCabecera.GetBy(x => x.CodigoMatricula.Contains(Valor["CodigoMatricula"]), x => new { x.Id, x.CodigoMatricula }));
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerDocumentosFiltrado([FromBody] FiltroControlDocumentoDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ControlDocRepositorio _repControlDoc = new ControlDocRepositorio();
                return Ok(_repControlDoc.ObtenerDocumentosFiltrado(Filtro));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerDocumentosPorMatricula(int IdMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ControlDocRepositorio _repControlDoc = new ControlDocRepositorio();
                return Ok(_repControlDoc.ObtenerDocumentosPorMatriculaCabecera(IdMatriculaCabecera));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarControlDocumento([FromBody] ControlDocumentoDTO ControlDocumentoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ControlDocRepositorio _repControlDoc = new ControlDocRepositorio();
                if (_repControlDoc.Exist(ControlDocumentoDTO.IdControlDoc))
                {
                    var controlDocBO = _repControlDoc.FirstById(ControlDocumentoDTO.IdControlDoc);
                    controlDocBO.FechaModificacion = DateTime.Now;
                    controlDocBO.UsuarioModificacion = ControlDocumentoDTO.NombreUsuario;
                    controlDocBO.EstadoDocumento = ControlDocumentoDTO.EstadoDocumento;
                    _repControlDoc.Update(controlDocBO);
                    return Ok(controlDocBO);
                }
                else {
                   return BadRequest("Registro no existente");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
    public class ValidadorControlDocDTO : AbstractValidator<TControlDoc>
    {
        public static ValidadorControlDocDTO Current = new ValidadorControlDocDTO();
        public ValidadorControlDocDTO()
        {
        }
    }
}
