using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers.Operaciones
{
    [Route("api/CentroCostoPartner")]
    public class CentroCostoPartnerController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public CentroCostoPartnerController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult Obtener()
        {
            try
            {
                CentroCostoRepositorio _repCertificadoDetalle = new CentroCostoRepositorio(_integraDBContext);

                //var rpta = _repCertificadoDetalle.ObtenerTodo();
                return Ok(_repCertificadoDetalle.ObtenerTodoCentroCostoPartner());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult obtenerfiltro()
        {
            try
            {
                CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio(_integraDBContext);
                TroncalPartnerRepositorio _repTroncalPartner = new TroncalPartnerRepositorio(_integraDBContext);

                var lista = new
                {
                    centroCosto = _repCentroCosto.ObtenerCentroCostoParaFiltro(),
                    Partner = _repTroncalPartner.ObtenerParaFiltro()
                };
                    
                return Ok(lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
