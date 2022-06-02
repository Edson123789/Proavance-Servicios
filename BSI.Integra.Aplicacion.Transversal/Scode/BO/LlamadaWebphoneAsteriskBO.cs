using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Transversal/LlamadaWebphoneAsteriskBO
    /// Autor: Ansoli Deyvis
    /// Fecha: 26-01-2021
    /// <summary>
    /// BO de la tabla T_LlamadaWebphoneAsterisk
    /// </summary>
    public class LlamadaWebphoneAsteriskBO : BaseBO
    {
        /// Propiedades	Significado
        /// -----------	------------
        /// FechaInicio		Fecha de inicio de la llamada
        /// FechaFin        Fecha de Finde la llamada
        /// Anexo		Anexo de la llamada
        /// TelefonoDestino		Telefono de Destino de la llamada
        /// IdActividadDetalle		IdActividadDetalle de la llamada
        /// IdLlamadaWebphoneTipo		Tipo de la llamada de la llamada
        /// CdrId		Id de la llamada en la DB de origen
        /// DuracionTimbrado		Duracion del Timbrado de la llamada
        /// DuracionContesto		Duracion de Contesto de la llamada
        /// NombreGrabacion		Nombre de Grabacion de la llamada
        /// IdProveedorNube     Lllave foranea con la tabla T_IdProveedorNube
        /// Url     Url del Archivo respaldado
        /// EsEliminado     Indica si el archivo ha sido eliminado del disco
        /// NroBytes    Nurmero de Bytes del archivo
        /// FechaSubida     Fecha de Subida del archivo
        /// FechaEliminacion        Fecha en la que se elimino el archivo del disco

        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Anexo { get; set; }
        public string TelefonoDestino { get; set; }
        public int IdActividadDetalle { get; set; }
        public int? IdLlamadaWebphoneTipo { get; set; }
        public int CdrId { get; set; }
        public int DuracionTimbrado { get; set; }
        public int DuracionContesto { get; set; }
        public string NombreGrabacion { get; set; }

        public int? IdProveedorNube { get; set; }
        public string Url { get; set; }
        public bool? EsEliminado { get; set; }
        public int? NroBytes { get; set; }
        public DateTime? FechaSubida { get; set; }
        public DateTime? FechaEliminacion { get; set; }
    }
}
