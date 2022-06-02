using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Servicios.SCode.BO;
using BSI.Integra.Aplicacion.Servicios.SCode.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MessengerEnvioMasivo")]
    [ApiController]
    public class MessengerEnvioMasivoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public MessengerEnvioMasivoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]/{IdConjuntoLista}/{IdActividadCabecera}")]
        [HttpGet]
        public IActionResult ObtenerConjuntoListaDetalleParaMessengerEnvioMasivo(int IdConjuntoLista, int IdActividadCabecera)
        {
            try
            {
                if(IdActividadCabecera == 0)
                {
                    ConjuntoListaDetalleRepositorio conjuntoListaDetalleRepositorio = new ConjuntoListaDetalleRepositorio(_integraDBContext);

                    return Ok(conjuntoListaDetalleRepositorio.ObtenerConjuntoListaDetalleParaMessengerEnvioMasivo(IdConjuntoLista));
                }
                else
                {
                    MessengerEnvioMasivoRepositorio messengerEnvioMasivoRepositorio = new MessengerEnvioMasivoRepositorio(_integraDBContext);
                    
                    return Ok(messengerEnvioMasivoRepositorio.ObtenerMessengerEnvioMasivo(IdActividadCabecera));
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult CrearMasivo([FromBody] ActividadCabeceraMessengerEnvioMasivoDTO actividadCabeceraMessengerEnvioMasivoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ActividadCabeceraRepositorio actividadCabeceraRepositorio = new ActividadCabeceraRepositorio(_integraDBContext);
                MessengerEnvioMasivoRepositorio messengerEnvioMasivoRepositorio = new MessengerEnvioMasivoRepositorio(_integraDBContext);
                ConjuntoListaDetalleRepositorio conjuntoListaDetalleRepositorio = new ConjuntoListaDetalleRepositorio(_integraDBContext);

                List<MessengerEnvioMasivoBO> messengerEnvioMasivoBOs = new List<MessengerEnvioMasivoBO>();
                bool guardadoCorrectoBD = false;

                ActividadCabeceraBO actividadCabeceraBO;
                MessengerEnvioMasivoBO messengerEnvioMasivoBO;

                using (TransactionScope scope = new TransactionScope())
                {
                    actividadCabeceraBO = new ActividadCabeceraBO();
                    actividadCabeceraBO.Nombre = actividadCabeceraMessengerEnvioMasivoDTO.Nombre;
                    actividadCabeceraBO.Descripcion = actividadCabeceraMessengerEnvioMasivoDTO.Descripcion;
                    actividadCabeceraBO.DuracionEstimada = 0;
                    actividadCabeceraBO.ReproManual = false;
                    actividadCabeceraBO.ReproAutomatica = false;
                    actividadCabeceraBO.Idplantilla = 1;
                    actividadCabeceraBO.IdActividadBase = actividadCabeceraMessengerEnvioMasivoDTO.IdActividadBase;
                    actividadCabeceraBO.ValidaLlamada = false;
                    actividadCabeceraBO.NumeroMaximoLlamadas = 0;
                    actividadCabeceraBO.FechaCreacion2 = DateTime.Now;
                    actividadCabeceraBO.FechaModificacion2 = DateTime.Now;

                    actividadCabeceraBO.IdConjuntoLista = actividadCabeceraMessengerEnvioMasivoDTO.IdConjuntoLista;
                    actividadCabeceraBO.FechaInicioActividad = actividadCabeceraMessengerEnvioMasivoDTO.FechaInicioActividad;
                    actividadCabeceraBO.FechaFinActividad = actividadCabeceraMessengerEnvioMasivoDTO.FechaFinActividad;
                    actividadCabeceraBO.HoraInicio = actividadCabeceraMessengerEnvioMasivoDTO.HoraInicio;
                    actividadCabeceraBO.HoraFin = actividadCabeceraMessengerEnvioMasivoDTO.HoraFin;

                    actividadCabeceraBO.Estado = true;
                    actividadCabeceraBO.UsuarioCreacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;
                    actividadCabeceraBO.UsuarioModificacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;
                    actividadCabeceraBO.FechaCreacion = DateTime.Now;
                    actividadCabeceraBO.FechaModificacion = DateTime.Now;
                    actividadCabeceraBO.IdPersonalAreaTrabajo = actividadCabeceraMessengerEnvioMasivoDTO.IdPersonalAreaTrabajo;
                    actividadCabeceraBO.IdFacebookCuentaPublicitaria = actividadCabeceraMessengerEnvioMasivoDTO.IdFacebookCuentaPublicitaria;

                    actividadCabeceraRepositorio.Insert(actividadCabeceraBO);

                    foreach (var MessengerEnvioMasivo in actividadCabeceraMessengerEnvioMasivoDTO.listaMessengerEnvioMasivo)
                    {
                        messengerEnvioMasivoBO = new MessengerEnvioMasivoBO();

                        messengerEnvioMasivoBO.Nombre = MessengerEnvioMasivo.Nombre;
                        messengerEnvioMasivoBO.Descripcion = MessengerEnvioMasivo.Descripcion ?? "";
                        messengerEnvioMasivoBO.PresupuestoDiario = MessengerEnvioMasivo.PresupuestoDiario != null ? (int)(MessengerEnvioMasivo.PresupuestoDiario*100) :  0;
                        messengerEnvioMasivoBO.IdPersonal = MessengerEnvioMasivo.IdPersonal == 0 ? null: MessengerEnvioMasivo.IdPersonal;
                        messengerEnvioMasivoBO.IdPgeneral = MessengerEnvioMasivo.IdPgeneral == 0 ? null: MessengerEnvioMasivo.IdPgeneral;
                        messengerEnvioMasivoBO.IdActividadCabecera = actividadCabeceraBO.Id;
                        messengerEnvioMasivoBO.IdPlantilla = MessengerEnvioMasivo.IdPlantilla ?? 0;
                        messengerEnvioMasivoBO.IdConjuntoListaDetalle = MessengerEnvioMasivo.IdConjuntoListaDetalle ?? 0;
                        messengerEnvioMasivoBO.IdFacebookPagina = MessengerEnvioMasivo.IdFacebookPagina;
                        messengerEnvioMasivoBO.IdFacebookCuentaPublicitaria = actividadCabeceraMessengerEnvioMasivoDTO.IdFacebookCuentaPublicitaria ?? 0;

                        messengerEnvioMasivoBO.Estado = true;
                        messengerEnvioMasivoBO.UsuarioCreacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;
                        messengerEnvioMasivoBO.UsuarioModificacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;
                        messengerEnvioMasivoBO.FechaCreacion = DateTime.Now;
                        messengerEnvioMasivoBO.FechaModificacion = DateTime.Now;

                        messengerEnvioMasivoRepositorio.Insert(messengerEnvioMasivoBO);

                        messengerEnvioMasivoBOs.Add(messengerEnvioMasivoBO);
                    }
                    scope.Complete();

                    guardadoCorrectoBD = true;
                }

                try
                {
                    APIGraphFacebook aPIGraphFacebook = new APIGraphFacebook();
                    FacebookCuentaPublicitariaRepositorio facebookCuentaPublicitariaRepositorio = new FacebookCuentaPublicitariaRepositorio(_integraDBContext);

                    FacebookCampanhaRepositorio facebookCampanhaRepositorio = new FacebookCampanhaRepositorio(_integraDBContext);
                    FacebookAudienciaRepositorio facebookAudienciaRepositorio = new FacebookAudienciaRepositorio(_integraDBContext);
                    FacebookAudienciaCuentaPublicitariaRepositorio facebookAudienciaCuentaPublicitariaRepositorio = new FacebookAudienciaCuentaPublicitariaRepositorio(_integraDBContext);
                    FacebookPaginaRepositorio facebookPaginaRepositorio = new FacebookPaginaRepositorio(_integraDBContext);
                    ConjuntoListaResultadoRepositorio conjuntoListaResultadoRepositorio = new ConjuntoListaResultadoRepositorio(_integraDBContext);
                    ConjuntoListaRepositorio conjuntoListaRepositorio = new ConjuntoListaRepositorio(_integraDBContext);
                    ConjuntoAnuncioFacebookRepositorio conjuntoAnuncioFacebookRepositorio = new ConjuntoAnuncioFacebookRepositorio(_integraDBContext);
                    FacebookAnuncioCreativoRepositorio facebookAnuncioCreativoRepositorio = new FacebookAnuncioCreativoRepositorio(_integraDBContext);
                    FacebookAnuncioRepositorio facebookAnuncioRepositorio = new FacebookAnuncioRepositorio(_integraDBContext);
                    PlantillaClaveValorRepositorio plantillaClaveValorRepositorio = new PlantillaClaveValorRepositorio(_integraDBContext);
                    PgeneralRepositorio pgeneralRepositorio = new PgeneralRepositorio(_integraDBContext);
                    PersonalRepositorio personalRepositorio = new PersonalRepositorio(_integraDBContext);


                    var facebookCuentaPublicitariaBO = facebookCuentaPublicitariaRepositorio.FirstById(actividadCabeceraMessengerEnvioMasivoDTO.IdFacebookCuentaPublicitaria ?? 0);
                    var facebookPaginas = facebookPaginaRepositorio.GetBy(x => true);

                    ApiGraphFacebookResponseCrearDTO apiGraphFacebookResponseCrearDTO;
                    apiGraphFacebookResponseCrearDTO = aPIGraphFacebook.CrearCampaña("NONE", actividadCabeceraMessengerEnvioMasivoDTO.Nombre, "MESSAGES", "PAUSED", facebookCuentaPublicitariaBO.FacebookIdCuentaPublicitaria);

                    FacebookCampanhaBO facebookCampanhaBO = new FacebookCampanhaBO();
                    facebookCampanhaBO.FacebookId = apiGraphFacebookResponseCrearDTO.id;
                    facebookCampanhaBO.Nombre = actividadCabeceraMessengerEnvioMasivoDTO.Nombre;
                    facebookCampanhaBO.Objetivo = "MESSAGES";
                    facebookCampanhaBO.EstadoFacebook = "PAUSED";
                    facebookCampanhaBO.IdFacebookCuentaPublicitaria = facebookCuentaPublicitariaBO.Id;

                    facebookCampanhaBO.Estado = true;
                    facebookCampanhaBO.UsuarioCreacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;
                    facebookCampanhaBO.UsuarioModificacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;
                    facebookCampanhaBO.FechaCreacion = DateTime.Now;
                    facebookCampanhaBO.FechaModificacion = DateTime.Now;

                    facebookCampanhaRepositorio.Insert(facebookCampanhaBO);

                    actividadCabeceraBO = actividadCabeceraRepositorio.FirstById(actividadCabeceraBO.Id);
                    actividadCabeceraBO.IdFacebookCampanha = facebookCampanhaBO.Id;
                    actividadCabeceraRepositorio.Update(actividadCabeceraBO);

                    FacebookAudienciaBO facebookAudienciaBO;
                    FacebookAudienciaCuentaPublicitariaBO facebookAudienciaCuentaPublicitariaBO;
                    ApiGraphFacebookResponseSubirListasDTO apiGraphFacebookResponseSubirListasDTO;
                    ConjuntoAnuncioFacebookBO conjuntoAnuncioFacebookBO;
                    FacebookAnuncioCreativoBO facebookAnuncioCreativoBO;
                    FacebookAnuncioBO facebookAnuncioBO;
                    PlantillaClaveValorBO plantillaClaveValorBO;
                    foreach (var segmento in messengerEnvioMasivoBOs)
                    {
                        var conjuntoListaDetalle =  conjuntoListaDetalleRepositorio.FirstById(segmento.IdConjuntoListaDetalle);
                        var conjuntoLista = conjuntoListaRepositorio.FirstById(conjuntoListaDetalle.IdConjuntoLista);

                        apiGraphFacebookResponseCrearDTO = aPIGraphFacebook.CrearAudiencia(segmento.Nombre, "CUSTOM", "", "USER_PROVIDED_ONLY", facebookCuentaPublicitariaBO.FacebookIdCuentaPublicitaria);

                        facebookAudienciaBO = new FacebookAudienciaBO();
                        facebookAudienciaBO.IdFiltroSegmento = conjuntoLista.IdFiltroSegmento;
                        facebookAudienciaBO.FacebookIdAudiencia = apiGraphFacebookResponseCrearDTO.id;
                        facebookAudienciaBO.Nombre = segmento.Nombre;
                        facebookAudienciaBO.Descripcion = "";
                        facebookAudienciaBO.Subtipo = "CUSTOM";
                        facebookAudienciaBO.RecursoArchivoCliente = "USER_PROVIDED_ONLY";
                        facebookAudienciaBO.Estado = true;
                        facebookAudienciaBO.FechaCreacion = DateTime.Now;
                        facebookAudienciaBO.FechaModificacion = DateTime.Now;
                        facebookAudienciaBO.UsuarioCreacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;
                        facebookAudienciaBO.UsuarioModificacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;

                        facebookAudienciaRepositorio.Insert(facebookAudienciaBO);

                        facebookAudienciaCuentaPublicitariaBO = new FacebookAudienciaCuentaPublicitariaBO();
                        facebookAudienciaCuentaPublicitariaBO.IdFacebookAudiencia = facebookAudienciaBO.Id;
                        facebookAudienciaCuentaPublicitariaBO.IdFacebookCuentaPublicitaria = facebookCuentaPublicitariaBO.Id;
                        facebookAudienciaCuentaPublicitariaBO.Subtipo = "CUSTOM";
                        facebookAudienciaCuentaPublicitariaBO.Origen = "Propio";
                        facebookAudienciaCuentaPublicitariaBO.Estado = true;
                        facebookAudienciaCuentaPublicitariaBO.FechaCreacion = DateTime.Now;
                        facebookAudienciaCuentaPublicitariaBO.FechaModificacion = DateTime.Now;
                        facebookAudienciaCuentaPublicitariaBO.UsuarioCreacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;
                        facebookAudienciaCuentaPublicitariaBO.UsuarioModificacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;

                        facebookAudienciaCuentaPublicitariaRepositorio.Insert(facebookAudienciaCuentaPublicitariaBO);

                        var lista = conjuntoListaResultadoRepositorio.ObtenerMessengerUsuarioPorConjuntoListaResultado(segmento.IdConjuntoListaDetalle, segmento.IdFacebookPagina);
                        apiGraphFacebookResponseSubirListasDTO = aPIGraphFacebook.SubirUsuariosParaAudienciaPorUsuariosFacebook(facebookPaginas.Where(x => x.Id == segmento.IdFacebookPagina).First().FacebookId, lista, apiGraphFacebookResponseCrearDTO.id);

                        messengerEnvioMasivoBO = messengerEnvioMasivoRepositorio.FirstById(segmento.Id);
                        messengerEnvioMasivoBO.IdFacebookAudiencia = facebookAudienciaBO.Id;
                        messengerEnvioMasivoRepositorio.Update(messengerEnvioMasivoBO);

                        apiGraphFacebookResponseCrearDTO = aPIGraphFacebook.CrearConjuntoAnuncioMessenger(segmento.Nombre, segmento.PresupuestoDiario.ToString(), facebookCampanhaBO.FacebookId, facebookAudienciaBO.FacebookIdAudiencia, (actividadCabeceraBO.FechaInicioActividad + actividadCabeceraBO.HoraInicio) ?? DateTime.Now, (actividadCabeceraBO.FechaFinActividad + actividadCabeceraBO.HoraFin), facebookPaginas.Where(x => x.Id == segmento.IdFacebookPagina).First().FacebookId, facebookCuentaPublicitariaBO.FacebookIdCuentaPublicitaria);

                        conjuntoAnuncioFacebookBO = new ConjuntoAnuncioFacebookBO();
                        conjuntoAnuncioFacebookBO.IdAnuncioFacebook = apiGraphFacebookResponseCrearDTO.id;
                        conjuntoAnuncioFacebookBO.BidAmount = 500;
                        conjuntoAnuncioFacebookBO.BillinEevent = "IMPRESSIONS";
                        conjuntoAnuncioFacebookBO.CampaignId = facebookCampanhaBO.FacebookId;
                        conjuntoAnuncioFacebookBO.CreatedTime = DateTime.Now;
                        conjuntoAnuncioFacebookBO.DailyBudget = segmento.PresupuestoDiario;
                        conjuntoAnuncioFacebookBO.Name = segmento.Nombre;
                        conjuntoAnuncioFacebookBO.OptimizationGoal = "IMPRESSIONS";
                        conjuntoAnuncioFacebookBO.StartTime = actividadCabeceraBO.FechaInicioActividad;
                        conjuntoAnuncioFacebookBO.Status = "PAUSED";
                        conjuntoAnuncioFacebookBO.CuentaPublicitaria = facebookCampanhaBO.IdFacebookCuentaPublicitaria;
                        conjuntoAnuncioFacebookBO.NombreCampania = facebookCampanhaBO.Nombre;
                        conjuntoAnuncioFacebookBO.Estado = true;
                        conjuntoAnuncioFacebookBO.FechaCreacion = DateTime.Now;
                        conjuntoAnuncioFacebookBO.FechaModificacion = DateTime.Now;
                        conjuntoAnuncioFacebookBO.UsuarioCreacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;
                        conjuntoAnuncioFacebookBO.UsuarioModificacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;

                        conjuntoAnuncioFacebookRepositorio.Insert(conjuntoAnuncioFacebookBO);

                        messengerEnvioMasivoBO = messengerEnvioMasivoRepositorio.FirstById(segmento.Id);
                        messengerEnvioMasivoBO.IdConjuntoAnuncioFacebook = conjuntoAnuncioFacebookBO.Id;
                        messengerEnvioMasivoRepositorio.Update(messengerEnvioMasivoBO);

                        var plantilla = plantillaClaveValorRepositorio.FirstBy(x => x.IdPlantilla == segmento.IdPlantilla && x.Clave == "Texto", y => y.Valor);

                        if (plantilla.Contains("{tPersonal.") && segmento.IdPersonal != null)
                        {
                            var personal = personalRepositorio.FirstBy(x => x.Id == segmento.IdPersonal);
                            if(personal!= null && personal.Id != 0)
                            {
                                if (plantilla.Contains("{tPersonal.nombres}") && personal.Nombres != null) plantilla = plantilla.Replace("{tPersonal.nombres}", personal.Nombres);
                                if (plantilla.Contains("{tPersonal.apellidos}") && personal.Nombres != null) plantilla = plantilla.Replace("{tPersonal.apellidos}", personal.Apellidos);
                            }
                        }

                        if (plantilla.Contains("{tPLA_PGeneral.Nombre}") && segmento.IdPgeneral != null)
                        {
                            var programa = pgeneralRepositorio.FirstBy(x => x.Id == segmento.IdPgeneral);
                            if(programa != null && programa.Id != 0)
                            {
                                if (plantilla.Contains("{tPLA_PGeneral.Nombre}") && programa.Nombre != null) plantilla = plantilla.Replace("{tPLA_PGeneral.Nombre}", programa.Nombre);
                            }
                        }

                        apiGraphFacebookResponseCrearDTO = aPIGraphFacebook.CrearAnuncioCreativo(facebookPaginas.Where(x => x.Id == segmento.IdFacebookPagina).First().FacebookId, "SHARE", plantilla, facebookCuentaPublicitariaBO.FacebookIdCuentaPublicitaria);

                        facebookAnuncioCreativoBO = new FacebookAnuncioCreativoBO();
                        facebookAnuncioCreativoBO.FacebookId = apiGraphFacebookResponseCrearDTO.id;
                        facebookAnuncioCreativoBO.IdPaginaFacebook = segmento.IdFacebookPagina;
                        facebookAnuncioCreativoBO.TipoObjetivo = "SHARE";
                        facebookAnuncioCreativoBO.Mensaje = plantilla;
                        facebookAnuncioCreativoBO.IdFacebookCuentaPublicitaria = facebookCampanhaBO.IdFacebookCuentaPublicitaria;
                        facebookAnuncioCreativoBO.Estado = true;
                        facebookAnuncioCreativoBO.FechaCreacion = DateTime.Now;
                        facebookAnuncioCreativoBO.FechaModificacion = DateTime.Now;
                        facebookAnuncioCreativoBO.UsuarioCreacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;
                        facebookAnuncioCreativoBO.UsuarioModificacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;

                        facebookAnuncioCreativoRepositorio.Insert(facebookAnuncioCreativoBO);

                        messengerEnvioMasivoBO = messengerEnvioMasivoRepositorio.FirstById(segmento.Id);
                        messengerEnvioMasivoBO.IdFacebookAnuncioCreativo = facebookAnuncioCreativoBO.Id;
                        messengerEnvioMasivoRepositorio.Update(messengerEnvioMasivoBO);

                        apiGraphFacebookResponseCrearDTO = aPIGraphFacebook.CrearAnuncio(segmento.Nombre, conjuntoAnuncioFacebookBO.IdAnuncioFacebook, facebookAnuncioCreativoBO.FacebookId, "PAUSED", facebookCuentaPublicitariaBO.FacebookIdCuentaPublicitaria);

                        facebookAnuncioBO = new FacebookAnuncioBO();
                        facebookAnuncioBO.FacebookId = apiGraphFacebookResponseCrearDTO.id;
                        facebookAnuncioBO.IdConjuntoAnuncioFacebook = conjuntoAnuncioFacebookBO.Id;
                        facebookAnuncioBO.IdFacebookAnuncioCreativo = facebookAnuncioCreativoBO.Id;
                        facebookAnuncioBO.IdFacebookCuentaPublicitaria = facebookCampanhaBO.IdFacebookCuentaPublicitaria;
                        facebookAnuncioBO.Nombre = segmento.Nombre;
                        facebookAnuncioBO.Estado = true;
                        facebookAnuncioBO.FechaCreacion = DateTime.Now;
                        facebookAnuncioBO.FechaModificacion = DateTime.Now;
                        facebookAnuncioBO.UsuarioCreacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;
                        facebookAnuncioBO.UsuarioModificacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;

                        facebookAnuncioRepositorio.Insert(facebookAnuncioBO);
                        messengerEnvioMasivoBO = messengerEnvioMasivoRepositorio.FirstById(segmento.Id);
                        messengerEnvioMasivoBO.IdFacebookAnuncio = facebookAnuncioBO.Id;
                        messengerEnvioMasivoRepositorio.Update(messengerEnvioMasivoBO);
                    }

                    return Ok(new { GuardadoCorrecto = guardadoCorrectoBD, CreacionFacebook = true });

                }
                catch (Exception e)
                {

                    return Ok( new { GuardadoCorrecto = guardadoCorrectoBD, CreacionFacebook = false, Error = e.Message });
                }

                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        [Route("[Action]")]
        [HttpPost]
        public IActionResult ActualizarMasivo([FromBody] ActividadCabeceraMessengerEnvioMasivoDTO actividadCabeceraMessengerEnvioMasivoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ActividadCabeceraRepositorio actividadCabeceraRepositorio = new ActividadCabeceraRepositorio(_integraDBContext);
                MessengerEnvioMasivoRepositorio messengerEnvioMasivoRepositorio = new MessengerEnvioMasivoRepositorio(_integraDBContext);
                ConjuntoListaDetalleRepositorio conjuntoListaDetalleRepositorio = new ConjuntoListaDetalleRepositorio(_integraDBContext);

                List<MessengerEnvioMasivoBO> messengerEnvioMasivoBOs = new List<MessengerEnvioMasivoBO>();
                bool guardadoCorrectoBD = false;

                ActividadCabeceraBO actividadCabeceraBO;
                MessengerEnvioMasivoBO messengerEnvioMasivoBO;

                using (TransactionScope scope = new TransactionScope())
                {
                    actividadCabeceraBO = actividadCabeceraRepositorio.FirstById(actividadCabeceraMessengerEnvioMasivoDTO.Id);
                    actividadCabeceraBO.Nombre = actividadCabeceraMessengerEnvioMasivoDTO.Nombre;
                    actividadCabeceraBO.Descripcion = actividadCabeceraMessengerEnvioMasivoDTO.Descripcion;
                    actividadCabeceraBO.FechaModificacion2 = DateTime.Now;

                    actividadCabeceraBO.FechaInicioActividad = actividadCabeceraMessengerEnvioMasivoDTO.FechaInicioActividad;
                    actividadCabeceraBO.FechaFinActividad = actividadCabeceraMessengerEnvioMasivoDTO.FechaFinActividad;
                    actividadCabeceraBO.HoraInicio = actividadCabeceraMessengerEnvioMasivoDTO.HoraInicio;
                    actividadCabeceraBO.HoraFin = actividadCabeceraMessengerEnvioMasivoDTO.HoraFin;

                    actividadCabeceraBO.UsuarioModificacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;
                    actividadCabeceraBO.FechaModificacion = DateTime.Now;
                    actividadCabeceraBO.IdPersonalAreaTrabajo = actividadCabeceraMessengerEnvioMasivoDTO.IdPersonalAreaTrabajo;
                    actividadCabeceraBO.IdFacebookCuentaPublicitaria = actividadCabeceraMessengerEnvioMasivoDTO.IdFacebookCuentaPublicitaria;

                    actividadCabeceraRepositorio.Update(actividadCabeceraBO);

                    foreach (var MessengerEnvioMasivo in actividadCabeceraMessengerEnvioMasivoDTO.listaMessengerEnvioMasivo)
                    {
                        messengerEnvioMasivoBO = messengerEnvioMasivoRepositorio.FirstById(MessengerEnvioMasivo.Id);

                        messengerEnvioMasivoBO.Nombre = MessengerEnvioMasivo.Nombre;
                        messengerEnvioMasivoBO.Descripcion = MessengerEnvioMasivo.Descripcion ?? "";
                        messengerEnvioMasivoBO.PresupuestoDiario = MessengerEnvioMasivo.PresupuestoDiario != null ? (int)(MessengerEnvioMasivo.PresupuestoDiario * 100) : 0;
                        messengerEnvioMasivoBO.IdPersonal = MessengerEnvioMasivo.IdPersonal == 0 ? null : MessengerEnvioMasivo.IdPersonal;
                        messengerEnvioMasivoBO.IdPgeneral = MessengerEnvioMasivo.IdPgeneral == 0 ? null : MessengerEnvioMasivo.IdPgeneral;
                        messengerEnvioMasivoBO.IdPlantilla = MessengerEnvioMasivo.IdPlantilla ?? 0;

                        messengerEnvioMasivoBO.UsuarioModificacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;
                        messengerEnvioMasivoBO.FechaModificacion = DateTime.Now;

                        messengerEnvioMasivoRepositorio.Update(messengerEnvioMasivoBO);

                        messengerEnvioMasivoBOs.Add(messengerEnvioMasivoBO);
                    }
                    scope.Complete();

                    guardadoCorrectoBD = true;
                }

                try
                {
                    APIGraphFacebook aPIGraphFacebook = new APIGraphFacebook();
                    FacebookCuentaPublicitariaRepositorio facebookCuentaPublicitariaRepositorio = new FacebookCuentaPublicitariaRepositorio(_integraDBContext);

                    FacebookCampanhaRepositorio facebookCampanhaRepositorio = new FacebookCampanhaRepositorio(_integraDBContext);
                    FacebookAudienciaRepositorio facebookAudienciaRepositorio = new FacebookAudienciaRepositorio(_integraDBContext);
                    FacebookAudienciaCuentaPublicitariaRepositorio facebookAudienciaCuentaPublicitariaRepositorio = new FacebookAudienciaCuentaPublicitariaRepositorio(_integraDBContext);
                    FacebookPaginaRepositorio facebookPaginaRepositorio = new FacebookPaginaRepositorio(_integraDBContext);
                    ConjuntoListaResultadoRepositorio conjuntoListaResultadoRepositorio = new ConjuntoListaResultadoRepositorio(_integraDBContext);
                    ConjuntoListaRepositorio conjuntoListaRepositorio = new ConjuntoListaRepositorio(_integraDBContext);
                    ConjuntoAnuncioFacebookRepositorio conjuntoAnuncioFacebookRepositorio = new ConjuntoAnuncioFacebookRepositorio(_integraDBContext);
                    FacebookAnuncioCreativoRepositorio facebookAnuncioCreativoRepositorio = new FacebookAnuncioCreativoRepositorio(_integraDBContext);
                    FacebookAnuncioRepositorio facebookAnuncioRepositorio = new FacebookAnuncioRepositorio(_integraDBContext);
                    PlantillaClaveValorRepositorio plantillaClaveValorRepositorio = new PlantillaClaveValorRepositorio(_integraDBContext);
                    PgeneralRepositorio pgeneralRepositorio = new PgeneralRepositorio(_integraDBContext);
                    PersonalRepositorio personalRepositorio = new PersonalRepositorio(_integraDBContext);


                    var facebookCuentaPublicitariaBO = facebookCuentaPublicitariaRepositorio.FirstById(actividadCabeceraMessengerEnvioMasivoDTO.IdFacebookCuentaPublicitaria ?? 0);
                    var facebookPaginas = facebookPaginaRepositorio.GetBy(x => true);

                    ApiGraphFacebookResponseCrearDTO apiGraphFacebookResponseCrearDTO;
                    FacebookCampanhaBO facebookCampanhaBO;
                    if (actividadCabeceraBO.IdFacebookCampanha == null)
                    {
                        apiGraphFacebookResponseCrearDTO = aPIGraphFacebook.CrearCampaña("NONE", actividadCabeceraMessengerEnvioMasivoDTO.Nombre, "MESSAGES", "PAUSED", facebookCuentaPublicitariaBO.FacebookIdCuentaPublicitaria);

                        facebookCampanhaBO = new FacebookCampanhaBO();
                        facebookCampanhaBO.FacebookId = apiGraphFacebookResponseCrearDTO.id;
                        facebookCampanhaBO.Nombre = actividadCabeceraMessengerEnvioMasivoDTO.Nombre;
                        facebookCampanhaBO.Objetivo = "MESSAGES";
                        facebookCampanhaBO.EstadoFacebook = "PAUSED";
                        facebookCampanhaBO.IdFacebookCuentaPublicitaria = facebookCuentaPublicitariaBO.Id;

                        facebookCampanhaBO.Estado = true;
                        facebookCampanhaBO.UsuarioCreacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;
                        facebookCampanhaBO.UsuarioModificacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;
                        facebookCampanhaBO.FechaCreacion = DateTime.Now;
                        facebookCampanhaBO.FechaModificacion = DateTime.Now;

                        facebookCampanhaRepositorio.Insert(facebookCampanhaBO);

                        actividadCabeceraBO = actividadCabeceraRepositorio.FirstById(actividadCabeceraBO.Id);
                        actividadCabeceraBO.IdFacebookCampanha = facebookCampanhaBO.Id;
                        actividadCabeceraRepositorio.Update(actividadCabeceraBO);
                    }
                    else
                    {
                        facebookCampanhaBO = facebookCampanhaRepositorio.FirstById(actividadCabeceraBO.IdFacebookCampanha ?? 0);
                    }

                    FacebookAudienciaBO facebookAudienciaBO;
                    FacebookAudienciaCuentaPublicitariaBO facebookAudienciaCuentaPublicitariaBO;
                    ApiGraphFacebookResponseSubirListasDTO apiGraphFacebookResponseSubirListasDTO;
                    ConjuntoAnuncioFacebookBO conjuntoAnuncioFacebookBO;
                    FacebookAnuncioCreativoBO facebookAnuncioCreativoBO;
                    FacebookAnuncioBO facebookAnuncioBO;
                    PlantillaClaveValorBO plantillaClaveValorBO;
                    foreach (var segmento in messengerEnvioMasivoBOs)
                    {
                        var conjuntoListaDetalle = conjuntoListaDetalleRepositorio.FirstById(segmento.IdConjuntoListaDetalle);
                        var conjuntoLista = conjuntoListaRepositorio.FirstById(conjuntoListaDetalle.IdConjuntoLista);

                        if(segmento.IdFacebookAudiencia == null)
                        {
                            apiGraphFacebookResponseCrearDTO = aPIGraphFacebook.CrearAudiencia(segmento.Nombre, "CUSTOM", "", "USER_PROVIDED_ONLY", facebookCuentaPublicitariaBO.FacebookIdCuentaPublicitaria);

                            facebookAudienciaBO = new FacebookAudienciaBO();
                            facebookAudienciaBO.IdFiltroSegmento = conjuntoLista.IdFiltroSegmento;
                            facebookAudienciaBO.FacebookIdAudiencia = apiGraphFacebookResponseCrearDTO.id;
                            facebookAudienciaBO.Nombre = segmento.Nombre;
                            facebookAudienciaBO.Descripcion = "";
                            facebookAudienciaBO.Subtipo = "CUSTOM";
                            facebookAudienciaBO.RecursoArchivoCliente = "USER_PROVIDED_ONLY";
                            facebookAudienciaBO.Estado = true;
                            facebookAudienciaBO.FechaCreacion = DateTime.Now;
                            facebookAudienciaBO.FechaModificacion = DateTime.Now;
                            facebookAudienciaBO.UsuarioCreacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;
                            facebookAudienciaBO.UsuarioModificacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;

                            facebookAudienciaRepositorio.Insert(facebookAudienciaBO);

                            facebookAudienciaCuentaPublicitariaBO = new FacebookAudienciaCuentaPublicitariaBO();
                            facebookAudienciaCuentaPublicitariaBO.IdFacebookAudiencia = facebookAudienciaBO.Id;
                            facebookAudienciaCuentaPublicitariaBO.IdFacebookCuentaPublicitaria = facebookCuentaPublicitariaBO.Id;
                            facebookAudienciaCuentaPublicitariaBO.Subtipo = "CUSTOM";
                            facebookAudienciaCuentaPublicitariaBO.Origen = "Propio";
                            facebookAudienciaCuentaPublicitariaBO.Estado = true;
                            facebookAudienciaCuentaPublicitariaBO.FechaCreacion = DateTime.Now;
                            facebookAudienciaCuentaPublicitariaBO.FechaModificacion = DateTime.Now;
                            facebookAudienciaCuentaPublicitariaBO.UsuarioCreacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;
                            facebookAudienciaCuentaPublicitariaBO.UsuarioModificacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;

                            facebookAudienciaCuentaPublicitariaRepositorio.Insert(facebookAudienciaCuentaPublicitariaBO);

                            var lista = conjuntoListaResultadoRepositorio.ObtenerMessengerUsuarioPorConjuntoListaResultado(segmento.IdConjuntoListaDetalle, segmento.IdFacebookPagina);
                            apiGraphFacebookResponseSubirListasDTO = aPIGraphFacebook.SubirUsuariosParaAudienciaPorUsuariosFacebook(facebookPaginas.Where(x => x.Id == segmento.IdFacebookPagina).First().FacebookId, lista, apiGraphFacebookResponseCrearDTO.id);

                            messengerEnvioMasivoBO = messengerEnvioMasivoRepositorio.FirstById(segmento.Id);
                            messengerEnvioMasivoBO.IdFacebookAudiencia = facebookAudienciaBO.Id;
                            messengerEnvioMasivoRepositorio.Update(messengerEnvioMasivoBO);
                        }
                        else
                        {
                            facebookAudienciaBO = facebookAudienciaRepositorio.FirstById(segmento.IdFacebookAudiencia??0);
                        }
                        

                        if(segmento.IdConjuntoAnuncioFacebook == null)
                        {
                            apiGraphFacebookResponseCrearDTO = aPIGraphFacebook.CrearConjuntoAnuncioMessenger(segmento.Nombre, segmento.PresupuestoDiario.ToString(), facebookCampanhaBO.FacebookId, facebookAudienciaBO.FacebookIdAudiencia, (actividadCabeceraBO.FechaInicioActividad + actividadCabeceraBO.HoraInicio) ?? DateTime.Now, (actividadCabeceraBO.FechaFinActividad + actividadCabeceraBO.HoraFin), facebookPaginas.Where(x => x.Id == segmento.IdFacebookPagina).First().FacebookId, facebookCuentaPublicitariaBO.FacebookIdCuentaPublicitaria);

                            conjuntoAnuncioFacebookBO = new ConjuntoAnuncioFacebookBO();
                            conjuntoAnuncioFacebookBO.IdAnuncioFacebook = apiGraphFacebookResponseCrearDTO.id;
                            conjuntoAnuncioFacebookBO.BidAmount = 500;
                            conjuntoAnuncioFacebookBO.BillinEevent = "IMPRESSIONS";
                            conjuntoAnuncioFacebookBO.CampaignId = facebookCampanhaBO.FacebookId;
                            conjuntoAnuncioFacebookBO.CreatedTime = DateTime.Now;
                            conjuntoAnuncioFacebookBO.DailyBudget = segmento.PresupuestoDiario;
                            conjuntoAnuncioFacebookBO.Name = segmento.Nombre;
                            conjuntoAnuncioFacebookBO.OptimizationGoal = "IMPRESSIONS";
                            conjuntoAnuncioFacebookBO.StartTime = actividadCabeceraBO.FechaInicioActividad;
                            conjuntoAnuncioFacebookBO.Status = "PAUSED";
                            conjuntoAnuncioFacebookBO.CuentaPublicitaria = facebookCampanhaBO.IdFacebookCuentaPublicitaria;
                            conjuntoAnuncioFacebookBO.NombreCampania = facebookCampanhaBO.Nombre;
                            conjuntoAnuncioFacebookBO.Estado = true;
                            conjuntoAnuncioFacebookBO.FechaCreacion = DateTime.Now;
                            conjuntoAnuncioFacebookBO.FechaModificacion = DateTime.Now;
                            conjuntoAnuncioFacebookBO.UsuarioCreacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;
                            conjuntoAnuncioFacebookBO.UsuarioModificacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;

                            conjuntoAnuncioFacebookRepositorio.Insert(conjuntoAnuncioFacebookBO);

                            messengerEnvioMasivoBO = messengerEnvioMasivoRepositorio.FirstById(segmento.Id);
                            messengerEnvioMasivoBO.IdConjuntoAnuncioFacebook = conjuntoAnuncioFacebookBO.Id;
                            messengerEnvioMasivoRepositorio.Update(messengerEnvioMasivoBO);
                        }
                        else
                        {
                            conjuntoAnuncioFacebookBO = conjuntoAnuncioFacebookRepositorio.FirstById(segmento.IdConjuntoAnuncioFacebook??0);

                            aPIGraphFacebook.ActualizarConjuntoAnuncioMessenger(segmento.Nombre, segmento.PresupuestoDiario.ToString(), (actividadCabeceraBO.FechaInicioActividad + actividadCabeceraBO.HoraInicio) ?? DateTime.Now, (actividadCabeceraBO.FechaFinActividad + actividadCabeceraBO.HoraFin), conjuntoAnuncioFacebookBO.IdAnuncioFacebook);
                            conjuntoAnuncioFacebookBO.Name = segmento.Nombre;
                            conjuntoAnuncioFacebookBO.DailyBudget = segmento.PresupuestoDiario;
                            conjuntoAnuncioFacebookBO.StartTime = actividadCabeceraBO.FechaInicioActividad;
                            conjuntoAnuncioFacebookBO.EndTime = actividadCabeceraBO.FechaFinActividad;
                            conjuntoAnuncioFacebookBO.FechaModificacion = DateTime.Now;
                            conjuntoAnuncioFacebookBO.UsuarioModificacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;

                            conjuntoAnuncioFacebookRepositorio.Update(conjuntoAnuncioFacebookBO);

                        }

                        if (segmento.IdFacebookAnuncioCreativo == null)
                        {
                            var plantilla = plantillaClaveValorRepositorio.FirstBy(x => x.IdPlantilla == segmento.IdPlantilla && x.Clave == "Texto", y => y.Valor);

                            if (plantilla.Contains("{tPersonal.") && segmento.IdPersonal != null)
                            {
                                var personal = personalRepositorio.FirstBy(x => x.Id == segmento.IdPersonal);
                                if (personal != null && personal.Id != 0)
                                {
                                    if (plantilla.Contains("{tPersonal.nombres}") && personal.Nombres != null) plantilla = plantilla.Replace("{tPersonal.nombres}", personal.Nombres);
                                    if (plantilla.Contains("{tPersonal.apellidos}") && personal.Nombres != null) plantilla = plantilla.Replace("{tPersonal.apellidos}", personal.Apellidos);
                                }
                            }

                            if (plantilla.Contains("{tPLA_PGeneral.Nombre}") && segmento.IdPgeneral != null)
                            {
                                var programa = pgeneralRepositorio.FirstBy(x => x.Id == segmento.IdPgeneral);
                                if (programa != null && programa.Id != 0)
                                {
                                    if (plantilla.Contains("{tPLA_PGeneral.Nombre}") && programa.Nombre != null) plantilla = plantilla.Replace("{tPLA_PGeneral.Nombre}", programa.Nombre);
                                }
                            }

                            apiGraphFacebookResponseCrearDTO = aPIGraphFacebook.CrearAnuncioCreativo(facebookPaginas.Where(x => x.Id == segmento.IdFacebookPagina).First().FacebookId, "SHARE", plantilla, facebookCuentaPublicitariaBO.FacebookIdCuentaPublicitaria);

                            facebookAnuncioCreativoBO = new FacebookAnuncioCreativoBO();
                            facebookAnuncioCreativoBO.FacebookId = apiGraphFacebookResponseCrearDTO.id;
                            facebookAnuncioCreativoBO.IdPaginaFacebook = segmento.IdFacebookPagina;
                            facebookAnuncioCreativoBO.TipoObjetivo = "SHARE";
                            facebookAnuncioCreativoBO.Mensaje = plantilla;
                            facebookAnuncioCreativoBO.IdFacebookCuentaPublicitaria = facebookCampanhaBO.IdFacebookCuentaPublicitaria;
                            facebookAnuncioCreativoBO.Estado = true;
                            facebookAnuncioCreativoBO.FechaCreacion = DateTime.Now;
                            facebookAnuncioCreativoBO.FechaModificacion = DateTime.Now;
                            facebookAnuncioCreativoBO.UsuarioCreacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;
                            facebookAnuncioCreativoBO.UsuarioModificacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;

                            facebookAnuncioCreativoRepositorio.Insert(facebookAnuncioCreativoBO);

                            messengerEnvioMasivoBO = messengerEnvioMasivoRepositorio.FirstById(segmento.Id);
                            messengerEnvioMasivoBO.IdFacebookAnuncioCreativo = facebookAnuncioCreativoBO.Id;
                            messengerEnvioMasivoRepositorio.Update(messengerEnvioMasivoBO);
                        }
                        else
                        {
                            facebookAnuncioCreativoBO = facebookAnuncioCreativoRepositorio.FirstById(segmento.IdFacebookAnuncioCreativo ?? 0);
                        }

                        if(segmento.IdFacebookAnuncio == null)
                        {
                            apiGraphFacebookResponseCrearDTO = aPIGraphFacebook.CrearAnuncio(segmento.Nombre, conjuntoAnuncioFacebookBO.IdAnuncioFacebook, facebookAnuncioCreativoBO.FacebookId, "PAUSED", facebookCuentaPublicitariaBO.FacebookIdCuentaPublicitaria);

                            facebookAnuncioBO = new FacebookAnuncioBO();
                            facebookAnuncioBO.FacebookId = apiGraphFacebookResponseCrearDTO.id;
                            facebookAnuncioBO.IdConjuntoAnuncioFacebook = conjuntoAnuncioFacebookBO.Id;
                            facebookAnuncioBO.IdFacebookAnuncioCreativo = facebookAnuncioCreativoBO.Id;
                            facebookAnuncioBO.IdFacebookCuentaPublicitaria = facebookCampanhaBO.IdFacebookCuentaPublicitaria;
                            facebookAnuncioBO.Nombre = segmento.Nombre;
                            facebookAnuncioBO.Estado = true;
                            facebookAnuncioBO.FechaCreacion = DateTime.Now;
                            facebookAnuncioBO.FechaModificacion = DateTime.Now;
                            facebookAnuncioBO.UsuarioCreacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;
                            facebookAnuncioBO.UsuarioModificacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;

                            facebookAnuncioRepositorio.Insert(facebookAnuncioBO);
                            messengerEnvioMasivoBO = messengerEnvioMasivoRepositorio.FirstById(segmento.Id);
                            messengerEnvioMasivoBO.IdFacebookAnuncio = facebookAnuncioBO.Id;
                            messengerEnvioMasivoRepositorio.Update(messengerEnvioMasivoBO);
                        }
                        else
                        {
                            facebookAnuncioBO = facebookAnuncioRepositorio.FirstById(segmento.IdFacebookAnuncio ?? 0);
                            aPIGraphFacebook.ActualizarAnuncio(segmento.Nombre, facebookAnuncioBO.FacebookId);

                            facebookAnuncioBO.Nombre = segmento.Nombre;
                            facebookAnuncioBO.FechaModificacion = DateTime.Now;
                            facebookAnuncioBO.UsuarioModificacion = actividadCabeceraMessengerEnvioMasivoDTO.Usuario;
                            facebookAnuncioRepositorio.Update(facebookAnuncioBO);
                        }
                    }

                    return Ok(new { GuardadoCorrecto = guardadoCorrectoBD, CreacionFacebook = true });

                }
                catch (Exception e)
                {
                    return Ok(new { GuardadoCorrecto = guardadoCorrectoBD, CreacionFacebook = false, Error = e.Message });
                }


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}