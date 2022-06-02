using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/FormularioRespuestaPlantilla")]
    public class FormularioRespuestaPlantillaController : Controller
    {
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerRegistros()
        {
            try
            {
                FormularioRespuestaPlantillaRepositorio formularioRespuestaPlantillaRepositorio = new FormularioRespuestaPlantillaRepositorio();
                return Ok(formularioRespuestaPlantillaRepositorio.ObtenerRegistrosGrid());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerRegistroPorId(int Id)
        {
            try
            {
                FormularioRespuestaPlantillaRepositorio formularioRespuestaPlantillaRepositorio = new FormularioRespuestaPlantillaRepositorio();
                return Ok(formularioRespuestaPlantillaRepositorio.ObtenerRegistroPorId(Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] FormularioRespuestaPlantillaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FormularioRespuestaPlantillaRepositorio formularioRespuestaPlantillaRepositorio = new FormularioRespuestaPlantillaRepositorio();

                FormularioRespuestaPlantillaBO formularioRespuestaPlantillaBO = new FormularioRespuestaPlantillaBO();
                formularioRespuestaPlantillaBO.NombrePlantilla = Json.NombrePlantilla;
                formularioRespuestaPlantillaBO.ColorTextoPgeneral = Json.ColorTextoPgeneral;
                formularioRespuestaPlantillaBO.ColorTextoDescripcionPgeneral = Json.ColorTextoDescripcionPgeneral;
                formularioRespuestaPlantillaBO.ColorTextoInvitacionBrochure = Json.ColorTextoInvitacionBrochure;
                formularioRespuestaPlantillaBO.TextoBotonBrochure = Json.TextoBotonBrochure;
                formularioRespuestaPlantillaBO.ColorFondoBotonBrochure = Json.ColorFondoBotonBrochure;
                formularioRespuestaPlantillaBO.ColorTextoBotonBrochure = Json.ColorTextoBotonBrochure;
                formularioRespuestaPlantillaBO.ColorBordeInferiorBotonBrochure = Json.ColorBordeInferiorBotonBrochure;
                formularioRespuestaPlantillaBO.ColorTextoBotonInvitacion = Json.ColorTextoBotonInvitacion;
                formularioRespuestaPlantillaBO.ColorFondoBotonInvitacion = Json.ColorFondoBotonInvitacion;
                formularioRespuestaPlantillaBO.FondoBotonLadoInvitacion = Json.FondoBotonLadoInvitacion;
                formularioRespuestaPlantillaBO.UrlimagenLadoInvitacion = Json.UrlimagenLadoInvitacion;
                formularioRespuestaPlantillaBO.TextoBotonInvitacionPagina = Json.TextoBotonInvitacionPagina;
                formularioRespuestaPlantillaBO.TextoBotonInvitacionArea = Json.TextoBotonInvitacionArea;
                formularioRespuestaPlantillaBO.ContenidoSeccionTelefonos = Json.ContenidoSeccionTelefonos;
                formularioRespuestaPlantillaBO.TextoInvitacionBrochure = Json.TextoInvitacionBrochure;
                formularioRespuestaPlantillaBO.Estado = true;
                formularioRespuestaPlantillaBO.UsuarioCreacion = Json.Usuario;
                formularioRespuestaPlantillaBO.UsuarioModificacion = Json.Usuario;
                formularioRespuestaPlantillaBO.FechaCreacion = DateTime.Now;
                formularioRespuestaPlantillaBO.FechaModificacion = DateTime.Now;

                if (!formularioRespuestaPlantillaBO.HasErrors)
                {
                    formularioRespuestaPlantillaRepositorio.Insert(formularioRespuestaPlantillaBO);
                }
                else
                {
                    return BadRequest(formularioRespuestaPlantillaBO.ActualesErrores);
                }

                return Ok(formularioRespuestaPlantillaBO.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] FormularioRespuestaPlantillaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FormularioRespuestaPlantillaRepositorio formularioRespuestaPlantillaRepositorio = new FormularioRespuestaPlantillaRepositorio();

                FormularioRespuestaPlantillaBO formularioRespuestaPlantillaBO = formularioRespuestaPlantillaRepositorio.FirstById(Json.Id);
                formularioRespuestaPlantillaBO.NombrePlantilla = Json.NombrePlantilla;
                formularioRespuestaPlantillaBO.ColorTextoPgeneral = Json.ColorTextoPgeneral;
                formularioRespuestaPlantillaBO.ColorTextoDescripcionPgeneral = Json.ColorTextoDescripcionPgeneral;
                formularioRespuestaPlantillaBO.ColorTextoInvitacionBrochure = Json.ColorTextoInvitacionBrochure;
                formularioRespuestaPlantillaBO.TextoBotonBrochure = Json.TextoBotonBrochure;
                formularioRespuestaPlantillaBO.ColorFondoBotonBrochure = Json.ColorFondoBotonBrochure;
                formularioRespuestaPlantillaBO.ColorTextoBotonBrochure = Json.ColorTextoBotonBrochure;
                formularioRespuestaPlantillaBO.ColorBordeInferiorBotonBrochure = Json.ColorBordeInferiorBotonBrochure;
                formularioRespuestaPlantillaBO.ColorTextoBotonInvitacion = Json.ColorTextoBotonInvitacion;
                formularioRespuestaPlantillaBO.ColorFondoBotonInvitacion = Json.ColorFondoBotonInvitacion;
                formularioRespuestaPlantillaBO.FondoBotonLadoInvitacion = Json.FondoBotonLadoInvitacion;
                formularioRespuestaPlantillaBO.UrlimagenLadoInvitacion = Json.UrlimagenLadoInvitacion;
                formularioRespuestaPlantillaBO.TextoBotonInvitacionPagina = Json.TextoBotonInvitacionPagina;
                formularioRespuestaPlantillaBO.TextoBotonInvitacionArea = Json.TextoBotonInvitacionArea;
                formularioRespuestaPlantillaBO.ContenidoSeccionTelefonos = Json.ContenidoSeccionTelefonos;
                formularioRespuestaPlantillaBO.TextoInvitacionBrochure = Json.TextoInvitacionBrochure;
                formularioRespuestaPlantillaBO.Estado = true;
                formularioRespuestaPlantillaBO.UsuarioModificacion = Json.Usuario;
                formularioRespuestaPlantillaBO.FechaModificacion = DateTime.Now;

                if (!formularioRespuestaPlantillaBO.HasErrors)
                {
                    formularioRespuestaPlantillaRepositorio.Update(formularioRespuestaPlantillaBO);
                }
                else
                {
                    return BadRequest(formularioRespuestaPlantillaBO.ActualesErrores);
                }

                return Ok(formularioRespuestaPlantillaBO.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] EliminarDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FormularioRespuestaPlantillaRepositorio formularioRespuestaPlantillaRepositorio = new FormularioRespuestaPlantillaRepositorio();
                formularioRespuestaPlantillaRepositorio.Delete(Json.Id,Json.NombreUsuario);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}