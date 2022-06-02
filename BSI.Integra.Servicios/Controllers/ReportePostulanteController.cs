using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReportePostulanteController
    /// Autor: Britsel C., Luis H., Edgar S.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de Reporte de Etapas de Postulantes y Proceso de Selección
    /// </summary>
    [Route("api/ReportePostulante")]
    public class ReportePostulanteController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly PostulanteRepositorio _repPostulante;
        private readonly EtapaProcesoSeleccionCalificadoRepositorio _repEtapaProcesoSeleccionCalificado;
        private readonly ExamenAsignadoRepositorio repExamenAsignado;
        private readonly ExamenAsignadoEvaluadorRepositorio _repExamenAsignadoEvaluador;
        private readonly ConfiguracionAsignacionEvaluacionRepositorio _repAsignacionEvaluacion;
        private readonly PostulanteComparacionRepositorio _repPostulanteComparacionRep;
        private readonly EtapaProcesoSeleccionCalificadoRepositorio _repEtapaCalificacionRep;
        private readonly ProcesoSeleccionEtapaRepositorio _repEtapaRep;
        private readonly ExamenTestRepositorio _repExamenTest;
        private readonly ExamenRepositorio _repExamen;


        public ReportePostulanteController()
        {
            _integraDBContext = new integraDBContext();
            _repPostulante = new PostulanteRepositorio(_integraDBContext);
            _repEtapaProcesoSeleccionCalificado= new EtapaProcesoSeleccionCalificadoRepositorio(_integraDBContext);
            repExamenAsignado = new ExamenAsignadoRepositorio(_integraDBContext);
            _repExamenAsignadoEvaluador = new ExamenAsignadoEvaluadorRepositorio(_integraDBContext);
            _repAsignacionEvaluacion= new ConfiguracionAsignacionEvaluacionRepositorio(_integraDBContext);
            _repPostulanteComparacionRep = new PostulanteComparacionRepositorio(_integraDBContext);
            _repEtapaCalificacionRep = new EtapaProcesoSeleccionCalificadoRepositorio(_integraDBContext);
            _repEtapaRep = new ProcesoSeleccionEtapaRepositorio(_integraDBContext);
            _repExamenTest = new ExamenTestRepositorio(_integraDBContext);
            _repExamen = new ExamenRepositorio(_integraDBContext);
        }

        /// TipoFuncion: POST
        /// Autor: Luis H, Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene lista de postulantes por filtro
        /// </summary>
        /// <returns> Lista de postulantes por filtro </returns>
        /// <returns> Lista de Objeto DTO: List<PostulanteInformacionDatosDTO> </returns>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult ObtenerPostulantesPorFiltro([FromBody] ReportePostulanteDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var idProcesoAuxiliar = Filtro.ListaProcesoSeleccion == null? 0 : Filtro.ListaProcesoSeleccion;
                var listaEstado = (Filtro.ListaEstadoProceso == null || Filtro.ListaEstadoProceso.Count == 0) ? "" : string.Join(",", Filtro.ListaEstadoProceso.Select(x => x));
                var listaEtapa = (Filtro.ListaEtapaProceso == null || Filtro.ListaEtapaProceso.Count == 0) ? "" : string.Join(",", Filtro.ListaEtapaProceso.Select(x => x));
                if (Filtro.Check == false) {

                    List<int> listapostulante = new List<int>();

                    //Si no tiene como fily
                    if (Filtro.ListaEstadoProceso.Count == 0 && Filtro.ListaEtapaProceso.Count == 0)
                    {
                        Filtro.ListaEtapaProceso = new List<int>();
                        Filtro.ListaEstadoProceso = new List<int>();
                    }
                    if (Filtro.ListaEstadoProceso.Count>0  && Filtro.ListaEtapaProceso.Count>0) { // SE AÑADIO VALIDACION DE ES ETAPA ACTUAL
                        listapostulante=_repEtapaProcesoSeleccionCalificado.GetBy(x => Filtro.ListaEtapaProceso.Contains(x.IdProcesoSeleccionEtapa) && Filtro.ListaEstadoProceso.Contains(x.IdEstadoEtapaProcesoSeleccion.Value)).Select(x => x.IdPostulante).Distinct().ToList();
                        if (listapostulante.Count == 0)
                        {
                            listapostulante.Add(0);
                        }
                        Filtro.ListaPostulante = listapostulante;
                        Filtro.ListaEtapaProceso = new List<int>();
                        Filtro.ListaEstadoProceso = new List<int>();
                        Filtro.ListaProcesoSeleccion = null;
                    }
                    if (Filtro.ListaEstadoProceso.Count>0 && Filtro.ListaEtapaProceso.Count==0)
                    {
                        listapostulante = _repEtapaProcesoSeleccionCalificado.GetBy(x => Filtro.ListaEstadoProceso.Contains(x.IdEstadoEtapaProcesoSeleccion.Value)).Select(x => x.IdPostulante).Distinct().ToList();
                        if (listapostulante.Count == 0)
                        {
                            listapostulante.Add(0);
                        }
                        Filtro.ListaPostulante = listapostulante;
                        Filtro.ListaEtapaProceso = new List<int>();
                        Filtro.ListaEstadoProceso = new List<int>();
                        Filtro.ListaProcesoSeleccion = null;
                    }
                    if (Filtro.ListaEstadoProceso.Count == 0 && Filtro.ListaEtapaProceso.Count > 0)
                    {
                        listapostulante = _repEtapaProcesoSeleccionCalificado.GetBy(x => Filtro.ListaEtapaProceso.Contains(x.IdProcesoSeleccionEtapa)).Select(x => x.IdPostulante).Distinct().ToList();
                        if (listapostulante.Count == 0)
                        {
                            listapostulante.Add(0);
                        }
                        Filtro.ListaPostulante = listapostulante;
                        Filtro.ListaEtapaProceso = new List<int>();
                        Filtro.ListaEstadoProceso = new List<int>();
                        Filtro.ListaProcesoSeleccion = null;
                    }

                }
                if ((Filtro.ListaProcesoSeleccion == null) && Filtro.Check == false && Filtro.ListaPostulante.Count<=0) {

                    Filtro.ListaProcesoSeleccion = -1;
                }
                var postulante = _repPostulante.ObtenerPostulanteInformacion_V2(Filtro);
                if (idProcesoAuxiliar > 0)
                {
                    postulante = postulante.Where(x => x.IdProceso == idProcesoAuxiliar).ToList(); //Filtrado de otros procesos de seleccion
                }
                foreach (var item in postulante)
                {
                    item.ListaEtapa = listaEtapa;
                    item.ListaEstados = listaEstado;
                }
                return Ok(postulante);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Luis H, Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Estados, etapas y puntaje de postulantes
        /// </summary>
        /// <returns> Reporte de Estados, etapas y puntaje de postulantes </returns>
        /// <returns>   Objeto Agrupado : 
        ///             DatosAgrupado = List<ProcesoSelecionExamenesCompletosDTO>,
        ///             Postulantes = List<ProcesoSelecionExamenesCompletosDTO>,
        ///             Estado = bool,
        ///             datosEtapaAprobada = List<ReportePruebaDTO>
        ///</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReportePostulanteDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var postulanteProceso = _repPostulante.ObtenerPostulantesUltimoProcesoSeleccion(Filtro);
                if (postulanteProceso == null || postulanteProceso.Count == 0) return BadRequest("No se encontraron postulantes en el Proceso de Seleccion");               
                if (Filtro.Check == false)
                {
                    List<int> listapostulante = new List<int>();
                    if (Filtro.ListaEstadoProceso.Count == 0 && Filtro.ListaEtapaProceso.Count == 0)
                    {
                        if (Filtro.IdGrupoComparacion != null && Filtro.IdGrupoComparacion > 0)
                        {
                            listapostulante = repExamenAsignado.GetBy(x => x.IdProcesoSeleccion == Filtro.ListaProcesoSeleccion).Select(x => x.IdPostulante).Distinct().ToList();
                            Filtro.ListaProcesoSeleccion = null;
                            Filtro.ListaPostulante = listapostulante;
                        }
                        Filtro.ListaEtapaProceso = new List<int>();
                        Filtro.ListaEstadoProceso = new List<int>();
                    }
                    if (Filtro.ListaEstadoProceso.Count > 0 && Filtro.ListaEtapaProceso.Count > 0)
                    {
                        string listaEstadoProceso = "";
                        for (var i = 0; Filtro.ListaEstadoProceso.Count > i; i++)
                        {
                            if (i == 0) listaEstadoProceso = Filtro.ListaEstadoProceso[i] + "";                            
                            else listaEstadoProceso = listaEstadoProceso + "," + Filtro.ListaEstadoProceso[i];                            
                        }

                        string listaEtapaProceso = "";
                        for (var i = 0; Filtro.ListaEtapaProceso.Count > i; i++)
                        {
                            if (i == 0) listaEtapaProceso = Filtro.ListaEtapaProceso[i] + "";                            
                            else listaEtapaProceso = listaEtapaProceso + "," + Filtro.ListaEtapaProceso[i]; 
                        }

                        string listaPostulantes = "";
                        for (var i = 0; postulanteProceso.Count > i; i++)
                        {
                            if (i == 0) listaPostulantes = postulanteProceso[i].IdPostulante + "";                            
                            else listaPostulantes = listaPostulantes + "," + postulanteProceso[i].IdPostulante;                            
                        }

                        string filtroPostulante = "AND IdEstadoEtapaProcesoSeleccion in(" + listaEstadoProceso + ") AND IdProcesoSeleccionEtapa IN ("+ listaEtapaProceso + ")";
                        var listapostulante_v2 = _repEtapaProcesoSeleccionCalificado.ObtenerEtapasProcesoSeleccionActual(listaPostulantes, filtroPostulante);

                        if (listapostulante_v2.Count == 0) return BadRequest("No se encontraron postulantes");
                        listapostulante = listapostulante_v2.Select(x => x.IdPostulante).Distinct().ToList();

                        if (listapostulante.Count == 0) listapostulante.Add(0);                       
                        Filtro.ListaPostulante = listapostulante;
                        Filtro.ListaEtapaProceso = new List<int>();
                        Filtro.ListaEstadoProceso = new List<int>();
                        Filtro.ListaProcesoSeleccion = null;

                        postulanteProceso = postulanteProceso.Where(x => listapostulante.Contains(x.IdPostulante)).ToList();
                    }
                    if (Filtro.ListaEstadoProceso.Count > 0 && Filtro.ListaEtapaProceso.Count == 0)
                    {
                        string listaEstadoProceso = "";
                        for (var i = 0; Filtro.ListaEstadoProceso.Count > i; i++)
                        {
                            if (i == 0) listaEstadoProceso = Filtro.ListaEstadoProceso[i] + "";
                            else listaEstadoProceso = listaEstadoProceso + "," + Filtro.ListaEstadoProceso[i];                           
                        }

                        string listaPostulantes = "";
                        for (var i = 0; postulanteProceso.Count > i; i++)
                        {
                            if (i == 0) listaPostulantes = postulanteProceso[i].IdPostulante + "";                            
                            else listaPostulantes = listaPostulantes + "," + postulanteProceso[i].IdPostulante;
                        }

                        string filtroPostulante = "AND IdEstadoEtapaProcesoSeleccion in(" + listaEstadoProceso + ")";
                        var listapostulante_v2 = _repEtapaProcesoSeleccionCalificado.ObtenerEtapasProcesoSeleccionActual(listaPostulantes, filtroPostulante);

                        if (listapostulante_v2.Count == 0) return BadRequest("No se encontraron postulantes");                        

                        listapostulante = listapostulante_v2.Select(x => x.IdPostulante).Distinct().ToList();

                        if (listapostulante.Count == 0) listapostulante.Add(0);
                       
                        Filtro.ListaPostulante = listapostulante;
                        Filtro.ListaEtapaProceso = new List<int>();
                        Filtro.ListaEstadoProceso = new List<int>();
                        Filtro.ListaProcesoSeleccion = null;

                        postulanteProceso = postulanteProceso.Where(x => listapostulante.Contains(x.IdPostulante)).ToList();
                    }
                    if (Filtro.ListaEstadoProceso.Count == 0 && Filtro.ListaEtapaProceso.Count > 0)
                    {

                        string listaEtapaProceso = "";
                        for (var i = 0; Filtro.ListaEtapaProceso.Count > i; i++)
                        {
                            if (i == 0) listaEtapaProceso = Filtro.ListaEtapaProceso[i] + "";                            
                            else listaEtapaProceso = listaEtapaProceso + "," + Filtro.ListaEtapaProceso[i];                            
                        }

                        string listaPostulantes = "";
                        for (var i = 0; postulanteProceso.Count > i; i++)
                        {
                            if (i == 0) listaPostulantes = postulanteProceso[i].IdPostulante + "";
                            else listaPostulantes = listaPostulantes + "," + postulanteProceso[i].IdPostulante;                            
                        }

                        string filtroPostulante = "AND IdProcesoSeleccionEtapa in(" + listaEtapaProceso + ") ";
                        var listapostulante_v2 = _repEtapaProcesoSeleccionCalificado.ObtenerEtapasProcesoSeleccionActual(listaPostulantes, filtroPostulante);

                        if (listapostulante_v2.Count == 0) return BadRequest("No se encontraron postulantes");                       

                        listapostulante = listapostulante_v2.Select(x => x.IdPostulante).Distinct().ToList();

                        if (listapostulante.Count == 0) listapostulante.Add(0);
                        
                        Filtro.ListaPostulante = listapostulante;
                        Filtro.ListaEtapaProceso = new List<int>();
                        Filtro.ListaEstadoProceso = new List<int>();
                        Filtro.ListaProcesoSeleccion = null;

                        postulanteProceso = postulanteProceso.Where(x => listapostulante.Contains(x.IdPostulante)).ToList();
                    }
                    if (Filtro.FechaInicio == null || Filtro.FechaFin == null)
                    {
                        Filtro.FechaFin = DateTime.Now;
                        Filtro.FechaInicio = new DateTime(1900, 12, 31);
                    }

                }
                else
                {
                    Filtro.FechaFin = DateTime.Now;
                    Filtro.FechaInicio = new DateTime(1900, 12, 31);
                }

                Filtro.ListaPostulante = postulanteProceso.Select(x => x.IdPostulante).ToList();

                List<int> listaPostulanteComparacion = new List<int>();
                if (Filtro.IdGrupoComparacion != null && Filtro.IdGrupoComparacion != 0)
                {
                    listaPostulanteComparacion = _repPostulanteComparacionRep.GetBy(x => x.Estado == true && x.IdGrupoComparacionProcesoSeleccion == Filtro.IdGrupoComparacion).Select(x=>x.IdPostulante.Value).ToList();
                    Filtro.ListaPostulanteGrupoComparacion = new List<int>();

                    foreach (var item in listaPostulanteComparacion)
                    {
                        Filtro.ListaPostulanteGrupoComparacion.Add(item);
                    }
                }
                ReporteExamenProcesoSeleccionBO reporteExamenProcesoSeleccion = new ReporteExamenProcesoSeleccionBO(_integraDBContext);

                var reporte = reporteExamenProcesoSeleccion.ObtenerReporteExamenesNuevaVersion(Filtro);

                var listaComponenteAcceso = _repExamen.GetBy(x => x.IdCentroCosto != null && x.CantidadDiasAcceso != null).Select(x => x.Nombre).ToList();
                var listaAgregarConfiguracion = reporte.Where(x => listaComponenteAcceso.Contains(x.Examen)).ToList();
                foreach (var agregar in listaAgregarConfiguracion)
                {
                    agregar.ConfiguracionComponenteCurso = true;
                    agregar.IdExamenAccesoTemporal = agregar.IdExamen;
                }

                //Obtenidas las calificaciones de los postulantes se ordena para que muestre en orden las evaluacion y componentes y se agrupa por cada item que exista en la lista final
                var datosAgrupado = (from p in reporte orderby p.OrdenReal
                                     group p by new { p.OrdenReal } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

                var postulantes = (from p in reporte
                                   group p by new { p.IdPostulante, p.Postulante, p.Edad } into grupo
                                   select new { IdPostulante = grupo.Key.IdPostulante, Postulante = grupo.Key.Postulante, Edad = grupo.Key.Edad }).ToList();

                //se elimina de los postulantes obtenidos a los potulantes que pertenezcan al grupo de comparacion en caso se haya seleccionado en el filtro
                if (listaPostulanteComparacion.Count > 0) {
                    reporte = reporte.Where(x => !listaPostulanteComparacion.Contains(x.IdPostulante)).ToList();
                }

                // de la lita de postulantes que hayan pasado el filtro se agrupan por proceso de seleccion los postulantes.
                var listaProcesoSeleccionAgrupado = (from p in postulanteProceso
                                                     group p by new { p.IdProcesoSeleccion, p.ProcesoSeleccion } into grupo
                                                     select new { IdProcesoSeleccion = grupo.Key.IdProcesoSeleccion, ProcesoSeleccion = grupo.Key.ProcesoSeleccion }).ToList();

                List<ReportePruebaDetalleDTO> listaEtapas = new List<ReportePruebaDetalleDTO>();
                List<ReportePruebaDTO> listaEtapasFinal = new List<ReportePruebaDTO>();

                //Se busca todas las etapas segun el proceso de seleccion y se coloca por defecto el estado SIN RENDIR ID 9
                foreach (var item in listaProcesoSeleccionAgrupado)
                {
                    listaEtapas = listaEtapas.Concat(_repEtapaRep.GetBy(x => x.IdProcesoSeleccion == item.IdProcesoSeleccion).OrderBy(x => x.NroOrden).Select(x => new ReportePruebaDetalleDTO { IdProcesoSeleccion = item.IdProcesoSeleccion, ProcesoSeleccion = item.ProcesoSeleccion, IdEtapa = x.Id, Etapa = x.Nombre, EstadoEtapa = 0, IdEstadoEtapaProceso = 9, NroOrden = x.NroOrden, EtapaContactado = false }).ToList()).ToList();
                }


                List<ObtenerPostulantesProcesoSeleccionDTO> listaEtapasOptimizacion = new List<ObtenerPostulantesProcesoSeleccionDTO>();
                if (Filtro.ListaProcesoSeleccion != null)//[…]
                {
                    var listaPostulantesPrueba = "";
                    for (var i = 0; postulanteProceso.Count > i; i++)
                    {
                        if (i == 0) listaPostulantesPrueba = postulanteProceso[i].IdPostulante + "";                        
                        else listaPostulantesPrueba = listaPostulantesPrueba + "," + postulanteProceso[i].IdPostulante;                        
                    }
                    var obtenerProcesoSeleccion = Filtro.ListaProcesoSeleccion.GetValueOrDefault().ToString();
                    if(listaPostulantesPrueba == "") return BadRequest("No se encontraron postulantes");                    
                    else listaEtapasOptimizacion = _repEtapaCalificacionRep.ObtenerTodosLosPostulantes(listaPostulantesPrueba, obtenerProcesoSeleccion);                    
                }
                else
                {
                    var listaPostulantesPrueba = "";
                    List<string> listaProcesoSeleccion = new List<string>();
                    for (var i = 0; postulanteProceso.Count > i; i++)
                    {
                        if (i == 0) listaPostulantesPrueba = Filtro.ListaPostulante[i] + "";                        
                        else listaPostulantesPrueba = listaPostulantesPrueba + "," + Filtro.ListaPostulante[i];                        
                    }
                    var listaProceso = "";
                    var obtenerProcesoSeleccion = reporte.Select(x => x.IdProcesoSeleccion).Distinct().ToList();

                    if (obtenerProcesoSeleccion.Count > 0)
                    {
                        for (var i = 0; obtenerProcesoSeleccion.Count > i; i++)
                        {
                            if (i == 0) listaProceso = obtenerProcesoSeleccion[i] + "";                           
                            else listaProceso = listaProceso + "," + obtenerProcesoSeleccion[i];                            
                        }
                    }
                    else
                    {
                        for (var i = 0; listaProcesoSeleccionAgrupado.Count > i; i++)
                        {
                            if (i == 0) listaProceso = listaProcesoSeleccionAgrupado[i].IdProcesoSeleccion + "";
                            else listaProceso = listaProceso + "," + listaProcesoSeleccionAgrupado[i].IdProcesoSeleccion;                            
                        }
                    }
                    listaEtapasOptimizacion = _repEtapaCalificacionRep.ObtenerTodosLosPostulantes(listaPostulantesPrueba, listaProceso);
                }


                List<ReportePruebaDetalleDTO> etapasList;
                ReportePruebaDetalleDTO itemEtapa;
                //Se recorre la lista de los postulantes que cumplen el filtro y cada postulante se le asigna sus etapas correspondientes segun el proceso de seleccion que se encuentre
                foreach (var item in postulanteProceso) {
                    etapasList = new List<ReportePruebaDetalleDTO>();
                    etapasList = listaEtapas.Where(x => x.IdProcesoSeleccion == item.IdProcesoSeleccion).OrderBy(x=>x.NroOrden).ToList();
                    ReportePruebaDTO obj = new ReportePruebaDTO();
                    obj.IdPostulante = item.IdPostulante;
                    obj.Postulante = item.Postulante;
                    obj.Etapas = new List<ReportePruebaDetalleDTO>();

                    foreach (var item2 in etapasList) {
                        itemEtapa = new ReportePruebaDetalleDTO();
                        var etapaCalificada = listaEtapasOptimizacion.Where(x => x.IdPostulante == item.IdPostulante && x.IdProcesoSeleccionEtapa == item2.IdEtapa).FirstOrDefault();

                        itemEtapa.IdEtapa = item2.IdEtapa;
                        itemEtapa.Etapa = item2.Etapa;
                        itemEtapa.IdProcesoSeleccion = item2.IdProcesoSeleccion;
                        itemEtapa.ProcesoSeleccion = item2.ProcesoSeleccion;
                        itemEtapa.EstadoEtapa = item2.EstadoEtapa;
                        itemEtapa.IdEstadoEtapaProceso = item2.IdEstadoEtapaProceso;
                        itemEtapa.NroOrden = item2.NroOrden;
                        itemEtapa.EtapaContactado = item2.EtapaContactado;
                        itemEtapa.EsCalificadoPorPostulante = item2.EsCalificadoPorPostulante;

                        if (etapaCalificada != null) // Si tiene una calificacion en la tabla gp.T_EtapaPRocesoSeleccionCalificado reemplaza lo datos, en caso no exista coloca los datos por defecto
                        {
                            itemEtapa.EstadoEtapa = etapaCalificada.EsEtapaAprobada == true ? 1 : 0;
                            itemEtapa.IdEstadoEtapaProceso = etapaCalificada.IdEstadoEtapaProcesoSeleccion;
                            itemEtapa.EtapaContactado = etapaCalificada.EsContactado==true?true:false;
                            itemEtapa.EsCalificadoPorPostulante = etapaCalificada.EsCalificadoPorPostulante == true ? true : false;
                        }
                        obj.Etapas.Add(itemEtapa);
                    }
                    listaEtapasFinal.Add(obj);
                }
                if (listaEtapasFinal.Count > 0)
                {
                    listaEtapasFinal = listaEtapasFinal.OrderBy(x => x.Postulante).ToList();
                }
                return Ok(new { DatosAgrupado = datosAgrupado, Postulantes = postulantes, Estado = true, datosEtapaAprobada = listaEtapasFinal, Cantidad = listaEtapasFinal.Count });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Jashin Salazar
        /// Fecha: 03/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene evaluaciones de los cursos temporales.
        /// </summary>
        /// <returns> Lista de Objeto DTO : List<EvaluacionesAsignadasEvaluador> </returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerEvaluacionesPortalPostulante([FromBody] ReportePostulanteDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaEvaluacionEvaluador = _repExamenAsignadoEvaluador.ObtenerEvaluacionesPortalPostulante(Filtro);
                return Ok(listaEvaluacionEvaluador);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Luis H, Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene evaluaciones asignadas al evaluador de postulantes por filtro
        /// </summary>
        /// <returns> evaluaciones asignadas al evaluador de postulantes por filtro </returns>
        /// <returns> Lista de Objeto DTO : List<EvaluacionesAsignadasEvaluador> </returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerEvaluacionesAsignadasEvaluador([FromBody]FiltroEvaluacionEvaluador Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaEvaluacionEvaluador = _repExamenAsignadoEvaluador.ObtenerListaEvaluacionEvaluador(Filtro);
                return Ok(listaEvaluacionEvaluador);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Luis H, Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene etapa de postulante por Filtro
        /// </summary>
        /// <returns> Lista de etapas de postulantes por Filtro </returns>
        /// <returns> Lista de objeto DTO : List<ReportePruebaDTO> </returns>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult ObtenerEtapasPostulante([FromBody] ReportePostulanteDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var postulanteProceso = _repPostulante.ObtenerPostulantesUltimoProcesoSeleccion(Filtro);


                if (Filtro.Check == false)
                {

                    List<int> listapostulante = new List<int>();

                    if (Filtro.ListaEstadoProceso.Count == 0 && Filtro.ListaEtapaProceso.Count == 0)
                    {
                        if (Filtro.IdGrupoComparacion != null && Filtro.IdGrupoComparacion > 0)
                        {
                            listapostulante = repExamenAsignado.GetBy(x => x.IdProcesoSeleccion == Filtro.ListaProcesoSeleccion).Select(x => x.IdPostulante).Distinct().ToList();
                            Filtro.ListaProcesoSeleccion = null;
                            Filtro.ListaPostulante = listapostulante;
                        }
                        Filtro.ListaEtapaProceso = new List<int>();
                        Filtro.ListaEstadoProceso = new List<int>();
                    }
                    if (Filtro.ListaEstadoProceso.Count > 0 && Filtro.ListaEtapaProceso.Count > 0)
                    {
                        listapostulante = _repEtapaProcesoSeleccionCalificado.GetBy(x => Filtro.ListaEtapaProceso.Contains(x.IdProcesoSeleccionEtapa) && Filtro.ListaEstadoProceso.Contains(x.IdEstadoEtapaProcesoSeleccion.Value)).Select(x => x.IdPostulante).Distinct().ToList();
                        if (listapostulante.Count == 0)
                        {
                            listapostulante.Add(0);
                        }
                        Filtro.ListaPostulante = listapostulante;
                        Filtro.ListaEtapaProceso = new List<int>();
                        Filtro.ListaEstadoProceso = new List<int>();
                        Filtro.ListaProcesoSeleccion = null;

                        postulanteProceso = postulanteProceso.Where(x => listapostulante.Contains(x.IdPostulante)).ToList();
                    }
                    if (Filtro.ListaEstadoProceso.Count > 0 && Filtro.ListaEtapaProceso.Count == 0)
                    {
                        listapostulante = _repEtapaProcesoSeleccionCalificado.GetBy(x => Filtro.ListaEstadoProceso.Contains(x.IdEstadoEtapaProcesoSeleccion.Value)).Select(x => x.IdPostulante).Distinct().ToList();
                        if (listapostulante.Count == 0)
                        {
                            listapostulante.Add(0);
                        }
                        Filtro.ListaPostulante = listapostulante;
                        Filtro.ListaEtapaProceso = new List<int>();
                        Filtro.ListaEstadoProceso = new List<int>();
                        Filtro.ListaProcesoSeleccion = null;

                        postulanteProceso = postulanteProceso.Where(x => listapostulante.Contains(x.IdPostulante)).ToList();
                    }
                    if (Filtro.ListaEstadoProceso.Count == 0 && Filtro.ListaEtapaProceso.Count > 0)
                    {
                        listapostulante = _repEtapaProcesoSeleccionCalificado.GetBy(x => Filtro.ListaEtapaProceso.Contains(x.IdProcesoSeleccionEtapa)).Select(x => x.IdPostulante).Distinct().ToList();
                        if (listapostulante.Count == 0)
                        {
                            listapostulante.Add(0);
                        }
                        Filtro.ListaPostulante = listapostulante;
                        Filtro.ListaEtapaProceso = new List<int>();
                        Filtro.ListaEstadoProceso = new List<int>();
                        Filtro.ListaProcesoSeleccion = null;

                        postulanteProceso = postulanteProceso.Where(x => listapostulante.Contains(x.IdPostulante)).ToList();
                    }
                    if (Filtro.FechaInicio == null || Filtro.FechaFin == null)
                    {
                        Filtro.FechaFin = DateTime.Now;
                        Filtro.FechaInicio = new DateTime(1900, 12, 31);
                    }

                }
                else
                {
                    Filtro.FechaFin = DateTime.Now;
                    Filtro.FechaInicio = new DateTime(1900, 12, 31);
                }

                var ListaProcesoSeleccion = (from p in postulanteProceso
                                             group p by new { p.IdProcesoSeleccion, p.ProcesoSeleccion } into grupo
                                             select new { IdProcesoSeleccion = grupo.Key.IdProcesoSeleccion, ProcesoSeleccion = grupo.Key.ProcesoSeleccion }).ToList();


                List<ReportePruebaDetalleDTO> listaEtapas = new List<ReportePruebaDetalleDTO>();
                List<ReportePruebaDTO> listaEtapasFinal = new List<ReportePruebaDTO>();

                foreach (var item in ListaProcesoSeleccion)
                {
                    listaEtapas = listaEtapas.Concat(_repEtapaRep.GetBy(x => x.IdProcesoSeleccion == item.IdProcesoSeleccion).Select(x => new ReportePruebaDetalleDTO { IdProcesoSeleccion = item.IdProcesoSeleccion, ProcesoSeleccion = item.ProcesoSeleccion, IdEtapa = x.Id, Etapa = x.Nombre, EstadoEtapa = 0, IdEstadoEtapaProceso = 9}).ToList()).ToList();
                }

                foreach (var item in postulanteProceso)
                {
                    List<ReportePruebaDetalleDTO> etapasList = new List<ReportePruebaDetalleDTO>();
                    etapasList = listaEtapas.Where(x => x.IdProcesoSeleccion == item.IdProcesoSeleccion).ToList();
                    ReportePruebaDTO obj = new ReportePruebaDTO();
                    obj.IdPostulante = item.IdPostulante;
                    obj.Postulante = item.Postulante;
                    obj.Etapas = new List<ReportePruebaDetalleDTO>();

                    foreach (var item2 in etapasList)
                    {
                        ReportePruebaDetalleDTO itemEtapa = new ReportePruebaDetalleDTO();
                        if (item2.IdEtapa == 221)
                        {
                            var c = 12;
                        }
                        var etapaCalificada = _repEtapaCalificacionRep.FirstBy(x => x.IdPostulante == item.IdPostulante && x.IdProcesoSeleccionEtapa == item2.IdEtapa);
                        itemEtapa.IdEtapa = item2.IdEtapa;
                        itemEtapa.Etapa = item2.Etapa;
                        itemEtapa.IdProcesoSeleccion = item2.IdProcesoSeleccion;
                        itemEtapa.ProcesoSeleccion = item2.ProcesoSeleccion;
                        itemEtapa.EstadoEtapa = item2.EstadoEtapa;
                        itemEtapa.IdEstadoEtapaProceso = item2.IdEstadoEtapaProceso;

                        if (etapaCalificada != null)
                        {
                            itemEtapa.EstadoEtapa = etapaCalificada.EsEtapaAprobada == true ? 1 : 0;
                            itemEtapa.IdEstadoEtapaProceso = etapaCalificada.IdEstadoEtapaProcesoSeleccion.Value;
                        }
                        obj.Etapas.Add(itemEtapa);
                    }

                    listaEtapasFinal.Add(obj);

                }

                return Ok(listaEtapasFinal);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 02/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Estados por etapa de Postulante
        /// </summary>
        /// <returns> Reporte de Estados, etapas y puntaje de postulantes </returns>
        /// <returns>   Objeto Agrupado  </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteIntegra([FromBody] ReportePostulanteDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var postulanteProceso = _repPostulante.ObtenerPostulantesUltimoProcesoSeleccion(Filtro);
                if (postulanteProceso == null || postulanteProceso.Count == 0)
                {
                    return Ok(new { Estado = false });
                }

                if (Filtro.Check == false)
                {

                    List<int> listapostulante = new List<int>();

                    if (Filtro.ListaEstadoProceso.Count == 0 && Filtro.ListaEtapaProceso.Count == 0)
                    {
                        if (Filtro.IdGrupoComparacion != null && Filtro.IdGrupoComparacion > 0)
                        {
                            listapostulante = repExamenAsignado.GetBy(x => x.IdProcesoSeleccion == Filtro.ListaProcesoSeleccion).Select(x => x.IdPostulante).Distinct().ToList();
                            Filtro.ListaProcesoSeleccion = null;
                            Filtro.ListaPostulante = listapostulante;
                        }
                        Filtro.ListaEtapaProceso = new List<int>();
                        Filtro.ListaEstadoProceso = new List<int>();
                    }
                    if (Filtro.ListaEstadoProceso.Count > 0 && Filtro.ListaEtapaProceso.Count > 0)
                    {
                        string listaEstadoProceso = "";
                        for (var i = 0; Filtro.ListaEstadoProceso.Count > i; i++)
                        {
                            if (i == 0)
                            {
                                listaEstadoProceso = Filtro.ListaEstadoProceso[i] + "";
                            }
                            else
                            {
                                listaEstadoProceso = listaEstadoProceso + "," + Filtro.ListaEstadoProceso[i];
                            }
                        }

                        string listaEtapaProceso = "";
                        for (var i = 0; Filtro.ListaEtapaProceso.Count > i; i++)
                        {
                            if (i == 0)
                            {
                                listaEtapaProceso = Filtro.ListaEtapaProceso[i] + "";
                            }
                            else
                            {
                                listaEtapaProceso = listaEtapaProceso + "," + Filtro.ListaEtapaProceso[i];
                            }
                        }

                        string listaPostulantes = "";
                        for (var i = 0; postulanteProceso.Count > i; i++)
                        {
                            if (i == 0)
                            {
                                listaPostulantes = postulanteProceso[i].IdPostulante + "";
                            }
                            else
                            {
                                listaPostulantes = listaPostulantes + "," + postulanteProceso[i].IdPostulante;
                            }
                        }

                        string filtroPostulante = "AND IdEstadoEtapaProcesoSeleccion in(" + listaEstadoProceso + ") AND IdProcesoSeleccionEtapa IN (" + listaEtapaProceso + ")";
                        var listapostulante_v2 = _repEtapaProcesoSeleccionCalificado.ObtenerEtapasProcesoSeleccionActual(listaPostulantes, filtroPostulante);

                        if (listapostulante_v2.Count == 0)
                        {
                            return Ok(new { Estado = false });
                        }

                        listapostulante = listapostulante_v2.Select(x => x.IdPostulante).Distinct().ToList();

                        if (listapostulante.Count == 0)
                        {
                            listapostulante.Add(0);
                        }
                        Filtro.ListaPostulante = listapostulante;
                        Filtro.ListaEtapaProceso = new List<int>();
                        Filtro.ListaEstadoProceso = new List<int>();
                        Filtro.ListaProcesoSeleccion = null;

                        postulanteProceso = postulanteProceso.Where(x => listapostulante.Contains(x.IdPostulante)).ToList();
                    }
                    if (Filtro.ListaEstadoProceso.Count > 0 && Filtro.ListaEtapaProceso.Count == 0)
                    {
                        string listaEstadoProceso = "";
                        for (var i = 0; Filtro.ListaEstadoProceso.Count > i; i++)
                        {
                            if (i == 0)
                            {
                                listaEstadoProceso = Filtro.ListaEstadoProceso[i] + "";
                            }
                            else
                            {
                                listaEstadoProceso = listaEstadoProceso + "," + Filtro.ListaEstadoProceso[i];
                            }
                        }

                        string listaPostulantes = "";
                        for (var i = 0; postulanteProceso.Count > i; i++)
                        {
                            if (i == 0)
                            {
                                listaPostulantes = postulanteProceso[i].IdPostulante + "";
                            }
                            else
                            {
                                listaPostulantes = listaPostulantes + "," + postulanteProceso[i].IdPostulante;
                            }
                        }

                        string filtroPostulante = "AND IdEstadoEtapaProcesoSeleccion in(" + listaEstadoProceso + ")";
                        var listapostulante_v2 = _repEtapaProcesoSeleccionCalificado.ObtenerEtapasProcesoSeleccionActual(listaPostulantes, filtroPostulante);

                        if (listapostulante_v2.Count == 0)
                        {
                            return Ok(new { Estado = false });
                        }

                        listapostulante = listapostulante_v2.Select(x => x.IdPostulante).Distinct().ToList();

                        if (listapostulante.Count == 0)
                        {
                            listapostulante.Add(0);
                        }
                        Filtro.ListaPostulante = listapostulante;
                        Filtro.ListaEtapaProceso = new List<int>();
                        Filtro.ListaEstadoProceso = new List<int>();
                        Filtro.ListaProcesoSeleccion = null;

                        postulanteProceso = postulanteProceso.Where(x => listapostulante.Contains(x.IdPostulante)).ToList();
                    }
                    if (Filtro.ListaEstadoProceso.Count == 0 && Filtro.ListaEtapaProceso.Count > 0)
                    {

                        string listaEtapaProceso = "";
                        for (var i = 0; Filtro.ListaEtapaProceso.Count > i; i++)
                        {
                            if (i == 0)
                            {
                                listaEtapaProceso = Filtro.ListaEtapaProceso[i] + "";
                            }
                            else
                            {
                                listaEtapaProceso = listaEtapaProceso + "," + Filtro.ListaEtapaProceso[i];
                            }
                        }

                        string listaPostulantes = "";
                        for (var i = 0; postulanteProceso.Count > i; i++)
                        {
                            if (i == 0)
                            {
                                listaPostulantes = postulanteProceso[i].IdPostulante + "";
                            }
                            else
                            {
                                listaPostulantes = listaPostulantes + "," + postulanteProceso[i].IdPostulante;
                            }
                        }

                        string filtroPostulante = "AND IdProcesoSeleccionEtapa in(" + listaEtapaProceso + ") ";
                        var listapostulante_v2 = _repEtapaProcesoSeleccionCalificado.ObtenerEtapasProcesoSeleccionActual(listaPostulantes, filtroPostulante);

                        if (listapostulante_v2.Count == 0)
                        {
                            return Ok(new { Estado = false });
                        }

                        listapostulante = listapostulante_v2.Select(x => x.IdPostulante).Distinct().ToList();

                        if (listapostulante.Count == 0)
                        {
                            listapostulante.Add(0);
                        }
                        Filtro.ListaPostulante = listapostulante;
                        Filtro.ListaEtapaProceso = new List<int>();
                        Filtro.ListaEstadoProceso = new List<int>();
                        Filtro.ListaProcesoSeleccion = null;

                        postulanteProceso = postulanteProceso.Where(x => listapostulante.Contains(x.IdPostulante)).ToList();
                    }
                    if (Filtro.FechaInicio == null || Filtro.FechaFin == null)
                    {
                        Filtro.FechaFin = DateTime.Now;
                        Filtro.FechaInicio = new DateTime(1900, 12, 31);
                    }

                }
                else
                {
                    Filtro.FechaFin = DateTime.Now;
                    Filtro.FechaInicio = new DateTime(1900, 12, 31);
                }

                Filtro.ListaPostulante = postulanteProceso.Select(x => x.IdPostulante).ToList();

                List<int> listaPostulanteComparacion = new List<int>();
                if (Filtro.IdGrupoComparacion != null && Filtro.IdGrupoComparacion != 0)
                {
                    listaPostulanteComparacion = _repPostulanteComparacionRep.GetBy(x => x.Estado == true && x.IdGrupoComparacionProcesoSeleccion == Filtro.IdGrupoComparacion).Select(x => x.IdPostulante.Value).ToList();
                    foreach (var item in listaPostulanteComparacion)
                    {
                        Filtro.ListaPostulante.Add(item);
                    }
                }
                ReporteExamenProcesoSeleccionBO reporteExamenProcesoSeleccion = new ReporteExamenProcesoSeleccionBO(_integraDBContext);

                // de la lita de postulantes que hayan pasado el filtro se agrupan por proceso de seleccion los postulantes.
                var listaProcesoSeleccionAgrupado = (from p in postulanteProceso
                                                     group p by new { p.IdProcesoSeleccion, p.ProcesoSeleccion } into grupo
                                                     select new { IdProcesoSeleccion = grupo.Key.IdProcesoSeleccion, ProcesoSeleccion = grupo.Key.ProcesoSeleccion }).ToList();

                List<ReportePruebaDetalleDTO> listaEtapas = new List<ReportePruebaDetalleDTO>();
                List<ReportePruebaDTO> listaEtapasFinal = new List<ReportePruebaDTO>();

                //Se busca todas las etapas segun el proceso de seleccion y se coloca por defecto el estado SIN RENDIR ID 9
                foreach (var item in listaProcesoSeleccionAgrupado)
                {
                    listaEtapas = listaEtapas.Concat(_repEtapaRep.GetBy(x => x.IdProcesoSeleccion == item.IdProcesoSeleccion).OrderBy(x => x.NroOrden).Select(x => new ReportePruebaDetalleDTO { IdProcesoSeleccion = item.IdProcesoSeleccion, ProcesoSeleccion = item.ProcesoSeleccion, IdEtapa = x.Id, Etapa = x.Nombre, EstadoEtapa = 0, IdEstadoEtapaProceso = 9, NroOrden = x.NroOrden, EtapaContactado = false }).ToList()).ToList();
                }


                List<ObtenerPostulantesProcesoSeleccionDTO> listaEtapasOptimizacion = new List<ObtenerPostulantesProcesoSeleccionDTO>();
                if (Filtro.ListaProcesoSeleccion != null)//[…]
                {
                    var listaPostulantesPrueba = "";
                    for (var i = 0; postulanteProceso.Count > i; i++)
                    {
                        if (i == 0)
                        {
                            listaPostulantesPrueba = postulanteProceso[i].IdPostulante + "";
                        }
                        else
                        {
                            listaPostulantesPrueba = listaPostulantesPrueba + "," + postulanteProceso[i].IdPostulante;
                        }
                    }
                    var obtenerProcesoSeleccion = Filtro.ListaProcesoSeleccion.GetValueOrDefault().ToString();
                    if (listaPostulantesPrueba == "")
                    {
                        return Ok(new { Estado = false });
                    }
                    else
                    {
                        listaEtapasOptimizacion = _repEtapaCalificacionRep.ObtenerTodosLosPostulantes(listaPostulantesPrueba, obtenerProcesoSeleccion);
                    }
                }
                else
                {
                    var listaPostulantesPrueba = "";
                    List<string> listaProcesoSeleccion = new List<string>();
                    for (var i = 0; postulanteProceso.Count > i; i++)
                    {
                        if (i == 0)
                        {
                            listaPostulantesPrueba = Filtro.ListaPostulante[i] + "";
                        }
                        else
                        {
                            listaPostulantesPrueba = listaPostulantesPrueba + "," + Filtro.ListaPostulante[i];
                        }
                    }
                    var listaProceso = "";

                    for (var i = 0; listaProcesoSeleccionAgrupado.Count > i; i++)
                    {
                        if (i == 0)
                        {
                            listaProceso = listaProcesoSeleccionAgrupado[i].IdProcesoSeleccion + "";
                        }
                        else
                        {
                            listaProceso = listaProceso + "," + listaProcesoSeleccionAgrupado[i].IdProcesoSeleccion;
                        }
                    }
                    listaEtapasOptimizacion = _repEtapaCalificacionRep.ObtenerTodosLosPostulantes(listaPostulantesPrueba, listaProceso);
                }


                List<ReportePruebaDetalleDTO> etapasList;
                ReportePruebaDetalleDTO itemEtapa;
                //Se recorre la lista de los postulantes que cumplen el filtro y cada postulante se le asigna sus etapas correspondientes segun el proceso de seleccion que se encuentre
                foreach (var item in postulanteProceso)
                {
                    etapasList = new List<ReportePruebaDetalleDTO>();
                    etapasList = listaEtapas.Where(x => x.IdProcesoSeleccion == item.IdProcesoSeleccion).OrderBy(x => x.NroOrden).ToList();
                    ReportePruebaDTO obj = new ReportePruebaDTO();
                    obj.IdPostulante = item.IdPostulante;
                    obj.Postulante = item.Postulante;
                    obj.Etapas = new List<ReportePruebaDetalleDTO>();

                    foreach (var item2 in etapasList)
                    {
                        itemEtapa = new ReportePruebaDetalleDTO();
                        var etapaCalificada = listaEtapasOptimizacion.Where(x => x.IdPostulante == item.IdPostulante && x.IdProcesoSeleccionEtapa == item2.IdEtapa).FirstOrDefault();

                        itemEtapa.IdEtapa = item2.IdEtapa;
                        itemEtapa.Etapa = item2.Etapa;
                        itemEtapa.IdProcesoSeleccion = item2.IdProcesoSeleccion;
                        itemEtapa.ProcesoSeleccion = item2.ProcesoSeleccion;
                        itemEtapa.EstadoEtapa = item2.EstadoEtapa;
                        itemEtapa.IdEstadoEtapaProceso = item2.IdEstadoEtapaProceso;
                        itemEtapa.NroOrden = item2.NroOrden;
                        itemEtapa.EtapaContactado = item2.EtapaContactado;
                        itemEtapa.EsCalificadoPorPostulante = item2.EsCalificadoPorPostulante;

                        if (etapaCalificada != null) // Si tiene una calificacion en la tabla gp.T_EtapaPRocesoSeleccionCalificado reemplaza lo datos, en caso no exista coloca los datos por defecto
                        {
                            itemEtapa.EstadoEtapa = etapaCalificada.EsEtapaAprobada == true ? 1 : 0;
                            itemEtapa.IdEstadoEtapaProceso = etapaCalificada.IdEstadoEtapaProcesoSeleccion;
                            itemEtapa.EtapaContactado = etapaCalificada.EsContactado == true ? true : false;
                            itemEtapa.EsCalificadoPorPostulante = etapaCalificada.EsCalificadoPorPostulante == true ? true : false;
                        }
                        obj.Etapas.Add(itemEtapa);
                    }
                    listaEtapasFinal.Add(obj);
                }
                if (listaEtapasFinal.Count > 0)
                {
                    listaEtapasFinal = listaEtapasFinal.OrderBy(x => x.Postulante).ToList();
                }
                return Ok(new { Estado = true, datosEtapaAprobada = listaEtapasFinal, Cantidad = listaEtapasFinal.Count });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 25/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene tipo de examen y Id de Examen
        /// </summary>
        /// <returns> TipoEvaluacionDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public TipoEvaluacionDTO ObtenerTipoExamen([FromBody] ObtenerTipoExamenDTO Filtro)
        {
            TipoEvaluacionDTO resultado = new TipoEvaluacionDTO();
            var configuracion = _repAsignacionEvaluacion.GetBy(x => x.IdProcesoSeleccion == Filtro.IdProcesoSeleccion && x.IdProcesoSeleccionEtapa == Filtro.IdEtapa).FirstOrDefault();
            if (configuracion == null)
            {
                var casoFiltro = _repEtapaCalificacionRep.GetBy(x=>x.IdPostulante == Filtro.IdPostulante && x.IdProcesoSeleccionEtapa == Filtro.IdEtapa).OrderByDescending(x=>x.Id).FirstOrDefault();
                if (casoFiltro != null)
                {
                    resultado.TipoEvaluacion = 1;
                    resultado.IdEvaluacion = null;
                    return resultado;
                }
                else
                {
                    resultado.TipoEvaluacion = 3;
                    resultado.IdEvaluacion = null;
                    return resultado;
                }
            }
            else
            {
                var examenTest = _repExamenTest.GetBy(x => x.Id == configuracion.IdEvaluacion).FirstOrDefault();
                if (examenTest != null)
                {
                    if (examenTest.EsCalificadoPorPostulante == true)
                    {
                        resultado.TipoEvaluacion = 1;
                        resultado.IdEvaluacion = examenTest.Id;
                        return resultado;
                    }
                    else
                    {
                        resultado.TipoEvaluacion = 2;
                        resultado.IdEvaluacion = examenTest.Id;
                        return resultado;
                    }
                }
                else
                {
                    resultado.TipoEvaluacion = 3;
                    resultado.IdEvaluacion = null;
                    return resultado;
                }
            }
        }


        /// TipoFuncion: POST
		/// Autor: Edgar S.
		/// Fecha: 25/03/2021
		/// Versión: 2.0
		/// <summary>
		/// Guarda respuestas realizadas por evaluador y califica etapa de evaluación
		/// </summary>
		/// <returns> Confirmación de inserción : Bool </returns>
		[HttpPost]
        [Route("[action]")]
        public ActionResult ActualizacionManualEtapaExamenAsignado([FromBody] CalificacionManualDTO RespuestaTest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionAsignacionEvaluacionRepositorio _repConfiguracionAsignacionEvaluacion = new ConfiguracionAsignacionEvaluacionRepositorio(_integraDBContext);
                EtapaProcesoSeleccionCalificadoRepositorio _repEtapaProcesoSeleccionCalificado = new EtapaProcesoSeleccionCalificadoRepositorio(_integraDBContext);
                ExamenRepositorio _repExamen = new ExamenRepositorio(_integraDBContext);
                var etapaAnterior = _repEtapaProcesoSeleccionCalificado.GetBy(x => x.IdPostulante == RespuestaTest.IdPostulanteEA && x.EsEtapaActual == true).OrderByDescending(x => x.Id).FirstOrDefault();
                if (etapaAnterior != null)
                {
                    etapaAnterior.EsEtapaActual = false;
                    etapaAnterior.UsuarioModificacion = RespuestaTest.Usuario;
                    etapaAnterior.FechaModificacion = DateTime.Now;
                    _repEtapaProcesoSeleccionCalificado.Update(etapaAnterior);
                }

                var idEtapaCalificadaActual = 0;
                var etapaCalificada = _repEtapaProcesoSeleccionCalificado.GetBy(x => x.IdPostulante == RespuestaTest.IdPostulanteEA && x.IdProcesoSeleccionEtapa == RespuestaTest.IdProcesoSeleccionEtapaEA).FirstOrDefault();
                if (etapaCalificada != null)
                {
                     idEtapaCalificadaActual = etapaCalificada.Id;
                    if (RespuestaTest.IdEstadoEA == 2 || RespuestaTest.IdEstadoEA == 3 || RespuestaTest.IdEstadoEA == 4 || RespuestaTest.IdEstadoEA == 9)
                    {
                        etapaCalificada.EsEtapaAprobada = false;
                    }
                    else
                    {
                        etapaCalificada.EsEtapaAprobada = true;
                    }

                    if (RespuestaTest.IdEstadoEA == 9) /// Estado de Etapa Sin rendir
                    {
                        etapaCalificada.EsEtapaActual = false;
                        etapaCalificada.EsContactado = false;                        
                    }
                    else
                    {
                        etapaCalificada.EsEtapaActual = true;
                        etapaCalificada.EsContactado = true;
                    }

                    etapaCalificada.IdEstadoEtapaProcesoSeleccion = RespuestaTest.IdEstadoEA;
                    etapaCalificada.UsuarioModificacion = RespuestaTest.Usuario;
                    etapaCalificada.FechaModificacion = DateTime.Now;

                    using (TransactionScope scope = new TransactionScope())
                    {
                        _repEtapaProcesoSeleccionCalificado.Update(etapaCalificada);
                        scope.Complete();
                    }


                    if (RespuestaTest.IdEstadoEA == 7)// Aprobado con Observaciones
                    {
                        //Obtenemos los examenes rendidos por el proceso y el postulante y luego obtenemos solos los que rinde el postulante
                        var validacionPaseAEvaluador = _repEtapaProcesoSeleccionCalificado.ObtenerListaEtapaExamenesPorPostulante(RespuestaTest.IdProcesoSeleccionEA, RespuestaTest.IdPostulanteEA);
                        var validacionExamenesPostulante = validacionPaseAEvaluador.Where(x => x.EsCalificadoPorPostulante == true).ToList();

                        //Validamos que el resto de examenes sean aprobados
                        bool banderaTodoPostulanteAprobado = true;
                        foreach (var item in validacionExamenesPostulante)
                        {
                            if (!item.EsEtapaAprobada) banderaTodoPostulanteAprobado = false;
                        }

                        //Si todas las etapas estan aprobadas, se coloca el contactado como si
                        if (banderaTodoPostulanteAprobado)
                        {
                            //Actualizamos el campo ES CONTACTADO del resto de examenes de postulante
                            foreach(var item in validacionExamenesPostulante)
                            {
                                var actualizar = _repEtapaProcesoSeleccionCalificado.GetBy(x => x.Id == item.Id).FirstOrDefault();
                                actualizar.EsContactado = true;
                                actualizar.UsuarioModificacion = RespuestaTest.Usuario;
                                actualizar.FechaModificacion = DateTime.Now;

                                _repEtapaProcesoSeleccionCalificado.Update(actualizar);
                            }

                            //Colocamos "En proceso" la evaluación de evaluador que prosigue
                            var nroEtapaMaximoDePostulante = validacionExamenesPostulante.OrderByDescending(x => x.NroOrden).FirstOrDefault();
                            var siguienteEvaluacionEvaluador = validacionPaseAEvaluador.Where(x => x.NroOrden == nroEtapaMaximoDePostulante.NroOrden + 1).FirstOrDefault();
                            var actualizarEvaluador = _repEtapaProcesoSeleccionCalificado.GetBy(x => x.Id == siguienteEvaluacionEvaluador.Id).FirstOrDefault();
                            if (actualizarEvaluador != null)
                            {
                                actualizarEvaluador.EsEtapaActual = true;
                                actualizarEvaluador.EsContactado = true;
                                actualizarEvaluador.IdEstadoEtapaProcesoSeleccion = 3; // En Proceso
                                actualizarEvaluador.UsuarioModificacion = RespuestaTest.Usuario;
                                actualizarEvaluador.FechaModificacion = DateTime.Now;

                                _repEtapaProcesoSeleccionCalificado.Update(actualizarEvaluador);
                            }
                        }
                    }
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// TipoFuncion: POST
		/// Autor: Edgar S.
		/// Fecha: 25/03/2021
		/// Versión: 1.0
		/// <summary>
		/// Califica etapas según examenes de portal
		/// </summary>
		/// <returns> Confirmación de calificación : Bool </returns>
		[HttpPost]
        [Route("[action]")]
        public ActionResult CalificarExamenPortal([FromBody] int IdExamenAsignado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReporteExamenProcesoSeleccionBO reporte = new ReporteExamenProcesoSeleccionBO(_integraDBContext);
                var examenCantidadConfigurada = _repEtapaProcesoSeleccionCalificado.ObtenerExamenesAsociados(IdExamenAsignado);
                var examenResueltaPostulante = _repEtapaProcesoSeleccionCalificado.ObtenerExamenesResueltos(IdExamenAsignado);
                if(examenCantidadConfigurada.Cantidad == examenResueltaPostulante.Cantidad)
                {
                    List<OrdenEtapaTipoDTO> ordenEtapasTipoCalificacion = new List<OrdenEtapaTipoDTO>();
                    OrdenEtapaTipoDTO ordenActual = new OrdenEtapaTipoDTO();
                    var calificacion = reporte.CalificarAutomaticamenteNuevaVersion(examenResueltaPostulante);

                    if (calificacion.Agrupado != null || calificacion.Detalle != null)
                    {
                        if (calificacion.Agrupado.Count > 0 || calificacion.Detalle.Count > 0)
                        {
                            //Orden de etapas y tipo de calificación para colocar etapa en proceso
                            if (calificacion.Agrupado.Count > 0 && calificacion.Agrupado[0].IdEtapa != null)
                            {
                                ordenEtapasTipoCalificacion = _repEtapaProcesoSeleccionCalificado.ObtenerOrdenEtapas(calificacion.Agrupado[0].IdProcesoSeleccion);
                                ordenActual = ordenEtapasTipoCalificacion.Where(x => x.Id == calificacion.Agrupado[0].IdEtapa).FirstOrDefault();

                            }
                            else if (calificacion.Detalle.Count > 0 && calificacion.Detalle[0].IdEtapa != null)
                            {
                                ordenEtapasTipoCalificacion = _repEtapaProcesoSeleccionCalificado.ObtenerOrdenEtapas(calificacion.Agrupado[0].IdProcesoSeleccion);
                                ordenActual = ordenEtapasTipoCalificacion.Where(x => x.Id == calificacion.Detalle[0].IdEtapa).FirstOrDefault();
                            }

                            bool esAprobado = false;
                            var idEstadoEtapa = 2;
                            var etapaProceso = calificacion.Agrupado[0].IdEtapa;

                            if (calificacion.Agrupado[0].IdEvaluacion == 55) //NEOPIR
                            {
                                bool franquesaAprobado = false;
                                bool neuroticismoAprobado = false;
                                bool responsabilidadAprobado = false;
                                var franquesa = calificacion.Detalle.Where(x => x.IdExamen == 80).FirstOrDefault();
                                if (franquesa != null)
                                {
                                    CultureInfo cultura = new CultureInfo("en-US");
                                    decimal puntaje = decimal.Parse(franquesa.Registro, cultura);
                                    if (puntaje > 30) franquesaAprobado = true;
                                }
                                var neuroticismo = calificacion.Agrupado.Where(x => x.IdGrupo == 3).FirstOrDefault();
                                if (neuroticismo.EsAprobado == true) neuroticismoAprobado = true;

                                var responsabilidad = calificacion.Agrupado.Where(x => x.IdGrupo == 7).FirstOrDefault();
                                if (neuroticismo.EsAprobado == true) responsabilidadAprobado = true;

                                if (franquesaAprobado == true && neuroticismoAprobado == true && responsabilidadAprobado == true)
                                {
                                    esAprobado = true;
                                    idEstadoEtapa = 1;
                                }
                            }
                            else if (calificacion.Agrupado[0].IdEvaluacion == 52)// ISRA
                            {
                                bool factor2Aprobado = false;
                                bool factor4Aprobado = false;
                                var factorF2 = calificacion.Agrupado.Where(x => x.IdExamen == 47).FirstOrDefault();// F2
                                if (factorF2.EsAprobado == true) factor2Aprobado = true;

                                var factor4 = calificacion.Agrupado.Where(x => x.IdExamen == 49).FirstOrDefault();// F4
                                if (factor4.EsAprobado == true) factor4Aprobado = true;

                                if (factor2Aprobado == true && factor4Aprobado == true)
                                {
                                    esAprobado = true;
                                    idEstadoEtapa = 1;
                                }
                            }
                            else if (calificacion.Agrupado[0].IdEvaluacion == 54)// Optimismo
                            {
                                esAprobado = true;
                                idEstadoEtapa = 1;
                            }
                            else if (calificacion.Agrupado[0].IdEvaluacion == 53)// Psicotécnico
                            {
                                bool comprensionAprobado = false;
                                bool razonamientoAprobado = false;
                                var comprension = calificacion.Agrupado.Where(x => x.IdExamen == 51).FirstOrDefault();// Comprensión de Lectura
                                if (comprension.EsAprobado == true) comprensionAprobado = true;

                                var razonamiento = calificacion.Agrupado.Where(x => x.IdExamen == 54).FirstOrDefault();// Razonamiento crítico
                                if (razonamiento.EsAprobado == true) razonamientoAprobado = true;

                                if (comprensionAprobado == true && razonamientoAprobado == true)
                                {
                                    esAprobado = true;
                                    idEstadoEtapa = 1;
                                }
                            }
                            else if (calificacion.Agrupado[0].IdEvaluacion == 57)// Aptitudes
                            {
                                bool grupo1PMAAprobado = false;
                                var grupo1PMA = calificacion.Agrupado.Where(x => x.IdGrupo == 11).FirstOrDefault();// Grupo 1 PMA
                                if (grupo1PMA.EsAprobado == true) grupo1PMAAprobado = true;
                                if (grupo1PMAAprobado == true)
                                {
                                    esAprobado = true;
                                    idEstadoEtapa = 1;
                                }
                            }
                            else
                            {
                                if (calificacion.Agrupado.Count > 0)
                                {
                                    var detalleListaDesaprobado = calificacion.Agrupado.Where(x => x.EsAprobado == false && x.NotaAprobatoria != "N.A").Count();
                                    var detalleListaAprobado = calificacion.Agrupado.Where(x => x.EsAprobado == true && x.NotaAprobatoria != "N.A").Count();
                                    if (detalleListaAprobado > detalleListaDesaprobado)
                                    {
                                        esAprobado = true;
                                        idEstadoEtapa = 1;
                                    }
                                }
                                else if (esAprobado == false && calificacion.Detalle != null && calificacion.Detalle.Count > 0)
                                {
                                    var detalleListaDesaprobado = calificacion.Detalle.Where(x => x.EsAprobado == false && x.NotaAprobatoria != "N.A").Count();
                                    var detalleListaAprobado = calificacion.Detalle.Where(x => x.EsAprobado == true && x.NotaAprobatoria != "N.A").Count();
                                    if (detalleListaAprobado > detalleListaDesaprobado)
                                    {
                                        esAprobado = true;
                                        idEstadoEtapa = 1;
                                    }
                                }
                            }

                            //Validamos el campo contactado
                            bool validacionEsContactadoAnterior = false;
                            bool validacionEsEtapaAprobadaAnterior = false;
                            OrdenEtapaTipoDTO ordenAnterior = new OrdenEtapaTipoDTO();
                            ordenAnterior = ordenEtapasTipoCalificacion.Where(x => x.NroOrden == (ordenActual.NroOrden - 1)).FirstOrDefault();
                            if (ordenAnterior != null)
                            {
                                var estadoEtapaCalificacionAnterior = _repEtapaCalificacionRep.GetBy(x => x.IdPostulante == examenResueltaPostulante.IdPostulante && x.IdProcesoSeleccionEtapa == ordenAnterior.Id).OrderByDescending(x => x.Id).FirstOrDefault();
                                if (estadoEtapaCalificacionAnterior != null && estadoEtapaCalificacionAnterior.EsContactado != null)
                                {
                                    //Asignamos valores a variables para verificar si la anterior etapa fue aprobada y contactada, actualizamos su campo de EsEtapaActual a 0
                                    validacionEsEtapaAprobadaAnterior = estadoEtapaCalificacionAnterior.EsEtapaAprobada;
                                    validacionEsContactadoAnterior = estadoEtapaCalificacionAnterior.EsContactado.GetValueOrDefault();
                                    estadoEtapaCalificacionAnterior.EsEtapaActual = false;
                                    _repEtapaCalificacionRep.Update(estadoEtapaCalificacionAnterior);
                                }
                            }
                            else
                            {
                                validacionEsEtapaAprobadaAnterior = true;
                                validacionEsContactadoAnterior = true;
                            }

                            //Validacion de etapa anterior, es aprobado y si tiene el valor contactado 
                            if (validacionEsEtapaAprobadaAnterior && validacionEsContactadoAnterior)
                            {
                                //Si la etapa anterior a sido aprobada y tiene el campo contactado como SI, hacemos el update del Contactado en SI de le etapa actual
                                var estadoEtapaCalificacion = _repEtapaCalificacionRep.GetBy(x => x.IdPostulante == examenResueltaPostulante.IdPostulante && x.IdProcesoSeleccionEtapa == etapaProceso).OrderByDescending(x => x.Id).FirstOrDefault();
                                if (estadoEtapaCalificacion != null)
                                {
                                    estadoEtapaCalificacion.EsEtapaActual = true;
                                    estadoEtapaCalificacion.EsEtapaAprobada = esAprobado;
                                    estadoEtapaCalificacion.IdEstadoEtapaProcesoSeleccion = idEstadoEtapa;
                                    estadoEtapaCalificacion.EsContactado = true;
                                    estadoEtapaCalificacion.UsuarioModificacion = "SYSTEM";
                                    estadoEtapaCalificacion.FechaModificacion = DateTime.Now;
                                    _repEtapaCalificacionRep.Update(estadoEtapaCalificacion);
                                }
                            }
                            else
                            {
                                //Si no entonces colocamo el estado de la etapa, pero su campo de contactado queda como NO
                                var estadoEtapaCalificacion = _repEtapaCalificacionRep.GetBy(x => x.IdPostulante == examenResueltaPostulante.IdPostulante && x.IdProcesoSeleccionEtapa == etapaProceso).OrderByDescending(x => x.Id).FirstOrDefault();
                                if (estadoEtapaCalificacion != null)
                                {
                                    estadoEtapaCalificacion.EsEtapaActual = true;
                                    estadoEtapaCalificacion.EsEtapaAprobada = esAprobado;
                                    estadoEtapaCalificacion.IdEstadoEtapaProcesoSeleccion = idEstadoEtapa;
                                    estadoEtapaCalificacion.EsContactado = false;
                                    estadoEtapaCalificacion.UsuarioModificacion = "SYSTEM";
                                    estadoEtapaCalificacion.FechaModificacion = DateTime.Now;
                                    _repEtapaCalificacionRep.Update(estadoEtapaCalificacion);
                                }
                            }

                            if (esAprobado)
                            {
                                //Obtenemos los examenes rendidos por el proceso y el postulante y luego obtenemos solos los que rinde el postulante
                                var validacionPaseAEvaluador = _repEtapaProcesoSeleccionCalificado.ObtenerListaEtapaExamenesPorPostulante(examenResueltaPostulante.IdProcesoSeleccion, examenResueltaPostulante.IdPostulante);
                                var validacionExamenesPostulante = validacionPaseAEvaluador.Where(x => x.EsCalificadoPorPostulante == true).ToList();

                                //Obtenemos el último examen de etapa que debe rendir el postulante y validamos si es el último examen que debe rendir el postulante
                                bool banderaEsUltimoPostulante = false;
                                var nroEtapaMaximoDePostulante = validacionExamenesPostulante.OrderByDescending(x => x.NroOrden).FirstOrDefault();
                                if (ordenActual.NroOrden == nroEtapaMaximoDePostulante.NroOrden)
                                {
                                    banderaEsUltimoPostulante = true;
                                }

                                //Verificar examenes de postulante para pase a evaluaciones de evaluador tienen que estar completos y aprobados                           
                                if (banderaEsUltimoPostulante)
                                {
                                    //Si es el último examen que debe rendir el postulante entonces se valida si todas las etapas anteriores fueron aprobadas
                                    bool banderaTodoPostulanteAprobado = true;
                                    foreach (var item in validacionExamenesPostulante)
                                    {
                                        if (!item.EsEtapaAprobada) banderaTodoPostulanteAprobado = false;
                                    }

                                    if (banderaTodoPostulanteAprobado)
                                    {
                                        //Si todos los anteriores evaluaciones del postulante fueron aprobadas entonces se pasa a la fase de evaluación de evaluador y se coloca el estado "En Proceso"
                                        OrdenEtapaTipoDTO ordenPosterior = new OrdenEtapaTipoDTO();
                                        ordenPosterior = ordenEtapasTipoCalificacion.Where(x => x.NroOrden == (ordenActual.NroOrden + 1)).FirstOrDefault();
                                        if (ordenPosterior != null)
                                        {
                                            var estadoEtapaCalificacionPosterior = _repEtapaCalificacionRep.GetBy(x => x.IdPostulante == examenResueltaPostulante.IdPostulante && x.IdProcesoSeleccionEtapa == ordenPosterior.Id).OrderByDescending(x => x.Id).FirstOrDefault();
                                            if (estadoEtapaCalificacionPosterior != null && estadoEtapaCalificacionPosterior.IdEstadoEtapaProcesoSeleccion == 9)
                                            {
                                                estadoEtapaCalificacionPosterior.IdEstadoEtapaProcesoSeleccion = 3; //"En Proceso"
                                                estadoEtapaCalificacionPosterior.EsContactado = true;
                                                _repEtapaCalificacionRep.Update(estadoEtapaCalificacionPosterior);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //Si no es la última etapa de exames de postulante entonces hará la actualización del siguiente examen como en proceso
                                    OrdenEtapaTipoDTO ordenPosterior = new OrdenEtapaTipoDTO();
                                    ordenPosterior = ordenEtapasTipoCalificacion.Where(x => x.NroOrden == (ordenActual.NroOrden + 1)).FirstOrDefault();
                                    if (ordenPosterior != null)
                                    {
                                        var estadoEtapaCalificacionPosterior = _repEtapaCalificacionRep.GetBy(x => x.IdPostulante == examenResueltaPostulante.IdPostulante && x.IdProcesoSeleccionEtapa == ordenPosterior.Id).OrderByDescending(x => x.Id).FirstOrDefault();
                                        if (estadoEtapaCalificacionPosterior != null && estadoEtapaCalificacionPosterior.IdEstadoEtapaProcesoSeleccion == 9)
                                        {
                                            if (validacionEsEtapaAprobadaAnterior && validacionEsContactadoAnterior) estadoEtapaCalificacionPosterior.EsContactado = true;
                                            else estadoEtapaCalificacionPosterior.EsContactado = false;
                                            estadoEtapaCalificacionPosterior.IdEstadoEtapaProcesoSeleccion = 3; //"En Proceso"

                                            _repEtapaCalificacionRep.Update(estadoEtapaCalificacionPosterior);
                                        }
                                    }
                                }
                            }
                        }
                    }                        
                    return Ok(true);
                }
                else
                {
                    return Ok(true);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}