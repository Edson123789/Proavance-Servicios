using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Servicios.BO;
using System.Transactions;
using MailChimp.Net.Core;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using MailChimp.Net.Models;
using System.Linq;
using BSI.Integra.Aplicacion.Base.BO;
using System.Threading;
using System.Net;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MailChimp")]
    [ApiController]
    public class MailChimpController : Controller
    {
        [Route("[Action]/{IdCampaniaMailingDetalle}")]
        [HttpGet]
        public async Task<ActionResult> Create(int IdCampaniaMailingDetalle, int? cantidadGrupo = 1000)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!cantidadGrupo.HasValue)
                {
                    cantidadGrupo = 1000;
                }

                //Se implementan los repositorios 
                var _mailchimpService = new TMK_MailchimpServiceImpl();
                var _repPrioridadMailChimpLista = new PrioridadMailChimpListaRepositorio();
                var _repMailchimpListaId = new MailChimpListaIdRepositorio();
                var _repPrioridadMailChimpListaCorreo = new PrioridadMailChimpListaCorreoRepositorio();
                var _repMailChimpListaControlSubida = new MailChimpListaControlSubidaRepositorio();
                var prioridadMailChimpLista = new PrioridadMailChimpListaBO();

                //Valida SeSuperoNroMaximoPeticionesPermitidosEnParalelo 
                if (_repMailChimpListaControlSubida.SeSuperoNroMaximoPeticionesPermitidosEnParalelo)
                {
                    return BadRequest("Estamos al limite");
                }

                //Inicializa variales
                List mailchimpList = null;
                Campaign campaign = null;
                List<Member> membersList = new List<Member>();
                MailChimpListaIdBO mailChimpListaId = null;
                MailChimpListaControlSubidaBO controlSubida = new MailChimpListaControlSubidaBO();
                List<string> emailMiembros = new List<string>();
                List<MailChimpListaCorreoDTO> listaPrioridadMandadaASubir = new List<MailChimpListaCorreoDTO>();
                List<PrioridadMailChimpListaCorreoBO> listaPrioridadMailChimpListaCorreoActualizar = new List<PrioridadMailChimpListaCorreoBO>();
                List<PrioridadMailChimpListaCorreoBO> prioridadMailChimpListaCorreo = null;

                //mkt.t_prioridadmailchimplista
                //Obtiene una prioridadMailChimpLista que se asocia al idcampaniamailingdetalle
                prioridadMailChimpLista = _repPrioridadMailChimpLista.PrioridadesMailChimpListaPorMailchimpDetalleNoEnviado(IdCampaniaMailingDetalle);


                //CREAMOS LA LISTA Y CAMPAÑA - TAMBIEN ETIQUETAS Y NECESARIOS
                //A LA LISTA AGREGAMOS LOS DATOS 

                if (prioridadMailChimpLista != null)//valida que exista
                {
                    #region CREACION DE LISTA(AUDIENCIA)

                    //mkt.t_mailchimplistaid
                    //Obtengo mailchimplistaid  por el id de la prioridadmailchimplista
                    mailChimpListaId = _repMailchimpListaId.ObtenerMailChimpListaIdPorLista(prioridadMailChimpLista.Id);

                    if (mailChimpListaId is null) //valida si no existe  
                    {
                        //Crea a Lista(Audencia)
                        mailchimpList = await prioridadMailChimpLista.CrearListaMailchimp();

                        //MailChimpListaId y asociamos el IdListaMailchimp (ejemplo:f33653b30a)
                        MailChimpListaIdBO idsListaNew = new MailChimpListaIdBO
                        {
                            IdCampaniaMailingLista = prioridadMailChimpLista.Id,
                            AsuntoLista = prioridadMailChimpLista.AsuntoLista,
                            IdListaMailchimp = mailchimpList.Id,
                            IdCampaniaMailchimp = null,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = "System Mailchimp",
                            UsuarioModificacion = "System Mailchimp",
                            Estado = true
                        };
                        _repMailchimpListaId.Insert(idsListaNew);

                        //Crea las etiquetas
                        await prioridadMailChimpLista.CrearEtiquetasMailchimp(mailchimpList.Id);
                    }
                    else //si existe solo obtengo la informacion
                    {
                        mailchimpList = prioridadMailChimpLista.GetListAsync(mailChimpListaId.IdListaMailchimp).Result;
                    }
                    #endregion

                    #region CREACION DE CAMPANIA

                    if (prioridadMailChimpLista.IdCampaniaMailchimp is null) //valida si no tiene campaña, la creamos
                    {
                        CampaniaMailchimpDatosDTO campaniaDatos = new CampaniaMailchimpDatosDTO
                        {
                            IdLista = mailchimpList.Id,
                            Asunto = prioridadMailChimpLista.Asunto,
                            AsuntoLista = prioridadMailChimpLista.AsuntoLista,
                            NombreAsesor = prioridadMailChimpLista.NombreAsesor,
                            Alias = prioridadMailChimpLista.Alias
                        };
                        while (campaign is null)
                        {
                            //Crea la campania en mailchimp con su contenido 
                            campaign = await prioridadMailChimpLista.CrearCampania(campaniaDatos, prioridadMailChimpLista.Contenido);

                            //Actualiza los campos  IdCampaniaMailchimp, IdListaMailchimp con los IDS generados en la plataforma
                            prioridadMailChimpLista.IdCampaniaMailchimp = campaign.Id;
                            prioridadMailChimpLista.IdListaMailchimp = mailchimpList.Id;
                            _repPrioridadMailChimpLista.Update(prioridadMailChimpLista);
                        }
                    }
                    #endregion

                    //var cantidadTotalContactos = _repPrioridadMailChimpListaCorreo.ObtenerCantidadListPrioridadMailChimpListaCorreoSinEnviar(prioridadMailChimpLista.Id);

                    //var cantidadRepeticiones = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(cantidadTotalContactos) / cantidadGrupo.Value));

                    //for (int i = 0; i < cantidadRepeticiones; i++)
                    //{
                    prioridadMailChimpListaCorreo = _repPrioridadMailChimpListaCorreo.ObtenerListPrioridadMailChimpListaCorreoSinEnviar(prioridadMailChimpLista.Id);
                    //debo eliminar duplicados
                    prioridadMailChimpListaCorreo = prioridadMailChimpListaCorreo.DistinctBy(x => x.Email1).ToList();

                    prioridadMailChimpLista.PrioridadMailchimpListaCorreo = prioridadMailChimpListaCorreo;

                    //agregamos datos a esa lista

                    var preciosCredito = _repPrioridadMailChimpLista.ObtenerPreciosAlCreditoPorIdDetalle(IdCampaniaMailingDetalle);
                    var preciosContado = _repPrioridadMailChimpLista.ObtenerPreciosAlContadoPorIdDetalle(IdCampaniaMailingDetalle);

                    membersList = prioridadMailChimpLista.CrearMiembrosMailchimp(preciosContado, preciosCredito);

                    var parteBucle = "inicio";
                    //var subidoCorrectamente = false;
                    foreach (var (x, index) in membersList.AsEnumerable().Split(cantidadGrupo.Value).Select((x, i) => (x, i)))
                    {
                        //subidoCorrectamente = false;
                        //emailMiembros = new List<string>();
                        //listaPrioridadMandadaASubir = new List<MailChimpListaCorreoDTO>();
                        //while (_repMailChimpListaControlSubida.SeSuperoNroMaximoPeticionesPermitidosEnParalelo)
                        //{
                        //    Thread.Sleep(1000);
                        //}

                        controlSubida = new MailChimpListaControlSubidaBO()
                        {
                            IdPrioridadMailLista = IdCampaniaMailingDetalle,
                            Grupo = index,
                            FechaInicioProceso = DateTime.Now,
                            FechaFinProceso = DateTime.Now,
                            EnProceso = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = "System Mailchimp",
                            UsuarioModificacion = "System Mailchimp",
                            Estado = true
                        };
                        _repMailChimpListaControlSubida.Insert(controlSubida);
                        parteBucle = "inicio subir contactos";

                        //while (subidoCorrectamente is false)
                        //{

                        var subidoCorrectamente = await prioridadMailChimpLista.SubirListaConMiembros(x.ToList(), mailchimpList);
                        parteBucle = "fin subir contactos";

                        //}
                        //await listaIntegra.SubirListaConMiembros(membersList, mailchimpList);
                        ////actualizo todos los emails que se han subido
                        //emailMiembros = membersList.ToList().Select(y => y.EmailAddress).ToList();

                        //listaPrioridadMandadaASubir = prioridadMailChimpListaCorreo.Where(w => emailMiembros.Contains(w.Email1)).ToList().Select(
                        //    y => new MailChimpListaCorreoDTO
                        //    {
                        //        Id = y.Id,
                        //        Email = y.Email1,
                        //        ExisteEnMailChimp = false
                        //    }
                        //).ToList();

                        //var emailsSubidoCorrectamente = await _mailchimpService.ObtenerContactosPorLista(mailchimpList, listaPrioridadMandadaASubir);

                        //foreach (var item in listaPrioridadMandadaASubir)
                        //{
                        //    var correoEnviadoAMailChimp = _repPrioridadMailChimpListaCorreo.FirstById(item.Id);
                        //    correoEnviadoAMailChimp.FechaModificacion = DateTime.Now;
                        //    if (item.ExisteEnMailChimp)//
                        //    {
                        //        correoEnviadoAMailChimp.EsSubidoCorrectamente = true;
                        //    }
                        //    else
                        //    {
                        //        correoEnviadoAMailChimp.EsSubidoCorrectamente = false;
                        //    }
                        //    _repPrioridadMailChimpListaCorreo.Update(correoEnviadoAMailChimp);
                        //    //listaPrioridadMailChimpListaCorreoActualizar.Add(correoEnviadoAMailChimp);
                        //}
                        //_repPrioridadMailChimpListaCorreo.Update(listaPrioridadMailChimpListaCorreoActualizar);

                        controlSubida = _repMailChimpListaControlSubida.FirstById(controlSubida.Id);
                        controlSubida.FechaFinProceso = DateTime.Now;
                        controlSubida.FechaModificacion = DateTime.Now;
                        if (subidoCorrectamente)
                        {
                            parteBucle = "fin subir contactos";
                            controlSubida.UsuarioModificacion = string.Join("Error en metodo", parteBucle);
                        }
                        else
                        {
                            controlSubida.UsuarioModificacion = string.Join("Ok", parteBucle);
                        }
                        controlSubida.EnProceso = false;
                        _repMailChimpListaControlSubida.Update(controlSubida);
                        //};
                    }
                    //prioridadMailChimpLista.IdCampaniaMailingDetalle

                    prioridadMailChimpLista = _repPrioridadMailChimpLista.FirstById(prioridadMailChimpLista.Id);
                    prioridadMailChimpLista.EsSubidoCorrectamente = true;
                    _repPrioridadMailChimpLista.Update(prioridadMailChimpLista);



                    //var listasIds = repMailchimpListaId.ObtenerIdListasMailchimp(listaIntegra.Id);
                    // TODO-WCHOQUE
                    //if (DateTime.Now.Year == FechaReal.Year && DateTime.Now.TimeOfDay.Days == FechaReal.TimeOfDay.Days && DateTime.Now.TimeOfDay.Hours == FechaReal.TimeOfDay.Hours && DateTime.Now.TimeOfDay.Minutes == FechaReal.TimeOfDay.Minutes)
                    //{
                    //    return BadRequest("ERROR-EXISTE YA LA LISTA");
                    //}

                    //if (idsLista != null)
                    //{
                    //    DateTime FechaReal = idsLista.FechaCreacion;
                    //    if (DateTime.Now.Year == FechaReal.Year && DateTime.Now.TimeOfDay.Days == FechaReal.TimeOfDay.Days && DateTime.Now.TimeOfDay.Hours == FechaReal.TimeOfDay.Hours && DateTime.Now.TimeOfDay.Minutes == FechaReal.TimeOfDay.Minutes)
                    //    {
                    //        return BadRequest("ERROR-EXISTE YA LA LISTA");
                    //    }
                    //    else
                    //    {
                    //        if (listasIds != null)
                    //        {
                    //            foreach (var mailChimp in listasIds)
                    //            {
                    //                mailChimp.Estado = false;
                    //                repMailchimpListaId.Update(mailChimp);
                    //            }
                    //        }
                    //    }
                    //}

                    return Ok();

                }
                else
                {
                    return BadRequest("No se encontro las lista");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                //return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdCampaniaMailingDetalle}")]
        [HttpGet]
        public async Task<ActionResult> CreateListCampaign(int IdCampaniaMailingDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _mailchimpService = new TMK_MailchimpServiceImpl();
                var _repPrioridadMailChimpLista = new PrioridadMailChimpListaRepositorio();
                var _repMailchimpListaId = new MailChimpListaIdRepositorio();
                var _repPrioridadMailChimpListaCorreo = new PrioridadMailChimpListaCorreoRepositorio();
                var _repMailChimpListaControlSubida = new MailChimpListaControlSubidaRepositorio();
                var prioridadMailChimpLista = new PrioridadMailChimpListaBO();

                if (_repMailChimpListaControlSubida.SeSuperoNroMaximoPeticionesPermitidosEnParalelo)
                {
                    return BadRequest("Estamos al limite");
                }

                List mailchimpList = null;
                Campaign campaign = null;
                List<Member> membersList = new List<Member>();
                MailChimpListaIdBO mailChimpListaId = null;
                MailChimpListaControlSubidaBO controlSubida = new MailChimpListaControlSubidaBO();
                List<string> emailMiembros = new List<string>();
                List<MailChimpListaCorreoDTO> listaPrioridadMandadaASubir = new List<MailChimpListaCorreoDTO>();
                List<PrioridadMailChimpListaCorreoBO> listaPrioridadMailChimpListaCorreoActualizar = new List<PrioridadMailChimpListaCorreoBO>();

                List<PrioridadMailChimpListaCorreoBO> prioridadMailChimpListaCorreo = null;

                prioridadMailChimpLista = _repPrioridadMailChimpLista.PrioridadesMailChimpListaPorMailchimpDetalleNoEnviado(IdCampaniaMailingDetalle);
                //CREAMOS LA LISTA Y CAMPAÑA - TAMBIEN ETIQUETAS Y NECESARIOS
                //A LA LISTA AGREGAMOS LOS DATOS 
                //Aqui esta la logica y bucles para determinar si se subio no
                //Primero valida que
                if (prioridadMailChimpLista != null)//valida que no exista
                {
                    mailChimpListaId = _repMailchimpListaId.ObtenerMailChimpListaIdPorLista(prioridadMailChimpLista.Id);

                    if (mailChimpListaId is null) //no existe 
                    {
                        mailchimpList = await prioridadMailChimpLista.CrearListaMailchimp();
                        //creamos solo agregamos la lista y campaña
                        MailChimpListaIdBO idsListaNew = new MailChimpListaIdBO
                        {
                            IdCampaniaMailingLista = prioridadMailChimpLista.Id,
                            AsuntoLista = prioridadMailChimpLista.AsuntoLista,
                            IdListaMailchimp = mailchimpList.Id,
                            IdCampaniaMailchimp = null,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = "System Mailchimp",
                            UsuarioModificacion = "System Mailchimp",
                            Estado = true
                        };
                        _repMailchimpListaId.Insert(idsListaNew);
                        await prioridadMailChimpLista.CrearEtiquetasMailchimp(mailchimpList.Id);
                    }
                    else
                    {
                        mailchimpList = prioridadMailChimpLista.GetListAsync(mailChimpListaId.IdListaMailchimp).Result;
                    }

                    //var campaniaInsertada = await listaIntegra.CrearCampania(campaniaDatos, listaIntegra.Contenido).Result;
                    if (prioridadMailChimpLista.IdCampaniaMailchimp is null) //no tiene campaña, la creamos
                    {
                        CampaniaMailchimpDatosDTO campaniaDatos = new CampaniaMailchimpDatosDTO
                        {
                            IdLista = mailchimpList.Id,
                            Asunto = prioridadMailChimpLista.Asunto,
                            AsuntoLista = prioridadMailChimpLista.AsuntoLista,
                            NombreAsesor = prioridadMailChimpLista.NombreAsesor,
                            Alias = prioridadMailChimpLista.Alias
                        };
                        while (campaign is null)
                        {
                            campaign = await prioridadMailChimpLista.CrearCampania(campaniaDatos, prioridadMailChimpLista.Contenido);
                            prioridadMailChimpLista.IdCampaniaMailchimp = campaign.Id;
                            prioridadMailChimpLista.IdListaMailchimp = mailchimpList.Id;
                            _repPrioridadMailChimpLista.Update(prioridadMailChimpLista);
                        }
                    }
                    return Ok();
                }
                else
                {
                    return BadRequest("No se encontro las lista");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                //return BadRequest(e.Message);
            }
        }


        /// Tipo Función: GET
        /// Autor: Carlos Crispin
        /// Fecha: 26/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Crear las campanias y sube la informacion  de las prioridades (Campaniasgeneraldetalle)
        /// </summary>
        /// <returns>ListaError,List<int></returns>
        [Route("[Action]/{IdCampaniaGeneralDetalle}")]
        [HttpGet]
        public async Task<ActionResult> CreateListCampaignGeneral(int IdCampaniaGeneralDetalle, DateTime FechaEnvio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                int? cantidadGrupo = 1000;

                // Se implementan los repositorios
                var _mailchimpService = new TMK_MailchimpServiceImpl();
                var _repPrioridadMailChimpLista = new PrioridadMailChimpListaRepositorio();
                var _repMailchimpListaId = new MailChimpListaIdRepositorio();
                var _repPrioridadMailChimpListaCorreo = new PrioridadMailChimpListaCorreoRepositorio();
                var _repMailChimpListaControlSubida = new MailChimpListaControlSubidaRepositorio();
                var prioridadMailChimpLista = new PrioridadMailChimpListaBO();

                // Valida SeSuperoNroMaximoPeticionesPermitidosEnParalelo 
                if (_repMailChimpListaControlSubida.SeSuperoNroMaximoPeticionesPermitidosEnParalelo)
                    return BadRequest("Estamos al limite");

                // Inicializa variables 
                List mailchimpList = null;
                Campaign campaign = null;
                List<Member> membersList = new List<Member>();
                MailChimpListaIdBO mailChimpListaId = null;
                MailChimpListaControlSubidaBO controlSubida = new MailChimpListaControlSubidaBO();
                List<string> emailMiembros = new List<string>();
                List<MailChimpListaCorreoDTO> listaPrioridadMandadaASubir = new List<MailChimpListaCorreoDTO>();
                List<PrioridadMailChimpListaCorreoBO> listaPrioridadMailChimpListaCorreoActualizar = new List<PrioridadMailChimpListaCorreoBO>();
                List<PrioridadMailChimpListaCorreoBO> prioridadMailChimpListaCorreo = null;
                // Obtiene una prioridadMailChimpLista que se asocia al idcampaniageneraldetalle
                prioridadMailChimpLista = _repPrioridadMailChimpLista.PrioridadesMailChimpListaPorMailchimpDetalleCampaniaGeneralNoEnviado(IdCampaniaGeneralDetalle);

                // CREAMOS LA LISTA Y CAMPAÑA - TAMBIEN ETIQUETAS Y NECESARIOS
                // A LA LISTA AGREGAMOS LOS DATOS
                // Valida que exista
                if (prioridadMailChimpLista != null)
                {
                    #region CREACION DE LISTA(AUDIENCIA)

                    // Obtengo mailchimplistaid  por el id de la prioridadmailchimplista
                    mailChimpListaId = _repMailchimpListaId.ObtenerMailChimpListaIdPorLista(prioridadMailChimpLista.Id);

                    //valida si no existe
                    if (mailChimpListaId is null)
                    {
                        // Crea a Lista(Audiencia)
                        mailchimpList = prioridadMailChimpLista.CrearListaMailchimp().Result;

                        // MailChimpListaId y asociamos el IdListaMailchimp (ejemplo:f33653b30a)
                        MailChimpListaIdBO idsListaNew = new MailChimpListaIdBO
                        {
                            IdCampaniaMailingLista = prioridadMailChimpLista.Id,
                            AsuntoLista = prioridadMailChimpLista.AsuntoLista,
                            IdListaMailchimp = mailchimpList.Id,//(ejemplo:f33653b30a)
                            IdCampaniaMailchimp = null,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = "System Mailchimp",
                            UsuarioModificacion = "System Mailchimp",
                            Estado = true
                        };
                        _repMailchimpListaId.Insert(idsListaNew);

                        //Crea las etiquetas
                        await prioridadMailChimpLista.CrearEtiquetasMailchimp(mailchimpList.Id).ConfigureAwait(true);
                    }
                    else// si existe solo obtengo la informacion
                    {
                        mailchimpList = prioridadMailChimpLista.GetListAsync(mailChimpListaId.IdListaMailchimp).Result;
                    }
                    #endregion

                    #region SUBIDA DATOS AUDIENCIA

                    //await prioridadMailChimpLista.CrearEtiquetasMailchimp(mailchimpList.Id);
                    var parteBucle = "Inicio";

                    var miembros = _repPrioridadMailChimpListaCorreo.ObtenerListPrioridadMailChimpListaCorreoSinEnviar(prioridadMailChimpLista.Id);
                    prioridadMailChimpLista.PrioridadMailchimpListaCorreo = miembros;
                    var preciosCredito = _repPrioridadMailChimpLista.ObtenerPreciosAlCreditoPorIdDetalleGeneral(IdCampaniaGeneralDetalle);
                    var preciosContado = _repPrioridadMailChimpLista.ObtenerPreciosAlContadoPorIdDetalleGeneral(IdCampaniaGeneralDetalle);
                    var correos = prioridadMailChimpLista.CrearMiembrosMailchimpGeneral(preciosContado, preciosCredito);

                    foreach (var (x, index) in correos.AsEnumerable().Split(cantidadGrupo.Value).Select((x, i) => (x, i)))
                    {
                        controlSubida = new MailChimpListaControlSubidaBO()
                        {
                            IdPrioridadMailLista = IdCampaniaGeneralDetalle,
                            Grupo = index,
                            FechaInicioProceso = DateTime.Now,
                            FechaFinProceso = DateTime.Now,
                            EnProceso = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = "System Mailchimp",
                            UsuarioModificacion = "System Mailchimp",
                            Estado = true
                        };
                        _repMailChimpListaControlSubida.Insert(controlSubida);

                        parteBucle = "Inicio subir contactos";
                        var subidoCorrectamente = prioridadMailChimpLista.SubirListaConMiembros(x.ToList(), mailchimpList).Result;
                        parteBucle = "Fin subir contactos";

                        controlSubida = _repMailChimpListaControlSubida.FirstById(controlSubida.Id);
                        controlSubida.FechaFinProceso = DateTime.Now;
                        controlSubida.FechaModificacion = DateTime.Now;
                        if (subidoCorrectamente)
                        {
                            parteBucle = "Fin subir contactos";
                            controlSubida.UsuarioModificacion = string.Join("Ok", parteBucle);
                        }
                        else
                        {
                            controlSubida.UsuarioModificacion = string.Join("Error en metodo", parteBucle);
                        }
                        controlSubida.EnProceso = false;
                        _repMailChimpListaControlSubida.Update(controlSubida);

                    }
                    prioridadMailChimpLista.EsSubidoCorrectamente = true;
                    //_repPrioridadMailChimpLista.Update(prioridadMailChimpLista);
                    #endregion

                    #region CREACION DE CAMPANIA

                    if (prioridadMailChimpLista.IdCampaniaMailchimp is null) //valida si no tiene campaña, la creamos
                    {
                        CampaniaMailchimpDatosDTO campaniaDatos = new CampaniaMailchimpDatosDTO
                        {
                            IdLista = mailchimpList.Id,
                            Asunto = prioridadMailChimpLista.Asunto,
                            AsuntoLista = prioridadMailChimpLista.AsuntoLista,
                            NombreAsesor = prioridadMailChimpLista.NombreAsesor,
                            Alias = prioridadMailChimpLista.Alias
                        };
                        while (campaign is null)
                        {
                            //Crea la campania en mailchimp con su contenido 
                            campaign = prioridadMailChimpLista.CrearCampania(campaniaDatos, prioridadMailChimpLista.Contenido).Result;

                            //Actualiza los campos  IdCampaniaMailchimp, IdListaMailchimp con los IDS generados en la plataforma
                            prioridadMailChimpLista.IdCampaniaMailchimp = campaign.Id;
                            prioridadMailChimpLista.IdListaMailchimp = mailchimpList.Id;
                            prioridadMailChimpLista.Enviado = true;

                            if (FechaEnvio.Kind != DateTimeKind.Utc)
                                FechaEnvio = FechaEnvio.ToUniversalTime();

                            try
                            {
                                prioridadMailChimpLista.ConfigurarHorario(campaign.Id, new CampaignScheduleRequest() { ScheduleTime = FechaEnvio.ToString("o") }).Wait();
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    _repPrioridadMailChimpLista.Update(prioridadMailChimpLista);
                    #endregion

                    return Ok();
                }
                else
                {
                    return BadRequest("No se encontraron las lista");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                //return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdCampaniaMailingDetalle}")]
        [HttpGet]
        public async Task<ActionResult> CrearCampaniaAutomatica(int IdCampaniaMailingDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TMK_MailchimpServiceImpl MailchimpServiceImpl = new TMK_MailchimpServiceImpl();
                PrioridadMailChimpListaRepositorio repoMailchimpLista = new PrioridadMailChimpListaRepositorio();
                MailChimpListaIdRepositorio repMailchimpListaId = new MailChimpListaIdRepositorio();
                PrioridadMailChimpListaCorreoRepositorio repCorreos = new PrioridadMailChimpListaCorreoRepositorio();
                PrioridadMailChimpListaBO listaIntegra = new PrioridadMailChimpListaBO();
                listaIntegra = repoMailchimpLista.PrioridadesMailChimpListaPorMailchimpDetalleNoEnviado(IdCampaniaMailingDetalle);

                if (listaIntegra != null)
                {
                    var idsLista = repMailchimpListaId.ObtenerMailChimpListaIdPorLista(listaIntegra.Id);
                    if (idsLista != null)
                    {
                        DateTime FechaReal = idsLista.FechaCreacion;
                        if (DateTime.Now.Year == FechaReal.Year && DateTime.Now.TimeOfDay.Days == FechaReal.TimeOfDay.Days && DateTime.Now.TimeOfDay.Hours == FechaReal.TimeOfDay.Hours && DateTime.Now.TimeOfDay.Minutes == FechaReal.TimeOfDay.Minutes)
                        {
                            return BadRequest("ERROR-EXISTE YA LA LISTA");
                        }
                        else
                        {
                            var listasIds = repMailchimpListaId.ObtenerIdListasMailchimp(listaIntegra.Id);
                            if (listasIds != null)
                            {
                                foreach (var mailChimp in listasIds)
                                {
                                    mailChimp.Estado = false;
                                    repMailchimpListaId.Update(mailChimp);
                                }
                            }
                        }
                    }
                    var listaResult = await listaIntegra.CrearListaMailchimp();
                    MailChimpListaIdBO idsListaNew = new MailChimpListaIdBO
                    {
                        IdCampaniaMailingLista = listaIntegra.Id,
                        AsuntoLista = listaIntegra.AsuntoLista,
                        IdListaMailchimp = listaResult.Id,
                        IdCampaniaMailchimp = null,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = "System Mailchimp",
                        UsuarioModificacion = "System Mailchimp",
                        Estado = true
                    };
                    repMailchimpListaId.Insert(idsListaNew);
                    await listaIntegra.CrearEtiquetasMailchimp(listaResult.Id);
                    var miembros = repCorreos.ObtenerListPrioridadMailChimpListaCorreoSinEnviar(listaIntegra.Id, 500);
                    listaIntegra.PrioridadMailchimpListaCorreo = miembros;
                    var preciosCredito = repoMailchimpLista.ObtenerPreciosAlCreditoPorIdDetalle(IdCampaniaMailingDetalle);
                    var preciosContado = repoMailchimpLista.ObtenerPreciosAlContadoPorIdDetalle(IdCampaniaMailingDetalle);
                    var correos = listaIntegra.CrearMiembrosMailchimp(preciosContado, preciosCredito);
                    await listaIntegra.SubirListaConMiembros(correos, listaResult);


                    CampaniaMailchimpDatosDTO campaniaDatos = new CampaniaMailchimpDatosDTO
                    {
                        IdLista = listaResult.Id,
                        Asunto = listaIntegra.Asunto,
                        AsuntoLista = listaIntegra.AsuntoLista,
                        NombreAsesor = listaIntegra.NombreAsesor,
                        Alias = listaIntegra.Alias
                    };

                    //var campaniaInsertada = await listaIntegra.CrearCampania(campaniaDatos, listaIntegra.Contenido).Result;
                    var campaniaInsertada = await listaIntegra.CrearCampania(campaniaDatos, listaIntegra.Contenido);

                    if (campaniaInsertada != null)
                    {
                        listaIntegra.IdCampaniaMailchimp = campaniaInsertada.Id;
                        listaIntegra.IdListaMailchimp = listaResult.Id;
                        listaIntegra.Enviado = true;
                        repoMailchimpLista.Update(listaIntegra);

                    }
                    return Ok();

                }
                else
                {
                    return BadRequest("No se encontro las lista");
                }
            }
            catch (Exception e)
            {
                throw e;
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
		/// Autor: Gian Miranda
		/// Fecha: 08/06/2021
		/// Versión: 1.0
		/// <summary>
		/// Crea la campania por campania integra
		/// </summary>
        /// <param name="IdCampaniaMailing">Id de la campania mailing (PK de la tabla mkt.T_CampaniaMailing)</param>
		/// <returns>Task(IActionResult)</returns>
        [Route("[Action]/{IdCampaniaMailing}")]
        [HttpGet]
        public async Task<IActionResult> CrearCampaniaPorCampaniaIntegra(int IdCampaniaMailing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TMK_MailchimpServiceImpl MailchimpServiceImpl = new TMK_MailchimpServiceImpl();
                PrioridadMailChimpListaRepositorio repMailchimpLista = new PrioridadMailChimpListaRepositorio();
                MailChimpListaIdRepositorio repMailchimpListaId = new MailChimpListaIdRepositorio();
                var listas = repMailchimpLista.PrioridadesMailChimpListaPorIdCampaniaMailing(IdCampaniaMailing);
                int totalCampaniaCreadas = 0;
                foreach (var item in listas)
                {
                    var idsLista = repMailchimpListaId.ObtenerMailChimpListaIdPorLista(item.Id);
                    if (idsLista != null)
                    {
                        CampaniaMailchimpDatosDTO campaniaDatos = new CampaniaMailchimpDatosDTO();
                        campaniaDatos.IdLista = idsLista.IdListaMailchimp;
                        campaniaDatos.Asunto = item.Asunto;
                        campaniaDatos.AsuntoLista = item.AsuntoLista;
                        campaniaDatos.NombreAsesor = item.NombreAsesor;
                        campaniaDatos.Alias = item.Alias;

                        var campaign = MailchimpServiceImpl.CrearEstructuraCampania(campaniaDatos);
                        var campaniaInsertada = await MailchimpServiceImpl.CrearCampania(campaign);

                        var content = new ContentRequest { PlainText = "", Html = item.Contenido };

                        var actualizado = await MailchimpServiceImpl.InsertaContenidoACampania(campaniaInsertada, content);

                        if (campaniaInsertada != null)
                        {
                            item.IdCampaniaMailchimp = campaniaInsertada.Id;
                            item.IdListaMailchimp = idsLista.IdListaMailchimp;
                            repMailchimpLista.Update(item);

                            totalCampaniaCreadas++;
                        }
                    }
                }
                return Ok(totalCampaniaCreadas);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{IdCampaniaMailingDetalle}")]
        [HttpGet]
        public async Task<IActionResult> Send(int IdCampaniaMailingDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TMK_MailchimpServiceImpl MailchimpServiceImpl = new TMK_MailchimpServiceImpl();
                PrioridadMailChimpListaRepositorio _repPrioridadesLista = new PrioridadMailChimpListaRepositorio();
                var prioridadLista = _repPrioridadesLista.PrioridadesMailChimpListaPorMailchimpDetalle(IdCampaniaMailingDetalle);

                if (prioridadLista != null)
                {
                    try
                    {
                        var enviarCampania = MailchimpServiceImpl.EnviarCampania(prioridadLista.IdCampaniaMailchimp);
                        bool estadoEnvio = enviarCampania.Result;

                        if (estadoEnvio)
                        {
                            prioridadLista.Enviado = true;
                            prioridadLista.FechaEnvio = DateTime.Now;
                            _repPrioridadesLista.Update(prioridadLista);
                            return Ok(IdCampaniaMailingDetalle);
                        }
                        else
                        {
                            return BadRequest("Error Envio Campania");
                        }
                    }
                    catch (MailChimpException mce)
                    {
                        return BadRequest(mce.Message);

                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);

                    }
                }
                return BadRequest("No se pudo encontrar la lista para enviar");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{IdCampaniaMailingDetalle}")]
        [HttpGet]
        public async Task<IActionResult> EnviarMensajesMailing(int IdCampaniaMailingDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TMK_MailchimpServiceImpl MailchimpServiceImpl = new TMK_MailchimpServiceImpl();
                PrioridadMailChimpListaRepositorio _repPrioridadesLista = new PrioridadMailChimpListaRepositorio();
                var prioridadLista = _repPrioridadesLista.PrioridadesMailChimpListaPorMailchimpDetalleAutomatico(IdCampaniaMailingDetalle);

                if (prioridadLista != null)
                {
                    try
                    {
                        var enviarCampania = MailchimpServiceImpl.EnviarCampania(prioridadLista.IdCampaniaMailchimp);
                        bool estadoEnvio = enviarCampania.Result;

                        if (estadoEnvio)
                        {
                            prioridadLista.Enviado = true;
                            prioridadLista.FechaEnvio = DateTime.Now;
                            _repPrioridadesLista.Update(prioridadLista);
                            return Ok(IdCampaniaMailingDetalle);
                        }
                        else
                        {
                            return BadRequest("Error Envio Campania");
                        }
                    }
                    catch (MailChimpException mce)
                    {
                        return BadRequest(mce.Message);

                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);

                    }
                }
                return BadRequest("No se pudo encontrar la lista para enviar");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{IdCampaniaGeneralDetalle}")]
        [HttpGet]
        public async Task<IActionResult> EnviarMensajesMailingGeneral(int IdCampaniaGeneralDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TMK_MailchimpServiceImpl MailchimpServiceImpl = new TMK_MailchimpServiceImpl();
                PrioridadMailChimpListaRepositorio _repPrioridadesLista = new PrioridadMailChimpListaRepositorio();
                CampaniaGeneralRepositorio _repCampaniaGeneral = new CampaniaGeneralRepositorio();
                CampaniaGeneralDetalleRepositorio _repCampaniaGeneralDetalle = new CampaniaGeneralDetalleRepositorio();

                var prioridadLista = _repPrioridadesLista.PrioridadesMailChimpListaPorMailchimpDetalleCampaniaGeneralAutomatico(IdCampaniaGeneralDetalle);

                if (prioridadLista != null)
                {
                    try
                    {
                        if (prioridadLista.IdCampaniaGeneralDetalle == null)
                        {
                            return BadRequest("La prioridad no tiene campania detalle");
                        }

                        var campaniaGeneralDetalle = _repCampaniaGeneralDetalle.FirstById(prioridadLista.IdCampaniaGeneralDetalle.GetValueOrDefault());

                        if (campaniaGeneralDetalle == null)
                        {
                            return BadRequest("No existe un detalle de campania general con ese Id");
                        }

                        var campaniaGeneral = _repCampaniaGeneral.FirstById(campaniaGeneralDetalle.IdCampaniaGeneral);

                        if (campaniaGeneral == null)
                        {
                            return BadRequest("No existe una campania general con ese Id");
                        }

                        CampaignScheduleRequest configuracionHorario = new CampaignScheduleRequest
                        {
                            ScheduleTime = campaniaGeneral.FechaEnvio.ToString(),
                            Timewarp = false,
                            BatchDelivery = null
                        };

                        //var enviarCampania = MailchimpServiceImpl.EnviarCampania(prioridadLista.IdCampaniaMailchimp);
                        var enviarCampania = MailchimpServiceImpl.EnviarCampaniaHorario(prioridadLista.IdCampaniaMailchimp, configuracionHorario);
                        bool estadoEnvio = enviarCampania.Result;

                        if (estadoEnvio)
                        {
                            prioridadLista.Enviado = true;
                            prioridadLista.FechaEnvio = DateTime.Now;
                            _repPrioridadesLista.Update(prioridadLista);
                            return Ok(IdCampaniaGeneralDetalle);
                        }
                        else
                        {
                            return BadRequest("Error Envio Campania General");
                        }
                    }
                    catch (MailChimpException mce)
                    {
                        return BadRequest(mce.Message);

                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);

                    }
                }
                return BadRequest("No se pudo encontrar la lista para enviar");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult DescargadeCampanias()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PrioridadMailChimpListaInteraccionRepositorio _repInteracciones = new PrioridadMailChimpListaInteraccionRepositorio();
                PrioridadMailChimpListaRepositorio _repListasMailchimp = new PrioridadMailChimpListaRepositorio();
                ConjuntoPrioridadMailChimpListaInteraccionBO interaccionesLista = new ConjuntoPrioridadMailChimpListaInteraccionBO();
                interaccionesLista.Interacciones = _repInteracciones.PrioridadesListaInteraccionesCincoDias();
                interaccionesLista.ListasMailchimp = _repListasMailchimp.PrioridadesMailChimpListaPorCincoDias();
                if (!interaccionesLista.HasErrors)
                {
                    var result = interaccionesLista.InsertarOActualizarTPrioridadMailChimpListaInteraccion();
                    return Ok();
                }
                else
                {
                    return BadRequest(interaccionesLista.GetErrors(null));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]/{FechaInicio}/{FechaFin}")]
        [HttpGet]
        public IActionResult DescargadeCampaniasByFecha(DateTime FechaInicio, DateTime FechaFin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PrioridadMailChimpListaInteraccionRepositorio _repInteracciones = new PrioridadMailChimpListaInteraccionRepositorio();
                PrioridadMailChimpListaRepositorio _repListasMailchimp = new PrioridadMailChimpListaRepositorio();
                ConjuntoPrioridadMailChimpListaInteraccionBO interaccionesLista = new ConjuntoPrioridadMailChimpListaInteraccionBO();
                interaccionesLista.Interacciones = _repInteracciones.PrioridadesListaInteraccionesPorRango(FechaInicio, FechaFin);
                interaccionesLista.ListasMailchimp = _repListasMailchimp.PrioridadesMailChimpListaPorFecha(FechaInicio, FechaFin);
                if (!interaccionesLista.HasErrors)
                {
                    var result = interaccionesLista.InsertarOActualizarTPrioridadMailChimpListaInteraccion();
                    return Ok();
                }
                else
                {
                    return BadRequest(interaccionesLista.GetErrors(null));
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// TipoFuncion: POST
		/// Autor: Gian Miranda
		/// Fecha: 08/06/2021
		/// Versión: 1.0
		/// <summary>
		/// Descarga los desuscritos de una fecha especifica
		/// </summary>
        /// <param name="Fechas">Objeto de clase ParametroFechasDTO de un rango</param>
		/// <returns>Task(IActionResult)</returns>
        [Route("[Action]")]
        [HttpPost]
        public async Task<IActionResult> DescargarUnsucriberAsync([FromBody] ParametroFechasDTO Fechas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TMK_MailchimpServiceImpl MailchimpServiceImpl = new TMK_MailchimpServiceImpl();
                PrioridadMailChimpListaRepositorio _repPrioridadesLista = new PrioridadMailChimpListaRepositorio();
                UnsuscribedMailchimpRepositorio _repoUnsuscribedMailchim = new UnsuscribedMailchimpRepositorio();
                int countSuscriptores = 0;

                List<PrioridadMailChimpListaBO> prioridadesLista = _repPrioridadesLista.PrioridadesMailChimpListaPorFecha(Fechas.FechaInicio, Fechas.FechaFin);
                foreach (var lista in prioridadesLista)
                {
                    var unsubscribes = await MailchimpServiceImpl.GetUnsubscribes(lista.IdCampaniaMailchimp);
                    foreach (var unsuscriber in unsubscribes)
                    {

                        var existeCorreo = _repoUnsuscribedMailchim.ValidadExisteEmail(unsuscriber.EmailAddress);
                        if (!existeCorreo)
                        {
                            using (TransactionScope scope = new TransactionScope())
                            {

                                UnsuscribedMailchimpBO unsuscribedMailchimp = new UnsuscribedMailchimpBO
                                {
                                    CampaniaId = unsuscriber.CampaignId,
                                    Email = unsuscriber.EmailAddress,
                                    EmailId = unsuscriber.EmailId,
                                    ListaId = unsuscriber.ListId,
                                    Reason = unsuscriber.Reason,
                                    FechaUnsuscribed = unsuscriber.Timestamp,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = "jcayo",
                                    UsuarioModificacion = "jcayo",
                                    Estado = true
                                };
                                _repoUnsuscribedMailchim.Insert(unsuscribedMailchimp);
                                scope.Complete();
                                countSuscriptores++;
                            }
                        }

                    }
                }
                _repoUnsuscribedMailchim.ActualizarAlumnoDesuscritos();
                return Ok(countSuscriptores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public async Task<IActionResult> RegularizarDesuscritosIntervaloFecha([FromBody] FechaIntervaloMailingDTO IntervaloFechaFiltro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repLog = new LogRepositorio();
                var MailchimpServiceImpl = new TMK_MailchimpServiceImpl();
                var _repPrioridadesLista = new PrioridadMailChimpListaRepositorio();
                var _repUnsuscribedMailchimp = new UnsuscribedMailchimpRepositorio();

                var horaInicio = DateTime.Now;
                int countSuscriptores = 0;

                var prioridadesLista = _repPrioridadesLista.PrioridadesMailChimpPorIntervaloFecha(IntervaloFechaFiltro.FechaInicio, IntervaloFechaFiltro.FechaFin);

                foreach (var lista in prioridadesLista)
                {
                    try
                    {
                        var unsubscribes = await MailchimpServiceImpl.GetUnsubscribes(lista.Valor);

                        // Verificar correos ya desuscritos
                        var correosYaDesuscritos = _repUnsuscribedMailchimp.GetBy(x => unsubscribes.Select(s => s.EmailAddress).Contains(x.Email)).Select(ss => ss.Email).ToList();

                        // Filtrar solo los nuevos correos
                        var desuscritosNuevos = unsubscribes.Where(x => !correosYaDesuscritos.Contains(x.EmailAddress)).ToList();

                        foreach (var unsuscriber in desuscritosNuevos)
                        {
                            try
                            {
                                using (TransactionScope scope = new TransactionScope())
                                {
                                    UnsuscribedMailchimpBO unsuscribedMailchimp = new UnsuscribedMailchimpBO
                                    {
                                        CampaniaId = unsuscriber.CampaignId,
                                        Email = unsuscriber.EmailAddress,
                                        EmailId = unsuscriber.EmailId,
                                        ListaId = unsuscriber.ListId,
                                        Reason = unsuscriber.Reason,
                                        FechaUnsuscribed = unsuscriber.Timestamp,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = "jcayo",
                                        UsuarioModificacion = "jcayo",
                                        Estado = true
                                    };
                                    _repUnsuscribedMailchimp.Insert(unsuscribedMailchimp);
                                    scope.Complete();
                                    countSuscriptores++;
                                }
                            }
                            catch (Exception ex)
                            {
                                try
                                {
                                    _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "RegulizarDesuscrito", Parametros = $"EmailAddress={unsuscriber.EmailAddress}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "SEND", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "RegulizarListaDesuscritos", Parametros = $"IdCampaniaMailchimp={lista.Valor}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "SEND", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

                _repUnsuscribedMailchimp.ActualizarAlumnoDesuscritos();

                DateTime horaFin = DateTime.Now;

                // Envio de correo
                List<string> correosPersonalizados = new List<string>
                {
                    "gmiranda@bsginstitute.com"
                };
                TMK_MailServiceImpl MailservicePersonalizado = new TMK_MailServiceImpl();
                var campaniaGeneralDetalle = new CampaniaGeneralDetalleBO();
                TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = string.Join(",", correosPersonalizados),
                    Subject = string.Concat("Descarga de desuscritos Intervalo - MAILING"),
                    Message = campaniaGeneralDetalle.GenerarPlantillaDescargaDesuscritos(horaInicio, horaFin, countSuscriptores),
                    Cc = string.Empty,
                    Bcc = string.Empty,
                    AttachedFiles = null
                };
                MailservicePersonalizado.SetData(mailDataPersonalizado);
                MailservicePersonalizado.SendMessageTask();

                return Ok(countSuscriptores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public async Task<IActionResult> RegularizarDesuscritosProcedimientoAlmacenado()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repLog = new LogRepositorio();
                var MailchimpServiceImpl = new TMK_MailchimpServiceImpl();
                var _repPrioridadesLista = new PrioridadMailChimpListaRepositorio();
                var _repUnsuscribedMailchimp = new UnsuscribedMailchimpRepositorio();

                var horaInicio = DateTime.Now;
                int countSuscriptores = 0;

                var prioridadesLista = _repPrioridadesLista.PrioridadesMailChimpPorProcedimientoAlmacenado();

                foreach (var lista in prioridadesLista)
                {
                    try
                    {
                        var unsubscribes = await MailchimpServiceImpl.GetUnsubscribes(lista.Valor);

                        // Verificar correos ya desuscritos
                        var correosYaDesuscritos = _repUnsuscribedMailchimp.GetBy(x => unsubscribes.Select(s => s.EmailAddress).Contains(x.Email)).Select(ss => ss.Email).ToList();

                        // Filtrar solo los nuevos correos
                        var desuscritosNuevos = unsubscribes.Where(x => !correosYaDesuscritos.Contains(x.EmailAddress)).ToList();

                        foreach (var unsuscriber in desuscritosNuevos)
                        {
                            try
                            {
                                using (TransactionScope scope = new TransactionScope())
                                {
                                    UnsuscribedMailchimpBO unsuscribedMailchimp = new UnsuscribedMailchimpBO
                                    {
                                        CampaniaId = unsuscriber.CampaignId,
                                        Email = unsuscriber.EmailAddress,
                                        EmailId = unsuscriber.EmailId,
                                        ListaId = unsuscriber.ListId,
                                        Reason = unsuscriber.Reason,
                                        FechaUnsuscribed = unsuscriber.Timestamp,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = "jcayo",
                                        UsuarioModificacion = "jcayo",
                                        Estado = true
                                    };
                                    _repUnsuscribedMailchimp.Insert(unsuscribedMailchimp);
                                    scope.Complete();
                                    countSuscriptores++;
                                }
                            }
                            catch (Exception ex)
                            {
                                try
                                {
                                    _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "RegulizarDesuscrito", Parametros = $"EmailAddress={unsuscriber.EmailAddress}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "SEND", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "RegulizarListaDesuscritos", Parametros = $"IdCampaniaMailchimp={lista.Valor}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "SEND", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

                _repUnsuscribedMailchimp.ActualizarAlumnoDesuscritos();

                DateTime horaFin = DateTime.Now;

                // Envio de correo
                List<string> correosPersonalizados = new List<string>
                {
                    "gmiranda@bsginstitute.com"
                };
                TMK_MailServiceImpl MailservicePersonalizado = new TMK_MailServiceImpl();
                var campaniaGeneralDetalle = new CampaniaGeneralDetalleBO();
                TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = string.Join(",", correosPersonalizados),
                    Subject = string.Concat("Descarga de desuscritos Intervalo - MAILING"),
                    Message = campaniaGeneralDetalle.GenerarPlantillaDescargaDesuscritos(horaInicio, horaFin, countSuscriptores),
                    Cc = string.Empty,
                    Bcc = string.Empty,
                    AttachedFiles = null
                };
                MailservicePersonalizado.SetData(mailDataPersonalizado);
                MailservicePersonalizado.SendMessageTask();

                return Ok(countSuscriptores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public async Task<IActionResult> DescargarUnsucriberCampaniGeneralAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TMK_MailchimpServiceImpl MailchimpServiceImpl = new TMK_MailchimpServiceImpl();
                PrioridadMailChimpListaRepositorio _repPrioridadesLista = new PrioridadMailChimpListaRepositorio();
                UnsuscribedMailchimpRepositorio _repUnsuscribedMailchimp = new UnsuscribedMailchimpRepositorio();

                DateTime horaInicio = DateTime.Now;
                int countSuscriptores = 0;

                List<PrioridadMailChimpListaBO> prioridadesLista = _repPrioridadesLista.PrioridadesMailChimpListaCampaniaGeneral();
                foreach (var lista in prioridadesLista)
                {
                    var unsubscribes = await MailchimpServiceImpl.GetUnsubscribes(lista.IdCampaniaMailchimp);

                    // Verificar correos ya desuscritos
                    var correosYaDesuscritos = _repUnsuscribedMailchimp.GetBy(x => unsubscribes.Select(s => s.EmailAddress).Contains(x.Email)).Select(ss => ss.Email).ToList();

                    var desuscritosNuevos = unsubscribes.Where(x => !correosYaDesuscritos.Contains(x.EmailAddress)).ToList();

                    if (desuscritosNuevos.Any())
                    {
                        foreach (var unsuscriber in desuscritosNuevos)
                        {
                            using (TransactionScope scope = new TransactionScope())
                            {
                                UnsuscribedMailchimpBO unsuscribedMailchimp = new UnsuscribedMailchimpBO
                                {
                                    CampaniaId = unsuscriber.CampaignId,
                                    Email = unsuscriber.EmailAddress,
                                    EmailId = unsuscriber.EmailId,
                                    ListaId = unsuscriber.ListId,
                                    Reason = unsuscriber.Reason,
                                    FechaUnsuscribed = unsuscriber.Timestamp,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = "jcayo",
                                    UsuarioModificacion = "jcayo",
                                    Estado = true
                                };
                                _repUnsuscribedMailchimp.Insert(unsuscribedMailchimp);
                                scope.Complete();
                                countSuscriptores++;
                            }
                        }
                    }
                }

                _repUnsuscribedMailchimp.ActualizarAlumnoDesuscritos();

                DateTime horaFin = DateTime.Now;

                // Envio de correo
                List<string> correosPersonalizados = new List<string>
                {
                    "ghuaylla@bsginstitute.com",
                    "gmiranda@bsginstitute.com"
                };
                TMK_MailServiceImpl MailservicePersonalizado = new TMK_MailServiceImpl();
                var campaniaGeneralDetalle = new CampaniaGeneralDetalleBO();
                TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = string.Join(",", correosPersonalizados),
                    Subject = string.Concat("Descarga de desuscritos - MAILING"),
                    Message = campaniaGeneralDetalle.GenerarPlantillaDescargaDesuscritos(horaInicio, horaFin, countSuscriptores),
                    Cc = string.Empty,
                    Bcc = string.Empty,
                    AttachedFiles = null
                };
                MailservicePersonalizado.SetData(mailDataPersonalizado);
                MailservicePersonalizado.SendMessageTask();

                return Ok(countSuscriptores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerMailingDetalleParaSubirListas()
        {
            try
            {
                var _repMailChimpListaControlSubida = new MailChimpListaControlSubidaRepositorio();
                if (_repMailChimpListaControlSubida.SeSuperoNroMaximoPeticionesPermitidosEnParalelo)
                {
                    return BadRequest("Estamos al limite");
                }
                var _repCampaniaMailingDetalle = new CampaniaMailingDetalleRepositorio();
                return Ok(_repCampaniaMailingDetalle.ObtenerCampaniaMailingDetalleParaSubirListas());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerMailingDetalleParaSubirListasAutomaticas()
        {

            try
            {
                CampaniaMailingDetalleRepositorio repDetalles = new CampaniaMailingDetalleRepositorio();
                var lista = repDetalles.ObtenerCampaniaMailingDetalleParaSubirListasAutomaticas();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerMailingDetalleParaEnviar()
        {

            try
            {
                CampaniaMailingDetalleRepositorio repDetalles = new CampaniaMailingDetalleRepositorio();
                var lista = repDetalles.ObtenerCampaniaMailingDetalleParaEnviar();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("[Action]")]
        [HttpPost]
        public IActionResult ActualizarEstadoEnvio([FromBody] CampaniaDetalleEnvioDTO Json)
        {

            try
            {
                CampaniaMailingDetalleRepositorio repDetalles = new CampaniaMailingDetalleRepositorio();
                CampaniaMailingDetalleBO detalle = new CampaniaMailingDetalleBO();

                if (repDetalles.Exist(Json.IdCampaniaMailingDetalle))
                {
                    detalle = repDetalles.FirstById(Json.IdCampaniaMailingDetalle);
                    detalle.EstadoEnvio = 1;
                    detalle.FechaModificacion = DateTime.Now;
                    repDetalles.Update(detalle);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]/{IdCampaniaMailing}")]
        [HttpGet]
        public IActionResult CrearCampaniaListaMailChimpPorCampaniaMailing(int IdCampaniaMailing)
        {

            try
            {
                var listaError = new ListError();
                var listaCorrecta = new List<int>();

                var _repCampaniaMailingDetalle = new CampaniaMailingDetalleRepositorio();

                var listaCampaniaParaSubir = _repCampaniaMailingDetalle.ObtenerPorCampaniaMailing(IdCampaniaMailing);
                foreach (var item in listaCampaniaParaSubir)
                {
                    try
                    {
                        string URI = "https://integrav4-servicios.bsginstitute.com/api/MailChimp/CreateListCampaign/" + item.Id;
                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            wc.DownloadString(URI);
                        }

                        listaCorrecta.Add(item.Id);
                    }
                    catch (Exception e)
                    {
                        listaError.AgregarError(new Error(0, e.Message, JsonConvert.SerializeObject(e)));
                    }
                }
                return Ok(new { listaError, listaCorrecta });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// Tipo Función: GET
        /// Autor: Carlos Crispin
        /// Fecha: 26/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las campaniasgeneraldetalle y llama a crearlas y subirlas
        /// </summary>
        /// <param name="IdCampaniaGeneral">Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)</param>
        /// <returns>ListaError,List<int></returns>
        [Route("[Action]/{IdCampaniaGeneral}/{Usuario}")]
        [HttpGet]
        public IActionResult CrearCampaniaListaMailChimpPorCampaniaGeneral(int IdCampaniaGeneral, string Usuario)
        {
            try
            {
                DateTime horaInicio = DateTime.Now;
                var listaError = new ListError();
                var listaCorrecta = new List<int>();
                var _repHora = new HoraRepositorio();

                var _repCampaniaGeneral = new CampaniaGeneralRepositorio();
                var _repCampaniaGeneralDetalle = new CampaniaGeneralDetalleRepositorio();
                IntegraAspNetUsersRepositorio _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio();

                if (!_repIntegraAspNetUsers.ExistePorNombreUsuario(Usuario))
                {
                    return BadRequest("El usuario no existe");
                }

                //Obtiene lista de detalles de la campania general (prioridades)
                var listaCampaniaParaSubir = _repCampaniaGeneralDetalle.ObtenerPorCampaniaGeneral(IdCampaniaGeneral);

                foreach (var campaniaParaSubir in listaCampaniaParaSubir)
                {
                    if (campaniaParaSubir.CantidadContactosMailing == null)
                        return BadRequest("Una o mas prioridades no han sido procesadas");
                }

                // Obtener fecha
                var campaniaGeneral = _repCampaniaGeneral.FirstBy(x => x.Id == IdCampaniaGeneral);

                if (campaniaGeneral.FechaEnvio == null)
                {
                    return BadRequest("No se ha configurado una fecha de envio valida");
                }
                if (campaniaGeneral.IdHoraEnvioMailing == null)
                {
                    return BadRequest("No se ha configurado una hora de envio valida");
                }

                if (campaniaGeneral.IncluirRebotes != true)
                {
                    _repCampaniaGeneral.EliminarRebotesPorIdCampaniaGeneral(IdCampaniaGeneral);
                }

                var hora = _repHora.FirstBy(x => x.Id == campaniaGeneral.IdHoraEnvioMailing);

                var fechaConcatenada = campaniaGeneral.FechaEnvio.Value.Add(hora.Nombre);

                if (DateTime.Now > fechaConcatenada)
                {
                    return BadRequest("Configurar una fecha y hora de envio posterior al momento actual");
                }

                // Itera cada CampaniaGeneralDetalle
                foreach (var item in listaCampaniaParaSubir)
                {
                    try
                    {
                        var resultado = CreateListCampaignGeneral(item.Id, fechaConcatenada).Result;

                        listaCorrecta.Add(item.Id);
                    }
                    catch (Exception e)
                    {
                        listaError.AgregarError(new Error(0, e.Message, JsonConvert.SerializeObject(e)));
                    }
                }

                campaniaGeneral.IdEstadoEnvioMailing = 2;
                _repCampaniaGeneral.Update(campaniaGeneral);

                DateTime horaFin = DateTime.Now;

                string usuarioResponsable = _repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(Usuario);

                // Envio de correo
                List<string> correosCopia = new List<string>
                {
                    "gmiranda@bsginstitute.com"
                };

                TMK_MailServiceImpl MailservicePersonalizado = new TMK_MailServiceImpl();
                var campaniaGeneralDetalle = new CampaniaGeneralDetalleBO();
                TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = usuarioResponsable,
                    Subject = string.Concat("Subida de campania a Mailchimp - MAILING"),
                    Message = campaniaGeneralDetalle.GenerarPlantillaSubidaMailchimp(horaInicio, horaFin),
                    Cc = string.Empty,
                    Bcc = string.Join(",", correosCopia),
                    AttachedFiles = null
                };
                MailservicePersonalizado.SetData(mailDataPersonalizado);
                MailservicePersonalizado.SendMessageTask();

                return Ok(new { listaError, listaCorrecta });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}