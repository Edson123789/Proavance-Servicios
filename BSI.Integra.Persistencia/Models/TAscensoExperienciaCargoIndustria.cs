using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAscensoExperienciaCargoIndustria
    {
        public int Id { get; set; }
        public int IdAscenso { get; set; }
        public int AniosExperiencia { get; set; }
        public int IdCargo { get; set; }
        public int IdIndustria { get; set; }
        public int IdAreaTrabajo { get; set; }
        public string DescripcionPuestoAnterior { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid IdMigracion { get; set; }
    }
}
