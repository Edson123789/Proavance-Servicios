using System;
using System.Linq;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.DTOs;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Servicios;
using System.Globalization;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using System.Transactions;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Configuracion/SubidaInformacion
    /// Autor: Carlos Crispin
    /// Fecha: 07/03/2022
    /// <summary>
    /// Sube el archivo y envia el correo
    /// </summary>
    [Route("api/SubidaInformacion")]
    public class SubidaInformacionController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly MaterialVersionRepositorio _repMaterialVersion;
        private readonly PersonalRepositorio _repPersonal;
        private readonly DocumentoMarketingRepositorio _repDocumentoMarketing;


        public SubidaInformacionController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repMaterialVersion = new MaterialVersionRepositorio(_integraDBContext);
            _repPersonal = new PersonalRepositorio(_integraDBContext);
            _repDocumentoMarketing = new DocumentoMarketingRepositorio(_integraDBContext);

        }

        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Sube el archivo al blobstorage
        /// </summary>
        /// <returns> Url direccion </returns>
        /// <returns> String </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult SubirDocumentosMarketing(DocumentosMarketingDTO MaterialMarketing, [FromForm] IList<IFormFile> Files)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();
                FiltroBandejaCorreoBO gmailCorreoBO = new FiltroBandejaCorreoBO();

                string mensaje = string.Empty;
                mensaje += "<p><b>Archivo Enviado desde el equipo de Marketing</b></p>";
                mensaje += "<ul>";
                mensaje += "<li><b>Fecha y Hora Subida:</b> " + DateTime.Now.ToString("g", CultureInfo.CreateSpecificCulture("es-ES")) + "</li>";
                mensaje += "<li><b>IP:</b> " + MaterialMarketing.IPCliente + "</li>";
                mensaje += "</ul>";

                var listaEmailConfigurados = _repPersonal.ObtenerPersonalEnviarCorreoMarketing();
                string listaEmail = string.Join(",", listaEmailConfigurados.Select(w=>w.Email).ToList());
                //string listaEmail = "ccrispin@bsginstitute.com,ccrispin@bsginstitute.com,ccrispin@bsginstitute.com";


                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "mktenvios@bsginstitute.com",
                    Recipient = listaEmail,
                    Subject = "Archivo Informacion Marketing",
                    Message = mensaje,
                    Bcc = "ccrispin@bsginstitute.com",
                    AttachedFiles = null,
                    RemitenteC = "Sistema Integra"
                };

                Mailservice.SetData(mailData);

                if (Files != null)
                {
                    foreach (var file in Files)
                    {
                        Mailservice.SetFiles(file);
                    }
                }
                var result = false;
                DocumentoMarketingBO documento = new DocumentoMarketingBO();
                using (TransactionScope scope = new TransactionScope())
                {

                    documento.NombreArchivo = "";
                    documento.Ipcliente = MaterialMarketing.IPCliente;
                    documento.FechaCreacion = DateTime.Now;
                    documento.FechaModificacion = DateTime.Now;
                    documento.UsuarioCreacion = MaterialMarketing.NombreUsuario;
                    documento.UsuarioModificacion = MaterialMarketing.NombreUsuario;

                    result = _repDocumentoMarketing.Insert(documento);

                    scope.Complete();
                }
                
                if (result)
                {
                    bool res = gmailCorreoBO.envioEmailAdjunto("mktenvios@bsginstitute.com", "zubventpjjqicyfb", mailData, Files);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
