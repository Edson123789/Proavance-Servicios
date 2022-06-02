using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Scode.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/WhatsAppNumeroValidado")]
    public class WhatsAppNumeroValidadoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public WhatsAppNumeroValidadoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }        

        [Route("[action]")]
        [HttpPost]
        public ActionResult VerificarInsertarNumeroValidado([FromBody]WhatsAppNumeroValidadoDTO DTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                WhatsAppNumeroValidoRepositorio _repWhatsAppNumeroValido = new WhatsAppNumeroValidoRepositorio(_integraDBContext);
                if (!_repWhatsAppNumeroValido.VerificarNumeroValidado(DTO.NumeroCelular))
                {
                    WhatsAppNumeroValidadoBO whatsAppNumeroValidado = new WhatsAppNumeroValidadoBO();
                    whatsAppNumeroValidado.IdAlumno = DTO.IdAlumno;
                    whatsAppNumeroValidado.NumeroCelular = DTO.NumeroCelular;
                    whatsAppNumeroValidado.IdPais = DTO.IdPais;
                    whatsAppNumeroValidado.Estado = true;
                    whatsAppNumeroValidado.FechaCreacion = DateTime.Now;
                    whatsAppNumeroValidado.FechaModificacion = DateTime.Now;
                    whatsAppNumeroValidado.UsuarioCreacion = DTO.Usuario;
                    whatsAppNumeroValidado.UsuarioModificacion = DTO.Usuario;
                    _repWhatsAppNumeroValido.Insert(whatsAppNumeroValidado);
                    return Ok(false);
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}
