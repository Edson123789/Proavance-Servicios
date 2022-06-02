using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace BSI.Integra.Aplicacion.Base.BO
{
    public static class ExtendedWebClient
    {
        /// <summary>
        /// Obtiene un archivo desde la web
        /// </summary>
        /// <param name="urlFile"></param>
        /// <returns></returns>
        public static byte[] GetFile(string urlFile) {
            try
            {
                using (var webClient = new WebClient())
                {
                    return webClient.DownloadData(urlFile);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
