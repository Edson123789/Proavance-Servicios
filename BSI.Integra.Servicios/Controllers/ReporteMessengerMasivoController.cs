using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Servicios.SCode.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteMessengerMasivo")]
    [ApiController]
    public class ReporteMessengerMasivoController : ControllerBase
    {
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombos()
        {
            try
            {
                integraDBContext contexto = new integraDBContext();
                PersonalRepositorio _repPersonal = new PersonalRepositorio(contexto);
                var personal = _repPersonal.GetBy(x=> x.Activo == true, y=> new FiltroDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombres + " " + y.Apellidos
                });
                personal.Add(new FiltroDTO { Id = 4419 , Nombre = "Facebook"});
                return Ok(personal);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] FiltroReporteMessengerMasivo filtroReporteMessengerMasivo)
        {
            try
            {
                integraDBContext contexto = new integraDBContext();
                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();
                List<string> Fechas = new List<string>();
                DateTime fechaActual = filtroReporteMessengerMasivo.FechaInicio;
                string dia, mes;


                while (true)
                {
                    if(fechaActual.Month == filtroReporteMessengerMasivo.FechaFin.Month && fechaActual.Day > filtroReporteMessengerMasivo.FechaFin.Day)
                    {
                        break;
                    }
                    dia = fechaActual.Day < 10 ? "0" + fechaActual.Day : fechaActual.Day + "";
                    mes = fechaActual.Month < 10 ? "0" + fechaActual.Month : fechaActual.Month + "";
                    Fechas.Add( dia + mes);
                    fechaActual = fechaActual.AddDays(1);
                }

                string _personal = ListIntToString(filtroReporteMessengerMasivo.Personal);

                var reporteGeneral = reportesRepositorio.ObtenerReporteMessengerMasivoGeneral(filtroReporteMessengerMasivo.FechaInicio, filtroReporteMessengerMasivo.FechaFin);
                var reporte = reportesRepositorio.ObtenerReporteMessengerMasivo(filtroReporteMessengerMasivo.FechaInicio, filtroReporteMessengerMasivo.FechaFin, _personal);

                return Ok(new
                {
                    Fechas,
                    reporteGeneral,
                    reporte
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private string ListIntToString(IList<int> datos)
        {
            if (datos == null)
                datos = new List<int>();
            int NumberElements = datos.Count;
            string rptaCadena = string.Empty;
            for (int i = 0; i < NumberElements - 1; i++)
                rptaCadena += Convert.ToString(datos[i]) + ",";
            if (NumberElements > 0)
                rptaCadena += Convert.ToString(datos[NumberElements - 1]);
            return rptaCadena;
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerEstadisticasConjuntoAnuncio(ParametrosCalculoReporteChatDTO Fechas) 
        {
            try
            {
                integraDBContext contexto = new integraDBContext();
                APIGraphFacebook aPIGraphFacebook = new APIGraphFacebook();
                MessengerEnvioMasivoRepositorio messengerEnvioMasivoRepositorio = new MessengerEnvioMasivoRepositorio(contexto);
                FacebookConjuntoAnuncioEstadisticaDiariaRepositorio facebookConjuntoAnuncioEstadisticaDiariaRepositorio = new FacebookConjuntoAnuncioEstadisticaDiariaRepositorio(contexto);
                DateTime Fecha = Fechas.Fecha;

                var conjuntoAnuncios = messengerEnvioMasivoRepositorio.ObtenerConjuntoAnuncioFacebookEnvioMasivo(Fecha);

                FacebookConjuntoAnuncioEstadisticaDiariaBO facebookConjuntoAnuncioEstadisticaDiariaBO;
                foreach (var conjuntoAnuncio in conjuntoAnuncios)
                {
                    facebookConjuntoAnuncioEstadisticaDiariaBO = facebookConjuntoAnuncioEstadisticaDiariaRepositorio.FirstBy(x => x.Fecha == Fecha && x.IdConjuntoAnuncioFacebook == conjuntoAnuncio.IdConjuntoAnuncioFacebook);
                    if(facebookConjuntoAnuncioEstadisticaDiariaBO == null)
                    {
                        var resultado = aPIGraphFacebook.InsightConjuntoAnuncioMessengerMasivo(Fecha, conjuntoAnuncio.FacebookIdConjuntoAnuncio);
                        if(resultado != null && resultado.data.Count > 0)
                        {
                            facebookConjuntoAnuncioEstadisticaDiariaBO = new FacebookConjuntoAnuncioEstadisticaDiariaBO();
                            facebookConjuntoAnuncioEstadisticaDiariaBO.IdConjuntoAnuncioFacebook = conjuntoAnuncio.IdConjuntoAnuncioFacebook;
                            facebookConjuntoAnuncioEstadisticaDiariaBO.Fecha = Fecha;
                            facebookConjuntoAnuncioEstadisticaDiariaBO.Impresiones = resultado.data[0].impressions;
                            facebookConjuntoAnuncioEstadisticaDiariaBO.Alcance = resultado.data[0].reach;
                            facebookConjuntoAnuncioEstadisticaDiariaBO.Estado = true;
                            facebookConjuntoAnuncioEstadisticaDiariaBO.UsuarioCreacion = "Sistemas";
                            facebookConjuntoAnuncioEstadisticaDiariaBO.UsuarioModificacion = "Sistemas";
                            facebookConjuntoAnuncioEstadisticaDiariaBO.FechaCreacion = DateTime.Now;
                            facebookConjuntoAnuncioEstadisticaDiariaBO.FechaModificacion = DateTime.Now;

                            facebookConjuntoAnuncioEstadisticaDiariaRepositorio.Insert(facebookConjuntoAnuncioEstadisticaDiariaBO);
                        }
                    }
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}