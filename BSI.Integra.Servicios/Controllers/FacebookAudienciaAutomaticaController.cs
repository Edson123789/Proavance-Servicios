using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/FacebookAudienciaAutomatica")]
    public class FacebookAudienciaAutomaticaController : Controller
    {
        private string UrlApiFacebook = "http://integra.bsgrupo.net:84/apps/external/facebookads3-v4/public/index.php/";
        //private string UrlApiFacebook = "http://localhost:84/facebookads3/public/";

        private readonly integraDBContext _integraDBContext;

        ConjuntoListaDetalleRepositorio _repConjuntoListaDetalle;
        FacebookCuentaPublicitariaRepositorio _repFacebookCuentaPublicitaria;
        ConjuntoListaResultadoRepositorio _repConjuntoListaResultado;
        FacebookAudienciaCuentaPublicitariaRepositorio _repFacebookAudienciaCuentaPublicitaria;
        FacebookAudienciaRepositorio _repFacebookAudienciaRepositorio;

        public FacebookAudienciaAutomaticaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repConjuntoListaDetalle = new ConjuntoListaDetalleRepositorio(_integraDBContext);
            _repFacebookCuentaPublicitaria = new FacebookCuentaPublicitariaRepositorio(_integraDBContext);
            _repConjuntoListaResultado = new ConjuntoListaResultadoRepositorio(_integraDBContext);
            _repFacebookAudienciaCuentaPublicitaria = new FacebookAudienciaCuentaPublicitariaRepositorio(_integraDBContext);
            _repFacebookAudienciaRepositorio = new FacebookAudienciaRepositorio(_integraDBContext);
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ProcesarConjuntoListaFacebook([FromBody]List<ActividadParaEjecutarDTO> Actividades)
        {
            try
            {
                foreach (var ActividadFacebook in Actividades)
                {

                    var respuesta = _repConjuntoListaDetalle.ObtenerListasAudiencia(ActividadFacebook.IdConjuntoLista ?? default (int));
                    foreach (var item in respuesta)
                    {
                        item.Cuenta = _repFacebookCuentaPublicitaria.ObtenerCuentaPorIdFacebook(item.Id.Value);
                    }

                    foreach (var ListaFacebook in respuesta)
                    {
                        FacebookAudienciaBO facebookAudienciaBO = _repFacebookAudienciaRepositorio.FirstBy(x => x.Id == ListaFacebook.Id);

                        var FacebookAudienciaCuentaPublicitaria = _repFacebookAudienciaCuentaPublicitaria.ObtenerFacebookAudienciaCuentaPublicitaria(ListaFacebook.IdConjuntoListaDetalle.Value);
                        var Alumno = _repConjuntoListaResultado.ObtenerConjuntoListaResultadoFacebook(ListaFacebook.IdConjuntoListaDetalle.Value);

                        if (ListaFacebook.FacebookIdAudiencia == "")
                        {
                            var cuentaPropio = FacebookAudienciaCuentaPublicitaria.Where(w => w.Origen == "Propio").FirstOrDefault();
                            if (cuentaPropio != null)
                            {
                                string cuentaPrincipal = ListaFacebook.Cuenta.Where(w => w.Id == cuentaPropio.IdFacebookCuentaPublicitaria).FirstOrDefault().FacebookIdCuentaPublicitaria;
                                
                                this.CrearAudienciaAutomatica(ref facebookAudienciaBO ,Alumno, ListaFacebook, cuentaPrincipal);
                                ListaFacebook.FacebookIdAudiencia = facebookAudienciaBO.FacebookIdAudiencia;
                                var CuentaCompartido = FacebookAudienciaCuentaPublicitaria.Where(w => w.Origen == "Compartido").ToList();

                                if (CuentaCompartido.Count > 0 && ListaFacebook.FacebookIdAudiencia !="")
                                {
                                    var cuentaCompartido = ListaFacebook.Cuenta.Where(w => w.Id != cuentaPropio.IdFacebookCuentaPublicitaria).ToList();
                                    //foreach (var item in CuentaCompartido)
                                    //{
                                    //string cuentaCompartido= ListaFacebook.Cuenta.Where(w => w.Id == item.IdFacebookCuentaPublicitaria).FirstOrDefault().FacebookIdCuentaPublicitaria;
                                    this.CompartirAudienciaAutomatico(Alumno, ListaFacebook, cuentaPrincipal, cuentaCompartido);
                                    //}
                                    
                                }
                            }

                        }
                        else
                        {
                            var cuentaPropio = FacebookAudienciaCuentaPublicitaria.Where(w => w.Origen == "Propio").FirstOrDefault();
                            string cuentaPrincipal = ListaFacebook.Cuenta.Where(w => w.Id == cuentaPropio.IdFacebookCuentaPublicitaria).FirstOrDefault().FacebookIdCuentaPublicitaria;
                            if (cuentaPropio != null)
                            {
                                this.ActualizarAudienciaAutomatico(Alumno,ListaFacebook, cuentaPrincipal);
                            }
                        }



                    }

                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            
        }

        private void CrearAudienciaAutomatica(ref FacebookAudienciaBO facebookAudienciaBO, List<FacebookAudienciaDatosAlumnoDTO> Alumno , ConjuntoListaDetalleAudienciaDTO ListaFacebook,string cuenta)
        {
            FacebookAudienciaDTO Json = new FacebookAudienciaDTO();
            Json.FacebookIdAudiencia = ListaFacebook.FacebookIdAudiencia;
            Json.Nombre = ListaFacebook.Nombre;
            Json.Descripcion = ListaFacebook.Descripcion;
            Json.Cuenta = cuenta;
            Json.Pais = "";
            Json.Usuario = "";
            Json.Alumnos = Alumno;


            string rpta;
            FacebookAudienciaRespuestaApiDTO respuestaAPI = new FacebookAudienciaRespuestaApiDTO();
            using (WebClient wc = new WebClient())
            {
                var dataString = JsonConvert.SerializeObject(Json);
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    rpta = wc.UploadString(new Uri(UrlApiFacebook + "/facebook/new/audience"), "POST", dataString);
                    rpta = rpta.Substring(0, rpta.IndexOf('}') + 1);
                    respuestaAPI = JsonConvert.DeserializeObject<FacebookAudienciaRespuestaApiDTO>(rpta);
                }
                catch (Exception ex)
                {
                    if (respuestaAPI.MensajeError == null)
                    {
                        respuestaAPI.MensajeError = ex.Message.ToString();
                    }
                }
            }

            if (respuestaAPI.FlagAudiencia && !String.IsNullOrEmpty(respuestaAPI.FacebookIdAudiencia))

            {
                
                if (facebookAudienciaBO == null)
                {
                    List<string> correos = new List<string>();
                    correos.Add("fvaldez@bsginstitute.com");

                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO();
                    mailData.Sender = "fvaldez@bsginstitute.com";
                    mailData.Recipient = string.Join(",", correos);
                    mailData.Subject = "Proceso Facebook";
                    mailData.Message = "No existe la audiencia seleccionada";
                    mailData.Cc = "";
                    mailData.Bcc = "";
                    mailData.AttachedFiles = null;

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
                facebookAudienciaBO.FacebookIdAudiencia = respuestaAPI.FacebookIdAudiencia;
                facebookAudienciaBO.FechaModificacion = DateTime.Now;
                facebookAudienciaBO.UsuarioModificacion = Json.Usuario;

                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        if (!facebookAudienciaBO.HasErrors)
                        {
                            _repFacebookAudienciaRepositorio.Update(facebookAudienciaBO);
                        }
                        else
                        {
                            List<string> correos = new List<string>();
                            correos.Add("fvaldez@bsginstitute.com");

                            TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                            TMKMailDataDTO mailData = new TMKMailDataDTO();
                            mailData.Sender = "fvaldez@bsginstitute.com";
                            mailData.Recipient = string.Join(",", correos);
                            mailData.Subject = "Proceso Facebook";
                            mailData.Message = " <br/> Mensaje toString <br/> " + facebookAudienciaBO.ActualesErrores.ToString();
                            mailData.Cc = "";
                            mailData.Bcc = "";
                            mailData.AttachedFiles = null;

                            Mailservice.SetData(mailData);
                            Mailservice.SendMessageTask();
                        }

                        scope.Complete();

                    }
                    catch (Exception ex)
                    {
                        scope.Dispose();
                        List<string> correos = new List<string>();
                        correos.Add("fvaldez@bsginstitute.com");

                        TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                        TMKMailDataDTO mailData = new TMKMailDataDTO();
                        mailData.Sender = "fvaldez@bsginstitute.com";
                        mailData.Recipient = string.Join(",", correos);
                        mailData.Subject = "Proceso Facebook";
                        mailData.Message = "Se actualizó el Público pero no se guardo en Integra. Error: " + ex.Message.ToString();
                        mailData.Cc = "";
                        mailData.Bcc = "";
                        mailData.AttachedFiles = null;

                        Mailservice.SetData(mailData);
                        Mailservice.SendMessageTask();
                    }
                }
            }
            else
            {
                List<string> correos = new List<string>();
                correos.Add("fvaldez@bsginstitute.com");

                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                TMKMailDataDTO mailData = new TMKMailDataDTO();
                mailData.Sender = "fvaldez@bsginstitute.com";
                mailData.Recipient = string.Join(",", correos);
                mailData.Subject = "Proceso Facebook";
                mailData.Message = "Error: " + respuestaAPI.MensajeError.ToString();
                mailData.Cc = "";
                mailData.Bcc = "";
                mailData.AttachedFiles = null;

                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();
            }
        }

        private void CompartirAudienciaAutomatico(List<FacebookAudienciaDatosAlumnoDTO> Alumno, ConjuntoListaDetalleAudienciaDTO ListaFacebook, string cuenta1, List<FacebookCuentaPublicitariaDTO> cuentas)
        {
            FacebookAudienciaDTO Json = new FacebookAudienciaDTO();
            Json.FacebookIdAudiencia = ListaFacebook.FacebookIdAudiencia;
            Json.Nombre = ListaFacebook.Nombre;
            Json.Descripcion = ListaFacebook.Descripcion;
            Json.Cuenta = cuenta1;
            Json.Pais = "";
            Json.Usuario = "";
            Json.Alumnos = Alumno;

            //var facebookAudiencia = facebookAudienciaRepositorio.FirstBy(x => x.FacebookIdAudiencia == Json.FacebookIdAudiencia);

            int contador = 0;
            string cuentaOrigen = Json.Cuenta.Substring(4);
            foreach (var cuenta in cuentas)
            {
                if (cuenta.FacebookIdCuentaPublicitaria.Substring(4) != cuentaOrigen)
                {
                    try
                    {
                        Json.Cuenta = cuenta.FacebookIdCuentaPublicitaria.Substring(4);
                        string rpta;
                        using (WebClient wc = new WebClient())
                        {
                            var dataString = JsonConvert.SerializeObject(Json);
                            wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                            try
                            {
                                rpta = wc.UploadString(new Uri(UrlApiFacebook + "facebook/share/audience"), "POST", dataString);
                                rpta = rpta.Substring(0, rpta.IndexOf('}') + 1);
                            }
                            catch (Exception ex)
                            {
                                continue;
                            }
                        }
                        var respuestaAPI = JsonConvert.DeserializeObject<FacebookAudienciaRespuestaApiDTO>(rpta);
                        if (respuestaAPI.FlagAudiencia)
                        {
                            //FacebookAudienciaCuentaPublicitariaBO facebookAudienciaCuentaPublicitariaBO = new FacebookAudienciaCuentaPublicitariaBO();
                            //facebookAudienciaCuentaPublicitariaBO.IdFacebookAudiencia = ListaFacebook.FacebookIdAudiencia != null ? ListaFacebook.FacebookIdAudiencia.Id : 0;
                            //facebookAudienciaCuentaPublicitariaBO.IdFacebookCuentaPublicitaria = cuenta.Id;
                            //facebookAudienciaCuentaPublicitariaBO.Subtipo = "CUSTOM";
                            //facebookAudienciaCuentaPublicitariaBO.Origen = "Compartido";
                            //facebookAudienciaCuentaPublicitariaBO.Estado = true;
                            //facebookAudienciaCuentaPublicitariaBO.FechaCreacion = DateTime.Now;
                            //facebookAudienciaCuentaPublicitariaBO.FechaModificacion = DateTime.Now;
                            //facebookAudienciaCuentaPublicitariaBO.UsuarioCreacion = Json.Usuario;
                            //facebookAudienciaCuentaPublicitariaBO.UsuarioModificacion = Json.Usuario;

                            //if (!facebookAudienciaCuentaPublicitariaBO.HasErrors) facebookAudienciaCuentaPublicitariaRepositorio.Insert(facebookAudienciaCuentaPublicitariaBO);
                            //else continue;

                            contador++;
                        }
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }

            }
        }

        private void ActualizarAudienciaAutomatico(List<FacebookAudienciaDatosAlumnoDTO> Alumno, ConjuntoListaDetalleAudienciaDTO ListaFacebook, string cuenta)
        {
            string rpta;
            FacebookAudienciaDTO Json = new FacebookAudienciaDTO();
            Json.FacebookIdAudiencia = ListaFacebook.FacebookIdAudiencia;
            Json.Nombre = ListaFacebook.Nombre;
            Json.Descripcion = ListaFacebook.Descripcion;
            Json.Cuenta = cuenta;
            Json.Pais = "";
            Json.Usuario = "";
            Json.Alumnos = Alumno;

            using (WebClient wc = new WebClient())
            {
                var dataString = JsonConvert.SerializeObject(Json);
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    rpta = wc.UploadString(new Uri(UrlApiFacebook + "/facebook/update/audience"), "POST", dataString);
                    rpta = rpta.Substring(0, rpta.IndexOf('}') + 1);
                }
                catch (Exception ex)
                {
                    List<string> correos = new List<string>();
                    correos.Add("fvaldez@bsginstitute.com");

                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO();
                    mailData.Sender = "fvaldez@bsginstitute.com";
                    mailData.Recipient = string.Join(",", correos);
                    mailData.Subject = "Proceso Facebook";
                    mailData.Message = "Error Proceso Actualizacion Facebook  " + ex.ToString();
                    mailData.Cc = "";
                    mailData.Bcc = "";
                    mailData.AttachedFiles = null;

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();

                    return;
                }
            }

            var respuestaAPI = JsonConvert.DeserializeObject<FacebookAudienciaRespuestaApiDTO>(rpta);
            if (respuestaAPI.FlagEmails)
            {
                //FacebookAudienciaBO facebookAudienciaBO = facebookAudienciaRepositorio.FirstBy(x => x.FacebookIdAudiencia == Json.FacebookIdAudiencia);
                //if (facebookAudienciaBO == null)
                //{
                //    return BadRequest("No existe la audiencia seleccionada");
                //} 
                //facebookAudienciaBO.FechaModificacion = DateTime.Now;
                //facebookAudienciaBO.UsuarioModificacion = Json.Usuario;

                //using (TransactionScope scope = new TransactionScope())
                //{
                //    try
                //    {
                //        if (!facebookAudienciaBO.HasErrors)
                //        {
                //            facebookAudienciaRepositorio.Update(facebookAudienciaBO);
                //        }
                //        else
                //        {
                //            return BadRequest(facebookAudienciaBO.ActualesErrores);
                //        }

                        
                //        scope.Complete();

                //    }
                //    catch (Exception ex)
                //    {
                //        scope.Dispose();
                //        return BadRequest("Se actualizó el Público pero no se guardo en Integra. Error:" + ex.Message);
                //    }
                //}
            }
        }
    }
}
