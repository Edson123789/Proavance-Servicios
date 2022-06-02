using System;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.DTOs;
using System.Transactions;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using AsignacionAutomaticaTempBO = BSI.Integra.Aplicacion.Transversal.BO.AsignacionAutomaticaTempBO;
using BSI.Integra.Aplicacion.Transversal.Helper;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Marketing/FacebookLead
    /// Autor: Joao Benavente - Esthephany Tanco - Richard Zenteno
    /// Fecha: 01/06/2021
    /// <summary>
    /// Gestiona las acciones para el procesamiento y seguimiento de Leads de Facebook
    /// </summary>
    [Route("api/FacebookLead")]
    [ApiController]
    public class FacebookLeadController : ControllerBase
    {

        [Route("[Action]")]
        [HttpPost]
        public ActionResult CrearFacebookFormularioWebhookLeadgen([FromBody] FacebookFormularioWebhookLeadgenDTO facebookFormularioWebhookLeadgenDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                FacebookFormularioWebhookLeadgenBO facebookFormularioWebhookLeadgenBO = new FacebookFormularioWebhookLeadgenBO();
                facebookFormularioWebhookLeadgenBO.FacebookIdLeadgen = facebookFormularioWebhookLeadgenDTO.FacebookIdLeadgen;
                facebookFormularioWebhookLeadgenBO.FacebookIdCampania = facebookFormularioWebhookLeadgenDTO.FacebookIdCampania;
                facebookFormularioWebhookLeadgenBO.FacebookIdFormulario = facebookFormularioWebhookLeadgenDTO.FacebookIdFormulario;
                facebookFormularioWebhookLeadgenBO.FacebookFechaUnix = facebookFormularioWebhookLeadgenDTO.FacebookFechaUnix;
                facebookFormularioWebhookLeadgenBO.FacebookIdPagina = facebookFormularioWebhookLeadgenDTO.FacebookIdPagina;
                facebookFormularioWebhookLeadgenBO.FacebookIdGrupo = facebookFormularioWebhookLeadgenDTO.FacebookIdGrupo;
                facebookFormularioWebhookLeadgenBO.EsProcesado = facebookFormularioWebhookLeadgenDTO.EsProcesado;
                facebookFormularioWebhookLeadgenBO.FacebookFechaLead = facebookFormularioWebhookLeadgenDTO.FacebookFechaLead;
                facebookFormularioWebhookLeadgenBO.Estado = true;
                facebookFormularioWebhookLeadgenBO.UsuarioCreacion = "WebhookFacebook";
                facebookFormularioWebhookLeadgenBO.UsuarioModificacion = "WebhookFacebook";
                facebookFormularioWebhookLeadgenBO.FechaCreacion = DateTime.Now;
                facebookFormularioWebhookLeadgenBO.FechaModificacion = DateTime.Now;

                if (!facebookFormularioWebhookLeadgenBO.HasErrors)
                {
                    FacebookFormularioWebhookLeadgenRepositorio _facebookFormularioWebhookLeadgenRepositorio = new FacebookFormularioWebhookLeadgenRepositorio();
                    _facebookFormularioWebhookLeadgenRepositorio.Insert(facebookFormularioWebhookLeadgenBO);
                    return Ok();
                }
                else
                {
                    return BadRequest(facebookFormularioWebhookLeadgenBO.GetErrors(null));
                }

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult CrearFacebookFormularioWebhookLeadgenError([FromBody] FacebookFormularioWebhookLeadgenErrorDTO facebookFormularioWebhookLeadgenDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FacebookFormularioWebhookLeadgenErrorBO facebookFormularioWebhookLeadgenErrorBO = new FacebookFormularioWebhookLeadgenErrorBO();
                facebookFormularioWebhookLeadgenErrorBO.FacebookIdLead = facebookFormularioWebhookLeadgenDTO.FacebookIdLead;
                facebookFormularioWebhookLeadgenErrorBO.FacebookIdCampania = facebookFormularioWebhookLeadgenDTO.FacebookIdCampania;
                facebookFormularioWebhookLeadgenErrorBO.FacebookIdFormulario = facebookFormularioWebhookLeadgenDTO.FacebookIdFormulario;
                facebookFormularioWebhookLeadgenErrorBO.FacebookFechaUnix = facebookFormularioWebhookLeadgenDTO.FacebookFechaUnix;
                facebookFormularioWebhookLeadgenErrorBO.FacebookIdPagina = facebookFormularioWebhookLeadgenDTO.FacebookIdPagina;
                facebookFormularioWebhookLeadgenErrorBO.FacebookIdGrupo = facebookFormularioWebhookLeadgenDTO.FacebookIdGrupo;
                facebookFormularioWebhookLeadgenErrorBO.EsProcesado = facebookFormularioWebhookLeadgenDTO.EsProcesado;
                facebookFormularioWebhookLeadgenErrorBO.ErrorReal = facebookFormularioWebhookLeadgenDTO.ErrorReal;
                facebookFormularioWebhookLeadgenErrorBO.Error = facebookFormularioWebhookLeadgenDTO.Error;
                facebookFormularioWebhookLeadgenErrorBO.Estado = true;
                facebookFormularioWebhookLeadgenErrorBO.UsuarioCreacion = "WebhookFacebook";
                facebookFormularioWebhookLeadgenErrorBO.UsuarioModificacion = "WebhookFacebook";
                facebookFormularioWebhookLeadgenErrorBO.FechaCreacion = DateTime.Now;
                facebookFormularioWebhookLeadgenErrorBO.FechaModificacion = DateTime.Now;

                if (!facebookFormularioWebhookLeadgenErrorBO.HasErrors)
                {
                    FacebookFormularioWebhookLeadgenErrorRepositorio facebookFormularioWebhookLeadgenErrorRepositorio = new FacebookFormularioWebhookLeadgenErrorRepositorio();
                    facebookFormularioWebhookLeadgenErrorRepositorio.Insert(facebookFormularioWebhookLeadgenErrorBO);
                    return Ok();
                }
                else
                {
                    return BadRequest(facebookFormularioWebhookLeadgenErrorBO.GetErrors(null));
                }

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]/{FacebookIdLeadgen}")]
        [HttpGet]
        public ActionResult ExisteFacebookFormularioWebhookLeadgen(string FacebookIdLeadgen)
        {
            try
            {
                FacebookFormularioWebhookLeadgenRepositorio _facebookFormularioWebhookLeadgenRepositorio = new FacebookFormularioWebhookLeadgenRepositorio();
                FacebookFormularioWebhookLeadgenBO facebookFormularioWebhookLeadgenBO = _facebookFormularioWebhookLeadgenRepositorio.FirstBy(x => x.FacebookIdLeadgen == FacebookIdLeadgen);
                if (facebookFormularioWebhookLeadgenBO != null)
                {
                    return Ok(new { Existe = true });
                }
                return Ok(new { Existe = false });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]/{FacebookIdLead}")]
        [HttpGet]
        public ActionResult ExisteFacebookFormularioWebhookLeadgenError(string FacebookIdLead)
        {
            try
            {
                FacebookFormularioWebhookLeadgenErrorRepositorio _facebookFormularioWebhookLeadgenRepositorio = new FacebookFormularioWebhookLeadgenErrorRepositorio();
                FacebookFormularioWebhookLeadgenErrorBO facebookFormularioWebhookLeadgenBO = _facebookFormularioWebhookLeadgenRepositorio.FirstBy(x => x.FacebookIdLead == FacebookIdLead);
                if (facebookFormularioWebhookLeadgenBO != null)
                {
                    return Ok(new { Existe = true });
                }
                return Ok(new { Existe = false });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 01/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Procesa los leads enviados desde Webhook erroneos en objetos
        /// </summary>
        /// <param name="FacebookRangoFechaDTO">Objeto de clase FacebookRangoFechaDTO</param>
        /// <returns>Response 200 con el bool, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ProcesarFacebookLeadsErroneos([FromBody] FacebookRangoFechaDTO FiltroProcesamiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                FacebookFormularioLeadgenBO facebookFormularioLeadgen = new FacebookFormularioLeadgenBO(contexto);
 
                try
                {
                    bool respuesta = facebookFormularioLeadgen.ProcesarDatosLeadErroneos(FiltroProcesamiento.FechaInicio, FiltroProcesamiento.FechaFin);
                }
                catch (Exception)
                {
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 01/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Procesa los leads enviados desde Webhook en objetos
        /// </summary>
        /// <param name="LeadgenInformacionDTO">Objeto de clase LeadgenInformacionDTO</param>
        /// <returns>Response 200 con el Id de AsignacionAutomaticaTemp, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ProcesarFacebookLead([FromBody] LeadgenInformacionDTO LeadgenInformacionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                FacebookFormularioLeadgenRepositorio _repFacebookFormularioLeadgen = new FacebookFormularioLeadgenRepositorio(contexto);
                AsignacionAutomaticaTempRepositorio _repAsignacionAutomaticaTemp = new AsignacionAutomaticaTempRepositorio(contexto);
                ConjuntoAnuncioRepositorio _repConjuntoAnuncio = new ConjuntoAnuncioRepositorio(contexto);
                AnuncioFacebookRepositorio _repAnuncioFacebook = new AnuncioFacebookRepositorio(contexto);
                ConjuntoAnuncioFacebookRepositorio _repConjuntoAnuncioFacebook = new ConjuntoAnuncioFacebookRepositorio(contexto);

                FacebookFormularioLeadgenBO facebookFormularioLeadgen = new FacebookFormularioLeadgenBO();

                facebookFormularioLeadgen.IdLeadgenFacebook = LeadgenInformacionDTO.Id;
                facebookFormularioLeadgen.IdCampanhaFacebook = LeadgenInformacionDTO.AdsetId;
                facebookFormularioLeadgen.NombreCampaniaFacebook = LeadgenInformacionDTO.NombreCampania;
                facebookFormularioLeadgen.FacebookAnuncioId = LeadgenInformacionDTO.AdId;
                facebookFormularioLeadgen.FacebookAnuncioNombre = LeadgenInformacionDTO.AdName;
                facebookFormularioLeadgen.AreaFormacion = LeadgenInformacionDTO.AreaFormacion;
                facebookFormularioLeadgen.AreaTrabajo = LeadgenInformacionDTO.AreaTrabajo;
                facebookFormularioLeadgen.Cargo = LeadgenInformacionDTO.Cargo;
                facebookFormularioLeadgen.Ciudad = LeadgenInformacionDTO.Ciudad;
                facebookFormularioLeadgen.FechaCreacionFacebook = LeadgenInformacionDTO.created_time;
                facebookFormularioLeadgen.Email = LeadgenInformacionDTO.Email;
                facebookFormularioLeadgen.NombreCompleto = LeadgenInformacionDTO.NombreCompleto;
                facebookFormularioLeadgen.Telefono = LeadgenInformacionDTO.Telefono;
                facebookFormularioLeadgen.Industria = LeadgenInformacionDTO.Industria;
                facebookFormularioLeadgen.InicioCapacitacion = LeadgenInformacionDTO.InicioCapacitacion;
                facebookFormularioLeadgen.EsProcesado = true;
                facebookFormularioLeadgen.Estado = true;
                facebookFormularioLeadgen.UsuarioCreacion = "WebhookFacebook";
                facebookFormularioLeadgen.UsuarioModificacion = "WebhookFacebook";
                facebookFormularioLeadgen.FechaCreacion = DateTime.Now;
                facebookFormularioLeadgen.FechaModificacion = DateTime.Now;

                try
                {
                    ConjuntoAnuncioFacebookBO conjuntoAnuncioFacebook = new ConjuntoAnuncioFacebookBO();

                    conjuntoAnuncioFacebook.IdAnuncioFacebook = LeadgenInformacionDTO.AdsetId;
                    conjuntoAnuncioFacebook.BudgetRemaining = Convert.ToDouble(LeadgenInformacionDTO.BudgetRemaining);
                    conjuntoAnuncioFacebook.CampaignId = LeadgenInformacionDTO.CampaignId;
                    conjuntoAnuncioFacebook.CreatedTime = LeadgenInformacionDTO.created_time;
                    conjuntoAnuncioFacebook.DailyBudget = Convert.ToInt32(LeadgenInformacionDTO.DailyBudget);
                    conjuntoAnuncioFacebook.EffectiveStatus = LeadgenInformacionDTO.EffectiveStatus;
                    conjuntoAnuncioFacebook.Name = LeadgenInformacionDTO.AdsetName;
                    conjuntoAnuncioFacebook.OptimizationGoal = LeadgenInformacionDTO.OptimizationGoal;
                    conjuntoAnuncioFacebook.StartTime = LeadgenInformacionDTO.created_time;
                    conjuntoAnuncioFacebook.Status = LeadgenInformacionDTO.AdsetStatus;
                    conjuntoAnuncioFacebook.UpdatedTime = LeadgenInformacionDTO.created_time;
                    conjuntoAnuncioFacebook.IdConjuntoAnuncio = 0;
                    conjuntoAnuncioFacebook.CuentaPublicitaria = LeadgenInformacionDTO.Account;
                    conjuntoAnuncioFacebook.NombreCampania = LeadgenInformacionDTO.NombreCampania;
                    conjuntoAnuncioFacebook.Estado = true;
                    conjuntoAnuncioFacebook.FechaCreacion = DateTime.Now;
                    conjuntoAnuncioFacebook.FechaModificacion = DateTime.Now;
                    conjuntoAnuncioFacebook.UsuarioCreacion = "CARGAMASIVA";
                    conjuntoAnuncioFacebook.UsuarioModificacion = "CARGAMASIVA";

                    AsignacionAutomaticaTempBO asignacionAutomaticaTemp = new AsignacionAutomaticaTempBO(LeadgenInformacionDTO, contexto);

                    string idCampania = LeadgenInformacionDTO.AdsetId?.Replace("as:", string.Empty);

                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (!string.IsNullOrEmpty(idCampania))
                        {
                            if (conjuntoAnuncioFacebook.DailyBudget == null)
                                conjuntoAnuncioFacebook.DailyBudget = 0;

                            var objetoConjuntoAnuncio = _repConjuntoAnuncio.FirstBy(x => x.IdConjuntoAnuncioFacebook == conjuntoAnuncioFacebook.IdAnuncioFacebook, s => new { s.Id });
                            if (objetoConjuntoAnuncio == null)
                            {
                                ConjuntoAnuncioBO conjuntoAnuncioBO = new ConjuntoAnuncioBO();
                                conjuntoAnuncioBO.IdCategoriaOrigen = ValorEstatico.IdFacebookFormulario5Campos;
                                conjuntoAnuncioBO.Origen = "LEADS_FACEBOOK";
                                conjuntoAnuncioBO.IdConjuntoAnuncioFacebook = LeadgenInformacionDTO.AdsetId;
                                conjuntoAnuncioBO.Estado = true;
                                conjuntoAnuncioBO.FechaCreacion = DateTime.Now;
                                conjuntoAnuncioBO.FechaModificacion = DateTime.Now;
                                conjuntoAnuncioBO.UsuarioCreacion = "CARGAMASIVA";
                                conjuntoAnuncioBO.UsuarioModificacion = "CARGAMASIVA";

                                var objetoConjuntoAnuncioFacebook = _repConjuntoAnuncioFacebook.FirstBy(x => x.IdAnuncioFacebook == conjuntoAnuncioFacebook.IdAnuncioFacebook, s => new { s.Name });
                                if (objetoConjuntoAnuncioFacebook != null)
                                {
                                    conjuntoAnuncioBO.Nombre = objetoConjuntoAnuncioFacebook.Name;
                                }
                                else
                                {
                                    conjuntoAnuncioBO.Nombre = LeadgenInformacionDTO.AdsetName;
                                    _repConjuntoAnuncioFacebook.Insert(conjuntoAnuncioFacebook);
                                }
                                _repConjuntoAnuncio.Insert(conjuntoAnuncioBO);
                                asignacionAutomaticaTemp.IdConjuntoAnuncio = conjuntoAnuncioBO.Id;
                            }
                            else
                                asignacionAutomaticaTemp.IdConjuntoAnuncio = objetoConjuntoAnuncio.Id;
                        }

                        var anuncioFacebook = _repAnuncioFacebook.FirstBy(x => x.FacebookIdAnuncio == LeadgenInformacionDTO.AdId);

                        if (anuncioFacebook == null)
                        {
                            anuncioFacebook = new AnuncioFacebookBO();
                            var objetoConjuntoAnuncioFacebook = _repConjuntoAnuncioFacebook.FirstBy(x => x.IdAnuncioFacebook == conjuntoAnuncioFacebook.IdAnuncioFacebook, s => new { s.Id });

                            anuncioFacebook.FacebookIdAnuncio = LeadgenInformacionDTO.AdId;
                            anuncioFacebook.FacebookNombreAnuncio = LeadgenInformacionDTO.AdName;
                            anuncioFacebook.FacebookIdConjuntoAnuncio = LeadgenInformacionDTO.AdsetId;
                            anuncioFacebook.Estado = true;
                            anuncioFacebook.FechaCreacion = DateTime.Now;
                            anuncioFacebook.FechaModificacion = DateTime.Now;
                            anuncioFacebook.UsuarioCreacion = "CARGAMASIVA";
                            anuncioFacebook.UsuarioModificacion = "CARGAMASIVA";
                            anuncioFacebook.IdConjuntoAnuncioFacebook = objetoConjuntoAnuncioFacebook.Id;

                            _repAnuncioFacebook.Insert(anuncioFacebook);
                        }
                        else
                        {
                            if (LeadgenInformacionDTO.AdName != anuncioFacebook.FacebookNombreAnuncio)
                            {
                                anuncioFacebook.FacebookNombreAnuncio = LeadgenInformacionDTO.AdName;

                                _repAnuncioFacebook.Update(anuncioFacebook);
                            }
                        }

                        asignacionAutomaticaTemp.Procesado = false;
                        asignacionAutomaticaTemp.IdAnuncioFacebook = anuncioFacebook.Id;
                        asignacionAutomaticaTemp.Estado = true;
                        asignacionAutomaticaTemp.UsuarioCreacion = "WebHookFacebookLeads";
                        asignacionAutomaticaTemp.UsuarioModificacion = "WebHookFacebookLeads";
                        asignacionAutomaticaTemp.FechaCreacion = DateTime.Now;
                        asignacionAutomaticaTemp.FechaModificacion = DateTime.Now;

                        _repFacebookFormularioLeadgen.Insert(facebookFormularioLeadgen);

                        asignacionAutomaticaTemp.IdFacebookFormularioLeadgen = facebookFormularioLeadgen.Id;
                        _repAsignacionAutomaticaTemp.Insert(asignacionAutomaticaTemp);

                        scope.Complete();
                    }
                    return Ok(asignacionAutomaticaTemp.Id);
                }
                catch (Exception ex)
                {
                    facebookFormularioLeadgen.EsProcesado = false;
                    facebookFormularioLeadgen.Excepcion = !string.IsNullOrEmpty(ex.ToString()) ? ex.ToString() : "";
                    //_repFacebookFormularioLeadgen.Insert(facebookFormularioLeadgen);
                    return BadRequest(ex.Message);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}