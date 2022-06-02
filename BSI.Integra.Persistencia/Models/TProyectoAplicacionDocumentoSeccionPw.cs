using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TProyectoAplicacionDocumentoSeccionPw
    {
        public int Id { get; set; }
        public int IdDocumentoSeccionPw { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string Valor { get; set; }
        public int IdPlantillaPw { get; set; }
        public int IdDocumentoPw { get; set; }
        public int IdProyectoAplicacionEntregaVersionPw { get; set; }
        public DateTime FechaCalificacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
