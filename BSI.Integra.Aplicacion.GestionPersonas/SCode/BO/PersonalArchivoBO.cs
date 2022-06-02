using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.BO
{
    /// BO: GestionPersonas/PersonalArchivo
    /// Autor: Edgar Serruto
    /// Fecha: 16/08/2021
    /// <summary>
    /// BO para la logica de T_PersonalArchivo
    /// </summary>
    public class PersonalArchivoBO : BaseBO
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
