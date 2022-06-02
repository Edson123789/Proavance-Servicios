using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Servicios.BO;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using MailChimp.Net;
using MailChimp.Net.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MailchimpCreateCampania")]
    [ApiController]
    public class MailchimpCreateCampaniaController : Controller
    {
        private static MailChimpManager Manager = new MailChimpManager("a607090294d386ecee0fdf944962f2f8-us12");
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [Route("[Action]/{IdCampaniaMailingDetalle}")]
        [HttpGet]
        public async Task<ActionResult> Create(int IdCampaniaMailingDetalle)
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
                  
                    try
                    {
                        var preciosCredito = repoMailchimpLista.ObtenerPreciosAlCreditoPorIdDetalle(IdCampaniaMailingDetalle);
                        var preciosContado = repoMailchimpLista.ObtenerPreciosAlContadoPorIdDetalle(IdCampaniaMailingDetalle);
                        var correos = listaIntegra.CrearMiembrosMailchimp(preciosContado, preciosCredito);

                        System.Net.ServicePointManager.Expect100Continue = false;
                        var batchRequest = new BatchRequest
                        {
                            Operations = correos.Select(x => new Operation
                            {
                                Method = "PUT",
                                Path = $"/lists/{listaResult.Id}/members/{Manager.Members.Hash(x.EmailAddress.ToLower())}",
                                Body = JsonConvert.SerializeObject(x, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
                            })
                        };
                        var batch = Manager.Batches.AddAsync(batchRequest).Result;

                        //esperamos hasta que el lote termine
                        while (batch.Status != "finished")
                        {
                            Thread.Sleep(10000);
                            var batchId = batch.Id;
                            batch = Manager.Batches.GetBatchStatus(batchId).Result;
                        }

                    }
                    catch (MailChimpException mce)
                    {
                       
                    }
                    catch (Exception ex)
                    {
                        

                    }

                    CampaniaMailchimpDatosDTO campaniaDatos = new CampaniaMailchimpDatosDTO();
                    campaniaDatos.IdLista = listaResult.Id;
                    campaniaDatos.Asunto = listaIntegra.Asunto;
                    campaniaDatos.AsuntoLista = listaIntegra.AsuntoLista;
                    campaniaDatos.NombreAsesor = listaIntegra.NombreAsesor;
                    campaniaDatos.Alias = listaIntegra.Alias;

                    //var campaniaInsertada = await listaIntegra.CrearCampania(campaniaDatos, listaIntegra.Contenido).Result;
                    var campaniaInsertada = await listaIntegra.CrearCampania(campaniaDatos, listaIntegra.Contenido);

                    if (campaniaInsertada != null)
                    {
                        listaIntegra.IdCampaniaMailchimp = campaniaInsertada.Id;
                        listaIntegra.IdListaMailchimp = listaResult.Id;

                        repoMailchimpLista.Update(listaIntegra);

                    }
                    return Ok();

                }
                else
                {
                    return BadRequest("No se encontro las lista");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}