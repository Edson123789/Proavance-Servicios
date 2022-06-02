using System;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/TipoCambioEntreMoneda")]
    public class TipoCambioEntreMonedaController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public TipoCambioEntreMonedaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerParaFiltro()
        {
            try
            {
                TipoCambioEntreMonedaRepositorio _repTipoCambioEntreMoneda = new TipoCambioEntreMonedaRepositorio(_integraDBContext);
                return Ok(_repTipoCambioEntreMoneda.GetBy(x=>x.Estado, x => new { x.Id, x.Nombre }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

    public class ValidadorTipoCambioEntreMonedaDTO : AbstractValidator<TTipoCambioEntreMoneda>
    {
        public static ValidadorTipoCambioEntreMonedaDTO Current = new ValidadorTipoCambioEntreMonedaDTO();
        public ValidadorTipoCambioEntreMonedaDTO()
        {
        }
    }
}
