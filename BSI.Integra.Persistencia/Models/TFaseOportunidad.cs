using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFaseOportunidad
    {
        public TFaseOportunidad()
        {
            TDatoOportunidadAreaVenta = new HashSet<TDatoOportunidadAreaVenta>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int? NroMinutos { get; set; }
        public int? IdActividad { get; set; }
        public int? MaxNumDias { get; set; }
        public int? MinNumDias { get; set; }
        public int? TasaConversionEsperada { get; set; }
        public int? Meta { get; set; }
        public bool? Final { get; set; }
        public bool? ReporteMeta { get; set; }
        public bool? EnSeguimiento { get; set; }
        public bool? EsCierre { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public string Descripcion { get; set; }
        public bool? VisibleEnReporte { get; set; }

        public virtual ICollection<TDatoOportunidadAreaVenta> TDatoOportunidadAreaVenta { get; set; }
    }
}
