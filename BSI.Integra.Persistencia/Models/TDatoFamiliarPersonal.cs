using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDatoFamiliarPersonal
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int IdSexo { get; set; }
        public int IdParentescoPersonal { get; set; }
        public int IdTipoDocumentoPersonal { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string NumeroDocumento { get; set; }
        public string NumeroReferencia1 { get; set; }
        public string NumeroReferencia2 { get; set; }
        public bool DerechoHabiente { get; set; }
        public bool EsContactoInmediato { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
