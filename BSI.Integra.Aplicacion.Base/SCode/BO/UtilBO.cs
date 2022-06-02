﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BSI.Integra.Aplicacion.Base.BO
{
    /// BO: Base/UtilBO
    /// Autor: Ansoli Espinoza
    /// Fecha: 24/09/2021
    /// <summary>
    /// BO de herramientas de desarrollo
    /// </summary>
    public class UtilBO
    {
        /// Autor: Ansoli Espinoza
        /// Fecha: 24/09/2021
        /// Version: 1.0
        /// <summary>
        /// Quita tildes de texto
        /// </summary>
        /// <param name="texto">String de Texto Original</param>
        /// <returns>string</returns>	
        public string RemoverTilde(string texto)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(texto);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
        /// Autor: Ansoli Espinoza
        /// Fecha: 24/09/2021
        /// Version: 1.0
        /// <summary>
        /// Adapta nombre de archivo para subir a Blob Storage
        /// </summary>
        /// <param name="textoOriginal">String de Texto Original</param>
        /// <returns>string</returns>	
        public string SlugNombreArchivo(string textoOriginal)
        {
            string extension = textoOriginal.Substring(textoOriginal.LastIndexOf("."));
            string texto = RemoverTilde(textoOriginal.Substring(0, textoOriginal.LastIndexOf(".")));

            // Caracteres inválidos         
            texto = Regex.Replace(texto, @"[^a-zA-Z0-9\s-]", "");
            texto = texto.Replace("+", " ");
            texto = texto.Replace("-", " ");

            // Convierte múltiples espacios
            texto = Regex.Replace(texto, @"\s+", " ").Trim();
            texto = texto.Trim();
            texto = texto + extension;

            return texto;
        }
    }
}
