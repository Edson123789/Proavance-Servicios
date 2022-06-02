using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Servicios.BO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class ConjuntoPrioridadMailChimpListaInteraccionBO : BaseBO
    {
        public List<PrioridadMailChimpListaInteraccionBO> Interacciones {get;set;}
        public List<PrioridadMailChimpListaBO> ListasMailchimp { get; set; }

        private TMK_MailchimpServiceImpl _mailchimpServiceImpl;
        private PrioridadMailChimpListaInteraccionRepositorio _repInteracciones;
        public ConjuntoPrioridadMailChimpListaInteraccionBO()
        {
            _mailchimpServiceImpl = new TMK_MailchimpServiceImpl();
            _repInteracciones = new PrioridadMailChimpListaInteraccionRepositorio();
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
        public Dictionary<int, PrioridadMailChimpListaInteraccionBO>  crearDiccionarioInteracciones()
        {
            Dictionary<int, PrioridadMailChimpListaInteraccionBO> diccionarioGuardado = new Dictionary<int, PrioridadMailChimpListaInteraccionBO>();

            try
            {
                foreach (var gua in Interacciones)
                {
                    diccionarioGuardado.Add(gua.IdPrioridadMailChimpLista, gua);
                }
                return diccionarioGuardado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertarOActualizarTPrioridadMailChimpListaInteraccion()
        {
            var dictGuardado = crearDiccionarioInteracciones();
            try
            {

                foreach (var item in ListasMailchimp)
                {
                    try
                    {
                        var model = await _mailchimpServiceImpl.GetCampania(item.IdCampaniaMailchimp);
                        var model2 = await _mailchimpServiceImpl.GetReporte(item.IdCampaniaMailchimp);

                        if (!dictGuardado.ContainsKey(item.Id))
                        {

                            PrioridadMailChimpListaInteraccionBO nuevo = new PrioridadMailChimpListaInteraccionBO
                            {
                                IdPrioridadMailChimpLista = item.Id,
                                OpenRate = model2.Opens.OpenRate,
                                Opens = model2.Opens.OpensTotal,
                                ClickRate = model2.Clicks.ClickRate,
                                Clicks = model2.Clicks.ClicksTotal,
                                SubscriberClicks = model2.Clicks.UniqueSubscriberClicks,
                                UniqueOpens = model2.Opens.UniqueOpens,
                                MemberCount = model2.EmailsSent,
                                CleanedCount = model2.Bounces.HardBounces + model2.Bounces.SoftBounces,

                                EmailSend = model.EmailsSent,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = "hvelasco",
                                UsuarioModificacion = "hvelasco",
                                Estado = true

                            };

                            _repInteracciones.Insert(nuevo);

                        }
                        else
                        {
                            var guardado = dictGuardado[item.Id];

                            if (
                                guardado.Opens < model.ReportSummary.Opens
                                || guardado.Clicks < model.ReportSummary.Clicks
                                || guardado.UniqueOpens < model.ReportSummary.UniqueOpens
                                )
                            {
                                guardado.OpenRate = model2.Opens.OpenRate;
                                guardado.Opens = model2.Opens.OpensTotal;
                                guardado.ClickRate = model2.Clicks.ClickRate;
                                guardado.Clicks = model2.Clicks.ClicksTotal;
                                guardado.SubscriberClicks = model2.Clicks.UniqueSubscriberClicks;
                                guardado.UniqueOpens = model2.Opens.UniqueOpens;
                                guardado.MemberCount = model2.EmailsSent;
                                guardado.CleanedCount = model2.Bounces.HardBounces + model2.Bounces.SoftBounces;

                                guardado.FechaModificacion = DateTime.Now;
                                guardado.UsuarioModificacion = "System";

                                _repInteracciones.Update(guardado);
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }
    }
}
