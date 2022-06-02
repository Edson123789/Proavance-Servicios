using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Sentinel")]
    public class SentinelController : Controller
    {
        public SentinelController()
        {

        }
        [Route("[action]/{idSentinel}")]
        [HttpGet]
        public ActionResult GetDetalleSentinel(int idSentinel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (idSentinel <= 0)
            {
                return BadRequest("El IdSentinel no Existe");
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                SentinelSdtEstandarItemRepositorio _repSentinelSdtEstandarItem = new SentinelSdtEstandarItemRepositorio(contexto);
                SentinelSdtInfGenRepositorio _repSentinelSdtInfGen = new SentinelSdtInfGenRepositorio(contexto);
                SentinelSdtLincreItemRepositorio _repSentinelSdtLincreItem = new SentinelSdtLincreItemRepositorio(contexto);
                SentinelSdtRepSbsitemRepositorio _repSentinelSdtRepSbsitem = new SentinelSdtRepSbsitemRepositorio(contexto);
                SentinelSdtPoshisItemRepositorio _repSentinelSdtPoshisItem = new SentinelSdtPoshisItemRepositorio(contexto);
                SentinelSdtResVenItemRepositorio _repSentinelSdtResVenItem = new SentinelSdtResVenItemRepositorio(contexto);

                DetalleSentinelDTO SentinelDetalle = new DetalleSentinelDTO();

                SentinelDetalle.DniRuc = _repSentinelSdtEstandarItem.ObtenerDniRucSentinel(idSentinel);
                SentinelDetalle.DatosGenerales = _repSentinelSdtInfGen.ObtenerDatosGenerales(idSentinel);
                SentinelDetalle.DatosVencidas = _repSentinelSdtResVenItem.ObtenerDatosVencidos(idSentinel);
                SentinelDetalle.LineaCredito = _repSentinelSdtLincreItem.ObtenerLineaDeCredito(idSentinel);
                SentinelDetalle.Deuda = _repSentinelSdtRepSbsitem.ObtenerLineaDeudaSentinel(idSentinel);
                SentinelDetalle.PosicionHistoria = _repSentinelSdtPoshisItem.ObtenerPosicionHistoria(idSentinel);

                return Ok(SentinelDetalle);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            

            
        }

    }
}
