using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMessengerEnvioMasivo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int PresupuestoDiario { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdPgeneral { get; set; }
        public int IdActividadCabecera { get; set; }
        public int IdPlantilla { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public int IdFacebookPagina { get; set; }
        public int IdFacebookCuentaPublicitaria { get; set; }
        public int? IdFacebookAudiencia { get; set; }
        public int? IdConjuntoAnuncioFacebook { get; set; }
        public int? IdFacebookAnuncioCreativo { get; set; }
        public int? IdFacebookAnuncio { get; set; }
        public int CantidadContactos { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
