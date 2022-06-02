using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TScrapingAerolineaResultadoDetalle
    {
        public int Id { get; set; }
        public int IdScrapingAerolineaResultado { get; set; }
        public string NroVuelo { get; set; }
        public int? IdProveedor { get; set; }
        public string NombreAerolinea { get; set; }
        public int? IdVueloTipoTramo { get; set; }
        public int? IdCiudadOrigen { get; set; }
        public int? IdCiudadDestino { get; set; }
        public bool EsIda { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaLlegada { get; set; }
        public string Clase { get; set; }
        public bool AplicaMochila { get; set; }
        public bool AplicaEquipajeMano { get; set; }
        public bool AplicaEquipajeBodega { get; set; }
        public int DuracionMinuto { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdPadre { get; set; }
        public string NombreCiudadOrigen { get; set; }
        public string NombreCiudadDestino { get; set; }
    }
}
