using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFacebookFormularioWebhookLeadgenError
    {
        public int Id { get; set; }
        public string FacebookIdLead { get; set; }
        public string FacebookIdCampania { get; set; }
        public string FacebookIdFormulario { get; set; }
        public string FacebookFechaUnix { get; set; }
        public string FacebookIdPagina { get; set; }
        public string FacebookIdGrupo { get; set; }
        public bool EsProcesado { get; set; }
        public string ErrorReal { get; set; }
        public string Error { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
