using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BSI.Integra.Aplicacion.Transversal.Helper
{
    public class HelperSlug
    {
        public string GenerateSlug(string phrase, int idBusqueda)
        {
            string str = phrase;
            // invalid chars           
            str = Regex.Replace(str, @"[^a-zA-Z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            //str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str + "-" + idBusqueda;
        }
    }
}
