using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/WebHookMandril")]
    [ApiController]
    public class WebHookMandrilController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public WebHookMandrilController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        [Route("[Action]")]
        [HttpPost]
        public IActionResult InsertarMandril([FromBody] MandrilDTO MandrilDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MandrilBO mandrilBO = new MandrilBO();
                mandrilBO.Evento = MandrilDTO.Evento;
                mandrilBO.IdEvent = MandrilDTO.IdEvent;
                mandrilBO.Ip = MandrilDTO.Ip;
                mandrilBO.Ts = MandrilDTO.Ts;
                mandrilBO.Url = MandrilDTO.Url;
                mandrilBO.UserAgent = MandrilDTO.UserAgent;
                mandrilBO.LocationCity = MandrilDTO.LocationCity;
                mandrilBO.LocationCountry = MandrilDTO.LocationCountry;
                mandrilBO.LocationCountryShort = MandrilDTO.LocationCountryShort;
                mandrilBO.LocationLatitude = MandrilDTO.LocationLatitude;
                mandrilBO.LocationLongitude = MandrilDTO.LocationLongitude;
                mandrilBO.LocationPostalCode = MandrilDTO.LocationPostalCode;
                mandrilBO.LocationRegion = MandrilDTO.LocationRegion;
                mandrilBO.LocationTimezone = MandrilDTO.LocationTimezone;
                mandrilBO.UserAgentMobile = MandrilDTO.UserAgentMobile;
                mandrilBO.UserAgentOsCompany = MandrilDTO.UserAgentOsCompany;
                mandrilBO.UserAgentOsCompanyUrl = MandrilDTO.UserAgentOsCompanyUrl;
                mandrilBO.UserAgentOsFamily = MandrilDTO.UserAgentOsFamily;
                mandrilBO.UserAgentOsIcon = MandrilDTO.UserAgentOsIcon;
                mandrilBO.UserAgentOsName = MandrilDTO.UserAgentOsName;
                mandrilBO.UserAgentOsUrl = MandrilDTO.UserAgentOsUrl;
                mandrilBO.UserAgentType = MandrilDTO.UserAgentType;
                mandrilBO.UserAgentUaCompany = MandrilDTO.UserAgentUaCompany;
                mandrilBO.UserAgentUaCompanyUrl = MandrilDTO.UserAgentUaCompanyUrl;
                mandrilBO.UserAgentUaFamily = MandrilDTO.UserAgentUaFamily;
                mandrilBO.UserAgentUaIcon = MandrilDTO.UserAgentUaIcon;
                mandrilBO.UserAgentUaName = MandrilDTO.UserAgentUaName;
                mandrilBO.UserAgentUaUrl = MandrilDTO.UserAgentUaUrl;
                mandrilBO.UserAgentUaVersion = MandrilDTO.UserAgentUaVersion;
                mandrilBO.MessageBgToolsCode = MandrilDTO.MessageBgToolsCode;
                mandrilBO.MessageBounceDescription = MandrilDTO.MessageBounceDescription;
                mandrilBO.MessageDiag = MandrilDTO.MessageDiag;
                mandrilBO.MessageEmail = MandrilDTO.MessageEmail;
                mandrilBO.MessageId = MandrilDTO.MessageId;
                mandrilBO.MessageSender = MandrilDTO.MessageSender;
                mandrilBO.MessageState = MandrilDTO.MessageState;
                mandrilBO.MessageSubAccount = MandrilDTO.MessageSubAccount;
                mandrilBO.MessageSubject = MandrilDTO.MessageSubject;
                mandrilBO.MessageTags = MandrilDTO.MessageTags;
                mandrilBO.MessageTemplate = MandrilDTO.MessageTemplate;
                mandrilBO.MessageTs = MandrilDTO.MessageTs;
                mandrilBO.MessageVersion = MandrilDTO.MessageVersion;
                mandrilBO.Estado = MandrilDTO.Estado;
                mandrilBO.UsuarioCreacion = MandrilDTO.UsuarioCreacion;
                mandrilBO.UsuarioModificacion = MandrilDTO.UsuarioModificacion;
                mandrilBO.FechaCreacion = MandrilDTO.FechaCreacion;
                mandrilBO.FechaModificacion = MandrilDTO.FechaModificacion;

                MandrilRepositorio _mandrilRepositorio = new MandrilRepositorio(_integraDBContext);
                MandrilEnvioCorreoRepositorio _mandrilEnvioCorreoRepositorio = new MandrilEnvioCorreoRepositorio(_integraDBContext);
                TipoInteraccionMandrilRepositorio _tipoInteraccionMandrilRepositorio = new TipoInteraccionMandrilRepositorio(_integraDBContext);

                var objetoMandrilEnvioCorreo = _mandrilEnvioCorreoRepositorio.FirstBy(x => x.FkMandril == MandrilDTO.IdEvent, s => new { s.IdAlumno });
                if (objetoMandrilEnvioCorreo != null) mandrilBO.IdAlumno = objetoMandrilEnvioCorreo.IdAlumno;
                else mandrilBO.IdAlumno = 0;

                var objetoTipoInteraccionMandril = _tipoInteraccionMandrilRepositorio.FirstBy(x => x.Nombre == MandrilDTO.Evento, s => new { s.IdTipoInteraccion });
                if (objetoTipoInteraccionMandril != null) mandrilBO.IdTipoInteraccion = objetoTipoInteraccionMandril.IdTipoInteraccion;
                else mandrilBO.IdTipoInteraccion = 0;

                if (!mandrilBO.HasErrors)
                {
                    _mandrilRepositorio.Insert(mandrilBO);


                    ////DESCOMENTAR PARA PRODUCCION
                    ////////Sincronizacion con dbV3
                    //////try
                    //////{
                    //////    var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:4348/Marketing/CrearMandrilV4aV3?IdMandril=" + mandrilBO.Id);
                    //////    httpWebRequest.ContentType = "application/json";
                    //////    httpWebRequest.Method = "GET";

                    //////    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    //////    var streamReader = new StreamReader(httpResponse.GetResponseStream());
                    //////    var result = streamReader.ReadToEnd();

                    //////    return Ok(new { Id = mandrilBO.Id });
                    //////}
                    //////catch (Exception Ex)
                    //////{
                    //////    return Ok(new { Id = mandrilBO.Id });
                    //////}

                    return Ok(new { Id = mandrilBO.Id });
                }
                else
                {
                    return BadRequest(mandrilBO.GetErrors(null));
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult InsertarMandrilClick([FromBody] MandrilClickDTO MandrilClickDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MandrilClickBO mandrilClickBO = new MandrilClickBO();
                mandrilClickBO.IdMandril = MandrilClickDTO.IdMandril;
                mandrilClickBO.Ip = MandrilClickDTO.Ip;
                mandrilClickBO.Ubicacion = MandrilClickDTO.Ubicacion;
                mandrilClickBO.Ts = MandrilClickDTO.Ts;
                mandrilClickBO.UserAgent = MandrilClickDTO.UserAgent;
                mandrilClickBO.Estado = MandrilClickDTO.Estado;
                mandrilClickBO.UsuarioCreacion = MandrilClickDTO.UsuarioCreacion;
                mandrilClickBO.UsuarioModificacion = MandrilClickDTO.UsuarioModificacion;
                mandrilClickBO.FechaCreacion = MandrilClickDTO.FechaCreacion;
                mandrilClickBO.FechaModificacion = MandrilClickDTO.FechaModificacion;
                mandrilClickBO.Url = MandrilClickDTO.Url;

                if (!mandrilClickBO.HasErrors)
                {
                    MandrilClickRepositorio _mandrilClickRepositorio = new MandrilClickRepositorio(_integraDBContext);
                    _mandrilClickRepositorio.Insert(mandrilClickBO);

                    ///DESCOMENTAR PARA PRODUCCION
                    //////////Sincronizacion con dbV3
                    ////////try
                    ////////{
                    ////////    var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:4348/Marketing/CrearMandrilClickV4aV3?IdMandrilClick=" + mandrilClickBO.Id);
                    ////////    httpWebRequest.ContentType = "application/json";
                    ////////    httpWebRequest.Method = "GET";

                    ////////    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    ////////    var streamReader = new StreamReader(httpResponse.GetResponseStream());
                    ////////    var result = streamReader.ReadToEnd();

                    ////////    return Ok();
                    ////////}
                    ////////catch (Exception Ex)
                    ////////{
                    ////////    return Ok();
                    ////////}
                    return Ok();
                }
                else
                {
                    return BadRequest(mandrilClickBO.GetErrors(null));
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult InsertarMandrilOpen([FromBody] MandrilOpenDTO MandrilOpenDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MandrilOpenBO mandrilOpenBO = new MandrilOpenBO();
                mandrilOpenBO.IdMandril = MandrilOpenDTO.IdMandril;
                mandrilOpenBO.Ip = MandrilOpenDTO.Ip;
                mandrilOpenBO.Ubicacion = MandrilOpenDTO.Ubicacion;
                mandrilOpenBO.Ts = MandrilOpenDTO.Ts;
                mandrilOpenBO.UserAgent = MandrilOpenDTO.UserAgent;
                mandrilOpenBO.Estado = MandrilOpenDTO.Estado;
                mandrilOpenBO.UsuarioCreacion = MandrilOpenDTO.UsuarioCreacion;
                mandrilOpenBO.UsuarioModificacion = MandrilOpenDTO.UsuarioModificacion;
                mandrilOpenBO.FechaCreacion = MandrilOpenDTO.FechaCreacion;
                mandrilOpenBO.FechaModificacion = MandrilOpenDTO.FechaModificacion;

                if (!mandrilOpenBO.HasErrors)
                {
                    MandrilOpenRepositorio _mandrilOpenRepositorio = new MandrilOpenRepositorio(_integraDBContext);
                    _mandrilOpenRepositorio.Insert(mandrilOpenBO);

                    ///DESCOMENTAR PARA PRODUCCION
                    //////////Sincronizacion con dbV3
                    ////////try
                    ////////{
                    ////////    var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:4348/Marketing/CrearMandrilOpenV4aV3?IdMandrilOpen=" + mandrilOpenBO.Id);
                    ////////    httpWebRequest.ContentType = "application/json";
                    ////////    httpWebRequest.Method = "GET";

                    ////////    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    ////////    var streamReader = new StreamReader(httpResponse.GetResponseStream());
                    ////////    var result = streamReader.ReadToEnd();

                    ////////    return Ok();
                    ////////}
                    ////////catch (Exception Ex)
                    ////////{
                    ////////    return Ok();
                    ////////}
                    return Ok();
                }
                else
                {
                    return BadRequest(mandrilOpenBO.GetErrors(null));
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult InsertarMandrilLog([FromBody] MandrilLogDTO MandrilLogDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MandrilLogBO mandrilLogBO = new MandrilLogBO();
                mandrilLogBO.Error = MandrilLogDTO.Error;
                mandrilLogBO.Data = MandrilLogDTO.Data;
                mandrilLogBO.Fecha = MandrilLogDTO.Fecha;
                mandrilLogBO.Estado = MandrilLogDTO.Estado;
                mandrilLogBO.UsuarioCreacion = MandrilLogDTO.UsuarioCreacion;
                mandrilLogBO.UsuarioModificacion = MandrilLogDTO.UsuarioModificacion;
                mandrilLogBO.FechaCreacion = MandrilLogDTO.FechaCreacion;
                mandrilLogBO.FechaModificacion = MandrilLogDTO.FechaModificacion;

                if (!mandrilLogBO.HasErrors)
                {
                    MandrilLogRepositorio _mandrilLogRepositorio = new MandrilLogRepositorio(_integraDBContext);
                    _mandrilLogRepositorio.Insert(mandrilLogBO);
                    return Ok();
                }
                else
                {
                    return BadRequest(mandrilLogBO.GetErrors(null));
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
    }
}