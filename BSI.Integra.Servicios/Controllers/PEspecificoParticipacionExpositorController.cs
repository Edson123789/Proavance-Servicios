using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/PEspecificoParticipacionExpositor")]
    [ApiController]
    public class PEspecificoParticipacionExpositorController : ControllerBase
    {

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarDocenteConfirmacion([FromBody] ParticipacionExpositor_ActualizarDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                PEspecificoParticipacionExpositorRepositorio _repParticipacion =  new PEspecificoParticipacionExpositorRepositorio();
                if (_repParticipacion.Exist(w => w.Id == dto.Id))
                {
                    PEspecificoParticipacionExpositorBO bo = _repParticipacion.FirstById(dto.Id);

                    bo.IdExpositorGrupoConfirmado = dto.IdExpositorConfirmado;

                    bo.UsuarioModificacion = dto.Usuario;
                    bo.FechaModificacion = DateTime.Now;

                    _repParticipacion.Update(bo);

                    return Ok(bo);
                }
                else
                {
                    return BadRequest("No existe el registro solicitado.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarProveedorConfirmacion([FromBody] ParticipacionExpositor_ActualizarDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                PEspecificoParticipacionExpositorRepositorio _repParticipacion = new PEspecificoParticipacionExpositorRepositorio();
                if (_repParticipacion.Exist(w => w.Id == dto.Id))
                {
                    PEspecificoParticipacionExpositorBO bo = _repParticipacion.FirstById(dto.Id);

                    bo.IdProveedorOperacionesGrupoConfirmado = dto.IdProveedorOperacionesGrupoConfirmado;

                    bo.UsuarioModificacion = dto.Usuario;
                    bo.FechaModificacion = DateTime.Now;

                    _repParticipacion.Update(bo);

                    return Ok(bo);
                }
                else
                {
                    return BadRequest("No existe el registro solicitado.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarProveedor([FromBody] ParticipacionExpositor_ActualizarProveedorDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                PEspecificoParticipacionExpositorRepositorio _repParticipacion = new PEspecificoParticipacionExpositorRepositorio();
                if (_repParticipacion.Exist(w => w.Id == dto.Id))
                {
                    PEspecificoParticipacionExpositorBO bo = _repParticipacion.FirstById(dto.Id);

                    bo.IdProveedorFurHonorario = dto.IdProveedorFur;

                    bo.UsuarioModificacion = dto.Usuario;
                    bo.FechaModificacion = DateTime.Now;

                    _repParticipacion.Update(bo);

                    return Ok(bo);
                }
                else
                {
                    return BadRequest("No existe el registro solicitado.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
