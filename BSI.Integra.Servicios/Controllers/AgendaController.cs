using System;
using System.Collections.Generic;
using System.Linq;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Servicios.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using Mandrill.Models;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Scode.Helper;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using System.Net;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Comercial/Agenda
    /// Autor: Jorge Rivera - Fischer Valdez - Wilber Choque - Johan Cayo - Esthephany - Ansoli Espinoza - Gian Miranda - Jashin Salazar
    /// Fecha: 15/01/2021
    /// <summary>
    /// Contiene las acciones para la interaccion con las agendas de Integra.
    /// </summary>
    [Route("api/Agenda")]
    public class AgendaController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private WhatsAppUsuarioCredencialRepositorio _repTokenUsuario;
        private WhatsAppConfiguracionLogEjecucionRepositorio _repWhatsAppConfiguracionLogEjecucion;
        private AlumnoRepositorio _repAlumno;
        private PersonalRepositorio _repPersonal;
        private PlantillaClaveValorRepositorio _repPlantillaClaveValor;
        private WhatsAppConfiguracionRepositorio _repCredenciales;
        private PlantillaRepositorio _repPlantilla;
        private CentroCostoRepositorio _repCentroCosto;
        private PespecificoRepositorio _repPespecifico;
        private PgeneralRepositorio _repPgeneral;
        private readonly ConfiguracionEnvioMailingRepositorio _repConfiguracionEnvioMailing;
        private readonly ConfiguracionEnvioMailingDetalleRepositorio _repConfiguracionEnvioMailingDetalle;
        private readonly OportunidadRepositorio _repOportunidad;
        private readonly ConjuntoListaDetalleRepositorio _repConjuntoListaDetalle;
        private readonly ConjuntoListaResultadoRepositorio _repConjuntoListaResultado;
        private readonly OportunidadClasificacionOperacionesRepositorio _repOportunidadClasificacionOperaciones;
        private readonly PespecificoRepositorio _repPEspecifico;
        private readonly PespecificoSesionRepositorio _repPEspecificoSesion;
        private readonly PlantillaBaseRepositorio _repPlantillaBase;
        private readonly CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal;
        private readonly MatriculaCabeceraRepositorio _repMatriculaCabecera;
        private WhatsAppConfiguracionEnvioDetalleRepositorio _repWhatsAppConfiguracionEnvioDetalle;
        private WhatsAppMensajePublicidadRepositorio _repWhatsAppMensajePublicidad;
        private ReemplazoEtiquetaPlantillaBO reemplazoEtiquetaPlantilla;

        public AgendaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _integraDBContext.ChangeTracker.AutoDetectChangesEnabled = false;
            _repConjuntoListaDetalle = new ConjuntoListaDetalleRepositorio(integraDBContext);
            _repConjuntoListaResultado = new ConjuntoListaResultadoRepositorio(integraDBContext);
            _repConfiguracionEnvioMailing = new ConfiguracionEnvioMailingRepositorio(integraDBContext);
            _repConfiguracionEnvioMailingDetalle = new ConfiguracionEnvioMailingDetalleRepositorio(integraDBContext);

            _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio(_integraDBContext);
            _repWhatsAppConfiguracionLogEjecucion = new WhatsAppConfiguracionLogEjecucionRepositorio(_integraDBContext);

            _repPlantillaClaveValor = new PlantillaClaveValorRepositorio(_integraDBContext);
            _repCredenciales = new WhatsAppConfiguracionRepositorio(_integraDBContext);
            _repCentroCosto = new CentroCostoRepositorio(_integraDBContext);
            _repPespecifico = new PespecificoRepositorio(_integraDBContext);
            _repPgeneral = new PgeneralRepositorio(_integraDBContext);
            _repOportunidad = new OportunidadRepositorio(_integraDBContext);

            _repAlumno = new AlumnoRepositorio(_integraDBContext);
            _repPlantilla = new PlantillaRepositorio(_integraDBContext);
            _repPersonal = new PersonalRepositorio(_integraDBContext);
            _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
            _repPEspecificoSesion = new PespecificoSesionRepositorio(_integraDBContext);
            _repPlantillaBase = new PlantillaBaseRepositorio(_integraDBContext);
            _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio(_integraDBContext);
            _repOportunidadClasificacionOperaciones = new OportunidadClasificacionOperacionesRepositorio(_integraDBContext);
            _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);

        }

        /// TipoFuncion: GET
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las actividades realizadas en la agenda
        /// </summary>
        /// <returns> objetoBO: AgendaBO</returns>
        // GET: api/<controller>
        [HttpGet]
        public ActionResult ObtenerActividades(int IdAsesor, string CodigoAreaTrabajo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(IdAsesor, CodigoAreaTrabajo);
                return Ok(new { agenda.ActividadesAgenda, agenda.ActividadesRealizadas });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las tabs de actividades realizadas en la agenda
        /// </summary>
        /// <returns> objetoBO: AgendaBO</returns>
        // GET: api/<controller>
        [Route("[action]/{IdAsesor}/{Validar}/{CodigoAreaTrabajo}")]
        [HttpGet]
        public ActionResult ObtenerActividadesAgenda(int IdAsesor, bool Validar, string CodigoAreaTrabajo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var diferenciaHoraria = _repPersonal.ObtenerDiferenciaHoraria(IdAsesor);
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdAsesor = IdAsesor,
                    ValidacionTabs = Validar,
                    DiferenciaHoraria= diferenciaHoraria.Valor
                };
                agenda.ObtenerTabAgenda();
                return Ok(new { ActividadesAgenda = agenda.ActividadesAgenda, estadosTabs = agenda.HabilitarEstados, agenda.LogCarlos });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// Tipo Función: POST
        /// Autor: Carlos Crispin - Fischer Valdez - Jashin Salazar
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la actividad filtrada por el asesor y seguimiento academico
        /// </summary>
        /// <param name="ObjetoDTO">Objeto de clase ObjetoDTO</param>
        /// <returns>Response 200 (Objeto anonimo con lista de CompuestoActividadesEjecutadasTempDTO y la cantidad de registros)</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerRealizadas([FromBody] CompuestoAgendaFiltroDTO ObjetoDTO)
        {
            AgendaTabRepositorio _repAgendaTab = new AgendaTabRepositorio();
            OportunidadRepositorio _repOportunidad = new OportunidadRepositorio();
            ReportesRepositorio _repReportes = new ReportesRepositorio();

            if (ObjetoDTO.IdCentroCosto == null || ObjetoDTO.IdCentroCosto == "") ObjetoDTO.IdCentroCosto = "0";
            if (ObjetoDTO.IdAlumno == null || ObjetoDTO.IdAlumno == "") ObjetoDTO.IdAlumno = "0";
            if (ObjetoDTO.IdEstado == null || ObjetoDTO.IdEstado == "") ObjetoDTO.IdEstado = "0";
            if (ObjetoDTO.IdFaseOportunidad == null || ObjetoDTO.IdFaseOportunidad == "") ObjetoDTO.IdFaseOportunidad = "0";
            if (ObjetoDTO.IdTipoDato == null || ObjetoDTO.IdTipoDato == "") ObjetoDTO.IdTipoDato = "0";
            if (ObjetoDTO.IdOrigen == null || ObjetoDTO.IdOrigen == "") ObjetoDTO.IdOrigen = "0";
            if (ObjetoDTO.Fecha == null || ObjetoDTO.Fecha == "") ObjetoDTO.Fecha = "00000000";
            if (ObjetoDTO.IdProbabilidadActual == null || ObjetoDTO.IdProbabilidadActual == "") ObjetoDTO.IdProbabilidadActual = "0";
            if (ObjetoDTO.categoria == null || ObjetoDTO.categoria == "") ObjetoDTO.categoria = "_";

            int diaFecha = Convert.ToInt32(ObjetoDTO.Fecha.Substring(6, 2));
            var idsAsesor = ObjetoDTO.IdsAsesores;
            var fecha = ObjetoDTO.Fecha;
            var idCentroCosto = Convert.ToInt32(ObjetoDTO.IdCentroCosto);
            var idAlumno = Convert.ToInt32(ObjetoDTO.IdAlumno);
            var idFaseOportunidad = Convert.ToInt32(ObjetoDTO.IdFaseOportunidad);
            var idTipoDato = Convert.ToInt32(ObjetoDTO.IdTipoDato);
            var idOrigen = Convert.ToInt32(ObjetoDTO.IdOrigen);
            var take = Convert.ToInt32(ObjetoDTO.pageSize);
            var skip = Convert.ToInt32(ObjetoDTO.skip);
            var idsCategoriaOrigen = ObjetoDTO.categoria;
            var idProbabilidad = Convert.ToInt32(ObjetoDTO.IdProbabilidadActual);
            var idEstado = Convert.ToInt32(ObjetoDTO.IdEstado);

            var temp = _repAgendaTab.ObtenerActividadesRealizadasSP(idsAsesor, fecha, idCentroCosto, idAlumno, idFaseOportunidad, idTipoDato, idOrigen, take, skip, idsCategoriaOrigen, idProbabilidad, idEstado);
            var result = (from p in temp
                          group p by new
                          {
                              p.Id,
                              p.NombreCentroCosto,
                              p.Contacto,
                              p.CodigoFase,
                              p.NombreTipoDato,
                              p.Origen,
                              p.FechaProgramada,
                              p.FechaReal,
                              p.Duracion,
                              p.Actividad,
                              p.Ocurrencia,
                              p.Comentario,
                              p.Asesor,
                              p.IdContacto,
                              p.IdOportunidad,
                              p.ProbActual,
                              p.NombreCategoria,
                              p.IdCategoria,
                              p.FaseInicial,
                              p.FaseMaxima,
                              p.TotalOportunidades,
                              p.UnicoTimbrado,
                              p.UnicoContesto,
                              p.UnicoEstadoLlamada,
                              p.NumeroLlamadas,
                              p.EstadoOcurrencia,
                              p.UnicoClasificacion,
                              p.UnicoFechaLlamada,
                              p.NombreGrupo,
                              p.IdFaseOportunidadInicial,
                              p.FechaModificacion

                          } into g
                          select new CompuestoActividadesEjecutadasTempDTO
                          {
                              Id = g.Key.Id,
                              CentroCosto = g.Key.NombreCentroCosto,
                              Contacto = g.Key.Contacto,
                              CodigoFase = g.Key.CodigoFase,
                              NombreTipoDato = g.Key.NombreTipoDato,
                              Origen = g.Key.Origen,
                              FechaProgramada = g.Key.FechaProgramada,
                              FechaReal = g.Key.FechaReal,
                              Duracion = g.Key.Duracion,
                              Actividad = g.Key.Actividad,
                              Ocurrencia = g.Key.Ocurrencia,
                              Comentario = g.Key.Comentario,
                              Asesor = g.Key.Asesor,
                              IdContacto = g.Key.IdContacto,
                              IdOportunidad = g.Key.IdOportunidad,
                              ProbActual = g.Key.ProbActual,
                              Ca_nombre = g.Key.NombreCategoria,
                              IdCategoria = g.Key.IdCategoria,
                              FaseInicial = g.Key.FaseInicial,
                              FaseMaxima = g.Key.FaseMaxima,
                              TotalOportunidades = g.Key.TotalOportunidades,
                              UnicoTimbrado = g.Key.UnicoTimbrado.ToString(),
                              UnicoContesto = g.Key.UnicoContesto.ToString(),
                              UnicoEstadoLlamada = g.Key.UnicoEstadoLlamada,
                              UnicoClasificacion = g.Key.UnicoClasificacion,
                              UnicoFechaLlamada = g.Key.UnicoFechaLlamada,
                              NumeroLlamadas = g.Key.NumeroLlamadas,
                              Estado = g.Key.EstadoOcurrencia,
                              NombreGrupo = g.Key.NombreGrupo,
                              IdFaseOportunidadInicial = g.Key.IdFaseOportunidadInicial,
                              FechaModificacion = g.Key.FechaModificacion,

                              lista = g.Select(o => new CompuestoActividadesEjecutadasTemp_DetalleDTO
                              {
                                  Id = o.IdTcentralLLamada,
                                  DuracionTimbrado = o.DuracionTimbrado,
                                  DuracionContesto = o.DuracionContesto,
                                  EstadoLlamada = o.EstadoLlamada,
                                  FechaLlamada = o.FechaLlamada,
                                  FechaLlamadaFin = o.FechaLlamadaFin,
                                  SubEstadoLlamada = o.SubEstadoLlamada,
                                  NombreGrabacion = o.NombreGrabacionIntegra,

                              }).OrderByDescending(o => o.FechaLlamada).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                              llamadasTresCX = g.Select(o => new CompuestoActividadesEjecutadasTemp_DetalleDTO
                              {
                                  Id = o.IdTresCX,
                                  DuracionContesto = o.TiempoContestoTresCx.ToString(),
                                  DuracionTimbrado = o.TiempoTimbradoTresCx.ToString(),
                                  EstadoLlamada = o.EstadoLlamadaTresCX,
                                  FechaLlamada = o.FechaIncioLlamadaTresCX,
                                  FechaLlamadaFin = o.FechaFinLlamadaTresCX,
                                  SubEstadoLlamada = o.SubEstadoLlamadaTresCX,
                                  NombreGrabacion = o.NombreGrabacionTresCX,

                              }).OrderBy(o => o.FechaLlamada).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList()
                          }).OrderBy(x => x.FechaReal);

            List<CompuestoActividadesEjecutadasTempDTO> final = new List<CompuestoActividadesEjecutadasTempDTO>();
            var flag = false;
            var count = 0;
            double minutos = 0;
            double totalContesto = 0;
            double totalTimbrado = 0;
            double totalPerdido = 0;
            double mayorPerdido = 0;
            DateTime fechaTemp = new DateTime();
            DateTime fechaActual = DateTime.Now;
            foreach (var item in result)
            {
                CompuestoActividadesEjecutadasTempDTO itemDetalle = new CompuestoActividadesEjecutadasTempDTO()
                {
                    Id = item.Id,
                    CentroCosto = item.CentroCosto,
                    Contacto = item.Contacto,
                    CodigoFase = item.CodigoFase,
                    NombreTipoDato = item.NombreTipoDato,
                    Origen = item.Origen,
                    FechaProgramada = item.FechaProgramada,
                    FechaReal = item.FechaReal,
                    Duracion = item.Duracion,
                    Actividad = item.Actividad,
                    Ocurrencia = item.Ocurrencia,
                    Comentario = item.Comentario,
                    Asesor = item.Asesor,
                    IdContacto = item.IdContacto,
                    IdOportunidad = item.IdOportunidad,
                    ProbActual = item.ProbActual,
                    Ca_nombre = item.Ca_nombre,
                    IdCategoria = item.IdCategoria,
                    FaseInicial = item.FaseInicial,
                    FaseMaxima = item.FaseMaxima,
                    TotalOportunidades = item.TotalOportunidades,
                    UnicoTimbrado = item.UnicoTimbrado,
                    UnicoContesto = item.UnicoContesto,
                    UnicoEstadoLlamada = item.UnicoEstadoLlamada,
                    Estado = item.Estado,
                    NombreGrupo = item.NombreGrupo
                };

                if (item.lista != null && item.lista.Select(s => s.DuracionTimbrado).FirstOrDefault() != null)
                {
                    var ordenLlamadas = item.lista.OrderBy(x => x.FechaLlamada).ToList();
                    var fechaUltima = ordenLlamadas.Select(s => s.FechaLlamada).FirstOrDefault();
                    if (count > 0 && flag)
                    {
                        if (diaFecha == fechaTemp.Day)
                        {
                            var min = ((fechaUltima.Value - fechaTemp).TotalSeconds / 60).ToString("0.0");
                            minutos = Convert.ToDouble(min);
                        }
                        else
                        {
                            minutos = 0;
                        }
                    }
                    if (fechaUltima != null)
                    {
                        flag = true;
                        fechaTemp = item.lista.Select(x => x.FechaLlamada).FirstOrDefault().Value;
                        double contesto = Convert.ToDouble(item.lista.Select(x => x.DuracionContesto).FirstOrDefault());
                        double timbrado = Convert.ToDouble(item.lista.Select(x => x.DuracionTimbrado).FirstOrDefault());
                        fechaTemp = fechaTemp.AddSeconds(contesto + timbrado);
                    }
                    totalTimbrado += (item.lista.Select(s => Convert.ToDouble(s.DuracionTimbrado))).Sum();
                    totalContesto += (item.lista.Select(s => Convert.ToDouble(s.DuracionContesto))).Sum();
                    if (minutos >= 0)
                    {
                        totalPerdido += minutos;
                    }
                    itemDetalle.NumeroLlamadas = item.lista.Count().ToString();
                    item.lista = item.lista.OrderBy(x => x.FechaLlamada).ToList();

                    itemDetalle.DuracionTimbrado = String.Concat(item.lista.Select(o => " <strong >TT:</strong > / " + o.DuracionTimbrado + " <strong >TC:</strong > " + o.DuracionContesto + "<br />"));
                    itemDetalle.EstadoLlamada = String.Concat(item.lista.Select(o => " <strong >Tipo: " + o.EstadoLlamada + "</strong><br>SubTipo: " + o.SubEstadoLlamada + "<br />"));
                    var existeFechaNull = item.lista.Select(x => x.FechaLlamadaFin == null).ToList();
                    if (existeFechaNull.Count() > 0)
                    {
                        itemDetalle.FechaLlamada = String.Concat(item.lista.Select(o => "<strong >I: </strong >" + o.FechaLlamada.Value.ToString("HH:mm:ss") + "<strong> T: </strong >" + o.FechaLlamadaFin.Value.ToString("HH:mm:ss") + "<br />"));
                    }
                    else
                    {
                        string html = "";
                        foreach (var llamada in item.lista)
                        {
                            if (llamada.FechaLlamadaFin != null)
                                html = html + "<strong >I: </strong >" + llamada.FechaLlamada.Value.ToString("HH:mm:ss") + "<strong> T: </strong >" + llamada.FechaLlamadaFin.Value.ToString("HH:mm:ss") + "<br />";
                            else
                                html = html + "<strong >I: </strong >" + llamada.FechaLlamada.Value.ToString("HH:mm:ss") + "<strong> T: -</strong ><br />";
                        }
                        itemDetalle.FechaLlamada = html;
                    }

                }
                else
                {
                    var fechaUltima = item.UnicoFechaLlamada;
                    if (count > 0 && flag)
                    {
                        if (diaFecha == fechaTemp.Day && fechaUltima != null)
                        {
                            var min = ((fechaUltima.Value - fechaTemp).TotalSeconds / 60).ToString("0.0");
                            minutos = Convert.ToDouble(min);
                        }
                        else
                        {
                            minutos = 0;
                        }
                    }
                    if (fechaUltima != null)
                    {
                        flag = true;
                        fechaTemp = fechaUltima.Value;
                        double contesto = Convert.ToDouble(item.UnicoContesto);
                        double timbrado = Convert.ToDouble(item.UnicoTimbrado);
                        fechaTemp = fechaTemp.AddSeconds(contesto + timbrado);
                    }
                    totalTimbrado += Convert.ToDouble(item.UnicoTimbrado);
                    totalContesto += Convert.ToDouble(item.UnicoContesto);
                    if (minutos >= 0)
                    {
                        totalPerdido += minutos;
                    }
                    string date = item.UnicoFechaLlamada == null ? "" : item.UnicoFechaLlamada.Value.ToString("yyyy/MM/dd HH:mm");
                    itemDetalle.NumeroLlamadas = "1";
                    //itemDetalle.DuracionTimbrado = String.Concat(item.lista.Select(o => " <strong >TT:</strong > / " + o.DuracionTimbrado + " <strong >TC:</strong > " + o.DuracionContesto + " <strong ><br />"));
                    //itemDetalle.EstadoLlamada = String.Concat(item.lista.Select(o => " <strong >Tipo: " + o.EstadoLlamada + "</strong><br>SubTipo: " + o.SubEstadoLlamada) + "<br />");
                    //itemDetalle.FechaLlamada = String.Concat(item.lista.Select(o => " <strong >I: </strong >" + o.FechaLlamada.Value.ToString("yyyy/MM/dd HH:mm") + "<strong> T: </strong >" + o.FechaLlamadaFin.Value.ToString("yyyy/MM/dd HH:mm") + "<br />"));

                    itemDetalle.DuracionTimbrado = item.UnicoEstadoLlamada + " <strong >- TT:</strong >" + item.UnicoTimbrado + "  <strong >TC:</strong >" + item.UnicoContesto + " <strong >-</strong > " + date + "<br /><strong id='estadoNuevoT'>Nuevo Estado: </strong ><strong id='estadoNuevoC'>" + item.UnicoClasificacion + "</strong><br />";
                }
                itemDetalle.MinutosIntervale = minutos;
                itemDetalle.MinutosTotalContesto = totalContesto;
                itemDetalle.MinutosTotalTimbrado = totalTimbrado;
                itemDetalle.MinutosTotalPerdido = totalPerdido;
                count++;

                mayorPerdido = mayorPerdido > minutos ? mayorPerdido : minutos;
                itemDetalle.MayorTiempo = mayorPerdido;

                itemDetalle.TiemposTresCX = String.Concat(item.llamadasTresCX.Select(o => " <strong >TT:</strong > / " + o.DuracionTimbrado + " <strong >TC:</strong > " + o.DuracionContesto + "<br />"));
                itemDetalle.EstadosTresCX = String.Concat(item.llamadasTresCX.Select(o => " <strong >Tipo: " + o.EstadoLlamada + "</strong><br>SubTipo: " + o.SubEstadoLlamada + "<br />"));
                var listaActividades = _repReportes.ReporteActividadOcurrencia(item.IdOportunidad);
                itemDetalle.TotalEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaEjecutado && x.IdFaseActual == item.IdFaseOportunidadInicial && x.FechaReal < item.FechaModificacion.Value).Count();
                itemDetalle.TotalNoEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaNoEjecutado && x.IdFaseActual == item.IdFaseOportunidadInicial && x.FechaReal < item.FechaModificacion.Value).Count();
                itemDetalle.TotalAsignacionManual = listaActividades.Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaAsignacionManual && x.IdFaseActual == item.IdFaseOportunidadInicial && x.FechaReal < item.FechaModificacion.Value).Count();

                var html1 = "";
                var html2 = "";
                foreach (var llamada in item.llamadasTresCX)
                {
                    if (llamada.NombreGrabacion != null)
                    {
                        html1 = html1 + "<a class='ex1' style='cursor: pointer;' onclick=\"return reproducirLlamada3CX('" + llamada.NombreGrabacion + "')\"><b>Escuchar</b></a>";
                    }
                    html1 = html1 + "</br>";

                }
                foreach (var llamada in item.lista)
                {
                    if (llamada.NombreGrabacion != null)
                    {
                        html2 = html2 + "<a class='ex1' style='cursor: pointer;' onclick=\"return reproducirLlamada('" + llamada.NombreGrabacion + "')\"><b>Escuchar</b></a>";
                    }
                    html2 = html2 + "</br>";

                }
                itemDetalle.NombreGrabacionTresCX = html1;
                itemDetalle.NombreGrabacionIntegra = html2;
                //itemDetalle.NombreGrabacionTresCX = String.Concat(item.llamadasTresCX.Select(o => "<a class='ex1' style='cursor: pointer;' onclick=\"return reproducirLlamada3CX('" + o.NombreGrabacion + "')\"><b>Escuchar</b></a><br/>"));
                //itemDetalle.NombreGrabacionIntegra = String.Concat(item.lista.Select(o => "<a class='ex1' style='cursor: pointer;' onclick=\"return reproducirLlamada('" + o.NombreGrabacion + "')\"><b>Escuchar</b></a><br/>"));

                final.Add(itemDetalle);
            }
            var data = final.OrderByDescending(s => s.FechaReal).ToList();

            var total = 0;
            if (data.Count != 0)
            {
                total = data.FirstOrDefault().TotalOportunidades;
            }

            return Ok(new { Records = data, Total = total });
        }

        /// TipoFuncion: GET
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Activdades Realizadas o Actividades para programar del Asesor 
        /// para un determinado Tab
        /// </summary>
        /// <returns> ObjetoDTO: List<ActividadAgendaDTO> </returns>
        [Route("[action]/{IdAsesor}/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpGet]
        public ActionResult ObtenerActividadSeleccionadaAsesor(int IdAsesor, int IdTab, string CodigoAreaTrabajo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdAsesor = IdAsesor,
                    IdTab = IdTab,
                };
                agenda.CargarActividadesSeleccionadaPorAsesor();
                return Ok(agenda.ActividadesAgenda);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab, con los filtros realizados
        /// </summary>
        /// <returns> ObjetoBO: AgendaBO </returns>
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesor(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                //llamar a la base de datos
                //if (IdTab == 11)
                //{
                //    return Ok(agenda.ActividadesRealizadas);
                //}
                //else
                //{
                return Ok(agenda.ActividadesAgenda);
                //}
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin - Fischer Valdez
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la actividad filtrada por el asesor y seguimiento academico
        /// </summary>
        /// <param name="IdTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="Filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorRN2(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["RN2"] = agenda.ActividadesAgenda["RN2"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["RN2"], Total = agenda.CantidadRN2 });//"take":20,"skip":5,"page":1,"pageSize":5,
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin - Fischer Valdez
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la actividad filtrada por el asesor y cuota de atraso
        /// </summary>
        /// <param name="IdTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="Filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorCuotaAtraso(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["Cuota Atraso"] = agenda.ActividadesAgenda["Cuota Atraso"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["Cuota Atraso"], Total = agenda.CantidadRN2 });//"take":20,"skip":5,"page":1,"pageSize":5,
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin - Fischer Valdez - Jashin Salazar.
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Mas de una cuota de atraso, con los filtros realizados
        /// </summary>
        /// <param name="IdTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="Filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>

        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorMasDeUnaCuotaAtraso(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["1+ Cuota Atraso"] = agenda.ActividadesAgenda["1+ Cuota Atraso"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["1+ Cuota Atraso"], Total = agenda.CantidadRN2 });//"take":20,"skip":5,"page":1,"pageSize":5,
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin - Fischer Valdez - Jashin Salazar.
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Cuota al dia, con los filtros realizados
        /// </summary>
        /// <param name="IdTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="Filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorCuotaAlDia(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["Cuota AlDia"] = agenda.ActividadesAgenda["Cuota AlDia"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["Cuota AlDia"], Total = agenda.CantidadRN2 });//"take":20,"skip":5,"page":1,"pageSize":5,
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin - Fischer Valdez - Jashin Salazar.
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Seguimiento academico, con los filtros realizados
        /// </summary>
        /// <param name="IdTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="Filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorSeguimientoAcademico(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["Seguimiento Academico"] = agenda.ActividadesAgenda["Seguimiento Academico"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["Seguimiento Academico"], Total = agenda.CantidadRN2 });//"take":20,"skip":5,"page":1,"pageSize":5,
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin - Fischer Valdez - Jashin Salazar.
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Profesores Programados, con los filtros realizados
        /// </summary>
        /// <param name="IdTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="Filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorProfesoresProgramados(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["Profesores Programados"] = agenda.ActividadesAgenda["Profesores Programados"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["Profesores Programados"], Total = agenda.CantidadRN2 });//"take":20,"skip":5,"page":1,"pageSize":5,
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin - Fischer Valdez - Jashin Salazar.
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Profesores No programados, con los filtros realizados
        /// </summary>
        /// <param name="IdTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="Filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorProfesoresNoProgramados(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["Profesores No Programados"] = agenda.ActividadesAgenda["Profesores No Programados"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["Profesores No Programados"], Total = agenda.CantidadRN2 });//"take":20,"skip":5,"page":1,"pageSize":5,
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin - Fischer Valdez - Jashin Salazar.
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Programadas Manuales, con los filtros realizados
        /// </summary>
        /// <param name="IdTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="Filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorProgramacionManual(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["Programacion Manual"] = agenda.ActividadesAgenda["Programacion Manual"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["Programacion Manual"], Total = agenda.CantidadRN2 });//"take":20,"skip":5,"page":1,"pageSize":5,
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin - Fischer Valdez - Jashin Salazar.
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Culminados, con los filtros realizados
        /// </summary>
        /// <param name="IdTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="Filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorCulminado(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["Culminado"] = agenda.ActividadesAgenda["Culminado"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["Culminado"], Total = agenda.CantidadRN2 });//"take":20,"skip":5,"page":1,"pageSize":5,
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin - Fischer Valdez - Jashin Salazar.
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Culminados deudor, con los filtros realizados
        /// </summary>
        /// <param name="IdTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="Filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorCulminadoDeudor(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["Culminado Deudor"] = agenda.ActividadesAgenda["Culminado Deudor"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["Culminado Deudor"], Total = agenda.CantidadRN2 });//"take":20,"skip":5,"page":1,"pageSize":5,
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Certificado, con los filtros realizados
        /// </summary>
        /// <returns> ObjetoBO: AgendaBO </returns>
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorCertificado(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["Certificado"] = agenda.ActividadesAgenda["Certificado"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["Certificado"], Total = agenda.CantidadRN2 });//"take":20,"skip":5,"page":1,"pageSize":5,
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin - Fischer Valdez - Jashin Salazar.
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Reservado con deuda, con los filtros realizados
        /// </summary>
        /// <param name="IdTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="Filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorReservadoConDeuda(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["Reservado Con Deuda"] = agenda.ActividadesAgenda["Reservado Con Deuda"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["Reservado Con Deuda"], Total = agenda.CantidadRN2 });//"take":20,"skip":5,"page":1,"pageSize":5,
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin - Fischer Valdez - Jashin Salazar.
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Reservado sin Deuda, con los filtros realizados
        /// </summary>
        /// <param name="IdTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="Filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorReservadoSinDeuda(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["Reservado Sin Deuda"] = agenda.ActividadesAgenda["Reservado Sin Deuda"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["Reservado Sin Deuda"], Total = agenda.CantidadRN2 });//"take":20,"skip":5,"page":1,"pageSize":5,
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin - Fischer Valdez -Jashin Salazar.
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Retirado, con los filtros realizados
        /// </summary>
        /// <param name="IdTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="Filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>

        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorRetirado(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["Retirado"] = agenda.ActividadesAgenda["Retirado"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["Retirado"], Total = agenda.CantidadRN2 });//"take":20,"skip":5,"page":1,"pageSize":5,
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin - Fischer Valdez
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la actividad filtrada por el asesor y acceso temporal
        /// </summary>
        /// <param name="IdTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="Filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>

        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorAccesoTemporal(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["Acceso Temporal"] = agenda.ActividadesAgenda["Acceso Temporal"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                // Llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["Acceso Temporal"], Total = agenda.CantidadRN2 });//"take":20,"skip":5,"page":1,"pageSize":5,
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jorge Rivera - Carlos Crispin - Fischer Valdez - Gian Miranda - Jashin Salazar.
        /// Fecha: 19/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Abandonado, con los filtros realizados
        /// </summary>
        /// <param name="IdTab">Id del tab de la agenda (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Codigo en cadena del area de trabajo</param>
        /// <param name="Filtros">Objeto de tipo (Dictionary(string, string)>)</param>
        /// <returns>Response 200 (Objeto anonimo con los registros y el total) o 400 con mensaje de error</returns>
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorAbandonado(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["Abandonado"] = agenda.ActividadesAgenda["Abandonado"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["Abandonado"], Total = agenda.CantidadRN2 });/*"take":20,"skip":5,"page":1,"pageSize":5,*/
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 20/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los pespecificos para los accesos temporales
        /// </summary>
        /// <returns>Response 200 (Objeto anonimo con los registros para los combos) o 400 con mensaje de error</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerPEspecificoAccesoTemporalCombo()
        {
            try
            {
                var resultado = _repPEspecifico.ObtenerPEspecificoNuevoAulaVirtualTipo();

                var programasAsignados = resultado.GroupBy(x => new { x.IdPEspecifico, x.NombrePEspecifico, x.IdCentroCosto, x.EstadoP, x.Modalidad, x.IdPGeneral, x.Ciudad, x.IdCursoMoodle, x.IdCursoMoodlePrueba, x.TipoPEspecifico })
                                                    .Select(s => new { s.Key.IdPEspecifico, s.Key.NombrePEspecifico, s.Key.IdCentroCosto, s.Key.EstadoP, s.Key.Modalidad, s.Key.IdPGeneral, s.Key.Ciudad, s.Key.IdCursoMoodle, s.Key.IdCursoMoodlePrueba, s.Key.TipoPEspecifico })
                                                    .ToList();
                var cursosAsignados = resultado.Select(s => new { IdPEspecificoPadre = s.IdPEspecifico, IdPEspecifico = s.IdPEspecificoHijo, NombrePEspecifico = s.NombrePEspecificoHijo }).ToList();

                return Ok(new { ProgramasAsignados = programasAsignados, CursosAsignados = cursosAsignados });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin - Fischer Valdez - Jashin Salazar.
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Por Abandonar con los filtros realizados
        /// </summary>
        /// <param name="IdTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="Filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorPorAbandonar(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["PorAbandonar"] = agenda.ActividadesAgenda["PorAbandonar"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["PorAbandonar"], Total = agenda.CantidadRN2 });//"take":20,"skip":5,"page":1,"pageSize":5,
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin - Fischer Valdez - Jashin Salazar.
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Por Contactar, con los filtros realizados
        /// </summary>
        /// <param name="IdTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="Filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorPorContactar(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["Por Contactar"] = agenda.ActividadesAgenda["Por Contactar"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["Por Contactar"], Total = agenda.CantidadRN2 });//"take":20,"skip":5,"page":1,"pageSize":5,
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin - Fischer Valdez - Jashin Salazar.
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab En Negociacion, con los filtros realizados
        /// </summary>
        /// <param name="IdTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="Filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorEnNegociacion(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["En Negociacion"] = agenda.ActividadesAgenda["En Negociacion"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["En Negociacion"], Total = agenda.CantidadRN2 });//"take":20,"skip":5,"page":1,"pageSize":5,
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin - Fischer Valdez - Jashin Salazar.
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab En Cierre De Negociacion, con los filtros realizados
        /// </summary>
        /// <param name="IdTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="Filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorEnCierreNegociacion(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["En Cierre De Negociacion"] = agenda.ActividadesAgenda["En Cierre De Negociacion"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["En Cierre De Negociacion"], Total = agenda.CantidadRN2 });//"take":20,"skip":5,"page":1,"pageSize":5,
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin - Fischer Valdez - Jashin Salazar.
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab BIC, con los filtros realizados
        /// </summary>
        /// <param name="IdTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="Filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorBic(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["Bic"] = agenda.ActividadesAgenda["Bic"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["Bic"], Total = agenda.CantidadRN2 });//"take":20,"skip":5,"page":1,"pageSize":5,
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin - Fischer Valdez - Jashin Salazar.
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Evaluacion, con los filtros realizados
        /// </summary>
        /// <param name="IdTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="Filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorEvaluacion(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["Evaluacion"] = agenda.ActividadesAgenda["Evaluacion"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["Evaluacion"], Total = agenda.CantidadRN2 });//"take":20,"skip":5,"page":1,"pageSize":5,
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin - Fischer Valdez - Jashin Salazar.
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Reasignado, con los filtros realizados
        /// </summary>
        /// <param name="IdTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="Filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorReasignado(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["Reasignado"] = agenda.ActividadesAgenda["Reasignado"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["Reasignado"], Total = agenda.CantidadRN2 });//"take":20,"skip":5,"page":1,"pageSize":5,
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin - Fischer Valdez - Jashin Salazar.
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Solicitud Cambio, con los filtros realizados
        /// </summary>
        /// <param name="IdTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="Filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorSolicitudCambio(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["Solicitud Cambio"] = agenda.ActividadesAgenda["Solicitud Cambio"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["Solicitud Cambio"], Total = agenda.CantidadRN2 });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Villena
        /// Fecha: 18/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Pre Reporte CR, con los filtros realizados
        /// </summary>
        /// <param name="IdTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="Filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorPreReporteCR(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["Pre Reporte CR"] = agenda.ActividadesAgenda["Pre Reporte CR"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["Pre Reporte CR"], Total = agenda.CantidadRN2 });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Villena
        /// Fecha: 18/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab Reportado CR, con los filtros realizados
        /// </summary>
        /// <param name="IdTab">Id del tab (PK de la tabla com.T_AgendaTab)</param>
        /// <param name="CodigoAreaTrabajo">Cadena con la abreviatura del codigo de area de trabajo</param>
        /// <param name="Filtros">Diccionario (string, string)</param>
        /// <returns>Response 200 (Objeto anonimo Diccionario con la lista de actividades y la cantidad de RN2)</returns>
        [Route("[action]/{IdTab}/{CodigoAreaTrabajo}")]
        [HttpPost]
        public ActionResult ObtenerActividadFiltradaPorAsesorReportadoCR(int IdTab, string CodigoAreaTrabajo, [FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaBO agenda = new AgendaBO(CodigoAreaTrabajo)
                {
                    IdTab = IdTab,
                    Filtros = Filtros
                };
                agenda.CargarActividadSeleccionadaPorFiltro();
                agenda.ActividadesAgenda["Reportado CR"] = agenda.ActividadesAgenda["Reportado CR"].OrderBy(x => x.UltimaFechaProgramada).ToList();
                //llamar a la base de datos
                return Ok(new { Records = agenda.ActividadesAgenda["Reportado CR"], Total = agenda.CantidadRN2 });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Ranking de Asesores
        /// </summary>
        /// <returns> ObjetoBO: RankingIngresoBO </returns>
        [Route("[action]/{IdAsesor}")]
        [HttpGet]
        public IActionResult ObtenerRankingV2(int IdAsesor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RankingIngresoBO rankingIngresoBO = new RankingIngresoBO();
                rankingIngresoBO.IdPersonal = IdAsesor;
                if (!rankingIngresoBO.HasErrors)
                {
                    RankingIngresoRepositorio _rankingIngresoRepositorio = new RankingIngresoRepositorio();
                    return Ok(_rankingIngresoRepositorio.GetRanking(rankingIngresoBO));
                }
                else
                    return BadRequest(rankingIngresoBO.GetErrors(null));
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Carlos Crispin - Fischer Valdez - Jashin Salazar.
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la plantilla dependiendo de la fase oportunidad
        /// </summary>
        /// <param name="IdFaseOportunidad">Id de la fase de oportunidad (PK de la tabla pla.T_FaseOportunidad)</param>
        /// <returns>Response 200 (Lista de objetos de clase ContenidoPlantillaDTO), response 400</returns>
        [Route("[Action]/{IdFaseOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerPlantilla(int IdFaseOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (IdFaseOportunidad <= 0)
            {
                return BadRequest("El Id de la Fase no Existe");
            }
            try
            {
                PlantillaClaveValorRepositorio _repPlantillaClaveValor = new PlantillaClaveValorRepositorio();

                return Ok(_repPlantillaClaveValor.ObtenerPlantillas(IdFaseOportunidad));
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// Tipo Función: GET
        /// Autor: Carlos Crispin - Fischer Valdez - Jashin Salazar.
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la toda slas plantillas
        /// </summary>
        /// <returns>Response 200 (Lista de objetos de clase ContenidoPlantillaDTO), response 400</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTodoPlantilla()
        {
            try
            {
                PlantillaRepositorio _repPlantilla = new PlantillaRepositorio();

                PlantillaClaveValorRepositorio _repPlantillaClaveValor = new PlantillaClaveValorRepositorio();

                return Ok(new { data = _repPlantillaClaveValor.ObtenerTodoPlantillasMailing() });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 10/01/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera plantilla Sms para mostrar
        /// </summary>
        /// <returns>Response 200 (Objeto anonimo con mensaje, cantidad de caracteres y mensajes), response 400</returns>
        [Route("[Action]/{IdOportunidad}/{IdCentroCosto}/{IdPlantilla}/{Usuario}")]
        [HttpGet]
        public ActionResult GenerarPlantillaSmsCentroCostoUsuario(int IdOportunidad, int IdCentroCosto, int IdPlantilla, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string mensajeFinal = string.Empty;
                var listaSubMensajeFinal = new List<string>();

                var _repSmsConfiguracionEnvio = new SmsConfiguracionEnvioRepositorio();
                var configuracionEstablecida = _repSmsConfiguracionEnvio.ConfiguracionSmsOportunidad(IdOportunidad);

                if (configuracionEstablecida == null) return Ok(false);

                string urlBase = $"http://{configuracionEstablecida.Servidor}:80/sendsms?username=smsuser&password=smspwd&phonenumber=";

                #region Cambio de etiqueta
                var reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                {
                    IdOportunidad = IdOportunidad,
                    IdPlantilla = IdPlantilla
                };

                reemplazoEtiquetaPlantilla.ReemplazarEtiquetasNuevasOportunidades(idCentroCosto: IdCentroCosto);
                #endregion

                string[] palabras = reemplazoEtiquetaPlantilla.SmsReemplazado.Cuerpo.Split(' ');

                foreach (string palabra in palabras)
                {
                    if ((mensajeFinal + " " + palabra).Length <= 128)
                    {
                        mensajeFinal += " " + palabra;
                    }
                    else
                    {
                        listaSubMensajeFinal.Add(mensajeFinal.Trim());
                        mensajeFinal = palabra;
                    }
                }

                listaSubMensajeFinal.Add(mensajeFinal.Trim());
                mensajeFinal = string.Empty;

                return Ok(new { MensajeReemplazado = reemplazoEtiquetaPlantilla.SmsReemplazado.Cuerpo, CantidadMensaje = listaSubMensajeFinal.Count, CantidadCaracter = reemplazoEtiquetaPlantilla.SmsReemplazado.Cuerpo.Length });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 10/01/2022
        /// Versión: 1.0
        /// <summary>
        /// Envia plantilla Sms con los parametros de usuario y oportunidad
        /// </summary>
        /// <returns>Response 200 (Objeto anonimo con mensaje, cantidad de caracteres y mensajes), response 400</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult EnviarTextoSmsPorOportunidadUsuario([FromBody] ContenidoSmsUsuarioDTO ContenidoMensaje)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                int seccionMensaje = 1;
                string mensajeFinal = string.Empty;
                List<string> listaSubMensajeFinal = new List<string>();

                /*Buscar configuracion para el envio de SMS individual*/
                var _repSmsConfiguracionEnvio = new SmsConfiguracionEnvioRepositorio();
                var configuracionEstablecida = _repSmsConfiguracionEnvio.ConfiguracionSmsOportunidad(ContenidoMensaje.IdOportunidad);

                if (configuracionEstablecida == null) return Ok(false);

                string urlBase = $"http://{configuracionEstablecida.Servidor}:80/sendsms?username=smsuser&password=smspwd&phonenumber=";

                string[] palabras = ContenidoMensaje.Cuerpo.Split(' ');

                foreach (string palabra in palabras)
                {
                    if ((mensajeFinal + " " + palabra).Length <= 128)
                    {
                        mensajeFinal += " " + palabra;
                    }
                    else
                    {
                        listaSubMensajeFinal.Add(mensajeFinal.Trim());
                        mensajeFinal = palabra;
                    }
                }

                listaSubMensajeFinal.Add(mensajeFinal.Trim());
                mensajeFinal = string.Empty;

                foreach (string mensajeAEnviar in listaSubMensajeFinal)
                {
                    string url = $"{urlBase}{configuracionEstablecida.Celular}&message={mensajeAEnviar.Replace(" ", "%20")}&[port={configuracionEstablecida.Tipo}-{configuracionEstablecida.Puerto}&][report=String&][timeout=5&][id=1]";

                    using (WebClient wc = new WebClient())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        wc.DownloadString(url);
                    }

                    _repAlumno.InsertaSMSOportunidadUsuario(configuracionEstablecida.Celular, configuracionEstablecida.IdPersonal, configuracionEstablecida.IdAlumno, mensajeAEnviar, seccionMensaje, configuracionEstablecida.IdCodigoPais.GetValueOrDefault(), ContenidoMensaje.Usuario);

                    seccionMensaje++;
                }

                var insertado = _repAlumno.InsertaSMSOportunidad(ContenidoMensaje.IdOportunidad, DateTime.Now);

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Carlos Crispin - Fischer Valdez - Jashin Salazar
        /// Fecha: 22/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera la plantilla de centros de costos de WhatsApp
        /// </summary>
        /// <param name="IdCentroCosto">Id del centro de costo (PK de la tabla pla.T_CentroCosto)</param>
        /// <param name="IdPlantilla">Id de la plantilla (PK de la tabla mkt.T_Plantilla)</param>
        /// <param name="IdPersonal">Id del personal (PK de la tabla gp.T_Personal)</param>
        /// <param name="Numero">Cadena con el numero de WhatsApp</param>
        /// <returns>Response 200 (Cadena con la plantilla), response 400</returns>
        [Route("[Action]/{IdCentroCosto}/{IdPlantilla}/{IdPersonal}/{Numero}")]
        [HttpGet]
        public ActionResult GenerarPlantillaCentroCostoWhatsapp(int IdCentroCosto, int IdPlantilla, int IdPersonal, string Numero)
        {

            var _repCentroCosto = new CentroCostoRepositorio();
            var _repPespecifico = new PespecificoRepositorio();
            var _repAlumno = new AlumnoRepositorio();
            var _repPersonal = new PersonalRepositorio();
            var _repOportunidad = new OportunidadRepositorio();
            var _repPgeneral = new PgeneralRepositorio();
            var fecha = new List<ModalidadProgramaDTO>();

            var _repPlantillaClaveValor = new PlantillaClaveValorRepositorio();

            string plantilla = string.Empty;
            string valor = string.Empty;
            string numeroAlterno = string.Empty;
            try
            {
                if (Numero.StartsWith("51"))
                {
                    Numero = Numero.Substring(2, 9);
                    numeroAlterno = Numero;
                }
                else if (Numero.StartsWith("57"))
                {
                    numeroAlterno = Numero.Substring(2);
                }
                else if (Numero.StartsWith("591"))
                {
                    numeroAlterno = Numero.Substring(3);
                }

                var Alumno = _repAlumno.FirstBy(w => w.Celular.Contains(Numero) || w.Celular.Contains(numeroAlterno), y => new { y.Id, y.Nombre1, y.Nombre2, y.ApellidoMaterno, y.ApellidoPaterno, y.Email1 });
                var Asesor = _repPersonal.ObtenerDatoPersonal(IdPersonal);
                //var Asesor = _repPersonal.FirstBy(w => w.Id == IdPersonal, y => new { y.Nombres, y.Apellidos, y.Anexo3Cx, y.Central, y.MovilReferencia });
                if (IdCentroCosto == 0 || IdCentroCosto == null)
                {
                    IdCentroCosto = _repOportunidad.ObtenerCentroCostoPorCelularAlumno(Numero, IdPersonal);
                }
                plantilla = _repPlantillaClaveValor.GetBy(w => w.Estado == true && w.IdPlantilla == IdPlantilla && w.Clave == "Texto", w => new { w.Valor }).FirstOrDefault().Valor;
                List<datosPlantillaWhatsApp> objetoplantilla = new List<datosPlantillaWhatsApp>();
                var rpta = _repCentroCosto.ObtenerCentroCostoParaPlantillaWhatsApp(IdCentroCosto);

                //if (plantilla.Contains("{T_Pespecifico.NombreMesFechaInicioPrograma}") || plantilla.Contains("{T_Pespecifico.DiaFechaInicioPrograma}") || plantilla.Contains("{T_Pespecifico.NombreMesFechaInicioPrograma}"))
                //{
                //    fecha = _repPgeneral.ObtenerFechaInicioProgramaGeneral(AlumnoEtiqueta.IdPgeneral ?? default(int), AlumnoEtiqueta.IdCodigoPais);

                //    if (fecha.Count > 0)
                //    {
                //        FechaInicioPrograma = fecha.Where(w => w.Tipo.ToUpper().Contains("PRESENCIAL")).OrderBy(w => w.FechaReal).FirstOrDefault();
                //        if (FechaInicioPrograma == null)
                //        {
                //            FechaInicioPrograma = fecha.Where(w => w.Tipo.ToUpper().Contains("ONLINE SINCRONICA")).OrderBy(w => w.FechaReal).FirstOrDefault();
                //        }
                //    }
                //}
                foreach (string word in plantilla.Split('{'))
                {
                    datosPlantillaWhatsApp plantillaEtiqueValor = new datosPlantillaWhatsApp();
                    if (word.Contains('}'))
                    {
                        string etiqueta = word.Split('}')[0];
                        //Separamos solo los Id´s

                        if (etiqueta.Contains("tPartner.nombre"))
                        {
                            if (IdCentroCosto == 0)
                            {
                                return Ok(null);
                            }
                            valor = rpta.NombrePartner;
                        }
                        else if (etiqueta.Contains("tPEspecifico.nombre"))
                        {
                            if (IdCentroCosto == 0)
                            {
                                return Ok(null);
                            }
                            valor = rpta.NombrePEspecifico;
                        }
                        else if (etiqueta.Contains("tPLA_PGeneral.Nombre"))
                        {
                            if (IdCentroCosto == 0)
                            {
                                return Ok(null);
                            }
                            valor = rpta.NombrePgeneral;
                        }

                        if (etiqueta.Contains("Template"))
                        {
                            List<string> ListaPalabras = new List<string>();
                            char[] delimitador = new char[] { '.' };
                            string idPlantilla = "";
                            string IdColumna = "";
                            string[] array = etiqueta.Split(delimitador, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string s in array)
                            {
                                ListaPalabras.Add(s);
                            }
                            idPlantilla = ListaPalabras[3].ToString();
                            IdColumna = ListaPalabras[4].ToString();
                            string Etiquetatemp = IdPlantilla + "." + IdColumna;
                            var result = _repPespecifico.ObtenerSeccionEtiquetaAgendaMensaje(IdColumna, idPlantilla, IdCentroCosto);
                            if (result != null)
                                valor = result.Valor;
                            else
                                valor = null;
                        }
                        else
                        {

                            if ((etiqueta == "tPersonal.nombres" || etiqueta == "tPersonal.Nombre1" || etiqueta == "tPersonal.apellidos" || etiqueta == "tPersonal.UrlFirmaCorreos" || etiqueta == "tPersonal.Telefono" || etiqueta == "tAlumnos.apepaterno" || etiqueta == "tAlumnos.apematerno" || etiqueta == "tAlumnos.nombre1" || etiqueta == "tAlumnos.nombre2" || etiqueta == "tAlumnos.email1" || etiqueta == "tPersonal.PrimerNombreApellidoPaterno" || etiqueta == "tPersonal.email" || etiqueta == "T_Alumno.NombrePGeneralUltimoEnvioMasivo" || etiqueta == "T_Alumno.NombrePGeneralUltimaSolicitudInformacion") && IdPersonal > 0)
                            {
                                switch (etiqueta)
                                {
                                    case "tPersonal.PrimerNombreApellidoPaterno":
                                        valor = Asesor.PrimerNombreApellidoPaterno; break;
                                    case "tPersonal.email":
                                        valor = Asesor.Email; break;
                                    case "tPersonal.Nombre1":
                                        valor = Asesor.Nombre1; break;
                                    case "tPersonal.nombres":
                                        valor = Asesor.Nombres; break;
                                    case "tPersonal.apellidos":
                                        valor = Asesor.Apellidos; break;
                                    case "tPersonal.Telefono":
                                        {
                                            if (!string.IsNullOrEmpty(Asesor.MovilReferencia))
                                            {
                                                valor = Asesor.MovilReferencia;
                                            }
                                            else
                                            {
                                                if (Asesor.Central == "192.168.0.20")
                                                {
                                                    //aqp
                                                    valor = "(51) 54 258787 - Anexo " + Asesor.Anexo3Cx;
                                                }
                                                else
                                                {
                                                    if (Asesor.Central == "192.168.2.20")
                                                    {
                                                        //lima
                                                        valor = "(51) 1 207 2770 - Anexo " + Asesor.Anexo3Cx;
                                                    }
                                                    else
                                                    {
                                                        valor = "(51) 54 258787";
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    case "tAlumnos.apepaterno":
                                        {
                                            if (Alumno != null)
                                            {
                                                valor = Alumno.ApellidoPaterno;
                                            }
                                        }
                                        break;
                                    case "tAlumnos.apematerno":
                                        {
                                            if (Alumno != null)
                                            {
                                                valor = Alumno.ApellidoMaterno;
                                            }
                                        }
                                        break;
                                    case "tAlumnos.nombre1":
                                        {
                                            if (Alumno != null)
                                            {
                                                valor = Alumno.Nombre1;
                                            }
                                        }
                                        break;
                                    case "tAlumnos.nombre2":
                                        {
                                            if (Alumno != null)
                                            {
                                                valor = Alumno.Nombre2;
                                            }
                                        }
                                        break;
                                    case "T_Alumno.NombrePGeneralUltimoEnvioMasivo":
                                        {
                                            if (Alumno != null)
                                            {
                                                valor = _repAlumno.ObtenerNombreProgramaGeneralUltimoEnvioMasivo(Alumno.Id);
                                            }
                                        }
                                        break;
                                    case "T_Alumno.NombrePGeneralUltimaSolicitudInformacion":
                                        {
                                            if (Alumno != null)
                                            {
                                                valor = _repAlumno.ObtenerNombreProgramaGeneralUltimaSolicitudInformacion(Alumno.Id);
                                            }
                                        }
                                        break;
                                    case "tAlumnos.email1":
                                        {
                                            if (Alumno != null)
                                            {
                                                valor = Alumno.Email1;
                                            }
                                        }
                                        break;
                                    default:
                                        valor = ""; break;
                                }

                            }
                        }
                        if (valor != null)
                        {
                            valor = valor.Replace("#$%", "<br>");
                            plantilla = plantilla.Replace("{" + etiqueta + "}", valor);

                            plantillaEtiqueValor.codigo = "{ " + etiqueta + "}";
                            plantillaEtiqueValor.texto = valor;

                        }
                        else
                        {
                            plantilla = plantilla.Replace("{" + etiqueta + "}", "");

                            plantillaEtiqueValor.codigo = "{ " + etiqueta + "}";
                            plantillaEtiqueValor.texto = "";
                        }
                        objetoplantilla.Add(plantillaEtiqueValor);


                    }
                }

                return Ok(new { plantilla, objetoplantilla });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera plantilla para centro de costo para programa especifico.
        /// </summary>
        /// <returns> String </returns>
        [Route("[Action]/{IdCentroCosto}/{IdPlantilla}")]
        [HttpGet]
        public ActionResult GenerarPlantillaCentroCosto(int IdCentroCosto, int IdPlantilla)
        {
            CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio();
            PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
            PlantillaClaveValorRepositorio _repPlantillaClaveValor = new PlantillaClaveValorRepositorio();
            ListadoEtiquetaBO ListadoEtiqueta = new ListadoEtiquetaBO(_integraDBContext);
            DocumentoSeccionPwRepositorio _repDocumentoSeccionPw = new DocumentoSeccionPwRepositorio(_integraDBContext);

            string plantilla = string.Empty;
            string valor = string.Empty;

            try
            {
                plantilla = _repPlantillaClaveValor.GetBy(w => w.Estado == true && w.IdPlantilla == IdPlantilla && w.Clave == "Texto", w => new { w.Valor }).FirstOrDefault().Valor;

                var rpta = _repCentroCosto.ObtenerCentroCostoParaPEspecifico(IdCentroCosto);
                if (rpta != null)
                {
                    plantilla = plantilla.Replace("{tPartner.nombre}", rpta.NombrePartner);
                    plantilla = plantilla.Replace("{tPEspecifico.nombre}", rpta.NombrePEspecifico);
                }

                foreach (string word in plantilla.Split('{'))
                {
                    if (word.Contains('}'))
                    {
                        string etiqueta = word.Split('}')[0];
                        if (etiqueta.Contains("TemplateV2"))
                        {
                            DocumentosBO documentos = new DocumentosBO(_integraDBContext);
                            var pEspecifico = _repPEspecifico.FirstBy(x => x.IdCentroCosto == IdCentroCosto);
                            var pGeneral = _repPgeneral.FirstBy(x => x.Id == pEspecifico.IdProgramaGeneral.Value);

                            List<ProgramaGeneralSeccionDocumentoDTO> listaSecciones = documentos.ObtenerListaSeccionDocumentoProgramaGeneral(pGeneral.Id);

                            string valorV2 = string.Empty;
                            string[] array = etiqueta.Split(".");

                            string nombreSeccion = array[array.Length - 1];
                            bool conTitulo = nombreSeccion == "Estructura Curricular" ? true : false;

                            List<ProgramaGeneralSeccionAnexosHTMLDTO> seccion = ListadoEtiqueta.GenerarHTMLProgramaGeneralDocumentoSeccion(listaSecciones.Where(x => x.Seccion == nombreSeccion).ToList(), conTitulo);

                            foreach (ProgramaGeneralSeccionAnexosHTMLDTO item01 in seccion)
                                valorV2 += item01.Contenido;

                            if (valorV2 == "")
                            {
                                nombreSeccion = nombreSeccion == "Certificacion" ? "Certificación" : nombreSeccion;

                                List<SeccionDocumentoDTO> seccionV1 = _repDocumentoSeccionPw.ObtenerSecciones(pGeneral.Id).Where(x => x.Titulo == nombreSeccion).ToList();

                                foreach (SeccionDocumentoDTO item01 in seccionV1)
                                    valorV2 += item01.Contenido;
                            }

                            if (valorV2 != null)
                                valor = valorV2;
                            else
                                valor = null;
                        }
                        //Separamos solo los Id´s
                        else if (etiqueta.Contains("Template") && !etiqueta.Contains("V2"))
                        {
                            List<string> ListaPalabras = new List<string>();
                            char[] delimitador = new char[] { '.' };
                            string idPlantilla = "";
                            string IdColumna = "";
                            string[] array = etiqueta.Split(delimitador, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string s in array)
                            {
                                ListaPalabras.Add(s);
                            }
                            idPlantilla = ListaPalabras[3].ToString();
                            IdColumna = ListaPalabras[4].ToString();
                            string Etiquetatemp = IdPlantilla + "." + IdColumna;
                            var result = _repPespecifico.ObtenerSeccionEtiquetaAgendaMensaje(IdColumna, idPlantilla, IdCentroCosto);
                            if (result != null)
                                valor = result.Valor;
                            else
                                valor = null;
                        }
                        else
                        {
                            valor = "";
                        }
                        if (valor != null)
                        {
                            valor = valor.Replace("#$%", "<br>");
                            plantilla = plantilla.Replace("{" + etiqueta + "}", valor);

                        }
                        else
                        {
                            plantilla = plantilla.Replace("{" + etiqueta + "}", "");
                        }
                    }
                }

                return Ok(plantilla);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera plantilla para centro de costo para programa especifico v2.
        /// </summary>
        /// <returns> String </returns>
        [Route("[Action]/{IdCentroCosto}/{IdPlantilla}/{IdPersonal?}")]
        [HttpGet]
        public ActionResult GenerarPlantillaCentroCostoV2(int IdCentroCosto, int IdPlantilla, int? IdPersonal)
        {
            CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio();
            PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
            PlantillaClaveValorRepositorio _repPlantillaClaveValor = new PlantillaClaveValorRepositorio();
            ListadoEtiquetaBO ListadoEtiqueta = new ListadoEtiquetaBO(_integraDBContext);
            DocumentoSeccionPwRepositorio _repDocumentoSeccionPw = new DocumentoSeccionPwRepositorio(_integraDBContext);

            string plantilla = string.Empty;

            try
            {
                plantilla = _repPlantillaClaveValor.GetBy(w => w.Estado == true && w.IdPlantilla == IdPlantilla && w.Clave == "Texto", w => new { w.Valor }).FirstOrDefault().Valor;

                var rpta = _repCentroCosto.ObtenerCentroCostoParaPEspecifico(IdCentroCosto);
                if (rpta != null)
                {
                    plantilla = plantilla.Replace("{tPartner.nombre}", rpta.NombrePartner);
                    plantilla = plantilla.Replace("{tPEspecifico.nombre}", rpta.NombrePEspecifico);
                }

                reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                {
                    IdPlantilla = IdPlantilla
                };
                reemplazoEtiquetaPlantilla.ReemplazarEtiquetasSinOportunidad(IdPersonal.Value, IdCentroCosto);

                return Ok(reemplazoEtiquetaPlantilla.EmailReemplazado.CuerpoHTML);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener Configuraciones.
        /// </summary>
        /// <returns>  </returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerConfiguraciones()
        {
            try
            {
                AreaCapacitacionRepositorio _repAreaCapacitacion = new AreaCapacitacionRepositorio();
                SubAreaCapacitacionRepositorio _repSubAreaCapacitacion = new SubAreaCapacitacionRepositorio();
                PgeneralRepositorio _repPgeneral = new PgeneralRepositorio();
                var ListaAreaCapacitacion = _repAreaCapacitacion.GetBy(x => x.Estado == true, x => new { x.Id, x.Nombre });
                var ListaSubAreaCapacitacion = _repSubAreaCapacitacion.GetBy(x => x.Estado == true, x => new { x.Id, x.Nombre, x.IdAreaCapacitacion });
                var ListaProgramaGeneral = _repPgeneral.GetBy(x => x.Estado == true, x => new { x.Id, x.Nombre });
                return Ok(new { ListaAreaCapacitacion, ListaSubAreaCapacitacion, ListaProgramaGeneral });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene filtros de agenda.
        /// </summary>
        /// <returns> ObjetoDTO: List<EstadoOcurrenciaFiltroDTO> </returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltrosAgenda()
        {
            try
            {
                FiltroAgendaDTO filtroAgenda = new FiltroAgendaDTO();
                EstadoOcurrenciaRepositorio _repEstadoOcurrencia = new EstadoOcurrenciaRepositorio();
                var data = _repEstadoOcurrencia.GetBy(x => x.Estado == true, x => new EstadoOcurrenciaFiltroDTO { Id = x.Id, Nombre = x.Nombre });
                filtroAgenda.listaEstadoOcurrencia = new List<EstadoOcurrenciaFiltroDTO>();
                foreach (var item in data)
                {
                    var temp = new EstadoOcurrenciaFiltroDTO
                    {
                        Id = item.Id,
                        Nombre = item.Nombre
                    };
                    filtroAgenda.listaEstadoOcurrencia.Add(temp);
                }
                FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio();
                filtroAgenda.listaFaseOportunidad = _repFaseOportunidad.ObtenerTodoFiltro();
                TipoDatoRepositorio _repTipoDato = new TipoDatoRepositorio();
                filtroAgenda.listaTipoDato = _repTipoDato.ObtenerFiltro();
                OrigenRepositorio _repOrigen = new OrigenRepositorio();
                filtroAgenda.listaOrigen = _repOrigen.ObtenerTodoFiltro();
                ProbabilidadRegistroPwRepositorio _repProbabilidadRegistro = new ProbabilidadRegistroPwRepositorio();
                filtroAgenda.listaProbabilidadRegistro = _repProbabilidadRegistro.ObtenerTodoFiltro();
                CategoriaOrigenRepositorio _repCategoriaOrigen = new CategoriaOrigenRepositorio();
                filtroAgenda.listaCategoriaOrigen = _repCategoriaOrigen.ObtenerCategoriaFiltro();

                return Ok(filtroAgenda);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 08/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Alumno AutoComplete
        /// </summary>
        /// <returns> Lista de objetoDTO: List<AlumnoFiltroAutocompleteDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerAlumnoAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                if (Filtros != null)
                {
                    AlumnoRepositorio _repAlumno = new AlumnoRepositorio();
                    return Ok(_repAlumno.ObtenerTodoFiltroAutoComplete(Filtros["valor"].ToString()));
                }
                else
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene filtro autocompletar para docente.
        /// </summary>
        /// <returns> ObjetoDTO: List<AlumnoFiltroAutocompleteDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerDocenteAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                if (Filtros != null)
                {
                    AlumnoRepositorio _repAlumno = new AlumnoRepositorio();
                    return Ok(_repAlumno.ObtenerTodoDocenteFiltroAutoComplete(Filtros["valor"].ToString()));
                }
                else
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene filtro autocompletar para proveedores.
        /// </summary>
        /// <returns> ObjetoDTO: List<AlumnoFiltroAutocompleteDTO> </returns>  
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerProveedoresAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                if (Filtros != null)
                {
                    AlumnoRepositorio _repAlumno = new AlumnoRepositorio();
                    return Ok(_repAlumno.ObtenerTodoProveedorFiltroAutoComplete(Filtros["valor"].ToString()));
                }
                else
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 08/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Centro de Costo AutoComplete
        /// </summary>
        /// <returns> Lista de objetoDTO: List<CentroCostoFiltroAutocompleteDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCentroCostoAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                if (Filtros != null)
                {
                    CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio();
                    return Ok(_repCentroCosto.ObtenerTodoFiltroAutoComplete(Filtros["valor"].ToString()));
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 08/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Centro de Costo AutoComplete
        /// </summary>
        /// <returns> Lista de objetoDTO: List<CentroCostoFiltroAutocompleteDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCentroCostoAutocompleteV2([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                if (Filtros != null)
                {
                    CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio();
                    return Ok(_repCentroCosto.ObtenerTodoFiltroAutoCompleteV2(Filtros["valor"].ToString()));
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las empresas que contengan el valor nombre.
        /// </summary>
        /// <returns> Lista de objetoDTO: List<CentroCostoFiltroAutocompleteDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerEmpresaAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                if (Filtros != null)
                {
                    EmpresaRepositorio _repEmpresa = new EmpresaRepositorio();

                    return Ok(_repEmpresa.ObtenerTodoFiltroAutoComplete(Filtros["valor"].ToString()));
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Jashin S.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene una lista de asesores filtrado por el nombre
        /// </summary>
        /// <returns> Lista de objetoDTO: List<CentroCostoFiltroAutocompleteDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerAsesorAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            PersonalBO asesor = new PersonalBO();
            if (Filtros != null)
            {
                if (Filtros["valor"] == null)
                {
                    return Ok(new { data = asesor.ListaAsesorAutocomplete });
                }
                asesor.CargarAsesorAutocomplete(Filtros["valor"].ToString());
                return Ok(new { data = asesor.ListaAsesorAutocomplete });
            }
            return Ok(new { data = asesor.ListaAsesorAutocomplete });
        }

        /// TipoFuncion: GET
        /// Autor: Jashin S.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Lista de Tipo de Datos con estado activo.
        /// </summary>
        /// <returns> objetoDTO: FiltroDTO </returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltroTipoDato()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoDatoRepositorio _repTipoDato = new TipoDatoRepositorio();
                return Ok(_repTipoDato.ObtenerFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jashin S.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener Centro Costo Para Filtro   
        /// </summary>
        /// <returns> Lista de objetoDTO: List<FiltroDTO> </returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltroCentroCosto()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio();
                return Ok(_repCentroCosto.ObtenerCentroCostoParaFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jashin S.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene lista de los cargos para ser usados en filtros 
        /// </summary>
        /// <returns> Lista de objetoDTO: List<FiltroDTO> </returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltroOrigen()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OrigenRepositorio _repOrigen = new OrigenRepositorio();
                return Ok(_repOrigen.ObtenerTodoFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jashin S.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las actividades no programadas
        /// </summary>
        /// <returns> objetoDTO: ActividadProgramadaAgendaDTO() </returns>
        [Route("[Action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerAgendaNoProgramada(int IdOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio();
                return Ok(_repOportunidad.ObtenerAgendaNoProgramada(IdOportunidad, ValorEstatico.IdEstadoOportunidadNoProgramada));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jashin S.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las configuraciones por defecto.
        /// </summary>
        /// <returns> Dictionary<string, string> </returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerValorEstatico()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(ValorEstatico.GetProperties());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jashin S.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Crea un nuevo registro de una persona y su clasificacion persona.
        /// </summary>
        /// <returns>  </returns>
        [Route("[Action]/{idTablaOriginal}/{idTipoPersona}/{usuario}")]
        [HttpGet]
        public ActionResult InsertarPersona(int idTablaOriginal, int idTipoPersona, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (idTipoPersona == 1)
                {
                    PersonaBO persona = new PersonaBO(new integraDBContext());
                    var val = persona.InsertarPersona(idTablaOriginal, Aplicacion.Base.Enums.Enums.TipoPersona.Alumno, usuario);
                    return Ok(new { val });
                }
                if (idTipoPersona == 2)
                {
                    PersonaBO persona = new PersonaBO(new integraDBContext());
                    var val = persona.InsertarPersona(idTablaOriginal, Aplicacion.Base.Enums.Enums.TipoPersona.Personal, usuario);
                    return Ok(new { val });
                }
                if (idTipoPersona == 3)
                {
                    PersonaBO persona = new PersonaBO(new integraDBContext());
                    var val = persona.InsertarPersona(idTablaOriginal, Aplicacion.Base.Enums.Enums.TipoPersona.Docente, usuario);
                    return Ok(new { val });
                }
                if (idTipoPersona == 4)
                {
                    PersonaBO persona = new PersonaBO(new integraDBContext());
                    var val = persona.InsertarPersona(idTablaOriginal, Aplicacion.Base.Enums.Enums.TipoPersona.Proveedor, usuario);
                    return Ok(new { val });
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jashin S.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene por CodigoMatricula a que Tab Pertenece
        /// </summary>
        /// <returns> OportunidadTabAgenda </returns>
        [Route("[Action]/{IdPersonal}/{TextoBuscar}/{Tipo}")]
        [HttpGet]
        public ActionResult ObtenerClasificacionTab(int IdPersonal, string TextoBuscar, int Tipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio();
                var clasificacionTab = _repOportunidad.ObtenerClasificacionTabAgenda(IdPersonal, TextoBuscar, Tipo);
                return Ok(clasificacionTab);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jashin S.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera plantilla para envio mailing.
        /// </summary>
        /// <returns> ObjetoDTO: PlantillaEmailMandrillDTO </returns>
        [Route("[Action]/{IdOportunidad}/{IdPlantilla}")]
        [HttpGet]
        public ActionResult GenerarPlantillaMailing(int IdOportunidad, int IdPlantilla)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repOportunidad.Exist(IdOportunidad))
                {
                    return BadRequest("Oportunidad no existente");
                }

                if (!_repPlantilla.Exist(IdPlantilla))
                {
                    return BadRequest("Plantilla no existente");
                }

                var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                {
                    IdOportunidad = IdOportunidad,
                    IdPlantilla = IdPlantilla
                };

                var plantillaEmail = new PlantillaEmailMandrillDTO();
                try
                {
                    _reemplazoEtiquetaPlantilla.ReemplazarEtiquetas();
                    plantillaEmail = _reemplazoEtiquetaPlantilla.EmailReemplazado;
                }
                catch (Exception)
                {
                }
                return Ok(plantillaEmail);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jashin S.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera plantilla para envio WhatsApp.
        /// </summary>
        /// <returns> ObjetoDTO: PlantillaWhatsAppCalculadoDTO </returns>
        [Route("[Action]/{IdOportunidad}/{IdPlantilla}")]
        [HttpGet]
        public ActionResult GenerarPlantillaWhatsapp(int IdOportunidad, int IdPlantilla)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                if (!_repOportunidad.Exist(IdOportunidad))
                {
                    return BadRequest("Oportunidad no existente");
                }

                if (!_repPlantilla.Exist(IdPlantilla))
                {
                    return BadRequest("Plantilla no existente");
                }

                var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                {
                    IdOportunidad = IdOportunidad,
                    IdPlantilla = IdPlantilla
                };

                var plantillaWhatsApp = new PlantillaWhatsAppCalculadoDTO();
                try
                {
                    _reemplazoEtiquetaPlantilla.ReemplazarEtiquetas();
                    plantillaWhatsApp = _reemplazoEtiquetaPlantilla.WhatsAppReemplazado;
                }
                catch (Exception)
                {
                }

                return Ok(new { plantilla = plantillaWhatsApp.Plantilla, objetoplantilla = plantillaWhatsApp.ListaEtiquetas });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jashin S.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera plantilla para envio WhatsApp Docente.
        /// </summary>
        /// <returns> ObjetoDTO: PlantillaWhatsAppCalculadoDTO </returns>
        [Route("[Action]/{IdOportunidad}/{IdPlantilla}")]
        [HttpGet]
        public ActionResult GenerarPlantillaWhatsappDocente(int IdOportunidad, int IdPlantilla)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repOportunidad.Exist(IdOportunidad))
                {
                    return BadRequest("Oportunidad no existente");
                }

                if (!_repPlantilla.Exist(IdPlantilla))
                {
                    return BadRequest("Plantilla no existente");
                }

                var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                {
                    IdOportunidad = IdOportunidad,
                    IdPlantilla = IdPlantilla
                };

                var plantillaWhatsApp = new PlantillaWhatsAppCalculadoDTO();
                try
                {
                    _reemplazoEtiquetaPlantilla.ReemplazarEtiquetasDocente();
                    plantillaWhatsApp = _reemplazoEtiquetaPlantilla.WhatsAppReemplazado;
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                return Ok(new { plantilla = plantillaWhatsApp.Plantilla, objetoplantilla = plantillaWhatsApp.ListaEtiquetas });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jashin S.
        /// Fecha: 14/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las plantillas mailing disponibles para operaciones.
        /// </summary>
        /// <returns> Lista de ObjetoDTO: List<ContenidoPlantillaDTO> </returns>
        [Route("[Action]/{IdPersonalAreaTrabajo}")]
        [HttpGet]
        public ActionResult ObtenerTodoPlantilla(int IdPersonalAreaTrabajo)
        {
            try
            {
                PlantillaClaveValorRepositorio Plantilla = new PlantillaClaveValorRepositorio();
                var listaPlantillasDisponibles = Plantilla.ObtenerTodoPlantillasMailing();
                if (IdPersonalAreaTrabajo == ValorEstatico.IdPersonalAreaTrabajoOperaciones)
                {
                    listaPlantillasDisponibles.AddRange(_repPlantillaClaveValor.ObtenerPlantillaGenerarMensajeOperaciones().Select(
                            x => new ContenidoPlantillaDTO
                            {
                                Id = x.Id,
                                Nombre = x.Nombre,
                                Clave = "",
                                IdAreaEtiqueta = 0,
                                IdPlantillaClaveValor = 0,
                                Valor = ""
                            }
                        ));
                }
                return Ok(new { data = listaPlantillasDisponibles });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }
        /// TipoFuncion: GET
        /// Autor: Jashin S.
        /// Fecha: 27/01/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera plantilla para envio WhatsApp.
        /// </summary>
        /// <returns> ObjetoDTO: PlantillaWhatsAppCalculadoDTO </returns>
        [Route("[Action]/{IdOportunidad}/{IdPlantilla}")]
        [HttpGet]
        public ActionResult GenerarPlantillaWhatsappAlterno(int IdOportunidad, int IdPlantilla)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                if (!_repOportunidad.Exist(IdOportunidad))
                {
                    return BadRequest("Oportunidad no existente");
                }

                if (!_repPlantilla.Exist(IdPlantilla))
                {
                    return BadRequest("Plantilla no existente");
                }

                var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                {
                    IdOportunidad = IdOportunidad,
                    IdPlantilla = IdPlantilla
                };

                var plantillaWhatsApp = new PlantillaWhatsAppCalculadoDTO();
                try
                {
                    _reemplazoEtiquetaPlantilla.ReemplazarEtiquetasNuevasOportunidades();
                    plantillaWhatsApp = _reemplazoEtiquetaPlantilla.WhatsAppReemplazado;
                }
                catch (Exception)
                {
                }

                return Ok(new { plantilla = plantillaWhatsApp.Plantilla, objetoplantilla = plantillaWhatsApp.ListaEtiquetas });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}