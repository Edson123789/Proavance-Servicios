using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CorreoGmailDTO
    {
        public int GmailCorreoId { get; set; }
        public string CuerpoHTML { get; set; }
    }
    public class InformacionDetalleCorreoGmailDTO
    {
        public int IdCorreoGmailArchivoAdjunto { get; set; }
        public string NombreCorreoGmailArchivoAdjunto { get; set; }
    }
    public class InformacionCorreoGmailDTO
    {
        public int GmailCorreoId { get; set; }
        public string CuerpoHTML { get; set; }
        public List<InformacionDetalleCorreoGmailDTO> ListaInformacionDetalleCorreoGmail { get; set; }
        public InformacionCorreoGmailDTO() {
            ListaInformacionDetalleCorreoGmail = new List<InformacionDetalleCorreoGmailDTO>();
        }
    }

    public class DatosGmailDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string CuerpoHTML { get; set; }
        public string AliasCorreo { get; set; }
        public string Clave { get; set; }
    }
}
