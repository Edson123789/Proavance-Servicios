using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCertificadoGeneradoAutomatico
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPgeneral { get; set; }
        public DateTime? FechaEmision { get; set; }
        public int IdUrlBlockStorage { get; set; }
        public string ContentType { get; set; }
        public string NombreArchivo { get; set; }
        public int IdPgeneralConfiguracionPlantilla { get; set; }
        public int IdPespecifico { get; set; }
        public int IdPlantilla { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdCronogramaPagoTarifario { get; set; }
    }
}
