using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Comercial.SCode.BO
{
    /// BO: Comercial/ChatDetalleIntegraArchivo
    /// Autor: Edgar Serruto. 
    /// Fecha: 17/07/2021
    /// <summary>
    /// BO para la logica de T_ChatDetalleIntegraArchivo
    /// </summary>
    public class ChatDetalleIntegraArchivoBO : BaseBO
    {
        /// Propiedades             Significado
		/// -----------	            ------------
		/// NombreArchivo           Nombre de Archivo
        /// RutaArchivo             Ruta de Archivo
        /// MimeType                Tipo de Archivo
        /// EsImagen                Validación de Imagen
        /// IdMigracion             Id de Migración
        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public string MimeType { get; set; }
        public bool? EsImagen { get; set; }
        public int? IdMigracion { get; set; }
    }
}
