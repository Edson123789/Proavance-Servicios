using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class WhatsAppPlantillaRepositorio //: BaseRepository<TWhatsAppPlantilla, WhatsAppPlantillaBO>
    {
        /// <summary>
        /// Obtiene los datos del host del whatsapp por idPais 
        /// </summary>
        //public WhatsAppPlantillaDTO ObtenerCredencialHost(string espacioNombre)
        //{
        //    try
        //    {
        //        WhatsAppPlantillaDTO hostDatos = new WhatsAppPlantillaDTO();
        //        var _query = string.Empty;
        //        _query = "SELECT Id, UrlWhatsApp, IpHost, IdPais FROM mkt.T_WhatsAppConfiguracion WHERE IdPais = @idPais AND Estado = 1";
        //        var WhatsAppConfiguracionDB = _dapper.FirstOrDefault(_query, new { espacioNombre });
        //        hostDatos = JsonConvert.DeserializeObject<WhatsAppPlantillaDTO>(WhatsAppConfiguracionDB);
        //        return hostDatos;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
    }
}
