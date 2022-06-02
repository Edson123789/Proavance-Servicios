using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCertificadoTipoPrograma
    {
        public int Id { get; set; }
        public string NombreProgramaCertificado { get; set; }
        public string Codigo { get; set; }
        public bool AplicaFondoDiploma { get; set; }
        public bool AplicaSeOtorga { get; set; }
        public bool AplicaNota { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
