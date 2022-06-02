using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAsignacionOportunidadLog
    {
        public int Id { get; set; }
        public int? IdAsignacionOportunidad { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdPersonalAnterior { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdCentroCostoAnt { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdAlumno { get; set; }
        public DateTime FechaLog { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdClasificacionPersona { get; set; }

        public virtual TAsignacionOportunidad IdAsignacionOportunidadNavigation { get; set; }
    }
}
