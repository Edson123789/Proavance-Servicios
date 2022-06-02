using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TOcurrenciaAlterno
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreM { get; set; }
        public int? NombreCs { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int? IdActividadCabecera { get; set; }
        public int? IdPlantillaSpeech { get; set; }
        public int IdEstadoOcurrencia { get; set; }
        public bool Oportunidad { get; set; }
        public string RequiereLlamada { get; set; }
        public string Roles { get; set; }
        public string Color { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public int? IdTipoOcurrencia { get; set; }
    }
}
