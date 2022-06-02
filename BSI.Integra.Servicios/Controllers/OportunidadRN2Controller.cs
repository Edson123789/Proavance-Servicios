using System;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.Transversal.BO;
using System.Linq;
using BSI.Integra.Aplicacion.Transversal.Helper;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/OportunidadRN2")]
    public class OportunidadRN2Controller : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public OportunidadRN2Controller(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult InsertarNuevosRN2()
        {
            RN2OportunidadErrorDTO respuesta = new RN2OportunidadErrorDTO();
            try
            {
                HistoricoOportunidadRn2Repositorio rep = new HistoricoOportunidadRn2Repositorio();
                int resultado = rep.InsertaNuevosRN2();
                respuesta.Error = "OK";
                respuesta.MensajeError = resultado.ToString();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Error = "ERROR";
                respuesta.MensajeError = ex.ToString();
                return Ok(respuesta);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult PreProcesarRN2([FromBody] FiltroFechaOportunidadRN2DTO FiltroFecha)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TotalRN2OportunidadDTO respuesta = new TotalRN2OportunidadDTO();
            try
            {
                HistoricoOportunidadRn2Repositorio rep = new HistoricoOportunidadRn2Repositorio();
                var resultado = rep.ProcesarProgramadosRN2(FiltroFecha);
                respuesta.TotalRN2AProcesar = resultado.TotalRN2AProcesar;
                respuesta.TotalRN2Clasificados = resultado.TotalRN2Clasificados;
                respuesta.ExisteError = "OK";
                respuesta.Error = "";
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                respuesta.ExisteError = "ERROR";
                respuesta.Error = ex.ToString();
                return Ok(respuesta);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult ProcesarRN2([FromBody] FiltroFechaOportunidadRN2DTO FiltroFecha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            RN2OportunidadErrorDTO respuesta = new RN2OportunidadErrorDTO();
            try
            {
                integraDBContext contexto = new integraDBContext();
                HistoricoOportunidadRn2Repositorio rep = new HistoricoOportunidadRn2Repositorio(contexto);
                HistoricoDetalleOportunidadRn2Repositorio repDetalle = new HistoricoDetalleOportunidadRn2Repositorio(contexto);
                CalculoOportunidadRn2Repositorio repCalculo = new CalculoOportunidadRn2Repositorio(contexto);
                ActividadDetalleRepositorio repActividadDetalle = new ActividadDetalleRepositorio(contexto);

                ActividadDetalleRepositorio actividadDetalleRepositorio = new ActividadDetalleRepositorio(contexto);
                OportunidadRepositorio oportunidadRepositorio = new OportunidadRepositorio(contexto);

                var listaRn2 = rep.ObtenerRN2PorFechaProgramada(FiltroFecha);
                foreach (var item in listaRn2)
                {
                    HistoricoOportunidadRn2BO nuevoHistorico = rep.FirstBy(x => x.IdOportunidadRn2 == item.IdOportunidad);
                    if (repDetalle.GetBy(x => x.IdHistoricoOportunidadRn2 == nuevoHistorico.Id).Count() == 0)
                    {
                        HistoricoDetalleOportunidadRn2BO detalleInicio = new HistoricoDetalleOportunidadRn2BO();
                        detalleInicio.IdAlumno = item.IdAlumno;
                        detalleInicio.EstadoValidacionRn2 = "VIGENTE";
                        detalleInicio.IdFaseOportunidadActual = item.IdFaseOportunidad;
                        detalleInicio.FechaProgramacionActual = item.UltimaFechaProgramada;
                        detalleInicio.FechaProgramacionNueva = null;
                        detalleInicio.IdOportunidadClasificacion = null;
                        detalleInicio.IdFaseOportunidadClasificacion = null;
                        detalleInicio.FechaLog = DateTime.Now;
                        detalleInicio.FechaCreacion = DateTime.Now;
                        detalleInicio.FechaModificacion = DateTime.Now;
                        detalleInicio.UsuarioCreacion = "AutomatizacionRN2";
                        detalleInicio.UsuarioModificacion = "AutomatizacionRN2";
                        detalleInicio.Estado = true;

                        nuevoHistorico.HistoricoDetalleOportunidadRn2Inicio = detalleInicio;
                    }
                    var calculoRN2 = repCalculo.FirstBy(x => x.IdOportunidadRn2 == item.IdOportunidad);
                    if(calculoRN2 != null)
                    {
                   
                        if(calculoRN2.TieneOportunidadAbierta || calculoRN2.TieneOportunidadCerradaSesentaDias)
                        {
                            try
                            {
                                OportunidadBO Oportunidad = new OportunidadBO(item.IdOportunidad, "AutomatizacionRN2", contexto);

                                ActividadDetalleBO actividadDetalleDTO = actividadDetalleRepositorio.FirstById(Oportunidad.IdActividadDetalleUltima);
                                actividadDetalleDTO.Comentario = "Reprogramacion Automatica RN2";
                                actividadDetalleDTO.IdOcurrencia = ValorEstatico.IdOcurrenciaReprogramacionAutomaticaRN2;
                                actividadDetalleDTO.IdOcurrenciaActividad = null;
                                actividadDetalleDTO.IdAlumno = Oportunidad.IdAlumno;
                                actividadDetalleDTO.IdOportunidad = item.IdOportunidad;
                                actividadDetalleDTO.IdEstadoActividadDetalle = 0;
                                actividadDetalleDTO.IdActividadCabecera = Oportunidad.IdActividadCabeceraUltima;
                                actividadDetalleDTO.FechaProgramada = actividadDetalleDTO.FechaProgramada.Value.AddDays(30);
                                Oportunidad.ActividadAntigua = actividadDetalleDTO;
                            

                                OportunidadDTO oportunidadDTO = new OportunidadDTO();
                                oportunidadDTO.IdEstadoOportunidad = Oportunidad.IdEstadoOportunidad;
                                oportunidadDTO.IdFaseOportunidad = Oportunidad.IdFaseOportunidad;
                                oportunidadDTO.IdFaseOportunidadIc = Oportunidad.IdFaseOportunidadIc;
                                oportunidadDTO.IdFaseOportunidadIp = Oportunidad.IdFaseOportunidadIp;
                                oportunidadDTO.IdFaseOportunidadPf = Oportunidad.IdFaseOportunidadPf;
                                oportunidadDTO.FechaEnvioFaseOportunidadPf = Oportunidad.FechaEnvioFaseOportunidadPf;
                                oportunidadDTO.FechaPagoFaseOportunidadIc = Oportunidad.FechaPagoFaseOportunidadIc;
                                oportunidadDTO.FechaPagoFaseOportunidadPf = Oportunidad.FechaPagoFaseOportunidadPf;
                                oportunidadDTO.CodigoPagoIc = Oportunidad.CodigoPagoIc;

                                Oportunidad.FinalizarActividad(false, oportunidadDTO);

                                Oportunidad.ProgramaActividad();
                           
                                using (TransactionScope scope = new TransactionScope())
                                {
                                    repActividadDetalle.Insert(Oportunidad.ActividadNuevaProgramarActividad);
                                    Oportunidad.IdActividadDetalleUltima = Oportunidad.ActividadNuevaProgramarActividad.Id;
                                    Oportunidad.ActividadNuevaProgramarActividad = null;
                                    oportunidadRepositorio.Update(Oportunidad);
                                    scope.Complete();
                                }

                                HistoricoDetalleOportunidadRn2BO detalle = new HistoricoDetalleOportunidadRn2BO();
                                if (calculoRN2.TieneOportunidadAbierta)
                                {
                                    int IdFaseOportunidadCambio = oportunidadRepositorio.FirstById(calculoRN2.IdOportunidadAbierta.Value).IdFaseOportunidad;
                                    detalle.IdAlumno = item.IdAlumno;
                                    detalle.EstadoValidacionRn2 = "REPROGRAMADO";
                                    detalle.IdFaseOportunidadActual = item.IdFaseOportunidad;
                                    detalle.FechaProgramacionActual = item.UltimaFechaProgramada;
                                    detalle.FechaProgramacionNueva = actividadDetalleDTO.FechaProgramada; ;
                                    detalle.IdOportunidadClasificacion = calculoRN2.IdOportunidadAbierta;
                                    detalle.IdFaseOportunidadClasificacion = IdFaseOportunidadCambio;
                                    detalle.FechaLog = DateTime.Now;
                                    detalle.FechaCreacion = DateTime.Now;
                                    detalle.FechaModificacion = DateTime.Now;
                                    detalle.UsuarioCreacion = "AutomatizacionRN2";
                                    detalle.UsuarioModificacion = "AutomatizacionRN2";
                                    detalle.Estado = true;
                                }
                                else
                                {
                                    int IdFaseOportunidadCambio = oportunidadRepositorio.FirstById(calculoRN2.IdOportunidadCerradaSesentaDias.Value).IdFaseOportunidad;
                                    detalle.IdAlumno = item.IdAlumno;
                                    detalle.EstadoValidacionRn2 = "REPROGRAMADO";
                                    detalle.IdFaseOportunidadActual = item.IdFaseOportunidad;
                                    detalle.FechaProgramacionActual = item.UltimaFechaProgramada;
                                    detalle.FechaProgramacionNueva = actividadDetalleDTO.FechaProgramada; 
                                    detalle.IdOportunidadClasificacion = calculoRN2.IdOportunidadCerradaSesentaDias;
                                    detalle.IdFaseOportunidadClasificacion = IdFaseOportunidadCambio;
                                    detalle.FechaLog = DateTime.Now;
                                    detalle.FechaCreacion = DateTime.Now;
                                    detalle.FechaModificacion = DateTime.Now;
                                    detalle.UsuarioCreacion = "AutomatizacionRN2";
                                    detalle.UsuarioModificacion = "AutomatizacionRN2";
                                    detalle.Estado = true;
                                }
                                nuevoHistorico.FechaProgramacionNueva = actividadDetalleDTO.FechaProgramada;
                                nuevoHistorico.EstadoValidacionRn2 = detalle.EstadoValidacionRn2;
                                nuevoHistorico.IdOportunidadClasificacion = detalle.IdOportunidadClasificacion;
                                nuevoHistorico.IdFaseOportunidadClasificacion = detalle.IdFaseOportunidadClasificacion;
                                nuevoHistorico.FechaModificacion = DateTime.Now;
                                nuevoHistorico.UsuarioModificacion = "AutomatizacionRN2";
                                nuevoHistorico.FechaLog = detalle.FechaLog;
                                nuevoHistorico.HistoricoDetalleOportunidadRn2 = detalle;
                                using (TransactionScope scope = new TransactionScope())
                                {
                                    rep.Update(nuevoHistorico);
                                    scope.Complete();
                                }
                            }
                            catch (Exception e)
                            {
                                continue;
                            }




                        }
                        else
                        {
                            if (calculoRN2.TieneOportunidadCerradaPosterior)
                            {
                                // Cerrar Oportunidad
                                try
                                {
                                    OportunidadBO Oportunidad = new OportunidadBO(item.IdOportunidad, "AutomatizacionRN2", contexto);


                                    ActividadDetalleBO actividadDetalleDTO = actividadDetalleRepositorio.FirstById(Oportunidad.IdActividadDetalleUltima);
                                    actividadDetalleDTO.Comentario = "Cerrado Automatico RN2";
                                    actividadDetalleDTO.IdOcurrencia = ValorEstatico.IdOcurrenciaCierreRN8;
                                    actividadDetalleDTO.IdOcurrenciaActividad = null;
                                    actividadDetalleDTO.IdAlumno = Oportunidad.IdAlumno;
                                    actividadDetalleDTO.IdOportunidad = item.IdOportunidad;
                                    actividadDetalleDTO.IdEstadoActividadDetalle = 0;
                                    actividadDetalleDTO.IdActividadCabecera = Oportunidad.IdActividadCabeceraUltima;
                                    Oportunidad.ActividadAntigua = actividadDetalleDTO;
                                    Oportunidad.ActividadNueva = new ActividadDetalleBO();

                                    OportunidadDTO oportunidadDTO = new OportunidadDTO();
                                    oportunidadDTO.IdEstadoOportunidad = Oportunidad.IdEstadoOportunidad;
                                    oportunidadDTO.IdFaseOportunidad = Oportunidad.IdFaseOportunidad;
                                    oportunidadDTO.IdFaseOportunidadIc = Oportunidad.IdFaseOportunidadIc;
                                    oportunidadDTO.IdFaseOportunidadIp = Oportunidad.IdFaseOportunidadIp;
                                    oportunidadDTO.IdFaseOportunidadPf = Oportunidad.IdFaseOportunidadPf;
                                    oportunidadDTO.FechaEnvioFaseOportunidadPf = Oportunidad.FechaEnvioFaseOportunidadPf;
                                    oportunidadDTO.FechaPagoFaseOportunidadIc = Oportunidad.FechaPagoFaseOportunidadIc;
                                    oportunidadDTO.FechaPagoFaseOportunidadPf = Oportunidad.FechaPagoFaseOportunidadPf;
                                    oportunidadDTO.CodigoPagoIc = Oportunidad.CodigoPagoIc;

                                    Oportunidad.FinalizarActividad(false, oportunidadDTO);

                                    using (TransactionScope scope = new TransactionScope())
                                    {
                                        oportunidadRepositorio.Update(Oportunidad);
                                        scope.Complete();
                                    }
                                    int IdFaseOportunidadCambio = oportunidadRepositorio.FirstById(calculoRN2.IdOportunidadCerradaPosterior).IdFaseOportunidad;
                                    HistoricoDetalleOportunidadRn2BO detalle = new HistoricoDetalleOportunidadRn2BO();
                                    detalle.IdAlumno = item.IdAlumno;
                                    detalle.EstadoValidacionRn2 = "CERRADO";
                                    detalle.IdFaseOportunidadActual = item.IdFaseOportunidad;
                                    detalle.FechaProgramacionActual = item.UltimaFechaProgramada;
                                    detalle.FechaProgramacionNueva = item.UltimaFechaProgramada;
                                    detalle.IdOportunidadClasificacion = calculoRN2.IdOportunidadCerradaPosterior;
                                    detalle.IdFaseOportunidadClasificacion = IdFaseOportunidadCambio;
                                    detalle.FechaLog = DateTime.Now;
                                    detalle.FechaCreacion = DateTime.Now;
                                    detalle.FechaModificacion = DateTime.Now;
                                    detalle.UsuarioCreacion = "AutomatizacionRN2";
                                    detalle.UsuarioModificacion = "AutomatizacionRN2";
                                    detalle.Estado = true;

                                    nuevoHistorico.EstadoValidacionRn2 = detalle.EstadoValidacionRn2;
                                    nuevoHistorico.IdOportunidadClasificacion = calculoRN2.IdOportunidadCerradaPosterior;
                                    nuevoHistorico.IdFaseOportunidadClasificacion = calculoRN2.IdOportunidadCerradaPosterior;
                                    nuevoHistorico.FechaModificacion = DateTime.Now;
                                    nuevoHistorico.UsuarioModificacion = "AutomatizacionRN2";
                                    nuevoHistorico.FechaLog = detalle.FechaLog;


                                    nuevoHistorico.HistoricoDetalleOportunidadRn2 = detalle;
                                    using (TransactionScope scope = new TransactionScope())
                                    {
                                        rep.Update(nuevoHistorico);
                                        scope.Complete();
                                    }
                                }
                                catch (Exception e)
                                {
                                    continue;
                                }

                               


                            }
                            else
                            {
                                // no hacer nada
                                //HistoricoDetalleOportunidadRn2BO detalle = new HistoricoDetalleOportunidadRn2BO();
                                //detalle.IdAlumno = item.IdAlumno;
                                //detalle.EstadoValidacionRn2 = "ITERADO";
                                //detalle.IdFaseOportunidadActual = item.IdFaseOportunidad;
                                //detalle.FechaProgramacionActual = item.UltimaFechaProgramada;
                                //detalle.FechaProgramacionNueva = item.UltimaFechaProgramada;
                                //detalle.IdOportunidadClasificacion = null;
                                //detalle.IdFaseOportunidadClasificacion = null;
                                //detalle.FechaLog = DateTime.Now;
                                //detalle.FechaCreacion = DateTime.Now;
                                //detalle.FechaModificacion = DateTime.Now;
                                //detalle.UsuarioCreacion = "AutomatizacionRN2";
                                //detalle.UsuarioModificacion = "AutomatizacionRN2";
                                //detalle.Estado = true;

                                nuevoHistorico.EstadoValidacionRn2 = "ITERADO";
                                nuevoHistorico.FechaModificacion = DateTime.Now;
                                nuevoHistorico.UsuarioModificacion = "AutomatizacionRN2";
                                nuevoHistorico.FechaLog = DateTime.Now;

                                using (TransactionScope scope = new TransactionScope())
                                {
                                    rep.Update(nuevoHistorico);
                                    scope.Complete();
                                }
                            }
                            

                        }
                      
                    }
                }
               respuesta.Error = "OK";
                
                return Ok(respuesta);
                
            }
            catch (Exception ex)
            {
                respuesta.Error = "ERROR";
                respuesta.MensajeError = ex.ToString();
                return Ok(respuesta);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult ProcesarRN2MesesPasados([FromBody] FiltroFechaOportunidadRN2DTO FiltroFecha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            RN2OportunidadErrorDTO respuesta = new RN2OportunidadErrorDTO();
            try
            {
                integraDBContext contexto = new integraDBContext();
                HistoricoOportunidadRn2Repositorio rep = new HistoricoOportunidadRn2Repositorio(contexto);
                HistoricoDetalleOportunidadRn2Repositorio repDetalle = new HistoricoDetalleOportunidadRn2Repositorio(contexto);
                CalculoOportunidadRn2Repositorio repCalculo = new CalculoOportunidadRn2Repositorio(contexto);
                ActividadDetalleRepositorio repActividadDetalle = new ActividadDetalleRepositorio(contexto);

                ActividadDetalleRepositorio actividadDetalleRepositorio = new ActividadDetalleRepositorio(contexto);
                OportunidadRepositorio oportunidadRepositorio = new OportunidadRepositorio(contexto);

                var listaRn2 = rep.ObtenerRN2PorFechaProgramada(FiltroFecha);
                foreach (var item in listaRn2)
                {
                    try
                    {

                        HistoricoOportunidadRn2BO nuevoHistorico = rep.FirstBy(x => x.IdOportunidadRn2 == item.IdOportunidad);
                        if (repDetalle.GetBy(x => x.IdHistoricoOportunidadRn2 == nuevoHistorico.Id).Count() == 0)
                        {
                            HistoricoDetalleOportunidadRn2BO detalleInicio = new HistoricoDetalleOportunidadRn2BO();
                            detalleInicio.IdAlumno = item.IdAlumno;
                            detalleInicio.EstadoValidacionRn2 = "VIGENTE";
                            detalleInicio.IdFaseOportunidadActual = item.IdFaseOportunidad;
                            detalleInicio.FechaProgramacionActual = item.UltimaFechaProgramada;
                            detalleInicio.FechaProgramacionNueva = null;
                            detalleInicio.IdOportunidadClasificacion = null;
                            detalleInicio.IdFaseOportunidadClasificacion = null;
                            detalleInicio.FechaLog = DateTime.Now;
                            detalleInicio.FechaCreacion = DateTime.Now;
                            detalleInicio.FechaModificacion = DateTime.Now;
                            detalleInicio.UsuarioCreacion = "AutomatizacionRN2";
                            detalleInicio.UsuarioModificacion = "AutomatizacionRN2";
                            detalleInicio.Estado = true;

                            nuevoHistorico.HistoricoDetalleOportunidadRn2Inicio = detalleInicio;
                        }
                        var calculoRN2 = repCalculo.FirstBy(x => x.IdOportunidadRn2 == item.IdOportunidad );
                        if (calculoRN2 != null)
                        {

                            if (calculoRN2.TieneOportunidadAbierta)
                            {
                                try
                                {
                                    OportunidadBO Oportunidad = new OportunidadBO(item.IdOportunidad, "AutomatizacionRN2", contexto);
                                    var FechaTreintaDiasProgramado = DateTime.Now.AddDays(30);

                                    ActividadDetalleBO actividadDetalleDTO = actividadDetalleRepositorio.FirstById(Oportunidad.IdActividadDetalleUltima);
                                    actividadDetalleDTO.Comentario = "Reprogramacion Automatica RN2";
                                    actividadDetalleDTO.IdOcurrencia = ValorEstatico.IdOcurrenciaReprogramacionAutomaticaRN2;
                                    actividadDetalleDTO.IdOcurrenciaActividad = null;
                                    actividadDetalleDTO.IdAlumno = Oportunidad.IdAlumno;
                                    actividadDetalleDTO.IdOportunidad = item.IdOportunidad;
                                    actividadDetalleDTO.IdEstadoActividadDetalle = 0;
                                    actividadDetalleDTO.IdActividadCabecera = Oportunidad.IdActividadCabeceraUltima;
                                    actividadDetalleDTO.FechaProgramada = FechaTreintaDiasProgramado;
                                    Oportunidad.ActividadAntigua = actividadDetalleDTO;


                                    OportunidadDTO oportunidadDTO = new OportunidadDTO();
                                    oportunidadDTO.IdEstadoOportunidad = Oportunidad.IdEstadoOportunidad;
                                    oportunidadDTO.IdFaseOportunidad = Oportunidad.IdFaseOportunidad;
                                    oportunidadDTO.IdFaseOportunidadIc = Oportunidad.IdFaseOportunidadIc;
                                    oportunidadDTO.IdFaseOportunidadIp = Oportunidad.IdFaseOportunidadIp;
                                    oportunidadDTO.IdFaseOportunidadPf = Oportunidad.IdFaseOportunidadPf;
                                    oportunidadDTO.FechaEnvioFaseOportunidadPf = Oportunidad.FechaEnvioFaseOportunidadPf;
                                    oportunidadDTO.FechaPagoFaseOportunidadIc = Oportunidad.FechaPagoFaseOportunidadIc;
                                    oportunidadDTO.FechaPagoFaseOportunidadPf = Oportunidad.FechaPagoFaseOportunidadPf;
                                    oportunidadDTO.CodigoPagoIc = Oportunidad.CodigoPagoIc;

                                    Oportunidad.FinalizarActividad(false, oportunidadDTO);

                                    Oportunidad.ProgramaActividad();

                                    using (TransactionScope scope = new TransactionScope())
                                    {
                                        repActividadDetalle.Insert(Oportunidad.ActividadNuevaProgramarActividad);
                                        Oportunidad.IdActividadDetalleUltima = Oportunidad.ActividadNuevaProgramarActividad.Id;
                                        Oportunidad.ActividadNuevaProgramarActividad = null;
                                        oportunidadRepositorio.Update(Oportunidad);
                                        scope.Complete();
                                    }

                                    HistoricoDetalleOportunidadRn2BO detalle = new HistoricoDetalleOportunidadRn2BO();
                                    if (calculoRN2.TieneOportunidadAbierta)
                                    {
                                        int IdFaseOportunidadCambio = oportunidadRepositorio.FirstById(calculoRN2.IdOportunidadAbierta.Value).IdFaseOportunidad;
                                        detalle.IdAlumno = item.IdAlumno;
                                        detalle.EstadoValidacionRn2 = "REPROGRAMADO";
                                        detalle.IdFaseOportunidadActual = item.IdFaseOportunidad;
                                        detalle.FechaProgramacionActual = item.UltimaFechaProgramada;
                                        detalle.FechaProgramacionNueva = actividadDetalleDTO.FechaProgramada; ;
                                        detalle.IdOportunidadClasificacion = calculoRN2.IdOportunidadAbierta;
                                        detalle.IdFaseOportunidadClasificacion = IdFaseOportunidadCambio;
                                        detalle.FechaLog = DateTime.Now;
                                        detalle.FechaCreacion = DateTime.Now;
                                        detalle.FechaModificacion = DateTime.Now;
                                        detalle.UsuarioCreacion = "AutomatizacionRN2";
                                        detalle.UsuarioModificacion = "AutomatizacionRN2";
                                        detalle.Estado = true;
                                    }
                                    else
                                    {
                                        int IdFaseOportunidadCambio = oportunidadRepositorio.FirstById(calculoRN2.IdOportunidadCerradaSesentaDias.Value).IdFaseOportunidad;
                                        detalle.IdAlumno = item.IdAlumno;
                                        detalle.EstadoValidacionRn2 = "REPROGRAMADO";
                                        detalle.IdFaseOportunidadActual = item.IdFaseOportunidad;
                                        detalle.FechaProgramacionActual = item.UltimaFechaProgramada;
                                        detalle.FechaProgramacionNueva = actividadDetalleDTO.FechaProgramada;
                                        detalle.IdOportunidadClasificacion = calculoRN2.IdOportunidadCerradaSesentaDias;
                                        detalle.IdFaseOportunidadClasificacion = IdFaseOportunidadCambio;
                                        detalle.FechaLog = DateTime.Now;
                                        detalle.FechaCreacion = DateTime.Now;
                                        detalle.FechaModificacion = DateTime.Now;
                                        detalle.UsuarioCreacion = "AutomatizacionRN2";
                                        detalle.UsuarioModificacion = "AutomatizacionRN2";
                                        detalle.Estado = true;
                                    }
                                    nuevoHistorico.FechaProgramacionNueva = actividadDetalleDTO.FechaProgramada;
                                    nuevoHistorico.EstadoValidacionRn2 = detalle.EstadoValidacionRn2;
                                    nuevoHistorico.IdOportunidadClasificacion = detalle.IdOportunidadClasificacion;
                                    nuevoHistorico.IdFaseOportunidadClasificacion = detalle.IdFaseOportunidadClasificacion;
                                    nuevoHistorico.FechaModificacion = DateTime.Now;
                                    nuevoHistorico.UsuarioModificacion = "AutomatizacionRN2";
                                    nuevoHistorico.FechaLog = detalle.FechaLog;
                                    nuevoHistorico.HistoricoDetalleOportunidadRn2 = detalle;
                                    using (TransactionScope scope = new TransactionScope())
                                    {
                                        rep.Update(nuevoHistorico);
                                        scope.Complete();
                                    }
                                }
                                catch (Exception e)
                                {
                                    continue;
                                }




                            }
                            else
                            {
                                if (calculoRN2.TieneOportunidadCerradaPosterior)
                                {
                                    // Cerrar Oportunidad
                                    try
                                    {
                                        OportunidadBO Oportunidad = new OportunidadBO(item.IdOportunidad, "AutomatizacionRN2", contexto);


                                        ActividadDetalleBO actividadDetalleDTO = actividadDetalleRepositorio.FirstById(Oportunidad.IdActividadDetalleUltima);
                                        actividadDetalleDTO.Comentario = "Cerrado Automatico RN2";
                                        actividadDetalleDTO.IdOcurrencia = ValorEstatico.IdOcurrenciaCierreRN8;
                                        actividadDetalleDTO.IdOcurrenciaActividad = null;
                                        actividadDetalleDTO.IdAlumno = Oportunidad.IdAlumno;
                                        actividadDetalleDTO.IdOportunidad = item.IdOportunidad;
                                        actividadDetalleDTO.IdEstadoActividadDetalle = 0;
                                        actividadDetalleDTO.IdActividadCabecera = Oportunidad.IdActividadCabeceraUltima;
                                        Oportunidad.ActividadAntigua = actividadDetalleDTO;
                                        Oportunidad.ActividadNueva = new ActividadDetalleBO();

                                        OportunidadDTO oportunidadDTO = new OportunidadDTO();
                                        oportunidadDTO.IdEstadoOportunidad = Oportunidad.IdEstadoOportunidad;
                                        oportunidadDTO.IdFaseOportunidad = Oportunidad.IdFaseOportunidad;
                                        oportunidadDTO.IdFaseOportunidadIc = Oportunidad.IdFaseOportunidadIc;
                                        oportunidadDTO.IdFaseOportunidadIp = Oportunidad.IdFaseOportunidadIp;
                                        oportunidadDTO.IdFaseOportunidadPf = Oportunidad.IdFaseOportunidadPf;
                                        oportunidadDTO.FechaEnvioFaseOportunidadPf = Oportunidad.FechaEnvioFaseOportunidadPf;
                                        oportunidadDTO.FechaPagoFaseOportunidadIc = Oportunidad.FechaPagoFaseOportunidadIc;
                                        oportunidadDTO.FechaPagoFaseOportunidadPf = Oportunidad.FechaPagoFaseOportunidadPf;
                                        oportunidadDTO.CodigoPagoIc = Oportunidad.CodigoPagoIc;

                                        Oportunidad.FinalizarActividad(false, oportunidadDTO);

                                        using (TransactionScope scope = new TransactionScope())
                                        {
                                            oportunidadRepositorio.Update(Oportunidad);
                                            scope.Complete();
                                        }
                                        int IdFaseOportunidadCambio = oportunidadRepositorio.FirstById(calculoRN2.IdOportunidadCerradaPosterior).IdFaseOportunidad;
                                        HistoricoDetalleOportunidadRn2BO detalle = new HistoricoDetalleOportunidadRn2BO();
                                        detalle.IdAlumno = item.IdAlumno;
                                        detalle.EstadoValidacionRn2 = "CERRADO";
                                        detalle.IdFaseOportunidadActual = item.IdFaseOportunidad;
                                        detalle.FechaProgramacionActual = item.UltimaFechaProgramada;
                                        detalle.FechaProgramacionNueva = item.UltimaFechaProgramada;
                                        detalle.IdOportunidadClasificacion = calculoRN2.IdOportunidadCerradaPosterior;
                                        detalle.IdFaseOportunidadClasificacion = IdFaseOportunidadCambio;
                                        detalle.FechaLog = DateTime.Now;
                                        detalle.FechaCreacion = DateTime.Now;
                                        detalle.FechaModificacion = DateTime.Now;
                                        detalle.UsuarioCreacion = "AutomatizacionRN2";
                                        detalle.UsuarioModificacion = "AutomatizacionRN2";
                                        detalle.Estado = true;

                                        nuevoHistorico.EstadoValidacionRn2 = detalle.EstadoValidacionRn2;
                                        nuevoHistorico.IdOportunidadClasificacion = calculoRN2.IdOportunidadCerradaPosterior;
                                        nuevoHistorico.IdFaseOportunidadClasificacion = calculoRN2.IdOportunidadCerradaPosterior;
                                        nuevoHistorico.FechaModificacion = DateTime.Now;
                                        nuevoHistorico.UsuarioModificacion = "AutomatizacionRN2";
                                        nuevoHistorico.FechaLog = detalle.FechaLog;


                                        nuevoHistorico.HistoricoDetalleOportunidadRn2 = detalle;
                                        using (TransactionScope scope = new TransactionScope())
                                        {
                                            rep.Update(nuevoHistorico);
                                            scope.Complete();
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        continue;
                                    }




                                }
                                else
                                {
                                    // no hacer nada
                                    //HistoricoDetalleOportunidadRn2BO detalle = new HistoricoDetalleOportunidadRn2BO();
                                    //detalle.IdAlumno = item.IdAlumno;
                                    //detalle.EstadoValidacionRn2 = "ITERADO";
                                    //detalle.IdFaseOportunidadActual = item.IdFaseOportunidad;
                                    //detalle.FechaProgramacionActual = item.UltimaFechaProgramada;
                                    //detalle.FechaProgramacionNueva = item.UltimaFechaProgramada;
                                    //detalle.IdOportunidadClasificacion = null;
                                    //detalle.IdFaseOportunidadClasificacion = null;
                                    //detalle.FechaLog = DateTime.Now;
                                    //detalle.FechaCreacion = DateTime.Now;
                                    //detalle.FechaModificacion = DateTime.Now;
                                    //detalle.UsuarioCreacion = "AutomatizacionRN2";
                                    //detalle.UsuarioModificacion = "AutomatizacionRN2";
                                    //detalle.Estado = true;

                                    nuevoHistorico.EstadoValidacionRn2 = "ITERADO";
                                    nuevoHistorico.FechaModificacion = DateTime.Now;
                                    nuevoHistorico.UsuarioModificacion = "AutomatizacionRN2";
                                    nuevoHistorico.FechaLog = DateTime.Now;

                                    using (TransactionScope scope = new TransactionScope())
                                    {
                                        rep.Update(nuevoHistorico);
                                        scope.Complete();
                                    }
                                }


                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                respuesta.Error = "OK";

                return Ok(respuesta);

            }
            catch (Exception ex)
            {
                respuesta.Error = "ERROR";
                respuesta.MensajeError = ex.ToString();
                return Ok(respuesta);
            }
        }
    }
}
