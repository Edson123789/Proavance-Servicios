using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Operaciones
{
    public class MatriculaCabeceraDatosCertificadoMensajesDTO
    {
        public int Id { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int? IdPersonalRemitente { get; set; }
        public string Remitente { get; set; }
        public int? IdPersonalReceptor { get; set; }
        public string Receptor { get; set; }
        public string Mensaje { get; set; }
        public string ValorAntiguo { get; set; }
        public string ValorNuevo { get; set; }
        public bool EstadoMensaje { get; set; }
        public string Usuario { get; set; }
        public bool? solicitud { get; set; }
        public string MensajeRespuesta { get; set; }
    }
}
