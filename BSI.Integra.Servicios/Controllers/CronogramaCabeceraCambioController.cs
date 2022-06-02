using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/CronogramaCabeceraCambio")]
    public class CronogramaCabeceraCambioController : BaseController<TCronogramaCabeceraCambio, ValidadorCronogramaCabeceraCambioControllerDTO>
    {
        public CronogramaCabeceraCambioController(IIntegraRepository<TCronogramaCabeceraCambio> repositorio, ILogger<BaseController<TCronogramaCabeceraCambio, ValidadorCronogramaCabeceraCambioControllerDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

        [Route("[Action]/{idPersonal}")]
        [HttpGet]
        public ActionResult ObtenerSolicitudesCambios(int idPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                //return Ok(_repMatriculaCabecera.ObtenerSolicitudesCambioCronograma(idPersonal));
                var listaRpta = _repMatriculaCabecera.ObtenerSolicitudesCambioCronograma(idPersonal);
                foreach (var item in listaRpta)
                {

                    string[] cambiostemp = item.Cambios.Split(',');
                    item.Cambios = "";
                    int cont = 1;
                    foreach (var cambio in cambiostemp)
                    {
                        item.Cambios = item.Cambios + cont + ". " + cambio + "<br>";
                        cont++;
                    }
                    item.Cambios = item.Cambios;
                }
                return Ok(listaRpta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerCronogramaFinal(int idMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio();
                var versionAprobada = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == idMatriculaCabecera && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();
                var listaCronogramaPagoDetalleFinal = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == idMatriculaCabecera && x.Version == versionAprobada.Version, x => new { x.Id, x.Cancelado, FlagCancelado = x.Cancelado, x.NroCuota, x.NroSubCuota, x.TipoCuota, x.FechaVencimiento, x.TotalPagar, x.Cuota, x.Mora, x.Saldo, x.Moneda, x.MontoPagado, x.FechaPago, x.IdFormaPago, x.IdCuenta, x.FechaPagoBanco, x.Enviado, x.Observaciones, x.IdDocumentoPago, x.NroDocumento, x.MonedaPago, x.TipoCambio, x.CuotaDolares, x.FechaProcesoPago, x.Version, x.FechaDeposito }).OrderBy(x => x.NroCuota).ThenBy(x => x.NroSubCuota);
                var versionNoAprobada = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == idMatriculaCabecera && x.Aprobado == false, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();
                //var versionNoAprobada = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == idMatriculaCabecera && x.Aprobado == false, x => new { x.Version });
                if (versionNoAprobada != null )
                {
                    //versionNoAprobada.GroupBy(x => x.Version).Max().FirstOrDefault();
                    var listaCronogramaNoAprobado = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == idMatriculaCabecera && x.Version == versionNoAprobada.Version, x => new { x.Id, x.Cancelado, x.NroCuota, x.NroSubCuota, x.TipoCuota, x.FechaVencimiento, x.TotalPagar, x.Cuota, x.Mora, x.Saldo, x.Moneda, x.MontoPagado, x.FechaPago, x.IdFormaPago, x.IdCuenta, x.FechaPagoBanco, x.Enviado, x.Observaciones, x.IdDocumentoPago, x.NroDocumento, x.MonedaPago, x.TipoCambio, x.CuotaDolares, x.FechaProcesoPago, x.Version }).OrderBy(x => x.NroCuota).ThenBy(x => x.NroSubCuota);
                    return Ok(new { listaCronogramaPagoDetalleFinal, listaCronogramaNoAprobado });
                }
                return Ok(new { listaCronogramaPagoDetalleFinal });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Insert(CronogramaCabeceraCambioDTO cronogramaDTO) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CronogramaCabeceraCambioRepositorio _repCronogramaCabeceraCambio = new CronogramaCabeceraCambioRepositorio();
                //CronogramaCabeceraCambioBO cronogramaBO = new CronogramaCabeceraCambioBO()
                //{
                //    IdCronogramaTipoModificacion = cronogramaDTO.IdCronogramaTipoModificacion,
                //};
                //_repCronogramaCabeceraCambio.Insert(cronogramaBO);
                //return Ok(cronogramaBO);


                if (_repCronogramaCabeceraCambio.Exist(cronogramaDTO.IdCronogramaTipoModificacion))
                {
                    var dataTemp = _repCronogramaCabeceraCambio.FirstById(cronogramaDTO.IdCronogramaTipoModificacion);
                    dataTemp.Observacion = cronogramaDTO.Observacion;
                    dataTemp.UsuarioModificacion = "";
                    dataTemp.FechaModificacion = DateTime.Now;
                    _repCronogramaCabeceraCambio.Update(dataTemp);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest( e.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult Aprobar([FromBody] CronogramaCabeceraCambioAprobarDTO cronogramaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CronogramaCabeceraCambioBO cronogramaCabeceraCambioBO = new CronogramaCabeceraCambioBO();
                List<string> listaCorreos = new List<string>
                {
                    //"bamontoya@bsginstitute.com",
                    "ccrispin@bsginstitute.com",
                    //"mapaza@bsginstitute.com"
                };
                PersonalRepositorio _repPersonal = new PersonalRepositorio();
                var personalAprobador =  _repPersonal.GetBy(x => x.Id == cronogramaDTO.IdPersonalAprobador, x => new { NombreCompleto = string.Concat(x.Nombres," ", x.Apellidos)}).FirstOrDefault();
                var rpta = 0;
                cronogramaDTO.IdsCambios = cronogramaDTO.IdsCambios.Replace(" ", "");
                string[] cambiosTemp = cronogramaDTO.IdsCambios.Split(',');
                CronogramaCabeceraCambioRepositorio _repCronogramaCabeceraCambio = new CronogramaCabeceraCambioRepositorio();
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                CronogramaDetalleCambioRepositorio _repCronogramaDetalleCambioRepositorio = new CronogramaDetalleCambioRepositorio();
                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == cronogramaDTO.CodigoMatricula, x => new { x.Id }).FirstOrDefault();
                List<AprobarRechazarCronogramaDTO> ObjetoTemporalSincronizacion = new List<AprobarRechazarCronogramaDTO>();
                for (int i = 0; i < cambiosTemp.Length; i++)
                {
                    rpta = _repCronogramaCabeceraCambio.AprobarRechazarCambios(matriculaCabeceraTemp.Id, Convert.ToInt32(cambiosTemp[i]), cronogramaDTO.Version, true, false).Valor;
                    //rpta = 1;
                    ObjetoTemporalSincronizacion.Add(new AprobarRechazarCronogramaDTO { IdMatriculaCabecera = matriculaCabeceraTemp.Id, IdCambio = cambiosTemp[i], Version = cronogramaDTO.Version, Aprobado = true, Cancelado = false });
                }
                var datosMatricula = _repMatriculaCabecera.ObtenerAlumnoProgramaEspecifico(matriculaCabeceraTemp.Id);
                var listaCambios = _repCronogramaDetalleCambioRepositorio.ObtenerCambiosPendientes(matriculaCabeceraTemp.Id, cronogramaDTO.Version);
                var centrocostoMatricula = _repMatriculaCabecera.ObtenerCentroCostoPorMatricula(matriculaCabeceraTemp.Id);
                var cambios = "";
                int cont = 1;
                foreach (var item in listaCambios)
                {
                    cambios = cambios + cont + ". " + item.TipoModificacion + " (" + item.SubTipo + "<br>";
                    cont++;
                }
                string mensaje = cronogramaCabeceraCambioBO.GenerarMensajeRespuestaCambioCronograma(cronogramaDTO.CodigoMatricula, datosMatricula[0].NombreCompleto, datosMatricula[0].PEspecifico, centrocostoMatricula.NombreCentroCosto, cronogramaDTO.Version, cronogramaDTO.NombreSolicitante, cambios, true, personalAprobador.NombreCompleto);
                HelperCorreo helperCorreo = new HelperCorreo();
                helperCorreo. envio_email("bamontoya@bsginstitute.com", "Integra Cronograma", "Respuesta de Cambio de Cronograma " + cronogramaDTO.CodigoMatricula, mensaje, listaCorreos );
                return Ok(new { rpta , ObjetoTemporalSincronizacion });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Rechazar([FromBody] CronogramaCabeceraCambioAprobarDTO cronogramaDTO)
        {
            List<string> listaCorreos = new List<string>
            {
                "bamontoya@bsginstitute.com",
                "ccrispin@bsginstitute.com",
                "mapaza@bsginstitute.com"
            };
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio();
                var personalAprobador = _repPersonal.GetBy(x => x.Id == cronogramaDTO.IdPersonalAprobador, x => new { NombreCompleto = string.Concat(x.Nombres, " ", x.Apellidos) }).FirstOrDefault();

                var rpta = 0;
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                CronogramaDetalleCambioRepositorio _repCronogramaDetalleCambioRepositorio = new CronogramaDetalleCambioRepositorio();
                CronogramaCabeceraCambioRepositorio _repCronogramaCabeceraCambio = new CronogramaCabeceraCambioRepositorio();
                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == cronogramaDTO.CodigoMatricula, x => new { x.Id }).FirstOrDefault();
                var listaCambios = _repCronogramaDetalleCambioRepositorio.ObtenerCambiosPendientes(matriculaCabeceraTemp.Id, cronogramaDTO.Version);
                //var listaCambios = _tcrm_CentroCostoService.GetCambiosCronogramas(matricula, Convert.ToInt32(version));
                cronogramaDTO.IdsCambios = cronogramaDTO.IdsCambios.Replace(" ", "");
                string[] cambiosTemp = cronogramaDTO.IdsCambios.Split(',');
                List<AprobarRechazarCronogramaDTO> ObjetoTemporalSincronizacion = new List<AprobarRechazarCronogramaDTO>();
                for (int i = 0; i < cambiosTemp.Length; i++)
                {
                    //rpta = _tcrm_CentroCostoService.AprobarRechazarCambios(matricula, cambiostemp[i], Convert.ToInt32(version), 0, 1);
                    rpta = _repCronogramaCabeceraCambio.AprobarRechazarCambios(matriculaCabeceraTemp.Id, Convert.ToInt32(cambiosTemp[i]), cronogramaDTO.Version, false, true).Valor;
                    //rpta = 1;
                    ObjetoTemporalSincronizacion.Add(new AprobarRechazarCronogramaDTO { IdMatriculaCabecera = matriculaCabeceraTemp.Id, IdCambio = cambiosTemp[i], Version = cronogramaDTO.Version, Aprobado = false, Cancelado = true });
                }
                var datosMatricula = _repMatriculaCabecera.ObtenerAlumnoProgramaEspecifico(matriculaCabeceraTemp.Id);
                //var datosM = _tcrm_CentroCostoService.GetPEspecificoAlumnoMatriculas(matricula);
                var centrocostoMatricula = _repMatriculaCabecera.ObtenerCentroCostoPorMatricula(matriculaCabeceraTemp.Id);
                //var centrocostoM = _tcrm_CentroCostoService.GetCentroCostoByMatricula(matricula);
                var cambios = "";
                int cont = 1;
                foreach (var item in listaCambios)
                {
                    cambios = cambios + cont + ". " + item.TipoModificacion + " (" + item.SubTipo + "<br>";
                    cont++;
                }
                CronogramaCabeceraCambioBO cronogramaCabeceraCambioBO = new CronogramaCabeceraCambioBO();
                string mensaje = cronogramaCabeceraCambioBO.GenerarMensajeRespuestaCambioCronograma(cronogramaDTO.CodigoMatricula, datosMatricula[0].NombreCompleto, datosMatricula[0].PEspecifico, centrocostoMatricula.NombreCentroCosto, cronogramaDTO.Version, cronogramaDTO.NombreSolicitante, cambios, false, personalAprobador.NombreCompleto);
                HelperCorreo helperCorreo = new HelperCorreo();
                helperCorreo.envio_email("bamontoya@bsginstitute.com", "Integra Cronograma", "Respuesta de Cambio de Cronograma " + cronogramaDTO.CodigoMatricula, mensaje, listaCorreos);
                return Json(new { Records = rpta , ObjetoTemporalSincronizacion });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{idPersonal}")]
        [HttpGet]
        public ActionResult ObtenerSolicitudesCambiosPendientes(int idPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio();
                var listaSolicitudesPendientes = _repMatriculaCabecera.ObtenerSolicitudesPendienteCambioCronograma(idPersonal).ToList();
                foreach (var item in listaSolicitudesPendientes)
                {
                    item.Enviados = "";
                    var listacuotasenviadas = _repCronogramaPagoDetalleFinal.GetBy(x => x.Version == item.Version && x.IdMatriculaCabecera == item.IdMatriculaCabecera, x => new { x.NroCuota, x.NroSubCuota, x.Enviado }).OrderBy( x => x.NroCuota).ThenBy( x => x.NroSubCuota );
                    foreach (var cuota in listacuotasenviadas)
                    {
                        item.Enviados = item.Enviados + cuota.NroCuota + " - " + cuota.NroSubCuota + (cuota.Enviado == true ? " Enviado" : " No enviado") + "<br>";
                    }
                    string[] cambiostemp = item.Cambios.Split(',');
                    item.Cambios = "";
                    int cont = 1;
                    foreach (var cambio in cambiostemp)
                    {
                        item.Cambios = item.Cambios + cont + ". " + cambio + "<br>";
                        cont++;
                    }
                    item.Cambios = item.Cambios;
                }
                return Ok(listaSolicitudesPendientes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }

    public class ValidadorCronogramaCabeceraCambioControllerDTO : AbstractValidator<TCronogramaCabeceraCambio>
    {
        public static ValidadorCronogramaCabeceraCambioControllerDTO Current = new ValidadorCronogramaCabeceraCambioControllerDTO();
        public ValidadorCronogramaCabeceraCambioControllerDTO()
        {
            RuleFor(objeto => objeto.SolicitadoPor).NotEmpty().WithMessage("Solicitado por es Obligatorio");
        }
    }
}
