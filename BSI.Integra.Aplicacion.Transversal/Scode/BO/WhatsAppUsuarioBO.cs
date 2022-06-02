using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class WhatsAppUsuarioBO : BaseBO
    {
        public int? IdPersonal { get; set; }
        public string RolUser { get; set; }
        public string UserUsername { get; set; }
        public string UserPassword { get; set; }
        public bool? EsMigracion { get; set; }
        public int? IdMigracion { get; set; }

        /// Autor: Jose Villena
        /// Fecha: 15/09/2021
        /// Version: 1.0
        /// <summary>
        /// Generar Credenciales WhatsApp Operaciones
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public void GenerarCredencialesWhatsAppOperaciones(WhatsAppDatoUsuarioDTO DatosUsuario)
        {
            try
            {
                string _urlGenerarCredencial = "http://localhost:63048/api/WhatsAppUsuario/WhatsAppInsertarUsuario";
                
                var _DatosUsuario = JsonConvert.SerializeObject(DatosUsuario);

                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                    string HtmlResult = wc.UploadString(_urlGenerarCredencial, _DatosUsuario);
                }
            }
            catch (Exception e)
            {
            }

        }
    }
}
