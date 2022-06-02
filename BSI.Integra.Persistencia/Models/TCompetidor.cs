using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCompetidor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int DuracionCronologica { get; set; }
        public int CostoNeto { get; set; }
        public int Precio { get; set; }
        public int IdMoneda { get; set; }
        public int IdInstitucionCompetidora { get; set; }
        public int IdPais { get; set; }
        public int IdCiudad { get; set; }
        public int? IdRegionCiudad { get; set; }
        public int IdAeaCapacitacion { get; set; }
        public int IdSubAreaCapacitacion { get; set; }
        public int IdCategoria { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
