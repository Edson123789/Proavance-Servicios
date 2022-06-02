using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/CloudflareUsuarioLlave")]
    [ApiController]
    public class CloudflareUsuarioLlaveController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public CloudflareUsuarioLlaveController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodosRegistros()
        {
            try
            {
                CloudflareUsuarioLlaveRepositorio cloudflareUsuarioLlaveRepositorio = new CloudflareUsuarioLlaveRepositorio(_integraDBContext);
                var registros = cloudflareUsuarioLlaveRepositorio.ObtenerListaCloudflareUsuarioLlave();
                return Ok(registros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarDominio([FromBody] CloudflareUsuarioLlaveDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CloudflareUsuarioLlaveRepositorio cloudflareUsuarioLlaveRepositorio = new CloudflareUsuarioLlaveRepositorio(_integraDBContext);
                CloudflareUsuarioLlaveBO cloudflareUsuarioLlave = new CloudflareUsuarioLlaveBO();


                using (TransactionScope scope = new TransactionScope())
                {
                    cloudflareUsuarioLlave.AuthKey = Json.AuthKey;
                    cloudflareUsuarioLlave.AuthEmail = Json.AuthEmail;
                    cloudflareUsuarioLlave.AccountId = Json.AccountId;
                    cloudflareUsuarioLlave.IdPersonal = Json.IdPersonal;
                    cloudflareUsuarioLlave.Activar = Json.Activar;
                    cloudflareUsuarioLlave.Estado = true;
                    cloudflareUsuarioLlave.UsuarioCreacion = Json.Usuario;
                    cloudflareUsuarioLlave.UsuarioModificacion = Json.Usuario;
                    cloudflareUsuarioLlave.FechaCreacion = DateTime.Now;
                    cloudflareUsuarioLlave.FechaModificacion = DateTime.Now;

                    cloudflareUsuarioLlaveRepositorio.Insert(cloudflareUsuarioLlave);
                    scope.Complete();
                }

                return Ok(cloudflareUsuarioLlave);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarDominio([FromBody] CloudflareUsuarioLlaveDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CloudflareUsuarioLlaveRepositorio cloudflareUsuarioLlaveRepositorio = new CloudflareUsuarioLlaveRepositorio(_integraDBContext);
                CloudflareUsuarioLlaveBO cloudflareUsuarioLlave = new CloudflareUsuarioLlaveBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    if (cloudflareUsuarioLlaveRepositorio.Exist(Json.Id))
                    {
                        cloudflareUsuarioLlave = cloudflareUsuarioLlaveRepositorio.FirstById(Json.Id);
                        cloudflareUsuarioLlave.AuthKey = Json.AuthKey;
                        cloudflareUsuarioLlave.AuthEmail = Json.AuthEmail;
                        cloudflareUsuarioLlave.AccountId = Json.AccountId;
                        cloudflareUsuarioLlave.IdPersonal = Json.IdPersonal;
                        cloudflareUsuarioLlave.Activar = Json.Activar;
                        cloudflareUsuarioLlave.UsuarioModificacion = Json.Usuario;
                        cloudflareUsuarioLlave.FechaModificacion = DateTime.Now;

                        cloudflareUsuarioLlaveRepositorio.Update(cloudflareUsuarioLlave);
                        scope.Complete();
                    }
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{Id}/{Usuario}")]
        [HttpDelete]
        public ActionResult EliminarArea(int Id, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CloudflareUsuarioLlaveRepositorio cloudflareUsuarioLlaveRepositorio = new CloudflareUsuarioLlaveRepositorio(_integraDBContext);
                CloudflareUsuarioLlaveBO cloudflareUsuarioLlave = new CloudflareUsuarioLlaveBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    if (cloudflareUsuarioLlaveRepositorio.Exist(Id))
                    {
                        cloudflareUsuarioLlaveRepositorio.Delete(Id, Usuario);

                    }
                    scope.Complete();
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
