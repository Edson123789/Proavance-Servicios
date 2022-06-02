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
    [Route("api/ModoPago")]
    [ApiController]
    public class ModoPagoController : ControllerBase
    {
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerModoPago()
        {
            try
            {
                ModoPagoRepositorio _repModoPago = new ModoPagoRepositorio();
                var listaModoPago = _repModoPago.ListarModosPagosPanel();

                return Ok(listaModoPago);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarModoPago([FromBody] ModoPagoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ModoPagoRepositorio repModoPago = new ModoPagoRepositorio();

                ModoPagoBO ModoPago = new ModoPagoBO();
                ModoPago.Nombre = Json.Nombre;
                ModoPago.UsuarioCreacion = Json.Usuario;
                ModoPago.UsuarioModificacion = Json.Usuario;
                ModoPago.FechaCreacion = DateTime.Now;
                ModoPago.FechaModificacion = DateTime.Now;
                ModoPago.Estado = true;

                var idTipoPago = repModoPago.Insert(ModoPago);
                return Ok(ModoPago);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarModoPago([FromBody] ModoPagoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ModoPagoRepositorio repModoPago = new ModoPagoRepositorio();
                bool result = false;

                if (repModoPago.Exist(Json.Id))
                {
                    ModoPagoBO ModoPago = repModoPago.FirstById(Json.Id);

                    ModoPago.Nombre = Json.Nombre;
                    ModoPago.UsuarioModificacion = Json.Usuario;
                    ModoPago.FechaModificacion = DateTime.Now;

                    result = repModoPago.Update(ModoPago);

                }

                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{Id}/{Usuario}")]
        [HttpDelete]
        public ActionResult EliminarModoPago(int Id, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ModoPagoRepositorio repModoPago = new ModoPagoRepositorio();
                bool result = false;
                if (repModoPago.Exist(Id))
                {
                    result = repModoPago.Delete(Id, Usuario);
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
