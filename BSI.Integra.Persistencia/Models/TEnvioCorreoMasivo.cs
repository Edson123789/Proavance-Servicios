using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TEnvioCorreoMasivo
    {
        public int Id { get; set; }
        public int? IdActividadDetalleInicial { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdCentroCostoOportunidad { get; set; }
        public int IdPersonal { get; set; }
        public int? IdAlumno { get; set; }
        public int IdTipoEnvioCorreo { get; set; }
        public int? IdMandrilTipoEnvio { get; set; }
        public int? IdPlantilla { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
