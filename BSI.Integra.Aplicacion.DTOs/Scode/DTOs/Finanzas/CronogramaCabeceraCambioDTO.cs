using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CronogramaCabeceraCambioDTO
    {
        //public int Id { get; set; }
        public int IdCronogramaTipoModificacion { get; set; }
        public int IdSolicitadoPor { get; set; }
        public int? IdAprobadoPor { get; set; }
        public bool Aprobado { get; set; }
        public bool Cancelado { get; set; }
        public string Observacion { get; set; }
        //public bool Estado { get; set; }
        //public string UsuarioCreacion { get; set; }
        //public string UsuarioModificacion { get; set; }
        //public DateTime FechaCreacion { get; set; }
        //public DateTime FechaModificacion { get; set; }
        //public byte[] RowVersion { get; set; }
        //public Guid? IdMigracion { get; set; }
    }
}
