using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ConfigurarProcesoSeleccion")]
    public class ConfigurarProcesoSeleccionController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public ConfigurarProcesoSeleccionController()
        {
            _integraDBContext = new integraDBContext();
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosProcesoSeleccion()
        {
            try
            {
                PuestoTrabajoRepositorio PuestoTrabajoRep = new PuestoTrabajoRepositorio(_integraDBContext);
                SedeTrabajoRepositorio SedeTrabajoRep = new SedeTrabajoRepositorio(_integraDBContext);
                CriterioEvaluacionProcesoRepositorio CriterioEvaluacionProcesoRep = new CriterioEvaluacionProcesoRepositorio(_integraDBContext);
                ExamenRepositorio ExamenRep = new ExamenRepositorio(_integraDBContext);
                ProcesoSeleccionRangoRepositorio ProcesoSeleccionRangoRep = new ProcesoSeleccionRangoRepositorio(_integraDBContext);
                ProcesoSeleccionEtapaRepositorio repProcesoSeleccionEtapaRep = new ProcesoSeleccionEtapaRepositorio(_integraDBContext);

                var listaPuestoTrabajo= PuestoTrabajoRep.GetBy(x => x.Estado == true, x => new {Id= x.Id, NomPuestoTrabajo= x.Nombre}).OrderByDescending(w => w.Id).ToList();
                var listaSedeTrabajo= SedeTrabajoRep.GetBy(x => x.Estado == true, x => new { Id=x.Id, NomSedeTrabajo= x.Nombre }).OrderByDescending(w => w.Id).ToList();
                var listaCriterioSeleccion = CriterioEvaluacionProcesoRep.GetBy(x => x.Estado == true, x => new { IdCriterio = x.Id, Nombre = x.Nombre }).ToList();
                var listaExamen = ExamenRep.GetBy(x => x.Estado == true, x => new { Id = x.Id, Nombre = x.Nombre,IdCriterio=x.IdCriterioEvaluacionProceso }).ToList();
                var listaRango= ProcesoSeleccionRangoRep.GetBy(x => x.Estado == true, x => new { Id = x.Id, Nombre = x.Nombre }).ToList();
                
                return Ok(new { listaPuestoTrabajo, listaSedeTrabajo, listaCriterioSeleccion, listaExamen, listaRango });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdProcesoSeleccion}")]
        [HttpPost]
        public ActionResult ObtenerEtapaProcesoSeleccion(int IdProcesoSeleccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                ProcesoSeleccionEtapaRepositorio _repProcesoSeleccionEtapa = new ProcesoSeleccionEtapaRepositorio(_integraDBContext);
                var Postulante = _repProcesoSeleccionEtapa.GetBy(x => x.Estado == true && x.IdProcesoSeleccion == IdProcesoSeleccion, x => new { x.Id, x.Nombre, IdProcesoSeleccion=x.IdProcesoSeleccion,NroOrden=x.NroOrden });
                return Ok(Postulante);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerProcesoSeleccion()
        {
            try
            {
                ProcesoSeleccionRepositorio procesoSeleccionRep = new ProcesoSeleccionRepositorio();

                return Ok(procesoSeleccionRep.ObtenerConfiguracionProcesoSeleccion().OrderByDescending(x=>x.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerExamenes()
        {
            try
            {
                ExamenRepositorio examenRep = new ExamenRepositorio();

                var listaExamen = examenRep.GetBy(x => x.Estado == true, x => new { Id = x.Id, Nombre = x.Nombre }).OrderByDescending(w => w.Id).ToList();

                return Ok(listaExamen);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdProcesoSeleccion}")]
        [HttpGet]
        public ActionResult ObtenerExamenesNoAsociados(int IdProcesoSeleccion)
        {
            try
            {
                ExamenRepositorio examenRep = new ExamenRepositorio();

                var listaExamen = examenRep.ObtenerExamenNoAsignadoProcesoSeleccion(IdProcesoSeleccion);

                return Ok(listaExamen);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdProcesoSeleccion}")]
        [HttpGet]
        public ActionResult ObtenerExamenesAsociados(int IdProcesoSeleccion)
        {
            try
            {
                ExamenRepositorio examenRep = new ExamenRepositorio();

                var listaExamen = examenRep.ObtenerExamenAsignadoProcesoSeleccion(IdProcesoSeleccion);

                return Ok(listaExamen);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarProcesoSeleccionConfiguracion([FromBody] ProcesoSeleccionAgrupadoInsertarModificarDTO Json) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try {
                List<EvaluacionAsignadoProcesoDTO> listaEvaluaciones = Json.listaEvaluaciones.Concat(Json.listaEvaluacionesEvaluador).ToList();

                ProcesoSeleccionRepositorio repProcesoSeleccionRep = new ProcesoSeleccionRepositorio(_integraDBContext);
                ProcesoSeleccionBO procesoSeleccion = repProcesoSeleccionRep.FirstById(Json.ConfiguracionProcesoSeleccion.Id);
                ProcesoSeleccionEtapaRepositorio etapaRep = new ProcesoSeleccionEtapaRepositorio(_integraDBContext);
                ConfiguracionAsignacionExamenRepositorio repConfiguracionAsignacionExamenRep = new ConfiguracionAsignacionExamenRepositorio(_integraDBContext);
                ConfiguracionAsignacionEvaluacionRepositorio repConfiguracionAsignacionEvaluacionRep = new ConfiguracionAsignacionEvaluacionRepositorio(_integraDBContext);
                List<ConfiguracionAsignacionEvaluacionBO> listaEvaluacion = repConfiguracionAsignacionEvaluacionRep.GetBy(x => x.IdProcesoSeleccion == Json.ConfiguracionProcesoSeleccion.Id).ToList();

                List <ConfiguracionAsignacionExamenBO> listaExamen= repConfiguracionAsignacionExamenRep.GetBy(x => x.IdProcesoSeleccion == Json.ConfiguracionProcesoSeleccion.Id).ToList();
                List<ProcesoSeleccionEtapaBO> listaEtapa = etapaRep.GetBy(x => x.IdProcesoSeleccion == Json.ConfiguracionProcesoSeleccion.Id).ToList();
                List<ConfiguracionAsignacionExamenBO> EliminarExamen = new List<ConfiguracionAsignacionExamenBO>();
                List<int> IdExistente = new List<int>();
                List<ConfiguracionAsignacionEvaluacionBO> EliminarEvaluacion = new List<ConfiguracionAsignacionEvaluacionBO>();
                List<int> IdExistenteEvaluacion = new List<int>();
                List<ProcesoSeleccionEtapaBO> EliminarEtapa = new List<ProcesoSeleccionEtapaBO>();
                List<int> IdExistenteEtapa = new List<int>();
                List<EtapaProcesoSeleccionDTO> ListaIdsEtapa = new List<EtapaProcesoSeleccionDTO>();
                var count = 1;
                // , x => new ConfiguracionAsignacionExamenBO { Id = x.Id, IdProcesoSeleccion = x.IdProcesoSeleccion, IdExamen = x.IdExamen, NroOrden = x.NroOrden.Value,Estado=x.Estado,FechaCreacion=x.FechaCreacion,FechaModificacion=x.FechaModificacion,UsuarioCreacion=x.UsuarioCreacion,UsuarioModificacion=x.UsuarioModificacion }
                using (TransactionScope scope = new TransactionScope())
                {
                    procesoSeleccion.Nombre = Json.ConfiguracionProcesoSeleccion.Nombre;
                    procesoSeleccion.IdPuestoTrabajo = Json.ConfiguracionProcesoSeleccion.IdPuestoTrabajo;
                    procesoSeleccion.Codigo = Json.ConfiguracionProcesoSeleccion.Codigo;
                    procesoSeleccion.Url = Json.ConfiguracionProcesoSeleccion.Url;
                    procesoSeleccion.Activo = Json.ConfiguracionProcesoSeleccion.Activo;
                    procesoSeleccion.IdSede = Json.ConfiguracionProcesoSeleccion.IdSede;
                    procesoSeleccion.FechaInicioProceso = Json.ConfiguracionProcesoSeleccion.FechaInicioProceso;
                    procesoSeleccion.FechaFinProceso = Json.ConfiguracionProcesoSeleccion.FechaFinProceso;

                    procesoSeleccion.UsuarioModificacion = Json.Usuario;
                    procesoSeleccion.FechaModificacion = DateTime.Now;
                    repProcesoSeleccionRep.Update(procesoSeleccion);


                    foreach (var item in Json.listaEtapas)
                    {
                        ProcesoSeleccionEtapaBO etapa = new ProcesoSeleccionEtapaBO();
                        var etapaNuevo = listaEtapa.Where(x => x.Id == item.Id).FirstOrDefault();
                        if (etapaNuevo != null && item.Id > 0)
                        {
                            etapa = etapaRep.FirstById(item.Id);
                            etapa.Nombre = item.Nombre;
                            //etapa.IdExamen = item.IdExamen;
                            etapa.IdProcesoSeleccion = procesoSeleccion.Id;
                            etapa.NroOrden = item.NroOrden;
                            etapa.UsuarioModificacion = Json.Usuario;
                            etapa.FechaModificacion = DateTime.Now;
                            etapaRep.Update(etapa);
                            IdExistenteEtapa.Add(etapa.Id);
                        }
                        else
                        {
                            etapa = new ProcesoSeleccionEtapaBO();
                            etapa.Nombre = item.Nombre;
                            etapa.IdProcesoSeleccion = procesoSeleccion.Id;
                            etapa.NroOrden = item.NroOrden;

                            etapa.Estado = true;
                            etapa.UsuarioCreacion = Json.Usuario;
                            etapa.UsuarioModificacion = Json.Usuario;
                            etapa.FechaCreacion = DateTime.Now;
                            etapa.FechaModificacion = DateTime.Now;
                            etapaRep.Insert(etapa);
                            IdExistenteEtapa.Add(etapa.Id);

                            EtapaProcesoSeleccionDTO EtapaProceso = new EtapaProcesoSeleccionDTO();
                            if (item.Id < 0)
                            {
                                EtapaProceso.IdEtapa = item.Id;
                                EtapaProceso.IdProcesoSeleccionEtapa = etapa.Id;
                            }
                            ListaIdsEtapa.Add(EtapaProceso);
                        }

                    }

                    foreach (var ev in listaEvaluaciones) {

                        var evaluacionNuevo = listaEvaluacion.Where(x => x.IdEvaluacion == ev.IdEvaluacion && x.IdProcesoSeleccion == Json.ConfiguracionProcesoSeleccion.Id).FirstOrDefault();
                        if (evaluacionNuevo != null && evaluacionNuevo.Id != 0 && evaluacionNuevo.Id != null)
                        {
                            ConfiguracionAsignacionEvaluacionBO evaluacion = repConfiguracionAsignacionEvaluacionRep.FirstBy(x => x.IdEvaluacion == ev.IdEvaluacion && x.IdProcesoSeleccion == Json.ConfiguracionProcesoSeleccion.Id); ;
                            evaluacion.IdProcesoSeleccion = procesoSeleccion.Id;
                            evaluacion.IdEvaluacion = ev.IdEvaluacion;
                            evaluacion.NroOrden = ev.NroOrden;
                            evaluacion.IdProcesoSeleccionEtapa = ev.IdProcesoSeleccionEtapa < 0 ? ListaIdsEtapa.Where(x => x.IdEtapa == ev.IdProcesoSeleccionEtapa).Select(x => x.IdProcesoSeleccionEtapa).FirstOrDefault() : ev.IdProcesoSeleccionEtapa;

                            evaluacion.UsuarioModificacion = Json.Usuario;
                            evaluacion.FechaModificacion = DateTime.Now;
                            repConfiguracionAsignacionEvaluacionRep.Update(evaluacion);

                            IdExistenteEvaluacion.Add(evaluacion.Id);
                        }
                        else
                        {
                            ConfiguracionAsignacionEvaluacionBO evaluacion = new ConfiguracionAsignacionEvaluacionBO();
                            evaluacion.IdProcesoSeleccion = procesoSeleccion.Id;
                            evaluacion.IdEvaluacion = ev.IdEvaluacion;
                            evaluacion.NroOrden = ev.NroOrden;
                            evaluacion.IdProcesoSeleccionEtapa = ev.IdProcesoSeleccionEtapa < 0 ? ListaIdsEtapa.Where(x => x.IdEtapa == ev.IdProcesoSeleccionEtapa).Select(x => x.IdProcesoSeleccionEtapa).FirstOrDefault() : ev.IdProcesoSeleccionEtapa;

                            evaluacion.Estado = true;
                            evaluacion.UsuarioCreacion = Json.Usuario;
                            evaluacion.UsuarioModificacion = Json.Usuario;
                            evaluacion.FechaCreacion = DateTime.Now;
                            evaluacion.FechaModificacion = DateTime.Now;
                            repConfiguracionAsignacionEvaluacionRep.Insert(evaluacion);
                            IdExistenteEvaluacion.Add(evaluacion.Id);
                        }
                    }

                    EliminarExamen=repConfiguracionAsignacionExamenRep.GetBy(x => !IdExistente.Contains(x.Id) && x.IdProcesoSeleccion == Json.ConfiguracionProcesoSeleccion.Id).ToList();
                    foreach (var eliminar in EliminarExamen) {
                        repConfiguracionAsignacionExamenRep.Delete(eliminar.Id,Json.Usuario);
                    }

                    EliminarEtapa = etapaRep.GetBy(x => !IdExistenteEtapa.Contains(x.Id) && x.IdProcesoSeleccion == Json.ConfiguracionProcesoSeleccion.Id).ToList();
                    foreach (var eliminar in EliminarEtapa)
                    {
                        etapaRep.Delete(eliminar.Id, Json.Usuario);
                    }
                    EliminarEvaluacion = repConfiguracionAsignacionEvaluacionRep.GetBy(x => !IdExistenteEvaluacion.Contains(x.Id) && x.IdProcesoSeleccion == Json.ConfiguracionProcesoSeleccion.Id).ToList();
                    foreach (var eliminar in EliminarEvaluacion)
                    {
                        repConfiguracionAsignacionEvaluacionRep.Delete(eliminar.Id, Json.Usuario);
                    }
                    scope.Complete();
                }

                return Ok(Json.ConfiguracionProcesoSeleccion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarProcesoSeleccionConfiguracion([FromBody] ProcesoSeleccionAgrupadoInsertarModificarDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IEnumerable < EvaluacionAsignadoProcesoDTO > listaEvaluaciones = Json.listaEvaluaciones.Concat(Json.listaEvaluacionesEvaluador);

                ProcesoSeleccionRepositorio repProcesoSeleccionRep = new ProcesoSeleccionRepositorio(_integraDBContext);
                ProcesoSeleccionEtapaRepositorio repProcesoSeleccionEtapaRep= new ProcesoSeleccionEtapaRepositorio(_integraDBContext);
                ProcesoSeleccionBO procesoSeleccion = new ProcesoSeleccionBO();
                ConfiguracionAsignacionExamenRepositorio repConfiguracionAsignacionExamenRep = new ConfiguracionAsignacionExamenRepositorio(_integraDBContext);
                ConfiguracionAsignacionEvaluacionRepositorio repConfiguracionAsignacionEvaluacionRep= new ConfiguracionAsignacionEvaluacionRepositorio(_integraDBContext);
                List<EtapaProcesoSeleccionDTO> ListaIdsEtapa = new List<EtapaProcesoSeleccionDTO>();
                var count = 1;

                using (TransactionScope scope = new TransactionScope()) {
                    procesoSeleccion.Nombre = Json.ConfiguracionProcesoSeleccion.Nombre;
                    procesoSeleccion.IdPuestoTrabajo = Json.ConfiguracionProcesoSeleccion.IdPuestoTrabajo;
                    procesoSeleccion.Codigo = Json.ConfiguracionProcesoSeleccion.Codigo;
                    procesoSeleccion.Url = Json.ConfiguracionProcesoSeleccion.Url;
                    procesoSeleccion.Activo = Json.ConfiguracionProcesoSeleccion.Activo;
                    procesoSeleccion.IdSede = Json.ConfiguracionProcesoSeleccion.IdSede;
                    procesoSeleccion.FechaInicioProceso = Json.ConfiguracionProcesoSeleccion.FechaInicioProceso;
                    procesoSeleccion.FechaFinProceso = Json.ConfiguracionProcesoSeleccion.FechaFinProceso;

                    procesoSeleccion.Estado = true;
                    procesoSeleccion.UsuarioCreacion = Json.Usuario;
                    procesoSeleccion.UsuarioModificacion = Json.Usuario;
                    procesoSeleccion.FechaCreacion = DateTime.Now;
                    procesoSeleccion.FechaModificacion= DateTime.Now;
                    repProcesoSeleccionRep.Insert(procesoSeleccion);
                    Json.ConfiguracionProcesoSeleccion.Id = procesoSeleccion.Id;


                    foreach (var item in Json.listaEtapas)
                    {
                        ProcesoSeleccionEtapaBO etapa = new ProcesoSeleccionEtapaBO();
                        etapa.Nombre = item.Nombre;
                        etapa.IdProcesoSeleccion = procesoSeleccion.Id;
                        etapa.NroOrden = item.NroOrden;

                        etapa.Estado = true;
                        etapa.UsuarioCreacion = Json.Usuario;
                        etapa.UsuarioModificacion = Json.Usuario;
                        etapa.FechaCreacion = DateTime.Now;
                        etapa.FechaModificacion = DateTime.Now;
                        repProcesoSeleccionEtapaRep.Insert(etapa);
                        EtapaProcesoSeleccionDTO EtapaProceso = new EtapaProcesoSeleccionDTO();
                        if (item.Id < 0)
                        {
                            EtapaProceso.IdEtapa = item.Id;
                            EtapaProceso.IdProcesoSeleccionEtapa = etapa.Id;
                        }
                        ListaIdsEtapa.Add(EtapaProceso);

                    }

                    foreach (var ev in listaEvaluaciones)
                    {
                        ConfiguracionAsignacionEvaluacionBO evaluacion = new ConfiguracionAsignacionEvaluacionBO();
                        evaluacion.IdProcesoSeleccion = procesoSeleccion.Id;
                        evaluacion.IdEvaluacion = ev.IdEvaluacion;
                        evaluacion.NroOrden = ev.NroOrden;
                        if (ev.IdProcesoSeleccionEtapa < 0)
                        {
                            evaluacion.IdProcesoSeleccionEtapa = ListaIdsEtapa.Where(x => x.IdEtapa == ev.IdProcesoSeleccionEtapa).Select(x => x.IdProcesoSeleccionEtapa).FirstOrDefault();
                        }
                        else {
                            evaluacion.IdProcesoSeleccionEtapa = null;
                        }
                        //evaluacion.IdProcesoSeleccionEtapa = ev.IdProcesoSeleccionEtapa < 0 ? ListaIdsEtapa.Where(x => x.IdEtapa == ev.IdProcesoSeleccionEtapa).Select(x => x.IdProcesoSeleccionEtapa).FirstOrDefault() :0;
                        
                        evaluacion.Estado = true;
                        evaluacion.UsuarioCreacion = Json.Usuario;
                        evaluacion.UsuarioModificacion = Json.Usuario;
                        evaluacion.FechaCreacion = DateTime.Now;
                        evaluacion.FechaModificacion = DateTime.Now;
                        repConfiguracionAsignacionEvaluacionRep.Insert(evaluacion);                        
                    }

                    scope.Complete();
                }

                return Ok(Json.ConfiguracionProcesoSeleccion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarProcesoSeleccionConfiguracion([FromBody] EliminacionConfiguracionProceso objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProcesoSeleccionRepositorio repProcesoRep = new ProcesoSeleccionRepositorio(_integraDBContext);
                ConfiguracionAsignacionExamenRepositorio repConfiguracionAsignacionExamenRep = new ConfiguracionAsignacionExamenRepositorio(_integraDBContext);
                List<int> ListaExamen = repConfiguracionAsignacionExamenRep.GetBy(x => x.IdProcesoSeleccion == objeto.ProcesoSeleccion.Id).Select(x => x.Id).ToList();

                foreach (int IdEliminar in ListaExamen)
                {
                    repConfiguracionAsignacionExamenRep.Delete(IdEliminar, objeto.Usuario);
                }
                if (repProcesoRep.Exist(objeto.ProcesoSeleccion.Id))
                {
                    repProcesoRep.Delete(objeto.ProcesoSeleccion.Id, objeto.Usuario);
                }
                else {
                    throw new Exception("El registro que se desea eliminar no existe ¿Id correcto?");
                }                               

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdProcesoSeleccion}")]
        [HttpGet]
        public ActionResult ObtenerEvaluacionesAsociacion(int IdProcesoSeleccion)
        {
            try
            {
                ExamenTestRepositorio evaluacionRep = new ExamenTestRepositorio();
                ProcesoSeleccionEtapaRepositorio repEtapaRep = new ProcesoSeleccionEtapaRepositorio();

                var listaEvaluacionNoAsociado = evaluacionRep.ObtenerEvaluacionNoAsignadoProcesoSeleccion(IdProcesoSeleccion);
                var listaEvaluacionAsociado = evaluacionRep.ObtenerEvaluacionAsignadoProcesoSeleccion(IdProcesoSeleccion).OrderBy(x=>x.NroOrden).ToList();
                var listaEtapaProcesoSeleccion= repEtapaRep.GetBy(x => x.Estado == true && x.IdProcesoSeleccion==IdProcesoSeleccion, x => new { Id = x.Id, Nombre = x.Nombre }).OrderByDescending(w => w.Id).ToList();

                return Ok(new { listaEvaluacionNoAsociado = listaEvaluacionNoAsociado.Where(x=>x.EsCalificadoPorPostulante==true).ToList(), listaEvaluacionAsociado=listaEvaluacionAsociado.Where(x=>x.EsCalificadoPorPostulante==true).ToList(),
                    listaEvaluacionNoAsociadoEvaluador = listaEvaluacionNoAsociado.Where(x => x.EsCalificadoPorPostulante == false).ToList(),
                    listaEvaluacionAsociadoEvaluador = listaEvaluacionAsociado.Where(x => x.EsCalificadoPorPostulante == false).ToList(),
                    listaEtapa= listaEtapaProcesoSeleccion
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdProcesoSeleccion}")]
        [HttpGet]
        public ActionResult ObtenerEvaluacionPuntaje(int IdProcesoSeleccion)
        {
            try
            {
                ExamenTestRepositorio evaluacionRep = new ExamenTestRepositorio(_integraDBContext);
                ProcesoSeleccionPuntajeCalificacionRepositorio PuntajeEvaluacionRep = new ProcesoSeleccionPuntajeCalificacionRepositorio(_integraDBContext);
                ConfiguracionAsignacionEvaluacionRepositorio AsignacionEvaluacionRep = new ConfiguracionAsignacionEvaluacionRepositorio(_integraDBContext);

                var listaEvaluacion = evaluacionRep.ObtenerNombreEvaluacionPuntaje(IdProcesoSeleccion);
                var ListaCalificacionTotal = listaEvaluacion.Where(x => x.CalificacionTotal == true).ToList();
                var ListaCalificacionAgrupadaIndependiente = listaEvaluacion.Where(x => x.CalificacionTotal == false).ToList();
                var ListaIndependiente = ListaCalificacionAgrupadaIndependiente.Where(x => x.IdGrupo == null || x.IdGrupo == 0).ToList();
                var ListaGrupo= ListaCalificacionAgrupadaIndependiente.Where(x => x.IdGrupo != null && x.IdGrupo != 0).ToList();
                List<NombreEvaluacionAgrupadaComponenteDTO> nuevoCalificacionTotal = new List<NombreEvaluacionAgrupadaComponenteDTO>();
                List<NombreEvaluacionAgrupadaComponenteDTO> nuevoCalificacionAgrupada = new List<NombreEvaluacionAgrupadaComponenteDTO>();
                List<NombreEvaluacionAgrupadaComponenteDTO> nuevoCalificacionIndependiente = new List<NombreEvaluacionAgrupadaComponenteDTO>();

                //Separa todas las Calificaciones Totales, se agrupa las evaluaciones y se colocan en nuevoCalificacionTotal
                if (ListaCalificacionTotal.Count() > 0) {
                    foreach (var item in ListaCalificacionTotal) {
                        item.IdComponente = null;
                        item.IdGrupo = null;
                        item.NombreComponente = null;
                        item.NombreGrupo = null;
                        item.CalificaAgrupadoNoIndependiente = false;
                    }
                    nuevoCalificacionTotal = ListaCalificacionTotal.GroupBy(u => (u.IdProcesoSeleccion, u.IdEvaluacion,u.NombreEvaluacion))
                        .Select(group => new NombreEvaluacionAgrupadaComponenteDTO
                        {IdEvaluacion=group.Key.IdEvaluacion
                        ,IdGrupo=null
                        ,IdComponente=null
                        ,IdProcesoSeleccion=group.Key.IdProcesoSeleccion
                        ,NombreComponente=null
                        ,NombreGrupo=null
                        ,NombreEvaluacion=group.Key.NombreEvaluacion
                        ,CalificacionTotal=true
                        ,Puntaje=null
                        ,CalificaPorCentil=false
                        ,IdProcesoSeleccionRango=0
                        ,EsCalificable=false
                        }).ToList();
                }

                //Separa todas las Calificaciones por Componente, se agrupa los Componentes y se colocan en nuevoCalificacionIndependiente
                if (ListaIndependiente.Count() > 0)
                {
                    foreach (var item in ListaIndependiente)
                    {
                        item.IdGrupo = null;
                        item.NombreGrupo = null;
                    }
                    nuevoCalificacionIndependiente = ListaIndependiente.GroupBy(u => (u.IdProcesoSeleccion, u.IdComponente, u.NombreComponente,u.IdEvaluacion,u.NombreEvaluacion))
                        .Select(group => new NombreEvaluacionAgrupadaComponenteDTO
                        {
                            IdEvaluacion = group.Key.IdEvaluacion
                            ,IdGrupo = null        
                            ,IdComponente = group.Key.IdComponente
                            ,IdProcesoSeleccion = group.Key.IdProcesoSeleccion
                            ,NombreComponente = group.Key.NombreComponente
                            ,NombreGrupo = null
                            ,NombreEvaluacion = group.Key.NombreEvaluacion
                            ,CalificacionTotal = false
                            ,Puntaje = null
                            ,CalificaPorCentil = false
                            ,CalificaAgrupadoNoIndependiente=false
                            ,IdProcesoSeleccionRango=0
                            ,EsCalificable=false
                        }).ToList();
                }

                //Separa todas las Calificaciones por Grupo, se agrupa los Grupos y se colocan en nuevoCalificacionAgrupada
                if (ListaGrupo.Count() > 0)
                {
                    foreach (var item in ListaGrupo)
                    {
                        item.IdComponente = null;
                        item.NombreComponente = null;
                    }
                    nuevoCalificacionAgrupada = ListaGrupo.GroupBy(u => (u.IdProcesoSeleccion, u.IdGrupo, u.NombreGrupo,u.IdEvaluacion,u.NombreEvaluacion))
                        .Select(group => new NombreEvaluacionAgrupadaComponenteDTO
                        {
                            IdEvaluacion = group.Key.IdEvaluacion
                            ,IdGrupo = group.Key.IdGrupo
                            ,IdComponente = null
                            ,IdProcesoSeleccion = group.Key.IdProcesoSeleccion
                            ,NombreComponente = null
                            ,NombreGrupo = group.Key.NombreGrupo
                            ,NombreEvaluacion = group.Key.NombreEvaluacion
                            ,CalificacionTotal = false
                            ,Puntaje = null
                            ,CalificaPorCentil = false
                            ,CalificaAgrupadoNoIndependiente=true
                            ,IdProcesoSeleccionRango=0
                            ,EsCalificable=false
                        }).ToList();
                }
                List<NombreEvaluacionAgrupadaComponenteDTO> listaPuntajeCalificacionTotal = new List<NombreEvaluacionAgrupadaComponenteDTO>();
                listaPuntajeCalificacionTotal = nuevoCalificacionTotal.Concat(nuevoCalificacionAgrupada).Concat(nuevoCalificacionIndependiente).ToList();

                foreach (var item in listaPuntajeCalificacionTotal) {
                    if (item.IdEvaluacion != null && item.IdGrupo==null && item.IdComponente==null) {

                        ProcesoSeleccionPuntajeCalificacionBO evaluacionPje = new ProcesoSeleccionPuntajeCalificacionBO();
                        evaluacionPje = PuntajeEvaluacionRep.FirstBy(x => x.IdProcesoSeleccion ==item.IdProcesoSeleccion && x.IdExamenTest==item.IdEvaluacion && x.IdGrupoComponenteEvaluacion==null &&x.IdExamen==null);
                        if (evaluacionPje != null && evaluacionPje.Id != 0) {
                            item.Puntaje = evaluacionPje.PuntajeMinimo;
                            item.CalificaPorCentil = evaluacionPje.CalificaPorCentil;
                            item.IdProcesoSeleccionRango = evaluacionPje.IdProcesoSeleccionRango;
                            item.EsCalificable = evaluacionPje.EsCalificable;
                        }
                    }
                    if (item.IdGrupo != null)
                    {

                        ProcesoSeleccionPuntajeCalificacionBO evaluacionPje = new ProcesoSeleccionPuntajeCalificacionBO();
                        evaluacionPje = PuntajeEvaluacionRep.FirstBy(x => x.IdProcesoSeleccion == item.IdProcesoSeleccion && x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdExamen==null);
                        if (evaluacionPje != null && evaluacionPje.Id != 0)
                        {
                            item.Puntaje = evaluacionPje.PuntajeMinimo;
                            item.CalificaPorCentil = evaluacionPje.CalificaPorCentil;
                            item.IdProcesoSeleccionRango = evaluacionPje.IdProcesoSeleccionRango;
                            item.EsCalificable = evaluacionPje.EsCalificable;
                        }
                    }
                    if (item.IdComponente != null)
                    {
                        ProcesoSeleccionPuntajeCalificacionBO evaluacionPje = new ProcesoSeleccionPuntajeCalificacionBO();
                        evaluacionPje = PuntajeEvaluacionRep.FirstBy(x => x.IdProcesoSeleccion == item.IdProcesoSeleccion && x.IdExamenTest == item.IdEvaluacion && x.IdExamen == item.IdComponente &&x.IdGrupoComponenteEvaluacion==null);
                        if (evaluacionPje != null && evaluacionPje.Id != 0)
                        {
                            item.Puntaje = evaluacionPje.PuntajeMinimo;
                            item.CalificaPorCentil = evaluacionPje.CalificaPorCentil;
                            item.IdProcesoSeleccionRango = evaluacionPje.IdProcesoSeleccionRango;
                            item.EsCalificable = evaluacionPje.EsCalificable;
                        }
                    }
                }

                List<NombreEvaluacionesAgrupadaIndependienteDTO> listaNombreEvaluacion = new List<NombreEvaluacionesAgrupadaIndependienteDTO>();
                foreach (var item in listaPuntajeCalificacionTotal) {
                    if (listaPuntajeCalificacionTotal.Count>0) {
                        listaNombreEvaluacion.Add(new NombreEvaluacionesAgrupadaIndependienteDTO
                        {
                            Id =item.IdEvaluacion.Value
                            ,Nombre=item.NombreEvaluacion
                            ,CalificacionTotal=item.CalificacionTotal
                            ,CalificaAgrupadoNoIndependiente=item.CalificaAgrupadoNoIndependiente
                        });
                    }
                    
                }
                var listaEvaluaciones= listaNombreEvaluacion.GroupBy(u => (u.Id, u.Nombre, u.CalificacionTotal, u.CalificaAgrupadoNoIndependiente))
                        .Select(group => new NombreEvaluacionesAgrupadaIndependienteDTO
                        {
                            Id= group.Key.Id
                            ,Nombre= group.Key.Nombre
                            ,CalificacionTotal = group.Key.CalificacionTotal
                            ,CalificaAgrupadoNoIndependiente = group.Key.CalificaAgrupadoNoIndependiente
                        }).ToList();


                return Ok(new { listaEvaluacionesPuntajeCalificacion = listaPuntajeCalificacionTotal, listaEvaluaciones = listaEvaluaciones });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarProcesoSeleccionConfiguracionCalificacion([FromBody] PuntajeEvaluacionAgrupadaComponenteDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProcesoSeleccionPuntajeCalificacionRepositorio puntajeCalificacionRep = new ProcesoSeleccionPuntajeCalificacionRepositorio();
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var item in Json.ListaPuntaje)
                    {
                        ProcesoSeleccionPuntajeCalificacionBO puntaje = puntajeCalificacionRep.FirstBy(x => x.IdProcesoSeleccion == item.IdProcesoSeleccion && x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdExamen == item.IdComponente);
                        if (puntaje != null && puntaje.Id != null && puntaje.Id != 0)
                        {

                            puntaje.CalificaPorCentil = item.CalificaPorCentil;
                            puntaje.PuntajeMinimo = item.Puntaje;
                            puntaje.IdProcesoSeleccionRango = item.IdProcesoSeleccionRango;
                            puntaje.EsCalificable = item.EsCalificable;

                            puntaje.UsuarioModificacion = Json.Usuario;
                            puntaje.FechaModificacion = DateTime.Now;
                            puntajeCalificacionRep.Update(puntaje);
                        }
                        else
                        {
                            puntaje = new ProcesoSeleccionPuntajeCalificacionBO();
                            puntaje.IdProcesoSeleccion = item.IdProcesoSeleccion;
                            puntaje.IdExamen = item.IdComponente;
                            puntaje.IdExamenTest = item.IdEvaluacion;
                            puntaje.IdGrupoComponenteEvaluacion = item.IdGrupo;
                            puntaje.CalificaPorCentil = item.CalificaPorCentil;
                            puntaje.PuntajeMinimo = item.Puntaje;
                            puntaje.IdProcesoSeleccionRango = item.IdProcesoSeleccionRango;
                            puntaje.EsCalificable = item.EsCalificable;

                            puntaje.Estado = true;
                            puntaje.UsuarioCreacion = Json.Usuario;
                            puntaje.FechaCreacion = DateTime.Now;
                            puntaje.UsuarioModificacion = Json.Usuario;
                            puntaje.FechaModificacion = DateTime.Now;
                            puntajeCalificacionRep.Insert(puntaje);
                        }
                    }

                    scope.Complete();
                }
                
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}