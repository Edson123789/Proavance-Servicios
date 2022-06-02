using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConfiguracionRutinaBncObsoleto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int NumDiasProbabilidadMedia { get; set; }
        public int NumDiasProbabilidadAlta { get; set; }
        public int NumDiasProbabilidadMuyAlta { get; set; }
        public bool EjecutarRutinaProbabilidadMedia { get; set; }
        public bool EjecutarRutinaProbabilidadAlta { get; set; }
        public bool EjecutarRutinaProbabilidadMuyAlta { get; set; }
        public int IdOcurrenciaDestino { get; set; }
        public bool EjecutarRutinaEnviarCorreo { get; set; }
        public int IdPlantillaCorreo { get; set; }
        public int IdPersonalCorreoNoExistente { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
