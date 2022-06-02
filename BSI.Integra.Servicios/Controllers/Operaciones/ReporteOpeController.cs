using System;
using BSI.Integra.Aplicacion.Operaciones.BO;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Operaciones/Reporte")]
    [ApiController]
    public class ReporteOpeController : Controller
    {

        [Route("[Action]")]
        [HttpGet]
        public ActionResult FormatoVerificacionUltimosDetalles()
        {
            try
            {
                ReporteBO reporte = new ReporteBO();
                var reporteExcel = reporte.ObtenerFormatoVerificacionUltimosDetalles();
                return File(reporteExcel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RE-EJP-039 Lista de verificación de últimos detalles.xlsx");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

     
    }
}
