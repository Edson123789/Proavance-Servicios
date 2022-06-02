
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteCuentasPorPagar")]
    public class ReporteCuentasPorPagarController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public ReporteCuentasPorPagarController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCuentasPorPagar(DateTime? FechaInicial, DateTime? FechaFinal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReportesRepositorio _repReportes = new ReportesRepositorio();
                var listado = _repReportes.ObtenerCuentasPorPagarByFecha(FechaInicial, FechaFinal);
                if (listado != null)
                {

                }
                return Ok(listado);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarDiferidoFurComprobante([FromBody] ListaEnterosDTO listaEnteros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FurRepositorio repFurRep = new FurRepositorio(_integraDBContext);
                ComprobantePagoRepositorio repComprobantePagoRep = new ComprobantePagoRepositorio(_integraDBContext);



                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var IdFur in listaEnteros.ListaEnteros)
                    {                        
                        var FurItem = repFurRep.FirstById(IdFur);
                        if (FurItem.EsDiferido != true)
                        {
                            FurItem.EsDiferido = true;
                            FurItem.UsuarioModificacion = listaEnteros.UsuarioModificacion;
                            FurItem.FechaModificacion = DateTime.Now;

                            repFurRep.Update(FurItem);
                        }
                    }
                    foreach (var IdComprobantePago in listaEnteros.listaEnteros2)
                    {

                        var ComprobantePago = repComprobantePagoRep.FirstById(IdComprobantePago);

                       ComprobantePago.EsDiferido = true;
                       ComprobantePago.UsuarioModificacion = listaEnteros.UsuarioModificacion;
                       ComprobantePago.FechaModificacion = DateTime.Now;

                        repComprobantePagoRep.Update(ComprobantePago);
                    }
                    scope.Complete();
                }

                return Ok();

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
    }
}