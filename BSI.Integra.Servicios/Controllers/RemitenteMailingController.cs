using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Servicios.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/RemitenteMailing")]
    public class RemitenteMailingController : ControllerBase
    {
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTodoPersonal()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio();
                return Ok(_repPersonal.GetTodoPersonalActivoParaFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdRemitenteMailing}")]
        [HttpGet]
        public ActionResult ObtenerAsesoresPorIdRemitenteMailing(int IdRemitenteMailing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RemitenteMailingAsesorRepositorio _repRemitenteMailingAsesor = new RemitenteMailingAsesorRepositorio();
                return Ok(_repRemitenteMailingAsesor.ObtenerListaRemitenteMailingAsesor(IdRemitenteMailing));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult VizualizarRemitenteMailings()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RemitenteMailingRepositorio _repRemitenteMailing = new RemitenteMailingRepositorio();
                return Ok(_repRemitenteMailing.ObtenerTodosRemitenteMailing());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarRemitenteMailing([FromBody] RemitenteMailingCompuestoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RemitenteMailingAsesorRepositorio _repoRemitenteMailingAsesor = new RemitenteMailingAsesorRepositorio();
                RemitenteMailingRepositorio _repRemitenteMailing = new RemitenteMailingRepositorio();
                RemitenteMailingBO NuevaRemitenteMailing = new RemitenteMailingBO
                {
                    Nombre = ObjetoDTO.Nombre.Trim(),
                    Descripcion = ObjetoDTO.Descripcion,
                    Estado = true,
                    UsuarioCreacion = ObjetoDTO.Usuario,
                    UsuarioModificacion = ObjetoDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                _repRemitenteMailing.Insert(NuevaRemitenteMailing);
                foreach (var _asesor in ObjetoDTO.Asesores)
                {
                    RemitenteMailingAsesorBO Asesor = new RemitenteMailingAsesorBO
                    {
                        IdRemitenteMailing = NuevaRemitenteMailing.Id,
                        IdPersonal = _asesor.Id,
                        NombreCompleto = string.Concat(_asesor.Apellidos, ", ", _asesor.Nombres),
                        CorreoElectronico = _asesor.Email,
                        Estado = true,
                        UsuarioCreacion = ObjetoDTO.Usuario,
                        UsuarioModificacion = ObjetoDTO.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    if (!Asesor.HasErrors)
                    {
                        _repoRemitenteMailingAsesor.Insert(Asesor);
                    }
                    else {
                        return BadRequest(Asesor.GetErrors(null));
                    }
                }
                return Ok(NuevaRemitenteMailing);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarRemitenteMailing([FromBody] RemitenteMailingCompuestoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RemitenteMailingAsesorRepositorio _repoRemitenteMailingAsesor = new RemitenteMailingAsesorRepositorio();
                RemitenteMailingRepositorio _repRemitenteMailing = new RemitenteMailingRepositorio();
                var remitenteMailing = _repRemitenteMailing.FirstById(ObjetoDTO.Id);
                if (remitenteMailing == null) throw new Exception("Entidad no encontrada!");

                remitenteMailing.Nombre = ObjetoDTO.Nombre.Trim();
                remitenteMailing.Descripcion = ObjetoDTO.Descripcion;
                remitenteMailing.Estado = true;
                remitenteMailing.UsuarioModificacion = ObjetoDTO.Usuario;
                remitenteMailing.FechaModificacion = DateTime.Now;
                _repRemitenteMailing.Update(remitenteMailing);

                //Eliminamos asesores asociados existentes
                var asesoresActuales = _repoRemitenteMailingAsesor.GetBy(x => x.IdRemitenteMailing == ObjetoDTO.Id).ToList();
                foreach (var item in asesoresActuales)
                {
                    _repoRemitenteMailingAsesor.Delete(item.Id, ObjetoDTO.Usuario);
                }


                // Agregamos los nuevos asesores asignados
                foreach (var _asesor in ObjetoDTO.Asesores)
                {
                    RemitenteMailingAsesorBO asesor = new RemitenteMailingAsesorBO
                    {
                        IdRemitenteMailing = remitenteMailing.Id,
                        IdPersonal = _asesor.Id,
                        NombreCompleto = string.Concat( _asesor.Apellidos, ", ", _asesor.Nombres),
                        CorreoElectronico = _asesor.Email,
                        Estado = true,
                        UsuarioCreacion = ObjetoDTO.Usuario,
                        UsuarioModificacion = ObjetoDTO.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    _repoRemitenteMailingAsesor.Insert(asesor);
                }
                return Ok(remitenteMailing);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarRemitenteMailing([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RemitenteMailingRepositorio _repRemitenteMailing = new RemitenteMailingRepositorio();
                RemitenteMailingAsesorRepositorio _repoRemitenteMailingAsesor = new RemitenteMailingAsesorRepositorio();
                var remitentesMailing = _repoRemitenteMailingAsesor.GetBy(x => x.IdRemitenteMailing == Eliminar.Id).ToList();
                foreach (var item in remitentesMailing)
                {
                    _repoRemitenteMailingAsesor.Delete(item.Id, Eliminar.NombreUsuario);
                }
                _repRemitenteMailing.Delete(Eliminar.Id, Eliminar.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
