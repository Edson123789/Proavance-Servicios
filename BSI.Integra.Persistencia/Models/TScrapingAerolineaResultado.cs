using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TScrapingAerolineaResultado
    {
        public int Id { get; set; }
        public int IdScrapingAerolineaConfiguracion { get; set; }
        public decimal Precio { get; set; }
        public int IdScrapingPagina { get; set; }
        public int IdCentroCosto { get; set; }
        public int? IdPespecifico { get; set; }
        public int? NroSesionGrupo { get; set; }
        public int? NroGrupoCronograma { get; set; }
        public int IdCiudadOrigen { get; set; }
        public int IdCiudadDestino { get; set; }
        public bool EsActual { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
