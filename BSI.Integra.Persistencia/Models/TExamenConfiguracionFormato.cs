using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TExamenConfiguracionFormato
    {
        public int Id { get; set; }
        public bool Activo { get; set; }
        public string ColorTextoTitulo { get; set; }
        public int? TamanioTextoTitulo { get; set; }
        public string ColorFondoTitulo { get; set; }
        public string TipoLetraTitulo { get; set; }
        public string ColorTextoEnunciado { get; set; }
        public int? TamanioTextoEnunciado { get; set; }
        public string ColorFondoEnunciado { get; set; }
        public string TipoLetraEnunciado { get; set; }
        public string ColorTextoRespuesta { get; set; }
        public int? TamanioTextoRespuesta { get; set; }
        public string ColorFondoRespuesta { get; set; }
        public string TipoLetraRespuesta { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
