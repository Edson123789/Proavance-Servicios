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
    [Route("api/ControlDocAlumno")]
    public class ControlDocAlumnoController : BaseController<TControlDocAlumno, ValidadorControlDocAlumnoDTO>
    {
        public ControlDocAlumnoController(IIntegraRepository<TControlDocAlumno> repositorio, ILogger<BaseController<TControlDocAlumno, ValidadorControlDocAlumnoDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
        
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarControlDocumentoAlumno([FromBody] ControlDocumentoAlumnoDTO ControlDocumentoAlumnoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ControlDocAlumnoRepositorio _repControlDocAlumno = new ControlDocAlumnoRepositorio();
                if (_repControlDocAlumno.Exist(ControlDocumentoAlumnoDTO.IdControlDocAlumno))
                {
                    var controlDocAlumnoBO = _repControlDocAlumno.FirstById(ControlDocumentoAlumnoDTO.IdControlDocAlumno);
                    controlDocAlumnoBO.FechaModificacion = DateTime.Now;
                    controlDocAlumnoBO.UsuarioModificacion = ControlDocumentoAlumnoDTO.NombreUsuario;
                    controlDocAlumnoBO.IdCriterioCalificacion = ControlDocumentoAlumnoDTO.IdCriterioCalificacion;
                    controlDocAlumnoBO.QuienEntrego = ControlDocumentoAlumnoDTO.QuienEntrego;
                    controlDocAlumnoBO.FechaEntregaDocumento = ControlDocumentoAlumnoDTO.FechaEntregaDocumento;
                    controlDocAlumnoBO.Observaciones = ControlDocumentoAlumnoDTO.Observaciones;
                    _repControlDocAlumno.Update(controlDocAlumnoBO);
                    return Ok(controlDocAlumnoBO);
                }
                else
                {
                    return BadRequest("Registro no existente");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

    public class ValidadorControlDocAlumnoDTO : AbstractValidator<TControlDocAlumno>
    {
        public static ValidadorControlDocAlumnoDTO Current = new ValidadorControlDocAlumnoDTO();
        public ValidadorControlDocAlumnoDTO()
        {
        }
    }
}
