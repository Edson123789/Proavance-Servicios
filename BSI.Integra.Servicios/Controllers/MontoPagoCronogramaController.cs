using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.DTO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: MontoPagoCronograma
    /// Autor: Jose Villena
    /// Fecha: 01/05/2021        
    /// <summary>
    /// Controlador de Monto Pago Cronograma
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MontoPagoCronogramaController : Controller
    {
        [Route("[Action]/{IdOportunidad}/{TipoPersona}")]
        [HttpGet]
        public ActionResult GetOportunidadCronograma(int IdOportunidad, string TipoPersona)
        {
            integraDBContext contexto = new integraDBContext();
            MontoPagoCronogramaBO Objeto = new MontoPagoCronogramaBO(IdOportunidad, TipoPersona, contexto);
            MontoPagoCronogramaBO MontoPago;
            MontoPagoCronogramaRepositorio _repMontoPagoCronograma = new MontoPagoCronogramaRepositorio();
            MontoPagoCronogramaDetalleRepositorio _repMontoPagoDetalleCronograma = new MontoPagoCronogramaDetalleRepositorio();
            var ExisteCronograma = _repMontoPagoCronograma.GetBy(w => w.IdOportunidad == Objeto.IdOportunidad).FirstOrDefault();
            string vistaPortalWeb = "";
            if (ExisteCronograma != null)
            {
                Objeto.Id = ExisteCronograma.Id;
                Objeto.IdOportunidad = ExisteCronograma.IdOportunidad;
                Objeto.IdMontoPago = ExisteCronograma.IdMontoPago;
                Objeto.IdPersonal = ExisteCronograma.IdPersonal;
                Objeto.Precio = ExisteCronograma.Precio;
                Objeto.PrecioDescuento = ExisteCronograma.PrecioDescuento;
                Objeto.IdMoneda = ExisteCronograma.IdMoneda;
                Objeto.IdTipoDescuento = ExisteCronograma.IdTipoDescuento;
                Objeto.EsAprobado = ExisteCronograma.EsAprobado;
                Objeto.NombrePlural = ExisteCronograma.NombrePlural;
                Objeto.Formula = ExisteCronograma.Formula;
                Objeto.MatriculaEnProceso = ExisteCronograma.MatriculaEnProceso;
                Objeto.CodigoMatricula = ExisteCronograma.CodigoMatricula;
                Objeto.ListaDetalleCuotas = _repMontoPagoDetalleCronograma.GetBy(w => w.IdMontoPagoCronograma == ExisteCronograma.Id).ToList();
                //MontoPago

                MontoPago = new MontoPagoCronogramaBO()
                {
                    Id = Objeto.IdMontoPago,
                    IdOportunidad = Objeto.IdOportunidad,
                    IdMontoPago = Objeto.IdMontoPago,
                    IdPersonal = Objeto.IdPersonal,
                    Precio = Objeto.Precio,
                    PrecioDescuento = Objeto.PrecioDescuento,
                    IdMoneda = Objeto.IdMoneda,
                    IdTipoDescuento = Objeto.IdTipoDescuento,
                    EsAprobado = Objeto.EsAprobado,
                    NombrePlural = Objeto.NombrePlural,
                    Formula = Objeto.Formula,
                    MatriculaEnProceso = Objeto.MatriculaEnProceso,
                    CodigoMatricula = Objeto.CodigoMatricula
                };


                vistaPortalWeb = "<div class='card'><div class='card-header cabecera_tab'> <span class='panel-title'>Cronograma de Asesor: </span></div><br> <div class='row'> <div class='col-sm-1'></div><div class='col-sm-9'> <nav class='jumbotron' id='header' style='background-color: #094d82 !important;height: 150px;background: #094d82;margin-bottom: 0;' h_cabecera> <div class='container'> <div> <a href='https://bsgrupo.com/' style='width: 163px;height: 192px;background: #afca0a url(https://bsginstitute.com/repositorioweb/img/logo.png) no-repeat center center;position: absolute;top: 0;text-indent: -9999px;z-index: 100;'></a> </div> </div> </nav> <div style=' margin-top: 80px;margin-bottom: 80px;'></div> <div class='bloque-blanco'> <div style='background: #EEEEEE;'> <div class='container' style='padding: 0px;'> <div class='row'> <div class='col-sm-1'></div><div class='col-sm-9' style='text-align:center;'> <div style='background-color: #094D82;padding: 2px;'> <p st_texto style='color:white;'>Cronograma de pagos</p> </div> <br> <br> <div> #tablacuotas </div><br> <br>  </div> <br> <br>";
                string tabla = "<table class='table table-striped table table-hover'><thead><tr><th> Nro.Cuota </th><th> Moneda </th><th> Monto </th><th> Fecha de vencimiento</th></tr></thead><tbody>";
                foreach (var item in Objeto.ListaDetalleCuotas.OrderBy(w => w.FechaPago))
                {
                    tabla = tabla + "<tr>";
                    tabla = tabla + "<td>" + item.NumeroCuota+"</td>";
                    tabla = tabla + "<td>" + Objeto.NombrePlural + "</td>";
                    tabla = tabla + "<td>" + item.MontoCuotaDescuento + "</td>";
                    tabla = tabla + "<td>" + item.FechaPago.ToShortDateString() + "</td>";
                    tabla = tabla + "</tr>";
                }
                tabla = tabla + "</tbody></table>";
                vistaPortalWeb = vistaPortalWeb.Replace("#tablacuotas", tabla);

            }
            else
            {
                MontoPago = new MontoPagoCronogramaBO();//Vacio
            }
            return Ok(new { Cronograma = Objeto, MontoPago, ListaCronogramaDetalle = Objeto.ListaDetalleCuotas, MontosPagosVentas = Objeto.ListaMontosPagosVentas, Descuentos = Objeto.ListaTipoDescuento, vistaPortalWeb });
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena
        /// Fecha: 01/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Cronograma de Pago del Alumno
        /// </summary>
        /// <returns> Retorna Lista de Objetos</returns>
        [Route("[Action]/{IdOportunidad}/{TipoPersona}/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult GetOportunidadCronogramaFinanzas(int IdOportunidad, string TipoPersona,int IdMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();

                MontoPagoCronogramaBO Objeto = new MontoPagoCronogramaBO(IdOportunidad, TipoPersona, contexto);
                MontoPagoCronogramaBO MontoPago;
                List<CronogramaPagoDetalleFinalDTO> CronogramaDetalle = new List<CronogramaPagoDetalleFinalDTO>();

                MontoPagoCronogramaRepositorio _repMontoPagoCronograma = new MontoPagoCronogramaRepositorio();
                MontoPagoCronogramaDetalleRepositorio _repMontoPagoDetalleCronograma = new MontoPagoCronogramaDetalleRepositorio();
                CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio();
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio();

                MatriculaCabeceraBO matricula = new MatriculaCabeceraBO();

                var ExisteCronograma = _repMontoPagoCronograma.GetBy(w => w.IdOportunidad == Objeto.IdOportunidad).FirstOrDefault();
                //if (ExisteCronograma == null || _repMatriculaCabecera.FirstBy(w => w.IdCronograma == ExisteCronograma.Id) ==null)
                //{
                //    var a = _repOportunidad.ObtenerDatosOportunidad(IdOportunidad);
                //    matricula = _repMatriculaCabecera.FirstBy(w => w.IdPespecifico == a.IdPespecifico && w.IdAlumno == a.IdAlumno);
                //}
                //else
                //{
                //    matricula = _repMatriculaCabecera.FirstBy(w => w.IdCronograma == ExisteCronograma.Id);//, w => new { w.Id });
                //}
                string vistaPortalWeb = "";
                if (ExisteCronograma != null)
                {
                    Objeto.Id = ExisteCronograma.Id;
                    Objeto.IdOportunidad = ExisteCronograma.IdOportunidad;
                    Objeto.IdMontoPago = ExisteCronograma.IdMontoPago;
                    Objeto.IdPersonal = ExisteCronograma.IdPersonal;
                    Objeto.Precio = ExisteCronograma.Precio;
                    Objeto.PrecioDescuento = ExisteCronograma.PrecioDescuento;
                    Objeto.IdMoneda = ExisteCronograma.IdMoneda;
                    Objeto.IdTipoDescuento = ExisteCronograma.IdTipoDescuento;
                    Objeto.EsAprobado = ExisteCronograma.EsAprobado;
                    Objeto.NombrePlural = ExisteCronograma.NombrePlural;
                    Objeto.Formula = ExisteCronograma.Formula;
                    Objeto.MatriculaEnProceso = ExisteCronograma.MatriculaEnProceso;
                    Objeto.CodigoMatricula = ExisteCronograma.CodigoMatricula;
                    Objeto.ListaDetalleCuotas = new List<MontoPagoCronogramaDetalleBO>();
                    //Objeto.ListaDetalleCuotas = _repMontoPagoDetalleCronograma.GetBy(w => w.IdMontoPagoCronograma == ExisteCronograma.Id).ToList();
                    //MontoPago

                    MontoPago = new MontoPagoCronogramaBO()
                    {
                        Id = Objeto.IdMontoPago,
                        IdOportunidad = Objeto.IdOportunidad,
                        IdMontoPago = Objeto.IdMontoPago,
                        IdPersonal = Objeto.IdPersonal,
                        Precio = Objeto.Precio,
                        PrecioDescuento = Objeto.PrecioDescuento,
                        IdMoneda = Objeto.IdMoneda,
                        IdTipoDescuento = Objeto.IdTipoDescuento,
                        EsAprobado = Objeto.EsAprobado,
                        NombrePlural = Objeto.NombrePlural,
                        Formula = Objeto.Formula,
                        MatriculaEnProceso = Objeto.MatriculaEnProceso,
                        CodigoMatricula = Objeto.CodigoMatricula
                    };

                    var versionAprobada = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == IdMatriculaCabecera && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();

                    vistaPortalWeb = "";
                    CronogramaDetalle = _repCronogramaPagoDetalleFinal.ObtenerCronogramaFinanzas(versionAprobada.Version.Value, IdMatriculaCabecera);
                    //CronogramaDetalle = _repCronogramaPagoDetalleFinal.GetBy(w => w.IdMatriculaCabecera == matricula.Id && w.Version == versionAprobada.Version).OrderBy(w=> w.NroCuota).ToList();
                }
                else
                {
                    MontoPago = new MontoPagoCronogramaBO();//Vacio
                    var versionAprobada = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == IdMatriculaCabecera && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();
                    CronogramaDetalle = _repCronogramaPagoDetalleFinal.ObtenerCronogramaFinanzas(versionAprobada.Version.Value, IdMatriculaCabecera);
                }
                return Ok(new { Cronograma = Objeto, MontoPago, ListaCronogramaDetalle = CronogramaDetalle, MontosPagosVentas = Objeto.ListaMontosPagosVentas, Descuentos = Objeto.ListaTipoDescuento, vistaPortalWeb});
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("[Action]/{IdOportunidad}/{TipoPersona}/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult GetOportunidadMontoComplementarios(int IdOportunidad, string TipoPersona, int IdMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();

                MontoPagoCronogramaBO Objeto = new MontoPagoCronogramaBO(IdOportunidad, TipoPersona, contexto);
                MontoPagoCronogramaBO MontoPago;

                MontoPagoCronogramaRepositorio _repMontoPagoCronograma = new MontoPagoCronogramaRepositorio();
                MontoPagoCronogramaDetalleRepositorio _repMontoPagoDetalleCronograma = new MontoPagoCronogramaDetalleRepositorio();
                CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio();
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio();
                MontoPagoRepositorio _repMontoPago = new MontoPagoRepositorio();

                MatriculaCabeceraBO matricula = new MatriculaCabeceraBO();

                var ExisteCronograma = _repMontoPagoCronograma.GetBy(w => w.IdOportunidad == Objeto.IdOportunidad).FirstOrDefault();
               
                if (ExisteCronograma != null)
                {
                    Objeto.Id = ExisteCronograma.Id;
                    Objeto.IdOportunidad = ExisteCronograma.IdOportunidad;
                    Objeto.IdMontoPago = ExisteCronograma.IdMontoPago;
                    Objeto.IdPersonal = ExisteCronograma.IdPersonal;
                    Objeto.Precio = ExisteCronograma.Precio;
                    Objeto.PrecioDescuento = ExisteCronograma.PrecioDescuento;
                    Objeto.IdMoneda = ExisteCronograma.IdMoneda;
                    Objeto.IdTipoDescuento = ExisteCronograma.IdTipoDescuento;
                    Objeto.EsAprobado = ExisteCronograma.EsAprobado;
                    Objeto.NombrePlural = ExisteCronograma.NombrePlural;
                    Objeto.Formula = ExisteCronograma.Formula;
                    Objeto.MatriculaEnProceso = ExisteCronograma.MatriculaEnProceso;
                    Objeto.CodigoMatricula = ExisteCronograma.CodigoMatricula;
                    Objeto.ListaDetalleCuotas = new List<MontoPagoCronogramaDetalleBO>();


                    //Complementarios
                    var montoPago = _repMontoPago.GetBy(w => w.Id == Objeto.IdMontoPago).FirstOrDefault();

                    if(montoPago!=null){
                        Objeto.MontoPago = montoPago;//complementario
                        Objeto.ListaMontosComplementarios = _repMontoPagoCronograma.ObtenerMontosComplementarios(montoPago.IdPrograma == null ? 0 : montoPago.IdPrograma.Value, montoPago.IdPais == null ? 0 : montoPago.IdPais.Value, montoPago.Id, IdMatriculaCabecera);
                    }
                    else{
                        Objeto.MontoPago = new MontoPagoBO();
                        Objeto.ListaMontosComplementarios = new List<DatosMontosComplementariosDTO>();
                    }

                }

                return Ok(new { Cronograma = Objeto, Descuentos = Objeto.ListaTipoDescuento, MontosComplementarios = Objeto.ListaMontosComplementarios });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena
        /// Fecha: 01/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Cronograma de Pago del Alumno por Codigo Matricula
        /// </summary>
        /// <returns> Retorna ojeto: List<CronogramaPagoDetalleFinalDTO> </returns>
        [Route("[Action]/{CodigoMatricula}")]
        [HttpGet]
        public ActionResult GetOportunidadCronogramaFinanzasPorMatricula(string CodigoMatricula)
        {

            integraDBContext contexto = new integraDBContext();
            MontoPagoCronogramaBO MontoPago;
            List<CronogramaPagoDetalleFinalDTO> CronogramaDetalle = new List<CronogramaPagoDetalleFinalDTO>();

            MontoPagoCronogramaDetalleRepositorio _repMontoPagoDetalleCronograma = new MontoPagoCronogramaDetalleRepositorio();
            CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio();
            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();

            var matricula = _repMatriculaCabecera.FirstBy(w => w.CodigoMatricula == CodigoMatricula);//, w => new { w.Id });
            var versionAprobada = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matricula.Id && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();
            CronogramaDetalle = _repCronogramaPagoDetalleFinal.ObtenerCronogramaFinanzas(versionAprobada.Version.Value, matricula.Id);
            //CronogramaDetalle = _repCronogramaPagoDetalleFinal.GetBy(w => w.IdMatriculaCabecera == matricula.Id && w.Version == versionAprobada.Version).OrderBy(w=> w.NroCuota).ToList();
            
            return Ok(CronogramaDetalle);
        }
        /// Tipo Función: GET
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 02/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Retorna los datos de comprobante pago por alumno
        /// </summary>
        /// <returns>Tipo de objeto que retorna la función</returns>
        [Route("[Action]/{CodigoAlumno}")]
        [HttpGet]
        public ActionResult GetOportunidadComprobantePagoPorAlumno(int codigoAlumno)
        {
            ComprobantePagoOportunidadRepositorio _repComprobantePago = new ComprobantePagoOportunidadRepositorio();
            
            var comprobante = _repComprobantePago.FirstByIdContacto(codigoAlumno);//, w => new { w.Id });

            return Ok(comprobante);
        }

        /// Tipo Función: POST 
        /// Autor: Abelson Quiñones
        /// Fecha: 30/09/2021
        /// Versión: 1.1
        /// <summary>
        /// Guarda el cronograma de ventas segun el idAlumno y la oportunidad, se el registro del metodo de pago y el idMatricula de regreso
        /// </summary>
        /// <returns>MontoPagoCronogramaBO</returns>
        [Route("[Action]/{IdOportunidad}/{IdAlumno}")]
        [HttpPost]
        public IActionResult GuardarCronogramaVentas(int IdOportunidad, int IdAlumno, [FromBody] MontoPagoCronogramaBO CronogramaDTO)
        {
            if (IdOportunidad == 0 && IdAlumno == 0)
            {
                return BadRequest("Los Ids IdOportunidad y/o IdAlumno son requeridos");
            }
            if (CronogramaDTO == null)
            {
                return BadRequest("Registros de Montos de pagos no puede ser nulo");
            }

            MontoPagoCronogramaRepositorio _repMontoPagoCronograma = new MontoPagoCronogramaRepositorio();
            MontoPagoCronogramaDetalleRepositorio _repMontoPagoCronogramaDetalle = new MontoPagoCronogramaDetalleRepositorio();

            MontoPagoCronogramaBO Cronograma = new MontoPagoCronogramaBO();
            Cronograma = CronogramaDTO;
            Cronograma.IdOportunidad = IdOportunidad;
            Cronograma.Estado = true;
            Cronograma.FechaCreacion = DateTime.Now;
            Cronograma.FechaModificacion = DateTime.Now;
            Cronograma.UsuarioCreacion = Cronograma.Usuario;
            Cronograma.UsuarioModificacion = Cronograma.Usuario;

            //Se calcula el Codigo de Matricula , alumno y programa especifico
            Cronograma.CalcularCodigoMatricula(IdAlumno);

            //Impide que un alumno se matricule en un programa que no este en lanzamiento
            if (Cronograma.PEspecificoInformacion.EstadoP != "Lanzamiento")
            {
                return BadRequest("No se puede matricular en un curso que no esta en lanzamiento.");
            }

            GuardarCronograma(IdOportunidad, IdAlumno, ref Cronograma);
            
            if (Cronograma.EsAprobado)
            {
                //enlazar alumno
                Cronograma.EnlazarAlumno();
                //envia correos
                Cronograma.EnviarCorreosFinanzasVentas();
            }

            //Se comenta ya que no V3 no necesitas estar syncronizado
            //string URI = "http://localhost:4348/Sincronizar/GuargarCronogramaV3?IdOportunidad=" + IdOportunidad + "&NombreUsuario=" + CronogramaDTO.Usuario + "&EsAprobado=" + CronogramaDTO.EsAprobado;

            //using (WebClient wc = new WebClient())
            //{
            //    wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
            //    wc.DownloadString(URI);
            //}

            var vistaPortalWeb = "";
            if (Cronograma != null && Cronograma.IdAlumnoPortal != 0)
            {
                vistaPortalWeb = "<div class='card'><div class='card-header cabecera_tab'> <span class='panel-title'>Cronograma de Asesor: </span></div><br> <div class='row'> <div class='col-sm-1'></div><div class='col-sm-9'> <nav class='jumbotron' id='header' style='background-color: #094d82 !important;height: 150px;background: #094d82;margin-bottom: 0;' h_cabecera> <div class='container'> <div> <a href='https://bsgrupo.com/' style='width: 163px;height: 192px;background: #afca0a url(https://bsginstitute.com/repositorioweb/img/logo.png) no-repeat center center;position: absolute;top: 0;text-indent: -9999px;z-index: 100;'></a> </div> </div> </nav> <div style=' margin-top: 80px;margin-bottom: 80px;'></div> <div class='bloque-blanco'> <div style='background: #EEEEEE;'> <div class='container' style='padding: 0px;'> <div class='row'> <div class='col-sm-1'></div><div class='col-sm-9' style='text-align:center;'> <div style='background-color: #094D82;padding: 2px;'> <p st_texto style='color:white;'>Cronograma de pagos</p> </div> <br> <br> <div> #tablacuotas </div><br> <br>  </div> <br> <br>";
                string tabla = "<table class='table table-striped table table-hover'><thead><tr><th> Nro.Cuota </th><th> Moneda </th><th> Monto </th><th> Fecha de vencimiento</th></tr></thead><tbody>";
                foreach (var item in Cronograma.ListaDetalleCuotas.OrderBy(w => w.FechaPago))
                {
                    tabla = tabla + "<tr>";
                    tabla = tabla + "<td>" + item.NumeroCuota + "</td>";
                    tabla = tabla + "<td>" + Cronograma.NombrePlural + "</td>";
                    tabla = tabla + "<td>" + item.MontoCuotaDescuento + "</td>";
                    tabla = tabla + "<td>" + item.FechaPago.ToShortDateString() + "</td>";
                    tabla = tabla + "</tr>";
                }
                tabla = tabla + "</tbody></table>";
                vistaPortalWeb = vistaPortalWeb.Replace("#tablacuotas", tabla);
            }

            //Registrar el metodo de pago
            PasarelaPagoPWBO ObjetoPasarelaBO = new PasarelaPagoPWBO();
            var _IdMatriculaCabecera = ObjetoPasarelaBO.BuscarIdMatriculaCabeceraPorCodigoMatricula(Cronograma.CodigoMatricula);
            try
            {
                if (_IdMatriculaCabecera > 0 && Cronograma.IdMedioPago > 0)
                {
                    RegistroMedioPagoMatriculaCronogramaDTO model = new RegistroMedioPagoMatriculaCronogramaDTO();
                    model.IdMatriculaCabecera = _IdMatriculaCabecera;
                    model.IdMedioPago = Cronograma.IdMedioPago;
                    model.Usuario = Cronograma.Usuario;
                    var resultado = ObjetoPasarelaBO.RegistroMedioPagoMatriculaCronograma(model);
                }
                else
                {
                    _IdMatriculaCabecera = 0;
                }
            }
            catch(Exception e)
            {
                _IdMatriculaCabecera = 0;
            }

            return Ok(new { Cronograma, vistaPortalWeb,IdMatriculaCabecera=_IdMatriculaCabecera });
            
        }

        [Route("[Action]/{IdOportunidad}/{IdAlumno}")]
        [HttpPost]
        public IActionResult EliminarCronogramaVentas(int IdOportunidad, int IdAlumno, [FromBody] MontoPagoCronogramaBO CronogramaDTO)
        {
            try
            {

                integraDBContext contexto = new integraDBContext();
                MontoPagoCronogramaRepositorio _repMontoPagoCronograma = new MontoPagoCronogramaRepositorio(contexto);
                string Firma = "<img src='https://repositorioweb.blob.core.windows.net/firmas/" + CronogramaDTO.Usuario + ".png' />";
                CronogramaDTO.CalcularCodigoMatricula(IdAlumno);

                var ExistenCuotas = _repMontoPagoCronograma.CuotasPagadas(CronogramaDTO.CodigoMatricula);
                if (Int32.Parse(ExistenCuotas.Resultado.ToString()) == 0)
                {
                    CronogramaDTO.GenerarArchivoCrepCronograma("Eliminar", Firma);

                    CronogramaDTO.MatriculaEnProceso = 0;
                    this.EliminarCronograma(ref CronogramaDTO);
                    CronogramaDTO = null;
                    return Ok(new { Cronograma = CronogramaDTO });
                }
                else {

                    return BadRequest(ExistenCuotas);
                }
              
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        /// Tipo Función: GET
        /// Autor: Jose Villena
        /// Fecha: 01/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Retorna Detalle Monto Pago
        /// </summary>
        /// <returns>Tipo de objeto que retorna la función</returns>
        [Route("[Action]/{IdMonto}")]
        [HttpGet]
        public IActionResult ObtenerDetalleMontoPago(int IdMonto)
        {
            try
            {
                MontoPagoCronogramaRepositorio _repMontoPagoCronograma = new MontoPagoCronogramaRepositorio();
                List<DetalleMontoPagoDTO> DetalleMontoPago = _repMontoPagoCronograma.ObtenerDetalleMontoPago(IdMonto);
                return Ok(new { DetalleMontoPago });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        private void GuardarCronograma(int IdOportunidad, int IdAlumno,ref MontoPagoCronogramaBO Cronograma)
        {


            integraDBContext contexto = new integraDBContext();
            MontoPagoCronogramaRepositorio _repMontoPagoCronograma = new MontoPagoCronogramaRepositorio(contexto);
            MontoPagoCronogramaDetalleRepositorio _repMontoPagoCronogramaDetalle = new MontoPagoCronogramaDetalleRepositorio(contexto);

            using (TransactionScope scope = new TransactionScope())
            {

                if (Cronograma.Id == 0)
                {
                    _repMontoPagoCronograma.Insert(Cronograma);
                }
                else
                {
                    //Delete Orquesta
                    int IdCronograma = Cronograma.Id;

                    var lista = _repMontoPagoCronogramaDetalle.GetBy(w => w.IdMontoPagoCronograma == IdCronograma).ToList();
                    foreach (var item in lista)
                    {
                        _repMontoPagoCronogramaDetalle.Delete(item.Id, Cronograma.Usuario);
                    }
                    //Fin Delete Orquesta

                    //Update
                    var MontoPagoCronogramaActualizar = _repMontoPagoCronograma.FirstById(Cronograma.Id);
                    MontoPagoCronogramaActualizar.IdMontoPago = Cronograma.IdMontoPago;
                    MontoPagoCronogramaActualizar.IdMoneda = Cronograma.IdMoneda;
                    MontoPagoCronogramaActualizar.IdTipoDescuento = Cronograma.IdTipoDescuento;
                    MontoPagoCronogramaActualizar.Precio = Cronograma.Precio;
                    MontoPagoCronogramaActualizar.PrecioDescuento = Cronograma.PrecioDescuento;
                    MontoPagoCronogramaActualizar.IdOportunidad = Cronograma.IdOportunidad;
                    MontoPagoCronogramaActualizar.IdPersonal = Cronograma.IdPersonal;
                    MontoPagoCronogramaActualizar.EsAprobado = Cronograma.EsAprobado;
                    MontoPagoCronogramaActualizar.NombrePlural = Cronograma.NombrePlural;
                    MontoPagoCronogramaActualizar.Formula = Cronograma.Formula;
                    MontoPagoCronogramaActualizar.MatriculaEnProceso = Cronograma.MatriculaEnProceso;
                    MontoPagoCronogramaActualizar.CodigoMatricula = Cronograma.CodigoMatricula;
                    MontoPagoCronogramaActualizar.FechaModificacion = DateTime.Now;
                    MontoPagoCronogramaActualizar.UsuarioModificacion = Cronograma.Usuario;

                    _repMontoPagoCronograma.Update(MontoPagoCronogramaActualizar);
                    //Fin Update
                }
                if (Cronograma.ListaDetalleCuotas != null)
                {
                    foreach (var item in Cronograma.ListaDetalleCuotas)
                    {
                        item.Id = 0;
                        item.IdMontoPagoCronograma = Cronograma.Id;
                        item.Estado = true;
                        item.FechaCreacion = DateTime.Now;
                        item.UsuarioCreacion = Cronograma.Usuario;
                        item.FechaModificacion = DateTime.Now;
                        item.RowVersion = null;
                        item.UsuarioModificacion = Cronograma.Usuario;
                        _repMontoPagoCronogramaDetalle.Insert(item);
                    }
                }
                if (Cronograma.EsAprobado)
                {
                    _repMontoPagoCronograma.GenerarCronogramaByCoordinador(Cronograma.Id);
                }
                else
                {
                    _repMontoPagoCronograma.EliminarCronogramaVentasByCordinador(Cronograma.Id);
                }

                scope.Complete();
            }
        }

        [Route("[Action]/{IdCronograma}/{Usuario}")]
        [HttpPost]
        public IActionResult CongelarCronogramaAlumno(int IdCronograma, string Usuario)
        {
            try
            {
                EstructuraEspecificaBO estructuraEspecifica = new EstructuraEspecificaBO();
                var estructura=estructuraEspecifica.CongelarEstructuraEspecifica(IdCronograma,Usuario);
                return Ok(estructura);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Max Mantilla
        /// Fecha: 30/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Congela la estructuta curricular por IdMatriculaCabecera
        /// </summary>
        /// <returns>EstructuraEspecificaBO</returns>
        [Route("[Action]/{IdMatriculaCabecera}/{Usuario}")]
        [HttpPost]
        public IActionResult CongelarCronogramaAlumnoPorIdMatricula(int IdMatriculaCabecera, string Usuario)
        {
            try
            {
                EstructuraEspecificaBO estructuraEspecifica = new EstructuraEspecificaBO();
                var estructura = estructuraEspecifica.CongelarEstructuraEspecificaPorIdMatricula(IdMatriculaCabecera, Usuario);
                return Ok(estructura);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{Cantidad}")]
        [HttpPost]
        public IActionResult CongelarCronogramaAlumnoMasivo(int Cantidad)
        {
            try
            {
                EstructuraEspecificaBO estructuraEspecifica = new EstructuraEspecificaBO();
                var estructura = estructuraEspecifica.CongelarEstructuraEspecificaMasivo(Cantidad);
                return Ok(estructura);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        private void EliminarCronograma(ref MontoPagoCronogramaBO Cronograma)
        {


            integraDBContext contexto = new integraDBContext();
            MontoPagoCronogramaRepositorio _repMontoPagoCronograma = new MontoPagoCronogramaRepositorio(contexto);
            MontoPagoCronogramaDetalleRepositorio _repMontoPagoCronogramaDetalle = new MontoPagoCronogramaDetalleRepositorio(contexto);

            using (TransactionScope scope = new TransactionScope())
            {
                //Delete Orquesta
                int IdCronograma = Cronograma.Id;
                var lista = _repMontoPagoCronogramaDetalle.GetBy(w => w.IdMontoPagoCronograma == IdCronograma).ToList();
                foreach (var item in lista)
                {
                    _repMontoPagoCronogramaDetalle.Delete(item.Id, Cronograma.Usuario);
                }
                //Fin Delete Orquesta
                _repMontoPagoCronograma.Delete(Cronograma.Id, Cronograma.Usuario);

                if (Cronograma.EsAprobado)
                {
                    _repMontoPagoCronograma.GenerarCronogramaByCoordinador(Cronograma.Id);
                }
                else
                {
                    _repMontoPagoCronograma.EliminarCronogramaVentasByCordinador(Cronograma.Id);
                }

                scope.Complete();
            }
        }

    }
}