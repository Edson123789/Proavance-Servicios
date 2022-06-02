using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCrucigramaProgramaCapacitacionDetalle
    {
        public int Id { get; set; }
        public int IdCrucigramaProgramaCapacitacionDetalle { get; set; }
        public int NumeroPalabra { get; set; }
        public string Palabra { get; set; }
        public string Definicion { get; set; }
        public int Tipo { get; set; }
        public int ColumnaInicio { get; set; }
        public int FilaInicio { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
