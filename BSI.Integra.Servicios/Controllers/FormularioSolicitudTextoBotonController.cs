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
    [Route("api/FormularioSolicitudTextoBoton")]
    public class FormularioSolicitudTextoBotonController : Controller
    {
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerRegistros()
        {
            try
            {
                FormularioSolicitudTextoBotonRepositorio formularioSolicitudTextoBotonRepositorio = new FormularioSolicitudTextoBotonRepositorio();
                return Ok(formularioSolicitudTextoBotonRepositorio.ObtenerRegistrosGrid());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] FormularioSolicitudTextoBotonDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FormularioSolicitudTextoBotonRepositorio formularioSolicitudTextoBotonRepositorio = new FormularioSolicitudTextoBotonRepositorio();

                FormularioSolicitudTextoBotonBO formularioSolicitudTextoBotonBO = new FormularioSolicitudTextoBotonBO();
                formularioSolicitudTextoBotonBO.TextoBoton = Json.TextoBoton;
                formularioSolicitudTextoBotonBO.Descripcion = Json.Descripcion;
                formularioSolicitudTextoBotonBO.PorDefecto = Json.PorDefecto;

                formularioSolicitudTextoBotonBO.Estado = true;
                formularioSolicitudTextoBotonBO.UsuarioCreacion = Json.Usuario;
                formularioSolicitudTextoBotonBO.UsuarioModificacion = Json.Usuario;
                formularioSolicitudTextoBotonBO.FechaCreacion = DateTime.Now;
                formularioSolicitudTextoBotonBO.FechaModificacion = DateTime.Now;

                if (!formularioSolicitudTextoBotonBO.HasErrors)
                {
                    formularioSolicitudTextoBotonRepositorio.Insert(formularioSolicitudTextoBotonBO);
                }
                else
                {
                    return BadRequest(formularioSolicitudTextoBotonBO.ActualesErrores);
                }

                return Ok(formularioSolicitudTextoBotonBO.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] FormularioSolicitudTextoBotonDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FormularioSolicitudTextoBotonRepositorio formularioSolicitudTextoBotonRepositorio = new FormularioSolicitudTextoBotonRepositorio();

                FormularioSolicitudTextoBotonBO formularioSolicitudTextoBotonBO = formularioSolicitudTextoBotonRepositorio.FirstById(Json.Id);
                formularioSolicitudTextoBotonBO.TextoBoton = Json.TextoBoton;
                formularioSolicitudTextoBotonBO.Descripcion = Json.Descripcion;
                formularioSolicitudTextoBotonBO.PorDefecto = Json.PorDefecto;

                formularioSolicitudTextoBotonBO.Estado = true;
                formularioSolicitudTextoBotonBO.UsuarioModificacion = Json.Usuario;
                formularioSolicitudTextoBotonBO.FechaModificacion = DateTime.Now;

                if (!formularioSolicitudTextoBotonBO.HasErrors)
                {
                    formularioSolicitudTextoBotonRepositorio.Update(formularioSolicitudTextoBotonBO);
                }
                else
                {
                    return BadRequest(formularioSolicitudTextoBotonBO.ActualesErrores);
                }

                return Ok(formularioSolicitudTextoBotonBO.Id);
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
                FormularioSolicitudTextoBotonRepositorio formularioSolicitudTextoBotonRepositorio = new FormularioSolicitudTextoBotonRepositorio();
                formularioSolicitudTextoBotonRepositorio.Delete(Json.Id, Json.NombreUsuario);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}