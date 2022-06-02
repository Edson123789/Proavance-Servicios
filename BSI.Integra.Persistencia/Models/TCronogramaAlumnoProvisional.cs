using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCronogramaAlumnoProvisional
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdCiudad { get; set; }
        public int Frecuencia { get; set; }
        public DateTime FechaInicio { get; set; }
        public bool Aprobado { get; set; }
        public string AprobadoPor { get; set; }
        public DateTime? FechaAprobado { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
