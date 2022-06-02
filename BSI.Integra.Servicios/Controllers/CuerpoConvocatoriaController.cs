using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/CuerpoConvocatoria")]
    public class CuerpoConvocatoriaController : Controller
    {

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerPDF()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConvocatoriaPersonalRepositorio repCajaIngresoRep = new ConvocatoriaPersonalRepositorio();
                ExamenRepositorio repExamen = new ExamenRepositorio();
                ConvocatoriaPersonalBO cajaIngreso = new ConvocatoriaPersonalBO();

                var html = repExamen.GetBy(x => x.Id == 11).Select(x => x.Instrucciones).FirstOrDefault();

                var pdf = cajaIngreso.GenerarPDFPrueba2(html);

                return Ok(pdf);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}