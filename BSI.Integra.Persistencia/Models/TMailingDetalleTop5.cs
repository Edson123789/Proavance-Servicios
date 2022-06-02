using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMailingDetalleTop5
    {
        public int Id { get; set; }
        public int IdCampaniaMailingTop5 { get; set; }
        public int IdRemitenteMailing { get; set; }
        public int IdPersonal { get; set; }
        public string Asunto { get; set; }
        public DateTime FechaEnvio { get; set; }
        public int IdHora { get; set; }
        public int CaEstado { get; set; }
        public int Proveedor { get; set; }
        public int IdPlantilla { get; set; }
        public string Campania { get; set; }
        public string CodigoMailing { get; set; }
        public int NumeroProgramas { get; set; }
        public int? IdCojuntoAnuncio { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
