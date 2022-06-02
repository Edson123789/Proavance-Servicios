using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Net;
using BSI.Integra.Aplicacion.Marketing.BO;
using System.Transactions;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/FiltroSegmentoRemarketing")]
    [ApiController]
    public class FiltroSegmentoRemarketingController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private string UrlApiFacebook = "http://integra.bsgrupo.net:84/apps/external/facebookads3-v4/public/index.php/";
        //private string UrlApiFacebook = "http://localhost:84/facebookads3/public/";
        public FiltroSegmentoRemarketingController(integraDBContext integraDBContext)
        {

            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosRemarketing()
        {
            try
            {
                FacebookAudienciaRepositorio facebookAudienciaRepositorio = new FacebookAudienciaRepositorio(_integraDBContext);
                FacebookCuentaPublicitariaRepositorio facebookCuentaPublicitariaRepositorio = new FacebookCuentaPublicitariaRepositorio(_integraDBContext);
                FiltroSegmentoRemarketingCombosDTO filtroSegmentoRemarketingCombosDTO = new FiltroSegmentoRemarketingCombosDTO();

                filtroSegmentoRemarketingCombosDTO.ListaFacebookAudiencia = facebookAudienciaRepositorio.ObtenerCombo();
                filtroSegmentoRemarketingCombosDTO.ListaFacebookCuentaPublicitaria = facebookCuentaPublicitariaRepositorio.ObtenerCombo();
                return Ok(filtroSegmentoRemarketingCombosDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaPublico()
        {
            try
            {
                FacebookAudienciaRepositorio facebookAudienciaRepositorio = new FacebookAudienciaRepositorio(_integraDBContext);

                return Ok(facebookAudienciaRepositorio.ObtenerCombo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]/{IdFiltroSegmento}")]
        [HttpGet]
        public ActionResult ObtenerHistorialAudiencia(int IdFiltroSegmento)
        {
            try
            {
                FacebookAudienciaRepositorio facebookAudienciaRepositorio = new FacebookAudienciaRepositorio(_integraDBContext);

                return Ok(facebookAudienciaRepositorio.ObtenerHistorialPorIdFiltroSegmento(IdFiltroSegmento));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarAudiencia([FromBody] FacebookAudienciaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FacebookAudienciaRepositorio facebookAudienciaRepositorio = new FacebookAudienciaRepositorio(_integraDBContext);
                FacebookAudienciaCuentaPublicitariaRepositorio facebookAudienciaCuentaPublicitariaRepositorio = new FacebookAudienciaCuentaPublicitariaRepositorio(_integraDBContext);
                FacebookAudienciaAlumnoRepositorio facebookAudienciaAlumnoRepositorio = new FacebookAudienciaAlumnoRepositorio(_integraDBContext);
                FacebookCuentaPublicitariaRepositorio facebookCuentaPublicitariaRepositorio = new FacebookCuentaPublicitariaRepositorio(_integraDBContext);

                string rpta;
                using (WebClient wc = new WebClient())
                {
                    var dataString = JsonConvert.SerializeObject(Json);
                    wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    try
                    {
                        rpta = wc.UploadString(new Uri(UrlApiFacebook + "/facebook/new/audience"), "POST", dataString);
                        rpta = rpta.Substring(0, rpta.IndexOf('}') + 1);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }

                var respuestaAPI = JsonConvert.DeserializeObject<FacebookAudienciaRespuestaApiDTO>(rpta);
                if (respuestaAPI.FlagAudiencia && !String.IsNullOrEmpty(respuestaAPI.FacebookIdAudiencia))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            FacebookAudienciaBO facebookAudienciaBO = new FacebookAudienciaBO();
                            facebookAudienciaBO.IdFiltroSegmento = Json.IdFiltroSegmento;
                            facebookAudienciaBO.FacebookIdAudiencia = respuestaAPI.FacebookIdAudiencia;
                            facebookAudienciaBO.Nombre = Json.Nombre;
                            facebookAudienciaBO.Descripcion = Json.Descripcion;
                            facebookAudienciaBO.Subtipo = "CUSTOM";
                            facebookAudienciaBO.RecursoArchivoCliente = "USER_PROVIDED_ONLY";
                            facebookAudienciaBO.Estado = true;
                            facebookAudienciaBO.FechaCreacion = DateTime.Now;
                            facebookAudienciaBO.FechaModificacion = DateTime.Now;
                            facebookAudienciaBO.UsuarioCreacion = Json.Usuario;
                            facebookAudienciaBO.UsuarioModificacion = Json.Usuario;

                            if (!facebookAudienciaBO.HasErrors) facebookAudienciaRepositorio.Insert(facebookAudienciaBO);
                            else return BadRequest(facebookAudienciaBO.ActualesErrores);

                            var cuentaPublicitaria = facebookCuentaPublicitariaRepositorio.FirstBy(x => x.FacebookIdCuentaPublicitaria == Json.Cuenta);
                            FacebookAudienciaCuentaPublicitariaBO facebookAudienciaCuentaPublicitariaBO = new FacebookAudienciaCuentaPublicitariaBO();
                            facebookAudienciaCuentaPublicitariaBO.IdFacebookAudiencia = facebookAudienciaBO.Id;
                            facebookAudienciaCuentaPublicitariaBO.IdFacebookCuentaPublicitaria = cuentaPublicitaria != null ? cuentaPublicitaria.Id : 0;
                            facebookAudienciaCuentaPublicitariaBO.Subtipo = "CUSTOM";
                            facebookAudienciaCuentaPublicitariaBO.Origen = "Propio";
                            facebookAudienciaCuentaPublicitariaBO.Estado = true;
                            facebookAudienciaCuentaPublicitariaBO.FechaCreacion = DateTime.Now;
                            facebookAudienciaCuentaPublicitariaBO.FechaModificacion = DateTime.Now;
                            facebookAudienciaCuentaPublicitariaBO.UsuarioCreacion = Json.Usuario;
                            facebookAudienciaCuentaPublicitariaBO.UsuarioModificacion = Json.Usuario;

                            if (!facebookAudienciaCuentaPublicitariaBO.HasErrors) facebookAudienciaCuentaPublicitariaRepositorio.Insert(facebookAudienciaCuentaPublicitariaBO);
                            else return BadRequest(facebookAudienciaCuentaPublicitariaBO.ActualesErrores);

                            //foreach (var objeto in Json.Alumnos)
                            //{
                            //    FacebookAudienciaAlumnoBO facebookAudienciaAlumnoBO = new FacebookAudienciaAlumnoBO();
                            //    facebookAudienciaAlumnoBO.IdFacebookAudiencia = facebookAudienciaBO.Id;
                            //    facebookAudienciaAlumnoBO.IdAlumno = objeto.IdAlumno;
                            //    facebookAudienciaAlumnoBO.Estado = true;
                            //    facebookAudienciaAlumnoBO.FechaCreacion = DateTime.Now;
                            //    facebookAudienciaAlumnoBO.FechaModificacion = DateTime.Now;
                            //    facebookAudienciaAlumnoBO.UsuarioCreacion = Json.Usuario;
                            //    facebookAudienciaAlumnoBO.UsuarioModificacion = Json.Usuario;

                            //    if (!facebookAudienciaAlumnoBO.HasErrors) facebookAudienciaAlumnoRepositorio.Insert(facebookAudienciaAlumnoBO);
                            //    else return BadRequest(facebookAudienciaAlumnoBO.ActualesErrores);
                            //}
                            scope.Complete();

                        }
                        catch (Exception ex)
                        {
                            scope.Dispose();
                            return BadRequest("Se creo el Público pero no se guardo en Integra. Error:" + ex.Message);
                        }
                    }

                }
                else return BadRequest(respuestaAPI.MensajeError);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult CrearAudienciaAutomatica([FromBody] List<FacebookAudienciaActividadDTO> Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FacebookAudienciaRepositorio facebookAudienciaRepositorio = new FacebookAudienciaRepositorio(_integraDBContext);
                FacebookAudienciaCuentaPublicitariaRepositorio facebookAudienciaCuentaPublicitariaRepositorio = new FacebookAudienciaCuentaPublicitariaRepositorio(_integraDBContext);
                FacebookAudienciaAlumnoRepositorio facebookAudienciaAlumnoRepositorio = new FacebookAudienciaAlumnoRepositorio(_integraDBContext);
                FacebookCuentaPublicitariaRepositorio facebookCuentaPublicitariaRepositorio = new FacebookCuentaPublicitariaRepositorio(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        
                        foreach (var Audiencia in Json)
                        {
                            int PrimeraCuenta = 0;

                            FacebookAudienciaBO facebookAudienciaBO = new FacebookAudienciaBO();
                            facebookAudienciaBO.IdFiltroSegmento = Audiencia.IdFiltroSegmento;
                            facebookAudienciaBO.FacebookIdAudiencia = "";
                            facebookAudienciaBO.Nombre = Audiencia.Nombre;
                            facebookAudienciaBO.Descripcion = Audiencia.Descripcion;
                            facebookAudienciaBO.Subtipo = "CUSTOM";
                            facebookAudienciaBO.RecursoArchivoCliente = "USER_PROVIDED_ONLY";
                            facebookAudienciaBO.Estado = true;
                            facebookAudienciaBO.FechaCreacion = DateTime.Now;
                            facebookAudienciaBO.FechaModificacion = DateTime.Now;
                            facebookAudienciaBO.UsuarioCreacion = Audiencia.Usuario;
                            facebookAudienciaBO.UsuarioModificacion = Audiencia.Usuario;

                            if (!facebookAudienciaBO.HasErrors) facebookAudienciaRepositorio.Insert(facebookAudienciaBO);
                            else return BadRequest(facebookAudienciaBO.ActualesErrores);

                            foreach (var Cuenta in Audiencia.Cuenta)
                            {
                                FacebookAudienciaCuentaPublicitariaBO facebookAudienciaCuentaPublicitariaBO = new FacebookAudienciaCuentaPublicitariaBO();

                                if (PrimeraCuenta == 0)
                                {
                                    facebookAudienciaCuentaPublicitariaBO.IdFacebookAudiencia = facebookAudienciaBO.Id;
                                    facebookAudienciaCuentaPublicitariaBO.IdFacebookCuentaPublicitaria = Cuenta != null ? Cuenta.Id : 0;
                                    facebookAudienciaCuentaPublicitariaBO.Subtipo = "CUSTOM";
                                    facebookAudienciaCuentaPublicitariaBO.Origen = "Propio";
                                    facebookAudienciaCuentaPublicitariaBO.IdConjuntoListaDetalle = Audiencia.IdConjuntoListaDetalle;
                                    facebookAudienciaCuentaPublicitariaBO.Estado = true;
                                    facebookAudienciaCuentaPublicitariaBO.FechaCreacion = DateTime.Now;
                                    facebookAudienciaCuentaPublicitariaBO.FechaModificacion = DateTime.Now;
                                    facebookAudienciaCuentaPublicitariaBO.UsuarioCreacion = Audiencia.Usuario;
                                    facebookAudienciaCuentaPublicitariaBO.UsuarioModificacion = Audiencia.Usuario;
                                }
                                else 
                                {
                                    facebookAudienciaCuentaPublicitariaBO.IdFacebookAudiencia = facebookAudienciaBO.Id;
                                    facebookAudienciaCuentaPublicitariaBO.IdFacebookCuentaPublicitaria = Cuenta != null ? Cuenta.Id : 0;
                                    facebookAudienciaCuentaPublicitariaBO.Subtipo = "CUSTOM";
                                    facebookAudienciaCuentaPublicitariaBO.Origen = "Compartido";
                                    facebookAudienciaCuentaPublicitariaBO.IdConjuntoListaDetalle = Audiencia.IdConjuntoListaDetalle;
                                    facebookAudienciaCuentaPublicitariaBO.Estado = true;
                                    facebookAudienciaCuentaPublicitariaBO.FechaCreacion = DateTime.Now;
                                    facebookAudienciaCuentaPublicitariaBO.FechaModificacion = DateTime.Now;
                                    facebookAudienciaCuentaPublicitariaBO.UsuarioCreacion = Audiencia.Usuario;
                                    facebookAudienciaCuentaPublicitariaBO.UsuarioModificacion = Audiencia.Usuario;
                                }

                                if (!facebookAudienciaCuentaPublicitariaBO.HasErrors) facebookAudienciaCuentaPublicitariaRepositorio.Insert(facebookAudienciaCuentaPublicitariaBO);
                                else return BadRequest(facebookAudienciaCuentaPublicitariaBO.ActualesErrores);

                                PrimeraCuenta++;
                            }

                        }
                        scope.Complete();

                    }
                    catch (Exception ex)
                    {
                        scope.Dispose();
                        return BadRequest("Se creo el Público pero no se guardo en Integra. Error:" + ex.Message);
                    }
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarAudiencia([FromBody] FacebookAudienciaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FacebookAudienciaRepositorio facebookAudienciaRepositorio = new FacebookAudienciaRepositorio(_integraDBContext);
                FacebookAudienciaCuentaPublicitariaRepositorio facebookAudienciaCuentaPublicitariaRepositorio = new FacebookAudienciaCuentaPublicitariaRepositorio(_integraDBContext);
                FacebookAudienciaAlumnoRepositorio facebookAudienciaAlumnoRepositorio = new FacebookAudienciaAlumnoRepositorio(_integraDBContext);

                string rpta;
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
                        return BadRequest(ex.Message);
                    }
                }

                var respuestaAPI = JsonConvert.DeserializeObject<FacebookAudienciaRespuestaApiDTO>(rpta);
                if (respuestaAPI.FlagEmails)
                {
                    FacebookAudienciaBO facebookAudienciaBO = facebookAudienciaRepositorio.FirstBy(x => x.FacebookIdAudiencia == Json.FacebookIdAudiencia);
                    if (facebookAudienciaBO == null) return BadRequest("No existe la audiencia seleccionada");
                    facebookAudienciaBO.FechaModificacion = DateTime.Now;
                    facebookAudienciaBO.UsuarioModificacion = Json.Usuario;

                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            if (!facebookAudienciaBO.HasErrors) facebookAudienciaRepositorio.Update(facebookAudienciaBO);
                            else return BadRequest(facebookAudienciaBO.ActualesErrores);

                            //foreach (var objeto in Json.Alumnos)
                            //{
                            //    FacebookAudienciaAlumnoBO facebookAudienciaAlumnoBO = facebookAudienciaAlumnoRepositorio.FirstBy(x => x.IdFacebookAudiencia == facebookAudienciaBO.Id && x.IdAlumno == objeto.IdAlumno);
                            //    if (facebookAudienciaAlumnoBO == null)
                            //    {
                            //        facebookAudienciaAlumnoBO = new FacebookAudienciaAlumnoBO();
                            //        facebookAudienciaAlumnoBO.IdFacebookAudiencia = facebookAudienciaBO.Id;
                            //        facebookAudienciaAlumnoBO.IdAlumno = objeto.IdAlumno;
                            //        facebookAudienciaAlumnoBO.Estado = true;
                            //        facebookAudienciaAlumnoBO.FechaCreacion = DateTime.Now;
                            //        facebookAudienciaAlumnoBO.FechaModificacion = DateTime.Now;
                            //        facebookAudienciaAlumnoBO.UsuarioCreacion = Json.Usuario;
                            //        facebookAudienciaAlumnoBO.UsuarioModificacion = Json.Usuario;
                            //        if (!facebookAudienciaAlumnoBO.HasErrors) facebookAudienciaAlumnoRepositorio.Insert(facebookAudienciaAlumnoBO);
                            //        else return BadRequest(facebookAudienciaAlumnoBO.ActualesErrores);
                            //    }
                            //    else
                            //    {
                            //        facebookAudienciaAlumnoBO.FechaModificacion = DateTime.Now;
                            //        facebookAudienciaAlumnoBO.UsuarioModificacion = Json.Usuario;
                            //        if (!facebookAudienciaAlumnoBO.HasErrors) facebookAudienciaAlumnoRepositorio.Update(facebookAudienciaAlumnoBO);
                            //        else return BadRequest(facebookAudienciaAlumnoBO.ActualesErrores);
                            //    }
                            //}
                            scope.Complete();

                        }
                        catch (Exception ex)
                        {
                            scope.Dispose();
                            return BadRequest("Se actualizó el Público pero no se guardo en Integra. Error:" + ex.Message);
                        }
                    }
                }
                else return BadRequest(respuestaAPI.MensajeError);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult CrearPublicoSimilar([FromBody] FacebookAudienciaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FacebookAudienciaRepositorio facebookAudienciaRepositorio = new FacebookAudienciaRepositorio(_integraDBContext);
                FacebookAudienciaCuentaPublicitariaRepositorio facebookAudienciaCuentaPublicitariaRepositorio = new FacebookAudienciaCuentaPublicitariaRepositorio(_integraDBContext);
                FacebookCuentaPublicitariaRepositorio facebookCuentaPublicitariaRepositorio = new FacebookCuentaPublicitariaRepositorio(_integraDBContext);

                string rpta;
                using (WebClient wc = new WebClient())
                {
                    var dataString = JsonConvert.SerializeObject(Json);
                    wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    try
                    {
                        rpta = wc.UploadString(new Uri(UrlApiFacebook + "/facebook/lookalike/audience"), "POST", dataString);
                        rpta = rpta.Substring(0, rpta.IndexOf('}') + 1);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }

                var respuestaAPI = JsonConvert.DeserializeObject<FacebookAudienciaRespuestaApiDTO>(rpta);
                if (respuestaAPI.FlagAudiencia)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            FacebookAudienciaBO facebookAudienciaBO = new FacebookAudienciaBO();
                            facebookAudienciaBO.IdFiltroSegmento = Json.IdFiltroSegmento;
                            facebookAudienciaBO.FacebookIdAudiencia = respuestaAPI.FacebookIdAudiencia;
                            facebookAudienciaBO.Nombre = Json.Nombre;
                            facebookAudienciaBO.Descripcion = Json.Descripcion;
                            facebookAudienciaBO.Subtipo = "LOOKALIKE";
                            facebookAudienciaBO.RecursoArchivoCliente = "";
                            facebookAudienciaBO.Estado = true;
                            facebookAudienciaBO.FechaCreacion = DateTime.Now;
                            facebookAudienciaBO.FechaModificacion = DateTime.Now;
                            facebookAudienciaBO.UsuarioCreacion = Json.Usuario;
                            facebookAudienciaBO.UsuarioModificacion = Json.Usuario;

                            if (!facebookAudienciaBO.HasErrors) facebookAudienciaRepositorio.Insert(facebookAudienciaBO);
                            else return BadRequest(facebookAudienciaBO.ActualesErrores);

                            var cuentaPublicitaria = facebookCuentaPublicitariaRepositorio.FirstBy(x => x.FacebookIdCuentaPublicitaria == Json.Cuenta);
                            FacebookAudienciaCuentaPublicitariaBO facebookAudienciaCuentaPublicitariaBO = new FacebookAudienciaCuentaPublicitariaBO();
                            facebookAudienciaCuentaPublicitariaBO.IdFacebookAudiencia = facebookAudienciaBO.Id;
                            facebookAudienciaCuentaPublicitariaBO.IdFacebookCuentaPublicitaria = cuentaPublicitaria != null ? cuentaPublicitaria.Id : 0;
                            facebookAudienciaCuentaPublicitariaBO.Subtipo = "LOOKALIKE";
                            facebookAudienciaCuentaPublicitariaBO.Origen = "Propio";
                            facebookAudienciaCuentaPublicitariaBO.Estado = true;
                            facebookAudienciaCuentaPublicitariaBO.FechaCreacion = DateTime.Now;
                            facebookAudienciaCuentaPublicitariaBO.FechaModificacion = DateTime.Now;
                            facebookAudienciaCuentaPublicitariaBO.UsuarioCreacion = Json.Usuario;
                            facebookAudienciaCuentaPublicitariaBO.UsuarioModificacion = Json.Usuario;

                            if (!facebookAudienciaCuentaPublicitariaBO.HasErrors) facebookAudienciaCuentaPublicitariaRepositorio.Insert(facebookAudienciaCuentaPublicitariaBO);
                            else return BadRequest(facebookAudienciaCuentaPublicitariaBO.ActualesErrores);

                            scope.Complete();

                        }
                        catch (Exception ex)
                        {
                            scope.Dispose();
                            return BadRequest("Se creo el Público Similar pero no se guardo en Integra. Error:" + ex.Message);
                        }
                    }
                }
                else return BadRequest(respuestaAPI.MensajeError);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult CompartirAudiencia([FromBody] FacebookAudienciaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FacebookAudienciaRepositorio facebookAudienciaRepositorio = new FacebookAudienciaRepositorio(_integraDBContext);
                FacebookAudienciaCuentaPublicitariaRepositorio facebookAudienciaCuentaPublicitariaRepositorio = new FacebookAudienciaCuentaPublicitariaRepositorio(_integraDBContext);
                FacebookCuentaPublicitariaRepositorio facebookCuentaPublicitariaRepositorio = new FacebookCuentaPublicitariaRepositorio(_integraDBContext);

                var cuentas = facebookCuentaPublicitariaRepositorio.GetBy(x => true);
                var facebookAudiencia = facebookAudienciaRepositorio.FirstBy(x => x.FacebookIdAudiencia == Json.FacebookIdAudiencia);

                int contador = 0;
                string cuentaOrigen = Json.Cuenta.Substring(4);
                foreach (var cuenta in cuentas)
                {
                    if(cuenta.FacebookIdCuentaPublicitaria.Substring(4) != cuentaOrigen)
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
                                FacebookAudienciaCuentaPublicitariaBO facebookAudienciaCuentaPublicitariaBO = new FacebookAudienciaCuentaPublicitariaBO();
                                facebookAudienciaCuentaPublicitariaBO.IdFacebookAudiencia = facebookAudiencia != null ? facebookAudiencia.Id : 0;
                                facebookAudienciaCuentaPublicitariaBO.IdFacebookCuentaPublicitaria = cuenta.Id;
                                facebookAudienciaCuentaPublicitariaBO.Subtipo = "CUSTOM";
                                facebookAudienciaCuentaPublicitariaBO.Origen = "Compartido";
                                facebookAudienciaCuentaPublicitariaBO.Estado = true;
                                facebookAudienciaCuentaPublicitariaBO.FechaCreacion = DateTime.Now;
                                facebookAudienciaCuentaPublicitariaBO.FechaModificacion = DateTime.Now;
                                facebookAudienciaCuentaPublicitariaBO.UsuarioCreacion = Json.Usuario;
                                facebookAudienciaCuentaPublicitariaBO.UsuarioModificacion = Json.Usuario;

                                if (!facebookAudienciaCuentaPublicitariaBO.HasErrors) facebookAudienciaCuentaPublicitariaRepositorio.Insert(facebookAudienciaCuentaPublicitariaBO);
                                else continue;

                                contador++;
                            }
                        }
                        catch (Exception e)
                        {
                            continue;
                        }
                    }

                }

                return Ok("Se compartio correctamente a " + contador + " cuenta(s).");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}