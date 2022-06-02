using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConfiguracionAsignacionCoordinadorOportunidadOperaciones
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int IdCentroCosto { get; set; }
        public int? IdCentroCostoHijo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdEstadoMatricula { get; set; }
        public int? IdSubEstadoMatricula { get; set; }
    }
}
