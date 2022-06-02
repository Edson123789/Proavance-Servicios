using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using BSI.Integra.Aplicacion.Servicios.DTOs.DataCredito;

namespace BSI.Integra.Aplicacion.Servicios.BO
{
    public class DataCreditoService
    {
        public DataCreditoService()
        {
            
        }

        public string ConsultarServicioHistoriaCreditoAlumnoColombia(string numeroDocumento, string primerApellido,
            int tipoIdentificacion)
        {
            WebClient wc = new WebClient();
            string url =
                $"http://167.71.100.135:8094/consultar?nroDoc={numeroDocumento}&primerApellido={primerApellido}&tipoIdentificacion={tipoIdentificacion}";
                //$"http://localhost:8094/consultar?nroDoc={numeroDocumento}&primerApellido={primerApellido}&tipoIdentificacion={tipoIdentificacion}";
            string resultado = wc.DownloadString(url);

            return resultado;
        }
    }
}
