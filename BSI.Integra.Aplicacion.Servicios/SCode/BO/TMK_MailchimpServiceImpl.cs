using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using MailChimp.Net;
using MailChimp.Net.Core;
using MailChimp.Net.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Servicios.BO
{
    public class TMK_MailchimpServiceImpl
    {
        private static MailChimpManager Manager;
        //private static MailChimpManager Manager = new MailChimpManager("a607090294d386ecee0fdf944962f2f8-us12");
        //private static MailChimpManager Manager = new MailChimpManager("cbbccefa80508ca32fa1249beb10229e-us7");

        public TMK_MailchimpServiceImpl()
        {
            Manager = new MailChimpManager("a607090294d386ecee0fdf944962f2f8-us12");
            var actionObj = new Action<MailChimpOptions>(obj => obj.Limit = 100000);
            Manager.Configure(actionObj);
        }

        /// <summary>
        /// Crea la lista en la plataforma Mailchimp
        /// </summary>
        /// <param name="Lista">Objeto de clase List</param>
        /// <returns>Objeto de clase Task <List></returns>
        public async Task<List> CrearLista(List Lista)
        {
            try
            {
                var newLista = Manager.Lists.AddOrUpdateAsync(Lista).Result;

                return newLista;
            }
            catch (Exception Ex)
            {
                throw new ArgumentException("Error Crear Lista", Ex);
            }
        }

        /// <summary>
        /// Obtiene la lista por campaña
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        public async Task<List> GetListAsync(string listId)
        {
            try
            {
                return await Manager.Lists.GetAsync(listId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Obtiene todos los miembros por lista
        /// </summary>
        /// <param name="listId">Id de la lista de Mailchimp</param>
        /// <param name="memberRequest">Objeto de clase MemberRequest</param>
        /// <returns>Objeto de clase Task(IEnumerable(Member))</returns>
        public async Task<IEnumerable<Member>> GetAllAsync(string listId, MemberRequest memberRequest = null)
        {
            try
            {
                return Manager.Members.GetAllAsync(listId, memberRequest).Result;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Crea etiquetas en la plataforma Mailchimp
        /// </summary>
        /// <param name="IdLista"></param>
        /// <param name="Etiqueta"></param>
        /// <returns></returns>
        public async Task<MergeField> CrearEtiqueta(string IdLista, MergeField Etiqueta)
        {
            try
            {
                var NewEtiqueta = await Manager.MergeFields.AddAsync(IdLista, Etiqueta);
                return NewEtiqueta;
            }
            catch (Exception Ex)
            {
                throw new ArgumentException("Error Crear Etiquetas", Ex);
            }
        }

        /// <summary>
        /// Sube las listas a la plataforma Mailchimp
        /// </summary>
        /// <param name="ListaMiembros">Lista de objetos de clase Member</param>
        /// <param name="Lista">Objeto de clase List</param>
        /// <returns>Task (bool)</returns>
        public async Task<bool> SubirLista(List<Member> ListaMiembros, List Lista)
        {
            try
            {
                try
                {
                    try
                    {
                        System.Net.ServicePointManager.Expect100Continue = false;
                        var batchRequest = new BatchRequest
                        {
                            Operations = ListaMiembros.Select(x => new Operation
                            {
                                Method = "PUT",
                                Path = $"/lists/{Lista.Id}/members/{Manager.Members.Hash(x.EmailAddress.ToLower())}",
                                Body = JsonConvert.SerializeObject(x, Formatting.None, new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore
                                })
                            })
                        };

                        var batch = Manager.Batches.AddAsync(batchRequest).Result;
                        //Manager.Batches.AddAsync(batchRequest).Wait(300000);
                        //esperamos hasta que el lote termine
                        while (batch.Status != "finished")
                        {
                            //Thread.Sleep(10000);
                            Thread.Sleep(2000);
                            var batchId = batch.Id;
                            batch = Manager.Batches.GetBatchStatus(batchId).Result;
                        }
                        return true;
                    }
                    catch (TaskCanceledException e)
                    {
                        throw e;
                    }
                }
                catch (AggregateException e)
                {
                    throw e;
                }
            }
            catch (Exception e)
            {
                return false;
                //throw e;
            }
        }

        /// <summary>
        /// Archiva las listas de la plataforma Mailchimp
        /// </summary>
        /// <param name="ListaMiembros">Lista de objetos de clase Member</param>
        /// <param name="Lista">Objeto de clase List</param>
        /// <returns>Task (bool)</returns>
        public async Task<bool> ArchivarLista(List<Member> ListaMiembros, List Lista)
        {
            try
            {
                try
                {
                    try
                    {
                        System.Net.ServicePointManager.Expect100Continue = false;
                        var batchRequest = new BatchRequest
                        {
                            Operations = ListaMiembros.Select(x => new Operation
                            {
                                Method = "DELETE",
                                Path = $"/lists/{Lista.Id}/members/{Manager.Members.Hash(x.EmailAddress.ToLower())}",
                                Body = JsonConvert.SerializeObject(x, Formatting.None, new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore
                                })
                            })
                        };

                        var batch = Manager.Batches.AddAsync(batchRequest).Result;
                        //Manager.Batches.AddAsync(batchRequest).Wait(300000);
                        //esperamos hasta que el lote termine
                        while (batch.Status != "finished")
                        {
                            //Thread.Sleep(10000);
                            Thread.Sleep(2000);
                            var batchId = batch.Id;
                            batch = Manager.Batches.GetBatchStatus(batchId).Result;
                        }
                        return true;
                    }
                    catch (TaskCanceledException e)
                    {
                        throw e;
                    }
                }
                catch (AggregateException e)
                {
                    throw e;
                }
            }
            catch (Exception e)
            {
                return false;
                //throw e;
            }
        }

        public async Task ConfigurarHorario(string idCampania, CampaignScheduleRequest content = null)
        {
            try
            {
                await Manager.Campaigns.ScheduleAsync(idCampania, content);
            }
            catch(Exception ex)
            {
                throw new Exception("Error configurar horario", ex);
            }
        }

        public async Task PausarHorario(string idCampania, CampaignScheduleRequest content = null)
        {
            try
            {
                await Manager.Campaigns.UnscheduleAsync(idCampania, content);
            }
            catch (Exception ex)
            {
                throw new Exception("Error configurar horario", ex);
            }
        }

        /// <summary>
        /// Crea campanias en la plataforma Mailchimp
        /// </summary>
        /// <param name="campania">Objeto de clase Campaign</param>
        /// <returns>Objeto de clase Task<List</returns>
        public async Task<Campaign> CrearCampania(Campaign campania)
        {
            try
            {
                var nuevaCampania = Manager.Campaigns.AddOrUpdateAsync(campania).Result;

                return nuevaCampania;
            }
            catch (Exception Ex)
            {
                throw new ArgumentException("Error Crear Campania", Ex);
            }
        }

        /// <summary>
        /// Inserta contenido a una campania en la plataforma Mailchimp
        /// </summary>
        /// <param name="Campania"></param>
        /// <param name="Contenido"></param>
        /// <returns></returns>
        public async Task<Content> InsertaContenidoACampania(Campaign Campania, ContentRequest Contenido)
        {
            try
            {
                var NewContent = await Manager.Content.AddOrUpdateAsync(Campania.Id, Contenido);
                return NewContent;
            }
            catch (Exception Ex)
            {
                throw new ArgumentException("Error Insertar Contenido", Ex);
            }
        }

        public static IEnumerable<T> ToEnumerable<T>(params T[] items)
        {
            return items;
        }

        /// <summary>
        /// Envia campanias de la plataforma Mailchimp
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public async Task<bool> EnviarCampania(string IdCampania)
        {
            try
            {
                await Manager.Campaigns.SendAsync(IdCampania);
                return true;
            }
            catch (Exception Ex)
            {
                throw new ArgumentException("Error Envio Campania", Ex);
            }
        }

        /// <summary>
        /// Crea campanias en la plataforma Mailchimp con horario preconfigurado
        /// </summary>
        /// <param name="idCampania">Id de Mailchimp de la campania</param>
        /// <param name="configuracionHorario">Objeto de clase CampaignScheduleRequest</param>
        /// <returns>Objeto de clase Task<List></returns>
        public async Task<bool> EnviarCampaniaHorario(string idCampania, CampaignScheduleRequest configuracionHorario)
        {
            try
            {
                await Manager.Campaigns.ScheduleAsync(idCampania, configuracionHorario);

                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Crear Campania y Configurar el horario", ex);
            }
        }

        /// <summary>
        /// Consigue las campanias de la plataforma Mailchimp 
        /// </summary>
        /// <param name="IdCampaniaMailchimp"></param>
        /// <returns></returns>
        public async Task<Campaign> GetCampania(string IdCampaniaMailchimp)
        {
            try
            {
                var Campania = await Manager.Campaigns.GetAsync(IdCampaniaMailchimp);
                return Campania;
            }
            catch (Exception Ex)
            {
                throw new ArgumentException("Error al Conseguir Campaign", Ex);
            }
        }

        /// <summary>
        /// Consigue los reportes de la plataforma Mailchimp
        /// </summary>
        /// <param name="IdCampaniaMailchimp"></param>
        /// <returns></returns>
        public async Task<Report> GetReporte(string IdCampaniaMailchimp)
        {
            try
            {
                var Reporte = await Manager.Reports.GetReportAsync(IdCampaniaMailchimp);
                return Reporte;
            }
            catch (Exception Ex)
            {
                throw new ArgumentException("Error al Conseguir Reporte", Ex);
            }
        }

        /// <summary>
        /// Consigue los usuarios desuscritos de la plataforma Mailchimp
        /// </summary>
        /// <param name="IdCampaniaMailchimp"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Unsubscribe>> GetUnsubscribes(string IdCampaniaMailchimp)
        {
            try
            {
                var Reporte = await Manager.Reports.GetUnsubscribesAsync(IdCampaniaMailchimp);
                return Reporte;
            }
            catch (Exception Ex)
            {
                throw new ArgumentException("Error al Conseguir desuscritos", Ex);
            }
        }
        public Campaign CrearEstructuraCampania(CampaniaMailchimpDatosDTO campaniaDatos)
        {
            try
            {
                return new Campaign
                {
                    Type = CampaignType.Regular,
                    Recipients = new Recipient
                    {
                        ListId = campaniaDatos.IdLista
                    },
                    Settings = new Setting
                    {
                        SubjectLine = campaniaDatos.Asunto,
                        Title = campaniaDatos.AsuntoLista,
                        FromName = campaniaDatos.NombreAsesor, // nombre asesor
                        ReplyTo = campaniaDatos.Alias
                    },
                    Tracking = new Tracking
                    {
                        Opens = true,
                        HtmlClicks = true,
                        TextClicks = true
                    },
                    SocialCard = new SocialCard
                    {
                        ImageUrl = "",
                        Description = "Campania enviada via MailChimp.NET",
                        Title = "Usando MailChimp API en .NET via MailChimp.NET.V3 wrapper"
                    },
                    ReportSummary = new ReportSummary(),
                    DeliveryStatus = new DeliveryStatus()
                };
            }
            catch (Exception Ex)
            {
                throw new ArgumentException("Error al Conseguir desuscritos", Ex);
            }
        }
        public List CrearEstructuraLista(ListaMailchimpDatosDTO lista)
        {
            var list = new List
            {
                Name = lista.AsuntoLista,
                Contact = new Contact
                {
                    Company = lista.Empresa,
                    Address1 = lista.Direccion,
                    City = lista.Ciudad,
                    State = lista.EstadoPais,
                    Zip = lista.Zip,
                    Country = lista.Pais
                },
                PermissionReminder = lista.Permiso,
                CampaignDefaults = new CampaignDefaults
                {
                    FromEmail = lista.Alias,
                    FromName = lista.NombreAsesor,
                    Subject = lista.AsuntoLista,
                    Language = lista.Lenguaje
                },
                EmailTypeOption = true
            };
            return list;
        }

        public async Task<bool> ObtenerContactosPorLista(List list, List<MailChimpListaCorreoDTO> listaContactos)
        {
            try
            {

                foreach (var item in listaContactos)
                {
                    item.ExisteEnMailChimp = await Manager.Members.ExistsAsync(list.Id, item.Email);
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
