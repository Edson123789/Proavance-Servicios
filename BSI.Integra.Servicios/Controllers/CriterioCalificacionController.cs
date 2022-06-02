using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Servicios.Helpers;
using FluentValidation;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.DTOs;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/CriterioCalificacion")]
    public class CriterioCalificacionController : BaseController<TCriterioCalificacion, ValidadorCriterioCalificacionDTO>
    {
        public CriterioCalificacionController(IIntegraRepository<TCriterioCalificacion> repositorio, ILogger<BaseController<TCriterioCalificacion, ValidadorCriterioCalificacionDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
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
                CriterioCalificacionRepositorio _repCriterioCalificacion = new CriterioCalificacionRepositorio();
                return Ok(_repCriterioCalificacion.GetBy( x => x.Estado == true, x=> new { x.Id, x.Sigla }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoReporteSeguimiento()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CriterioCalificacionRepositorio _repCriterioCalificacion = new CriterioCalificacionRepositorio();
                var criterios = _repCriterioCalificacion.GetBy(x => x.Estado == true, x => new { x.Id, x.Sigla });

                List<CriterioReporteSeguimientoDTO> lista = new List<CriterioReporteSeguimientoDTO>(); 
                foreach (var item in criterios)
                {
                    CriterioReporteSeguimientoDTO nuevo = new CriterioReporteSeguimientoDTO();
                    switch (item.Sigla)
                    {
                        case "D":
                            nuevo.Id = item.Id;
                            nuevo.Sigla = "Se acepta(Convenio de Voz)";
                            lista.Add(nuevo);
                            break;
                        case "DE":
                            nuevo.Id = item.Id;
                            nuevo.Sigla = "Se acepta(Convenio Firmado)";
                            lista.Add(nuevo);
                            break;
                        case "EM":
                            nuevo.Id = item.Id;
                            nuevo.Sigla = "Empresa";
                            lista.Add(nuevo);
                            break;
                        case "OBS":
                            nuevo.Id = item.Id;
                            nuevo.Sigla = "Observaciones";
                            lista.Add(nuevo);
                            break;
                    }
                }
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarCriterioCalificacionMatricula([FromBody] ReporteSeguiminetoCriterioObservacionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ControlDocAlumnoRepositorio _repControlDocAlumno = new ControlDocAlumnoRepositorio();
                if (dto != null)
                {
                    var controldoc = _repControlDocAlumno.FirstBy(w => w.IdMatriculaCabecera == dto.IdMatriculaCabecera);
                    controldoc.IdCriterioCalificacion = dto.IdTabla;
                    controldoc.UsuarioModificacion = dto.Usuario;
                    controldoc.FechaModificacion = DateTime.Now;
                    controldoc.FechaEntregaDocumento = DateTime.Now;
                    _repControlDocAlumno.Update(controldoc);
                    return Ok(dto);
                }
                else {
                    return BadRequest("No existe Valores en el objeto");
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarMatriculaObservacionMatricula([FromBody] ReporteSeguiminetoCriterioObservacionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ControlDocAlumnoRepositorio _repControlDocAlumno = new ControlDocAlumnoRepositorio();
                if (dto != null)
                {
                    var controldoc = _repControlDocAlumno.FirstBy(w => w.IdMatriculaCabecera == dto.IdMatriculaCabecera);
                    controldoc.IdMatriculaObservacion = dto.IdTabla;
                    controldoc.UsuarioModificacion = dto.Usuario;
                    controldoc.FechaModificacion = DateTime.Now;
                    _repControlDocAlumno.Update(controldoc);
                    return Ok(dto);
                }
                else
                {
                    return BadRequest("No existe Valores en el objeto");
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

    public class ValidadorCriterioCalificacionDTO : AbstractValidator<TCriterioCalificacion>
    {
        public static ValidadorCriterioCalificacionDTO Current = new ValidadorCriterioCalificacionDTO();
        public ValidadorCriterioCalificacionDTO()
        {
        }
    }
}
