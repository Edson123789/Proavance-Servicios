using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/GmailCliente")]
    public class GmailClienteController : ControllerBase
    {

        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerPanel([FromBody] FiltroPaginadorDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                GmailClienteRepositorio gmailClienteRepositorio = new GmailClienteRepositorio();

                return Ok(gmailClienteRepositorio.ObtenerRegistrosPorFiltro(Filtro));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerPersonalAutocomplete()
        {
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio();
                var personal = _repPersonal.ObtenerPersonalAsesoresFiltro();
                return Ok(personal);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] GmailClienteDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                GmailClienteRepositorio gmailClienteRepositorio = new GmailClienteRepositorio();

                GmailClienteBO gmailClienteBO = new GmailClienteBO();
                gmailClienteBO.IdAsesor = Json.IdAsesor;
                gmailClienteBO.EmailAsesor = Json.EmailAsesor;
                gmailClienteBO.PasswordCorreo = Json.PasswordCorreo;
                gmailClienteBO.NombreAsesor = Json.NombreAsesor;
                gmailClienteBO.IdClient = Json.IdClient;
                gmailClienteBO.ClientSecret = Json.ClientSecret;

                gmailClienteBO.Estado = true;
                gmailClienteBO.UsuarioCreacion = Json.Usuario;
                gmailClienteBO.UsuarioModificacion = Json.Usuario;
                gmailClienteBO.FechaCreacion = DateTime.Now;
                gmailClienteBO.FechaModificacion = DateTime.Now;

                if (!gmailClienteBO.HasErrors)
                {
                    gmailClienteRepositorio.Insert(gmailClienteBO);
                }
                else
                {
                    return BadRequest(gmailClienteBO.ActualesErrores);
                }

                return Ok(gmailClienteBO.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] GmailClienteDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                GmailClienteRepositorio gmailClienteRepositorio = new GmailClienteRepositorio();

                GmailClienteBO gmailClienteBO = gmailClienteRepositorio.FirstById(Json.Id);
                gmailClienteBO.IdAsesor = Json.IdAsesor;
                gmailClienteBO.EmailAsesor = Json.EmailAsesor;
                gmailClienteBO.PasswordCorreo = Json.PasswordCorreo;
                gmailClienteBO.NombreAsesor = Json.NombreAsesor;
                gmailClienteBO.IdClient = Json.IdClient;
                gmailClienteBO.ClientSecret = Json.ClientSecret;

                gmailClienteBO.Estado = true;
                gmailClienteBO.UsuarioModificacion = Json.Usuario;
                gmailClienteBO.FechaModificacion = DateTime.Now;

                if (!gmailClienteBO.HasErrors)
                {
                    gmailClienteRepositorio.Update(gmailClienteBO);
                }
                else
                {
                    return BadRequest(gmailClienteBO.ActualesErrores);
                }

                return Ok(gmailClienteBO.Id);
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
                GmailClienteRepositorio gmailClienteRepositorio = new GmailClienteRepositorio();
                gmailClienteRepositorio.Delete(Json.Id, Json.NombreUsuario);

                return Ok(Json.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}