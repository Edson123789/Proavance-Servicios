using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConfiguracionCerrarBnc
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdTipoDato { get; set; }
        public int IdOcurrencia { get; set; }
        public int? IdPlantilla { get; set; }
        public int? IdAsesor { get; set; }
        public int DiasMedia { get; set; }
        public int DiasAlta { get; set; }
        public int DiasMuyAlta { get; set; }
        public bool EjecutarDiasMedia { get; set; }
        public bool EjecutarDiasAlta { get; set; }
        public bool EjecutarDiasMuyAlta { get; set; }
        public bool EnviarCorreo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
